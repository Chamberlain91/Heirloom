using System;
using System.Collections.Generic;
using System.Runtime.CompilerServices;

using Meadows.Mathematics;

namespace Meadows.Drawing
{
    public abstract partial class GraphicsContext
    {
        // used to center the line within the 1x1 pixel image to anchor at left-center
        private static readonly Matrix _lineOffsetMatrix = Matrix.CreateTranslation(0, -1 / 2F);

        private static readonly Mesh _mesh = new();
        private static readonly List<Vector> _sequence = new();

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
            var edge = p1 - p0;
            var edgeLength = edge.Length;
            var edgeDir = edge / edgeLength;

            // Approximates 1 pixel
            var step = ApproximatePixelScale / edgeLength;

            // todo: inline into one matrix construction
            var transform = Matrix.CreateTransform(p0, edgeDir.Angle, (edgeLength, width))
                          * Matrix.CreateTranslation(-step / 2F, -1 / 2F);

            DrawImage(Image.Default, transform);
        }

        /// <summary>
        /// Draws a dotted line segment between two points to the current surface.
        /// </summary> 
        /// <param name="p0">The start point.</param>
        /// <param name="p1">The end point.</param>
        /// <param name="width">The thickness of the line in pixels.</param>
        /// <param name="step">The length of each dash/dot segment.</param>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public void DrawDottedLine(in Vector p0, in Vector p1, float width = 1F, float step = 4F)
        {
            // Compute edge information
            var edge = p1 - p0;
            var edgeLength = edge.Length;
            edge /= edgeLength;

            // Compute number of steps along edge
            var steps = Calc.Floor(edgeLength / step);

            // Draw a line for each second step between endpoints
            for (var i = 0; i < steps; i += 2)
            {
                var a = p0 + (edge * step * (i + 0));
                var b = p0 + (edge * step * (i + 1));
                DrawLine(a, b, width);
            }
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
            // Draw curve as a polyline
            DrawPolyLine(GenerateInterpolatedSequence(), width);

            IEnumerable<Vector> GenerateInterpolatedSequence()
            {
                yield return p0;
                var t = 0F;

                // length of derivative curve
                var dLength = CurveTools.ApproximateLength(t => CurveTools.InterpolateDerivative(p0, p1, p2, p3, t), 4);
                var nominal = 0.08F;

                while (t < 1F)
                {
                    // Emit intermediate point
                    var p = CurveTools.Interpolate(p0, p1, p2, p3, t);
                    yield return p;

                    // Compute derivative to step long the line in a non-linear fashion to enhance curve quiality
                    var derivative = CurveTools.InterpolateDerivative(p0, p1, p2, p3, t).Length / dLength;
                    t += Calc.Clamp(nominal * derivative, 0.05F, 0.15F); // keeps steps within 5% to 15% 

                    DrawCross(p, 20);
                }

                yield return p3;
            }
        }

        /// <summary>
        /// Draws a bezier curve to the current surface.
        /// </summary>
        /// <param name="curve">Some bezier curve.</param>
        /// <param name="width">The thickness of the line in pixels.</param>
        /// <exception cref="ArgumentNullException">Thrown if <paramref name="curve"/> is null.</exception>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public void DrawCurve(Bezier curve, float width = 1F)
        {
            if (curve is null) { throw new ArgumentNullException(nameof(curve)); }
            DrawPolyLine(curve.GenerateInterpolatedSequence(), width);
        }

        #endregion

        public void DrawPolyLine(IEnumerable<Vector> points, float width = 1F)
        {
            // Copy enumerable into temporary list
            _sequence.Clear();
            _sequence.AddRange(points);

            // Clear mesh
            _mesh.Clear();

            if (_sequence.Count >= 2)
            {
                var a = _sequence[0];
                for (var i = 1; i < _sequence.Count; i++)
                {
                    var t0 = (i - 1) / (float) _sequence.Count;
                    var t1 = (i + 0) / (float) _sequence.Count;

                    var b = _sequence[i];

                    var pA = Vector.Normalize(b - a).Perpendicular;
                    var pB = pA;

                    if (i < _sequence.Count - 1)
                    {
                        var c = _sequence[i + 1];
                        pB = Vector.Normalize(c - b).Perpendicular;
                    }

                    // Add segment to mesh
                    _mesh.AddVertex(new MeshVertex(a - pA * width, (t0, 0F)));
                    _mesh.AddVertex(new MeshVertex(a + pA * width, (t0, 1F)));
                    _mesh.AddVertex(new MeshVertex(b + pB * width, (t1, 1F)));
                    _mesh.AddVertex(new MeshVertex(a - pA * width, (t0, 0F)));
                    _mesh.AddVertex(new MeshVertex(b + pB * width, (t1, 1F)));
                    _mesh.AddVertex(new MeshVertex(b - pB * width, (t1, 0F)));

                    // DrawCross(a, 20, 1);

                    a = b;
                }

                // Draw mesh
                Draw(_mesh, Image.Default, Matrix.Identity);
            }
        }

        #region Draw Cross

        /// <summary>
        /// Draws a simple axis aligned 'cross' or 'plus' shape, useful for debugging positions.
        /// </summary> 
        /// <param name="center">The position of the cross.</param>
        /// <param name="size">Size in screen pixels (not world space).</param>
        /// <param name="width">Width of the lines screen pixels (not world space).</param>
        public void DrawCross(in Vector center, float size = 3, float width = 1F)
        {
            // Draw axis
            DrawLine(center + (Vector.Left * size), center + (Vector.Right * size), width);
            DrawLine(center + (Vector.Up * size), center + (Vector.Down * size), width);
        }

        #endregion

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
            _mesh.Clear();

            // Append vertices
            _mesh.AddVertex(new MeshVertex(a, Vector.Zero));
            _mesh.AddVertex(new MeshVertex(b, Vector.Zero));
            _mesh.AddVertex(new MeshVertex(c, Vector.Zero));

            // Draw mesh
            Draw(_mesh, Image.Default, Matrix.Identity);
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
