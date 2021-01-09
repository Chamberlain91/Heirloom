using System;
using System.Collections.Generic;
using System.Runtime.CompilerServices;

using Meadows.Mathematics;

namespace Meadows.Drawing
{
    public enum LineJoinType
    {
        None,
        Bevel
    }

    public abstract partial class GraphicsContext
    {
        private static readonly Mesh _mesh = new Mesh();
        private static readonly List<Vector> _sequence = new List<Vector>();

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
            var edgeLength = edge.LengthSquared;
            if (edgeLength > 0)
            {
                // Note: Doing the sqrt here to avoid it outside the branch
                edgeLength = Calc.Sqrt(edgeLength);

                // todo: should this be ApproximatePixelScale?
                var step = 1F / edgeLength;

                // todo: inline into one matrix construction?
                var transform = Matrix.CreateRotation(edge.Angle)
                              * Matrix.CreateTranslation(-step / 2F, -width / 2F)
                              * Matrix.CreateScale(edgeLength + step, width);

                // todo: confirm the sanity of this optimization (saves on 12 mul ops)
                // transform = Matrix.CreateTranslation(p0 + (0.5F, 0.5F)) * transform;
                transform.M2 += p0.X + 0.5F;
                transform.M5 += p0.Y + 0.5F;

                DrawImage(Image.Default, transform);
            }
            else
            {
                // A single pixel "DrawLine(a, a)"
                var transform = Matrix.CreateTranslation(p0);
                DrawImage(Image.Default, transform);
            }
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

        #region Draw PolyLine

        public void DrawPolyLine(IEnumerable<Vector> points, float width = 1F, LineJoinType joinType = LineJoinType.Bevel, bool loop = false)
        // todo: miter / round joins/
        {
            // Copy enumerable into temporary list
            _sequence.Clear();
            _sequence.AddRange(points);

            // To make width radial
            width /= 2F;

            // Clear mesh
            _mesh.Clear();

            if (_sequence.Count >= 2)
            {
                for (var i = 1; i < _sequence.Count; i++)
                {
                    // Get points around join
                    var v0 = _sequence[i - 1];
                    var v1 = _sequence[i + 0];
                    var v2 = _sequence[(i + 1) % _sequence.Count];

                    // Compute edge directions
                    var e01 = Vector.Normalize(v1 - v0).Perpendicular;
                    var e12 = Vector.Normalize(v2 - v1).Perpendicular;

                    // Append segment and join
                    AppendSegment(v0, v1, e01);
                    if (i < _sequence.Count - 1 || loop)
                    {
                        AppendJoin(v1, e01, e12);
                    }
                }

                // at least a triangle is required to make a loop
                if (loop && _sequence.Count >= 3)
                {
                    // Get points around join
                    var v0 = _sequence[^1];
                    var v1 = _sequence[0];
                    var v2 = _sequence[1];

                    // Compute edge directions
                    var e01 = Vector.Normalize(v1 - v0).Perpendicular;
                    var e12 = Vector.Normalize(v2 - v1).Perpendicular;

                    // Append segment and join
                    AppendSegment(v0, v1, e01);
                    AppendJoin(v1, e01, e12);
                }

                // Draw mesh
                Draw(_mesh, Texture.Default, Matrix.Identity);
            }

            void AppendSegment(Vector v0, Vector v1, Vector e01)
            {
                _mesh.AddVertex(new MeshVertex(v0 - e01 * width, Vector.Zero)); // tri 1
                _mesh.AddVertex(new MeshVertex(v0 + e01 * width, Vector.Zero));
                _mesh.AddVertex(new MeshVertex(v1 + e01 * width, Vector.Zero));
                _mesh.AddVertex(new MeshVertex(v0 - e01 * width, Vector.Zero)); // tri 2
                _mesh.AddVertex(new MeshVertex(v1 + e01 * width, Vector.Zero));
                _mesh.AddVertex(new MeshVertex(v1 - e01 * width, Vector.Zero));
            }

            void AppendJoin(Vector v1, Vector e01, Vector e12)
            {
                // Must be greater than 1 pixel to have significance.
                // todo: determine if this is sane
                if (ApproximatePixelScale * width > 1)
                {
                    switch (joinType)
                    {
                        case LineJoinType.Bevel:
                            _mesh.AddVertex(new MeshVertex(v1 - e01 * width, Vector.Zero)); // tri 1
                            _mesh.AddVertex(new MeshVertex(v1 + e01 * width, Vector.Zero));
                            _mesh.AddVertex(new MeshVertex(v1 + e12 * width, Vector.Zero));
                            _mesh.AddVertex(new MeshVertex(v1 - e01 * width, Vector.Zero)); // tri 2
                            _mesh.AddVertex(new MeshVertex(v1 + e12 * width, Vector.Zero));
                            _mesh.AddVertex(new MeshVertex(v1 - e12 * width, Vector.Zero));
                            break;
                    }
                }
            }
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

        #region Draw Rectangle

        /// <summary>
        /// Draws a rectangle to the current surface.
        /// </summary> 
        /// <param name="rectangle">The rectangular region of the rectangle.</param>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public void DrawRect(in Rectangle rectangle)
        {
            DrawImage(Texture.Default, in rectangle);
        }

        /// <summary>
        /// Draws the outline of a rectangel to the current surface.
        /// </summary> 
        /// <param name="rectangle">The rectangular region of the rectangle.</param>
        /// <param name="width">Width of the outline in pixels.</param>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public void DrawRectOutline(in Rectangle rectangle, float width = 1)
        {
            DrawPolyLine(rectangle.GetVertices(), width, loop: true);
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
            Draw(_mesh, Texture.Default, Matrix.Identity);
        }

        /// <summary>
        /// Draw a triangle outline to the current surface.
        /// </summary> 
        /// <param name="triangle">The triangle to draw.</param>
        /// <param name="width">The thickness of the line in pixels.</param>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public void DrawTriangleOutline(in Triangle triangle, float width = 1F)
        {
            DrawPolyLine(triangle.GetVertices(), width, loop: true);
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
            DrawTriangleOutline(new Triangle(a, b, c), width);
        }

        #endregion

        #region Draw Circle

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public void DrawCircle(Vector center, float radius, float error = 1F)
        {
            var segments = GeometryTools.GetCircleApproximateSegmentCount(radius, error * ApproximatePixelScale);
            DrawRegularPolygon(center, radius, segments);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public void DrawCircleOutline(Vector center, float radius, float width = 1F, float error = 1F)
        {
            var segments = GeometryTools.GetCircleApproximateSegmentCount(radius, error * ApproximatePixelScale);
            DrawRegularPolygonOutline(center, radius, segments, width);
        }

        #endregion

        #region Draw Regular Polygon

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public void DrawRegularPolygon(Vector center, float radius, int segments)
        // todo: optimize/evaluate performance
        {
            //
            _mesh.Clear();

            // Append vertices
            var vertices = GeometryTools.GenerateRegularPolygon(center, segments, radius);
            foreach (var (a, b, c) in GeometryTools.TriangulateConvex(vertices))
            {
                _mesh.AddVertex(new MeshVertex(a, Vector.Zero));
                _mesh.AddVertex(new MeshVertex(b, Vector.Zero));
                _mesh.AddVertex(new MeshVertex(c, Vector.Zero));
            }

            // 
            Draw(_mesh, Texture.Default);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public void DrawRegularPolygonOutline(Vector center, float radius, int segments, float width = 1F)
        // todo: optimize/evaluate performance
        {
            DrawPolyLine(GeometryTools.GenerateRegularPolygon(center, segments, radius), width, loop: true);
        }

        #endregion

        #region Draw Polygon

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public void DrawPolygon(Polygon polygon)
        // todo: optimize/evaluate performance
        {
            _mesh.Clear();

            // todo: is there a better algorithm for real-time performance?
            foreach (var (a, b, c) in polygon.Triangulate())
            {
                _mesh.AddVertex(new MeshVertex(a, Vector.Zero));
                _mesh.AddVertex(new MeshVertex(b, Vector.Zero));
                _mesh.AddVertex(new MeshVertex(c, Vector.Zero));
            }

            // 
            Draw(_mesh, Texture.Default);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public void DrawPolygonOutline(Polygon polygon, float width = 1F)
        {
            DrawPolyLine(polygon.Vertices, width, loop: true);
        }

        #endregion
    }
}
