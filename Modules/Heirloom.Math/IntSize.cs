using System;
using System.Runtime.InteropServices;

namespace Heirloom.Math
{
    [StructLayout(LayoutKind.Sequential, Pack = 4)]
    public struct IntSize : IEquatable<IntSize>, IComparable<IntSize>
    {
        public int Width;

        public int Height;

        #region Constants

        public static readonly IntSize Max = new IntSize(int.MaxValue, int.MaxValue);

        public static readonly IntSize Zero = new IntSize(0, 0);

        public static readonly IntSize One = new IntSize(1, 1);

        #endregion

        #region Properties

        public int Area => Width * Height;

        public float Aspect => Width / (float) Height;

        #endregion

        #region Constructors

        public IntSize(int width, int height)
        {
            Width = width;
            Height = height;
        }

        #endregion

        #region Deconstruct

        public void Deconstruct(out int width, out int height)
        {
            width = Width;
            height = Height;
        }

        #endregion

        #region Conversion Operators

        public static explicit operator IntVector(IntSize size)
        {
            var x = size.Width;
            var y = size.Height;

            return new IntVector(x, y);
        }

        public static explicit operator Vector(IntSize size)
        {
            var x = size.Width;
            var y = size.Height;

            return new Vector(x, y);
        }

        public static implicit operator Size(IntSize vec)
        {
            var width = vec.Width;
            var height = vec.Height;

            return new Size(width, height);
        }

        public static implicit operator IntSize((int width, int height) size)
        {
            return new IntSize(size.width, size.height);
        }

        public static implicit operator (int width, int height) (IntSize size)
        {
            return (size.Width, size.Height);
        }

        #endregion

        #region Arithmetic Operators

        public static IntSize operator -(IntSize v)
        {
            var width = -v.Width;
            var height = -v.Height;

            return new IntSize(width, height);
        }

        #region Addition / Subtraction

        public static IntSize operator +(IntSize a, IntSize b)
        {
            var width = a.Width + b.Width;
            var height = a.Height + b.Height;

            return new IntSize(width, height);
        }

        public static IntSize operator -(IntSize a, IntSize b)
        {
            var width = a.Width - b.Width;
            var height = a.Height - b.Height;

            return new IntSize(width, height);
        }

        #endregion

        #region Multiply

        public static IntSize operator *(IntSize a, IntSize b)
        {
            var width = a.Width * b.Width;
            var height = a.Height * b.Height;

            return new IntSize(width, height);
        }

        public static Size operator *(IntSize a, float v)
        {
            var width = a.Width * v;
            var height = a.Height * v;

            return new Size(width, height);
        }

        public static IntSize operator *(IntSize a, int v)
        {
            var width = a.Width * v;
            var height = a.Height * v;

            return new IntSize(width, height);
        }

        public static IntSize operator *(int v, IntSize a)
        {
            var width = a.Width * v;
            var height = a.Height * v;

            return new IntSize(width, height);
        }

        public static Size operator *(float v, IntSize a)
        {
            var width = a.Width * v;
            var height = a.Height * v;

            return new Size(width, height);
        }

        #endregion

        #region Divide

        public static IntSize operator /(IntSize a, IntSize b)
        {
            var width = a.Width / b.Width;
            var height = a.Height / b.Height;

            return new IntSize(width, height);
        }

        public static IntSize operator /(IntSize a, int v)
        {
            var width = a.Width / v;
            var height = a.Height / v;

            return new IntSize(width, height);
        }

        public static Size operator /(IntSize a, float v)
        {
            var width = a.Width / v;
            var height = a.Height / v;

            return new Size(width, height);
        }

        public static IntSize operator /(int v, IntSize a)
        {
            var width = v / a.Width;
            var height = v / a.Height;

            return new IntSize(width, height);
        }

        public static Size operator /(float v, IntSize a)
        {
            var width = v / a.Width;
            var height = v / a.Height;

            return new Size(width, height);
        }

        #endregion

        public static bool operator ==(IntSize a, IntSize b)
        {
            return a.Equals(b);
        }

        public static bool operator !=(IntSize a, IntSize b)
        {
            return !(a == b);
        }

        #endregion

        #region Equality

        public override bool Equals(object obj)
        {
            return obj is IntSize size
                && Equals(size);
        }

        public bool Equals(IntSize other)
        {
            return Width == other.Width &&
                   Height == other.Height;
        }

        public override int GetHashCode()
        {
            var hashCode = 859600377;
            hashCode = hashCode * -1521134295 + Width.GetHashCode();
            hashCode = hashCode * -1521134295 + Height.GetHashCode();
            return hashCode;
        }

        #endregion

        public int CompareTo(IntSize other)
        {
            return Area.CompareTo(other.Area);
        }

        public override string ToString()
        {
            return $"({Width} by {Height})";
        }
    }
}
