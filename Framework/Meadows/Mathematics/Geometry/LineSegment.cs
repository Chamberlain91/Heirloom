using System;
using System.Runtime.CompilerServices;

namespace Meadows.Mathematics
{
    /// <summary>
    /// Represents a line segment defined by two end points.
    /// </summary>
    public struct LineSegment : IEquatable<LineSegment>
    {
        /// <summary>
        /// A value to adjust the intersection tolerance to compensate for floating-point error.
        /// </summary>
        public static float IntersectionTolerance = 0.001F;

        /// <summary>
        /// The first end-point.
        /// </summary>
        public Vector A;

        /// <summary>
        /// The second end-point.
        /// </summary>
        public Vector B;

        #region Constructors

        /// <summary>
        /// Constructs a new <see cref="LineSegment"/>.
        /// </summary>
        /// <param name="a">The first end-point of this <see cref="LineSegment"/>.</param>
        /// <param name="b">The second end-point of this <see cref="LineSegment"/>.</param>
        public LineSegment(Vector a, Vector b)
        {
            A = a;
            B = b;
        }

        #endregion

        /// <summary>
        /// Sets the components of this line segment.
        /// </summary>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public void Set(in Vector a, in Vector b)
        {
            A = a;
            B = b;
        }

        /// <summary>
        /// Gets a intermediate point along the line segment.
        /// </summary>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public Vector GetPoint(float t)
        {
            return Vector.Lerp(A, B, t);
        }

        #region Intersects

        /// <summary>
        /// Checks if this line segment intersects another.
        /// </summary>
        public bool Intersects(in LineSegment other)
        {
            return Intersects(in this, in other);
        }

        /// <summary>
        /// Checks if this line segment intersects another.
        /// </summary>
        public bool Intersects(in LineSegment other, out Vector point)
        {
            return Intersects(in this, in other, out point);
        }

        /// <summary>
        /// Computes the intersection of two line segments.
        /// This function may be unclamped to compute the intersection of two infinite lines.
        /// </summary>
        /// <param name="a">The first line.</param>
        /// <param name="b">The second line.</param>
        /// <param name="clampSegment">Should the computatio clamp to line segments?</param>
        /// <returns>The point of intersection. If</returns>
        public static bool Intersects(in LineSegment a, in LineSegment b, bool clampSegment = true)
        {
            return Intersects(in a, in b, out _, clampSegment);
        }

        /// <summary>
        /// Computes the intersection of two line segments.
        /// This function may be unclamped to compute the intersection of two infinite lines.
        /// </summary>
        /// <param name="a0">The first point of the first line.</param>
        /// <param name="a1">The second point of the first line.</param>
        /// <param name="b0">The first point of the second line.</param>
        /// <param name="b1">The second point of the second line.</param>
        /// <param name="clampSegment">Should the computatio clamp to line segments?</param>
        /// <returns>The point of intersection. If</returns>
        public static bool Intersects(in Vector a0, in Vector a1, in Vector b0, in Vector b1, bool clampSegment = true)
        {
            return Intersects(a0, a1, b0, b1, out float _, clampSegment);
        }

        /// <summary>
        /// Computes the intersection of two line segments.
        /// This function may be unclamped to compute the intersection of two infinite lines.
        /// </summary>
        /// <param name="a">The first line.</param>
        /// <param name="b">The second line.</param>
        /// <param name="intersection">The point of intersection.</param>
        /// <param name="clampSegment">Should the computatio clamp to line segments?</param>
        /// <returns>The point of intersection. If</returns>
        public static bool Intersects(in LineSegment a, in LineSegment b, out Vector intersection, bool clampSegment = true)
        {
            return Intersects(a.A, in a.B, in b.A, in b.B, out intersection, clampSegment);
        }

        /// <summary>
        /// Computes the intersection of two line segments.
        /// This function may be unclamped to compute the intersection of two infinite lines.
        /// </summary>
        /// <param name="a0">The first point of the first line.</param>
        /// <param name="a1">The second point of the first line.</param>
        /// <param name="b0">The first point of the second line.</param>
        /// <param name="b1">The second point of the second line.</param>
        /// <param name="intersection">The point of intersection.</param>
        /// <param name="clampSegment">Should the computation clamp to line segments?</param>
        /// <returns>The point of intersection. If</returns>
        public static bool Intersects(in Vector a0, in Vector a1, in Vector b0, in Vector b1, out Vector intersection, bool clampSegment = true)
        {
            var intersectionStatus = Intersects(in a0, in a1, in b0, in b1, out float time, clampSegment);

            // Interpolate point of intersection
            intersection = Vector.Lerp(a0, a1, time);
            return intersectionStatus;
        }

        /// <summary>
        /// Computes the intersection of two line segments.
        /// This function may be unclamped to compute the intersection of two infinite lines.
        /// </summary>
        /// <param name="a0">The first point of the first line.</param>
        /// <param name="a1">The second point of the first line.</param>
        /// <param name="b0">The first point of the second line.</param>
        /// <param name="b1">The second point of the second line.</param>
        /// <param name="intersectionTime">The intersection time between <paramref name="a0"/> and <paramref name="a1"/>.</param>
        /// <param name="clampSegment">Should the computation clamp to line segments?</param>
        /// <returns>The point of intersection. If</returns>
        public static bool Intersects(in Vector a0, in Vector a1, in Vector b0, in Vector b1, out float intersectionTime, bool clampSegment = true)
        {
            // Edge vectors
            var ae = a1 - a0;
            var be = b1 - b0;

            var denominator = Vector.Cross(be, ae);

            // Prevent divide by zero errors
            if (Calc.NearZero(denominator))
            {
                // todo: is this the most suitable point?
                intersectionTime = 0F;
                return false;
            }

            // Find the point of intersection.
            // todo: what is this? A mixture of dot/cross?
            var time = (((a0.X - b0.X) * be.Y) + ((b0.Y - a0.Y) * be.X)) / denominator;
            var intersectionStatus = true;

            // If we are clamping to the line segment, we cannot extend beyond the segment.
            // So we will clamp the intersection 'time' to the segment and return no intersection status.
            if (clampSegment && (time < 0 || time > 1))
            {
                intersectionStatus = false;
                time = Calc.Clamp(time, 0F, 1F);
            }

            // Compute point of intersection (or closest to segment)
            intersectionTime = time;
            return intersectionStatus;
        }

        #endregion

        #region Closest Point

        /// <summary>
        /// Gets the closest point on the line segment to the specified point.
        /// </summary>
        public Vector GetClosestPoint(Vector p)
        {
            return GetClosestPoint(A, B, p);
        }

        /// <summary>
        /// Gets the closest point on a line segment to the specified point.
        /// </summary>
        public static Vector GetClosestPoint(Vector a, Vector b, Vector p)
        {
            var t = Vector.Project(a, b, p, true);
            return a + (b - a) * t;
        }

        /// <summary>
        /// Gets the closest point on a line segment to the specified point.
        /// </summary>
        public static Vector ClosestPoint(Vector a, Vector b, Vector p, out float distance)
        {
            // todo: possibly extract distance from the projection itself to avoid the sqrt
            var q = GetClosestPoint(a, b, p);
            distance = Vector.Distance(q, p);
            return q;
        }

        #endregion

        #region Comparison Operators

        /// <summary>
        /// Compares two instances of <see cref="LineSegment"/> for equality.
        /// </summary>
        public static bool operator ==(LineSegment left, LineSegment right)
        {
            return left.Equals(right);
        }

        /// <summary>
        /// Compares two instances of <see cref="LineSegment"/> for inequality.
        /// </summary>
        public static bool operator !=(LineSegment left, LineSegment right)
        {
            return !(left == right);
        }

        #endregion

        #region Equality

        /// <summary>
        /// Compares this <see cref="LineSegment"/> for equality with another object.
        /// </summary>
        public override bool Equals(object obj)
        {
            return obj is LineSegment edge
                && Equals(edge);
        }

        /// <summary>
        /// Compares this <see cref="LineSegment"/> for equality with another <see cref="LineSegment"/>.
        /// </summary>
        public bool Equals(LineSegment other)
        {
            return (A.Equals(other.A) && B.Equals(other.B))
                || (A.Equals(other.B) && B.Equals(other.A));
        }

        /// <summary>
        /// Returns the hash code for this <see cref="LineSegment"/>.
        /// </summary>
        public override int GetHashCode()
        {
            return A.GetHashCode() ^ B.GetHashCode();
        }

        #endregion
    }
}
