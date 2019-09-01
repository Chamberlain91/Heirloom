using Heirloom.Drawing;
using Heirloom.Math;
using Heirloom.Runtime;

namespace Shooter
{
    public class Projectile : Entity
    {
        public SpriteRenderer Renderer { get; }

        public Vector Velocity;

        public Projectile()
        {
            // 
            Renderer = AddComponent<SpriteRenderer>();
            Renderer.Sprite = Assets.Get<Sprite>("images/lasers/laserBlue01.png");

            // Play laser 'pew pew' sound
            //Assets.Load<AudioClip>("audio/sfx_laser1.ogg").Play();
        }

        protected override void Update()
        {
            Transform.Position += Velocity;
            Transform.Rotation = Velocity.Angle + Calc.HalfPi;

            //// Check against each asteroid
            //foreach (var asteroid in Scene.GetEntities<Asteroid>())
            //{
            //    // Is the projectile within the bounds of the asteroid?
            //    if (Collisions.CheckOverlap(Transform.Position, asteroid.Collider, out _))
            //    {
            //        // 
            //        Assets.Get<AudioClip>("audio/sfx_hit.ogg").Play();

            //        // 
            //        asteroid.Velocity += Velocity * 0.1F;

            //        // 
            //        var spark = Scene.AddEntity(new ProjectileSpark());
            //        spark.Transform.Position = Transform.Position;
            //        Scene.RemoveEntity(this);
            //        return;
            //    }
            //}
        }

        protected override void Render(RenderContext ctx)
        {
            //// Projectile has gone off screen, remove
            //if (!Scene.CurrentCamera.Bounds.Contains(Transform.Position))
            //{
            //    Scene.Remove(this);
            //}
        }
    }

    public class ProjectileSpark : Entity
    {
        public Sprite Sprite { get; }

        private float _timer;

        public ProjectileSpark()
        {
            // Load sprite with centered pivot
            Sprite = Assets.Get<Sprite>("images/lasers/laserBlue08.png");
            Sprite.Origin = ((Vector) Sprite[0].Size) / 2F;

            // 
            Transform.Rotation = Calc.Random.NextFloat(0, Calc.TwoPi);

            _timer = Calc.Random.NextFloat(1 / 8F, 1 / 4F);
        }

        protected override void Update()
        {
            _timer -= Time.Delta;

            // 
            if (_timer <= 0)
            {
                Scene.RemoveEntity(this);
            }
        }

        protected override void Render(RenderContext ctx)
        {
            ctx.Draw(Sprite, 0, Transform.Matrix, Color.White);
        }
    }
}
