using System;
using System.Runtime.InteropServices;

namespace Heirloom.Math
{
    [StructLayout(LayoutKind.Sequential, Pack = 4)]
    public struct IntVector : IEquatable<IntVector>
    {
        public int X;

        public int Y;

        #region Constants

        public static readonly IntVector Zero = new IntVector(0, 0);

        public static readonly IntVector One = new IntVector(1, 1);

        public static readonly IntVector UnitX = new IntVector(1, 0);

        public static readonly IntVector UnitY = new IntVector(0, 1);

        #endregion

        #region Properties

        public float Length => Calc.Sqrt(LengthSquared);

        public float LengthSquared => X * X + Y * Y;

        #endregion

        #region Constructors

        public IntVector(int x, int y)
        {
            X = x;
            Y = y;
        }

        #endregion

        #region Min / Max

        public static int MaxComponent(IntVector vec)
        {
            return Calc.Max(vec.X, vec.Y);
        }

        public static int MinComponent(IntVector vec)
        {
            return Calc.Min(vec.X, vec.Y);
        }

        public static IntVector Min(IntVector a, IntVector b)
        {
            var x = Calc.Min(a.X, b.X);
            var y = Calc.Min(a.Y, b.Y);

            return new IntVector(x, y);
        }

        public static IntVector Max(IntVector a, IntVector b)
        {
            var x = Calc.Max(a.X, b.X);
            var y = Calc.Max(a.Y, b.Y);

            return new IntVector(x, y);
        }

        #endregion

        #region Distance

        public static float Distance(IntVector a, IntVector b)
        {
            return Distance(in a, in b);
        }

        public static float Distance(in IntVector a, in IntVector b)
        {
            return Calc.Sqrt(DistanceSquared(a, b));
        }

        public static float DistanceSquared(IntVector a, IntVector b)
        {
            return DistanceSquared(in a, in b);
        }

        public static float DistanceSquared(in IntVector a, in IntVector b)
        {
            var dx = a.X - b.X;
            var dy = a.Y - b.Y;

            return dx * dx + dy * dy;
        }

        public static float ManhattanDistance(IntVector a, IntVector b)
        {
            return ManhattanDistance(in a, in b);
        }

        public static float ManhattanDistance(in IntVector a, in IntVector b)
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

        public static implicit operator (int x, int y) (IntVector vec)
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

        public static IntVector operator +(IntVector a, IntVector b)
        {
            var x = a.X + b.X;
            var y = a.Y + b.Y;
            return new IntVector(x, y);
        }

        public static IntVector operator -(IntVector a, IntVector b)
        {
            var x = a.X - b.X;
            var y = a.Y - b.Y;
            return new IntVector(x, y);
        }

        public static Vector operator *(IntVector a, float v)
        {
            var x = a.X * v;
            var y = a.Y * v;
            return new Vector(x, y);
        }

        public static IntVector operator *(IntVector a, int v)
        {
            var x = a.X * v;
            var y = a.Y * v;
            return new IntVector(x, y);
        }

        public static IntVector operator /(IntVector a, int v)
        {
            var x = a.X / v;
            var y = a.Y / v;
            return new IntVector(x, y);
        }

        public static Vector operator *(float v, IntVector a)
        {
            var x = a.X * v;
            var y = a.Y * v;
            return new Vector(x, y);
        }

        public static IntVector operator *(int v, IntVector a)
        {
            var x = a.X * v;
            var y = a.Y * v;
            return new IntVector(x, y);
        }

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
            return X == other.X &&
                   Y == other.Y;
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
            return $"({X}, {Y})";
        }
    }
}
