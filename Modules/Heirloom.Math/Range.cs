using System;
using System.Runtime.CompilerServices;
using System.Runtime.InteropServices;

namespace Heirloom.Math
{
    /// <summary>
    /// Represents a range of single-precision floating point numbers from <see cref="Min"/> to <see cref="Max"/>.
    /// </summary>
    [StructLayout(LayoutKind.Sequential, Pack = 4)]
    public struct Range : IEquatable<Range>
    {
        /// <summary>
        /// The minimum value in the range.
        /// </summary>
        public float Min;

        /// <summary>
        /// The maximum value in the range.
        /// </summary>
        public float Max;

        #region Constants

        /// <summary>
        /// Range from <see cref="float.NegativeInfinity"/> to <see cref="float.PositiveInfinity"/> (the widest possible range).
        /// </summary>
        public static readonly Range Infinite = new Range(float.NegativeInfinity, float.PositiveInfinity);

        /// <summary>
        /// Range from <see cref="float.PositiveInfinity"/> to <see cref="float.NegativeInfinity"/> useful for an indeterminate range to compute bounds.
        /// </summary>
        public static readonly Range Indeterminate = new Range(float.PositiveInfinity, float.NegativeInfinity);

        /// <summary>
        /// Zero width range centered on zero.
        /// </summary>
        public static readonly Range Zero = new Range(0, 0);

        #endregion

        #region Properties

        /// <summary>
        /// Gets the mean of <see cref="Min"/> and <see cref="Max"/>.
        /// </summary>
        public float Average => (Min + Max) / 2;

        /// <summary>
        /// Gets the size of the range.
        /// </summary>
        public float Size => Max - Min;

        /// <summary>
        /// Gets a value that determines if the range is valid (ie, <c><see cref="Max"/> >= <see cref="Min"/></c>).
        /// </summary>
        public bool IsValid => Max >= Min;

        #endregion

        #region Constructors

        public Range(float min, float max)
        {
            Min = min;
            Max = max;
        }

        #endregion

        #region Contains, Overlaps

        /// <summary>
        /// Determines if this range contains the specified value.
        /// </summary>
        public bool Contains(in float x)
        {
            if (x < Min) { return false; }
            if (x > Max) { return false; }
            return true;
        }

        /// <summary>
        /// Determines if this range overlaps another range.
        /// </summary>
        public bool Overlaps(in Range other)
        {
            return Min < other.Max && Max > other.Min;
        }

        #endregion

        #region Include

        /// <summary>
        /// Mutate this range (by expansion) to include the specified value.
        /// </summary>
        public void Include(in float val)
        {
            Min = Calc.Min(Min, val);
            Max = Calc.Max(Max, val);
        }

        /// <summary>
        /// Mutate this range (by expansion) to include the specified range.
        /// </summary>
        public void Include(in Range range)
        {
            Min = Calc.Min(Min, range.Min);
            Max = Calc.Max(Max, range.Max);
        }

        #endregion

        #region Rescale

        /// <summary>
        /// Scales <paramref name="x"/> from input domain (this range) to output range.
        /// </summary>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public float Rescale(in float x, in float outMin, in float outMax)
        {
            return Calc.Rescale(x, Min, Max, outMin, outMax);
        }

        /// <summary>
        /// Scales <paramref name="x"/> from input domain (this range) to output range.
        /// </summary>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public float Rescale(in float x, in Range outRange)
        {
            return Calc.Rescale(x, this, outRange);
        }

        #endregion

        #region Deconstruct

        public void Deconstruct(out float min, out float max)
        {
            min = Min;
            max = Max;
        }

        #endregion

        #region Conversion Operators

        public static explicit operator Range(Vector vec)
        {
            var width = vec.X;
            var height = vec.Y;

            return new Range(width, height);
        }

        public static explicit operator Vector(Range range)
        {
            var x = range.Min;
            var y = range.Max;

            return new Vector(x, y);
        }

        public static implicit operator (float min, float max)(Range range)
        {
            return (range.Min, range.Max);
        }

        public static implicit operator Range((float min, float max) vec)
        {
            return new Range(vec.min, vec.max);
        }

        #endregion

        #region Comparison Operators

        public static bool operator ==(Range range1, Range range2)
        {
            return range1.Equals(range2);
        }

        public static bool operator !=(Range range1, Range range2)
        {
            return !(range1 == range2);
        }

        #endregion

        #region Equality

        public override bool Equals(object obj)
        {
            return obj is Range range
                && Equals(range);
        }

        public bool Equals(Range other)
        {
            return Calc.NearEquals(Min, other.Min)
                && Calc.NearEquals(Max, other.Max);
        }

        public override int GetHashCode()
        {
            var hashCode = 1537547080;
            hashCode = hashCode * -1521134295 + Min.GetHashCode();
            hashCode = hashCode * -1521134295 + Max.GetHashCode();
            return hashCode;
        }

        #endregion

        public override string ToString()
        {
            return $"({Min} to {Max})";
        }
    }
}
