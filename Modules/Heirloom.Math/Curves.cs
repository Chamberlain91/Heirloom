using System.Runtime.CompilerServices;

namespace Heirloom.Math
{
    public static class Curves
    {
        #region Quadratic

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Vector Quadratic(in Vector a, in Vector b, in Vector c, in float t)
        {
            return ((1 - t) * (1 - t) * a) + (2 * (1 - t) * t * b) + (t * t * c);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Vector QuadraticDerivative(in Vector a, in Vector b, in Vector c, in float t)
        {
            return 2 * ((-a * (1 - t)) - (2 * b * t) + b + (c * t));
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static float QuadraticApproximateLength(in Vector a, in Vector b, in Vector c)
        {
            var p = Quadratic(in a, in b, in c, 0.5F);
            return (p - a).Length + (c - p).Length;
        }

        #endregion
    }
}
