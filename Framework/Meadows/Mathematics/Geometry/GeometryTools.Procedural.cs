using System;
using System.Collections.Generic;

namespace Meadows.Mathematics
{
    public static partial class GeometryTools
    {
        /// <summary>
        /// Triangulates a convex polygon.
        /// </summary>
        public static IEnumerable<Triangle> TriangulateConvex(IEnumerable<Vector> vertices)
        {
            var a0 = default(Vector);
            var a1 = default(Vector);

            var c = 0;

            foreach (var point in vertices)
            {
                if (c == 0) { a0 = point; c++; }
                else
                if (c == 1) { a1 = point; c++; }
                else
                {
                    yield return new Triangle(a0, a1, point);
                    a1 = point;
                }
            }
        }

        #region Generate Regular Polygon

        /// <summary>
        /// Generates the points of a regular polygon.
        /// </summary>
        public static IEnumerable<Vector> GenerateRegularPolygon(int segments, float radius)
        {
            return GenerateRegularPolygon(Vector.Zero, segments, radius);
        }

        /// <summary>
        /// Generates the points of a regular polygon.
        /// </summary>
        public static IEnumerable<Vector> GenerateRegularPolygon(Vector center, int segments, float radius)
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

        #endregion

        #region Generate Star

        /// <summary>
        /// Generates the points of a common five-pointed star.
        /// </summary>
        public static IEnumerable<Vector> GenerateStar(float radius)
        {
            return GenerateStar(Vector.Zero, radius);
        }

        /// <summary>
        /// Generates the points of a common five-pointed star.
        /// </summary>
        public static IEnumerable<Vector> GenerateStar(Vector center, float radius)
        {
            return GenerateStar(center, 5, radius);
        }

        /// <summary>
        /// Generates the points of a star.
        /// </summary>
        public static IEnumerable<Vector> GenerateStar(int numPoints, float radius)
        {
            return GenerateStar(Vector.Zero, numPoints, radius * 0.6F, radius);
        }

        /// <summary>
        /// Generates the points of a star.
        /// </summary>
        public static IEnumerable<Vector> GenerateStar(Vector center, int numPoints, float radius)
        {
            return GenerateStar(center, numPoints, radius * 0.6F, radius);
        }

        /// <summary>
        /// Generates the points of a star.
        /// </summary>
        public static IEnumerable<Vector> GenerateStar(int numPoints, float innerRadius, float outerRadius)
        {
            return GenerateStar(Vector.Zero, numPoints, innerRadius, outerRadius);
        }

        /// <summary>
        /// Generates the points of a star.
        /// </summary>
        public static IEnumerable<Vector> GenerateStar(Vector center, int numPoints, float innerRadius, float outerRadius)
        {
            if (numPoints < 2) { throw new ArgumentOutOfRangeException(); }

            numPoints *= 2; // For each point and valley

            for (var i = 0; i < numPoints; i++)
            {
                var a = i / (float) numPoints * Calc.TwoPi;
                var r = i % 2 == 0 ? innerRadius : outerRadius;

                // 
                var x = center.X + Calc.Cos(a) * r;
                var y = center.Y + Calc.Sin(a) * r;

                yield return new Vector(x, y);
            }
        }

        #endregion

        #region Vertices of Shape

        /// <summary>
        /// Gets the vertices of the specified shape.
        /// </summary>
        /// <param name="shape">Some shape.</param>
        /// <param name="error">An error factor for approximating a circle.</param>
        /// <returns></returns>
        public static IEnumerable<Vector> GetVertices(this IShape shape, float error = 1F)
        {
            return GetVertices(shape, Matrix.Identity, error);
        }

        /// <summary>
        /// Gets the vertices of the specified shape.
        /// </summary>
        /// <param name="shape">Some shape.</param>
        /// <param name="error">An error factor for approximating a circle.</param>
        /// <returns></returns>
        public static IEnumerable<Vector> GetVertices(this IShape shape, Matrix matrix, float error = 1F)
        {
            switch (shape)
            {
                case null:
                    throw new ArgumentNullException(nameof(shape));

                default:
                    throw new ArgumentException("Unknown shape type.", nameof(shape));

                case Rectangle rectangle:
                    yield return matrix * rectangle.TopLeft;
                    yield return matrix * rectangle.TopRight;
                    yield return matrix * rectangle.BottomRight;
                    yield return matrix * rectangle.BottomLeft;
                    break;

                case Triangle triangle:
                    yield return matrix * triangle.A;
                    yield return matrix * triangle.B;
                    yield return matrix * triangle.C;
                    break;

                case Circle circle:
                {
                    var segments = GetCircleApproximateSegmentCount(circle.Radius, error);
                    foreach (var v in GenerateRegularPolygon(circle.Position, segments, circle.Radius))
                    {
                        yield return matrix * v;
                    }

                    break;
                }

                case Polygon polygon:
                {
                    // todo: do we only accept convex polygons?
                    foreach (var v in polygon.Vertices)
                    {
                        yield return matrix * v;
                    }

                    break;
                }
            }
        }

        internal static int GetCircleApproximateSegmentCount(float radius, float error)
        {
            // todo: compute based on some arc error?
            return 5 + (int) Calc.Sqrt(radius * 2 * Calc.Pi);
        }

        #endregion
    }
}
