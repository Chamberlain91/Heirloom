using System;
using System.Runtime.InteropServices;

namespace Heirloom.Math
{
    [StructLayout(LayoutKind.Sequential, Pack = 4)]
    public struct IntRectangle : IEquatable<IntRectangle>
    {
        public int X;

        public int Y;

        public int Width;

        public int Height;

        #region Constants

        public static IntRectangle InvertedInfinite { get; } = new IntRectangle(IntVector.One * int.MaxValue, IntVector.One * int.MinValue);

        public static IntRectangle Infinite { get; } = new IntRectangle(IntVector.One * int.MinValue, IntVector.One * int.MaxValue);

        public static IntRectangle Zero { get; } = new IntRectangle(0, 0, 0, 0);

        #endregion

        #region Properties

        public int Area => Width * Height;

        public IntSize Size
        {
            get => new IntSize(Width, Height);

            set
            {
                Width = value.Width;
                Height = value.Height;
            }
        }

        public IntVector Position
        {
            get => new IntVector(X, Y);

            set
            {
                X = value.X;
                Y = value.Y;

                Width = Max.X - X;
                Height = Max.Y - Y;
            }
        }

        public IntVector Center
        {
            get => (Min + Max) / 2;
            set => Position = new IntVector(value.X - Width / 2, value.Y - Height / 2);
        }

        public IntVector Min => Position;

        public IntVector Max => Position + (IntVector) Size;

        public int Left => X;

        public int Top => Y;

        public int Right => X + Width;

        public int Bottom => Y + Height;

        /// <summary>
        /// Determines if the values of this rectangle are considered to be valid or
        /// that left is less than right and top is less than bottom.
        /// </summary>
        public bool IsValid => Left < Right && Top < Bottom;

        #endregion

        #region Constructors

        public IntRectangle(int x, int y, int width, int height)
        {
            X = x;
            Y = y;
            Width = width;
            Height = height;
        }

        public IntRectangle(IntVector position, IntSize size)
            : this(position.X, position.Y, size.Width, size.Height)
        { }

        public IntRectangle(IntVector min, IntVector max)
            : this(min, (IntSize) (max - min))
        { }

        #endregion

        #region Bounds Inclusion

        /// <summary>
        /// Mutates this rectangle to accommodate the given point.
        /// Useful for computing the size of a bounding rectangle.
        /// </summary>
        /// <param name="point">Some point to include.</param>
        public void Include(IntVector point)
        {
            var min = IntVector.Min(point, Min);
            var max = IntVector.Max(point, Max);
            this = new IntRectangle(min, max);
        }

        /// <summary>
        /// Mutates this rectangle to accommodate the given rectangle.
        /// Useful for computing the size of a bounding rectangle.
        /// </summary>
        /// <param name="rect">Some rectangle to include.</param>
        public void Include(IntRectangle rect)
        {
            var min = IntVector.Min(rect.Min, Min);
            var max = IntVector.Max(rect.Max, Max);
            this = new IntRectangle(min, max);
        }

        /// <summary>
        /// Merges the given rectangles int one potentially larger rectangle.
        /// Useful for computing a new bounding rectangle that fits the given two.
        /// </summary>
        /// <param name="a">Some rectangle '<paramref name="a"/>'.</param>
        /// <param name="b">Some rectangle '<paramref name="b"/>'.</param>
        /// <returns> A potentially larger rectangle comprised of the two given. </returns>
        public static IntRectangle Merge(in IntRectangle a, in IntRectangle b)
        {
            a.Include(b);
            return a;
        }

        #endregion

        #region Contains / Overlaps

        public bool Contains(Vector point)
        {
            var xMax = X + Width;
            var yMax = Y + Height;

            if (point.X < X) { return false; }
            if (point.X >= xMax) { return false; }

            if (point.Y < Y) { return false; }
            if (point.Y >= yMax) { return false; }

            return true;
        }

        public bool Contains(IntVector point)
        {
            var xMax = X + Width;
            var yMax = Y + Height;

            if (point.X < X) { return false; }
            if (point.X >= xMax) { return false; }

            if (point.Y < Y) { return false; }
            if (point.Y >= yMax) { return false; }

            return true;
        }

        /// <summary>
        /// Does this rectangle contain the given rectangle?
        /// </summary>
        public bool Contains(in IntRectangle other)
        {
            if (other.Right > Right || other.Left < Left) { return false; }
            if (other.Top < Top || other.Bottom > Bottom) { return false; }
            return true;
        }

        public bool Overlaps(IntRectangle other)
        {
            /**
             * .---.
             * | A |--.
             * `---`B |
             *    `---`
             */

            // Optimized Separating Axis
            if (Right < other.Left) { return false; }
            if (Left > other.Right) { return false; }
            if (Bottom < other.Top) { return false; }
            if (Top > other.Bottom) { return false; }

            // Overlapping
            return true;
        }

        public IntVector ClosestPoint(IntVector point)
        {
            IntVector closest;
            closest.X = (point.X < Position.X) ? Position.X : (point.X > Max.X) ? Max.X : point.X;
            closest.Y = (point.Y < Position.Y) ? Position.Y : (point.Y > Max.Y) ? Max.Y : point.Y;
            return closest;
        }

        #endregion

        public IntRectangle Inflate(int factor)
        {
            var r = this;

            r.X -= factor;
            r.Y -= factor;
            r.Width += factor * 2;
            r.Height += factor * 2;

            return r;
        }

        #region Deconstruct

        public void Deconstruct(out int x, out int y, out int w, out int h)
        {
            x = X;
            y = Y;
            w = Width;
            h = Height;
        }

        public void Deconstruct(out IntVector position, out IntSize size)
        {
            position = Position;
            size = Size;
        }

        #endregion

        #region Conversion Operators

        public static implicit operator Rectangle(IntRectangle rect)
        {
            var x = rect.X;
            var y = rect.Y;
            var w = rect.Width;
            var h = rect.Height;

            return new Rectangle(x, y, w, h);
        }

        public static implicit operator IntRectangle((int x, int y, int width, int height) rect)
        {
            return new IntRectangle(rect.x, rect.y, rect.width, rect.height);
        }

        public static implicit operator IntRectangle((IntVector position, IntSize size) rect)
        {
            return new IntRectangle(rect.position, rect.size);
        }

        public static implicit operator (int x, int y, int width, int height)(IntRectangle rect)
        {
            return (rect.X, rect.Y, rect.Width, rect.Height);
        }

        #endregion

        #region Comparison Operators

        public static bool operator ==(IntRectangle a, IntRectangle b)
        {
            return a.Equals(b);
        }

        public static bool operator !=(IntRectangle a, IntRectangle b)
        {
            return !(a == b);
        }

        #endregion

        #region Equality

        public override bool Equals(object obj)
        {
            return obj is IntRectangle rect
                && Equals(rect);
        }

        public bool Equals(IntRectangle b)
        {
            return X == b.X
                && Y == b.Y
                && Width == b.Width
                && Height == b.Height;
        }

        public override int GetHashCode()
        {
            var hashCode = 466501756;
            hashCode = hashCode * -1521134295 + X.GetHashCode();
            hashCode = hashCode * -1521134295 + Y.GetHashCode();
            hashCode = hashCode * -1521134295 + Width.GetHashCode();
            hashCode = hashCode * -1521134295 + Height.GetHashCode();
            return hashCode;
        }

        #endregion

        public override string ToString()
        {
            return $"({Position}, {Size})";
        }
    }
}
