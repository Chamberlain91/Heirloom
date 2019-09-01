using Heirloom.Drawing;
using Heirloom.Math;
using Heirloom.Runtime;

namespace Shooter
{
    public class Environment : Entity
    {
        public Image Stars;

        public Environment()
        {
            Stars = Assets.Get<Image>("backgrounds/purple.png");
            // Layer = -10;

            // Create random asteroids
            for (var i = 0; i < 100; i++)
            {
                var asteroid = new Asteroid();
                Scene.AddEntity(asteroid);

                var x = Calc.Random.NextFloat(-5000, +5000);
                var y = Calc.Random.NextFloat(-5000, +5000);
                asteroid.Transform.Position = (x, y);
            }
        }

        protected override void Render(RenderContext ctx)
        {
            // todo: camera bounds rectangle
            // ctx.DrawImageTiled(Stars, Scene.CurrentCamera.Bounds);
            ctx.Draw(Stars, Matrix.Identity);
        }
    }
}
