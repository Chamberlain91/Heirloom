using System;
using System.Runtime.CompilerServices;

namespace Heirloom.Math
{
    /// <summary>
    /// Represents a line segment defined by two <see cref="Vector"/>.
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

        public LineSegment(Vector a, Vector b)
        {
            A = a;
            B = b;
        }

        #endregion

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
        public bool Intersects(LineSegment other)
        {
            return Intersects(this, other);
        }

        /// <summary>
        /// Checks if this line segment intersects another.
        /// </summary>
        public bool Intersects(LineSegment other, out Vector point)
        {
            return Intersects(this, other, out point);
        }

        /// <summary>
        /// Checks if two line segments intersect.
        /// </summary>
        public static bool Intersects(LineSegment s1, LineSegment s2)
        {
            Intersects(s1.A, s1.B, s2.A, s2.B, out _, out var intersect, out _, out _, out _, out _);
            return intersect;
        }

        /// <summary>
        /// Checks if two line segments intersect.
        /// </summary>
        public static bool Intersects(LineSegment s1, LineSegment s2, out Vector point)
        {
            Intersects(s1.A, s1.B, s2.A, s2.B, out _, out var intersect, out _, out point, out _, out _);
            return intersect;
        }

        /// <summary>
        /// Checks if two line segments intersect.
        /// </summary>
        public static bool Intersects(Vector p1, Vector p2, Vector q1, Vector q2)
        {
            Intersects(p1, p2, q1, q2, out _, out var intersect, out _, out _, out _, out _);
            return intersect;
        }

        /// <summary>
        /// Checks if two line segments intersect.
        /// </summary>
        public static bool Intersects(Vector p1, Vector p2, Vector p3, Vector p4, out Vector point)
        {
            Intersects(p1, p2, p3, p4, out _, out var intersect, out _, out point, out _, out _);
            return intersect;
        }

        // Find the point of intersection between
        // the lines p1 --> p2 and q1 --> q2.
        private static void Intersects(
            Vector p1, Vector p2, Vector q1, Vector q2,
            out bool lines_intersect, out bool segments_intersect, out bool parallel,
            out Vector intersection,
            out Vector close_p1, out Vector close_p2)
        // http://csharphelper.com/blog/2014/08/determine-where-two-lines-intersect-in-c/
        {
            // todo: Model as Ray-Ray intersection to simplify/reduce code-duplication

            // Get the segments' parameters.
            var dx12 = p2.X - p1.X;
            var dy12 = p2.Y - p1.Y;
            var dx34 = q2.X - q1.X;
            var dy34 = q2.Y - q1.Y;

            // Solve for t1 and t2
            var denominator = (dy12 * dx34) - (dx12 * dy34);

            // 
            // Modified to more safely check divide by 0
            if (Calc.NearZero(denominator))
            {
                // The lines are parallel (or close enough to it).
                lines_intersect = false;
                segments_intersect = false;
                parallel = true;

                intersection = new Vector(float.NaN, float.NaN);
                close_p1 = new Vector(float.NaN, float.NaN);
                close_p2 = new Vector(float.NaN, float.NaN);
                return;
            }

            var t1 = (((p1.X - q1.X) * dy34) + ((q1.Y - p1.Y) * dx34)) / denominator;
            var t2 = (((q1.X - p1.X) * dy12) + ((p1.Y - q1.Y) * dx12)) / -denominator;

            lines_intersect = true;
            parallel = false;

            // Find the point of intersection.
            intersection = new Vector(p1.X + (dx12 * t1), p1.Y + (dy12 * t1));

            // The segments intersect if t1 and t2 are between 0 and 1.
            // Modified to have a better tolerance for segment edges
            segments_intersect = (t1 > (0 + IntersectionTolerance)) && (t1 < (1 - IntersectionTolerance)) &&
                                 (t2 > (0 + IntersectionTolerance)) && (t2 < (1 - IntersectionTolerance));

            // Clamp intersection times in order to
            // find the closest points on the segments.
            t1 = Calc.Clamp(t1, 0F, 1F);
            t2 = Calc.Clamp(t2, 0F, 1F);

            // ie, ray.GetPoint(t1)
            close_p1 = new Vector(p1.X + (dx12 * t1), p1.Y + (dy12 * t1));
            close_p2 = new Vector(q1.X + (dx34 * t2), q1.Y + (dy34 * t2));
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

        #region Equality

        public override bool Equals(object obj)
        {
            return obj is LineSegment segment && Equals(segment);
        }

        public bool Equals(LineSegment other)
        {
            return A.Equals(other.A) &&
                   B.Equals(other.B);
        }

        public override int GetHashCode()
        {
            return HashCode.Combine(A, B);
        }

        public static bool operator ==(LineSegment left, LineSegment right)
        {
            return left.Equals(right);
        }

        public static bool operator !=(LineSegment left, LineSegment right)
        {
            return !(left == right);
        }

        #endregion
    }
}
