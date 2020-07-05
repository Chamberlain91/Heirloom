using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Runtime.InteropServices;

namespace Heirloom
{
    /// <summary>
    /// Represents a range of single-precision floating point numbers from <see cref="Min"/> to <see cref="Max"/>.
    /// </summary>
    /// <category>Mathematics</category>
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

        #region Constructors

        /// <summary>
        /// Constructs a new <see cref="Range"/>.
        /// </summary>
        public Range(float min, float max)
        {
            Min = min;
            Max = max;
        }

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

        /// <summary>
        /// Sets the components of this range.
        /// </summary>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public void Set(float min, float max)
        {
            Min = min;
            Max = max;
        }

        /// <summary>
        /// Sets both components to a constant value.
        /// </summary>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public void Set(float constant)
        {
            Min = constant;
            Max = constant;
        }

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

        /// <summary>
        /// Computes the intersection of this range with another.
        /// </summary>
        public Range Intersect(in Range other)
        {
            var min = Calc.Max(Min, other.Min);
            var max = Calc.Min(Max, other.Max);
            return new Range(min, max);
        }

        /// <summary>
        /// Computes the union of this range with another.
        /// </summary>
        public Range Union(in Range other)
        {
            var min = Calc.Min(Min, other.Min);
            var max = Calc.Max(Max, other.Max);
            return new Range(min, max);
        }

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
            return Calc.Rescale(x, Min, Max, outRange.Min, outRange.Max);
        }

        #endregion

        /// <summary>
        /// Creates an instance of <see cref="Range"/> from the extreme values of the given collection of numbers.
        /// </summary>
        /// <exception cref="ArgumentNullException">Thrown when a <paramref name="values"/> is null.</exception>
        public static Range FromValues(IEnumerable<float> values)
        {
            if (values is null) { throw new ArgumentNullException(nameof(values)); }

            if (!values.Any()) { return Zero; }
            else
            {
                var max = float.NegativeInfinity;
                var min = float.PositiveInfinity;

                foreach (var val in values)
                {
                    max = Calc.Max(max, val);
                    min = Calc.Min(min, val);
                }

                return new Range(min, max);
            }
        }

        #region Deconstruct

        /// <summary>
        /// Deconstructs this <see cref="Range"/> into consituent parts.
        /// </summary>
        /// <param name="min">Outputs the minumum value of the range.</param>
        /// <param name="max">Outputs the maximum value of the range.</param>
        public void Deconstruct(out float min, out float max)
        {
            min = Min;
            max = Max;
        }

        #endregion

        #region Conversion Operators

        /// <summary>
        /// Converts an <see cref="Vector"/> into <see cref="Range"/>
        /// by convention of <see cref="Vector.X"/> and <see cref="Vector.Y"/> as <see cref="Min"/> and <see cref="Max"/> respectively.
        /// </summary>
        public static explicit operator Range(Vector vec)
        {
            var width = vec.X;
            var height = vec.Y;

            return new Range(width, height);
        }

        /// <summary>
        /// Converts an <see cref="Range"/> into <see cref="Vector"/>
        /// by convention of <see cref="Min"/> and <see cref="Max"/> to <see cref="Vector.X"/> and <see cref="Vector.Y"/> respectively.
        /// </summary>
        public static explicit operator Vector(Range range)
        {
            var x = range.Min;
            var y = range.Max;

            return new Vector(x, y);
        }

        /// <summary>
        /// Converts <see cref="Range"/> into a formatted tuple.
        /// </summary>
        public static implicit operator (float min, float max)(Range range)
        {
            return (range.Min, range.Max);
        }

        /// <summary>
        /// Converts a formatted tuple into a <see cref="Range"/>.
        /// </summary>
        public static implicit operator Range((float min, float max) vec)
        {
            return new Range(vec.min, vec.max);
        }

        /// <summary>
        /// Converts a single value into a <see cref="Range"/> with <see cref="Min"/> and <see cref="Max"/> set to <paramref name="constant"/>.
        /// </summary>
        public static implicit operator Range(float constant)
        {
            return new Range(constant, constant);
        }

        #endregion

        #region Comparison Operators

        /// <summary>
        /// Compares two ranges for equality.
        /// </summary>
        public static bool operator ==(Range range1, Range range2)
        {
            return range1.Equals(range2);
        }

        /// <summary>
        /// Compares two ranges for inequality.
        /// </summary>
        public static bool operator !=(Range range1, Range range2)
        {
            return !(range1 == range2);
        }

        #endregion

        #region Equality

        /// <summary>
        /// Compares this range for equality with another object.
        /// </summary>
        public override bool Equals(object obj)
        {
            return obj is Range range
                && Equals(range);
        }

        /// <summary>
        /// Compares this range for equality with another range.
        /// </summary>
        public bool Equals(Range other)
        {
            return Calc.NearEquals(Min, other.Min)
                && Calc.NearEquals(Max, other.Max);
        }

        /// <summary>
        /// Returns the hash code for this range.
        /// </summary>
        public override int GetHashCode()
        {
            return HashCode.Combine(Min, Max);
        }

        #endregion

        /// <summary>
        /// Returns the string representation of this <see cref="Range"/>.
        /// </summary>
        public override string ToString()
        {
            return $"({Min} to {Max})";
        }
    }
}
