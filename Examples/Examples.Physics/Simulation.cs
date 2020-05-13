using System;
using System.Collections.Generic;

using Heirloom;
using Heirloom.Collections;
using Heirloom.Geometry;

using static Heirloom.Calc;
using static Heirloom.Vector;

namespace Examples.Physics
{
    public sealed class Simulation
    // Code here has been refactored is ultimately has just been ported to Heirloom
    // from Randy Gaul's "Impulse Engine" (http://RandyGaul.net)
    // The zlib license requires "state changes and keep the notice",
    // hopefully this counts as that notice.
    {
        private readonly ObjectPool<Manifold> _manifoldPool = new ObjectPool<Manifold>();

        private readonly ISpatialCollection<RigidBody> _bodies = new SparseGridSpatialCollection<RigidBody>(32);
        private readonly List<Manifold> _contacts = new List<Manifold>();
        private int _iterations;

        public Simulation(Vector gravity, int iterations = 7)
        {
            Iterations = iterations;
            Gravity = gravity;
        }

        public int Iterations
        {
            get => _iterations;

            set
            {
                if (value <= 0) { throw new ArgumentException("Simulation must have an iteration count greater than zero."); }
                _iterations = value;
            }
        }

        public Vector Gravity { get; set; }

        public RigidBody Add(RigidBody body)
        {
            _bodies.Add(body, body.Bounds);
            return body;
        }

        public void Step(float dt)
        {
            // Clear Contacts List
            _contacts.Clear();

            // Prepare each body for this frame
            foreach (var body in _bodies)
            {
                // Updates Body Shape/Bounds
                body.UpdateCollider();

                // Update Spatial Structure
                // todo: somehow optimize for static / things that did not move
                _bodies.Update(body, body.Bounds);
            }

            // Broad Phase
            foreach (var a in _bodies)
            {
                foreach (var b in _bodies.Query(a.Bounds))
                {
                    // Don't process against self
                    if (a == b) { continue; }

                    // Don't process static pairs
                    if (a.IsStatic && b.IsStatic) { continue; }

                    // Narrow Phase?
                    if (Collision.Collide(a.Collider.WorldShape, b.Collider.WorldShape, out var data))
                    {
                        // Assembly manifold
                        var manifold = _manifoldPool.Request();
                        manifold.Set(a, b, data);

                        // Append manifold to contacts list
                        _contacts.Add(manifold);
                    }
                }
            }

            // Integrate forces
            foreach (var b in _bodies)
            {
                IntegrateForces(b, dt);
            }

            // Solve collisions
            for (var j = 0; j < _iterations; ++j)
            {
                for (var i = 0; i < _contacts.Count; ++i)
                {
                    _contacts[i].ApplyImpulse();
                }
            }

            // Integrate velocities
            foreach (var b in _bodies)
            {
                IntegrateVelocity(b, dt);
                IntegrateForces(b, dt);

                // Clear all forces
                b.Force.Set(0, 0);
                b.Torque = 0;
            }

            // Correct positions
            for (var i = 0; i < _contacts.Count; ++i)
            {
                _contacts[i].PositionalCorrection();
                _manifoldPool.Recycle(_contacts[i]);
            }
        }

        private void IntegrateForces(RigidBody b, float dt)
        {
            // Force does not apply to static objects.
            if (b.IsStatic) { return; }

            // 
            b.Velocity += (b.Force * b.InverseMass + Gravity) * (dt / 2.0f);
            b.AngularVelocity += b.Torque * b.InverseIntertia * (dt / 2.0f);
        }

        private void IntegrateVelocity(RigidBody b, float dt)
        {
            // Velocity does not apply to static objects.
            if (b.IsStatic) { return; }

            b.Position += b.Velocity * dt;
            b.Rotation += b.AngularVelocity * dt;
        }

        public void Render(GraphicsContext gfx)
        {
            gfx.Color = Color.White;
            foreach (var body in _bodies)
            {
                body.Collider.Draw(gfx);
            }

            //gfx.Color = Color.Red;
            //foreach (var body in _bodies)
            //{
            //    gfx.DrawRectOutline(body.Bounds);
            //}

            //gfx.Color = Color.Yellow;
            //foreach (var manifold in _contacts)
            //{
            //    for (var i = 0; i < manifold.Contact.Count; i++)
            //    {
            //        var c = manifold.Contact.GetPoint(i);
            //        gfx.DrawLine(c, c + manifold.Contact.Normal * manifold.Contact.Penetration);
            //    }
            //}

            gfx.PushState(true);
            gfx.DrawText($"Contacts: {_contacts.Count}\nBodies: {_bodies.Count}", (10, 10), Font.Default, 16);
            gfx.PopState();
        }

        private class Manifold
        {
            public RigidBody A;
            public RigidBody B;

            public CollisionData Contact;

            public float MixedRestitution;
            public float MixedDynamicFriction;
            public float MixedStaticFriction;

            internal void Set(RigidBody a, RigidBody b, CollisionData contact)
            {
                A = a;
                B = b;

                Contact = contact;

                // Mix restitution. Randy Gaul suggested min(a, b) which probably makes sense...
                // but this seemed alright visually speaking.
                MixedRestitution = Sqrt(A.Restitution * B.Restitution);

                // Calculate static and dynamic friction
                MixedStaticFriction = Sqrt(A.StaticFriction * B.StaticFriction);
                MixedDynamicFriction = Sqrt(A.DynamicFriction * B.DynamicFriction);
            }

            // Naive correction of positional penetration
            public void PositionalCorrection()
            {
                const float k_slop = 0.02f; // Penetration allowance
                const float percent = 0.33f; // Penetration percentage to correct

                var correction = Max(Contact.Penetration - k_slop, 0.0f) / (A.InverseMass + B.InverseMass) * Contact.Normal * percent;
                A.Position -= correction * A.InverseMass;
                B.Position += correction * B.InverseMass;
            }

            // Solve impulse and apply
            public void ApplyImpulse()
            {
                // Early out and positional correct if both objects have infinite mass
                if (NearEquals(A.InverseMass + B.InverseMass, 0))
                {
                    A.Velocity.Set(0, 0);
                    B.Velocity.Set(0, 0);
                    return;
                }

                for (var i = 0; i < Contact.Count; ++i)
                {
                    // Calculate radii from COM to contact
                    var ra = Contact.GetPoint(i) - A.Position;
                    var rb = Contact.GetPoint(i) - B.Position;

                    // Relative velocity
                    var rv = B.Velocity + Cross(B.AngularVelocity, rb) -
                             A.Velocity - Cross(A.AngularVelocity, ra);

                    // Relative velocity along the normal
                    var contactVel = Dot(rv, Contact.Normal);

                    // Do not resolve if velocities are separating
                    if (contactVel > 0) { return; }

                    var raCrossN = Cross(ra, Contact.Normal);
                    var rbCrossN = Cross(rb, Contact.Normal);
                    var invMassSum = A.InverseMass + B.InverseMass + (Pow(raCrossN, 2) * A.InverseIntertia) + (Pow(rbCrossN, 2) * B.InverseIntertia);

                    // Calculate impulse scalar
                    var j = -(1.0f + MixedRestitution) * contactVel;
                    j /= invMassSum;
                    j /= Contact.Count;

                    // Apply impulse
                    var impulse = Contact.Normal * j;
                    A.ApplyImpulse(-impulse, ra);
                    B.ApplyImpulse(impulse, rb);

                    // Friction impulse
                    rv = B.Velocity + Cross(B.AngularVelocity, rb) -
                         A.Velocity - Cross(A.AngularVelocity, ra);

                    var t = rv - (Contact.Normal * Dot(rv, Contact.Normal));
                    t.Normalize();

                    // j tangent magnitude
                    var jt = -Dot(rv, t);
                    jt /= Contact.Count;
                    jt /= invMassSum;

                    // Don't apply tiny friction impulses
                    if (NearEquals(jt, 0.0f)) { return; }

                    // Coulumb's law
                    Vector tangentImpulse;
                    if (Abs(jt) < j * MixedStaticFriction)
                    {
                        tangentImpulse = t * jt;
                    }
                    else
                    {
                        tangentImpulse = t * -j * MixedDynamicFriction;
                    }

                    // Apply friction impulse
                    A.ApplyImpulse(-tangentImpulse, ra);
                    B.ApplyImpulse(tangentImpulse, rb);
                }
            }
        }
    }
}
