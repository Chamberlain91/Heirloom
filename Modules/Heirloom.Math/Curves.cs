using System.Runtime.CompilerServices;

using static Heirloom.Math.Calc;

namespace Heirloom.Math
{
    public static class Curves
    {
        #region Quadratic

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Vector Quadratic(in Vector a, in Vector b, in Vector c, in float t)
        {
            var r = 1 - t;
            return (r * r * a) + (2 * r * t * b) + (t * t * c);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Vector QuadraticDerivative(in Vector a, in Vector b, in Vector c, in float t)
        {
            return 2 * ((-a * (1 - t)) - (2 * b * t) + b + (c * t));
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static float QuadraticApproximateLength(in Vector a, in Vector b, in Vector c)
        {
            var p = QuadraticDerivative(in a, in b, in c, 0.5F);

            return Vector.Distance(a, p) +
                   Vector.Distance(p, c);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static float QuadraticDerivativeApproximateLength(in Vector a, in Vector b, in Vector c)
        {
            var p0 = QuadraticDerivative(in a, in b, in c, 0.0F);
            var p1 = QuadraticDerivative(in a, in b, in c, 0.33F);
            var p2 = QuadraticDerivative(in a, in b, in c, 0.66F);
            var p3 = QuadraticDerivative(in a, in b, in c, 1.00F);

            return Vector.Distance(p0, p1) +
                   Vector.Distance(p1, p2) +
                   Vector.Distance(p2, p3);
        }

        #endregion

        #region Cubic

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Vector Cubic(in Vector a, in Vector b, in Vector c, in Vector d, in float t)
        {
            var r = 1 - t;
            return (Pow(r, 3) * a) + (3 * Pow(r, 2) * t * b) + (3 * r * Pow(t, 2) * c) + (Pow(t, 3) * d);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Vector CubicDerivative(in Vector a, in Vector b, in Vector c, in Vector d, in float t)
        {
            var r = 1 - t;
            var r2 = Pow(r, 2);
            var t2 = Pow(t, 2);

            return (-3 * a * r2) + (3 * b * r2) - (6 * b * t * r) - (3 * c * t2) + (6 * c * t * r) + (3 * d * t2);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static float CubicApproximateLength(in Vector a, in Vector b, in Vector c, in Vector d)
        {
            var p = Cubic(in a, in b, in c, in d, 0.25F);
            var q = Cubic(in a, in b, in c, in d, 0.75F);

            return Vector.Distance(a, p) + Vector.Distance(p, q) + Vector.Distance(q, d);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static float CubicDerivativeApproximateLength(in Vector a, in Vector b, in Vector c, in Vector d)
        {
            var p0 = CubicDerivative(in a, in b, in c, in d, 0.00F);
            var p1 = CubicDerivative(in a, in b, in c, in d, 0.25F);
            var p2 = CubicDerivative(in a, in b, in c, in d, 0.50F);
            var p3 = CubicDerivative(in a, in b, in c, in d, 0.75F);
            var p4 = CubicDerivative(in a, in b, in c, in d, 1.00F);

            return Vector.Distance(p0, p1) +
                   Vector.Distance(p1, p2) +
                   Vector.Distance(p2, p3) +
                   Vector.Distance(p3, p4);
        }

        #endregion
    }
}
