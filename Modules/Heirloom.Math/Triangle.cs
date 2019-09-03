using System.Runtime.CompilerServices;

namespace Heirloom.Math
{
    public struct Triangle
    {
        public Vector A;

        public Vector B;

        public Vector C;

        #region Constructors

        public Triangle(Vector a, Vector b, Vector c)
        {
            A = a;
            B = b;
            C = c;
        }

        #endregion

        #region Contains

        public bool Contains(Vector point)
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

            bool CheckPointToSegment(Vector sA, Vector sB, Vector sP)
            {
                if ((sA.Y < sP.Y && sB.Y >= sP.Y) ||
                    (sB.Y < sP.Y && sA.Y >= sP.Y))
                {
                    var x =
                        sA.X +
                        ((sP.Y - sA.Y) /
                        (sB.Y - sA.Y) *
                        (sB.X - sA.X));

                    if (x < sP.X)
                    {
                        return true;
                    }
                }

                return false;
            }
        }

        #endregion

        // Raycast?

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
    }
}
