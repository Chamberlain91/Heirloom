using System;
using System.Runtime.CompilerServices;

namespace Heirloom.Geometry
{
    /// <summary>
    /// Utility function for computation with quadratic and cubic curves.
    /// </summary>
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
            // c*t^2 + 2*b*(1-t)*t + a*(1-t)^2 
            var r = 1 - t;
            return (c * (t * t)) + (2 * b * r * t) + (a * r * r);
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
            // 2*((c-2*b+a)*t+b-a)
            return 2 * (((c - (2 * b) + a) * t) + b - a);
        }

        /// <summary>
        /// Computes the approximate arc length of the quadratic curve using line segments.
        /// </summary>
        /// <param name="a">The curve starting point.</param>
        /// <param name="b">The curve middle (handle).</param>
        /// <param name="c">The curve ending point.</param>
        /// <param name="resolution">The number of segments to approximate the curve with.</param>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static float ApproximateLength(Vector a, Vector b, Vector c, int resolution = 4)
        {
            return ApproximateLength(t => Interpolate(a, b, c, t), resolution);
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
            // d*t^3+3*c*(1-t)*t^2+3*b*(1-t)^2*t+a*(1-t)^3
            return (d * t * t * t) + (3 * c * (1 - t) * t * t) + (3 * b * (1 - t) * (1 - t) * t) + (a * (1 - t) * (1 - t) * (1 - t));
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
            // 3*((d-3*c+3*b-a)*t^2+(2*c-4*b+2*a)*t+b-a) 
            return 3 * (((d - (3 * c) + (3 * b) - a) * t * t) + (((2 * c) - (4 * b) + (2 * a)) * t) + b - a);
        }

        /// <summary>
        /// Computes the approximate arc length of the cubic curve using line segments.
        /// </summary>
        /// <param name="a">The curve's starting point.</param>
        /// <param name="b">The curve's first handle.</param>
        /// <param name="c">The curve's second handle.</param>
        /// <param name="d">The curve's ending point.</param>
        /// <param name="resolution">The number of segments to approximate the curve with.</param>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static float ApproximateLength(Vector a, Vector b, Vector c, Vector d, int resolution = 5)
        {
            return ApproximateLength(t => Interpolate(a, b, c, d, t), resolution);
        }

        #endregion

        /// <summary>
        /// Helper function to compute the approximate length of a curve using discrete segments.
        /// </summary>
        /// <param name="getPoint">A function to get a interpolated point.</param>
        /// <param name="resolution">The number of discretes steps along the curve.</param>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static float ApproximateLength(Func<float, Vector> getPoint, int resolution = 5)
        {
            var length = 0F;
            var prev = getPoint(0);

            var rate = 1F / resolution;
            var t = 0F;

            for (var i = 0; i <= resolution; i++)
            {
                var curr = getPoint(t);
                length += Vector.Distance(prev, curr);
                prev = curr;
                t += rate;
            }

            return length;
        }
    }
}
