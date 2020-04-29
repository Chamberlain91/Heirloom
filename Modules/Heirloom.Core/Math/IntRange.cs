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

        #region Deconstruct

        public void Deconstruct(out int min, out int max)
        {
            min = Min;
            max = Max;
        }

        #endregion

        #region Conversion Operators

        public static implicit operator Range(IntRange vec)
        {
            var min = vec.Min;
            var max = vec.Max;

            return new Range(min, max);
        }

        public static explicit operator IntRange(IntVector vec)
        {
            var width = vec.X;
            var height = vec.Y;

            return new IntRange(width, height);
        }

        public static explicit operator IntVector(IntRange range)
        {
            var x = range.Min;
            var y = range.Max;

            return new IntVector(x, y);
        }

        public static implicit operator (int min, int max)(IntRange range)
        {
            return (range.Min, range.Max);
        }

        public static implicit operator IntRange((int min, int max) vec)
        {
            return new IntRange(vec.min, vec.max);
        }

        #endregion

        #region Comparison Operators

        public static bool operator ==(IntRange range1, IntRange range2)
        {
            return range1.Equals(range2);
        }

        public static bool operator !=(IntRange range1, IntRange range2)
        {
            return !(range1 == range2);
        }

        #endregion

        #region Equality

        public override bool Equals(object obj)
        {
            return obj is IntRange range
                && Equals(range);
        }

        public bool Equals(IntRange other)
        {
            return Min == other.Min
                && Max == other.Max;
        }

        public override int GetHashCode()
        {
            var hashCode = 1537547080;
            hashCode = hashCode * -1521134295 + Min.GetHashCode();
            hashCode = hashCode * -1521134295 + Max.GetHashCode();
            return hashCode;
        }

        #endregion

        #region IEnumerable<int>

        public IEnumerator<int> GetEnumerator()
        {
            return Enumerable.Range(Min, Max - Min).GetEnumerator();
        }

        IEnumerator IEnumerable.GetEnumerator()
        {
            return GetEnumerator();
        }

        #endregion

        public override string ToString()
        {
            return $"({Min} to {Max})";
        }
    }
}
