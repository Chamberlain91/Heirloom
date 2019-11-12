using System.Collections.Generic;

using Heirloom.Drawing;
using Heirloom.Math;

namespace Examples.Drawing
{
    public sealed class PolygonDemo : Demo
    {
        public Polygon Polygon, PolygonHull;
        public IEnumerable<Triangle> Triangles;

        public PolygonDemo()
            : base("Polygon")
        {
            // Create a classic 5 point star
            Polygon = Polygon.CreateStar(5, 1);

            // Construct the convex hull
            PolygonHull = Polygon.CreateConvexHull(Polygon.Vertices);

            // Decompose the polygon into triangles
            Triangles = Polygon.Triangulate();
        }

        internal override void Draw(RenderContext ctx, Rectangle contentBounds)
        {
            // Draws a circle, polygon and regular polygon in each row
            for (var i = 0; i < 4; i++)
            {
                ctx.SaveState();

                var w = contentBounds.Width / 3;
                var h = contentBounds.Height / 2;
                var x = contentBounds.X + w * (i % 3);
                var y = contentBounds.Y + h * (i / 3);

                // Compute new sub content box
                var box = new Rectangle(x, y, w, h);
                var boxSize = box.Size * 0.9F;
                var boxOffset = box.Min + ((Vector) (box.Size - boxSize)) * 0.5F;
                box = new Rectangle(boxOffset, boxSize);

                DrawPolygon(i, box);

                ctx.RestoreState();
            }

            void DrawPolygon(int index, Rectangle bounds)
            {
                var center = bounds.Center;
                var radius = Calc.Min(bounds.Width, bounds.Height) / 2F;

                var transform = Matrix.CreateTransform(center, 0, Vector.One * radius);

                switch (index)
                {
                    case 0:
                        ctx.DrawPolygonOutline(Polygon, transform);
                        break;

                    case 1:
                        foreach (var triangle in Triangles)
                        {
                            var a = transform * triangle.A;
                            var b = transform * triangle.B;
                            var c = transform * triangle.C;

                            ctx.DrawLine(a, b);
                            ctx.DrawLine(b, c);
                            ctx.DrawLine(c, a);
                        }
                        break;

                    case 2:
                        ctx.DrawPolygonOutline(PolygonHull, transform);
                        break;

                    case 3:
                        foreach (var convex in Polygon.ConvexPartitions)
                        {
                            ctx.DrawPolygonOutline(convex, transform, 1);
                        }
                        break;
                }
            }
        }
    }
}
