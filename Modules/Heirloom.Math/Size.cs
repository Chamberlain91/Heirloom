using System;
using System.Runtime.InteropServices;

namespace Heirloom.Math
{
    [StructLayout(LayoutKind.Sequential, Pack = 4)]
    public struct Size : IEquatable<Size>, IComparable<Size>
    {
        /// <summary>
        /// The width (horizontal size measure).
        /// </summary>
        public float Width;

        /// <summary>
        /// The height (vertical size measure).
        /// </summary>
        public float Height;

        #region Constants

        /// <summary>
        /// An infinite size.
        /// </summary>
        public static readonly Size Infinite = new Size(float.PositiveInfinity, float.PositiveInfinity);

        /// <summary>
        /// The maximum representable size possible.
        /// </summary>
        public static readonly Size Max = new Size(float.MaxValue, float.MaxValue);

        /// <summary>
        /// A 0x0 size.
        /// </summary>
        public static readonly Size Zero = new Size(0, 0);

        /// <summary>
        /// A 1x1 size.
        /// </summary>
        public static readonly Size One = new Size(1, 1);

        #endregion

        #region Constructors

        public Size(float width, float height)
        {
            Width = width;
            Height = height;
        }

        #endregion

        #region Properties

        /// <summary>
        /// Gets the area of this size as if it was a rectangle at the origin.
        /// </summary>
        public float Area => Width * Height;

        /// <summary>
        /// Gets the aspect ratio of this size.
        /// </summary>
        public float Aspect => Width / (float) Height;

        #endregion

        #region Deconstruct

        public void Deconstruct(out float width, out float height)
        {
            width = Width;
            height = Height;
        }

        #endregion

        #region Conversion Operators

        public static explicit operator Vector(Size size)
        {
            var x = size.Width;
            var y = size.Height;

            return new Vector(x, y);
        }

        public static explicit operator IntVector(Size size)
        {
            var x = (int) size.Width;
            var y = (int) size.Height;

            return new IntVector(x, y);
        }

        public static explicit operator IntSize(Size size)
        {
            var width = (int) size.Width;
            var height = (int) size.Height;

            return new IntSize(width, height);
        }

        public static implicit operator Size((float width, float height) size)
        {
            return new Size(size.width, size.height);
        }

        public static implicit operator (float width, float height)(Size size)
        {
            return (size.Width, size.Height);
        }

        #endregion

        #region Arithmetic Operators

        public static Size operator -(Size v)
        {
            var width = -v.Width;
            var height = -v.Height;
            return new Size(width, height);
        }

        #region Addition

        public static Size operator +(Size a, Size b)
        {
            var width = a.Width + b.Width;
            var height = a.Height + b.Height;
            return new Size(width, height);
        }

        public static Size operator +(IntSize a, Size b)
        {
            var width = a.Width + b.Width;
            var height = a.Height + b.Height;
            return new Size(width, height);
        }

        public static Size operator +(Size a, IntSize b)
        {
            var width = a.Width + b.Width;
            var height = a.Height + b.Height;
            return new Size(width, height);
        }

        #endregion

        #region Subtraction

        public static Size operator -(Size a, Size b)
        {
            var width = a.Width - b.Width;
            var height = a.Height - b.Height;
            return new Size(width, height);
        }

        public static Size operator -(IntSize a, Size b)
        {
            var width = a.Width - b.Width;
            var height = a.Height - b.Height;
            return new Size(width, height);
        }

        public static Size operator -(Size a, IntSize b)
        {
            var width = a.Width - b.Width;
            var height = a.Height - b.Height;
            return new Size(width, height);
        }

        #endregion

        #region Multiply / Divide

        public static Size operator /(Size a, Size b)
        {
            var width = a.Width / b.Width;
            var height = a.Height / b.Height;

            return new Size(width, height);
        }

        public static Size operator *(Size a, float v)
        {
            var width = a.Width * v;
            var height = a.Height * v;
            return new Size(width, height);
        }

        public static Size operator *(float v, Size a)
        {
            var width = a.Width * v;
            var height = a.Height * v;
            return new Size(width, height);
        }

        public static Size operator /(Size a, float v)
        {
            var width = a.Width / v;
            var height = a.Height / v;
            return new Size(width, height);
        }

        public static Size operator /(float v, Size a)
        {
            var width = v / a.Width;
            var height = v / a.Height;
            return new Size(width, height);
        }

        #endregion

        #endregion

        #region Comparison Operators

        public int CompareTo(Size other)
        {
            return Area.CompareTo(other.Area);
        }

        public static bool operator <(Size left, Size right)
        {
            return left.CompareTo(right) < 0;
        }

        public static bool operator <=(Size left, Size right)
        {
            return left.CompareTo(right) <= 0;
        }

        public static bool operator >(Size left, Size right)
        {
            return left.CompareTo(right) > 0;
        }

        public static bool operator >=(Size left, Size right)
        {
            return left.CompareTo(right) >= 0;
        }

        #endregion

        #region Equality

        public override bool Equals(object obj)
        {
            return obj is Size && Equals((Size) obj);
        }

        public bool Equals(Size other)
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

        public static bool operator ==(Size size1, Size size2)
        {
            return size1.Equals(size2);
        }

        public static bool operator !=(Size size1, Size size2)
        {
            return !(size1 == size2);
        }

        #endregion

        public override string ToString()
        {
            return $"({Width} by {Height})";
        }
    }
}
