using System;
using System.Collections.Generic;
using System.Runtime.CompilerServices;

using Heirloom.Collections;
using Heirloom.Collections.Spatial;
using Heirloom.Math;

namespace Heirloom.Examples.Physics
{
    class PhysicsWorld
    {
        private readonly BroadPhase<PhysicsBody> _bodies;

        private readonly ObjectPool<RigidContact> _collisionPairsPool;
        private readonly List<RigidContact> _collisionPairs;

        public PhysicsWorld()
        {
            _collisionPairsPool = new ObjectPool<RigidContact>(() => new RigidContact());
            _collisionPairs = new List<RigidContact>();

            // Creates a sparse grid w/ 0.5 sized cells, value is only suitable for the demo.
            // The size should be approximately be the average size of your objects.

            // Faster, but grid size affects performance / objects, raycast implmented with 
            // bresenham line drawing so infinite distance is infinite work.
            _bodies = new SparseGridBroadPhase<PhysicsBody>(0.5F);

            // Slower, but scale independant and suppports infinite raycast
            // note: appears to be 2x slower than sparse grid
            // _bodies = new SpatialBroadPhase<PhysicsBody>();
        }

        public IReadOnlyList<PhysicsBody> Bodies => _bodies;

        public int NumberOfCollisions { get; private set; }

        public int NumberOfCollisionTests { get; private set; }

        public void Add(PhysicsBody body)
        {
            _bodies.Add(body);
        }

        public IEnumerable<PhysicsBody> Query(PhysicsBody body)
        {
            return _bodies.Query(body);
        }

        public void Simulate(float dt)
        {
            var inv_dt = 1F / dt;

            // Integrates Forces (Adjust Velocity)
            foreach (var body in _bodies)
            {
                body.IntegrateForces(dt);
            }

            // == Broad Phase (Detect Collision Pairs) 
            FindContacts();

            // == Collision Response (Apply Impulse) 
            SimulateContacts(_collisionPairs, 8, inv_dt);

            // 
            foreach (var body in _bodies)
            {
                body.IntegrateVelocities(dt);
                body.UpdateBroadPhase(_bodies);
            }
        }

        private void FindContacts()
        {
            // 
            foreach (var rc in _collisionPairs) { _collisionPairsPool.Recycle(rc); }
            _collisionPairs.Clear();

            NumberOfCollisionTests = 0;
            NumberOfCollisions = 0;

            // Query broad phase for all collision candidates
            foreach (var pair in _bodies.QueryCollisionCandidates())
            {
                var b1 = pair.A;
                var b2 = pair.B;

                // 
                NumberOfCollisionTests++;

                // No physics response needed, both are static
                if (b1.IsStatic && b2.IsStatic) { continue; }

                // Check if the pair really do collide (and construct contacts)
                if (Collisions.CheckCollision(b1.WorldShape, b2.WorldShape, out var manifold))
                {
                    // 
                    NumberOfCollisions++;

                    // Compute aggregate friction
                    var friction = Calc.Sqrt(b1.Friction * b2.Friction);

                    // For each contact in the manifold
                    for (var i = 0; i < manifold.Count; i++)
                    {
                        // 
                        var contact = _collisionPairsPool.Request();
                        contact.Set(b1, b2, manifold, i, friction);

                        // Append contact to list
                        _collisionPairs.Add(contact);
                    }
                }
            }
        }

        private static void SimulateContacts(IEnumerable<RigidContact> contacts, int iterations, float invdt)
        {
            for (var i = 0; i < iterations; i++)
            {
                foreach (var rc in contacts)
                {
                    var b1 = rc.B1;
                    var b2 = rc.B2;

                    // == Normal Impulse

                    // Relative velocity at contact
                    var dv = ComputeRelativeVelocity(b1, b2, in rc.Contact.Position);

                    var dPn = rc.MassNormal * (Calc.Max(0, rc.Bias * invdt) - Vector.Dot(dv, rc.Contact.Normal));
                    dPn = Calc.Max(dPn, 0.0f);

                    // Apply contact impulse
                    b1.ApplyImpulse(-dPn * rc.Contact.Normal, rc.Contact.Position);
                    b2.ApplyImpulse(+dPn * rc.Contact.Normal, rc.Contact.Position);

                    // == Tangent Impulse

                    var tangent = rc.Contact.Normal.Perpendicular;
                    var maxPt = rc.Friction * dPn;

                    // Relative velocity at contact
                    var dv2 = ComputeRelativeVelocity(b1, b2, in rc.Contact.Position);

                    var dPt = rc.MassTangent * -Vector.Dot(dv2, tangent);
                    dPt = Calc.Clamp(dPt, -maxPt, maxPt);

                    // Apply contact impulse
                    b1.ApplyImpulse(-dPt * tangent, rc.Contact.Position);
                    b2.ApplyImpulse(+dPt * tangent, rc.Contact.Position);
                }
            }
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        private static Vector ComputeRelativeVelocity(PhysicsBody b1, PhysicsBody b2, in Vector point)
        {
            // 
            var r1 = point - b1.Position;
            var r2 = point - b2.Position;

            // b2 linear + b2 tangental at contact - b1 linear - b1 tangental at contact
            return b2.Velocity + Vector.Cross(b2.AngularVelocity, r2)
                 - b1.Velocity - Vector.Cross(b1.AngularVelocity, r1);
        }

        internal sealed class RigidContact
        {
            public PhysicsBody B1;
            public PhysicsBody B2;

            public float MassNormal;
            public float MassTangent;
            public float Bias;

            public float Friction;

            public Contact Contact;

            internal void Set(PhysicsBody b1, PhysicsBody b2, in Manifold m, int mi, float friction)
            {
                B1 = b1 ?? throw new ArgumentNullException(nameof(b1));
                B2 = b2 ?? throw new ArgumentNullException(nameof(b2));

                // 
                Contact = new Contact(m.GetPosition(mi), m.Normal, m.GetSeparation(mi));

                Friction = friction;

                Bias = -0.2F * Calc.Min(0, Contact.Depth + 0.001F);

                // == Rigid Body Computation (can be avoided when physics response is not needed)

                var tangent = Contact.Normal.Perpendicular;

                // 
                var r1 = Contact.Position - b1.Position;
                var r2 = Contact.Position - b2.Position;

                var rn1 = Vector.Dot(r1, Contact.Normal);
                var rn2 = Vector.Dot(r2, Contact.Normal);

                var rt1 = Vector.Dot(r1, tangent);
                var rt2 = Vector.Dot(r2, tangent);

                var dr1 = Vector.Dot(r1, r1);
                var dr2 = Vector.Dot(r2, r2);

                var invMassSum = b1.InvMass + b2.InvMass;

                var kNormal = invMassSum;
                kNormal += b1.InvRotationalInertia * (dr1 - rn1 * rn1) + b2.InvRotationalInertia * (dr2 - rn2 * rn2);

                MassNormal = 1F / kNormal;

                var kTangent = invMassSum;
                kTangent += b1.InvRotationalInertia * (dr1 - rt1 * rt1) + b2.InvRotationalInertia * (dr2 - rt2 * rt2);

                MassTangent = 1F / kTangent;
            }
        }
    }
}
