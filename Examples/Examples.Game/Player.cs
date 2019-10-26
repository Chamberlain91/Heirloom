using System;
using Heirloom.Drawing;
using Heirloom.Game;
using Heirloom.Math;

using static Heirloom.Game.AssetDatabase;

namespace Examples.Game
{
    public class Player : Entity
    {
        // Input state
        private bool _moveLeft;
        private bool _moveRight;
        private bool _moveUp;
        private bool _moveDown;

        private bool _doJump;

        private bool _allowJump;
        private bool _isClimb;

        public readonly SpriteComponent SpriteRenderer;

        public readonly PhysicsComponent Physics;

        public readonly Collider Collider;

        public Player()
        {
            // 
            SpriteRenderer = AddComponent(new SpriteComponent(GetAsset<Sprite>("player")));

            // 
            Physics = AddComponent(new PhysicsComponent());
            Physics.WallCollision += Physics_WallCollision;

            // 
            Collider = AddComponent(new Collider(new Rectangle(-24, 0, 48, 48)));
        }

        private void ChangeAnimation(string name, bool play = true)
        {
            if (SpriteRenderer.Animation.Name != name)
            {
                if (play) { SpriteRenderer.Play(name); }
                else
                {
                    SpriteRenderer.SetAnimation(name);
                    SpriteRenderer.Stop();
                }
            }
        }

        protected override void Update(float dt)
        {
            // Check input buttons
            _moveLeft = Input.GetButton("a") == ButtonState.Down;
            _moveRight = Input.GetButton("d") == ButtonState.Down;
            _moveUp = Input.GetButton("w") == ButtonState.Down;
            _moveDown = Input.GetButton("s") == ButtonState.Down;

            // Detect climb
            var map = Scene.GetEntity<Map>();
            if (!map.IsNearClimable(Transform.Position))
            {
                if (_isClimb)
                {
                    Physics.Velocity = Vector.Zero;
                    ChangeAnimation("idle");
                }

                _isClimb = false;
            }
            else if (_moveUp || _moveDown)
            {
                _isClimb = true;
            }

            // Did we press the jump button?
            if (Input.GetButton("space") == ButtonState.Pressed)
            {
                if (_isClimb)
                {
                    ChangeAnimation("jump");
                    _isClimb = false;
                }

                _doJump = true;
            }

            // Movement
            if (_isClimb)
            {
                Physics.GravityMultiplier = 0;

                // 
                ChangeAnimation("climb", false);
                SpriteRenderer.GotoFrame((int) Transform.Position.Y / 32 % 2);
            }
            else
            {
                Physics.GravityMultiplier = 1;

                if (_moveLeft || _moveRight)
                {
                    // Flip sprite for movement direction
                    if (_moveLeft) { Transform.Scale = (-1, 1); }
                    else { Transform.Scale = (1, 1); }

                    // If on the ground, set walking animation
                    if (Physics.HasGroundCollision)
                    {
                        ChangeAnimation("walk");
                    }
                }
                else
                {
                    // If on the ground, set idle animation
                    if (Physics.HasGroundCollision)
                    {
                        ChangeAnimation("idle");
                    }
                }
            }

            // Are we on the ground?
            if (Physics.HasGroundCollision)
            {
                // Reset jumping state
                _allowJump = true;
                _isClimb = false;

                // If we intend to do a jump
                if (_doJump)
                {
                    // Set jump animation
                    ChangeAnimation("jump");
                }
            }
        }

        protected override void FixedUpdate(float ft)
        {
            // Do we want to jump and are we on solid ground?
            if (_doJump)
            {
                // Are we allowed to regular jump?
                if (_allowJump && Physics.HasGroundCollision)
                {
                    // Apply impulse upwards
                    Physics.Velocity = (Physics.Velocity.X, -720);

                    // We have now jumped
                    _allowJump = false;
                }

                // 
                _doJump = false;
            }

            if (_isClimb)
            {
                if (_moveLeft || _moveRight || _moveUp || _moveDown)
                {
                    var xMove = 0;
                    var yMove = 0;

                    if (_moveLeft) { xMove -= 180; }
                    if (_moveRight) { xMove += 180; }
                    if (_moveUp) { yMove -= 180; }
                    if (_moveDown) { yMove += 180; }

                    Physics.Velocity = new Vector(xMove, yMove);
                }
                else
                {
                    // Not correct friction, but a sliding stop.
                    Physics.Velocity -= Physics.Velocity * 0.5F;
                }
            }
            else
            {
                // Are we pressing left?
                if (_moveLeft) { Physics.Acceleration += (-960, 0); }
                // Are we pressing right?
                else if (_moveRight) { Physics.Acceleration += (+960, 0); }
                // No input, and on the ground, smoothly stop player
                else if (Physics.HasGroundCollision)
                {
                    // Not correct friction, but a sliding stop.
                    Physics.Velocity -= (Physics.Velocity.X * 0.5F, 0);
                }
            }
        }

        private void Physics_WallCollision(Collider collider, Vector overlap)
        {
            if (collider.Entity is Crate cr)
            {
                cr.Physics.Velocity = new Vector(-overlap.Normalized.X * 64, 0);
            }
        }
    }
}
