using System.Collections.Generic;
using Heirloom.Drawing;
using Heirloom.Math;

namespace Examples.Drawing
{
    public sealed class PolygonDemo : Demo
    {
        public Polygon Polygon;
        public IEnumerable<Polygon> Triangles;
        public Polygon PolygonHull;

        public PolygonDemo()
            : base("Polygon")
        {
            // Create a classic 5 point star
            Polygon = Polygon.CreateStar(5, 150);

            // Construct the convex hull
            PolygonHull = Polygon.CreateConvexHull(Polygon);

            // Decompose the polygon into triangles
            Triangles = Polygon.DecomposeTriangles(Polygon);
        }

        internal override void Draw(RenderContext ctx)
        {
            var s = (Vector) ctx.Surface.Size;

            var w = s.X * 0.25F;

            var left = Matrix.CreateTranslation(s * 0.5F - (w, 0));
            var center = Matrix.CreateTranslation(s * 0.5F);
            var right = Matrix.CreateTranslation(s * 0.5F + (w, 0));

            ctx.Color = Color.Cyan;
            ctx.DrawPolygonOutline(PolygonHull, left, 1);

            // 
            foreach (var triangle in Triangles)
            {
                ctx.Color = Color.Pink;
                ctx.DrawPolygonOutline(triangle, center, 1);
            }

            // 
            ctx.Color = Color.Yellow;
            ctx.DrawPolygonOutline(Polygon, right, 3);
        }
    }
}
