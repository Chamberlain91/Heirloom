using System;
using System.Runtime.CompilerServices;
using System.Runtime.InteropServices;

namespace Meadows.Mathematics
{
    /// <summary>
    /// Represents a circle via center position and radius.
    /// </summary>
    [StructLayout(LayoutKind.Sequential, Pack = 4)]
    public struct Circle : IShape, IEquatable<Circle>
    {
        /// <summary>
        /// The center position of the circle.
        /// </summary>
        public Vector Position;

        /// <summary>
        /// The radius of the circle.
        /// </summary>
        public float Radius;

        #region Constructors

        /// <summary>
        /// Constructs a new instance of <see cref="Circle"/>.
        /// </summary>
        /// <param name="x">The x-coordinate of the center of the circle.</param>
        /// <param name="y">The y-coordiante of the center of the circle.</param>
        /// <param name="radius">The radius of the circle.</param>
        public Circle(float x, float y, float radius)
            : this((x, y), radius)
        { }

        /// <summary>
        /// Constructs a new instance of <see cref="Circle"/>.
        /// </summary>
        /// <param name="position">The center of the circle.</param>
        /// <param name="radius">The radius of the circle.</param>
        public Circle(Vector position, float radius)
        {
            Position = position;
            Radius = radius;
        }

        #endregion

        #region Properties

        Vector IShape.Center => Position;

        // A circle is always convex
        bool IShape.IsConvex => true;

        /// <summary>
        /// Gets the area of the circle.
        /// </summary>
        public float Area => Calc.Pi * (Radius * Radius);

        /// <summary>
        /// Gets the bounding rectangle of this circle.
        /// </summary>
        public Rectangle Bounds
        {
            get
            {
                // todo: optimize to reduce copies
                var min = Position - (Vector.One * Radius);
                var max = Position + (Vector.One * Radius);
                return new Rectangle(min, max);
            }
        }

        #endregion

        /// <summary>
        /// Sets the components of this circle.
        /// </summary>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public void Set(in Vector position, float radius)
        {
            Position = position;
            Radius = radius;
        }

        #region Nearest Point / Support

        /// <summary>
        /// Gets the nearest point on the circle to the specified point.
        /// </summary>
        public Vector GetNearestPoint(Vector point)
        {
            return GetSupport(point - Position);
        }

        /// <inheritdoc/>
        public Vector GetSupport(Vector dir)
        {
            return Position + (Radius * Vector.Normalize(dir));
        }

        #endregion

        #region Contains (Point, Circle)

        /// <summary>
        /// Determines if the specified point is contained by the circle.
        /// </summary>
        public bool Contains(Vector point)
        {
            return Vector.DistanceSquared(Position, point) < (Radius * Radius);
        }

        /// <summary>
        /// Determines if this circle contains another circle.
        /// </summary>
        public bool Contains(Circle circle)
        {
            var d = Vector.DistanceSquared(in Position, in circle.Position);
            return Radius > (d + circle.Radius);
        }

        #endregion

        #region Overlaps

        /// <summary>
        /// Determines if this circle overlaps another shape.
        /// </summary>
        public bool Overlaps(IShape shape)
        {
            return Collision.CheckOverlap(this, shape);
        }

        #endregion

        #region Raycast

        /// <summary>
        /// Peforms a raycast onto this circle, returning true upon intersection.
        /// </summary>
        /// <param name="ray">Some ray.</param>
        /// <param name="contact">Ray intersection information.</param>
        /// <returns></returns>
        public bool Raycast(Ray ray, out RayContact contact)
        {
            var v = ray.Origin - Position;
            var c = Vector.Dot(v, v) - (Radius * Radius);
            var b = Vector.Dot(v, ray.Direction);
            var disc = (b * b) - c;

            // ...? Ray origin inside circle?
            if (disc < 0)
            {
                contact = default;
                return false;
            }

            // Compute `time`
            var t = -b - Calc.Sqrt(disc);

            // Positive `time` (forward on line)
            if (t >= 0)
            {
                var impact = ray.Origin + (ray.Direction * t);
                var normal = Vector.Normalize(impact - Position);
                contact = new RayContact(impact, normal, t);
                return true;
            }
            // Negative `time` (behind on line)
            else
            {
                // Do nothing, but felt important to comment in case
                // I feel inspired to create a line shape as well
            }

            contact = default;
            return false;
        }

        #endregion

        #region Inflate

        /// <summary>
        /// Adjusts the circle radius by <paramref name="factor"/>.
        /// </summary>
        public void Inflate(float factor)
        {
            this = Inflate(this, factor);
        }

        /// <summary>
        /// Creates a new circle with the radius extended (or shrunk) by <paramref name="factor"/>.
        /// </summary>
        public static Circle Inflate(Circle circle, float factor)
        {
            circle.Radius += factor;
            return circle;
        }

        #endregion

        /// <summary>
        /// Creates an approximate polygon representation from this circle.
        /// </summary>
        /// <param name="pointCount">The number of points of the approximating n-gon.</param>
        public Polygon ToPolygon(int pointCount = 24)
        {
            if (pointCount < 3) { throw new ArgumentException("Polygon approximation must have at least 3 points."); }

            // Approximates a circle with a 24 point regular polygon
            var points = GeometryTools.GenerateRegularPolygon(Position, pointCount, Radius);
            return new Polygon(points);
        }

        #region Deconstruct

        /// <summary>
        /// Deconstructs the circle into constituient parts.
        /// </summary>
        /// <param name="center">The circle position.</param>
        /// <param name="radius">The circle radius.</param>
        public void Deconstruct(out Vector center, out float radius)
        {
            center = Position;
            radius = Radius;
        }

        #endregion

        #region Comparison Operators

        /// <summary>
        /// Compares two instances of <see cref="Circle"/> for equality.
        /// </summary>
        public static bool operator ==(Circle left, Circle right)
        {
            return left.Equals(right);
        }

        /// <summary>
        /// Compares two instances of <see cref="Circle"/> for inequality.
        /// </summary>
        public static bool operator !=(Circle left, Circle right)
        {
            return !(left == right);
        }

        #endregion

        #region Equality

        /// <summary>
        /// Compares this <see cref="Circle"/> for equality with another object.
        /// </summary>
        public override bool Equals(object obj)
        {
            return obj is Circle circle
                && Equals(circle);
        }

        /// <summary>
        /// Compares this <see cref="Circle"/> for equality with another <see cref="Circle"/>.
        /// </summary>
        public bool Equals(Circle other)
        {
            return Position.Equals(other.Position)
                && Calc.NearEquals(Radius, other.Radius);
        }

        /// <summary>
        /// Returns the hash code for this <see cref="Circle"/>.
        /// </summary>
        public override int GetHashCode()
        {
            return HashCode.Combine(Position, Radius);
        }

        #endregion

        /// <summary>
        /// Gets the string representation of this <see cref="Circle"/>.
        /// </summary>
        public override string ToString()
        {
            return $"(Circle, {Position}, {Radius})";
        }
    }
}
