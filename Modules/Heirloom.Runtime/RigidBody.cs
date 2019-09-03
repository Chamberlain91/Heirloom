using System;

using Heirloom.Math;

namespace Heirloom.Runtime
{
    public class RigidBody : Component
    {
        private float _mass;

        public RigidBody()
        {
            Mass = 1F;
        }

        public Collider Collider { get; private set; }

        public Vector Position
        {
            get => Transform.Position;
            set => Transform.Position = value;
        }

        public float Rotation
        {
            get => Transform.Rotation;
            set => Transform.Rotation = value;
        }

        public Vector Velocity { get; set; }

        public float AngularVelocity { get; set; }

        public Vector Force { get; set; }

        public float Torque { get; set; }

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
                    RotationalInertia = Mass * (Collider?.Shape?.Area ?? 1F) / 6F;
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

        public float Friction { get; set; } = 0.2F;

        internal float InvMass { get; private set; }

        internal float RotationalInertia { get; private set; }

        internal float InvRotationalInertia { get; private set; }

        protected internal override void Start()
        {
            Collider = RequireComponent<Collider>();
        }

        #region Apply Impulse / Forces

        public void ApplyImpulse(Vector impulse)
        {
            Velocity += InvMass * impulse;
        }

        public void ApplyImpulse(Vector impulse, Vector point)
        {
            Velocity += InvMass * impulse;
            AngularVelocity += InvRotationalInertia * Vector.Cross(point - Position, impulse);
        }

        public void ApplyForce(Vector force)
        {
            Force += force;
        }

        public void ApplyForce(Vector force, Vector point)
        {
            Force += force;
            Torque += Vector.Cross(point - Position, force);
        }

        #endregion

        #region Integrate

        internal void IntegrateForces(float dt, in Vector gravity)
        {
            // Non dynamic
            if (!Collider.IsDynamic) { return; }

            // Apply forces
            if (InvMass > 0) { Velocity += dt * gravity; }
            Velocity += dt * (InvMass * Force);

            AngularVelocity += dt * InvRotationalInertia * Torque;
        }

        internal void IntegrateVelocities(float dt)
        {
            // Apply velocities
            Position += dt * Velocity;
            Rotation += dt * AngularVelocity;

            // Clear forces
            Force.Set(0.0f, 0.0f);
            Torque = 0.0f;
        }

        #endregion
    }
}
