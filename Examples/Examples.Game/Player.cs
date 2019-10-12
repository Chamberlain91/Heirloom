using System;
using Heirloom.Drawing;
using Heirloom.Game;
using Heirloom.Math;

using static Heirloom.Game.AssetDatabase;

namespace Examples.Game
{
    public class Player : Entity
    {
        private const float CollisionTolerance = 0.1F;

        public readonly SpriteComponent SpriteRenderer;

        private Vector _velocity;
        private bool _hasGroundCollision;
        private bool _hasWallCollision;
        private bool _canJump = false;

        private bool _moveLeft;
        private bool _moveRight;
        private bool _doJump;

        public Player()
        {
            SpriteRenderer = AddComponent(new SpriteComponent(GetAsset<Sprite>("player")));
        }

        public Rectangle GetCollisionBounds()
        {
            var left = Transform.Position.X - 24;
            var top = Transform.Position.Y;

            // Compute player bounds
            return new Rectangle(left, top, 48, 48);
        }

        protected override void Update(float dt)
        {
            // Check input buttons
            _moveLeft = Input.GetButton("a") == ButtonState.Down;
            _moveRight = Input.GetButton("d") == ButtonState.Down;
            _doJump = Input.GetButton("space") == ButtonState.Down;

            // Jump
            if (_doJump && _canJump)
            {
                _canJump = false;

                // 
                Transform.Position -= (0, 1);
                _velocity.Y -= 12;

                // 
                if (SpriteRenderer.Animation.Name != "jump")
                {
                    SpriteRenderer.Play("jump");
                }
            }

            // Movement
            if (_moveLeft || _moveRight)
            {
                // Flip sprite for movement direction
                if (_moveLeft) { Transform.Scale = (-1, 1); }
                else { Transform.Scale = (1, 1); }

                // If we can jump (thus on stable ground), set to walking animation.
                if (_canJump && SpriteRenderer.Animation.Name != "walk")
                {
                    SpriteRenderer.Play("walk");
                }
            }
            else
            {
                // If we can jump (thus on stable ground), set to idle animation
                if (_canJump && SpriteRenderer.Animation.Name != "idle")
                {
                    SpriteRenderer.Play("idle");
                }
            }
        }

        protected override void FixedUpdate()
        {
            // Collision Phase
            var map = Scene.GetEntity<Map>();

            // Reset collision state
            _hasGroundCollision = false;
            _hasWallCollision = false;
            _canJump = false;

            // Integrate Velocity Y Component
            Transform.Position += (0, _velocity.Y);
            ProcessVerticalCollisions(map);

            // Integrate Velocity X Component
            Transform.Position += (_velocity.X, 0);
            ProcessHorizontalCollisions(map);

            // Apply Gravity
            _velocity.Y += 0.5F;

            // Move character based on input
            if (_moveLeft) { _velocity.X -= 1; }
            else if (_moveRight) { _velocity.X += 1; }
            // If can jump (thus on stable ground), apply friction
            else if (_canJump)
            {
                _velocity.X *= 0.7F;
            }

            // Clamp max velocity
            if (Calc.Abs(_velocity.X) > 4) { _velocity.X = Calc.Sign(_velocity.X) * 4; }
            if (Calc.Abs(_velocity.Y) > 8) { _velocity.Y = Calc.Sign(_velocity.Y) * 8; }
        }

        private void ProcessHorizontalCollisions(Map map)
        {
            // Get player collision bounds
            var bounds = GetCollisionBounds();

            // Convert world position in to map coordinates
            var mapCoord = (IntVector) (Transform.Position / map.TileSize);

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
                    Transform.Position += (other.Right - bounds.Left + CollisionTolerance, 0);
                    _velocity.X = 0;

                    _hasWallCollision = true;
                }

                // Moving right into tile
                if (_velocity.X > 0 && bounds.Left < other.Left && bounds.Right > other.Left)
                {
                    Transform.Position += (other.Left - bounds.Right - CollisionTolerance, 0);
                    _velocity.X = 0;

                    _hasWallCollision = true;
                }
            }
        }

        private void ProcessVerticalCollisions(Map map)
        {
            // Get player collision bounds
            var bounds = GetCollisionBounds();

            // Convert world position in to map coordinates
            var mapCoord = (IntVector) (Transform.Position / map.TileSize);

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
                    Transform.Position += (0, other.Top - bounds.Bottom - CollisionTolerance);
                    _velocity.Y = 0;

                    _hasGroundCollision = true;
                    _canJump = true;
                }

                // Moving into ceiling
                if (_velocity.Y < 0 && bounds.Bottom > other.Bottom && bounds.Top < other.Bottom)
                {
                    Transform.Position += (0, other.Bottom - bounds.Top + CollisionTolerance);
                    _velocity.Y = 0;
                }
            }
        }
    }
}
