using System.Collections.Generic;
using System.Diagnostics;
using System.Runtime.CompilerServices;

using Heirloom.Collections;
using Heirloom.Math;
using Heirloom.Collections.Spatial;

namespace Heirloom.Runtime
{
    public class CollisionSystem : ComponentSystem
    {
        private readonly BroadPhase<Collider> _broadphase;

        private readonly ObjectPool<RigidContact> _collisionPairsPool;
        private readonly List<RigidContact> _collisionPairs;

        private static float _invFixedDelta;

        internal CollisionSystem()
            : base(typeof(Collider))
        {
            _collisionPairsPool = new ObjectPool<RigidContact>(() => new RigidContact());
            _collisionPairs = new List<RigidContact>();

            _broadphase = new SpatialBroadPhase<Collider>();
        }

        protected override void OnAddEntity(Entity entity)
        {
            var collider = entity.GetComponent<Collider>();
            _broadphase.Add(collider);

            // Console.WriteLine("Adding Collider");
        }

        protected override void OnRemoveEntity(Entity entity)
        {
            var collider = entity.GetComponent<Collider>();
            _broadphase.Remove(collider);

            // Console.WriteLine("Removing Collider");
        }

        protected internal override void Update()
        {
            // Do Nothing
        }

        protected internal override void FixedUpdate()
        {
            // 
            _invFixedDelta = 1F / Time.FixedDelta;

            // Integrates Forces (Adjust Velocity)
            foreach (var collider in _broadphase)
            {
                // Update collider in broadphase structure
                collider.UpdateBroadPhase(_broadphase);

                var body = collider.GetComponent<RigidBody>();
                body?.IntegrateForces(Time.FixedDelta, gravity: (0, 30));
            }

            // == Detect Collision Pairs
            FindContacts();

            // == Apply Impulse
            SimulateContacts(_collisionPairs, 8, _invFixedDelta);

            // 
            foreach (var collider in _broadphase)
            {
                // Integrate velocities
                var body = collider.GetComponent<RigidBody>();
                body?.IntegrateVelocities(Time.FixedDelta);
            }
        }

        private void FindContacts()
        {
            // 
            foreach (var rc in _collisionPairs) { _collisionPairsPool.Recycle(rc); }
            _collisionPairs.Clear();

            // == Broad Phase - Query broad phase for all collision candidates
            foreach (var pair in _broadphase.QueryCollisionCandidates())
            {
                var a = pair.A;
                var b = pair.B;

                // == Narrow Phase - Check if the pair really do collide (and construct contacts)
                if (Collisions.CheckCollision(a.WorldShape, b.WorldShape, out var contacts))
                {
                    // == Trigger Collision Events

                    // Invoke collision callback on entities
                    InvokeCollisionCallback(a.Entity, a, b, contacts);
                    InvokeCollisionCallback(b.Entity, a, b, contacts);

                    // == Physics Response

                    // Get rigid bodies
                    var r1 = a.GetComponent<RigidBody>();
                    var r2 = b.GetComponent<RigidBody>();

                    // If no rigid bodies found, skip physics response
                    if (r1 == null && r2 == null) { continue; }

                    // Get friction coefficients
                    var f1 = r1?.Friction ?? 1F;
                    var f2 = r2?.Friction ?? 1F;

                    // Compute aggregate friction
                    // todo: why is it this? Box2Dlite has this
                    var friction = Calc.Sqrt(f1 * f2);

                    // For each contact in the manifold
                    for (var i = 0; i < contacts.Count; i++)
                    {
                        // 
                        var contact = _collisionPairsPool.Request();
                        contact.Set(r1, r2, contacts, i, friction);

                        // Append contact to list
                        _collisionPairs.Add(contact);
                    }
                }
            }
        }

        private static void InvokeCollisionCallback(Entity entity, Collider a, Collider b, Manifold contacts)
        {
            // 
            foreach (var callback in entity.GetInherited<ICollisionCallback>())
            {
                callback.OnCollision(a, b, in contacts);
            }
        }

        private static void SimulateContacts(IEnumerable<RigidContact> contacts, int iterations, float invDelta)
        {
            for (var i = 0; i < iterations; i++)
            {
                foreach (var c in contacts)
                {
                    var b1 = c.Body1;
                    var b2 = c.Body2;

                    // == Normal Impulse

                    // Relative velocity at contact
                    var dv = ComputeRelativeVelocity(b1, b2, c);

                    var dPn = c.MassN * (Calc.Max(0, c.Bias * invDelta) - Vector.Dot(dv, (Vector) c.Contact.Normal));
                    dPn = Calc.Max(dPn, 0.0f);

                    // Apply contact impulse
                    b1?.ApplyImpulse(-dPn * c.Contact.Normal, c.Contact.Position);
                    b2?.ApplyImpulse(+dPn * c.Contact.Normal, c.Contact.Position);

                    // == Tangent Impulse

                    var tangent = c.Contact.Normal.Perpendicular;
                    var maxPt = c.Friction * dPn;

                    // Relative velocity at contact
                    var dv2 = ComputeRelativeVelocity(b1, b2, c);

                    var dPt = c.MassT * -Vector.Dot(dv2, tangent);
                    dPt = Calc.Clamp(dPt, -maxPt, maxPt);

                    // Apply contact impulse
                    b1?.ApplyImpulse(-dPt * tangent, c.Contact.Position);
                    b2?.ApplyImpulse(+dPt * tangent, c.Contact.Position);
                }
            }
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        private static Vector ComputeRelativeVelocity(RigidBody b1, RigidBody b2, RigidContact rc)
        {
            var re = Vector.Zero;
            if (b1 != null) { re -= b1.Velocity + Vector.Cross(b1.AngularVelocity, rc.R1); }
            if (b2 != null) { re += b2.Velocity + Vector.Cross(b2.AngularVelocity, rc.R2); }
            return re;
        }

        #region Check / Manual

        public bool Overlaps(Rectangle bounds)
        {
            // For everything in the region
            foreach (var other in _broadphase.Query(bounds))
            {
                var b = other.Shape;
                var bx = other.Transform.Matrix;

                // 
                if (other.Bounds.Overlaps(in bounds))
                {
                    // Input rectangle does overlap some collider
                    return true;
                }
            }

            // Input rectangle does not overlap any collider
            return false;
        }

        public bool CheckCollision(Collider collider)
        {
            var a = collider.Shape;
            var ax = collider.Transform.Matrix;

            // For everything in the region
            foreach (var other in _broadphase.Query(collider))
            {
                var b = other.Shape;
                var bx = other.Transform.Matrix;

                // Test precise bounds
                if (Collisions.Overlaps(a, b))
                {
                    // A collision was determined
                    return true;
                }
            }

            return false;
        }

        //public bool CheckCollision(IShape a, Matrix ax)
        //{
        //    // Compute the shapes bounds
        //    var bounds = a.ComputeBounds(ax);

        //    // For everything in the region
        //    foreach (var body in _broadphase.Query(bounds))
        //    {
        //        var b = body.Shape;
        //        var bx = body.Transform.Matrix;

        //        // Test precise bounds
        //        if (Collisions.CheckCollision(a, in ax, b, in bx))
        //        {
        //            // A collision was determined
        //            return true;
        //        }
        //    }

        //    return false;
        //}

        public bool Raycast(in Ray ray, float maxDistance)
        {
            foreach (var body in _broadphase.Query(ray, maxDistance))
            {
                if (Polygon.Raycast(body.WorldShape, in ray))
                {
                    return true;
                }
            }

            return false;
        }

        public bool Raycast(in Ray ray, float maxDistance, out Contact hit)
        {
            var best = maxDistance;
            hit = default;

            foreach (var body in _broadphase.Query(ray, maxDistance))
            {
                var ax = body.Transform.Matrix;
                var a = body.WorldShape;

                if (Polygon.Raycast(a, in ray, out var h))
                {
                    if (h.Depth < best)
                    {
                        best = h.Depth;
                        hit = h;
                    }
                }
            }

            return best < maxDistance;
        }

        public IEnumerable<Collider> Query(Ray ray, float maxDistance)
        {
            return _broadphase.Query(ray, maxDistance);
        }

        public IEnumerable<Collider> Query(Collider collider)
        {
            return _broadphase.Query(collider);
        }

        public IEnumerable<Collider> Query(Rectangle region)
        {
            return _broadphase.Query(region);
        }

        #endregion

        public override void Dispose()
        {
            // ...?
        }

        internal sealed class RigidContact
        {
            public RigidBody Body1;
            public RigidBody Body2;

            public Vector R1;
            public Vector R2;

            public float MassN;
            public float MassT;
            public float Bias;

            public float Friction;

            public Contact Contact;

            internal void Set(RigidBody body1, RigidBody body2, in Manifold manifold, int index, float friction)
            {
                Body1 = body1;
                Body2 = body2;

                // Assert both bodies are not null
                Debug.Assert(!(Body1 == null && Body2 == null), "Both bodies must not be null when building a rigid contact.");

                // 
                Contact = new Contact(
                    manifold.GetPosition(index),
                    manifold.Normal,
                    manifold.GetSeparation(index));

                Bias = -0.2F * Calc.Min(0, Contact.Depth + 0.00F);

                Friction = friction;

                // == Rigid Body Computation

                var massN = 0F;
                var massT = 0F;

                var tangent = Contact.Normal.Perpendicular;

                if (body1 != null)
                {
                    R1 = Contact.Position - body1.Position;

                    var rn1 = Vector.Dot(R1, (Vector) Contact.Normal);
                    var rt1 = Vector.Dot(R1, tangent);
                    var dr1 = Vector.Dot(R1, R1);

                    massN += body1.InvMass + body1.InvRotationalInertia * (dr1 - rn1 * rn1);
                    massT += body1.InvMass + body1.InvRotationalInertia * (dr1 - rt1 * rt1);
                }
                else
                {
                    R1 = Vector.Zero;
                }

                if (body2 != null)
                {
                    R2 = Contact.Position - body2.Position;

                    var rn = Vector.Dot(R2, (Vector) Contact.Normal);
                    var rt = Vector.Dot(R2, tangent);
                    var dr = Vector.Dot(R2, R2);

                    massN += body2.InvMass + body2.InvRotationalInertia * (dr - rn * rn);
                    massT += body2.InvMass + body2.InvRotationalInertia * (dr - rt * rt);
                }
                else
                {
                    R2 = Vector.Zero;
                }

                // Note: Division should be safe because zero should not happen!
                // Static pairs should not produce contacts (avoids 0 + 0) and at least one
                // rigid body should be defined by the assert above.

                MassN = 1F / massN;
                MassT = 1F / massT;
            }
        }
    }
}
