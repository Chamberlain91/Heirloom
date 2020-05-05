using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;

using Heirloom.Geometry;

namespace Heirloom
{
    public abstract partial class GraphicsContext
    {
        // used to center the line within the 1x1 pixel image to anchor at left-center
        private static readonly Matrix _lineOffsetMatrix = Matrix.CreateTranslation(0, -1 / 2F);

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

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public void DrawCurve(Curve curve, float width = 1F)
        {
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

        #region Draw Circle

        /// <summary>
        /// Draws a circle to the current surface.
        /// </summary> 
        /// <param name="circle">The circle to draw.</param>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public void DrawCircle(in Circle circle)
        {
            DrawCircle(in circle.Position, circle.Radius);
        }

        /// <summary>
        /// Draws a circle to the current surface.
        /// </summary> 
        /// <param name="position">The center of the circle.</param>
        /// <param name="radius">The radius of the circle.</param>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public void DrawCircle(in Vector position, float radius)
        {
            var sides = ComputeCircleSegments(radius);
            DrawPolygon(position, sides, radius);
        }

        /// <summary>
        /// Draws the outline of a circle to the current surface.
        /// </summary> 
        /// <param name="circle">The circle to draw.</param>
        /// <param name="width">Width of the outline in pixels.</param>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public void DrawCircleOutline(in Circle circle, float width = 1F)
        {
            DrawCircleOutline(in circle.Position, circle.Radius, width);
        }

        /// <summary>
        /// Draws the outline of a circle to the current surface.
        /// </summary> 
        /// <param name="position">The centr of the circle.</param>
        /// <param name="radius">The radius of the circle.</param>
        /// <param name="width">Width of the outline in pixels.</param>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public void DrawCircleOutline(in Vector position, float radius, float width = 1F)
        {
            var sides = ComputeCircleSegments(radius);
            DrawPolygonOutline(position, sides, radius, width);
        }

        private static int ComputeCircleSegments(float radius)
        {
            // todo: This is a brutal inaccurate piece of code that can be fixed/replaced with a 
            //       mathimatically rigid approximation using arc length and linear approximation 
            //       using a triangle. This is tricky since the number of needed segments depends
            //       the global transformation and the subsequent scaling between screen space 
            //       and drawing space.

            var s = (int) (Calc.Sqrt(radius) * 2.8F);
            return Calc.Clamp(s, 3, 64);
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
        /// <param name="width">The thickness of the line in pixels.</param>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public void DrawTriangle(in Vector a, in Vector b, in Vector c)
        {
            _temporaryMesh.Clear();

            // Append vertices
            _temporaryMesh.AddVertex(new Vertex(a, Vector.Zero));
            _temporaryMesh.AddVertex(new Vertex(b, Vector.Zero));
            _temporaryMesh.AddVertex(new Vertex(c, Vector.Zero));

            // Append indices
            _temporaryMesh.AddIndices(0);
            _temporaryMesh.AddIndices(1);
            _temporaryMesh.AddIndices(2);

            // Draw mesh
            DrawMesh(Image.Default, _temporaryMesh, Matrix.Identity);
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

        #region Draw Regular Polygon

        /// <summary>
        /// Draws a regular polygon to the current surface.
        /// </summary> 
        /// <param name="sides">The number of sides in the regular polygon.</param>
        /// <param name="radius">The radius of the regular polygon.</param>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public void DrawPolygon(in Vector position, int sides, float radius)
        {
            // 
            _temporaryMesh.Clear();

            // Append vertices
            foreach (var pt in GenerateRegularPolygon(position, sides, radius))
            {
                var vertex = new Vertex(pt, Vector.Zero);
                _temporaryMesh.AddVertex(vertex);
            }

            // Append indices
            for (var i = 1; i < (sides - 1); i++)
            {
                _temporaryMesh.AddIndex(0);
                _temporaryMesh.AddIndex(i + 0);
                _temporaryMesh.AddIndex(i + 1);
            }

            // 
            DrawMesh(Image.Default, _temporaryMesh, Matrix.Identity);
        }

        /// <summary>
        /// Draws the outline of a regular polygon to the current surface.
        /// </summary> 
        /// <param name="sides">The number of sides in the regular polygon.</param>
        /// <param name="radius">The radius of the regular polygon.</param>
        /// <param name="width">Width of the outline in pixels.</param>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public void DrawPolygonOutline(in Vector position, int sides, float radius, float width = 1F)
        {
            var regular = GenerateRegularPolygon(position, sides, radius);
            DrawPolygonOutline(regular, width);
        }

        #endregion

        #region Draw Polygon

        /// <summary>
        /// Draws a simple polygon to the current surface.
        /// </summary> 
        /// <param name="polygon">Some polygon.</param>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public void DrawPolygon(Polygon polygon)
        {
            DrawPolygon(polygon.Vertices, Matrix.Identity);
        }

        /// <summary>
        /// Draws a simple polygon to the current surface.
        /// </summary> 
        /// <param name="polygon">Some polygon.</param>
        /// <param name="transform">Some transform.</param>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public void DrawPolygon(Polygon polygon, in Matrix transform)
        {
            DrawPolygon(polygon.Vertices, in transform);
        }

        /// <summary>
        /// Draws a simple polygon to the current surface.
        /// </summary> 
        /// <param name="polygon">Some polygon.</param>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public void DrawPolygon(IEnumerable<Vector> polygon)
        {
            DrawPolygon(polygon, Matrix.Identity);
        }

        /// <summary>
        /// Draws a simple polygon to the current surface.
        /// </summary> 
        /// <param name="polygon">Some polygon.</param>
        /// <param name="transform">Some transform.</param>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public void DrawPolygon(IEnumerable<Vector> polygon, in Matrix transform)
        {
            if (polygon?.Any() ?? throw new ArgumentNullException(nameof(polygon)))
            {
                _temporaryMesh.Clear();

                // Append vertices
                foreach (var pt in polygon)
                {
                    var vertex = new Vertex(pt, Vector.Zero);
                    _temporaryMesh.AddVertex(vertex);
                }

                // Append indices
                foreach (var (a, b, c) in PolygonTools.TriangulateIndices(polygon))
                {
                    _temporaryMesh.AddIndices(a);
                    _temporaryMesh.AddIndices(b);
                    _temporaryMesh.AddIndices(c);
                }

                // Draw mesh
                DrawMesh(Image.Default, _temporaryMesh, transform);
            }
        }

        /// <summary>
        /// Draws the outline of a simple polygon to the current surface.
        /// </summary> 
        /// <param name="polygon">Some polygon.</param>
        /// <param name="width">Width of the outline in pixels.</param>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public void DrawPolygonOutline(Polygon polygon, float width = 1F)
        {
            DrawPolygonOutline(polygon.Vertices, Matrix.Identity, width);
        }

        /// <summary>
        /// Draws the outline of a simple polygon to the current surface.
        /// </summary> 
        /// <param name="polygon">Some polygon.</param>
        /// <param name="transform">Some transform.</param>
        /// <param name="width">Width of the outline in pixels.</param>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public void DrawPolygonOutline(Polygon polygon, in Matrix transform, float width = 1F)
        {
            DrawPolygonOutline(polygon.Vertices, in transform, width);
        }

        /// <summary>
        /// Draws the outline of a simple polygon to the current surface.
        /// </summary> 
        /// <param name="polygon">Some polygon.</param>
        /// <param name="width">Width of the outline in pixels.</param>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public void DrawPolygonOutline(IEnumerable<Vector> polygon, float width = 1F)
        {
            DrawPolygonOutline(polygon, Matrix.Identity, width);
        }

        /// <summary>
        /// Draws the outline of a simple polygon to the current surface.
        /// </summary> 
        /// <param name="polygon">Some polygon.</param>
        /// <param name="transform">Some transform.</param>
        /// <param name="width">Width of the outline in pixels.</param>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public void DrawPolygonOutline(IEnumerable<Vector> polygon, in Matrix transform, float width = 1F)
        {
            if (polygon?.Any() ?? throw new ArgumentNullException(nameof(polygon)))
            {
                var first = transform * polygon.First();
                var point = first;

                // Draw (i+1 to n-1)
                foreach (var v in polygon.Skip(1))
                {
                    var V = transform * v;
                    DrawLine(point, V, width);
                    point = V;
                }

                // Draw (n-1 to 0)
                DrawLine(point, first, width);
            }
        }

        #endregion

        /// <summary>
        /// Generates the points of a regular polygon.
        /// </summary> 
        private static IEnumerable<Vector> GenerateRegularPolygon(Vector center, int segments, float radius)
        {
            if (segments < 3) { throw new ArgumentOutOfRangeException(); }

            for (var i = 0; i < segments; i++)
            {
                var a = i / (float) segments * Calc.TwoPi;

                // 
                var x = center.X + Calc.Cos(a) * radius;
                var y = center.Y + Calc.Sin(a) * radius;

                yield return new Vector(x, y);
            }
        }
    }
}
