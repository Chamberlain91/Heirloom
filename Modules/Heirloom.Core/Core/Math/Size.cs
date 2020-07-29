using System;
using System.Runtime.CompilerServices;
using System.Runtime.InteropServices;

namespace Heirloom
{
    /// <summary>
    /// Represents two dimensional size by a measure in each axis.
    /// </summary>
    /// <category>Mathematics</category>
    [StructLayout(LayoutKind.Sequential, Pack = 4)]
    public struct Size : IEquatable<Size>, IFormattable
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

        /// <summary>
        /// Constructs a new instance of <see cref="Size"/>.
        /// </summary>
        /// <param name="width">The width value.</param>
        /// <param name="height">The height value.</param>
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
        public float Aspect => Width / Height;

        #endregion

        #region Indexer

        /// <summary>
        /// Accesses the <see cref="Width"/> and <see cref="Height"/> components by numeric index respectively.
        /// </summary>
        public float this[int i]
        {
            get => i switch
            {
                0 => Width,
                1 => Height,
                _ => throw new IndexOutOfRangeException(),
            };

            set
            {
                switch (i)
                {
                    case 0: Width = value; break;
                    case 1: Height = value; break;
                    default:
                        throw new IndexOutOfRangeException();
                }
            }
        }

        #endregion

        /// <summary>
        /// Sets the components of this size.
        /// </summary>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public void Set(float w, float h)
        {
            Width = w;
            Height = h;
        }

        #region Deconstruct

        /// <summary>
        /// Deconstructs this <see cref="Size"/> int constituent components.
        /// </summary>
        /// <param name="width">Outputs the width component.</param>
        /// <param name="height">Outputs the height comonent.</param>
        public void Deconstruct(out float width, out float height)
        {
            width = Width;
            height = Height;
        }

        #endregion

        #region Conversion Operators

        /// <summary>
        /// Converts a size structure into a <see cref="Vector"/> by convention of <see cref="Width"/> and <see cref="Height"/>
        /// to <see cref="Vector.X"/> and <see cref="Vector.Y"/> respectively.
        /// </summary>
        public static explicit operator Vector(Size size)
        {
            var x = size.Width;
            var y = size.Height;

            return new Vector(x, y);
        }

        /// <summary>
        /// Converts a size structure into a <see cref="IntVector"/> by convention of <see cref="Width"/> and <see cref="Height"/>
        /// to <see cref="IntVector.X"/> and <see cref="IntVector.Y"/> respectively.
        /// </summary>
        public static explicit operator IntVector(Size size)
        {
            var x = (int) size.Width;
            var y = (int) size.Height;

            return new IntVector(x, y);
        }

        /// <summary>
        /// Converts a <see cref="Size"/> structure into <see cref="IntSize"/> by integer truncation.
        /// </summary>
        public static explicit operator IntSize(Size size)
        {
            var width = (int) size.Width;
            var height = (int) size.Height;

            return new IntSize(width, height);
        }

        /// <summary>
        /// Converts a formatted tuple into <see cref="Size"/>.
        /// </summary>
        public static implicit operator Size((float width, float height) size)
        {
            return new Size(size.width, size.height);
        }

        #endregion

        #region Arithmetic Operators

        /// <summary>
        /// Returns the negated version of a size structure.
        /// </summary>
        public static Size operator -(Size v)
        {
            var width = -v.Width;
            var height = -v.Height;
            return new Size(width, height);
        }

        #region Addition / Subtraction

        /// <summary>
        /// Performs the addition of two size structures.
        /// </summary>
        public static Size operator +(Size a, Size b)
        {
            var width = a.Width + b.Width;
            var height = a.Height + b.Height;
            return new Size(width, height);
        }

        /// <summary>
        /// Performs the subtraction of two size structures.
        /// </summary>
        public static Size operator -(Size a, Size b)
        {
            var width = a.Width - b.Width;
            var height = a.Height - b.Height;
            return new Size(width, height);
        }

        #endregion

        #region Multiply / Divide

        /// <summary>
        /// Performs the component-wise multiplication of two size structures.
        /// </summary>
        public static Size operator *(Size a, Size b)
        {
            var width = a.Width * b.Width;
            var height = a.Height * b.Height;

            return new Size(width, height);
        }

        /// <summary>
        /// Performs the component-wise division of two size structures.
        /// </summary>
        public static Size operator /(Size a, Size b)
        {
            var width = a.Width / b.Width;
            var height = a.Height / b.Height;

            return new Size(width, height);
        }

        /// <summary>
        /// Performs the component-wise scaling of a size structure.
        /// </summary>
        public static Size operator *(Size a, float v)
        {
            var width = a.Width * v;
            var height = a.Height * v;
            return new Size(width, height);
        }

        /// <summary>
        /// Performs the component-wise scaling of a size structure.
        /// </summary>
        public static Size operator *(float v, Size a)
        {
            var width = a.Width * v;
            var height = a.Height * v;
            return new Size(width, height);
        }

        /// <summary>
        /// Performs the component-wise scaling of a size structure via division.
        /// </summary>
        public static Size operator /(Size a, float v)
        {
            var width = a.Width / v;
            var height = a.Height / v;
            return new Size(width, height);
        }

        /// <summary>
        /// Performs the component-wise scaling of a size structure via division.
        /// </summary>
        public static Size operator /(float v, Size a)
        {
            var width = v / a.Width;
            var height = v / a.Height;
            return new Size(width, height);
        }

        #endregion

        #endregion

        #region Equality

        /// <summary>
        /// Compares this <see cref="IntSize"/> for equality with another object.
        /// </summary>
        public override bool Equals(object obj)
        {
            return obj is Size size
                && Equals(size);
        }

        /// <summary>
        /// Compares this <see cref="Size"/> for equality with another <see cref="Size"/>.
        /// </summary>
        public bool Equals(Size other)
        {
            return Width == other.Width
                && Height == other.Height;
        }

        /// <summary>
        /// Returns the hash code for this <see cref="Size"/>.
        /// </summary>
        public override int GetHashCode()
        {
            return HashCode.Combine(Width, Height);
        }

        /// <summary>
        /// Compares two instances of <see cref="Size"/> for equality.
        /// </summary>
        public static bool operator ==(Size size1, Size size2)
        {
            return size1.Equals(size2);
        }

        /// <summary>
        /// Compares two instances of <see cref="Size"/> for inequality.
        /// </summary>
        public static bool operator !=(Size size1, Size size2)
        {
            return !(size1 == size2);
        }

        #endregion

        /// <summary>
        /// Converts this <see cref="Size"/> into string representation.
        /// </summary>
        public override string ToString()
        {
            return $"{Width} x {Height}";
        }

        /// <summary>
        /// Converts this <see cref="Size"/> into string representation.
        /// </summary>
        public string ToString(string format, IFormatProvider formatProvider)
        {
            var _Width = Width.ToString(format, formatProvider);
            var _Height = Height.ToString(format, formatProvider);
            return $"{_Width} x {_Height}";
        }
    }
}
