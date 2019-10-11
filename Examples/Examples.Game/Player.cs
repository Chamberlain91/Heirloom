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

        public Vector Velocity;

        public bool HasGroundCollision;
        public bool HasWallCollision;

        public bool CanJump = false;

        public float GroundPlane;

        public Player()
        {
            SpriteRenderer = AddComponent(new SpriteComponent(GetAsset<Sprite>("player")));
        }

        public Rectangle GetCollisionBounds()
        {
            var left = Transform.Position.X - 24;
            var top = Transform.Position.Y - 18;

            // Compute player bounds
            return new Rectangle(left, top, 48, 66);
        }

        protected override void Update(float dt)
        {
            // Apply Gravity
            if (!HasGroundCollision) { Velocity.Y += 10; }

            // Collision Phase
            var map = Scene.GetEntity<Map>();
            var mapCoord = (IntVector) (Transform.Position / map.TileSize);

            var falling = Velocity.Y > 0;

            GroundPlane = float.MaxValue;

            HasGroundCollision = false;
            HasWallCollision = false;
            CanJump = false;

            // 
            Transform.Position += (0, Velocity.Y * dt); // m/s

            // Get player collision bounds
            var playerBounds = GetCollisionBounds();

            // Vertical resolution
            foreach (var tileBounds in map.GetCollisionBounds(mapCoord.X, mapCoord.Y))
            {
                // Player is outside the tile collision column
                if (playerBounds.Right <= tileBounds.Left) { continue; }
                if (playerBounds.Left >= tileBounds.Right) { continue; }

                // Is the tile below the player?
                if (playerBounds.Center.Y < tileBounds.Top)
                {
                    // Find the nearest ground plane
                    GroundPlane = Calc.Min(GroundPlane, tileBounds.Top);
                }
            }

            // 
            if (falling && playerBounds.Bottom >= GroundPlane)
            {
                var overlap = playerBounds.Bottom - GroundPlane;
                Transform.Position += (0, -overlap - CollisionTolerance);
                Velocity.Y = 0;

                HasGroundCollision = true;
                CanJump = true;
            }

            // 
            Transform.Position += (Velocity.X * dt, 0); // m/s

            // Get updated player bounds
            playerBounds = GetCollisionBounds();

            // Horizontal resolution
            foreach (var tileBounds in map.GetCollisionBounds(mapCoord.X, mapCoord.Y))
            {
                // Player is outside the tile collision row
                if (playerBounds.Bottom <= tileBounds.Top) { continue; }
                if (playerBounds.Top >= tileBounds.Bottom) { continue; }

                // Moving left into tile
                if (Velocity.X < 0 && playerBounds.Right > tileBounds.Right && playerBounds.Left < tileBounds.Right)
                {
                    var overlap = playerBounds.Left - tileBounds.Right;
                    Transform.Position += (-overlap + CollisionTolerance, 0);
                    Velocity.X = 0;

                    HasWallCollision = true;
                }

                // Moving right into tile
                if (Velocity.X > 0 && playerBounds.Left < tileBounds.Left && playerBounds.Right > tileBounds.Left)
                {
                    var overlap = playerBounds.Right - tileBounds.Left;
                    Transform.Position += (-overlap - CollisionTolerance, 0);
                    Velocity.X = 0;

                    HasWallCollision = true;
                }
            }

            // Check input buttons
            var goLeft = Input.GetButton("a") == ButtonState.Down;
            var goRight = Input.GetButton("d") == ButtonState.Down;
            var doJump = Input.GetButton("space") == ButtonState.Down;

            // 
            if (Input.GetButton("r") == ButtonState.Pressed)
            {
                Transform.Position = (96, -96);
                Velocity = Vector.Zero;
            }

            // Jump
            if (doJump && CanJump)
            {
                CanJump = false;
                Transform.Position -= (0, 1);
                Velocity.Y -= 288;

                if (SpriteRenderer.Animation.Name != "jump")
                {
                    SpriteRenderer.Play("jump");
                }
            }

            // 
            if (goLeft || goRight)
            {
                if (CanJump && SpriteRenderer.Animation.Name != "walk")
                {
                    SpriteRenderer.Play("walk");
                }

                // Flip sprite 
                if (goLeft) { Transform.Scale = (-1, 1); }
                else { Transform.Scale = (1, 1); }

                var movement = Velocity.X;

                //
                if (goLeft) { movement -= 12; }
                else { movement += 12; }

                // Clamp movement
                if (movement < -240) { movement = -240; }
                if (movement > +240) { movement = +240; }

                // Apply movment
                Velocity.X = movement;
            }
            else
            {
                if (CanJump) // Aka, on the ground
                {
                    Velocity.X *= 0.66F;

                    if (SpriteRenderer.Animation.Name != "idle")
                    {
                        SpriteRenderer.Play("idle");
                    }
                }
            }
        }
    }
}
