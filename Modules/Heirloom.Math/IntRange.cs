using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.InteropServices;

namespace Heirloom.Math
{
    [StructLayout(LayoutKind.Sequential, Pack = 4)]
    public struct IntRange : IEquatable<IntRange>, IEnumerable<int>
    {
        public int Min;

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

        public int Random => Calc.Lerp(Min, Max, Calc.Random.NextFloat());

        public int Average => (Min + Max) / 2;

        public int Size => Max - Min;

        #endregion

        #region Constructors

        public IntRange(int min, int max)
        {
            Min = min;
            Max = max;
        }

        #endregion

        #region Contains, Overlaps

        public bool Contains(in int x)
        {
            if (x < Min) { return false; }
            if (x > Max) { return false; }
            return true;
        }

        public bool Overlaps(in IntRange other)
        {
            return Min < other.Max && Max > other.Min;
        }

        #endregion

        #region Include

        public void Include(int val)
        {
            Min = Calc.Min(Min, val);
            Max = Calc.Max(Max, val);
        }

        public void Include(IntRange range)
        {
            Min = Calc.Min(Min, range.Min);
            Max = Calc.Max(Max, range.Max);
        }

        #endregion

        #region Order / Swap

        public void Order()
        {
            if (Max < Min) { Swap(); }
        }

        public void Swap()
        {
            Calc.Swap(ref Min, ref Max);
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

        public IEnumerator<int> GetEnumerator()
        {
            return Enumerable.Range(Min, Max - Min).GetEnumerator();
        }

        IEnumerator IEnumerable.GetEnumerator()
        {
            return GetEnumerator();
        }

        public override string ToString()
        {
            return $"({Min} to {Max})";
        }
    }
}
