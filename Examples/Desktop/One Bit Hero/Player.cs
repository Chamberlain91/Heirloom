using System.Linq;

using Heirloom.Drawing;
using Heirloom.Input;
using Heirloom.Math;
using Heirloom.Runtime;

namespace OneBitHero
{
    public class Player : Actor, ICollisionCallback
    {
        private const float MaxVerticalSpeed = 192F;

        public SpriteCollection Animations;

        private Direction _facing = Direction.Right;

        private enum Direction { Left, Right }

        private Vector _velocity;
        private bool _isAir = false;

        private bool _jump;
        private int _move;

        public Player()
        {
            // Get tileset asset
            var tileset = Assets.Get<TileCollection>("tilemap/tileset_colored.tsx");

            // Construct sprite from tile set
            Animations = new SpriteCollection {
                { "idle", tileset.CreateSprite(Enumerable.Range(274, 1), (8, 16)) },
                { "move", tileset.CreateSprite(Enumerable.Range(275, 2), (8, 16)) },
                { "jump", tileset.CreateSprite(Enumerable.Range(277, 1), (8, 16)) },
                { "fall", tileset.CreateSprite(Enumerable.Range(278, 1), (8, 16)) },
                { "dead", tileset.CreateSprite(Enumerable.Range(279, 1), (8, 16)) }
            };

            // Set initial sprite to idle
            SpriteRenderer.Sprite = Animations.Get("idle");

            // Set collider polygon to a box
            Collider.Shape = new Polygon(new[] {
                new Vector(-6, -11),
                new Vector(+6, -11),
                new Vector(+6, 1),
                new Vector(-6, 1),
            });
        }

        protected override void FixedUpdate()
        {
            // Gravity
            _velocity.Y += 6 * Time.FixedDelta;

            // Movement
            _velocity.X = _move * 64 * Time.FixedDelta;
            _move = 0;

            // Jump
            if (_jump) { _velocity.Y = -160 * Time.FixedDelta; }
            _jump = false;

            // 
            if (Calc.Abs(_velocity.Y) > MaxVerticalSpeed)
            {
                // Scale back to terminal speed
                _velocity.Y *= MaxVerticalSpeed / Calc.Abs(_velocity.Y);
            }

            // Integrate
            Transform.Position += _velocity;
        }

        protected override void Update()
        {
            if (Input.GetKey(Key.D))
            {
                _facing = Direction.Right;
                _move = +1;
            }

            if (Input.GetKey(Key.A))
            {
                _facing = Direction.Left;
                _move = -1;
            }

            if ((Input.GetKey(Key.W) || Input.GetKey(Key.Space)) && _isAir == false)
            {
                _isAir = true;
                _jump = true;
            }

            // 
            Transform.Scale = (_facing == Direction.Right ? 1 : -1, 1);

            // 
            if (_isAir) { ChangeSprite(Animations.Get("jump")); }
            else
            {
                if (_move != 0) { ChangeSprite(Animations.Get("move")); }
                else { ChangeSprite(Animations.Get("idle")); }
            }

            // Animate the camera to follow the player
            // todo: Component on camera entity
            var followPosition = Vector.Lerp(Scene.Camera.Position, Transform.Position, Calc.DeltaTimeInterpolationFactor(0.5F, Time.Delta));
            Scene.Camera.Position = followPosition;
        }

        private void ChangeSprite(Sprite moveSprite)
        {
            if (SpriteRenderer.Sprite != moveSprite)
            {
                SpriteRenderer.Sprite = moveSprite;
            }
        }

        public void OnCollision(Collider a, Collider b, in Manifold manifold)
        {
            // 
            var normal = manifold.Normal;

            var isFloor = Vector.Dot(normal, Vector.Up) < 0.0F;
            if (isFloor) { _isAir = false; }

            // 
            foreach (var contact in manifold)
            {
                var mtv = contact.Normal * contact.Depth;
                Transform.Position += mtv;
            }

            // Project velocity onto contact
            _velocity = Vector.Dot(_velocity, manifold.Tangent) * manifold.Tangent;
        }

        protected override void Render(RenderContext ctx)
        {
            var ray = new Ray(Transform.Position, Vector.Right);
            if (Scene.Collisions.Raycast(in ray, 128, out var hit)) { ctx.DrawLine(hit.Position, hit.Position + hit.Normal * 16, Color.Orange); }

            ray = new Ray(Transform.Position, Vector.Left);
            if (Scene.Collisions.Raycast(in ray, 128, out hit)) { ctx.DrawLine(hit.Position, hit.Position + hit.Normal * 16, Color.Cyan); }
        }
    }
}
