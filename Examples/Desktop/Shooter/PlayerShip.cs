using System.Collections;
using System.Collections.Generic;
using System.Linq;

using Heirloom.Drawing;
using Heirloom.Drawing.Utilities;
using Heirloom.Math;
using Heirloom.Runtime;

namespace Shooter
{
    public class PlayerShip : Entity
    {
        private const float ROTATION_DAMPING = 0.96F;
        private const float VELOCITY_DAMPING = 0.98F;

        public Sprite Sprite;
        public Sprite DamageSprite;

        public float RotationVelocity;
        public Vector Velocity;

        private float _rotation;
        private Vector _forward;

        private readonly Trail _trail1;
        private readonly Trail _trail2;

        private float _weaponCooldown;

        private Size BoundsSize => (Sprite[0].Height, Sprite[0].Height);
        public Rectangle Bounds => (Transform.Position - (Vector) BoundsSize / 2F, BoundsSize);

        public int Health;

        public bool Visible = true;

        public bool Immune;

        // 
        public Polygon Polygon;
        public List<Vector> Collider;

        public PlayerShip()
        {
            var img = Assets.Get<Image>("images/effects/speed.png"); ;

            // Create trails
            _trail1 = AddComponent<Trail>();
            _trail1.Image = img;

            _trail2 = AddComponent<Trail>();
            _trail2.Image = img;

            // 
            Sprite = Assets.Get<Sprite>("images/playerShip1_orange.png");
            Sprite.Origin = ((Vector) Sprite[0].Size) / 2F;

            // 
            DamageSprite = new Sprite(Enumerable.Range(0, 3).Select(i => Assets.Get<Image>($"images/damage/playerShip1_damage{i + 1}.png")), 1F);

            // 
            var image = Sprite[0];
            var solidPixels = Rasterizer.Rectangle(0, 0, image.Width, image.Height).Where(co => image.GetPixel(co).A > 0);
            Polygon = Polygon.CreateConvexHull(solidPixels.Cast<Vector>());
            Collider = new List<Vector>(Polygon);

            // 
            Health = 4;
        }

        protected override void Update()
        {
            if (Health > 0)
            {
                var input = GetInputVector();

                var damping = 1 / 8F;
                if (Velocity.LengthSquared > 0)
                {
                    damping = 1F / (8F + Calc.Log(Velocity.Length + 1) * 0.25F);
                }

                // Process rotation
                RotationVelocity += input.X * Calc.Pi * damping * Time.Delta;
                _rotation += RotationVelocity;
                RotationVelocity *= ROTATION_DAMPING;

                // 
                Transform.Rotation = _rotation + Calc.HalfPi;
                _forward = Vector.FromAngle(_rotation);

                // Process thrust
                // TODO: Overdrive thrust
                // TODO: Steering whilst moving should also turn ship some
                // Currently the ship moves as a free body
                Velocity += _forward * input.Y * 16F * Time.Delta;

                // 
                Transform.Position += Velocity;
                Velocity *= VELOCITY_DAMPING;

                //// Damping
                //if (Keyboard.IsDown(KeyboardButton.Control))
                //{
                //    RotationVelocity *= Time.Delta / 0.2F;
                //    Velocity *= Time.Delta / 0.2F;
                //}

                //// 
                //_weaponCooldown -= Time.Delta;
                //if (Keyboard.IsDown(KeyboardButton.Space) && _weaponCooldown <= 0)
                //{
                //    _weaponCooldown = 1 / 10F;

                //    var laser = new Projectile();
                //    laser.Transform.Position = Transform.Position;

                //    var dot = Calc.Max(0, Vector.Dot(Velocity.Normalized, _forward));
                //    laser.Velocity = _forward * (20 + Velocity.Length * dot);

                //    Scene.AddEntity(laser);
                //}
            }

            // 
            for (var i = 0; i < Polygon.Count; i++)
            {
                Collider[i] = Transform.Matrix.Multiply(Polygon[i]);
            }

            // 
            CheckCollisions();
            UpdateTrails();

            //
            if (Health <= 0)
            {

            }
        }

        private void CheckCollisions()
        {
            //// Collision w/ Ships
            //foreach (var asteroid in Scene.GetEntities<Asteroid>())
            //{
            //    if (Collisions.CheckOverlap(Collider, asteroid.Collider, out _))
            //    {
            //        if (!Immune)
            //        {
            //            StartCoroutine(TakeDamage(1));
            //        }
            //    }
            //}
        }

        private IEnumerator TakeDamage(int damage)
        {
            Health -= damage;
            Immune = true;

            // 
            //Assets.Load<AudioClip>("audio/sfx_hurt.ogg").Play();

            // 
            var time = Time.Now;
            while (Time.Now - time < 1)
            {
                Visible = !Visible;
                yield return 0.05F;
            }

            Visible = true;
            Immune = false;
        }

        private void UpdateTrails()
        {
            // 
            _trail1.Position = Transform.Position;
            _trail1.Position += Vector.FromAngle(Transform.Rotation) * 45;

            // 
            _trail2.Position = Transform.Position;
            _trail2.Position += Vector.FromAngle(Transform.Rotation - Calc.Pi) * 45;
        }

        protected override void Render(RenderContext ctx)
        {
            if (Visible && Health > 0)
            {
                // 
                ctx.Draw(Sprite, 0, Transform.Matrix, Color.White);

                // 
                if (Health < 4)
                {
                    ctx.Draw(DamageSprite, 2 - (Health - 1), Transform.Matrix, Color.White);
                }

                // 
                //if (Game.IsDebugDrawing)
                {
                    ctx.DrawText($"Health: {Health}", Transform.Position + (64, 0), TextAlign.Left, Font.Default, 32, Color.Yellow);
                    // surface.DrawOutlineRect(Bounds, Color.Cyan, 1F);
                    ctx.DrawPolygon(Collider, Color.Red, 2F);
                }
            }
        }

        private static Vector GetInputVector()
        {
            var vec = Vector.Zero;

            //if (Keyboard.IsDown(KeyboardButton.W))
            //{
            //    vec.Y += +1;
            //}

            //if (Keyboard.IsDown(KeyboardButton.S))
            //{
            //    vec.Y += -1;
            //}

            //if (Keyboard.IsDown(KeyboardButton.A))
            //{
            //    vec.X += -1;
            //}

            //if (Keyboard.IsDown(KeyboardButton.D))
            //{
            //    vec.X += +1;
            //}

            return vec.Normalized;
        }
    }
}
