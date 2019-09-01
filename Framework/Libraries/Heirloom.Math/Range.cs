using System;
using System.Runtime.InteropServices;

namespace Heirloom.Math
{
    [StructLayout(LayoutKind.Sequential, Pack = 4)]
    public struct Range : IEquatable<Range>
    {
        public float Min;

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

        public float Random => Calc.Lerp(Min, Max, Calc.Random.NextFloat());

        #endregion

        #region Constructors

        public Range(float min, float max)
        {
            Min = min;
            Max = max;
        }

        #endregion

        #region Contains, Overlaps

        public bool Contains(in float x)
        {
            if (x < Min) { return false; }
            if (x > Max) { return false; }
            return true;
        }

        public bool Overlaps(in Range other)
        {
            return Min < other.Max && Max > other.Min;
        }

        #endregion

        #region Include

        public void Include(in float val)
        {
            Min = Calc.Min(Min, val);
            Max = Calc.Max(Max, val);
        }

        public void Include(in Range range)
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
