using System;
using System.Collections.Generic;
using Heirloom.Game;
using Heirloom.Math;

namespace Examples.Game
{
    public class PhysicsComponent : Component
    {
        private const float CollisionTolerance = 0.1F;

        // For integration
        private Vector _velocity;
        private Vector _acceleration;
        private Vector _position;

        // For interpolation
        private Vector _lastPosition;
        private float _interTime;

        private readonly List<CollisionInfo> _wallCollisions = new List<CollisionInfo>();

        // Collision
        public Collider Collider => Entity.GetComponent<Collider>();

        public float GravityMultiplier { get; set; } = 1F;

        /// <summary>
        /// Gets or sets the velocity.
        /// </summary>
        public Vector Velocity
        {
            get => _velocity;
            set => _velocity = value;
        }

        /// <summary>
        /// Gets or sets the acceleration.
        /// </summary>
        public Vector Acceleration
        {
            get => _acceleration;
            set => _acceleration = value;
        }

        /// <summary>
        /// Gets or sets the position.
        /// </summary>
        public Vector Position
        {
            get => _position;
            set
            {
                if (Transform != null) { Transform.Position = value; }
                _position = value;
            }
        }

        /// <summary>
        /// Gets if a collision with the ground has been detected.
        /// </summary>
        public bool HasGroundCollision { get; private set; }

        /// <summary>
        /// Gets if a collision with a wall has been detected.
        /// </summary>
        public bool HasWallCollision { get; private set; }

        /// <summary>
        /// Event triggered when a collision with the ground is detected.
        /// </summary>
        public event Action GroundCollision;

        /// <summary>
        /// Event triggered when a collision with a wall is detected.
        /// </summary>
        public event Action<Collider, Vector> WallCollision;

        protected override void OnAdded()
        {
            _position = Transform.Position;
        }

        protected override void Update(float dt)
        {
            _interTime += dt;
            Transform.Position = Vector.Lerp(_lastPosition, _position, _interTime / Scene.FixedDeltaTime);
        }

        protected override void FixedUpdate(float ft)
        {
            _lastPosition = _position;
            _interTime = 0;

            // Apply Gravity
            _acceleration.Y += 1440 * GravityMultiplier;

            // Integrate velocity
            _velocity += _acceleration * ft;
            _acceleration = Vector.Zero;

            // Clamp max velocity
            if (Calc.Abs(_velocity.X) > 240) { _velocity.X = Calc.Sign(_velocity.X) * 240; }
            if (Calc.Abs(_velocity.Y) > 480) { _velocity.Y = Calc.Sign(_velocity.Y) * 480; }

            // == Collision Phase

            var wasGroundCollision = HasGroundCollision;
            var wasWallCollision = HasWallCollision;

            // Reset collision state
            HasGroundCollision = false;
            HasWallCollision = false;

            _wallCollisions.Clear();

            // Integrate Position (Y Component)
            _position += (0, _velocity.Y * ft);
            ProcessVerticalCollisions();

            // Integrate Position (X Component)
            _position += (_velocity.X * ft, 0);
            ProcessHorizontalCollisions();

            // == Collision Callbacks

            // Has the ground collision state changed?
            if (wasGroundCollision != HasGroundCollision && HasGroundCollision)
            {
                GroundCollision?.Invoke();
            }

            // Has the wall collision state changed?
            if (wasWallCollision != HasWallCollision && HasWallCollision)
            {
                foreach (var collision in _wallCollisions)
                {
                    WallCollision?.Invoke(collision.Collider, collision.Normal);
                }
            }
        }

        private void ProcessHorizontalCollisions()
        {
            // Get player collision bounds
            var bounds = Collider.Bounds;

            // For each collider near the player on the map
            // todo: Use a more universal selection so other solid entities can be considered
            foreach (var collider in Collider.GetColliders(Collider))
            {
                var other = collider.Bounds;

                // Player is outside the tile collision row
                if (bounds.Bottom <= other.Top) { continue; }
                if (bounds.Top >= other.Bottom) { continue; }

                // Moving left into tile
                if (_velocity.X < 0 && bounds.Right > other.Right && bounds.Left < other.Right)
                {
                    var overlapAmount = other.Right - bounds.Left;
                    _position += (overlapAmount + CollisionTolerance, 0);
                    _velocity.X = 0;

                    HasWallCollision = true;
                    _wallCollisions.Add(new CollisionInfo
                    {
                        Normal = (overlapAmount, 0),
                        Collider = collider
                    });
                }

                // Moving right into tile
                if (_velocity.X > 0 && bounds.Left < other.Left && bounds.Right > other.Left)
                {
                    var overlapAmount = other.Left - bounds.Right;
                    _position += (overlapAmount - CollisionTolerance, 0);
                    _velocity.X = 0;

                    HasWallCollision = true;
                    _wallCollisions.Add(new CollisionInfo
                    {
                        Normal = (overlapAmount, 0),
                        Collider = collider
                    });
                }
            }
        }

        private void ProcessVerticalCollisions()
        {
            // Get player collision bounds
            var bounds = Collider.Bounds;

            // For each collider near the player on the map
            // todo: Use a more universal selection so other solid entities can be considered
            foreach (var collider in Collider.GetColliders(Collider))
            {
                var other = collider.Bounds;

                // Player is outside the tile collision column
                if (bounds.Right <= other.Left) { continue; }
                if (bounds.Left >= other.Right) { continue; }

                // Moving into ground
                if (_velocity.Y > 0 && bounds.Top < other.Top && bounds.Bottom > other.Top)
                {
                    _position += (0, other.Top - bounds.Bottom - CollisionTolerance);
                    _velocity.Y = 0;

                    HasGroundCollision = true;
                }

                // Moving into ceiling
                if (_velocity.Y < 0 && bounds.Bottom > other.Bottom && bounds.Top < other.Bottom)
                {
                    _position += (0, other.Bottom - bounds.Top + CollisionTolerance);
                    _velocity.Y = 0;
                }
            }
        }

        private struct CollisionInfo
        {
            public Collider Collider;
            public Vector Normal;
        }
    }
}
