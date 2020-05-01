using System.Collections.Generic;
using System.Runtime.CompilerServices;

using static Heirloom.Calc;

namespace Heirloom.Geometry
{
    public sealed class CurveTools
    {
        #region Quadratic

        /// <summary>
        /// Computes the interpolated point on a quadratic curve defined by <paramref name="a"/>, <paramref name="b"/>, <paramref name="c"/>.
        /// </summary>
        /// <param name="a">The curve starting point.</param>
        /// <param name="b">The curve middle (handle).</param>
        /// <param name="c">The curve ending point.</param>
        /// <param name="t">The interpolation factor along the curve.</param>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Vector Interpolate(in Vector a, in Vector b, in Vector c, in float t)
        {
            var r = 1 - t;
            return (r * r * a) + (2 * r * t * b) + (t * t * c);
        }

        /// <summary>
        /// Computes the interpolated point on the derivative of a quadratic curve defined by <paramref name="a"/>, <paramref name="b"/>, <paramref name="c"/>.
        /// </summary>
        /// <param name="a">The curve starting point.</param>
        /// <param name="b">The curve middle (handle).</param>
        /// <param name="c">The curve ending point.</param>
        /// <param name="t">The interpolation factor along the curve.</param>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Vector InterpolateDerivative(in Vector a, in Vector b, in Vector c, in float t)
        {
            return 2 * ((-a * (1 - t)) - (2 * b * t) + b + (c * t));
        }

        /// <summary>
        /// Computes the approximate arc length of the quadratic curve using line segments.
        /// </summary>
        /// <param name="a">The curve starting point.</param>
        /// <param name="b">The curve middle (handle).</param>
        /// <param name="c">The curve ending point.</param>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static float ApproximateLength(in Vector a, in Vector b, in Vector c)
        {
            var p = InterpolateDerivative(in a, in b, in c, 0.5F);

            return Vector.Distance(a, p) +
                   Vector.Distance(p, c);
        }

        /// <summary>
        /// Computes the approximate arc length of the derivative of a quadratic curve using line segments.
        /// </summary>
        /// <param name="a">The curve starting point.</param>
        /// <param name="b">The curve middle (handle).</param>
        /// <param name="c">The curve ending point.</param>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static float DerivativeApproximateLength(in Vector a, in Vector b, in Vector c)
        {
            var p0 = InterpolateDerivative(in a, in b, in c, 0.0F);
            var p1 = InterpolateDerivative(in a, in b, in c, 0.33F);
            var p2 = InterpolateDerivative(in a, in b, in c, 0.66F);
            var p3 = InterpolateDerivative(in a, in b, in c, 1.00F);

            return Vector.Distance(p0, p1) +
                   Vector.Distance(p1, p2) +
                   Vector.Distance(p2, p3);
        }

        #endregion

        #region Cubic

        /// <summary>
        /// Computes the interpolated point on a cubic curve defined by <paramref name="a"/>, <paramref name="b"/>, <paramref name="c"/>, <paramref name="d"/>.
        /// </summary>
        /// <param name="a">The curve's starting point.</param>
        /// <param name="b">The curve's first handle.</param>
        /// <param name="c">The curve's second handle.</param>
        /// <param name="d">The curve's ending point.</param>
        /// <param name="t">The interpolation factor along the curve.</param>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Vector Interpolate(in Vector a, in Vector b, in Vector c, in Vector d, in float t)
        {
            var r = 1 - t;
            return (Pow(r, 3) * a) + (3 * Pow(r, 2) * t * b) + (3 * r * Pow(t, 2) * c) + (Pow(t, 3) * d);
        }

        /// <summary>
        /// Computes the interpolated point on the derivative of a cubic curve defined by <paramref name="a"/>, <paramref name="b"/>, <paramref name="c"/>, <paramref name="d"/>.
        /// </summary>
        /// <param name="a">The curve's starting point.</param>
        /// <param name="b">The curve's first handle.</param>
        /// <param name="c">The curve's second handle.</param>
        /// <param name="d">The curve's ending point.</param>
        /// <param name="t">The interpolation factor along the curve.</param>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Vector InterpolateDerivative(in Vector a, in Vector b, in Vector c, in Vector d, in float t)
        {
            var r = 1 - t;
            var r2 = Pow(r, 2);
            var t2 = Pow(t, 2);

            return (-3 * a * r2) + (3 * b * r2) - (6 * b * t * r) - (3 * c * t2) + (6 * c * t * r) + (3 * d * t2);
        }

        /// <summary>
        /// Computes the approximate arc length of the cubic curve using line segments.
        /// </summary>
        /// <param name="a">The curve's starting point.</param>
        /// <param name="b">The curve's first handle.</param>
        /// <param name="c">The curve's second handle.</param>
        /// <param name="d">The curve's ending point.</param>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static float ApproximateLength(in Vector a, in Vector b, in Vector c, in Vector d)
        {
            var p = Interpolate(in a, in b, in c, in d, 0.25F);
            var q = Interpolate(in a, in b, in c, in d, 0.75F);

            return Vector.Distance(a, p) + Vector.Distance(p, q) + Vector.Distance(q, d);
        }

        /// <summary>
        /// Computes the approximate arc length of the cubic curve using line segments.
        /// </summary>
        /// <param name="a">The curve's starting point.</param>
        /// <param name="b">The curve's first handle.</param>
        /// <param name="c">The curve's second handle.</param>
        /// <param name="d">The curve's ending point.</param>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static float DerivativeApproximateLength(in Vector a, in Vector b, in Vector c, in Vector d)
        {
            var p0 = InterpolateDerivative(in a, in b, in c, in d, 0.00F);
            var p1 = InterpolateDerivative(in a, in b, in c, in d, 0.25F);
            var p2 = InterpolateDerivative(in a, in b, in c, in d, 0.50F);
            var p3 = InterpolateDerivative(in a, in b, in c, in d, 0.75F);
            var p4 = InterpolateDerivative(in a, in b, in c, in d, 1.00F);

            return Vector.Distance(p0, p1) +
                   Vector.Distance(p1, p2) +
                   Vector.Distance(p2, p3) +
                   Vector.Distance(p3, p4);
        }

        #endregion
    }
}
