using System;
using System.Runtime.CompilerServices;
using System.Runtime.InteropServices;

namespace Heirloom.Mathematics
{
    /// <summary>
    /// Represents a vector with two integer values.
    /// </summary>
    /// <category>Mathematics</category>
    [StructLayout(LayoutKind.Sequential, Pack = 4)]
    public struct IntVector : IEquatable<IntVector>, IFormattable
    {
        /// <summary>
        /// The x-component of this vector.
        /// </summary>
        public int X;

        /// <summary>
        /// The y-component of this vector.
        /// </summary>
        public int Y;

        #region Constants

        /// <summary>
        /// A vector with value (0, 0).
        /// </summary>
        public static readonly IntVector Zero = new IntVector(0, 0);

        /// <summary>
        /// A vector with value (1, 1).
        /// </summary>
        public static readonly IntVector One = new IntVector(1, 1);

        /// <summary>
        /// A vector with value (1, 0).
        /// </summary>
        public static readonly IntVector Right = new IntVector(1, 0);

        /// <summary>
        /// A vector with value (0, -1).
        /// </summary>
        public static readonly IntVector Up = new IntVector(0, -1);

        /// <summary>
        /// A vector with value (-1, 0).
        /// </summary>
        public static readonly IntVector Left = new IntVector(-1, 0);

        /// <summary>
        /// A vector with value (0, 1).
        /// </summary>
        public static readonly IntVector Down = new IntVector(0, 1);

        #endregion

        #region Constructors

        /// <summary>
        /// Constructs a new instance of <see cref="IntVector"/>.
        /// </summary>
        /// <param name="x">The x component.</param>
        /// <param name="y">The y component.</param>
        public IntVector(int x, int y)
        {
            X = x;
            Y = y;
        }

        #endregion

        #region Properties

        /// <summary>
        /// Gets the magnitude of this vector.
        /// </summary>
        public float Length => MathF.Sqrt(LengthSquared);

        /// <summary>
        /// Gets the squared magnitude of this vector.
        /// </summary>
        public float LengthSquared => (X * X) + (Y * Y);

        /// <summary>
        /// Gets a perpendicular copy of this vector.
        /// </summary>
        public IntVector Perpendicular => new IntVector(Y, -X);

        #endregion

        #region Indexer

        /// <summary>
        /// Accesses the <see cref="X"/> and <see cref="Y"/> components by numerical index in respective order.
        /// </summary>
        public int this[int i]
        {
            get => i switch
            {
                0 => X,
                1 => Y,
                _ => throw new IndexOutOfRangeException(),
            };

            set
            {
                switch (i)
                {
                    case 0: X = value; break;
                    case 1: Y = value; break;
                    default:
                        throw new IndexOutOfRangeException();
                }
            }
        }

        #endregion

        /// <summary>
        /// Sets the components of this vector.
        /// </summary>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public void Set(int x, int y)
        {
            X = x;
            Y = y;
        }

        #region Min / Max / Abs

        /// <summary>
        /// Gets the maximal component in the input vector.
        /// </summary>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static int GetMaxComponent(IntVector vec)
        {
            return Math.Max(vec.X, vec.Y);
        }

        /// <summary>
        /// Gets the minimal component in the input vector.
        /// </summary>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static int GetMinComponent(IntVector vec)
        {
            return Math.Min(vec.X, vec.Y);
        }

        /// <summary>
        /// Computes a new vector where each component is the minimum component in each respective input vector.
        /// </summary>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static IntVector Min(IntVector a, IntVector b)
        {
            var x = Math.Min(a.X, b.X);
            var y = Math.Min(a.Y, b.Y);

            return new IntVector(x, y);
        }

        /// <summary>
        /// Computes a new vector where each component is the maximum component in each respective input vector.
        /// </summary>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static IntVector Max(IntVector a, IntVector b)
        {
            var x = Math.Max(a.X, b.X);
            var y = Math.Max(a.Y, b.Y);

            return new IntVector(x, y);
        }

        /// <summary>
        /// Computes a new vector where each component is the absolute value of each component in the input vector.
        /// </summary>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static IntVector Abs(IntVector vec)
        {
            var x = Math.Abs(vec.X);
            var y = Math.Abs(vec.Y);

            return new IntVector(x, y);
        }

        #endregion

        #region Distance

        /// <summary>
        /// Computes the euclidean distance between any two vectors.
        /// </summary>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static float Distance(in Vector a, in Vector b)
        {
            return Calc.Distance(a.X, a.Y, b.X, b.Y);
        }

        /// <summary>
        /// Computes the squared euclidean distance between any two vectors.
        /// </summary>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static float DistanceSquared(in Vector a, in Vector b)
        {
            return Calc.DistanceSquared(a.X, a.Y, b.X, b.Y);
        }

        /// <summary>
        /// Computes the manhattan distance between any two vectors.
        /// </summary>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static float ManhattanDistance(in Vector a, in Vector b)
        {
            return Calc.ManhattanDistance(a.X, a.Y, b.X, b.Y);
        }

        #endregion

        #region Deconstruct

        /// <summary>
        /// Deconstructs this <see cref="IntVector"/> into constituent components.
        /// </summary>
        /// <param name="x">Outputs the x component.</param>
        /// <param name="y">Outputs the y component.</param>
        public void Deconstruct(out int x, out int y)
        {
            x = X;
            y = Y;
        }

        #endregion

        #region Conversion Operators

        /// <summary>
        /// Converts an <see cref="IntVector"/> into a <see cref="IntSize"/> by convention of
        /// <see cref="X"/> and <see cref="Y"/> to <see cref="IntSize.Width"/> and <see cref="IntSize.Height"/> respectively.
        /// </summary>
        public static explicit operator IntSize(IntVector vec)
        {
            var width = vec.X;
            var height = vec.Y;

            return new IntSize(width, height);
        }

        /// <summary>
        /// Converts an <see cref="IntVector"/> into a <see cref="Size"/> by convention of
        /// <see cref="X"/> and <see cref="Y"/> to <see cref="Size.Width"/> and <see cref="Size.Height"/> respectively.
        /// </summary>
        public static explicit operator Size(IntVector vec)
        {
            var width = vec.X;
            var height = vec.Y;

            return new Size(width, height);
        }

        /// <summary>
        /// Converts an <see cref="IntVector"/> into <see cref="Vector"/>.
        /// </summary>
        public static implicit operator Vector(IntVector vec)
        {
            var x = vec.X;
            var y = vec.Y;

            return new Vector(x, y);
        }

        /// <summary>
        /// Converts a formatted tuple into an <see cref="IntVector"/>.
        /// </summary>
        public static implicit operator IntVector((int x, int y) vec)
        {
            return new IntVector(vec.x, vec.y);
        }

        #endregion

        #region Arithmetic Operators

        /// <summary>
        /// Returns a negated copy of a vector.
        /// </summary>
        public static IntVector operator -(IntVector v)
        {
            var x = -v.X;
            var y = -v.Y;
            return new IntVector(x, y);
        }

        /// <summary>
        /// Returns a copy of this vector.
        /// </summary>
        public static IntVector operator +(IntVector v)
        {
            return v;
        }

        #region Add

        /// <summary>
        /// Performs the addition of two vectors.
        /// </summary>
        public static IntVector operator +(IntVector a, IntVector b)
        {
            var x = a.X + b.X;
            var y = a.Y + b.Y;
            return new IntVector(x, y);
        }

        #endregion

        #region Subtract

        /// <summary>
        /// Performs the subtraction of two vectors.
        /// </summary>
        public static IntVector operator -(IntVector a, IntVector b)
        {
            var x = a.X - b.X;
            var y = a.Y - b.Y;
            return new IntVector(x, y);
        }

        #endregion

        #region Multiply

        /// <summary>
        /// Performs a component-wise multiplication of two vectors.
        /// </summary>
        public static IntVector operator *(IntVector a, IntVector b)
        {
            var x = a.X * b.X;
            var y = a.Y * b.Y;
            return new IntVector(x, y);
        }

        /// <summary>
        /// Performs a scaling of a vector.
        /// </summary>
        public static IntVector operator *(int v, IntVector a)
        {
            var x = a.X * v;
            var y = a.Y * v;
            return new IntVector(x, y);
        }

        /// <summary>
        /// Performs a scaling of a vector.
        /// </summary>
        public static IntVector operator *(IntVector a, int v)
        {
            var x = a.X * v;
            var y = a.Y * v;
            return new IntVector(x, y);
        }

        /// <summary>
        /// Performs a scaling of a vector.
        /// </summary>
        public static Vector operator *(float v, IntVector a)
        {
            var x = a.X * v;
            var y = a.Y * v;
            return new Vector(x, y);
        }

        /// <summary>
        /// Performs a scaling of a vector.
        /// </summary>
        public static Vector operator *(IntVector a, float v)
        {
            var x = a.X * v;
            var y = a.Y * v;
            return new Vector(x, y);
        }

        #endregion

        #region Divide

        /// <summary>
        /// Performs a scaling of a vector via division.
        /// </summary>
        public static IntVector operator /(IntVector a, int v)
        {
            var x = a.X / v;
            var y = a.Y / v;
            return new IntVector(x, y);
        }

        /// <summary>
        /// Performs a scaling of a vector via division.
        /// </summary>
        public static Vector operator /(float v, IntVector b)
        {
            var x = v / b.X;
            var y = v / b.Y;
            return new Vector(x, y);
        }

        #endregion

        #endregion

        #region Comparison Operators

        /// <summary>
        /// Compares two vectors for equality.
        /// </summary>
        public static bool operator ==(IntVector vector1, IntVector vector2)
        {
            return vector1.Equals(vector2);
        }

        /// <summary>
        /// Compares two vectors for inequality.
        /// </summary>
        public static bool operator !=(IntVector vector1, IntVector vector2)
        {
            return !(vector1 == vector2);
        }

        #endregion

        #region Equality

        /// <summary>
        /// Compares this <see cref="IntVector"/> for equality with another object.
        /// </summary>
        public override bool Equals(object obj)
        {
            return obj is IntVector vec
                && Equals(vec);
        }

        /// <summary>
        /// Compares this <see cref="IntVector"/> for equality with another <see cref="IntVector"/>.
        /// </summary>
        public bool Equals(IntVector other)
        {
            return X == other.X
                && Y == other.Y;
        }

        /// <summary>
        /// Returns the hash code for this <see cref="IntVector"/>.
        /// </summary>
        /// <returns></returns>
        public override int GetHashCode()
        {
            return HashCode.Combine(X, Y);
        }

        #endregion

        /// <summary>
        /// Converts this <see cref="IntVector"/> into string representation.
        /// </summary>
        public override string ToString()
        {
            return $"({X}, {Y})";
        }

        /// <summary>
        /// Converts this <see cref="IntVector"/> into string representation.
        /// </summary>
        public string ToString(string format, IFormatProvider formatProvider)
        {
            var _X = X.ToString(format, formatProvider);
            var _Y = Y.ToString(format, formatProvider);
            return $"({_X}, {_Y})";
        }
    }
}
