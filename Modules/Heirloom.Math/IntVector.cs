using System;
using System.Runtime.CompilerServices;
using System.Runtime.InteropServices;

namespace Heirloom.Math
{
    /// <summary>
    /// Represents a vector with two integer values.
    /// </summary>
    [StructLayout(LayoutKind.Sequential, Pack = 4)]
    public struct IntVector : IEquatable<IntVector>
    {
        /// <summary>
        /// The x-coordinate of this vector.
        /// </summary>
        public int X;

        /// <summary>
        /// The y-coordinate of this vector.
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
        public float Length => Calc.Sqrt(LengthSquared);

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
            return Calc.Max(vec.X, vec.Y);
        }

        /// <summary>
        /// Gets the minimal component in the input vector.
        /// </summary>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static int GetMinComponent(IntVector vec)
        {
            return Calc.Min(vec.X, vec.Y);
        }

        /// <summary>
        /// Computes a new vector where each component is the minimum component in each respective input vector.
        /// </summary>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static IntVector Min(IntVector a, IntVector b)
        {
            var x = Calc.Min(a.X, b.X);
            var y = Calc.Min(a.Y, b.Y);

            return new IntVector(x, y);
        }

        /// <summary>
        /// Computes a new vector where each component is the maximum component in each respective input vector.
        /// </summary>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static IntVector Max(IntVector a, IntVector b)
        {
            var x = Calc.Max(a.X, b.X);
            var y = Calc.Max(a.Y, b.Y);

            return new IntVector(x, y);
        }

        /// <summary>
        /// Computes a new vector where each component is the absolute value of each component in the input vector.
        /// </summary>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static IntVector Abs(IntVector vec)
        {
            var x = Calc.Abs(vec.X);
            var y = Calc.Abs(vec.Y);

            return new IntVector(x, y);
        }

        #endregion

        #region Distance

        /// <summary>
        /// Computes the euclidean distance between any two vectors.
        /// </summary>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static float Distance(in IntVector a, in IntVector b)
        {
            return Calc.Sqrt(DistanceSquared(a, b));
        }

        /// <summary>
        /// Computes the squared euclidean distance between any two vectors.
        /// </summary>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static int DistanceSquared(in IntVector a, in IntVector b)
        {
            var dx = a.X - b.X;
            var dy = a.Y - b.Y;

            return (dx * dx) + (dy * dy);
        }

        /// <summary>
        /// Computes the manhattan distance between any two vectors.
        /// </summary>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static int ManhattanDistance(in IntVector a, in IntVector b)
        {
            var dx = Calc.Abs(a.X - b.X);
            var dy = Calc.Abs(a.Y - b.Y);
            return dx + dy;
        }

        #endregion

        #region Deconstruct

        public void Deconstruct(out int x, out int y)
        {
            x = X;
            y = Y;
        }

        #endregion

        #region Conversion Operators

        public static explicit operator IntSize(IntVector vec)
        {
            var width = vec.X;
            var height = vec.Y;

            return new IntSize(width, height);
        }

        public static explicit operator Size(IntVector vec)
        {
            var width = vec.X;
            var height = vec.Y;

            return new Size(width, height);
        }

        public static implicit operator Vector(IntVector vec)
        {
            var x = vec.X;
            var y = vec.Y;

            return new Vector(x, y);
        }

        public static implicit operator (int x, int y)(IntVector vec)
        {
            return (vec.X, vec.Y);
        }

        public static implicit operator IntVector((int x, int y) vec)
        {
            return new IntVector(vec.x, vec.y);
        }

        #endregion

        #region Arithmetic Operators

        public static IntVector operator -(IntVector v)
        {
            var x = -v.X;
            var y = -v.Y;
            return new IntVector(x, y);
        }

        public static IntVector operator +(IntVector v)
        {
            return v;
        }

        #region Add

        public static IntVector operator +(IntVector a, IntVector b)
        {
            var x = a.X + b.X;
            var y = a.Y + b.Y;
            return new IntVector(x, y);
        }

        #endregion

        #region Subtract

        public static IntVector operator -(IntVector a, IntVector b)
        {
            var x = a.X - b.X;
            var y = a.Y - b.Y;
            return new IntVector(x, y);
        }

        #endregion

        #region Multiply

        public static IntVector operator *(IntVector a, IntVector b)
        {
            var x = a.X * b.X;
            var y = a.Y * b.Y;
            return new IntVector(x, y);
        }

        public static IntVector operator *(int v, IntVector a)
        {
            var x = a.X * v;
            var y = a.Y * v;
            return new IntVector(x, y);
        }

        public static IntVector operator *(IntVector a, int v)
        {
            var x = a.X * v;
            var y = a.Y * v;
            return new IntVector(x, y);
        }

        public static Vector operator *(float v, IntVector a)
        {
            var x = a.X * v;
            var y = a.Y * v;
            return new Vector(x, y);
        }

        public static Vector operator *(IntVector a, float v)
        {
            var x = a.X * v;
            var y = a.Y * v;
            return new Vector(x, y);
        }

        #endregion

        #region Divide

        public static IntVector operator /(IntVector a, int v)
        {
            var x = a.X / v;
            var y = a.Y / v;
            return new IntVector(x, y);
        }

        public static Vector operator /(float v, IntVector b)
        {
            var x = v / b.X;
            var y = v / b.Y;
            return new Vector(x, y);
        }

        #endregion

        #endregion

        #region Comparison Operators

        public static bool operator ==(IntVector vector1, IntVector vector2)
        {
            return vector1.Equals(vector2);
        }

        public static bool operator !=(IntVector vector1, IntVector vector2)
        {
            return !(vector1 == vector2);
        }

        #endregion

        #region Equality

        public override bool Equals(object obj)
        {
            return obj is IntVector vec
                && Equals(vec);
        }

        public bool Equals(IntVector other)
        {
            return X == other.X
                && Y == other.Y;
        }

        public override int GetHashCode()
        {
            var hashCode = 1861411795;
            hashCode = hashCode * -1521134295 + X.GetHashCode();
            hashCode = hashCode * -1521134295 + Y.GetHashCode();
            return hashCode;
        }

        #endregion

        public override string ToString()
        {
            return $"{X}, {Y}";
        }
    }
}
