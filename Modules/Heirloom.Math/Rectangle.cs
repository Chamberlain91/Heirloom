using System;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using System.Runtime.InteropServices;

namespace Heirloom.Math
{
    [StructLayout(LayoutKind.Sequential, Pack = 4)]
    public struct Rectangle : IShape, IEquatable<Rectangle>
    {
        public float X;

        public float Y;

        public float Width;

        public float Height;

        [ThreadStatic]
        private static readonly Vector[][] _polygon = new Vector[2][] { new Vector[4], new Vector[4] };

        #region Constants

        /// <summary>
        /// A rectangle that spans the entire 2D plane (inverted, with min and max reversed).
        /// </summary>
        public static Rectangle InvertedInfinite { get; } = new Rectangle(Vector.One * float.MaxValue, Vector.One * float.MinValue);

        /// <summary>
        /// A rectangle that spans the entire 2D plane.
        /// </summary>
        public static Rectangle Infinite { get; } = new Rectangle(Vector.One * float.MinValue, Vector.One * float.MaxValue);

        public static Rectangle One { get; } = new Rectangle(0, 0, 1, 1);

        public static Rectangle Zero { get; } = new Rectangle(0, 0, 0, 0);

        #endregion

        #region Constructors

        public Rectangle(float x, float y, float width, float height)
        {
            X = x;
            Y = y;
            Width = width;
            Height = height;
        }

        public Rectangle(Vector position, Size size)
            : this(position.X, position.Y, size.Width, size.Height)
        { }

        public Rectangle(Vector min, Vector max)
            : this(min, (Size) (max - min))
        { }

        #endregion

        #region Properties

        Rectangle IShape.Bounds => this;

        public float Area => Width * Height;

        public Size Size
        {
            get => new Size(Width, Height);

            set
            {
                Width = value.Width;
                Height = value.Height;
            }
        }

        public Vector Position
        {
            get => new Vector(X, Y);

            set
            {
                X = value.X;
                Y = value.Y;
            }
        }

        public Vector Center
        {
            get => (Min + Max) / 2F;
            set => Position = new Vector(value.X - Width / 2F, value.Y - Height / 2F);
        }

        public Vector Min => new Vector(X, Y);

        public Vector Max => new Vector(X + Width, Y + Height);

        public float Left => X;

        public float Top => Y;

        public float Right => X + Width;

        public float Bottom => Y + Height;

        public Vector TopLeft => new Vector(X, Y); // Min

        public Vector BottomLeft => new Vector(X, Bottom);

        public Vector BottomRight => new Vector(Right, Bottom); // Max

        public Vector TopRight => new Vector(Right, Y);

        /// <summary>
        /// Determines if the values of this rectangle are considered to be valid or
        /// that left is less than right and top is less than bottom.
        /// </summary>
        public bool IsValid => Left < Right && Top < Bottom;

        #endregion

        #region Merge (Rectangles)

        /// <summary>
        /// Mutates this rectangle to accommodate the given rectangle.
        /// Useful for computing the size of a bounding rectangle.
        /// </summary>
        /// <param name="rect">Some rectangle to include.</param>
        public void Merge(Rectangle rect)
        {
            this = Merge(this, rect);
        }

        /// <summary>
        /// Merges the given rectangles into one potentially larger rectangle.
        /// Useful for computing a new bounding rectangle that fits the given two.
        /// </summary>
        /// <param name="a">Some rectangle '<paramref name="a"/>'.</param>
        /// <param name="b">Some rectangle '<paramref name="b"/>'.</param>
        /// <returns> A potentially larger rectangle comprised of the two given. </returns>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Rectangle Merge(in Rectangle a, in Rectangle b)
        {
            var min = Vector.Min(a.Min, b.Min);
            var max = Vector.Max(a.Max, b.Max);
            return new Rectangle(min, max);
        }

        /// <summary>
        /// Merges the given rectangles into one potentially larger rectangle.
        /// Useful for computing a new bounding rectangle that fits the given two.
        /// </summary>
        /// <param name="rects">A collection of rectangles to merge.</param>
        /// <returns> A potentially larger rectangle comprised of the two given. </returns>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Rectangle Merge(params Rectangle[] rects)
        {
            var min = rects[0].Min;
            var max = rects[0].Max;

            for (var i = 1; i < rects.Length; i++)
            {
                var b = rects[i];
                min = Vector.Min(min, b.Min);
                max = Vector.Max(max, b.Max);
            }

            return new Rectangle(min, max);
        }

        #endregion

        #region Include (Points)

        /// <summary>
        /// Mutates this rectangle to accommodate the given point.
        /// Useful for computing the size of a bounding rectangle.
        /// </summary>
        /// <param name="point">Some point to include.</param>
        public void Include(Vector point)
        {
            var min = Vector.Min(point, Min);
            var max = Vector.Max(point, Max);

            this = new Rectangle(min, max);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Rectangle FromPoints(params Vector[] points)
        {
            return FromPoints((IEnumerable<Vector>) points);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Rectangle FromPoints(IEnumerable<Vector> points)
        {
            var b = InvertedInfinite;
            foreach (var v in points) { b.Include(v); }
            return b;
        }

        #endregion

        public Rectangle Transform(in Matrix matrix)
        {
            var v0 = matrix * TopLeft;
            var v1 = matrix * TopRight;
            var v2 = matrix * BottomRight;
            var v3 = matrix * BottomLeft;

            return FromPoints(v0, v1, v2, v3);
        }

        /// <summary>
        /// Adjusts the bounding size of the rectangle and returns the new rectangle.
        /// </summary>
        public Rectangle Inflate(float size)
        {
            var r = this;

            r.X -= size;
            r.Y -= size;
            r.Width += size * 2;
            r.Height += size * 2;

            return r;
        }

        public void Offset(float x, float y)
        {
            X += x;
            Y += y;
        }

        public void Offset(Vector offset)
        {
            Offset(offset.X, offset.Y);
        }

        #region Closest Point

        /// <summary>
        /// Returns the nearest point on the rectangle to the given point.
        /// </summary>
        public Vector GetClosestPoint(in Vector point)
        {
            Vector closest;
            closest.X = (point.X < Min.X) ? Min.X : (point.X > Max.X) ? Max.X : point.X;
            closest.Y = (point.Y < Min.Y) ? Min.Y : (point.Y > Max.Y) ? Max.Y : point.Y;
            return closest;
        }

        #endregion

        #region Contains

        /// <summary>
        /// Does this rectangle contain the given point?
        /// </summary>
        public bool ContainsPoint(in Vector point)
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
        public bool Contains(in Rectangle other)
        {
            if (other.Right > Right || other.Left < Left) { return false; }
            if (other.Top < Top || other.Bottom > Bottom) { return false; }
            return true;
        }

        #endregion

        #region Overlaps

        public bool Overlaps(IShape shape)
        {
            // rec - rec
            if (shape is Rectangle rec) { return Overlaps(rec); }
            // rec - cir
            else if (shape is Circle cir) { return Overlaps(cir); }
            // rec - pol
            else if (shape is Polygon pol) { return Overlaps((IReadOnlyList<Vector>) pol); }
            // rec - tri
            else if (shape is Triangle tri) { return Overlaps(tri); }
            // unknown case
            else
            {
                throw new InvalidOperationException("Unable to determine overlap, shape was not a known type.");
            }
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public bool Overlaps(in Circle circle)
        {
            // circle has the implementation
            return circle.Overlaps(this);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public bool Overlaps(in Triangle triangle)
        {
            // triangle has the implementation
            return triangle.Overlaps(this);
        }

        /// <summary>
        /// Does this rectangle overlap the given rectangle?
        /// </summary>
        public bool Overlaps(in Rectangle other)
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

        public bool Overlaps(IReadOnlyList<Vector> polygon)
        {
            var rec = GetTempPolygon(0);
            return Collisions.Overlaps(rec, polygon);
        }

        #endregion

        #region Raycast

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public bool Raycast(in Ray ray)
        {
            return Raycast(in ray, out _);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public bool Raycast(in Ray ray, out Contact contact)
        {
            // r.dir is unit direction vector of ray
            Vector dirfrac;
            dirfrac.X = 1.0f / ray.Direction.X;
            dirfrac.Y = 1.0f / ray.Direction.Y;

            var lb = Min;
            var rt = Max;

            // lb is the corner of AABB with minimal coordinates - left bottom, rt is maximal corner
            // r.org is origin of ray
            var t1 = (lb.X - ray.Origin.X) * dirfrac.X;
            var t2 = (rt.X - ray.Origin.X) * dirfrac.X;
            var t3 = (lb.Y - ray.Origin.Y) * dirfrac.Y;
            var t4 = (rt.Y - ray.Origin.Y) * dirfrac.Y;

            // 
            var tmin = Calc.Max(Calc.Min(t1, t2), Calc.Min(t3, t4));
            var tmax = Calc.Min(Calc.Max(t1, t2), Calc.Max(t3, t4));

            // if tmax < 0, ray (line) is intersecting AABB, but the whole AABB is behind us
            if (tmax < 0)
            {
                // 
                // var pMax = org + dir * tmax;
                // contact = new Contact(tmax, pMax, GetNormal(pMax - rectangle.Center));
                contact = default;
                return false;
            }

            // if tmin > tmax, ray doesn't intersect AABB
            if (tmin > tmax)
            {
                contact = default;
                return false;
            }

            // 
            var point = ray.Origin + (ray.Direction * tmin);
            contact = new Contact(point, GetBoxNormal(point - Center), tmin);
            return true;
        }

        private static Vector GetBoxNormal(Vector offset)
        {
            // todo: validate this gets the normal
            if (Calc.Abs(offset.X) > Calc.Abs(offset.Y)) { return new Vector(Calc.Sign(offset.X), 0); }
            else { return new Vector(0, Calc.Sign(offset.Y)); }
        }

        #endregion

        #region Deconstruct

        public void Deconstruct(out float x, out float y, out float w, out float h)
        {
            x = X;
            y = Y;
            w = Width;
            h = Height;
        }

        public void Deconstruct(out Vector position, out Size size)
        {
            position = Position;
            size = Size;
        }

        #endregion

        #region Conversion Operators

        public static explicit operator IntRectangle(Rectangle rect)
        {
            var x = (int) rect.X;
            var y = (int) rect.Y;
            var w = (int) rect.Width;
            var h = (int) rect.Height;

            return new IntRectangle(x, y, w, h);
        }

        public static implicit operator Rectangle((float x, float y, float width, float height) rect)
        {
            return new Rectangle(rect.x, rect.y, rect.width, rect.height);
        }

        public static implicit operator Rectangle((Vector position, Size size) rect)
        {
            return new Rectangle(rect.position, rect.size);
        }

        public static implicit operator (float x, float y, float width, float height)(Rectangle rect)
        {
            return (rect.X, rect.Y, rect.Width, rect.Height);
        }

        #endregion

        #region Comparison Operators

        public static bool operator ==(Rectangle rectangle1, Rectangle rectangle2)
        {
            return rectangle1.Equals(rectangle2);
        }

        public static bool operator !=(Rectangle rectangle1, Rectangle rectangle2)
        {
            return !(rectangle1 == rectangle2);
        }

        #endregion

        #region Equality

        public override bool Equals(object obj)
        {
            return obj is Rectangle rect
                && Equals(rect);
        }

        public bool Equals(Rectangle other)
        {
            return Calc.NearEquals(X, other.X)
                && Calc.NearEquals(Y, other.Y)
                && Calc.NearEquals(Width, other.Width)
                && Calc.NearEquals(Height, other.Height);
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

        internal Vector[] GetTempPolygon(int number)
        {
            var polygon = _polygon[number];

            polygon[0] = TopLeft;
            polygon[1] = TopRight;
            polygon[2] = BottomRight;
            polygon[3] = BottomLeft;

            return polygon;
        }

        public override string ToString()
        {
            return $"({Position}, {Size})";
        }
    }
}
