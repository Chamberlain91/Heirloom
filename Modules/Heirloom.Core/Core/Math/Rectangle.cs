using System;
using System.Collections.Generic;
using System.Runtime.CompilerServices;

using Heirloom.Geometry;

namespace Heirloom
{
    /// <summary>
    /// Represents a rectangle, defined by the top left corner position and size.
    /// </summary>
    public struct Rectangle : IShape, IEquatable<Rectangle>
    {
        /// <summary>
        /// The x-coordinate of this rectangle.
        /// </summary>
        public float X;

        /// <summary>
        /// The y-coordinate of this rectangle.
        /// </summary>
        public float Y;

        /// <summary>
        /// The width of this rectangle.
        /// </summary>
        public float Width;

        /// <summary>
        /// The height of this rectangle.
        /// </summary>
        public float Height;

        #region Constants

        /// <summary>
        /// A rectangle that spans the entire 2D plane (but inverted, with min and max reversed).
        /// </summary>
        public static Rectangle InvertedInfinite { get; } = new Rectangle(Vector.One * float.MaxValue, Vector.One * float.MinValue);

        /// <summary>
        /// A rectangle that spans the entire 2D plane.
        /// </summary>
        public static Rectangle Infinite { get; } = new Rectangle(Vector.One * float.MinValue, Vector.One * float.MaxValue);

        /// <summary>
        /// A 1x1 rectangle that is positioned at the origin.
        /// </summary>
        public static Rectangle One { get; } = new Rectangle(0, 0, 1, 1);

        /// <summary>
        /// A 0x0 rectangle that is positioned at the origin.
        /// </summary>
        public static Rectangle Zero { get; } = new Rectangle(0, 0, 0, 0);

        #endregion

        #region Constructors

        /// <summary>
        /// Constructs a new instance of <see cref="Rectangle"/>.
        /// </summary>
        /// <param name="x">The x position of the rectangle.</param>
        /// <param name="y">The y position of the rectangle.</param>
        /// <param name="width">The width of the rectangle.</param>
        /// <param name="height">The height of the rectangle.</param>
        public Rectangle(float x, float y, float width, float height)
        {
            X = x;
            Y = y;
            Width = width;
            Height = height;
        }

        /// <summary>
        /// Constructs a new instance of <see cref="Rectangle"/>.
        /// </summary>
        /// <param name="position">The position of the rectangle.</param>
        /// <param name="size">The size of the rectangle.</param>
        public Rectangle(Vector position, Size size)
            : this(position.X, position.Y, size.Width, size.Height)
        { }

        /// <summary>
        /// Constructs a new instance of <see cref="Rectangle"/> using minimum and maximum points.
        /// </summary>
        /// <param name="min">The minimum point.</param>
        /// <param name="max">The maximum point.</param>
        public Rectangle(Vector min, Vector max)
            : this(min, (Size) (max - min))
        { }

        #endregion

        #region Properties

        Rectangle IShape.Bounds => this;

        /// <summary>
        /// Gets the area of this rectangle.
        /// </summary>
        public float Area => Width * Height;

        /// <summary>
        /// Gets or sets the size of this rectangle.
        /// </summary>
        public Size Size
        {
            get => new Size(Width, Height);

            set
            {
                Width = value.Width;
                Height = value.Height;
            }
        }

        /// <summary>
        /// Gets or sets the position of this rectangle.
        /// </summary>
        public Vector Position
        {
            get => TopLeft;

            set
            {
                X = value.X;
                Y = value.Y;
            }
        }

        /// <summary>
        /// Gets or sets the center position of this rectangle.
        /// </summary>
        public Vector Center
        {
            get => (Min + Max) / 2F;
            set => Position = new Vector(value.X - Width / 2F, value.Y - Height / 2F);
        }

        /// <summary>
        /// Gets the minimum corner of this rectangle.
        /// </summary>
        public Vector Min => TopLeft;

        /// <summary>
        /// Gets the maximum corner of this rectangle.
        /// </summary>
        public Vector Max => BottomRight;

        /// <summary>
        /// Gets the left extent of this rectangle.
        /// </summary>
        public float Left => X;

        /// <summary>
        /// Gets the top extent of this rectangle.
        /// </summary>
        public float Top => Y;

        /// <summary>
        /// Gets the right extent of this rectangle.
        /// </summary>
        public float Right => X + Width;

        /// <summary>
        /// Gets the bottom extent of this rectangle.
        /// </summary>
        public float Bottom => Y + Height;

        /// <summary>
        /// Gets the top left corner of this rectangle.
        /// </summary>
        public Vector TopLeft => new Vector(X, Y);

        /// <summary>
        /// Gets the bottom left corner of this rectangle.
        /// </summary>
        public Vector BottomLeft => new Vector(X, Bottom);

        /// <summary>
        /// Gets the bottom right corner of this rectangle.
        /// </summary>
        public Vector BottomRight => new Vector(Right, Bottom);

        /// <summary>
        /// Gets the top right corner of this rectangle.
        /// </summary>
        public Vector TopRight => new Vector(Right, Y);

        /// <summary>
        /// Determines if the values of this rectangle are considered to be valid or in other words
        /// that <c>left &lt; right</c> and <c>top &lt; bottom</c>.
        /// </summary>
        public bool IsValid => Left < Right && Top < Bottom;

        #endregion

        /// <summary>
        /// Sets the components of this rectangle.
        /// </summary>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public void Set(float x, float y, float w, float h)
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
            // Clone this rectangle as a polygon
            var vertices = PolygonTools.RequestTempPolygon(in this);
            var polygon = new Polygon(vertices);

            // Recycle temp poylgon and return clone.
            PolygonTools.RecycleTempPolygon(vertices);
            return polygon;
        }

        #region Transform (Offset)

        /// <summary>
        /// Translates this rectangle.
        /// </summary>
        public void Offset(float x, float y)
        {
            X += x;
            Y += y;
        }

        /// <summary>
        /// Translates this rectangle.
        /// </summary>
        public void Offset(Vector offset)
        {
            Offset(offset.X, offset.Y);
        }

        /// <summary>
        /// Copies and translates the given rectangle.
        /// </summary>
        public static Rectangle Offset(Rectangle rect, float x, float y)
        {
            rect.X += x;
            rect.Y += y;

            return rect;
        }

        /// <summary>
        /// Copies and translates the given rectangle.
        /// </summary>
        public static Rectangle Offset(Rectangle rect, Vector offset)
        {
            return Offset(rect, offset.X, offset.Y);
        }

        #endregion

        #region Transform (Matrix)

        /// <summary>
        /// Transforms the four corners of this rectangle and updates itself to bound these points.
        /// </summary>
        public Rectangle Transform(in Matrix matrix)
        {
            this = Transform(this, in matrix);
            return this;
        }

        /// <summary>
        /// Transforms the four corners of this rectangle and returns the bounding rectangle of these points.
        /// </summary>
        public static Rectangle Transform(Rectangle rectangle, in Matrix matrix)
        {
            var v0 = matrix * rectangle.TopLeft;
            var v1 = matrix * rectangle.TopRight;
            var v2 = matrix * rectangle.BottomRight;
            var v3 = matrix * rectangle.BottomLeft;

            return FromPoints(v0, v1, v2, v3);
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
        public void Include(Vector point)
        {
            var min = Vector.Min(Min, point);
            var max = Vector.Max(Max, point);

            this = new Rectangle(min, max);
        }

        /// <summary>
        /// Mutates this rectangle to accommodate the given rectangle.
        /// </summary>
        /// <remarks>
        /// Useful for computing a bounding rectangle.
        /// </remarks>
        /// <param name="rect">Some rectangle to include.</param>
        public void Include(in Rectangle rect)
        {
            this = Merge(in this, in rect);
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
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Rectangle Merge(in Rectangle a, in Rectangle b)
        {
            var min = Vector.Min(a.Min, b.Min);
            var max = Vector.Max(a.Max, b.Max);

            return new Rectangle(min, max);
        }

        /// <summary>
        /// Merges the given rectangles into one potentially larger rectangle.
        /// </summary>
        /// <remarks>
        /// Useful for computing a bounding rectangle.
        /// </remarks>
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

        #region Inflate

        /// <summary>
        /// Expands (or shrinks) the rectangle by a factor on both axis.
        /// </summary>
        public void Inflate(float factor)
        {
            this = Inflate(this, factor);
        }

        /// <summary>
        /// Expands (or shrinks) the rectangle by a factor on each axis.
        /// </summary>
        public void Inflate(float xFactor, float yFactor)
        {
            this = Inflate(this, xFactor, yFactor);
        }

        /// <summary>
        /// Expands (or shrinks) the input rectangle by a factor on both axis.
        /// </summary>
        public static Rectangle Inflate(Rectangle rect, float factor)
        {
            return Inflate(rect, factor, factor);
        }

        /// <summary>
        /// Expands (or shrinks) the input rectangle by a factor on each axis.
        /// </summary>
        public static Rectangle Inflate(Rectangle rect, float xFactor, float yFactor)
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
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Rectangle FromPoints(params Vector[] points)
        {
            return FromPoints((IEnumerable<Vector>) points);
        }

        /// <summary>
        /// Computes the bounding rectangle of the given set of points.
        /// </summary>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Rectangle FromPoints(IEnumerable<Vector> points)
        {
            var b = InvertedInfinite;
            foreach (var v in points) { b.Include(v); }
            return b;
        }

        #endregion

        #region Closest Point

        /// <summary>
        /// Returns the nearest point on the rectangle to the given point.
        /// </summary>
        public Vector GetNearestPoint(in Vector point)
        {
            Vector closest;
            closest.X = (point.X < Min.X) ? Min.X : (point.X > Max.X) ? Max.X : point.X;
            closest.Y = (point.Y < Min.Y) ? Min.Y : (point.Y > Max.Y) ? Max.Y : point.Y;
            return closest;
        }

        #endregion

        #region Contains (Point, Rectangle)

        /// <summary>
        /// Determines if this rectangle contains the given point?
        /// </summary>
        public bool Contains(in Vector point)
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
        public bool Contains(in Rectangle other)
        {
            if (other.Right > Right || other.Left < Left) { return false; }
            if (other.Top < Top || other.Bottom > Bottom) { return false; }
            return true;
        }

        #endregion

        #region Overlaps

        /// <summary>
        /// Determines if this rectangle overlaps another shape.
        /// </summary>
        public bool Overlaps(IShape shape)
        {
            return shape switch
            {
                Circle cir => Overlaps(cir),
                Triangle tri => Overlaps(tri),
                Rectangle rec => Overlaps(rec),
                Polygon pol => Overlaps(pol),

                // Unknown shape
                _ => throw new InvalidOperationException("Unable to determine overlap, shape was not a known type."),
            };
        }

        /// <summary>
        /// Determines if this rectangle overlaps the specified circle.
        /// </summary>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public bool Overlaps(in Circle circle)
        {
            // circle has the implementation
            return circle.Overlaps(in this);
        }

        /// <summary>
        /// Determines if this rectangle overlaps the specified triangle.
        /// </summary>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public bool Overlaps(in Triangle triangle)
        {
            // Get temporary polygon representation
            var polygon = PolygonTools.RequestTempPolygon(in this);

            // Check for overlap
            var result = triangle.Overlaps(polygon);

            // Recycle temporary polygon and return overlap status
            PolygonTools.RecycleTempPolygon(polygon);
            return result;
        }

        /// <summary>
        /// Determines if this rectangle overlaps another rectangle.
        /// </summary>
        public bool Overlaps(in Rectangle other)
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

        /// <summary>
        /// Determines if this rectangle overlaps the specified convex polygon.
        /// </summary>
        public bool Overlaps(IReadOnlyList<Vector> polygon)
        {
            // Get temporary polygon representation
            var other = PolygonTools.RequestTempPolygon(in this);

            // Check for overlap
            var result = SeparatingAxis.Overlaps(polygon, other);

            // Recycle temporary polygon and return overlap status
            PolygonTools.RecycleTempPolygon(other);
            return result;
        }

        /// <summary>
        /// Determines if this rectangle overlaps the specified simple polygon.
        /// </summary>
        public bool Overlaps(Polygon polygon)
        {
            // polygon has the implementation
            return polygon.Overlaps(in this);
        }

        #endregion

        #region Axis Projection

        /// <summary>
        /// Project this rectangle onto the specified axis.
        /// </summary>
        public Range Project(in Vector axis)
        {
            // Get temporary polygon representation
            var polygon = PolygonTools.RequestTempPolygon(in this);

            // Project polygon onto axis
            var result = PolygonTools.Project(polygon, in axis);

            // Recycle temporary polygon and return overlap status
            PolygonTools.RecycleTempPolygon(polygon);
            return result;
        }

        #endregion

        #region Raycast

        /// <summary>
        /// Peforms a raycast onto this rectangle, returning true upon intersection.
        /// </summary>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public bool Raycast(in Ray ray)
        {
            return Raycast(in ray, out _);
        }

        /// <summary>
        /// Peforms a raycast onto this circle, returning true upon intersection.
        /// </summary>
        /// <param name="ray">Some ray.</param>
        /// <param name="contact">Ray intersection information.</param>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public bool Raycast(in Ray ray, out RayContact contact)
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
            contact = new RayContact(point, GetBoxNormal(point - Center), tmin);
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

        /// <summary>
        /// Deconstructs this rectangle into consituent components.
        /// </summary>
        /// <param name="x">The x position of the rectangle.</param>
        /// <param name="y">The y position of the rectangle.</param>
        /// <param name="w">The width of the rectangle.</param>
        /// <param name="h">The height of the rectangle.</param>
        public void Deconstruct(out float x, out float y, out float w, out float h)
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
        public void Deconstruct(out Vector position, out Size size)
        {
            position = Position;
            size = Size;
        }

        #endregion

        #region Conversion Operators

        /// <summary>
        /// Converts a <see cref="Rectangle"/> into <see cref="IntRectangle"/> by integer truncation.
        /// </summary>
        public static explicit operator IntRectangle(Rectangle rect)
        {
            var x = (int) rect.X;
            var y = (int) rect.Y;
            var w = (int) rect.Width;
            var h = (int) rect.Height;

            return new IntRectangle(x, y, w, h);
        }

        /// <summary>
        /// Converts a rectangle formatted tuple into <see cref="Rectangle"/>.
        /// </summary>
        public static implicit operator Rectangle((float x, float y, float width, float height) rect)
        {
            return new Rectangle(rect.x, rect.y, rect.width, rect.height);
        }

        /// <summary>
        /// Converts a rectangle formatted tuple into <see cref="Rectangle"/>.
        /// </summary>
        public static implicit operator Rectangle((Vector position, Size size) rect)
        {
            return new Rectangle(rect.position, rect.size);
        }

        #endregion

        #region Comparison Operators

        /// <summary>
        /// Compares two rectangles for equality.
        /// </summary>
        public static bool operator ==(Rectangle rectangle1, Rectangle rectangle2)
        {
            return rectangle1.Equals(rectangle2);
        }

        /// <summary>
        /// Compares two rectangles for inequality.
        /// </summary>
        public static bool operator !=(Rectangle rectangle1, Rectangle rectangle2)
        {
            return !(rectangle1 == rectangle2);
        }

        #endregion

        #region Equality

        /// <summary>
        /// Compares this rectangle for equalty with another object.
        /// </summary>
        public override bool Equals(object obj)
        {
            return obj is Rectangle rect
                && Equals(rect);
        }

        /// <summary>
        /// Compares this rectangle for equalty with another rectangle.
        /// </summary>
        public bool Equals(Rectangle other)
        {
            return Calc.NearEquals(X, other.X)
                && Calc.NearEquals(Y, other.Y)
                && Calc.NearEquals(Width, other.Width)
                && Calc.NearEquals(Height, other.Height);
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
        /// Returns the string representation of this rectangle.
        /// </summary>
        public override string ToString()
        {
            return $"({Position}, {Size})";
        }
    }
}
