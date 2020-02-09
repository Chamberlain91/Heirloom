using System;
using System.Collections.Generic;
using System.Runtime.CompilerServices;

namespace Heirloom.Math
{
    public struct Triangle : IShape, IEquatable<Triangle>
    {
        /// <summary>
        /// The first point.
        /// </summary>
        public Vector A;

        /// <summary>
        /// The second point.
        /// </summary>
        public Vector B;

        /// <summary>
        /// The third point.
        /// </summary>
        public Vector C;

        #region Constructors

        public Triangle(Vector a, Vector b, Vector c)
        {
            A = a;
            B = b;
            C = c;
        }

        #endregion

        #region Properties

        /// <summary>
        /// Gets the bounds of this triangle.
        /// </summary>
        public Rectangle Bounds => Rectangle.FromPoints(A, B, C);

        /// <summary>
        /// Gets the area of this triangle.
        /// </summary>
        public float Area => Vector.Cross(B - A, C - A) / 2F;

        /// <summary>
        /// Gets the center of triangle (mean of corner points).
        /// </summary>
        public Vector Centroid => (A + B + C) / 3F;

        #endregion

        /// <summary>
        /// Create a polygon from this triangle.
        /// </summary>
        public Polygon ToPolygon()
        {
            // Clone this rectangle as a polygon
            var vertices = PolygonTools.RequestTempPolygon(in this);
            var polygon = new Polygon(vertices);

            // Recycle temp poylgon and return clone.
            PolygonTools.RecycleTempPolygon(vertices);
            return polygon;
        }

        #region Closest Point

        /// <summary>
        /// Gets the closest point on the triangle to the specified point.
        /// </summary>
        public Vector GetClosestPoint(in Vector point)
        {
            // Get temporary polygon representation
            var polygon = PolygonTools.RequestTempPolygon(in this);

            // Check for overlap
            var result = PolygonTools.GetClosestPoint(polygon, point);

            // Recycle temporary polygon and return overlap status
            PolygonTools.RecycleTempPolygon(polygon);
            return result;
        }

        #endregion

        #region Contains

        /// <summary>
        /// Determines if this triangle contains the specified point.
        /// </summary>
        public bool Contains(in Vector point)
        {
            return ContainsPoint(in A, in B, in C, point);
        }

        /// <summary>
        /// Determines if the triangle defined by <paramref name="a"/>, <paramref name="b"/>, <paramref name="c"/> contains the specified point.
        /// </summary>
        public static bool ContainsPoint(in Vector a, in Vector b, in Vector c, in Vector point)
        {
            // Get temporary polygon representation
            var polygon = PolygonTools.RequestTempPolygon(3);

            // Configure temp polygon to input points
            polygon[0] = a;
            polygon[1] = b;
            polygon[2] = c;

            // Checks if the polygon contains the point
            var result = PolygonTools.ContainsPoint(polygon, in point);

            // Recycle temporary polygon and return containment status
            PolygonTools.RecycleTempPolygon(polygon);
            return result;
        }

        #endregion

        #region Overlaps

        /// <summary>
        /// Determines if this triangle overlaps another shape.
        /// </summary>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public bool Overlaps(IShape shape)
        {
            return shape switch
            {
                Circle cir => Overlaps(in cir),
                Triangle tri => Overlaps(in tri),
                Rectangle rec => Overlaps(in rec),
                Polygon pol => Overlaps(pol),

                // Unknown shape
                _ => throw new InvalidOperationException("Unable to determine overlap, shape was not a known type."),
            };
        }

        /// <summary>
        /// Determines if this triangle overlaps the specified circle.
        /// </summary>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public bool Overlaps(in Circle circle)
        {
            // circle has the implementation
            return circle.Overlaps(in this);
        }

        /// <summary>
        /// Determines if this triangle overlaps another triangle.
        /// </summary>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public bool Overlaps(in Triangle triangle)
        {
            // Get temporary polygon representation
            var polygon = PolygonTools.RequestTempPolygon(in this);

            // Check for overlap
            var result = triangle.Overlaps(polygon);

            // Recycle temporary polygon and return overlap status
            PolygonTools.RecycleTempPolygon(polygon);
            return result;
        }

        /// <summary>
        /// Determines if this triangle overlaps the specified rectangle.
        /// </summary>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public bool Overlaps(in Rectangle rectangle)
        {
            // Get temporary polygon representation
            var polygon = PolygonTools.RequestTempPolygon(in this);

            // Check for overlap
            var result = rectangle.Overlaps(polygon);

            // Recycle temporary polygon and return overlap status
            PolygonTools.RecycleTempPolygon(polygon);
            return result;
        }

        /// <summary>
        /// Determines if this triangle overlaps the specified convex polygon.
        /// </summary>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public bool Overlaps(IReadOnlyList<Vector> polygon)
        {
            // Get temporary polygon representation
            var other = PolygonTools.RequestTempPolygon(in this);

            // Check for overlap
            var result = SeparatingAxis.Overlaps(polygon, other);

            // Recycle temporary polygon and return overlap status
            PolygonTools.RecycleTempPolygon(other);
            return result;
        }

        /// <summary>
        /// Determines if this triangle overlaps the specified simple polygon.
        /// </summary>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public bool Overlaps(Polygon polygon)
        {
            // polygon has the implementation
            return polygon.Overlaps(in this);
        }

        #endregion

        #region Axis Projection

        /// <summary>
        /// Project this polygon onto the specified axis.
        /// </summary>
        public Range Project(in Vector axis)
        {
            // Get temporary polygon representation
            var polygon = PolygonTools.RequestTempPolygon(in this);

            // Project polygon onto axis
            var result = PolygonTools.Project(polygon, in axis);

            // Recycle temporary polygon and return overlap status
            PolygonTools.RecycleTempPolygon(polygon);
            return result;
        }

        #endregion

        #region Raycast

        /// <summary>
        /// Peforms a raycast onto this rectangle, returning true upon intersection.
        /// </summary>
        /// <param name="ray">Some ray.</param>
        public bool Raycast(in Ray ray)
        {
            return Raycast(in ray, out _);
        }

        /// <summary>
        /// Peforms a raycast onto this rectangle, returning true upon intersection.
        /// </summary>
        /// <param name="ray">Some ray.</param>
        /// <param name="contact">Ray intersection information.</param>
        public bool Raycast(in Ray ray, out RayContact contact)
        {
            // Get temporary polygon representation
            var polygon = PolygonTools.RequestTempPolygon(in this);

            // Raycast against polygon
            var status = PolygonTools.Raycast(polygon, in ray, out contact);

            // Recycle temporary polygon and return overlap status
            PolygonTools.RecycleTempPolygon(polygon);
            return status;
        }

        #endregion

        #region Barycentric Coordinates

        /// <summary>
        /// Computes the barycentric coefficients of the point <paramref name="p"/> within the triangle.
        /// </summary>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public void Barycentric(in Vector p, out float u, out float v, out float w)
        {
            Barycentric(in p, in A, in B, in C, out u, out v, out w);
        }

        /// <summary>
        /// Computes the barycentric coefficients of the point <paramref name="p"/> within the triangle <paramref name="a"/>, <paramref name="b"/>, <paramref name="c"/>.
        /// </summary>
        public static void Barycentric(in Vector p, in Vector a, in Vector b, in Vector c, out float u, out float v, out float w)
        {
            var v0 = b - a;
            var v1 = c - a;
            var v2 = p - a;

            var d00 = Vector.Dot(v0, v0);
            var d01 = Vector.Dot(v0, v1);
            var d11 = Vector.Dot(v1, v1);
            var d20 = Vector.Dot(v2, v0);
            var d21 = Vector.Dot(v2, v1);

            var denom = d00 * d11 - d01 * d01;
            v = (d11 * d20 - d01 * d21) / denom;
            w = (d00 * d21 - d01 * d20) / denom;
            u = 1.0f - v - w;
        }

        #endregion

        #region Circumcircle (Static)

        /// <summary>
        /// Computes the circumcircle for the specified triangle.
        /// </summary>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Circle CreateCircumcircle(in Triangle tri)
        {
            return CreateCircumcircle(in tri.A, in tri.B, in tri.C);
        }

        /// <summary>
        /// Computes the circumcircle for the specified triangle.
        /// </summary>
        public static Circle CreateCircumcircle(in Vector a, in Vector b, in Vector c)
        // https://gist.github.com/mutoo/5617691
        {
            var A = b.X - a.X;
            var B = b.Y - a.Y;
            var C = c.X - a.X;
            var D = c.Y - a.Y;
            var E = A * (a.X + b.X) + B * (a.Y + b.Y);
            var F = C * (a.X + c.X) + D * (a.Y + c.Y);
            var G = 2 * (A * (c.Y - b.Y) - B * (c.X - b.X));

            // If the points of the triangle are collinear, then just find the
            // extremes and use the midpoint as the center of the circumcircle.
            if (Calc.NearZero(G))
            {
                var minx = Calc.Min(a.X, b.X, c.X);
                var miny = Calc.Min(a.Y, b.Y, c.Y);

                var dx = (Calc.Max(a.X, b.X, c.X) - minx) * 0.5F;
                var dy = (Calc.Max(a.Y, b.Y, c.Y) - miny) * 0.5F;

                var x = minx + dx;
                var y = miny + dy;

                return new Circle(x, y, Calc.Sqrt((dx * dx) + (dy * dy)));
            }
            else
            {
                var x = ((D * E) - (B * F)) / G;
                var y = ((A * F) - (C * E)) / G;

                return new Circle(x, y, Calc.Distance(x, y, a.X, a.Y));
            }
        }

        #endregion

        #region Deconstruct

        public void Deconstruct(out Vector a, out Vector b, out Vector c)
        {
            a = A;
            b = B;
            c = C;
        }

        #endregion

        #region Equality

        public override bool Equals(object obj)
        {
            return obj is Triangle triangle && Equals(triangle);
        }

        public bool Equals(Triangle other)
        {
            return A.Equals(other.A) &&
                   B.Equals(other.B) &&
                   C.Equals(other.C) &&
                   Bounds.Equals(other.Bounds) &&
                   Area == other.Area;
        }

        public override int GetHashCode()
        {
            return HashCode.Combine(A, B, C, Bounds, Area);
        }

        public static bool operator ==(Triangle left, Triangle right)
        {
            return left.Equals(right);
        }

        public static bool operator !=(Triangle left, Triangle right)
        {
            return !(left == right);
        }

        #endregion

        public override string ToString()
        {
            return $"(Triangle, {A}, {B}, {C})";
        }
    }
}
