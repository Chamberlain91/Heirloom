using System;
using System.Linq;
using System.Runtime.CompilerServices;

using Heirloom.Collections.Spatial;
using Heirloom.Math;

namespace Heirloom.Examples.Physics
{
    public class PhysicsBody : ISpatialObject
    {
        private const float StaticMass = float.MaxValue;

        private Vector _position, _lastPosition;
        private Vector _scale;
        private float _rotation, _lastRotation;
        private float _mass;

        private bool _changed;
        private bool _firstIteration;

        public static readonly Vector Gravity = new Vector(0, 3);

        public PhysicsBody(IPolygon shape, Vector position, bool dynamic)
            : this(shape, position, dynamic ? shape.Area : StaticMass)
        { }

        public PhysicsBody(IPolygon shape, Vector position, float mass)
        {
            Shape = shape ?? throw new ArgumentNullException(nameof(shape));
            WorldShape = Shape.ToArray();

            Scale = Vector.One;
            Position = position;
            Mass = mass;

            _firstIteration = true;
            _changed = true;
        }

        public IPolygon Shape { get; }

        internal Vector[] WorldShape { get; }

        public bool IsStatic => Mass >= float.MaxValue;

        public bool IsDynamic => !IsStatic;

        public float Mass
        {
            get => _mass;

            set
            {
                // 
                if (value <= 0F) { throw new ArgumentException($"Mass must be a positive number."); }

                // If mass wasn't the static constant
                if (value < float.MaxValue)
                {
                    // Set mass
                    _mass = value;
                    InvMass = 1F / Mass;

                    // Compute I...
                    RotationalInertia = Mass * Shape.Area / 6F;
                    InvRotationalInertia = 1F / RotationalInertia;
                }
                else
                {
                    _mass = float.MaxValue;
                    InvMass = 0F;

                    RotationalInertia = float.MaxValue;
                    InvRotationalInertia = 0F;
                }
            }
        }

        internal float InvMass { get; private set; }

        internal float RotationalInertia { get; private set; }

        internal float InvRotationalInertia { get; private set; }

        public float Friction { get; set; } = 0.2F;

        public Vector Position
        {
            get => _position;

            set
            {
                _position = value;
                _changed = true;
            }
        }

        public float Rotation
        {
            get => _rotation;

            set
            {
                _rotation = value;
                _changed = true;
            }
        }

        public Vector Scale
        {
            get => _scale;

            set
            {
                _scale = value;
                _changed = true;
            }
        }

        public Vector Velocity { get; set; }

        public float AngularVelocity { get; set; }

        public Vector Force { get; set; }

        public float Torque { get; set; }

        public Matrix Transform => Matrix.CreateTransform(Position, Rotation, Scale);
         
        public Rectangle Bounds { get; private set; }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public void IntegrateVelocities(float dt)
        {
            Position += dt * Velocity;
            Rotation += dt * AngularVelocity;

            Force.Set(0.0f, 0.0f);
            Torque = 0.0f;
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public void IntegrateForces(float dt)
        {
            if (IsStatic) { return; }

            Velocity += dt * (Gravity + InvMass * Force);
            AngularVelocity += dt * InvRotationalInertia * Torque;
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public void ApplyImpulse(Vector impulse)
        {
            Velocity += InvMass * impulse;
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public void ApplyImpulse(Vector impulse, Vector point)
        {
            Velocity += InvMass * impulse;
            AngularVelocity += InvRotationalInertia * Vector.Cross(point - Position, impulse);
        }

        public void UpdateBroadPhase(BroadPhase<PhysicsBody> broadPhase)
        {
            if (_changed)
            {
                //
                ComputeShape();

                // 
                ComputeBounds();

                // Update broad phase structure 
                broadPhase.Update(this);

                // 
                if (_firstIteration)
                {
                    _firstIteration = false;

                    // Ensures the initial velocity of a static/kinematic body
                    // is zero instead of computing the offset from zero.
                    _lastPosition = Position;
                    _lastRotation = Rotation;
                }

                // 
                if (IsStatic) // todo: Kind == PhysicsKind.Kinematic
                {
                    // Compute temporal velocity of static objects.
                    // They can only have differing position by user input.

                    // todo: Bodies having 3 classes of being, Dynamic, Kinematic and Static
                    //
                    // * Dynamic   - Standard Body w/ Mass            (ie. box, debris, etc)
                    // * Kinematic - Static Body w/ Temporal Velocity (ie. player, moving platform, etc?)
                    // * Static    - Static Body                      (ie. platforms, walls, etc)
                    // 

                    // Compute temporal velocity
                    // todo: avoid computation if velocities were manually set?
                    Velocity = Position - _lastPosition;
                    AngularVelocity = Rotation - _lastRotation;
                }

                // 
                _lastPosition = Position;
                _lastRotation = Rotation;

                // 
                _changed = false;
            }
        }

        private void ComputeShape()
        {
            // Compute world position of each vertex
            for (var i = 0; i < Shape.Count; i++)
            {
                WorldShape[i] = Transform * Shape[i];
            }

            // 
            if (!Polygon.IsConvex(WorldShape, 0))
            {
                Array.Reverse(WorldShape);
            }
        }

        private void ComputeBounds()
        {
            var bounds = Rectangle.InvertedInfinite;

            for (var i = 0; i < Shape.Count; i++)
            {
                bounds.Merge(WorldShape[i]);
            }

            Bounds = bounds;
        }
    }
}
