using System.Collections.Generic;
using System.Linq;
using Heirloom.Drawing;
using Heirloom.Drawing.Utilities;
using Heirloom.Math;
using Heirloom.Runtime;

namespace Shooter
{
    public class Asteroid : Entity
    {
        public SpriteRenderer Renderer;

        public Vector Velocity;

        public Polygon Polygon;
        public List<Vector> Collider;

        public Asteroid()
        {
            Renderer = AddComponent<SpriteRenderer>();

            var i = Calc.Random.Choose(1, 2, 3, 4); // Load a random meteor image
            Renderer.Sprite = Assets.Get<Sprite>($"images/meteors/meteorBrown_big{i}.png");

            var rx = Calc.Random.NextFloat(-1F, +1F);
            var ry = Calc.Random.NextFloat(-1F, +1F);
            Velocity += new Vector(rx, ry).Normalized * Calc.Random.NextFloat(0F, 1F);

            // 
            var image = Renderer.Sprite[0];
            var solidPixels = Rasterizer.Rectangle(0, 0, image.Width, image.Height).Where(co => image.GetPixel(co).A > 0);
            Polygon = Polygon.CreateConvexHull(solidPixels.Cast<Vector>());
            Collider = new List<Vector>(Polygon);
        }

        protected override void Update()
        {
            Transform.Position += Velocity * Time.Delta;
            // Velocity *= 0.98F;

            // 
            for (var i = 0; i < Polygon.Count; i++)
            {
                Collider[i] = Transform.Matrix * Polygon[i];
            }
        }

        protected override void Render(RenderContext ctx)
        {
            // if (Game.IsDebugDrawing)
            {
                ctx.DrawPolygon(Collider, Color.Cyan, 2F);
            }
        }
    }
}
