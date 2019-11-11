using System;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using System.Runtime.InteropServices;

namespace Heirloom.Math
{
    [StructLayout(LayoutKind.Sequential, Pack = 4)]
    public struct Circle : IShape, IEquatable<Circle>
    {
        public Vector Position;

        public float Radius;

        #region Constructors

        public Circle(Vector position, float radius)
        {
            Position = position;
            Radius = radius;
        }

        #endregion

        #region Properties

        public float Area => Calc.Pi * (Radius * Radius);

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

        #region Closest Point

        public Vector ClosestPoint(in Vector point)
        {
            var off = point - Position;
            return off * (Radius / off.Length);
        }

        #endregion

        #region Contains

        public bool ContainsPoint(in Vector point)
        {
            return Vector.DistanceSquared(Position, point) < (Radius * Radius);
        }

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

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public bool Raycast(in Ray ray)
        {
            return Raycast(in ray, out _);
        }

        public bool Raycast(in Ray ray, out RayContact hit)
        {
            var v = ray.Origin - Position;
            var c = Vector.Dot(v, v) - (Radius * Radius);
            var b = Vector.Dot(v, ray.Direction);
            var disc = (b * b) - c;

            // ...? Ray origin inside circle?
            if (disc < 0)
            {
                hit = default;
                return false;
            }

            // Compute `time`
            var t = -b - Calc.Sqrt(disc);

            // Positive `time` (forward on line)
            if (t >= 0)
            {
                var impact = ray.Origin + (ray.Direction * t);
                var normal = Vector.Normalize(impact - Position);
                hit = new RayContact(impact, normal, t);
                return true;
            }
            // Negative `time` (behind on line)
            else
            {
                // Do nothing, but felt important to comment in case
                // I feel inspired to create a line shape as well
            }

            hit = default;
            return false;
        }

        #endregion

        #region Equality

        public override bool Equals(object obj)
        {
            return obj is Circle circle && Equals(circle);
        }

        public bool Equals(Circle other)
        {
            return Position.Equals(other.Position) &&
                   Radius == other.Radius &&
                   Area == other.Area &&
                   Bounds.Equals(other.Bounds);
        }

        public override int GetHashCode()
        {
            return HashCode.Combine(Position, Radius, Area, Bounds);
        }

        public static bool operator ==(Circle left, Circle right)
        {
            return left.Equals(right);
        }

        public static bool operator !=(Circle left, Circle right)
        {
            return !(left == right);
        }

        #endregion

        public override string ToString()
        {
            return $"(Circle, {Position}, {Radius})";
        }
    }
}
