using System;

using Heirloom;

namespace Examples.Physics
{
    public sealed class RigidBody
    {
        // Body Mass Information
        internal float Inertia;
        internal float InverseIntertia;
        internal float Mass;
        internal float InverseMass;
        private float _density;

        public RigidBody(IShape shape, Vector position, Vector scale, float density = 1F)
        {
            Collider = CreateCollider(shape);
            Collider.RegisterBody(this);

            Scale = scale;

            Position = position;
            Density = density;
            UpdateCollider();
        }

        public Vector Scale { get; set; } = Vector.One;

        public Vector Position { get; set; }
        public Vector Velocity { get; set; }
        public Vector Force { get; set; }

        public float Rotation { get; set; }
        public float AngularVelocity { get; set; }
        public float Torque { get; set; }

        // Material Information
        public float StaticFriction { get; set; } = 0.33F;
        public float DynamicFriction { get; set; } = 0.33F;
        public float Restitution { get; set; } = 0.5F;

        public float Density
        {
            get => _density;

            set
            {
                _density = value;

                // The density changed, so we need to recompute mass data
                Collider.ComputeMassData(this);
            }
        }

        public bool IsStatic => Density >= float.MaxValue;

        public Collider Collider { get; private set; }

        public Rectangle Bounds { get; private set; }

        private Collider CreateCollider(IShape shape)
        {
            if (shape is Circle c)
            {
                return new CircleCollider(c);
            }
            else if (shape is Polygon p)
            {
                return new PolygonCollider(p);
            }
            else if (shape is Rectangle r)
            {
                return new PolygonCollider(r);
            }
            else if (shape is Triangle t)
            {
                return new PolygonCollider(t);
            }
            else
            {
                throw new NotImplementedException($"Unable to use {shape.GetType().Name} as a collider shape.");
            }
        }

        public void ApplyForce(in Vector force)
        {
            Force += force;
        }

        public void ApplyImpulse(in Vector impulse, in Vector point)
        {
            Velocity += InverseMass * impulse;
            AngularVelocity += InverseIntertia * Vector.Cross(point, impulse);
        }

        internal void UpdateCollider()
        {
            Bounds = Rectangle.Transform(Collider.Shape.Bounds, Collider.Matrix);
        }
    }
}
