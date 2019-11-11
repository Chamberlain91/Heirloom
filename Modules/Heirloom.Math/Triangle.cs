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

        #endregion

        #region Closest Point

        public Vector ClosestPoint(in Vector point)
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

        public bool ContainsPoint(in Vector point)
        {
            return ContainsPoint(in A, in B, in C, point);
        }

        public static bool ContainsPoint(in Vector a, in Vector b, in Vector c, in Vector point)
        // todo: source?
        {
            //return true if the point to test is one of the vertices
            if (point.Equals(a) || point.Equals(b) || point.Equals(c))
            {
                return true;
            }

            var oddNodes = false;

            if (CheckPointToSegment(c, a, point))
            {
                oddNodes = !oddNodes;
            }

            if (CheckPointToSegment(a, b, point))
            {
                oddNodes = !oddNodes;
            }

            if (CheckPointToSegment(b, c, point))
            {
                oddNodes = !oddNodes;
            }

            return oddNodes;

            static bool CheckPointToSegment(Vector sA, Vector sB, Vector sP)
            {
                if ((sA.Y < sP.Y && sB.Y >= sP.Y) || (sB.Y < sP.Y && sA.Y >= sP.Y))
                {
                    var x = sA.X + ((sP.Y - sA.Y) / (sB.Y - sA.Y) * (sB.X - sA.X));
                    if (x < sP.X) { return true; }
                }

                return false;
            }
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

        public bool Raycast(in Ray ray)
        {
            // Get temporary polygon representation
            var polygon = PolygonTools.RequestTempPolygon(in this);

            // Raycast against polygon
            var status = PolygonTools.Raycast(polygon, in ray);

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
