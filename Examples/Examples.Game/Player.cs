using Heirloom.Drawing;
using Heirloom.Game;
using Heirloom.Math;

using static Heirloom.Game.AssetDatabase;

namespace Examples.Game
{
    public class Player : Entity
    {
        private float _ignoreInputTime = 0;

        // Input state
        private bool _moveLeft;
        private bool _moveRight;
        private bool _doJump;

        private bool _allowJump;

        // Wall jump mechanics
        private enum WallJump { Any, Left, Right }
        private WallJump _wallJump;
        private Vector _wallNormal;
        private bool _isTouchWall;

        public readonly SpriteComponent SpriteRenderer;

        public readonly PhysicsComponent Physics;

        public Player()
        {
            // 
            SpriteRenderer = AddComponent(new SpriteComponent(GetAsset<Sprite>("player")));

            // 
            Physics = AddComponent(new PhysicsComponent());
            Physics.WallCollision += Physics_WallCollision;
            Physics.Shape = new Rectangle(-24, 0, 48, 48);
        }

        /// <summary>
        /// Gets a value determining if we should to respect user input.
        /// </summary>
        private bool AllowInput => _ignoreInputTime <= 0;

        private void ChangeAnimation(string name)
        {
            if (SpriteRenderer.Animation.Name != name)
            {
                SpriteRenderer.Play(name);
            }
        }

        protected override void Update(float dt)
        {
            // Check input buttons
            _moveLeft = Input.GetButton("a") == ButtonState.Down;
            _moveRight = Input.GetButton("d") == ButtonState.Down;

            // Did we press the jump button?
            if (Input.GetButton("space") == ButtonState.Pressed)
            {
                _doJump = true;
            }

            // 
            _ignoreInputTime -= dt;

            // Movement

            if (_moveLeft || _moveRight)
            {
                if (AllowInput)
                {
                    // Flip sprite for movement direction
                    if (_moveLeft) { Transform.Scale = (-1, 1); }
                    else { Transform.Scale = (1, 1); }
                }

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

            // Are we on the ground?
            if (Physics.HasGroundCollision)
            {
                // We cannot be touching a wall if we are touching the ground
                _isTouchWall = false;

                // Reset jumping state
                _wallJump = WallJump.Any;
                _allowJump = true;

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
                // Are we allowed to wall jump?
                if (_isTouchWall)
                {
                    // Jump off left facing wall
                    if (_wallJump != WallJump.Right && _wallNormal.X > 0)
                    {
                        // Apply top-right jumpy velocity
                        Physics.Velocity = (360, -360);

                        // Allow only jumps off a left facing wall now
                        _wallJump = WallJump.Right;

                        // Ignore input for a short time to allow time to release
                        // directional input and get full wall jump velocity
                        _ignoreInputTime = 0.2F;

                        // Face right
                        Transform.Scale = (1, 1);

                        // 
                        ChangeAnimation("jump");
                    }

                    // Jump off right facing wall
                    if (_wallJump != WallJump.Left && _wallNormal.X < 0)
                    {
                        // Apply top-left jumpy velocity
                        Physics.Velocity = (-360, -360);

                        // Allow only jumps off a left facing wall now
                        _wallJump = WallJump.Left;

                        // Ignore input for a short time to allow time to release
                        // directional input and get full wall jump velocity
                        _ignoreInputTime = 0.2F;

                        // Face right
                        Transform.Scale = (-1, 1);

                        // 
                        ChangeAnimation("jump");
                    }
                }

                // Are we allowed to regular jump?
                if (_allowJump && Physics.HasGroundCollision)
                {
                    // Apply impulse upwards
                    Physics.Velocity = (Physics.Velocity.X, -360);

                    // We have now jumped
                    _wallJump = WallJump.Any;
                    _allowJump = false;
                }

                // 
                _doJump = false;
            }

            // 
            if (AllowInput)
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

        private void Physics_WallCollision(Vector normal)
        {
            _wallNormal = normal;
            _isTouchWall = true;
        }
    }
}
