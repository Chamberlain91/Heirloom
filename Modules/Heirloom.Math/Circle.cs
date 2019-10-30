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

        public Vector GetClosestPoint(in Vector point)
        {
            var off = point - Position;
            return off * (Radius / off.Length);
        }

        #endregion

        #region Contains

        public bool Contains(in Vector point)
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

        public bool Overlaps(IShape shape)
        {
            // cir - cir
            if (shape is Circle cir) { return Overlaps(cir); }
            // cir - rec
            else if (shape is Rectangle rec) { return Overlaps(rec); }
            // cir - tri
            else if (shape is Triangle tri) { return Overlaps(tri); }
            // cir - pol
            else if (shape is IPolygon pol) { return Overlaps((IReadOnlyList<Vector>) pol); }
            // unknown case
            else
            {
                throw new InvalidOperationException("Unable to determine overlap, shape was not a known type.");
            }
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public bool Overlaps(in Circle b)
        {
            var c = b.Position - Position;
            var r = Radius + b.Radius;

            return Vector.Dot(in c, in c) < (r * r);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public bool Overlaps(in Rectangle rect)
        {
            var poly = rect.GetTempPolygon(0);
            return Collisions.Overlaps(this, poly);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public bool Overlaps(in Triangle tri)
        {
            // triangle has the implementation
            return tri.Overlaps(this);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public bool Overlaps(in IReadOnlyList<Vector> poly)
        {
            return Collisions.Overlaps(this, poly);
        }

        #endregion

        #region Raycast

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public bool Raycast(in Ray ray)
        {
            return Raycast(in ray, out _);
        }

        public bool Raycast(in Ray ray, out Contact hit)
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
                hit = new Contact(impact, normal, t);
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
