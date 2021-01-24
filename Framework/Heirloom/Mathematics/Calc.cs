using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;

namespace Heirloom.Mathematics
{
    /// <summary>
    /// Provides various math operations for <see cref="float"/> and <see cref="int"/> types including
    /// a few genric utility functions such as <see cref="Swap{T}(ref T, ref T)"/> or <see cref="Lerp(float, float, float)"/>.
    /// </summary>
    /// <category>Mathematics</category>
    public static class Calc
    {
        #region Constants

        /// <summary>
        /// <para> An approximation of the constant Pi (180 Degrees or Pi Radians). </para>
        /// <para> 3.14159265359... </para>
        /// </summary>
        public const float Pi = 3.14159265359F;

        /// <summary>
        /// <para> Two times Pi. 360 Degrees in Radians. </para>
        /// <para> 6.28318530718... </para>
        /// </summary>
        public const float TwoPi = 6.28318530718F;

        /// <summary>
        /// <para> Half Pi. 90 Degrees in Radians. </para>
        /// <para> 0.5 * 3.141592653... </para>
        /// </summary>
        public const float HalfPi = Pi * 0.5F;

        /// <summary>
        /// Approximately the value <c>Sqrt(2)</c>.
        /// </summary>
        public const float Sqrt2 = 1.41421F;

        /// <summary>
        /// Pi / 180.0
        /// </summary>
        public const float ToRadians = Pi / 180f;

        /// <summary>
        /// 180.0 / Pi
        /// </summary>
        public const float ToDegree = 180f / Pi;

        /// <summary>
        /// A small number almost considered zero, greatly differs from <see cref="float.Epsilon"/>.
        /// </summary>
        public const float Epsilon = 10e-10F;

        /// <summary>
        /// A static instance of the <see cref="Random"/> for convenience.
        /// </summary>
        public static Random Random { get; private set; } = new Random();

        /// <summary>
        /// A static instance of the <see cref="PerlinNoise"/> for convenience.
        /// </summary>
        public static PerlinNoise Perlin { get; private set; } = new PerlinNoise(Random.Next());

        /// <summary>
        /// A static instance of the <see cref="SimplexNoise"/> for convenience.
        /// </summary>
        public static SimplexNoise Simplex { get; private set; } = new SimplexNoise(Random.Next());

        #endregion

        /// <summary>
        /// Initializes <see cref="Random"/>, <see cref="Simplex"/> and <see cref="Perlin"/> with the specified seed.
        /// </summary>
        /// <param name="seed"></param>
        public static void SetRandomSeed(int seed)
        {
            Random = new Random(seed);
            Perlin = new PerlinNoise(seed);
            Simplex = new SimplexNoise(seed);
        }

        #region Distance

        /// <summary>
        /// Computes the distance squared between a pair of two-dimensional points.
        /// </summary>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static float DistanceSquared(float x1, float y1, float x2, float y2)
        {
            var dx = x1 - x2;
            var dy = y1 - y2;
            return (dx * dx) + (dy * dy);
        }

        /// <summary>
        /// Computes the distance between a pair of two-dimensional points.
        /// </summary>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static float Distance(float x1, float y1, float x2, float y2)
        {
            return Sqrt(DistanceSquared(x1, y1, x2, y2));
        }

        /// <summary>
        /// Computes the manhattan-distance between a pair of two-dimensional points.
        /// </summary>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static float ManhattanDistance(float x1, float y1, float x2, float y2)
        {
            var dx = Abs(x1 - x2);
            var dy = Abs(y1 - y2);
            return dx + dy;
        }

        /// <summary>
        /// Computes an approximation of euclidean distancing using
        /// </summary>
        /// <param name="x1"></param>
        /// <param name="y1"></param>
        /// <param name="x2"></param>
        /// <param name="y2"></param>
        /// <returns></returns>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static float ApproximateDistance(float x1, float y1, float x2, float y2)
        // https://www.flipcode.com/archives/Fast_Approximate_Distance_Functions.shtml
        {
            const float MaxCoefficient = 1007 / 1024F;
            const float MinCoefficient = 441 / 1024F;

            var dx = Abs(x2 - x1);
            var dy = Abs(y2 - y1);

            if (dx < dy) { return (MaxCoefficient * dy) + (MinCoefficient * dx); }
            else { return (MaxCoefficient * dx) + (MinCoefficient * dy); }
        }

        #endregion

        #region Factorial

        /// <summary>
        /// Computes the factorial of <paramref name="x"/>.
        /// </summary>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static int Factorial(int x)
        {
            return (int) Factorial((uint) x);
        }

        /// <summary>
        /// Computes the factorial of <paramref name="x"/>.
        /// </summary>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static uint Factorial(uint x)
        {
            uint r = 1;
            for (uint i = 1; i < x; i++)
            {
                r *= i;
            }

            return r;
        }

        #endregion

        #region Clamp

        /// <summary>
        /// Returns the value <paramref name="x"/> clamped to the specified range.
        /// </summary>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static int Clamp(int x, int min, int max)
        {
            return Min(max, Max(min, x));
        }

        /// <summary>
        /// Returns the value <paramref name="x"/> clamped to the specified range.
        /// </summary>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static float Clamp(float x, float min, float max)
        {
            return Min(max, Max(min, x));
        }

        #endregion

        #region Min

        /// <summary>
        /// Returns the smaller of two values.
        /// </summary>
        /// <param name="x">The first value.</param>
        /// <param name="y">The second value.</param>
        /// <returns>The minimum value.</returns>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static int Min(int x, int y)
        {
            return x < y ? x : y;
        }

        /// <summary>
        /// Returns the smaller of two values.
        /// </summary>
        /// <param name="x">The first value.</param>
        /// <param name="y">The second value.</param>
        /// <returns>The minimum value.</returns>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static float Min(float x, float y)
        {
            return x < y ? x : y;
        }

        /// <summary>
        /// Returns the minimum value in the given set of numbers.
        /// </summary>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static int Min(params int[] vals)
        {
            if (vals.Length == 0) { throw new ArgumentException("Must provide at least one value."); }

            var m = vals[0];
            for (var i = 1; i < vals.Length; i++)
            {
                m = Min(m, vals[i]);
            }

            return m;
        }

        /// <summary>
        /// Returns the minimum value in the given set of numbers.
        /// </summary>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static float Min(params float[] vals)
        {
            if (vals.Length == 0) { throw new ArgumentException("Must provide at least one value."); }

            var m = vals[0];
            for (var i = 1; i < vals.Length; i++)
            {
                m = Min(m, vals[i]);
            }

            return m;
        }

        /// <summary>
        /// Finds the comparably minimum value from the set of values.
        /// </summary>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static T Min<T>(params T[] vals) where T : IComparable
        {
            return vals.Min();
        }

        #endregion

        #region Max

        /// <summary>
        /// Returns the larger of two values.
        /// </summary>
        /// <param name="x">The first value.</param>
        /// <param name="y">The second value.</param>
        /// <returns>The maximum value.</returns>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static int Max(int x, int y)
        {
            return x > y ? x : y;
        }

        /// <summary>
        /// Returns the larger of two values.
        /// </summary>
        /// <param name="x">The first value.</param>
        /// <param name="y">The second value.</param>
        /// <returns>The maximum value.</returns>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static float Max(float x, float y)
        {
            return x > y ? x : y;
        }

        /// <summary>
        /// Returns the maximum value in the given set of numbers.
        /// </summary>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static int Max(params int[] vals)
        {
            if (vals.Length == 0) { throw new ArgumentException("Must provide at least one value."); }

            var m = vals[0];
            for (var i = 1; i < vals.Length; i++)
            {
                m = Max(m, vals[i]);
            }

            return m;
        }

        /// <summary>
        /// Returns the maximum value in the given set of numbers.
        /// </summary>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static float Max(params float[] vals)
        {
            if (vals.Length == 0) { throw new ArgumentException("Must provide at least one value."); }

            var m = vals[0];
            for (var i = 1; i < vals.Length; i++)
            {
                m = Max(m, vals[i]);
            }

            return m;
        }

        /// <summary>
        /// Finds the comparably maximum value from the set of value.
        /// </summary>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static T Max<T>(params T[] vals) where T : IComparable
        {
            return vals.Max();
        }

        #endregion

        #region Abs

        /// <summary>
        /// Returns the absolute value of some number.
        /// </summary>
        /// <param name="x">Some value.</param>
        /// <returns>The absolute value.</returns>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static int Abs(int x)
        {
            var mask = x >> 31;
            return (x + mask) ^ mask;
        }

        /// <summary>
        /// Returns the absolute value of some number.
        /// </summary>
        /// <param name="x">Some value.</param>
        /// <returns>The absolute value.</returns>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static float Abs(float x)
        {
            return MathF.Abs(x);
        }

        #endregion

        #region Sign

        /// <summary>
        /// Returns an integer that represents the sign of the specified number.
        /// </summary>
        /// <param name="x">Some number.</param>
        /// <returns>Will return -1 if negative, 0 if zero and +1 if positive.</returns>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static int Sign(int x)
        {
            return Math.Sign(x);
        }

        /// <summary>
        /// Returns an integer that represents the sign of the specified number.
        /// </summary>
        /// <param name="x">Some number.</param>
        /// <returns>Will return -1 if negative, 0 if zero and +1 if positive.</returns>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static int Sign(float x)
        {
            return Math.Sign(x);
        }

        #endregion

        #region Sqrt

        /// <summary>
        /// Returns the square root value of some number.
        /// </summary>
        /// <param name="x">Some value.</param>
        /// <returns>The square root value.</returns>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static float Sqrt(float x)
        {
            return MathF.Sqrt(x);
        }

        #endregion

        #region Sin, Cos, Tan and Inversions

        /// <summary>
        /// The function <see cref="Sin(float)"/> mapped to [0.0, 1.0].
        /// </summary>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static float Osc(float x)
        {
            return (1F + Sin(x)) / 2F;
        }

        /// <summary>
        /// Return the sine of the specified angle.
        /// </summary>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static float Sin(float x)
        {
            return MathF.Sin(x);
        }

        /// <summary>
        /// Return the arcsine of the specified angle.
        /// </summary>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static float Asin(float x)
        {
            return MathF.Asin(x);
        }

        /// <summary>
        /// Return the cosine of the specified angle.
        /// </summary>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static float Cos(float x)
        {
            return MathF.Cos(x);
        }

        /// <summary>
        /// Return the arccosine of the specified angle.
        /// </summary>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static float Acos(float x)
        {
            return MathF.Acos(x);
        }

        /// <summary>
        /// Return the tangent of the specified angle.
        /// </summary>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static float Tan(float x)
        {
            return MathF.Tan(x);
        }

        /// <summary>
        /// Return the arctangent of the specified angle.
        /// </summary>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static float Atan(float x)
        {
            return MathF.Atan(x);
        }

        /// <summary>
        /// Computes the angle whose tangent is quotient to <paramref name="x"/> and <paramref name="y"/>.
        /// </summary>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static float Atan2(float y, float x)
        {
            return MathF.Atan2(y, x);
        }

        #endregion

        #region Pow & Log

        /// <summary>
        /// Computes <paramref name="x"/> raised to the power of <paramref name="y"/>.
        /// </summary>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static float Pow(float x, float y)
        {
            return MathF.Pow(x, y);
        }

        /// <summary>
        /// Computes the natural logarithm of <paramref name="x"/>.
        /// </summary>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static float Log(float x)
        {
            return MathF.Log(x);
        }

        /// <summary>
        /// Computes the logarithm of <paramref name="x"/> with base <paramref name="b"/>.
        /// </summary>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static float Log(float x, float b)
        {
            return MathF.Log(x, b);
        }

        #endregion

        #region Fraction, Rounding & Truncation

        /// <summary>
        /// Compute the fractional (decimal) portion of the number <paramref name="x"/>.
        /// </summary>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static float Fraction(float x)
        {
            return x - Floor(x);
        }

        /// <summary>
        /// Computes the floor integer (rounding down) of the value <paramref name="x"/>.
        /// </summary>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static int Floor(float x)
        {
            return (int) MathF.Floor(x);
        }

        /// <summary>
        /// Computes the ceiling integer (rounding up) of the value <paramref name="x"/>.
        /// </summary>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static int Ceil(float x)
        {
            return (int) MathF.Ceiling(x);
        }

        /// <summary>
        /// Computes the nearest integer of the value <paramref name="x"/>.
        /// </summary>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static int Round(float x)
        {
            return (int) MathF.Round(x);
        }

        /// <summary>
        /// Determines if the two values are nearly equal comparing equality within a <see cref="Epsilon"/> threshold.
        /// </summary> 
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static bool NearEquals(float x1, float x2)
        {
            return MathF.Abs(x1 - x2) < Epsilon;
        }

        /// <summary>
        /// Determines if the two values are nearly equal comparing equality within a threshold.
        /// </summary> 
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static bool NearEquals(float x1, float x2, float threshold)
        {
            return MathF.Abs(x1 - x2) < threshold;
        }

        /// <summary>
        /// Determines if the value is nearly equal to zero by comparing equality within a <see cref="Epsilon"/> threshold.
        /// </summary> 
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static bool NearZero(float v)
        {
            return v > -Epsilon && v < Epsilon;
        }

        #endregion

        #region Interpolation

        /// <summary>
        /// Computes the linear interpolation from <paramref name="x1"/> to <paramref name="x2"/> by factor <paramref name="t"/>.
        /// </summary>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static float Lerp(float x1, float x2, float t)
        {
            return x1 + ((x2 - x1) * t);
        }

        /// <summary>
        /// Computes the linear interpolation of two angles across the shorter distance.
        /// </summary>
        /// <param name="x1">Start angle.</param>
        /// <param name="x2">End angle.</param>
        /// <param name="t">Interpolation factor</param>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static float LerpAngle(float x1, float x2, float t)
        {
            var d1 = Abs(x1 - x2);
            var d2 = Abs(x1 - (x2 + TwoPi));
            var d3 = Abs(x1 - (x2 - TwoPi));

            if (d2 < d1)
            {
                x1 = Lerp(x1, x2 + TwoPi, t);
            }
            else if (d3 < d1)
            {
                x1 = Lerp(x1, x2 - TwoPi, t);
            }
            else
            {
                x1 = Lerp(x1, x2, t);
            }

            return x1 % TwoPi;
        }

        /// <summary>
        /// Computes a non-linear interpolation of two values adjusted for frame-rate.
        /// </summary>
        /// <param name="x1">The starting value.</param>
        /// <param name="x2">The target value.</param>
        /// <param name="t">The interpolation factor (percentage).</param>
        /// <param name="delta">The time in seconds since last frame.</param>
        /// <param name="duration">The length of time (in seconds) that the values are interpolated <paramref name="t"/> percent. </param>
        /// <returns></returns>
        public static float LerpTime(float x1, float x2, float t, float duration, float delta)
        {
            return Lerp(x1, x2, 1F - Pow(1F - t, delta / duration));
        }

        #endregion

        #region Between

        /// <summary>
        /// Computes the interpolation factor (0.0 to 1.0) of <paramref name="x"/> between <paramref name="min"/> and <paramref name="max"/>.
        /// </summary>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static float Between(float x, float min, float max)
        {
            return (x - min) / (max - min);
        }

        /// <summary>
        /// Computes the interpolation factor (0.0 to 1.0) of <paramref name="x"/> between <paramref name="min"/> and <paramref name="max"/>.
        /// </summary>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static bool IsBetween(float x, float min, float max)
        {
            return x >= min && x <= max;
        }

        #endregion

        #region Rescale

        /// <summary>
        /// Rescales a value with domain <paramref name="min1"/> to <paramref name="max1"/> to a new domain <paramref name="min2"/> to <paramref name="max2"/>.
        /// </summary>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static float Rescale(float x, float min1, float max1, float min2, float max2)
        {
            return Lerp(min2, max2, Between(x, min1, max1));
        }

        /// <summary>
        /// Rescales a value from the source domain a target domain.
        /// </summary>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static float Rescale(float x, Range src, Range dst)
        {
            return Rescale(x, src.Min, src.Max, dst.Min, dst.Max);
        }

        #endregion

        #region Smooth Step

        /// <summary>
        /// Computes the smooth-step of <paramref name="x"/> between <paramref name="min"/> and <paramref name="max"/>.
        /// </summary>
        /// <param name="min">The lower edge.</param>
        /// <param name="max">The upper edge.</param>
        /// <param name="x">Some number.</param>
        /// <returns>The smoothstep of <paramref name="x"/>.</returns>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static float SmoothStep(float x, float min, float max)
        {
            return SmoothStep(Between(x, min, max));
        }

        /// <summary>
        /// Computes smoothstep of a number. (Assumes <paramref name="x"/> is in the range 0.0 to 1.0).
        /// </summary>
        /// <param name="x">Some number.</param>
        /// <returns>The smoothstep of <paramref name="x"/>.</returns>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static float SmoothStep(float x)
        {
            x = Clamp(x, 0.0F, 1.0F);
            return x * x * (3 - 2 * x);
        }

        /// <summary>
        /// Computes the inverse of <see cref="SmoothStep(float)"/>.
        /// </summary>
        /// <param name="x">Some number with <see cref="SmoothStep(float)"/> applied to it.</param>
        /// <returns>The number before <see cref="SmoothStep(float)"/> was applied.</returns>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static float InverseSmoothStep(float x)
        {
            return 0.5F - Sin(Asin(1F - 2F * x) / 3F);
        }

        #endregion

        #region Power of 2

        /// <summary>
        /// Computes the nearest power of 2 to a number. 
        /// This is done by computing both lower and upper power of 2, and then comparing the distance to x.
        /// </summary>
        /// <param name="x"> Some unsigned integer </param>
        /// <returns> The nearest power of 2 to x </returns>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static uint NearestPowerOfTwo(uint x)
        {
            var U = UpperPowerOfTwo(x);
            var L = LowerPowerOfTwo(x);
            if ((x - L) < (U - x))
            {
                return L;
            }
            else
            {
                return U;
            }
        }

        /// <summary>
        /// Computes the nearest power of 2 to a number. 
        /// This is done by computing both lower and upper power of 2, and then comparing the distance to x.
        /// </summary>
        /// <param name="x"> Some unsigned integer </param>
        /// <returns> The nearest power of 2 to x </returns>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static int NearestPowerOfTwo(int x)
        {
            var U = UpperPowerOfTwo(x);
            var L = LowerPowerOfTwo(x);
            if ((x - L) < (U - x))
            {
                return L;
            }
            else
            {
                return U;
            }
        }

        /// <summary>
        /// Computes the lower power of 2 nearest to x.
        /// </summary>
        /// <param name="x"> Some unsigned integer </param>
        /// <returns> The nearest lower power of 2 to x </returns>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static uint LowerPowerOfTwo(uint x)
        {
            x |= x >> 1;
            x |= x >> 2;
            x |= x >> 4;
            x |= x >> 8;
            x |= x >> 16;
            return x - (x >> 1);
        }

        /// <summary>
        /// Computes the lower power of 2 nearest to x.
        /// </summary>
        /// <param name="x"> Some unsigned integer </param>
        /// <returns> The nearest lower power of 2 to x </returns>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static int LowerPowerOfTwo(int x)
        {
            x |= x >> 1;
            x |= x >> 2;
            x |= x >> 4;
            x |= x >> 8;
            x |= x >> 16;
            return x - (x >> 1);
        }

        /// <summary>
        /// Computes the upper power of 2 nearest to x.
        /// </summary>
        /// <param name="x"> Some unsigned integer </param>
        /// <returns> The nearest upper power of 2 to x </returns>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static uint UpperPowerOfTwo(uint x)
        {
            x--;
            x |= x >> 1;
            x |= x >> 2;
            x |= x >> 4;
            x |= x >> 8;
            x |= x >> 16;
            x++;

            return x;
        }

        /// <summary>
        /// Computes the upper power of 2 nearest to x.
        /// </summary>
        /// <param name="x"> Some integer </param>
        /// <returns> The nearest upper power of 2 to x </returns>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static int UpperPowerOfTwo(int x)
        {
            x--;
            x |= x >> 1;
            x |= x >> 2;
            x |= x >> 4;
            x |= x >> 8;
            x |= x >> 16;
            x++;

            return x;
        }

        /// <summary>
        /// Determines if the given integer is a power of 2.
        /// </summary>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static bool IsPowerOfTwo(ulong x)
        {
            return (x & ~(x - 1)) == x;
        }

        /// <summary>
        /// Determines if the given integer is a power of 2.
        /// </summary>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static bool IsPowerOfTwo(uint x)
        {
            return (x & ~(x - 1)) == x;
        }

        /// <summary>
        /// Determines if the given integer is a power of 2.
        /// </summary>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static bool IsPowerOfTwo(int x)
        {
            return (x & ~(x - 1)) == x;
        }

        #endregion

        #region Swap / Order

        /// <summary>
        /// Swaps two references.
        /// </summary>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static void Swap<T>(ref T a, ref T b)
        {
            var t = a;
            a = b;
            b = t;
        }

        /// <summary>
        /// Swaps two positions within the given list.
        /// </summary>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static void Swap<T>(this IList<T> list, int a, int b)
        {
            var t = list[a];
            list[a] = list[b];
            list[b] = t;
        }

        /// <summary>
        /// Orders the two given references so they are in comparable order.
        /// </summary>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static void Order<T>(ref T a, ref T b) where T : IComparable
        {
            if (a.CompareTo(b) > 0)
            {
                Swap(ref a, ref b);
            }
        }

        #endregion

        #region Wrap

        /// <summary>
        /// Wraps (loops) a number within a zero to n range.
        /// </summary>
        /// <param name="x">Some value to wrap.</param>
        /// <param name="n">Some upper bound from zero.</param>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static int Wrap(int x, int n)
        {
            // todo: optimize if(x > 0) could do less modulus
            if (n <= 0) { throw new ArgumentException($"Argument {nameof(n)} must be greater than zero."); }
            return ((x % n) + n) % n;
        }

        /// <summary>
        /// Wraps (loops) a number within a range.
        /// </summary>
        /// <param name="x">Some value to wrap.</param>
        /// <param name="min">Some lower bound.</param>
        /// <param name="max">Some upper bound.</param>
        /// <returns>The resulting number contrained to the range in a loop.</returns>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static int Wrap(int x, int min, int max)
        {
            return min + Wrap(x - min, max - min);
        }

        /// <summary>
        /// Wraps (loops) a number within a range.
        /// </summary>
        /// <param name="x">Some value to wrap.</param>
        /// <param name="range">Some range.</param>
        /// <returns>The resulting number contrained to the range in a loop.</returns>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static int Wrap(int x, IntRange range)
        {
            return Wrap(x, range.Max, range.Min);
        }

        /// <summary>
        /// Wraps (loops) a number within a zero to n range.
        /// </summary>
        /// <param name="x">Some value to wrap.</param>
        /// <param name="n">Some upper bound from zero.</param>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static float Wrap(float x, float n)
        {
            if (n <= 0) { throw new ArgumentException($"Argument {nameof(n)} must be greater than zero."); }
            return ((x % n) + n) % n;
        }

        /// <summary>
        /// Wraps (loops) a number within a range.
        /// </summary>
        /// <param name="x">Some value to wrap.</param>
        /// <param name="min">Some lower bound.</param>
        /// <param name="max">Some upper bound.</param>
        /// <returns>The resulting number contrained to the range in a loop.</returns>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static float Wrap(float x, float min, float max)
        {
            return min + Wrap(x - min, max - min);
        }

        /// <summary>
        /// Wraps (loops) a number within a range.
        /// </summary>
        /// <param name="x">Some value to wrap.</param>
        /// <param name="range">Some range.</param>
        /// <returns>The resulting number contrained to the range in a loop.</returns>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static float Wrap(float x, Range range)
        {
            return Wrap(x, range.Max, range.Min);
        }

        #endregion
    }
}
