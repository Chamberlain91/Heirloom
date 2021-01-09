using System;
using System.Collections.Generic;

namespace Heirloom.Mathematics
{
    /// <summary>
    /// An implementation of a bezier spline using multiple 'segments' of cubic curves.
    /// </summary>
    public sealed class Bezier
    {
        private readonly List<Segment> _segments;

        /// <summary>
        /// Constructs a new instance of <see cref="Bezier"/>.
        /// </summary>
        public Bezier()
        {
            _segments = new List<Segment>();
        }

        /// <summary>
        /// The number of segments in this curve.
        /// </summary>
        public int Count => _segments.Count;

        /// <summary>
        /// Adds a segment to the end of the curve.
        /// </summary>
        /// <param name="controlPoint">The control point.</param>
        /// <param name="inHandle">The first handle, relative to this newly added point.</param>
        /// <param name="outHandle">The second handle, relative to the next point in the curve.</param>
        /// <param name="type">The type of curve the segment after this point represents will act like.</param>
        public void Add(Vector controlPoint, Vector inHandle, Vector outHandle)
        {
            var segment = new Segment(controlPoint, inHandle, outHandle);
            _segments.Add(segment);
        }

        /// <summary>
        /// Inserts a segment into the curve.
        /// </summary>
        /// <param name="index">Some index within the curve.</param>
        /// <param name="controlPoint">The control point.</param>
        /// <param name="inHandle">The first handle, relative to this newly added point.</param>
        /// <param name="outHandle">The second handle, relative to the next point in the curve.</param>
        /// <param name="type">The type of curve the segment after this point represents will act like.</param>
        public void Insert(int index, Vector controlPoint, Vector inHandle, Vector outHandle)
        {
            var s = new Segment(controlPoint, inHandle, outHandle);
            _segments.Insert(index, s);
        }

        /// <summary>
        /// Removes a segment from the curve.
        /// </summary>
        /// <param name="index">Some index within the curve.</param>
        /// <exception cref="IndexOutOfRangeException">If the <paramref name="index"/> is less than zero or greater than or equal to <see cref="Count"/>.</exception>
        public void RemoveAt(int index)
        {
            _segments.RemoveAt(index);
        }

        /// <summary>
        /// Gets the point of the curve at the specified index.
        /// </summary>
        /// <param name="index">The index of the point.</param>
        /// <returns>The point at the specified index.</returns>
        /// <exception cref="IndexOutOfRangeException">If the <paramref name="index"/> is less than zero or greater than or equal to <see cref="Count"/>.</exception>
        public Vector GetPoint(int index)
        {
            return _segments[index].Point;
        }

        /// <summary>
        /// Sets the point of the curve at the specified index.
        /// </summary>
        /// <param name="index">The index of the point.</param>
        /// <param name="point">The value to set the point with.</param>
        /// <exception cref="IndexOutOfRangeException">If the <paramref name="index"/> is less than zero or greater than or equal to <see cref="Count"/>.</exception>
        public void SetPoint(int index, Vector point)
        {
            _segments[index].Point = point;
        }

        /// <summary>
        /// Gets the incoming handle point of the curve at the specified index.
        /// </summary>
        /// <param name="index">The index of the point.</param>
        /// <returns>The incoming handle point at the specified index.</returns>
        /// <exception cref="IndexOutOfRangeException">If the <paramref name="index"/> is less than zero or greater than or equal to <see cref="Count"/>.</exception>
        public Vector GetInHandle(int index)
        {
            return _segments[index].InHandle;
        }

        /// <summary>
        /// Sets the incoming handle point of the curve at the specified index.
        /// </summary>
        /// <param name="index">The index of the point.</param>
        /// <param name="point">The value to set the point with.</param>
        /// <exception cref="IndexOutOfRangeException">If the <paramref name="index"/> is less than zero or greater than or equal to <see cref="Count"/>.</exception>
        public void SetInHandle(int index, Vector point)
        {
            _segments[index].InHandle = point;
        }

        /// <summary>
        /// Gets the outgoing handle point of the curve at the specified index.
        /// </summary>
        /// <param name="index">The index of the point.</param>
        /// <returns>The outgoing handle point at the specified index.</returns>
        /// <exception cref="IndexOutOfRangeException">If the <paramref name="index"/> is less than zero or greater than or equal to <see cref="Count"/>.</exception>
        public Vector GetOutHandle(int index)
        {
            return _segments[index].OutHandle;
        }

        /// <summary>
        /// Sets the outgoing handle point of the curve at the specified index.
        /// </summary>
        /// <param name="index">The index of the point.</param>
        /// <param name="point">The value to set the point with.</param>
        /// <exception cref="IndexOutOfRangeException">If the <paramref name="index"/> is less than zero or greater than or equal to <see cref="Count"/>.</exception>
        public void SetOutHandle(int index, Vector point)
        {
            _segments[index].OutHandle = point;
        }

        /// <summary>
        /// Computes a point interpolated across the curve.
        /// </summary>
        public Vector Interpolate(float t)
        {
            var currentIndex = Calc.Floor(t);
            var nextIndex = Calc.Ceil(t);
            var current = _segments[currentIndex];
            var next = nextIndex < _segments.Count ? _segments[nextIndex] : current;
            return current.Interpolate(t - currentIndex, next.Point);
        }

        /// <summary>
        /// Computes the derivative of a point interpolated across the curve.
        /// </summary>
        public Vector InterpolateDerivative(float t)
        {
            var currentIndex = Calc.Floor(t);
            var nextIndex = Calc.Ceil(t);
            var current = _segments[currentIndex];
            var next = nextIndex < _segments.Count ? _segments[nextIndex] : current;
            return current.InterpolateDerivative(t - currentIndex, next.Point);
        }

        internal IEnumerable<Vector> GenerateInterpolatedSequence()
        {
            const float StepNominal = 0.2F;
            const float StepMin = StepNominal / 3F;

            for (var i = 0; i < Count - 1; i++)
            {
                var curr = GetPoint(i + 0);
                var next = GetPoint(i + 1);

                var p0 = curr;
                var p1 = curr + GetInHandle(i);
                var p2 = next + GetOutHandle(i);
                var p3 = next;

                var t = 0F;

                // length of derivative curve
                var derivitiveLength = CurveTools.ApproximateLength(t => CurveTools.InterpolateDerivative(p0, p1, p2, p3, t), resolution: 8);

                while (t < 1F)
                {
                    // Emit interpolated point
                    yield return CurveTools.Interpolate(p0, p1, p2, p3, t);

                    // Compute derivative to step long the line in a non-linear fashion to enhance curve quiality
                    var derivative = CurveTools.InterpolateDerivative(p0, p1, p2, p3, t).Length / derivitiveLength;
                    t += Calc.Max(StepNominal * derivative, StepMin); // advance along line
                }
            }

            // Connect to final point
            yield return GetPoint(Count - 1);
        }

        private sealed class Segment
        {
            public Vector Point;
            public Vector InHandle;
            public Vector OutHandle;

            public Segment(Vector point, Vector inHandle, Vector outHandle)
            {
                Point = point;
                InHandle = inHandle;
                OutHandle = outHandle;
            }

            internal Vector Interpolate(float t, Vector nextPoint)
            {
                var p0 = Point;
                var p1 = Point + InHandle;
                var p2 = nextPoint + OutHandle;
                var p3 = nextPoint;

                return CurveTools.Interpolate(p0, p1, p2, p3, t);
            }

            internal Vector InterpolateDerivative(float t, Vector nextPoint)
            {
                var p0 = Point;
                var p1 = Point + InHandle;
                var p2 = nextPoint + OutHandle;
                var p3 = nextPoint;

                return CurveTools.InterpolateDerivative(p0, p1, p2, p3, t);
            }
        }
    }
}
