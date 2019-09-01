using Heirloom.Drawing;
using Heirloom.Math;

namespace Heirloom.Examples.Physics
{
    public class Graphic
    {
        public ImageSource Surface; // todo: replace w/ image when glReadPixels is implemented
        public Matrix Matrix;

        public Graphic(RenderContext ctx, IPolygon shape)
        {
            // Approximates how big a screen pixel is in world units
            var pixelScale = 1F / ctx.ApproximatePixelScale;

            // 
            Surface = CreateGraphicSurface(ctx, shape, pixelScale, out var matrix);
            Matrix = matrix;
        }

        private static ImageSource CreateGraphicSurface(RenderContext ctx, IPolygon polygon, float objectToPixelScale, out Matrix graphicMatrix)
        {
            // 
            var bounds = Rectangle.FromPoints(polygon);

            // 
            var q = 4F / objectToPixelScale;
            var trans = Matrix.CreateTranslation(bounds.Min - (q, q));
            var scale = Matrix.CreateScale(objectToPixelScale);

            // 
            graphicMatrix = trans * Matrix.Inverse(in scale);
            var transform = scale * Matrix.Inverse(in trans);

            // Create surface
            using (var surface = ctx.CreateSurface((IntSize) (bounds.Size * objectToPixelScale) + (8, 8)))
            using (ctx.PushState())
            {
                // 
                ctx.ResetState();
                ctx.Surface = surface;

                // 
                ctx.Clear(Color.Transparent);

                var mesh = Mesh.CreateFromConvexPolygon(polygon);
                ctx.Draw(Image.White, mesh, transform);
                ctx.DrawPolygon(polygon, transform, Color.Gray, 3F);

                // todo: replace w/ when glReadPixels implemented
                // return ctx.Capture();
                return surface;
            }
        }
    }
}
