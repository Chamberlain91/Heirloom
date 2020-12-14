using System;
using System.Runtime.CompilerServices;

using Meadows.Mathematics;

namespace Meadows.Drawing
{
    public abstract partial class GraphicsContext
    {
        // used to center the line within the 1x1 pixel image to anchor at left-center
        private static readonly Matrix _lineOffsetMatrix = Matrix.CreateTranslation(0, -1 / 2F);

        private static readonly Mesh _temporaryMesh = new Mesh();

        #region Draw Line / Curve

        /// <summary>
        /// Draws a line segment between two points to the current surface.
        /// </summary> 
        /// <param name="p0">The start point.</param>
        /// <param name="p1">The end point.</param>
        /// <param name="width">The thickness of the line in pixels.</param>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public void DrawLine(in Vector p0, in Vector p1, float width = 1F)
        {
            var off = p1 - p0;
            var len = off.Length;
            var dir = off * len;

            var angle = dir.Angle;
            var transform = Matrix.CreateTransform(p0, angle, (len, width))
                          * _lineOffsetMatrix;

            DrawImage(Image.Default, transform);
        }

        /// <summary>
        /// Draws a quadratic curve using three control points to the current surface.
        /// </summary> 
        /// <param name="p0">The first control point.</param>
        /// <param name="p1">The second control point.</param>
        /// <param name="p2">The third control point.</param>
        /// <param name="width">The thickness of the line in pixels.</param> F
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public void DrawCurve(Vector p0, Vector p1, Vector p2, float width = 1F)
        {
            var p = p0;
            var t = 0F;

            // length of derivative curve
            var dLength = CurveTools.ApproximateLength(t => CurveTools.InterpolateDerivative(p0, p1, p2, t), 4);
            var nominal = 0.06F;

            while (t < 1F)
            {
                // Draw segment
                var v = CurveTools.Interpolate(p0, p1, p2, t);
                DrawLine(p, v, width);

                var d = CurveTools.InterpolateDerivative(p0, p1, p2, t).Length / dLength;
                t += Calc.Clamp(nominal * d, 0.02F, 0.1F); // keeps steps within 2% to 10%
                p = v;
            }

            // Draw last segment (to ensure a smooth join to the end point)
            DrawLine(p, p2, width);
        }

        /// <summary>
        /// Draws a cubic curve using four control points to the current surface.
        /// </summary> 
        /// <param name="p0">The first control point.</param>
        /// <param name="p1">The second control point.</param>
        /// <param name="p2">The third control point.</param>
        /// <param name="p3">The fourth control point.</param>
        /// <param name="width">The thickness of the line in pixels.</param> 
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public void DrawCurve(Vector p0, Vector p1, Vector p2, Vector p3, float width = 1F)
        {
            var p = p0;
            var t = 0F;

            // length of derivative curve
            var dLength = CurveTools.ApproximateLength(t => CurveTools.InterpolateDerivative(p0, p1, p2, p3, t), 4);
            var nominal = 0.06F;

            while (t < 1F)
            {
                // Draw segment
                var v = CurveTools.Interpolate(p0, p1, p2, p3, t);
                DrawLine(p, v, width);

                var d = CurveTools.InterpolateDerivative(p0, p1, p2, p3, t).Length / dLength;
                t += Calc.Clamp(nominal * d, 0.02F, 0.1F); // keeps steps within 2% to 10%
                p = v;
            }

            // Draw last segment (to ensure a smooth join to the end point)
            DrawLine(p, p3, width);
        }

        /// <summary>
        /// Draws a bezier curve to the current surface.
        /// </summary>
        /// <param name="curve">Some bezier curve.</param>
        /// <param name="width">The thickness of the line in pixels.</param>
        /// <exception cref="ArgumentNullException">Thrown if <paramref name="curve"/> is null.</exception>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public void DrawCurve(Curve curve, float width = 1F)
        {
            if (curve is null) { throw new ArgumentNullException(nameof(curve)); }

            for (var i = 0; i < curve.Count - 1; i++)
            {
                var current = curve.GetPoint(i);
                var next = curve.GetPoint(i + 1);

                switch (curve.GetCurveType(i))
                {
                    case CurveType.Stepped:
                        break; // hmm...

                    case CurveType.Linear:
                        DrawLine(current, next, width);
                        break;

                    case CurveType.Quadratic:
                        DrawCurve(current, curve.GetInHandle(i), next, width);
                        break;

                    case CurveType.Cubic:
                        DrawCurve(current, current + curve.GetInHandle(i), next + curve.GetOutHandle(i), next, width);
                        break;
                }
            }
        }

        #endregion

        /// <summary>
        /// Draws an image stretched to fill a rectangular region to the current surface ignoring the image origin offset.
        /// </summary> 
        /// <param name="image">Some image.</param>
        /// <param name="rectangle">The bounds of the drawn image.</param>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public void DrawImage(Texture image, in Rectangle rectangle)
        {
            var transform = Matrix.CreateTransform(rectangle.Position, 0, (Vector) rectangle.Size);
            Draw(image, Mesh.QuadMesh, transform);
        }

        #region Draw Rectangle

        /// <summary>
        /// Draws a rectangle to the current surface.
        /// </summary> 
        /// <param name="rectangle">The rectangular region of the rectangle.</param>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public void DrawRect(in Rectangle rectangle)
        {
            DrawImage(Image.Default, in rectangle);
        }

        /// <summary>
        /// Draws the outline of a rectangel to the current surface.
        /// </summary> 
        /// <param name="rectangle">The rectangular region of the rectangle.</param>
        /// <param name="width">Width of the outline in pixels.</param>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public void DrawRectOutline(in Rectangle rectangle, float width = 1)
        {
            DrawLine(rectangle.TopLeft, rectangle.BottomLeft, width);
            DrawLine(rectangle.TopRight, rectangle.BottomRight, width);
            DrawLine(rectangle.TopLeft, rectangle.TopRight, width);
            DrawLine(rectangle.BottomLeft, rectangle.BottomRight, width);
        }

        #endregion

        #region Draw Triangle

        /// <summary>
        /// Draw a triangle to the current surface.
        /// </summary> 
        /// <param name="triangle">The triangle to draw.</param>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public void DrawTriangle(in Triangle triangle)
        {
            DrawTriangle(in triangle.A, in triangle.B, in triangle.C);
        }

        /// <summary>
        /// Draw a triangle outline to the current surface.
        /// </summary> 
        /// <param name="a">The first point.</param>
        /// <param name="b">The second point.</param>
        /// <param name="c">The third point.</param>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public void DrawTriangle(in Vector a, in Vector b, in Vector c)
        {
            _temporaryMesh.Clear();

            // Append vertices
            _temporaryMesh.AddVertex(new MeshVertex(a, Vector.Zero));
            _temporaryMesh.AddVertex(new MeshVertex(b, Vector.Zero));
            _temporaryMesh.AddVertex(new MeshVertex(c, Vector.Zero));

            // Draw mesh
            Draw(Image.Default, _temporaryMesh, Matrix.Identity);
        }

        /// <summary>
        /// Draw a triangle outline to the current surface.
        /// </summary> 
        /// <param name="triangle">The triangle to draw.</param>
        /// <param name="width">The thickness of the line in pixels.</param>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public void DrawTriangleOutline(in Triangle triangle, float width = 1F)
        {
            DrawTriangleOutline(in triangle.A, in triangle.B, in triangle.C, width);
        }

        /// <summary>
        /// Draw a triangle outline to the current surface.
        /// </summary> 
        /// <param name="a">The first point.</param>
        /// <param name="b">The second point.</param>
        /// <param name="c">The third point.</param>
        /// <param name="width">The thickness of the line in pixels.</param>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public void DrawTriangleOutline(in Vector a, in Vector b, in Vector c, float width = 1F)
        {
            DrawLine(in a, in b, width);
            DrawLine(in b, in c, width);
            DrawLine(in c, in a, width);
        }

        #endregion 
    }
}
