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
        private float _lastTime;

        // Collision
        public Rectangle Shape { get; set; }

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

        protected override void OnAdded()
        {
            _position = Transform.Position;
        }

        public Rectangle ComputeCollisionBounds()
        {
            var left = _position.X + Shape.X;
            var top = _position.Y + Shape.Y;

            // Compute player bounds
            return new Rectangle(left, top, Shape.Width, Shape.Height);
        }

        protected override void Update(float dt)
        {
            _lastTime += dt;
            Transform.Position = Vector.Lerp(_lastPosition, _position, _lastTime / Scene.FixedDeltaTime);
        }

        protected override void FixedUpdate(float ft)
        {
            _lastPosition = _position;
            _lastTime = 0;

            // Apply Gravity
            _acceleration.Y += 720;

            // Integrate velocity
            _velocity += _acceleration * ft;
            _acceleration = Vector.Zero;

            // Clamp max velocity
            if (Calc.Abs(_velocity.X) > 240) { _velocity.X = Calc.Sign(_velocity.X) * 240; }
            if (Calc.Abs(_velocity.Y) > 480) { _velocity.Y = Calc.Sign(_velocity.Y) * 480; }

            // == Collision Phase

            // Reset collision state
            HasGroundCollision = false;
            HasWallCollision = false;

            var map = Scene.GetEntity<Map>();

            // Integrate Position (Y Component)
            _position += (0, _velocity.Y * ft);
            ProcessVerticalCollisions(map);

            // Integrate Position (X Component)
            _position += (_velocity.X * ft, 0);
            ProcessHorizontalCollisions(map);
        }

        private void ProcessHorizontalCollisions(Map map)
        {
            // Get player collision bounds
            var bounds = ComputeCollisionBounds();

            // Convert world position in to map coordinates
            var mapCoord = (IntVector) (_position / map.TileSize);

            // For each collider near the player on the map
            // todo: Use a more universal selection so other solid entities can be considered
            foreach (var other in map.GetCollisionBounds(mapCoord.X, mapCoord.Y))
            {
                // Player is outside the tile collision row
                if (bounds.Bottom <= other.Top) { continue; }
                if (bounds.Top >= other.Bottom) { continue; }

                // Moving left into tile
                if (_velocity.X < 0 && bounds.Right > other.Right && bounds.Left < other.Right)
                {
                    _position += (other.Right - bounds.Left + CollisionTolerance, 0);
                    _velocity.X = 0;

                    HasWallCollision = true;
                }

                // Moving right into tile
                if (_velocity.X > 0 && bounds.Left < other.Left && bounds.Right > other.Left)
                {
                    _position += (other.Left - bounds.Right - CollisionTolerance, 0);
                    _velocity.X = 0;

                    HasWallCollision = true;
                }
            }
        }

        private void ProcessVerticalCollisions(Map map)
        {
            // Get player collision bounds
            var bounds = ComputeCollisionBounds();

            // Convert world position in to map coordinates
            var mapCoord = (IntVector) (_position / map.TileSize);

            // For each collider near the player on the map
            // todo: Use a more universal selection so other solid entities can be considered
            foreach (var other in map.GetCollisionBounds(mapCoord.X, mapCoord.Y))
            {
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
    }
}
