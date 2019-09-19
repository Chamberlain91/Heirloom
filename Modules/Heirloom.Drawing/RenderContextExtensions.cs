using System;
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

        #region Draw Image

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static void DrawImage(this RenderContext ctx, ImageSource image, Vector position)
        {
            ctx.DrawImage(image, Matrix.CreateTranslation(position));
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static void DrawImage(this RenderContext ctx, ImageSource image, Vector position, float rotation)
        {
            ctx.DrawImage(image, Matrix.CreateTransform(position, rotation, Vector.One));
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static void DrawImage(this RenderContext ctx, ImageSource image, Vector position, float rotation, Vector scale)
        {
            ctx.DrawImage(image, Matrix.CreateTransform(position, rotation, scale));
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static void DrawImage(this RenderContext ctx, ImageSource image, Rectangle rectangle)
        {
            var scale = rectangle.Size / image.Size;
            var transform = Matrix.CreateTransform(rectangle.Position, 0, (Vector) scale);
            ctx.DrawImage(image, transform);
        }

        #endregion

        #region Draw Primitive

        /// <summary>
        /// Draws a line segment between two points.
        /// </summary>
        /// <param name="ctx">The drawing context.</param>
        /// <param name="p0">The start point.</param>
        /// <param name="p1">The end point.</param>
        /// <param name="width">The thickness of the line in pixels.</param>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static void DrawLine(this RenderContext ctx, Vector p0, Vector p1, float width = 1F)
        {
            var s = ctx.ApproximatePixelScale;

            var off = p1 - p0;
            var len = off.Length;
            var dir = off * len;

            var angle = dir.Angle;
            var transform = Matrix.CreateTransform(p0, angle, (len, width * s))
                          * _lineOffsetMatrix;

            ctx.DrawImage(Image.White, transform);
        }

        /// <summary>
        /// Draws a quadratic curve using three control points.
        /// </summary>
        /// <param name="ctx">The drawing context.</param>
        /// <param name="p0">The first control point.</param>
        /// <param name="p1">The second control point.</param>
        /// <param name="p2">The third control point.</param>
        /// <param name="width">The thickness of the line in pixels.</param>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static void DrawCurve(this RenderContext ctx, Vector p0, Vector p1, Vector p2, float width = 1F)
        {
            // Compute approximate curve length
            var length = Curves.QuadraticDerivativeApproximateLength(p0, p1, p2);

            // Calibrates the curve approximation quality
            const float MinStepSize = 1 / 64F; // No more than 64 segments
            const float MaxStepSize = 1 / 8F;  // No fewer than 8 segments

            var prev = p0;
            var t = 0F;

            var terminate = false;
            while (!terminate)
            {
                // Compute tangent vector
                var tangent = Curves.QuadraticDerivative(p0, p1, p2, t);
                t += Calc.Clamp(MaxStepSize * (tangent.Length / length), MinStepSize, MaxStepSize);

                // Have we advanced beyond the curve?
                if (t >= 1F)
                {
                    // Moved to last point
                    terminate = true;
                    t = 1F;
                }

                // Compute interpolated point
                var curr = Curves.Quadratic(p0, p1, p2, t);

                // Draw line from previous point to current point
                ctx.DrawLine(prev, curr, width);

                // The current point will be the previous next iteration
                prev = curr;
            }
        }

        /// <summary>
        /// Draws a cubic curve using four control points.
        /// </summary>
        /// <param name="ctx">The drawing context.</param>
        /// <param name="p0">The first control point.</param>
        /// <param name="p1">The second control point.</param>
        /// <param name="p2">The third control point.</param>
        /// <param name="p3">The fourth control point.</param>
        /// <param name="width">The thickness of the line in pixels.</param>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static void DrawCurve(this RenderContext ctx, Vector p0, Vector p1, Vector p2, Vector p3, float width = 1F)
        {
            // Compute approximate curve length
            var length = Curves.CubicDerivativeApproximateLength(p0, p1, p2, p3);

            // Calibrates the curve approximation quality
            const float MinStepSize = 1 / 64F; // No more than 64 segments
            const float MaxStepSize = 1 / 8F;  // No fewer than 8 segments

            var prev = p0;
            var t = 0F;

            var terminate = false;
            while (!terminate)
            {
                // Compute tangent vector
                var tangent = Curves.CubicDerivative(p0, p1, p2, p3, t);
                t += Calc.Clamp(MaxStepSize * (tangent.Length / length), MinStepSize, MaxStepSize);

                // Have we advanced beyond the curve?
                if (t >= 1F)
                {
                    // Moved to last point
                    terminate = true;
                    t = 1F;
                }

                // Compute interpolated point
                var curr = Curves.Cubic(p0, p1, p2, p3, t);

                // Draw line from previous point to current point
                ctx.DrawLine(prev, curr, width);

                // The current point will be the previous next iteration
                prev = curr;
            }
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static void DrawRect(this RenderContext ctx, Rectangle rectangle)
        {
            DrawImage(ctx, Image.White, rectangle);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static void DrawRectOutline(this RenderContext ctx, Rectangle rectangle, float width = 1)
        {
            DrawLine(ctx, rectangle.TopLeft, rectangle.BottomLeft, width);
            DrawLine(ctx, rectangle.TopRight, rectangle.BottomRight, width);
            DrawLine(ctx, rectangle.TopLeft, rectangle.TopRight, width);
            DrawLine(ctx, rectangle.BottomLeft, rectangle.BottomRight, width);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static void DrawCircle(this RenderContext ctx, Vector position, float radius)
        {
            throw new NotImplementedException();
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static void DrawCircleOutline(this RenderContext ctx, Vector position, float radius, float width = 1F)
        {
            // Draw a regular polygon to approximate a circle w/ number sides needed to imitate that circle 
            var sides = ComputeCircleSegments(radius, 1F / ctx.ApproximatePixelScale);
            DrawPolygonOutline(ctx, position, sides, radius, width);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static void DrawPolygon(this RenderContext ctx, Vector position, int sides, float radius)
        {
            throw new NotImplementedException();
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static void DrawPolygonOutline(this RenderContext ctx, Vector position, int sides, float radius, float width = 1F)
        {
            var polygon = Polygon.GetRegularPolygonPoints(position, sides, radius);
            DrawPolygonOutline(ctx, polygon, width);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static void DrawPolygonOutline(this RenderContext ctx, IEnumerable<Vector> polygon, float width = 1F)
        {
            DrawPolygonOutline(ctx, polygon, Matrix.Identity, width);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static void DrawPolygonOutline(this RenderContext ctx, IEnumerable<Vector> polygon, in Matrix transform, float width = 1F)
        {
            if (polygon.Any())
            {
                var first = transform * polygon.First();
                var point = first;

                // Draw (i+1 to n-1)
                foreach (var v in polygon.Skip(1))
                {
                    var V = transform * v;
                    DrawLine(ctx, point, V, width);
                    point = V;
                }

                // Draw (n-1 to 0)
                DrawLine(ctx, point, first, width);
            }
        }

        internal static int ComputeCircleSegments(float radius, float objectToPixelScale)
        {
            // Computes (hopefully), a decent number of segments to approximate a circle with a regular polygon
            var s = (int) (Calc.Sqrt(radius * objectToPixelScale) * 2.8F);
            return Calc.Clamp(s, 3, 64);
        }

        #endregion

        #region Draw Nine Slice

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static void DrawNineSlice(this RenderContext ctx, NineSlice nine, Rectangle rectangle)
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
            ctx.DrawImage(nine.TopLeftImage, Matrix.CreateTranslation(x0, y0));
            ctx.DrawImage(nine.TopRightImage, Matrix.CreateTranslation(x2, y0));
            ctx.DrawImage(nine.BottomLeftImage, Matrix.CreateTranslation(x0, y2));
            ctx.DrawImage(nine.BottomRightImage, Matrix.CreateTranslation(x2, y2));

            if (w > 0)
            {
                // Horizontal
                ctx.DrawImage(nine.TopMiddleImage, Matrix.CreateTransform(x1, y0, 0, w, 1));
                ctx.DrawImage(nine.BottomMiddleImage, Matrix.CreateTransform(x1, y2, 0, w, 1));
            }

            if (h > 0)
            {
                // Vertical
                ctx.DrawImage(nine.MiddleLeftImage, Matrix.CreateTransform(x0, y1, 0, 1, h));
                ctx.DrawImage(nine.MiddleRightImage, Matrix.CreateTransform(x2, y1, 0, 1, h));
            }

            if (w > 0 && h > 0)
            {
                // Middle
                ctx.DrawImage(nine.MiddleImage, Matrix.CreateTransform(x1, y1, 0, w, h));
            }
        }

        #endregion

        /// <summary>
        /// Draws a simple axis aligned 'cross' or 'plus' shape, useful for debugging positions.
        /// </summary>
        /// <param name="width">Width of the lines screen pixels (not world space).</param>
        /// <param name="size">Size in screen pixels (not world space).</param>
        public static void DrawCross(this RenderContext ctx, Vector center, float size = 2, float width = 1F)
        {
            // Scale input size by pixel scaling
            size *= ctx.ApproximatePixelScale;

            // Draw axis
            ctx.DrawLine(center + (Vector.Left * size), center + (Vector.Right * size), width);
            ctx.DrawLine(center + (Vector.Up * size), center + (Vector.Down * size), width);
        }
    }
}
