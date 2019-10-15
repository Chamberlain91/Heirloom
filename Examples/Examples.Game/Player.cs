using Heirloom.Drawing;
using Heirloom.Game;
using Heirloom.Math;

using static Heirloom.Game.AssetDatabase;

namespace Examples.Game
{
    public class Player : Entity
    {
        public readonly SpriteComponent SpriteRenderer;

        public readonly PhysicsComponent Physics;

        private bool _moveLeft;
        private bool _moveRight;
        private bool _doJump;

        public Player()
        {
            // 
            SpriteRenderer = AddComponent(new SpriteComponent(GetAsset<Sprite>("player")));

            // 
            Physics = AddComponent(new PhysicsComponent());
            Physics.Shape = new Rectangle(-24, 0, 48, 48);
        }

        protected override void Update(float dt)
        {
            // Check input buttons
            _moveLeft = Input.GetButton("a") == ButtonState.Down;
            _moveRight = Input.GetButton("d") == ButtonState.Down;
            _doJump = Input.GetButton("space") == ButtonState.Down;

            // Jump
            if (_doJump)
            {
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
                if (Physics.HasGroundCollision && SpriteRenderer.Animation.Name != "walk")
                {
                    SpriteRenderer.Play("walk");
                }
            }
            else
            {
                // If we can jump (thus on stable ground), set to idle animation
                if (Physics.HasGroundCollision && SpriteRenderer.Animation.Name != "idle")
                {
                    SpriteRenderer.Play("idle");
                }
            }
        }

        protected override void FixedUpdate(float ft)
        {
            // Move character based on input
            if (_moveLeft) { Physics.Acceleration += (-960, 0); }
            else if (_moveRight) { Physics.Acceleration += (+960, 0); }
            // If can jump (thus on stable ground), apply friction
            else if (Physics.HasGroundCollision)
            {
                // Friction...?
                Physics.Velocity -= (Physics.Velocity.X * 0.5F, 0);
            }

            // 
            if (_doJump && Physics.HasGroundCollision)
            {
                _doJump = false;

                // 
                Physics.Position -= (0, 1);
                Physics.Velocity -= (0, 360);
            }
        }
    }
}
