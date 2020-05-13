using System;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using System.Runtime.InteropServices;

namespace Heirloom.Geometry
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

        /// <summary>
        /// Create a polygon from this rectangle.
        /// </summary>
        public Polygon ToPolygon()
        {
            // Approximates a circle with a 24 point regular polygon
            var points = GeometryTools.GenerateRegularPolygon(Position, 24, Radius);
            return new Polygon(points);
        }

        #region Closest Point

        /// <summary>
        /// Gets the nearest point on the circle to the specified point.
        /// </summary>
        public Vector GetNearestPoint(in Vector point)
        {
            var offset = Vector.Normalize(point - Position);
            return Position + (offset * Radius);
        }

        #endregion

        #region Contains (Point, Circle)

        /// <summary>
        /// Determines if the specified point is contained by the circle.
        /// </summary>
        public bool Contains(in Vector point)
        {
            return Vector.DistanceSquared(Position, point) < (Radius * Radius);
        }

        /// <summary>
        /// Determines if this circle contains another circle.
        /// </summary>
        public bool Contains(in Circle circle)
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
            return shape switch
            {
                Circle cir => Overlaps(in cir),
                Triangle tri => Overlaps(in tri),
                Rectangle rec => Overlaps(in rec),
                Polygon pol => Overlaps(pol),

                // Unknown shape
                _ => throw new InvalidOperationException("Unable to determine overlap, shape was not a known type."),
            };
        }

        /// <summary>
        /// Determines if this circle overlaps another circle.
        /// </summary>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public bool Overlaps(in Circle b)
        {
            var c = b.Position - Position;
            var r = Radius + b.Radius;

            return Vector.Dot(in c, in c) < (r * r);
        }

        /// <summary>
        /// Determines if this circle overlaps the specified rectangle.
        /// </summary>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public bool Overlaps(in Rectangle rectangle)
        {
            // Assembly temporary polygon representation of the rectangle
            var polygon = PolygonTools.RequestTempPolygon(in rectangle);

            // Test overlap with temp polygon
            var overlap = SeparatingAxis.Overlaps(polygon, in this);

            // Recycle temporary polygon and return overlap status
            PolygonTools.RecycleTempPolygon(polygon);
            return overlap;
        }

        /// <summary>
        /// Determines if this circle overlaps the specified triangle.
        /// </summary>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public bool Overlaps(in Triangle triangle)
        {
            // Assembly temporary polygon representation of the triangle
            var polygon = PolygonTools.RequestTempPolygon(in triangle);

            // Test overlap with temp polygon
            var overlap = SeparatingAxis.Overlaps(polygon, in this);

            // Recycle temporary polygon and return overlap status
            PolygonTools.RecycleTempPolygon(polygon);
            return overlap;
        }

        /// <summary>
        /// Determines if this circle overlaps the specified simple polygon.
        /// </summary>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public bool Overlaps(Polygon polygon)
        {
            if (polygon is null) { throw new ArgumentNullException(nameof(polygon)); }

            // For each convex partition on this polygon,
            foreach (var partition in polygon.ConvexPartitions)
            {
                // check if this partition overlaps the circle.
                if (SeparatingAxis.Overlaps(partition, in this))
                {
                    // An overlap was detected
                    return true;
                }
            }

            // No overlap was detected
            return false;
        }

        /// <summary>
        /// Determines if this circle overlaps the specified convex polygon.
        /// </summary>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public bool Overlaps(IReadOnlyList<Vector> polygon)
        {
            return SeparatingAxis.Overlaps(polygon, in this);
        }

        #endregion

        #region Axis Projection

        /// <summary>
        /// Project this circle onto the specified axis.
        /// </summary>
        public Range Project(in Vector axis)
        {
            var t = Vector.Project(in Position, in axis);
            return new Range(t - Radius, t + Radius);
        }

        #endregion

        #region Raycast

        /// <summary>
        /// Peforms a raycast onto this circle, returning true upon intersection.
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
        /// <returns></returns>
        public bool Raycast(in Ray ray, out RayContact contact)
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
