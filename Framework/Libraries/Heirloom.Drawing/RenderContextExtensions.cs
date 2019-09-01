using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;

using Heirloom.Math;

namespace Heirloom.Drawing
{
    public static class RenderContextExtensions
    {
        // used to center the line within the 1x1 pixel image to anchor at left-center
        private static readonly Matrix _lineOffsetMatrix = Matrix.CreateTranslation(0, -1 / 2F);

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static void Draw(this RenderContext ctx, ImageSource image, Rectangle rectangle, Color color)
        {
            var scale = rectangle.Size / image.Size;
            var transform = Matrix.CreateTransform(rectangle.Position, 0, (Vector) scale);
            ctx.Draw(image, transform, color);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static void DrawRect(this RenderContext ctx, Rectangle rectangle, Color color)
        {
            Draw(ctx, Image.White, rectangle, color);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static void DrawRectOutline(this RenderContext ctx, Rectangle rectangle, Color color, float width = 1)
        {
            DrawLine(ctx, rectangle.TopLeft, rectangle.BottomLeft, color, width);
            DrawLine(ctx, rectangle.TopRight, rectangle.BottomRight, color, width);
            DrawLine(ctx, rectangle.TopLeft, rectangle.TopRight, color, width);
            DrawLine(ctx, rectangle.BottomLeft, rectangle.BottomRight, color, width);
        }

        /// <summary>
        /// Draws a line.
        /// </summary>
        /// <param name="ctx"></param>
        /// <param name="start"></param>
        /// <param name="end"></param>
        /// <param name="color"></param>
        /// <param name="width">Width of the lines screen pixels (not world space).</param>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static void DrawLine(this RenderContext ctx, Vector start, Vector end, Color color, float width = 1F)
        {
            var s = ctx.ApproximatePixelScale;

            var off = end - start;
            var len = off.Length;
            var dir = off * len;

            var angle = dir.Angle;
            var transform = Matrix.CreateTransform(start, angle, (len, width * s))
                          * _lineOffsetMatrix;

            ctx.Draw(Image.White, transform, color);
        }

        /// <summary>
        /// Draws a simple axis aligned 'cross' or 'plus' shape, useful for debugging positions.
        /// </summary>
        /// <param name="width">Width of the lines screen pixels (not world space).</param>
        /// <param name="size">Size in screen pixels (not world space).</param>
        public static void DrawCross(this RenderContext ctx, Vector center, Color color, float size = 2, float width = 1F)
        {
            // Scale input size by pixel scaling
            size *= ctx.ApproximatePixelScale;

            // Draw axis
            ctx.DrawLine(center + (Vector.Left * size), center + (Vector.Right * size), color, width);
            ctx.DrawLine(center + (Vector.Up * size), center + (Vector.Down * size), color, width);
        }

        public static void DrawPolygon(this RenderContext ctx, IEnumerable<Vector> polygon, in Matrix transform, Color color, float width = 1F)
        {
            if (polygon.Any())
            {
                var first = transform * polygon.First();
                var point = first;

                // Draw (i+1 to n-1)
                foreach (var v in polygon.Skip(1))
                {
                    var V = transform * v;
                    DrawLine(ctx, point, V, color, width);
                    point = V;
                }

                // Draw (n-1 to 0)
                DrawLine(ctx, point, first, color, width);
            }
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static void DrawPolygon(this RenderContext ctx, IEnumerable<Vector> polygon, Color color, float width = 1F)
        {
            DrawPolygon(ctx, polygon, Matrix.Identity, color, width);
        }

        public static void DrawRegularPolygon(this RenderContext ctx, Vector center, int sides, float radius, Color color, float width = 1F)
        {
            var polygon = Polygon.GetRegularPolygonPoints(center, sides, radius);
            DrawPolygon(ctx, polygon, color, width);
        }

        public static void DrawCircle(this RenderContext ctx, Vector center, float radius, Color color, float width = 1F)
        {
            // Draw a regular polygon to approximate a circle w/ number sides needed to imitate that circle 
            var sides = ComputeCircleSegments(radius, 1F / ctx.ApproximatePixelScale);
            DrawRegularPolygon(ctx, center, sides, radius, color, width);
        }

        internal static int ComputeCircleSegments(float radius, float objectToPixelScale)
        {
            // Computes (hopefully), a decent number of segments to approximate a circle with a regular polygon
            var s = (int) (Calc.Sqrt(radius * objectToPixelScale) * 2.8F);
            return Calc.Clamp(s, 3, 64);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static void Draw(this RenderContext ctx, NineSlice nine, Rectangle rectangle, Color color)
        {
            // Compute stretch factors
            var w = (rectangle.Width - nine.TopLeftImage.Width - nine.TopRightImage.Width) / nine.MiddleImage.Width;
            var h = (rectangle.Height - nine.TopLeftImage.Height - nine.BottomLeftImage.Height) / nine.MiddleImage.Height;

            var x0 = rectangle.Left + 0;
            var x1 = x0 + nine.TopLeftImage.Width;
            var x2 = x1 + nine.MiddleImage.Width * w;

            var y0 = rectangle.Top + 0;
            var y1 = y0 + nine.TopLeftImage.Height;
            var y2 = y1 + nine.MiddleImage.Height * h;

            // Corners
            ctx.Draw(nine.TopLeftImage, Matrix.CreateTranslation(x0, y0), color);
            ctx.Draw(nine.TopRightImage, Matrix.CreateTranslation(x2, y0), color);
            ctx.Draw(nine.BottomLeftImage, Matrix.CreateTranslation(x0, y2), color);
            ctx.Draw(nine.BottomRightImage, Matrix.CreateTranslation(x2, y2), color);

            if (w > 0)
            {
                // Horizontal
                ctx.Draw(nine.TopMiddleImage, Matrix.CreateTransform(x1, y0, 0, w, 1), color);
                ctx.Draw(nine.BottomMiddleImage, Matrix.CreateTransform(x1, y2, 0, w, 1), color);
            }

            if (h > 0)
            {
                // Vertical
                ctx.Draw(nine.MiddleLeftImage, Matrix.CreateTransform(x0, y1, 0, 1, h), color);
                ctx.Draw(nine.MiddleRightImage, Matrix.CreateTransform(x2, y1, 0, 1, h), color);
            }

            if (w > 0 && h > 0)
            {
                // Middle
                ctx.Draw(nine.MiddleImage, Matrix.CreateTransform(x1, y1, 0, w, h), color);
            }
        }
    }
}
