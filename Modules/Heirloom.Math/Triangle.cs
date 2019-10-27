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

        [ThreadStatic]
        private static readonly Vector[][] _polygon = new Vector[2][] { new Vector[3], new Vector[3] };

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
            var poly = GetTempPolygon(0);
            return Polygon.ClosestPoint(poly, point);
        }

        #endregion

        #region Contains

        public bool Contains(in Vector point)
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

        public bool Overlaps(IShape shape)
        {
            // tri - tri
            if (shape is Triangle tri) { return Overlaps(tri); }
            // tri - rec
            else if (shape is Rectangle rec) { return Overlaps(rec); }
            // tri - cir
            else if (shape is Circle cir) { return Overlaps(cir); }
            // tri - pol
            else if (shape is IPolygon pol) { return Overlaps((IReadOnlyList<Vector>) pol); }
            // unknown case
            else
            {
                throw new InvalidOperationException("Unable to determine overlap, shape was not a known type.");
            }
        }

        public bool Overlaps(Circle circle)
        {
            var poly = GetTempPolygon(0);
            return Collisions.Overlaps(circle, poly);
        }

        public bool Overlaps(Triangle triangle)
        {
            var polyA = GetTempPolygon(0);
            var polyB = triangle.GetTempPolygon(1);

            return Collisions.Overlaps(polyA, polyB);
        }

        public bool Overlaps(Rectangle rectangle)
        {
            var tri = GetTempPolygon(0);
            var rec = rectangle.GetTempPolygon(1);

            return Collisions.Overlaps(tri, rec);
        }

        public bool Overlaps(IReadOnlyList<Vector> polygon)
        {
            var tri = GetTempPolygon(0);
            return Collisions.Overlaps(tri, polygon);
        }

        #endregion

        #region Raycast

        public bool Raycast(in Ray ray, out Contact contact)
        {
            var poly = GetTempPolygon(0);
            return Polygon.Raycast(poly, in ray, out contact);
        }

        public bool Raycast(in Ray ray)
        {
            var poly = GetTempPolygon(0);
            return Polygon.Raycast(poly, in ray);
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

        internal Vector[] GetTempPolygon(int number)
        {
            var polygon = _polygon[number];

            polygon[0] = A;
            polygon[1] = B;
            polygon[2] = C;

            return polygon;
        }

        public override string ToString()
        {
            return $"(Triangle, {A}, {B}, {C})";
        }
    }
}
