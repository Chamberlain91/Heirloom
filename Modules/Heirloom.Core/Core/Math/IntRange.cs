using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Runtime.InteropServices;

namespace Heirloom
{
    /// <summary>
    /// Represents a range of integers from <see cref="Min"/> to <see cref="Max"/>.
    /// </summary>
    [StructLayout(LayoutKind.Sequential, Pack = 4)]
    public struct IntRange : IEquatable<IntRange>, IEnumerable<int>
    {
        /// <summary>
        /// The minimum value in the range.
        /// </summary>
        public int Min;

        /// <summary>
        /// The maximum value in the range.
        /// </summary>
        public int Max;

        #region Constants

        /// <summary>
        /// Range from <see cref="int.MinValue"/> to <see cref="int.MaxValue"/> (the widest possible range).
        /// </summary>
        public static readonly IntRange Huge = new IntRange(int.MinValue, int.MaxValue);

        /// <summary>
        /// Range from <see cref="int.MaxValue"/> to <see cref="int.MinValue"/> useful for an indeterminate range to compute bounds.
        /// </summary>
        public static readonly IntRange Indeterminate = new IntRange(int.MinValue, int.MaxValue);

        /// <summary>
        /// Zero width range centered on zero.
        /// </summary>
        public static readonly IntRange Zero = new IntRange(0, 0);

        #endregion

        #region Properties

        /// <summary>
        /// Gets the mean of <see cref="Min"/> and <see cref="Max"/>.
        /// </summary>
        public int Average => (Min + Max) / 2;

        /// <summary>
        /// Gets the size of the range.
        /// </summary>
        public int Size => Max - Min;

        /// <summary>
        /// Gets a value that determines if the range is valid (ie, <c><see cref="Max"/> >= <see cref="Min"/></c>).
        /// </summary>
        public bool IsValid => Max >= Min;

        #endregion

        /// <summary>
        /// Sets the components of this range.
        /// </summary>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public void Set(int min, int max)
        {
            Min = min;
            Max = max;
        }

        #region Constructors

        /// <summary>
        /// Constructs a new <see cref="IntRange"/>.
        /// </summary>
        /// <param name="min">The minimum value.</param>
        /// <param name="max">The maximum value.</param>
        public IntRange(int min, int max)
        {
            Min = min;
            Max = max;
        }

        #endregion

        #region Contains, Overlaps

        /// <summary>
        /// Determines if this range contains the specified value.
        /// </summary>
        public bool Contains(in int x)
        {
            if (x < Min) { return false; }
            if (x > Max) { return false; }
            return true;
        }

        /// <summary>
        /// Determines if this range overlaps another integer range.
        /// </summary>
        public bool Overlaps(in IntRange other)
        {
            return Min < other.Max && Max > other.Min;
        }

        #endregion

        #region Include

        /// <summary>
        /// Mutate this range (by expansion) to include the specified value.
        /// </summary>
        public void Include(int val)
        {
            Min = Calc.Min(Min, val);
            Max = Calc.Max(Max, val);
        }

        /// <summary>
        /// Mutate this range (by expansion) to include the specified range.
        /// </summary>
        public void Include(IntRange range)
        {
            Min = Calc.Min(Min, range.Min);
            Max = Calc.Max(Max, range.Max);
        }

        #endregion

        /// <summary>
        /// Computes the intersection of this range with another.
        /// </summary>
        public IntRange Intersect(in IntRange other)
        {
            var min = Calc.Max(Min, other.Min);
            var max = Calc.Min(Max, other.Max);
            return new IntRange(min, max);
        }

        /// <summary>
        /// Computes the union of this range with another.
        /// </summary>
        public IntRange Union(in IntRange other)
        {
            var min = Calc.Min(Min, other.Min);
            var max = Calc.Max(Max, other.Max);
            return new IntRange(min, max);
        }

        /// <summary>
        /// Creates an instance of <see cref="IntRange"/> from the extreme values of the given collection of numbers.
        /// </summary>
        /// <exception cref="ArgumentNullException">Thrown when a <paramref name="values"/> is null.</exception>
        public static IntRange FromValues(IEnumerable<int> values)
        {
            if (values is null) { throw new ArgumentNullException(nameof(values)); }

            if (!values.Any()) { return Zero; }
            else
            {
                var max = int.MinValue;
                var min = int.MaxValue;

                foreach (var val in values)
                {
                    max = Calc.Max(max, val);
                    min = Calc.Min(min, val);
                }

                return new IntRange(min, max);
            }
        }

        #region Deconstruct

        /// <summary>
        /// Deconstructs this <see cref="IntRange"/> into consituent parts.
        /// </summary>
        /// <param name="min">Outputs the minumum value of the range.</param>
        /// <param name="max">Outputs the maximum value of the range.</param>
        public void Deconstruct(out int min, out int max)
        {
            min = Min;
            max = Max;
        }

        #endregion

        #region Conversion Operators

        /// <summary>
        /// Converts an <see cref="IntRange"/> into <see cref="Range"/>.
        /// </summary>
        public static implicit operator Range(IntRange vec)
        {
            var min = vec.Min;
            var max = vec.Max;

            return new Range(min, max);
        }

        /// <summary>
        /// Converts an <see cref="IntVector"/> into <see cref="IntRange"/>
        /// by convention of <see cref="IntVector.X"/> and <see cref="IntVector.Y"/> as <see cref="Min"/> and <see cref="Max"/> respectively.
        /// </summary>
        public static explicit operator IntRange(IntVector vec)
        {
            var min = vec.X;
            var max = vec.Y;

            return new IntRange(min, max);
        }

        /// <summary>
        /// Converts an <see cref="IntRange"/> into <see cref="IntVector"/>
        /// by convention of <see cref="Min"/> and <see cref="Max"/> to <see cref="IntVector.X"/> and <see cref="IntVector.Y"/> respectively.
        /// </summary>
        public static explicit operator IntVector(IntRange range)
        {
            var x = range.Min;
            var y = range.Max;

            return new IntVector(x, y);
        }

        /// <summary>
        /// Converts <see cref="IntRange"/> into a formatted tuple.
        /// </summary>
        public static implicit operator (int min, int max)(IntRange range)
        {
            return (range.Min, range.Max);
        }

        /// <summary>
        /// Converts a formatted tuple into a <see cref="IntRange"/>.
        /// </summary>
        public static implicit operator IntRange((int min, int max) vec)
        {
            return new IntRange(vec.min, vec.max);
        }

        #endregion

        #region Comparison Operators

        /// <summary>
        /// Compares two ranges for equality.
        /// </summary>
        public static bool operator ==(IntRange range1, IntRange range2)
        {
            return range1.Equals(range2);
        }

        /// <summary>
        /// Compares two ranges for inequality.
        /// </summary>
        public static bool operator !=(IntRange range1, IntRange range2)
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
            return obj is IntRange range
                && Equals(range);
        }

        /// <summary>
        /// Compares this range for equality with another range.
        /// </summary>
        public bool Equals(IntRange other)
        {
            return Min == other.Min
                && Max == other.Max;
        }

        /// <summary>
        /// Returns the hash code for this range.
        /// </summary>
        /// <returns></returns>
        public override int GetHashCode()
        {
            return HashCode.Combine(Min, Max);
        }

        #endregion

        #region IEnumerable<int>

        /// <summary>
        /// Returns an enumerator that will iterate over all integer values from <see cref="Min"/> to <see cref="Max"/>.
        /// </summary>
        public IEnumerator<int> GetEnumerator()
        {
            return Enumerable.Range(Min, Max - Min).GetEnumerator();
        }

        IEnumerator IEnumerable.GetEnumerator()
        {
            return GetEnumerator();
        }

        #endregion

        /// <summary>
        /// Returns the string representation of this <see cref="IntRange"/>.
        /// </summary>
        public override string ToString()
        {
            return $"({Min} to {Max})";
        }
    }
}
