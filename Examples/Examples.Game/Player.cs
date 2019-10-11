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
            // Apply Gravity
            _velocity.Y += 680 * dt;

            // Collision Phase
            var map = Scene.GetEntity<Map>();

            // Reset collision state
            _hasGroundCollision = false;
            _hasWallCollision = false;
            _canJump = false;

            // Integrate Velocity Y Component
            Transform.Position += (0, _velocity.Y * dt);
            ProcessVerticalCollisions(map);

            // Integrate Velocity X Component
            Transform.Position += (_velocity.X * dt, 0);
            ProcessHorizontalCollisions(map);

            // Check input buttons
            var goLeft = Input.GetButton("a") == ButtonState.Down;
            var goRight = Input.GetButton("d") == ButtonState.Down;
            var doJump = Input.GetButton("space") == ButtonState.Down;

            // 
            if (Input.GetButton("r") == ButtonState.Pressed)
            {
                Transform.Position = (96, -96);
                _velocity = Vector.Zero;
            }

            // Jump
            if (doJump && _canJump)
            {
                _canJump = false;
                Transform.Position -= (0, 1);
                _velocity.Y -= 320;

                if (SpriteRenderer.Animation.Name != "jump")
                {
                    SpriteRenderer.Play("jump");
                }
            }

            // 
            if (goLeft || goRight)
            {
                // Apply movement
                if (goLeft)
                {
                    // Flip sprite for movement direction
                    Transform.Scale = (-1, 1);

                    // 
                    _velocity.X -= 800 * dt;
                }
                else
                {
                    // Flip sprite for movement direction
                    Transform.Scale = (1, 1);

                    // 
                    _velocity.X += 800 * dt;
                }

                // If we can jump (thus, on stable ground), set to walking animation.
                if (_canJump && SpriteRenderer.Animation.Name != "walk")
                {
                    SpriteRenderer.Play("walk");
                }
            }
            else
            {
                if (_canJump) // Aka, on the ground
                {
                    // Stop moving
                    _velocity.X = 0;

                    if (SpriteRenderer.Animation.Name != "idle")
                    {
                        SpriteRenderer.Play("idle");
                    }
                }
            }

            // Clamp max velocity
            if (Calc.Abs(_velocity.X) > 240) { _velocity.X = Calc.Sign(_velocity.X) * 240; }
            if (Calc.Abs(_velocity.Y) > 480) { _velocity.Y = Calc.Sign(_velocity.Y) * 480; }
        }

        private void ProcessHorizontalCollisions(Map map)
        {
            var bounds = GetCollisionBounds();

            // Convert world position in to map coordinates
            var mapCoord = (IntVector) (Transform.Position / map.TileSize);

            foreach (var other in map.GetCollisionBounds(mapCoord.X, mapCoord.Y))
            {
                // Player is outside the tile collision row 
                // If not on the ground, raise feet, making collision with corners softer
                if (!_hasGroundCollision) { if ((bounds.Bottom - 16) <= other.Top) { continue; } }
                else if (bounds.Bottom <= other.Top) { continue; }
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

            // Vertical resolution
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
