using System;
using System.Collections.Generic;
using System.Runtime.CompilerServices;

using M = System.Math;

namespace Heirloom.Math
{
    /// <summary>
    /// Math operations for <see cref="float"/> and a few other data types including <see cref="int"/>. <para/>
    /// Includes a few genric utility functions such as <see cref="Swap{T}(ref T, ref T)"/>
    /// </summary>
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
        public static readonly Random Random = new Random();

        /// <summary>
        /// A static instance of the <see cref="PerlinNoise"/> for convenience.
        /// </summary>
        public static readonly PerlinNoise Perlin = new PerlinNoise(Random.Next());

        /// <summary>
        /// A static instance of the <see cref="SimplexNoise"/> for convenience.
        /// </summary>
        public static readonly SimplexNoise Simplex = new SimplexNoise(Random.Next());

        #endregion

        #region Trig Functions

        /// <summary>
        /// The function sine mapped to [0.0, 1.0].
        /// </summary>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static float Osc(float x) { return (float) (1F + M.Sin(x)) / 2F; }

        /// <summary>
        /// The function sine.
        /// </summary>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static float Sin(float x) { return (float) M.Sin(x); }

        /// <summary>
        /// Ihe inverse of the function sine.
        /// </summary>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static float Asin(float x) { return (float) M.Asin(x); }

        /// <summary>
        /// The function cosine.
        /// </summary>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static float Cos(float x) { return (float) M.Cos(x); }

        /// <summary>
        /// The inverse of the function cosine.
        /// </summary>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static float Acos(float x) { return (float) M.Acos(x); }

        /// <summary>
        /// The tangent function.
        /// </summary>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static float Tan(float x) { return (float) M.Tan(x); }

        /// <summary>
        /// The inverse of the tangent function.
        /// </summary>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static float Atan(float x) { return (float) M.Atan(x); }

        /// <summary>
        /// Computes the angle whose tangent is quotient to <paramref name="x"/> and <paramref name="y"/>.
        /// </summary>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static float Atan2(float y, float x) { return (float) M.Atan2(y, x); }

        /// <summary>
        /// Computes the distance between a pair of one-dimensional points.
        /// </summary>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static float Distance(float x1, float x2) { return Abs(x1 - x2); }

        /// <summary>
        /// Computes the distance between a pair of two-dimensional points.
        /// </summary>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static float Distance(float x1, float y1, float x2, float y2)
        {
            var dx = x1 - x2;
            var dy = y1 - y2;
            return Sqrt((dx * dx) + (dy * dy));
        }

        #endregion

        #region Exponential Functions

        /// <summary>
        /// Computes <paramref name="x"/> raised to the power of <paramref name="y"/>.
        /// </summary>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static float Pow(float x, float y) { return (float) M.Pow(x, y); }

        /// <summary>
        /// Computes <paramref name="x"/> raised to the power of <paramref name="y"/>.
        /// </summary>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static double Pow(double x, double y) { return M.Pow(x, y); }

        /// <summary>
        /// Computes the natural logarithm of <paramref name="x"/>.
        /// </summary>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static float Log(float x) { return (float) M.Log(x); }

        /// <summary>
        /// Computes the natural logarithm of <paramref name="x"/>.
        /// </summary>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static double Log(double x) { return M.Log(x); }

        /// <summary>
        /// Computes the logarithm of <paramref name="x"/> with base <paramref name="b"/>.
        /// </summary>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static float Log(float x, float b) { return (float) M.Log(x, b); }

        /// <summary>
        /// Computes the logarithm of <paramref name="x"/> with base <paramref name="b"/>.
        /// </summary>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static double Log(double x, double b) { return M.Log(x, b); }

        /// <summary>
        /// Computes the square root of <paramref name="x"/>.
        /// </summary>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static float Sqrt(float x) { return (float) M.Sqrt(x); }

        /// <summary>
        /// Computes the square root of <paramref name="x"/>.
        /// </summary>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static double Sqrt(double x) { return M.Sqrt(x); }

        /// <summary>
        /// Computes the factorial of <paramref name="x"/>.
        /// </summary>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static int Factorial(int x) { return (int) Factorial((uint) x); }

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

        #region Min Max Clamp Abs Sign ( double )

        /// <summary>
        /// Returns the minimum value between <paramref name="x"/> and <paramref name="y"/>.
        /// </summary>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static double Min(double x, double y) { return x < y ? x : y; }

        /// <summary>
        /// Returns the maximum value between <paramref name="x"/> and <paramref name="y"/>.
        /// </summary>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static double Max(double x, double y) { return x > y ? x : y; }

        /// <summary>
        /// Returns the value <paramref name="x"/> clamped to the specified range.
        /// </summary>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static double Clamp(double x, double min, double max) { return Min(max, Max(min, x)); }

        /// <summary>
        /// Computes the absolute value of <paramref name="x"/>.
        /// </summary>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static double Abs(double x) { return x < 0 ? -x : x; }

        public static object Sqrt(object p)
        {
            throw new NotImplementedException();
        }

        /// <summary>
        /// Returns the sign of <paramref name="x"/> as if compared against zero (-1, 0 or +1)
        /// </summary>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static int Sign(double x) { return M.Sign(x); }

        /// <summary>
        /// Compute the fractional (decimal) portion of the number <paramref name="x"/>.
        /// </summary>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static double Fraction(double x)
        {
            return x - Floor(x);
        }

        #endregion

        #region Min Max Clamp Sign Abs ( float )

        /// <summary>
        /// Returns the minimum value between <paramref name="x"/> and <paramref name="y"/>.
        /// </summary>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static float Min(float x, float y) { return x < y ? x : y; }

        /// <summary>
        /// Returns the maximum value between <paramref name="x"/> and <paramref name="y"/>.
        /// </summary>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static float Max(float x, float y) { return x > y ? x : y; }

        /// <summary>
        /// Returns the value <paramref name="x"/> clamped to the specified range.
        /// </summary>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static float Clamp(float x, float min, float max) { return Min(max, Max(min, x)); }

        /// <summary>
        /// Computes the absolute value of <paramref name="x"/>.
        /// </summary>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static float Abs(float x) { return x < 0 ? -x : x; }

        /// <summary>
        /// Returns the sign of <paramref name="x"/> as if compared against zero (-1, 0 or +1)
        /// </summary>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static int Sign(float x) { return M.Sign(x); }

        /// <summary>
        /// Compute the fractional (decimal) portion of the number <paramref name="x"/>.
        /// </summary>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static float Fraction(float x)
        {
            return x - (int) x;
        }

        #endregion

        #region Min Max Clamp Abs Sign ( int )

        /// <summary>
        /// Returns the minimum value between <paramref name="x"/> and <paramref name="y"/>.
        /// </summary>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static int Min(int x, int y) { return x < y ? x : y; }

        /// <summary>
        /// Returns the maximum value between <paramref name="x"/> and <paramref name="y"/>.
        /// </summary>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static int Max(int x, int y) { return x > y ? x : y; }

        /// <summary>
        /// Returns the value <paramref name="x"/> clamped to the specified range.
        /// </summary>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static int Clamp(int x, int min, int max) { return Min(max, Max(min, x)); }

        /// <summary>
        /// Computes the absolute value of <paramref name="x"/>.
        /// </summary>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static int Abs(int x) { return x < 0 ? -x : x; }

        /// <summary>
        /// Returns the sign of <paramref name="x"/> as if compared against zero (-1, 0 or +1)
        /// </summary>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static int Sign(int x) { return M.Sign((double) x); }

        #endregion

        #region Min Max Clamp ( uint )

        /// <summary>
        /// Returns the minimum value between <paramref name="x"/> and <paramref name="y"/>.
        /// </summary>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static uint Min(uint x, uint y) { return x < y ? x : y; }

        /// <summary>
        /// Returns the maximum value between <paramref name="x"/> and <paramref name="y"/>.
        /// </summary>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static uint Max(uint x, uint y) { return x > y ? x : y; }

        /// <summary>
        /// Returns the value <paramref name="x"/> clamped to the specified range.
        /// </summary>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static uint Clamp(uint x, uint min, uint max) { return Min(max, Max(min, x)); }

        #endregion

        #region Min Max Clamp Abs Sign ( short )

        /// <summary>
        /// Returns the minimum value between <paramref name="x"/> and <paramref name="y"/>.
        /// </summary>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static short Min(short x, short y) { return x < y ? x : y; }

        /// <summary>
        /// Returns the maximum value between <paramref name="x"/> and <paramref name="y"/>.
        /// </summary>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static short Max(short x, short y) { return x > y ? x : y; }

        /// <summary>
        /// Returns the value <paramref name="x"/> clamped to the specified range.
        /// </summary>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static short Clamp(short x, short min, short max) { return Min(max, Max(min, x)); }

        /// <summary>
        /// Computes the absolute value of <paramref name="x"/>.
        /// </summary>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static short Abs(short x) { return (short) (x < 0 ? -x : x); }

        /// <summary>
        /// Returns the sign of <paramref name="x"/> as if compared against zero (-1, 0 or +1)
        /// </summary>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static int Sign(short x) { return M.Sign((double) x); }

        #endregion

        #region Min Max Clamp ( ushort )

        /// <summary>
        /// Returns the minimum value between <paramref name="x"/> and <paramref name="y"/>.
        /// </summary>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static ushort Min(ushort x, ushort y) { return x < y ? x : y; }

        /// <summary>
        /// Returns the maximum value between <paramref name="x"/> and <paramref name="y"/>.
        /// </summary>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static ushort Max(ushort x, ushort y) { return x > y ? x : y; }

        /// <summary>
        /// Returns the value <paramref name="x"/> clamped to the specified range.
        /// </summary>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static ushort Clamp(ushort x, ushort min, ushort max) { return Min(max, Max(min, x)); }

        #endregion

        #region Min Max Clamp Abs Sign ( sbyte )

        /// <summary>
        /// Returns the minimum value between <paramref name="x"/> and <paramref name="y"/>.
        /// </summary>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static sbyte Min(sbyte x, sbyte y) { return x < y ? x : y; }

        /// <summary>
        /// Returns the maximum value between <paramref name="x"/> and <paramref name="y"/>.
        /// </summary>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static sbyte Max(sbyte x, sbyte y) { return x > y ? x : y; }

        /// <summary>
        /// Returns the value <paramref name="x"/> clamped to the specified range.
        /// </summary>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static sbyte Clamp(sbyte x, sbyte min, sbyte max) { return Min(max, Max(min, x)); }

        /// <summary>
        /// Computes the absolute value of <paramref name="x"/>.
        /// </summary>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static sbyte Abs(sbyte x) { return (sbyte) (x < 0 ? -x : x); }

        /// <summary>
        /// Returns the sign of <paramref name="x"/> as if compared against zero (-1, 0 or +1)
        /// </summary>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static int Sign(sbyte x) { return M.Sign((double) x); }

        #endregion

        #region Min Max Clamp ( byte )

        /// <summary>
        /// Returns the minimum value between <paramref name="x"/> and <paramref name="y"/>.
        /// </summary>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static byte Min(byte x, byte y) { return x < y ? x : y; }

        /// <summary>
        /// Returns the maximum value between <paramref name="x"/> and <paramref name="y"/>.
        /// </summary>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static byte Max(byte x, byte y) { return x > y ? x : y; }

        /// <summary>
        /// Returns the value <paramref name="x"/> clamped to the specified range.
        /// </summary>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static byte Clamp(byte x, byte min, byte max) { return Min(max, Max(min, x)); }

        #endregion

        #region Min Max of Many Values

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
        /// Finds the comparably minimum value from the set of value.
        /// </summary>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static T Min<T>(params T[] vals) where T : IComparable
        {
            return Min((IList<T>) vals);
        }

        /// <summary>
        /// Finds the comparably minimum value from the set of value.
        /// </summary>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static T Min<T>(IList<T> vals) where T : IComparable
        {
            var m = vals[0];
            for (var i = 1; i < vals.Count; i++)
            {
                if (m.CompareTo(vals[i]) < 0)
                {
                    m = vals[i];
                }
            }

            return m;
        }

        /// <summary>
        /// Finds the comparably maximum value from the set of value.
        /// </summary>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static T Max<T>(params T[] vals) where T : IComparable
        {
            return Max(vals);
        }

        /// <summary>
        /// Finds the comparably maximum value from the set of value.
        /// </summary>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static T Max<T>(IList<T> vals) where T : IComparable
        {
            var m = vals[0];
            for (var i = 0; i < vals.Count; i++)
            {
                if (m.CompareTo(vals[i]) > 0)
                {
                    m = vals[i];
                }
            }

            return m;
        }

        #endregion

        #region Rounding & Truncation

        /// <summary>
        /// Computes the floor integer (rounding down) of the value <paramref name="x"/>.
        /// </summary>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static int Floor(double x) { return (int) M.Floor(x); }

        /// <summary>
        /// Computes the ceiling integer (rounding up) of the value <paramref name="x"/>.
        /// </summary>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static int Ceil(double x) { return (int) M.Ceiling(x); }

        /// <summary>
        /// Computes the nearest integer of the value <paramref name="x"/>.
        /// </summary>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static int Round(double x) { return (int) M.Round(x); }

        /// <summary>
        /// Computes the floor integer (rounding down) of the value <paramref name="x"/>.
        /// </summary>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static int Floor(float x) { return (int) M.Floor(x); }

        /// <summary>
        /// Computes the ceiling integer (rounding up) of the value <paramref name="x"/>.
        /// </summary>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static int Ceil(float x) { return (int) M.Ceiling(x); }

        /// <summary>
        /// Computes the nearest integer of the value <paramref name="x"/>.
        /// </summary>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static int Round(float x) { return (int) M.Round(x); }

        /// <summary>
        /// Determines if the two values are nearly equal comparing equality within a <see cref="Epsilon"/> threshold.
        /// </summary> 
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static bool NearEquals(double x1, double x2) { return Abs(x1 - x2) < Epsilon; }

        /// <summary>
        /// Determines if the two values are nearly equal comparing equality within a threshold.
        /// </summary> 
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static bool NearEquals(double x1, double x2, float threshold) { return Abs(x1 - x2) < threshold; }

        /// <summary>
        /// Determines if the value is nearly equal to zero by comparing equality within a <see cref="Epsilon"/> threshold.
        /// </summary> 
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static bool NearZero(double v) { return v > -Epsilon && v < Epsilon; }

        /// <summary>
        /// Determines if the two values are nearly equal comparing equality within a <see cref="Epsilon"/> threshold.
        /// </summary> 
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static bool NearEquals(float x1, float x2) { return Abs(x1 - x2) < Epsilon; }

        /// <summary>
        /// Determines if the two values are nearly equal comparing equality within a threshold.
        /// </summary> 
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static bool NearEquals(float x1, float x2, float threshold) { return Abs(x1 - x2) < threshold; }

        /// <summary>
        /// Determines if the value is nearly equal to zero by comparing equality within a <see cref="Epsilon"/> threshold.
        /// </summary> 
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static bool NearZero(float v) { return v > -Epsilon && v < Epsilon; }

        #endregion

        #region Time Sensitive Interpolation

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static float LerpTime(float a, float b, float time, float dt)
        {
            return Lerp(a, b, DeltaTimeInterpolationFactor(1F, dt / time));
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static float AngleLerpTime(float a, float b, float time, float dt)
        {
            return AngleLerp(a, b, DeltaTimeInterpolationFactor(1F, dt / time));
        }

        /// <summary>
        /// Calculates a interpolation factor that moves a percentage of the interpolation adjusted independant of framerate.
        /// </summary>
        /// <param name="t"> Percentage to interpolate ( the normal 't' value of a interpolation function ) </param>
        /// <param name="dt"> Delta time, the difference in time since last frame. </param>
        /// <returns> An adjusted 't' value for use in interpolation functions. </returns> 
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static float DeltaTimeInterpolationFactor(float t, float dt)
        {
            return 1F - Pow(1F - (t - Epsilon), dt);
        }

        #endregion

        #region Linear Interpolation

        /// <summary>
        /// Computes the linear interpolation from <paramref name="x1"/> to <paramref name="x2"/> by factor <paramref name="t"/>.
        /// </summary>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static float Lerp(float x1, float x2, float t)
        {
            return x1 * (1 - t) + x2 * t;
        }

        /// <summary>
        /// Computes the linear interpolation from <paramref name="x1"/> to <paramref name="x2"/> by factor <paramref name="t"/>.
        /// </summary>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static double Lerp(double x1, double x2, double t)
        {
            return x1 * (1 - t) + x2 * t;
        }

        /// <summary>
        /// Computes the linear interpolation from <paramref name="x1"/> to <paramref name="x2"/> by factor <paramref name="t"/>.
        /// </summary>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static int Lerp(int x1, int x2, float t)
        {
            return (int) (x1 * (1 - t) + x2 * t);
        }

        /// <summary>
        /// Computes the linear interpolation from <paramref name="x1"/> to <paramref name="x2"/> by factor <paramref name="t"/>.
        /// </summary>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static uint Lerp(uint x1, uint x2, float t)
        {
            return (uint) (x1 * (1 - t) + x2 * t);
        }

        /// <summary>
        /// Computes the linear interpolation from <paramref name="x1"/> to <paramref name="x2"/> by factor <paramref name="t"/>.
        /// </summary>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static short Lerp(short x1, short x2, float t)
        {
            return (short) (x1 * (1 - t) + x2 * t);
        }

        /// <summary>
        /// Computes the linear interpolation from <paramref name="x1"/> to <paramref name="x2"/> by factor <paramref name="t"/>.
        /// </summary>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static ushort Lerp(ushort x1, ushort x2, float t)
        {
            return (ushort) (x1 * (1 - t) + x2 * t);
        }

        /// <summary>
        /// Computes the linear interpolation from <paramref name="x1"/> to <paramref name="x2"/> by factor <paramref name="t"/>.
        /// </summary>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static byte Lerp(byte x1, byte x2, float t)
        {
            return (byte) (x1 * (1 - t) + x2 * t);
        }

        /// <summary>
        /// Computes the linear interpolation from <paramref name="x1"/> to <paramref name="x2"/> by factor <paramref name="t"/>.
        /// </summary>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static sbyte Lerp(sbyte x1, sbyte x2, float t)
        {
            return (sbyte) (x1 * (1 - t) + x2 * t);
        }

        #endregion

        #region Angles

        /// <summary>
        /// Computes the linear interpolation of two angles across the shorter distance.
        /// </summary>
        /// <param name="x1">Start angle.</param>
        /// <param name="x2">End angle.</param>
        /// <param name="t">Interpolation factor</param>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static double AngleLerp(double x1, double x2, double t)
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
        /// Computes the linear interpolation of two angles across the shorter distance.
        /// </summary>
        /// <param name="x1">Start angle.</param>
        /// <param name="x2">End angle.</param>
        /// <param name="t">Interpolation factor</param>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static float AngleLerp(float x1, float x2, float t)
        {
            return (float) AngleLerp((double) x1, x2, t);
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

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static float Rescale(float x, float min1, float max1, float min2, float max2)
        {
            return Lerp(min2, max2, Calc.Between(x, min1, max1)); ;
        }

        #endregion

        #region Smoothing

        /// <summary>
        /// Computes a cosine based interpolation from <paramref name="x1"/> to <paramref name="x2"/>.
        /// </summary>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static float CosineInterpolation(float x1, float x2, float t)
        {
            var angle = t * Pi;
            var prc = (1.0f - Cos(angle)) * 0.5f;
            return x1 * (1.0f - prc) + x2 * prc;
        }

        /// <summary>
        /// Computes the smooth-step of <paramref name="x"/> between <paramref name="min"/> and <paramref name="max"/>.
        /// </summary>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static float SmoothStep(float min, float max, float x)
        {
            // Scale, bias and clamp to 0 to 1 range
            x = Clamp(Between(x, min, max), 0.0F, 1.0F);

            // Evaluate polynomial
            return x * x * (3 - 2 * x);
        }

        /// <summary>
        /// Computes the smoother smooth-step of <paramref name="x"/> between <paramref name="min"/> and <paramref name="max"/>.
        /// </summary>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static float SmootherStep(float min, float max, float x)
        {
            // Scale, and clamp to 0 to 1 range
            x = Clamp(Between(x, min, max), 0.0F, 1.0F);

            // Evaluate polynomial
            return x * x * x * (x * (x * 6 - 15) + 10);
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
            x = x | (x >> 1);
            x = x | (x >> 2);
            x = x | (x >> 4);
            x = x | (x >> 8);
            x = x | (x >> 16);
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
            x = x | (x >> 1);
            x = x | (x >> 2);
            x = x | (x >> 4);
            x = x | (x >> 8);
            x = x | (x >> 16);
            return x - (x >> 1);
        }

        /// <summary>
        /// Computes the upper power of 2 nearest to x.
        /// </summary>
        /// <param name="x"> Some unsigned integer </param>
        /// <returns> The nearest upper power of 2 to x </returns>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static uint UpperPowerOfTwo(uint v)
        {
            v--;
            v |= v >> 1;
            v |= v >> 2;
            v |= v >> 4;
            v |= v >> 8;
            v |= v >> 16;
            v++;

            return v;
        }

        /// <summary>
        /// Computes the upper power of 2 nearest to x.
        /// </summary>
        /// <param name="x"> Some integer </param>
        /// <returns> The nearest upper power of 2 to x </returns>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static int UpperPowerOfTwo(int v)
        {
            v--;
            v |= v >> 1;
            v |= v >> 2;
            v |= v >> 4;
            v |= v >> 8;
            v |= v >> 16;
            v++;

            return v;
        }

        /// <summary>
        /// Determines if the given integer is a power of 2.
        /// </summary>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static bool IsPowerOfTwo(ulong x) { return (x & ~(x - 1)) == x; }

        /// <summary>
        /// Determines if the given integer is a power of 2.
        /// </summary>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static bool IsPowerOfTwo(uint x) { return (x & ~(x - 1)) == x; }

        /// <summary>
        /// Determines if the given integer is a power of 2.
        /// </summary>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static bool IsPowerOfTwo(int x) { return (x & ~(x - 1)) == x; }

        #endregion

        #region Swap

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
        public static void Swap<T>(IList<T> list, int a, int b)
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
        /// <param name="min">Some lower bound.</param>
        /// <param name="max">Some upper bound.</param>
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
        /// <param name="min">Some lower bound.</param>
        /// <param name="max">Some upper bound.</param>
        /// <returns>The resulting number contrained to the range in a loop.</returns>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static float Wrap(float x, Range range)
        {
            return Wrap(x, range.Max, range.Min);
        }

        #endregion

        /// <summary>
        /// Returns <paramref name="center"/> if <paramref name="x"/> is within <paramref name="spread"/> units of <paramref name="center"/> otherwise <paramref name="x"/>.
        /// </summary>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static float DeadZone(float x, float center, float spread)
        {
            if (x < (center - spread))
            {
                return x;
            }
            else if (x > (center + spread))
            {
                return x;
            }
            else
            {
                return center;
            }
        }
    }
}
