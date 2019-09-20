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

        internal override void Draw(RenderContext ctx, Rectangle contentBounds)
        {

            var center = contentBounds.Center;
            var offset = contentBounds.Width * 0.33F;

            var transA = Matrix.CreateTranslation(center - (offset, 0));
            var transB = Matrix.CreateTranslation(center);
            var transC = Matrix.CreateTranslation(center + (offset, 0));

            ctx.DrawPolygonOutline(PolygonHull, transA, 1);

            // 
            foreach (var triangle in Triangles)
            {
                ctx.DrawPolygonOutline(triangle, transB, 1);
            }

            // 
            ctx.DrawPolygonOutline(Polygon, transC, 3);
        }
    }
}
