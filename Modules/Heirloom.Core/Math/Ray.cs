using System;
using System.Diagnostics;
using System.Runtime.CompilerServices;
using System.Runtime.InteropServices;

namespace Heirloom
{
    /// <summary>
    /// Represents a ray by orgin and direction vectors.
    /// </summary>
    [StructLayout(LayoutKind.Sequential, Pack = 4)]
    public struct Ray : IEquatable<Ray>
    {
        /// <summary>
        /// The origin of the ray.
        /// </summary>
        public Vector Origin;

        /// <summary>
        /// The direction of the ray.
        /// </summary>
        public Vector Direction;

        #region Constructors

        public Ray(Vector origin, Vector direction)
        {
            Debug.Assert(Calc.NearEquals(direction.LengthSquared, 1F, 0.001F));

            Origin = origin;
            Direction = direction;
        }

        #endregion 

        /// <summary>
        /// Sets the components of this size.
        /// </summary>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public void Set(in Vector origin, in Vector direction)
        {
            Origin = origin;
            Direction = direction;
        }

        /// <summary>
        /// Gets a point along the ray.
        /// </summary>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public Vector GetPoint(float distance)
        {
            return Origin + (Direction * distance);
        }

        #region Create From (Static)

        /// <summary>
        /// Creates a ray from a line segment.
        /// </summary>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Ray FromLineSegment(in Vector origin, in Vector target)
        {
            var dir = (target - origin).Normalized;
            return new Ray(origin, dir);
        }

        /// <summary>
        /// Creates a ray from a line segment.
        /// </summary>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Ray FromLineSegment(in LineSegment segment)
        {
            return FromLineSegment(in segment.A, in segment.B);
        }

        /// <summary>
        /// Creates a ray pointed along the specified angle from the origin given.
        /// </summary>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Ray FromAngle(Vector origin, float angle)
        {
            return new Ray(origin, Vector.FromAngle(angle));
        }

        #endregion

        #region Intersects (Static)

        /// <summary>
        /// Computes the intersection of two rays.
        /// </summary>
        public static bool Intersects(Ray r1, Ray r2, out float t1, out float t2)
        // todo: test!
        {
            // var d1x = r1.Direction.X; // p2.X - p1.X;
            // var d1y = r1.Direction.Y; // p2.Y - p1.Y;
            // var d2x = r2.Direction.X; // q2.X - q1.X;
            // var d2y = r2.Direction.Y; // q2.Y - q1.Y; 

            // var denominator = (d1y * d2x) - (d1x * d2y);
            var denominator = -Vector.Cross(r1.Direction, r2.Direction);

            // Modified to more safely check divide by 0
            if (Calc.NearZero(denominator))
            {
                // The lines are parallel (or close enough to it).
                t1 = t2 = -1;
                return false;
            }

            var offset = r1.Origin - r2.Origin;

            // 
            // t1 = (((r1.Origin.X - r2.Origin.X) * d2y) + ((r2.Origin.Y - r1.Origin.Y) * d2x)) / denominator;
            // t1 = ((offset.X * d2y) - (offset.Y * d2x)) / denominator;
            t1 = Vector.Cross(offset, r2.Direction) / denominator;

            // 
            // t2 = (((r2.Origin.X - r1.Origin.X) * d1y) + ((r1.Origin.Y - r2.Origin.Y) * d1x)) / -denominator;
            // t2 = ((offset.X * d1y) - (offset.Y * d1x)) / denominator;
            t2 = Vector.Cross(offset, r1.Direction) / denominator;

            // Both in positive domain of ray
            return t1 > 0 && t2 > 0;
        }

        #endregion

        #region Deconstruct

        public void Deconstruct(out Vector origin, out Vector direction)
        {
            origin = Origin;
            direction = Direction;
        }

        #endregion

        #region Comparison Operators

        public static bool operator ==(Ray left, Ray right)
        {
            return left.Equals(right);
        }

        public static bool operator !=(Ray left, Ray right)
        {
            return !(left == right);
        }

        #endregion

        #region Equality

        public override bool Equals(object obj)
        {
            return obj is Ray ray
                && Equals(ray);
        }

        public bool Equals(Ray other)
        {
            return Origin.Equals(other.Origin)
                && Direction.Equals(other.Direction);
        }

        public override int GetHashCode()
        {
            return HashCode.Combine(Origin, Direction);
        }

        #endregion

        public override string ToString()
        {
            return $"({Origin} -> {Direction})";
        }
    }
}
