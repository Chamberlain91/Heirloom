using System;
using System.Runtime.CompilerServices;
using System.Runtime.InteropServices;

namespace Heirloom.Math
{
    [StructLayout(LayoutKind.Sequential, Pack = 4)]
    public struct Circle
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

        #region Contains / Overlaps / Closest Point

        public bool Contains(in Vector point)
        {
            return Vector.DistanceSquared(Position, point) < (Radius * Radius);
        }

        public bool Contains(in Circle circle)
        {
            throw new NotImplementedException();
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public bool Overlaps(in Circle b)
        {
            var c = b.Position - Position;
            var r = Radius + b.Radius;

            return Vector.Dot(c, c) < (r * r);
        }

        public Vector ClosestPoint(in Vector point)
        {
            var off = point - Position;
            return off * (Radius / off.Length);
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

        public override string ToString()
        {
            return $"(Circle, {Position}, {Radius})";
        }
    }
}
