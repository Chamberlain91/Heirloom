using System;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using System.Runtime.InteropServices;

namespace Heirloom.Mathematics
{
    /// <summary>
    /// Represents a rectangle defined with integer coordinates.
    /// </summary>
    /// <category>Mathematics</category>
    [StructLayout(LayoutKind.Sequential, Pack = 4)]
    public struct IntRectangle : IEquatable<IntRectangle>, IFormattable
    {
        /// <summary>
        /// The x-coordinate of this rectangle.
        /// </summary>
        public int X;

        /// <summary>
        /// The y-coordinate of this rectangle.
        /// </summary>
        public int Y;

        /// <summary>
        /// The width of this rectangle.
        /// </summary>
        public int Width;

        /// <summary>
        /// The height of this rectangle.
        /// </summary>
        public int Height;

        #region Constants

        /// <summary>
        /// A rectangle that spans the entire 2D integer plane (but inverted, with min and max reversed).
        /// </summary>
        public static IntRectangle InvertedInfinite { get; } = new IntRectangle(IntVector.One * int.MaxValue, IntVector.One * int.MinValue);

        /// <summary>
        /// A rectangle that spans the entire 2D integer plane.
        /// </summary>
        public static IntRectangle Infinite { get; } = new IntRectangle(IntVector.One * int.MinValue, IntVector.One * int.MaxValue);

        /// <summary>
        /// A 1x1 rectangle that is positioned at the origin.
        /// </summary>
        public static IntRectangle One { get; } = new IntRectangle(0, 0, 1, 1);

        /// <summary>
        /// A 0x0 rectangle that is positioned at the origin.
        /// </summary>
        public static IntRectangle Zero { get; } = new IntRectangle(0, 0, 0, 0);

        #endregion

        #region Constructors

        /// <summary>
        /// Constructs a new instance of <see cref="IntRectangle"/>.
        /// </summary>
        /// <param name="x">The x position of the rectangle.</param>
        /// <param name="y">The y position of the rectangle.</param>
        /// <param name="width">The width of the rectangle.</param>
        /// <param name="height">The height of the rectangle.</param>
        public IntRectangle(int x, int y, int width, int height)
        {
            X = x;
            Y = y;
            Width = width;
            Height = height;
        }

        /// <summary>
        /// Constructs a new instance of <see cref="IntRectangle"/>.
        /// </summary>
        /// <param name="position">The position of the rectangle.</param>
        /// <param name="size">The size of the rectangle.</param>
        public IntRectangle(IntVector position, IntSize size)
            : this(position.X, position.Y, size.Width, size.Height)
        { }

        /// <summary>
        /// Constructs a new instance of <see cref="IntRectangle"/> using minimum and maximum points.
        /// </summary>
        /// <param name="min">The minimum point.</param>
        /// <param name="max">The maximum point.</param>
        public IntRectangle(IntVector min, IntVector max)
            : this(min, (IntSize) (max - min))
        { }

        #endregion

        #region Properties

        /// <summary>
        /// Gets the area of this rectangle.
        /// </summary>
        public int Area => Width * Height;

        /// <summary>
        /// Gets or sets the size of this rectangle.
        /// </summary>
        public IntSize Size
        {
            get => new IntSize(Width, Height);

            set
            {
                Width = value.Width;
                Height = value.Height;
            }
        }

        /// <summary>
        /// Gets or sets the position of this rectangle.
        /// </summary>
        public IntVector Position
        {
            get => TopLeft;

            set
            {
                X = value.X;
                Y = value.Y;

                Width = Max.X - X;
                Height = Max.Y - Y;
            }
        }

        /// <summary>
        /// Gets or sets the center position of this rectangle.
        /// </summary>
        public IntVector Center
        {
            get => (Min + Max) / 2;
            set => Position = new IntVector(value.X - Width / 2, value.Y - Height / 2);
        }

        /// <summary>
        /// Gets the minimum corner of this rectangle.
        /// </summary>
        public IntVector Min => TopLeft;

        /// <summary>
        /// Gets the maximum corner of this rectangle.
        /// </summary>
        public IntVector Max => BottomRight;

        /// <summary>
        /// Gets the left extent of this rectangle.
        /// </summary>
        public int Left => X;

        /// <summary>
        /// Gets the top extent of this rectangle.
        /// </summary>
        public int Top => Y;

        /// <summary>
        /// Gets the right extent of this rectangle.
        /// </summary>
        public int Right => X + Width;

        /// <summary>
        /// Gets the bottom extent of this rectangle.
        /// </summary>
        public int Bottom => Y + Height;

        /// <summary>
        /// Gets the top left corner of this rectangle.
        /// </summary>
        public IntVector TopLeft => new IntVector(X, Y); // Min

        /// <summary>
        /// Gets the bottom left corner of this rectangle.
        /// </summary>
        public IntVector BottomLeft => new IntVector(X, Bottom);

        /// <summary>
        /// Gets the bottom right corner of this rectangle.
        /// </summary>
        public IntVector BottomRight => new IntVector(Right, Bottom); // Max

        /// <summary>
        /// Gets the top right corner of this rectangle.
        /// </summary>
        public IntVector TopRight => new IntVector(Right, Y);

        /// <summary>
        /// Determines if the values of this rectangle are considered to be valid or in other words
        /// that left &lt; right and top &lt; bottom.
        /// </summary>
        public bool IsValid => Left < Right && Top < Bottom;

        #endregion

        /// <summary>
        /// Sets the components of this rectangle.
        /// </summary>
        /*[MethodImpl(MethodImplOptions.AggressiveInlining)]*/
        public void Set(int x, int y, int w, int h)
        {
            X = x;
            Y = y;
            Width = w;
            Height = h;
        }

        /// <summary>
        /// Create a polygon from this rectangle.
        /// </summary>
        public Polygon ToPolygon()
        {
            return ((Rectangle) this).ToPolygon();
        }

        #region Transform (Offset)

        /// <summary>
        /// Translates this rectangle.
        /// </summary>
        public void Offset(int x, int y)
        {
            X += x;
            Y += y;
        }

        /// <summary>
        /// Translates this rectangle.
        /// </summary>
        public void Offset(IntVector offset)
        {
            Offset(offset.X, offset.Y);
        }

        /// <summary>
        /// Copies and translates the given rectangle.
        /// </summary>
        public static IntRectangle Offset(IntRectangle rect, int x, int y)
        {
            rect.X += x;
            rect.Y += y;

            return rect;
        }

        /// <summary>
        /// Copies and translates the given rectangle.
        /// </summary>
        public static IntRectangle Offset(IntRectangle rect, IntVector offset)
        {
            return Offset(rect, offset.X, offset.Y);
        }

        #endregion

        #region Include (Point, Rectangle)

        /// <summary>
        /// Mutates this rectangle to accommodate the given point.
        /// </summary>
        /// <remarks>
        /// Useful for computing a bounding rectangle.
        /// </remarks>
        /// <param name="point">Some point to include.</param>
        public void Include(IntVector point)
        {
            var min = IntVector.Min(point, Min);
            var max = IntVector.Max(point, Max);
            this = new IntRectangle(min, max);
        }

        /// <summary>
        /// Mutates this rectangle to accommodate the given rectangle.
        /// </summary>
        /// <remarks>
        /// Useful for computing a bounding rectangle.
        /// </remarks>
        /// <param name="rect">Some rectangle to include.</param>
        public void Include(IntRectangle rect)
        {
            this = Merge(this, rect);
        }

        #endregion

        #region Merge (Rectangles)

        /// <summary>
        /// Merges the given rectangles into one potentially larger rectangle.
        /// </summary>
        /// <remarks>
        /// Useful for computing a bounding rectangle.
        /// </remarks>
        /// <param name="a">Some rectangle '<paramref name="a"/>'.</param>
        /// <param name="b">Some rectangle '<paramref name="b"/>'.</param>
        /// <returns> A potentially larger rectangle comprised of the two given. </returns>
        /*[MethodImpl(MethodImplOptions.AggressiveInlining)]*/
        public static IntRectangle Merge(IntRectangle a, IntRectangle b)
        {
            var minX = Calc.Min(a.X, b.X);
            var minY = Calc.Min(a.Y, b.Y);

            var maxX = Calc.Max(a.X + a.Width, b.X + b.Width);
            var maxY = Calc.Max(a.Y + a.Height, b.Y + b.Height);

            return new IntRectangle(minX, minY, maxX - minX, maxY - minY);
        }

        /// <summary>
        /// Merges the given rectangles into one potentially larger rectangle.
        /// </summary>
        /// <remarks>
        /// Useful for computing a bounding rectangle.
        /// </remarks>
        /// <param name="rects">A collection of rectangles to merge.</param>
        /// <returns> A potentially larger rectangle comprised of the two given. </returns>
        /*[MethodImpl(MethodImplOptions.AggressiveInlining)]*/
        public static IntRectangle Merge(params IntRectangle[] rects)
        {
            var min = rects[0].Min;
            var max = rects[0].Max;

            for (var i = 1; i < rects.Length; i++)
            {
                var b = rects[i];
                min = IntVector.Min(min, b.Min);
                max = IntVector.Max(max, b.Max);
            }

            return new IntRectangle(min, max);
        }

        #endregion

        #region Inflate

        /// <summary>
        /// Expands (or shrinks) the rectangle by a factor on both axis.
        /// </summary>
        public void Inflate(int factor)
        {
            this = Inflate(this, factor);
        }

        /// <summary>
        /// Expands (or shrinks) the rectangle by a factor on each axis.
        /// </summary>
        public void Inflate(int xFactor, int yFactor)
        {
            this = Inflate(this, xFactor, yFactor);
        }

        /// <summary>
        /// Expands (or shrinks) the input rectangle by a factor on both axis.
        /// </summary>
        public static IntRectangle Inflate(IntRectangle rect, int factor)
        {
            return Inflate(rect, factor, factor);
        }

        /// <summary>
        /// Expands (or shrinks) the input rectangle by a factor on each axis.
        /// </summary>
        public static IntRectangle Inflate(IntRectangle rect, IntVector factor)
        {
            return Inflate(rect, factor.X, factor.Y);
        }

        /// <summary>
        /// Expands (or shrinks) the input rectangle by a factor on each axis.
        /// </summary>
        public static IntRectangle Inflate(IntRectangle rect, int xFactor, int yFactor)
        {
            rect.X -= xFactor;
            rect.Y -= yFactor;
            rect.Width += xFactor * 2;
            rect.Height += yFactor * 2;

            return rect;
        }

        #endregion

        #region Create (Point Cloud)

        /// <summary>
        /// Computes the bounding rectangle of the given set of points.
        /// </summary>
        /*[MethodImpl(MethodImplOptions.AggressiveInlining)]*/
        public static IntRectangle FromPoints(params IntVector[] points)
        {
            return FromPoints((IEnumerable<IntVector>) points);
        }

        /// <summary>
        /// Computes the bounding rectangle of the given set of points.
        /// </summary>
        /*[MethodImpl(MethodImplOptions.AggressiveInlining)]*/
        public static IntRectangle FromPoints(IEnumerable<IntVector> points)
        {
            var b = InvertedInfinite;
            foreach (var v in points) { b.Include(v); }
            return b;
        }

        #endregion

        #region Nearest Point

        /// <summary>
        /// Returns the nearest point on the rectangle to the given point.
        /// </summary>
        public IntVector GetNearestPoint(IntVector point)
        {
            const int side_top = 0;
            const int side_right = 1;
            const int side_bottom = 2;
            const int side_left = 3;

            if (Contains(point))
            {
                // Inside rectangle, snap nearest edge
                // todo: is there a better implementation?

                var side = side_top;
                var best = int.MaxValue;
                CheckEdge(ref best, point.X - Left, ref side, side_left);
                CheckEdge(ref best, Right - point.X, ref side, side_right);
                CheckEdge(ref best, point.Y - Top, ref side, side_top);
                CheckEdge(ref best, Bottom - point.Y, ref side, side_bottom);

                return side switch
                {
                    side_left => new IntVector(Left, point.Y),
                    side_right => new IntVector(Right, point.Y),
                    side_bottom => new IntVector(point.X, Bottom),
                    side_top => new IntVector(point.X, Top),
                    _ => throw new InvalidOperationException(),
                };
            }
            else
            {
                // Outside rectangle, clamp edges
                point.X = (point.X < Min.X) ? Min.X : (point.X > Max.X) ? Max.X : point.X;
                point.Y = (point.Y < Min.Y) ? Min.Y : (point.Y > Max.Y) ? Max.Y : point.Y;
                return point;
            }

            static void CheckEdge(ref int best, int dist, ref int side, int edge)
            {
                if (dist < best)
                {
                    best = dist;
                    side = edge;
                }
            }
        }

        #endregion

        #region Contains (Point, Rectangle)

        /// <summary>
        /// Determines if this rectangle contains the given point?
        /// </summary>
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

        /// <summary>
        /// Determines if this rectangle contains the given point?
        /// </summary>
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
        /// Determines if this rectangle contains another rectangle?
        /// </summary>
        public bool Contains(IntRectangle other)
        {
            if (other.Right > Right || other.Left < Left) { return false; }
            if (other.Top < Top || other.Bottom > Bottom) { return false; }
            return true;
        }

        #endregion

        #region Overlaps

        /// <summary>
        /// Determines if this rectangle overlaps another rectangle.
        /// </summary>
        public bool Overlaps(IntRectangle other)
        {
            /*
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

        #endregion

        #region Deconstruct

        /// <summary>
        /// Deconstructs this rectangle into consituent components.
        /// </summary>
        /// <param name="x">The x position of the rectangle.</param>
        /// <param name="y">The y position of the rectangle.</param>
        /// <param name="w">The width of the rectangle.</param>
        /// <param name="h">The height of the rectangle.</param>
        public void Deconstruct(out int x, out int y, out int w, out int h)
        {
            x = X;
            y = Y;
            w = Width;
            h = Height;
        }

        /// <summary>
        /// Deconstructs this rectangle into consituent parts.
        /// </summary>
        /// <param name="position">The position of the rectangle.</param>
        /// <param name="size">The size of the rectangle.</param>
        public void Deconstruct(out IntVector position, out IntSize size)
        {
            position = Position;
            size = Size;
        }

        #endregion

        #region Conversion Operators

        /// <summary>
        /// Converts a <see cref="IntRectangle"/> into <see cref="Rectangle"/>.
        /// </summary>
        public static implicit operator Rectangle(IntRectangle rect)
        {
            var x = rect.X;
            var y = rect.Y;
            var w = rect.Width;
            var h = rect.Height;

            return new Rectangle(x, y, w, h);
        }

        /// <summary>
        /// Converts a rectangle formatted tuple into <see cref="IntRectangle"/>.
        /// </summary>
        public static implicit operator IntRectangle((int x, int y, int width, int height) rect)
        {
            return new IntRectangle(rect.x, rect.y, rect.width, rect.height);
        }

        /// <summary>
        /// Converts a rectangle formatted tuple into <see cref="IntRectangle"/>.
        /// </summary>
        public static implicit operator IntRectangle((IntVector position, IntSize size) rect)
        {
            return new IntRectangle(rect.position, rect.size);
        }

        #endregion

        #region Comparison Operators

        /// <summary>
        /// Compares two rectangles for equality.
        /// </summary>
        public static bool operator ==(IntRectangle a, IntRectangle b)
        {
            return a.Equals(b);
        }

        /// <summary>
        /// Compares two rectangles for inequality.
        /// </summary>
        public static bool operator !=(IntRectangle a, IntRectangle b)
        {
            return !(a == b);
        }

        #endregion

        #region Equality

        /// <summary>
        /// Compares this rectangle for equalty with another object.
        /// </summary>
        public override bool Equals(object obj)
        {
            return obj is IntRectangle rect
                && Equals(rect);
        }

        /// <summary>
        /// Compares this rectangle for equalty with another rectangle.
        /// </summary>
        public bool Equals(IntRectangle b)
        {
            return X == b.X
                && Y == b.Y
                && Width == b.Width
                && Height == b.Height;
        }

        /// <summary>
        /// Returns the hash code for this rectangle.
        /// </summary>
        public override int GetHashCode()
        {
            return HashCode.Combine(X, Y, Width, Height);
        }

        #endregion

        /// <summary>
        /// Converts this <see cref="IntRectangle"/> into string representation.
        /// </summary>
        public override string ToString()
        {
            return $"({Position}, {Size})";
        }

        /// <summary>
        /// Converts this <see cref="IntRectangle"/> into string representation.
        /// </summary>
        public string ToString(string format, IFormatProvider formatProvider)
        {
            var _Position = Position.ToString(format, formatProvider);
            var _Size = Size.ToString(format, formatProvider);
            return $"({_Position}, {_Size})";
        }
    }
}
