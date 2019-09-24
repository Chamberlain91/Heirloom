using Heirloom.Drawing;
using Heirloom.Math;

namespace Examples.Drawing
{
    public sealed class PrimitivesDemo : Demo
    {
        public Polygon StarPolygon = Polygon.CreateStar(5, 1);

        public PrimitivesDemo()
            : base("Primitives")
        { }

        internal override void Draw(RenderContext ctx, Rectangle contentBounds)
        {
            // circle 
            // polygon
            // regular polygon

            for (var j = 0; j < 2; j++)
            {
                for (var i = 0; i < 3; i++)
                {
                    ctx.SaveState();

                    var w = contentBounds.Width / 3;
                    var h = contentBounds.Height / 2;
                    var x = contentBounds.X + w * i;
                    var y = contentBounds.Y + h * j;

                    // Compute new sub content box
                    var box = new Rectangle(x, y, w, h);
                    var boxSize = box.Size * 0.9F;
                    var boxOffset = box.Min + ((Vector) (box.Size - boxSize)) * 0.5F;
                    box = new Rectangle(boxOffset, boxSize);

                    DrawPrimitive(ctx, i, box, j == 1);

                    ctx.RestoreState();
                }
            }
        }

        private void DrawPrimitive(RenderContext ctx, int index, Rectangle bounds, bool fill)
        {
            var center = bounds.Center;
            var radius = Calc.Min(bounds.Width, bounds.Height) / 2F;

            if (fill)
            {
                switch (index)
                {
                    case 0:
                        ctx.DrawRect(bounds);
                        break;

                    case 1:
                        ctx.DrawPolygon(StarPolygon, Matrix.CreateTransform(center, 0, Vector.One * radius));
                        break;

                    case 2:
                        ctx.DrawCircle(center, radius);
                        break;
                }
            }
            else
            {
                switch (index)
                {
                    case 0:
                        ctx.DrawRectOutline(bounds);
                        break;

                    case 1:
                        ctx.DrawPolygonOutline(StarPolygon, Matrix.CreateTransform(center, 0, Vector.One * radius));
                        break;

                    case 2:
                        ctx.DrawCircleOutline(center, radius);
                        break;
                }
            }
        }
    }
}
