using System.Runtime.CompilerServices;

namespace Heirloom.Math
{
    public struct LineSegment
    {
        public Vector A;

        public Vector B;

        public static float Tolerance = 0.001F;

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
        internal static void Intersects(
            Vector p1, Vector p2, Vector q1, Vector q2,
            out bool lines_intersect, out bool segments_intersect, out bool parallel,
            out Vector intersection,
            out Vector close_p1, out Vector close_p2)
        // http://csharphelper.com/blog/2014/08/determine-where-two-lines-intersect-in-c/
        {
            // todo: Model as Ray-Ray intersection

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
            segments_intersect = (t1 > (0 + Tolerance)) && (t1 < (1 - Tolerance)) &&
                                 (t2 > (0 + Tolerance)) && (t2 < (1 - Tolerance));

            // Clamp intersection times in order to
            // find the closest points on the segments.
            t1 = Calc.Clamp(t1, 0F, 1F);
            t2 = Calc.Clamp(t2, 0F, 1F);

            // ie, ray.GetPoint(t1)
            close_p1 = new Vector(p1.X + (dx12 * t1), p1.Y + (dy12 * t1));
            close_p2 = new Vector(q1.X + (dx34 * t2), q1.Y + (dy34 * t2));
        }

        #endregion
    }
}
