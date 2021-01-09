using System;
using System.Collections.Generic;

namespace Heirloom
{
    /// <summary>
    /// An implementation of a multi-point bezier curve using multiple 'segments' of simple curves.
    /// </summary>
    public sealed class Curve
    {
        private readonly List<Segment> _segments;

        /// <summary>
        /// Constructs a new instance of <see cref="Curve"/>.
        /// </summary>
        public Curve()
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
        public void Add(Vector controlPoint, Vector inHandle, Vector outHandle, CurveType type = CurveType.Cubic)
        {
            var segment = new Segment(controlPoint, inHandle, outHandle, type);
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
        public void Insert(int index, Vector controlPoint, Vector inHandle, Vector outHandle, CurveType type = CurveType.Cubic)
        {
            var s = new Segment(controlPoint, inHandle, outHandle, type);
            _segments.Insert(index, s);
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
        /// Sets the type of curve of the segment following the point at the specified index.
        /// </summary>
        /// <param name="index">The index of the point.</param>
        /// <param name="type">The type of curve the segment will act like.</param>
        /// <exception cref="IndexOutOfRangeException">If the <paramref name="index"/> is less than zero or greater than or equal to <see cref="Count"/>.</exception>
        public void SetCurveType(int index, CurveType type)
        {
            _segments[index].CurveType = type;
        }

        /// <summary>
        /// Gets the type of curve of the segment following the point at the specified index.
        /// </summary>
        /// <param name="index">The index of the point.</param>
        /// <returns>The type of curve the segment acts like.</returns>
        /// <exception cref="IndexOutOfRangeException">If the <paramref name="index"/> is less than zero or greater than or equal to <see cref="Count"/>.</exception>
        public CurveType GetCurveType(int index)
        {
            return _segments[index].CurveType;
        }

        private class Segment
        {
            public Vector Point;
            public Vector InHandle;
            public Vector OutHandle;
            public CurveType CurveType;

            public Segment(Vector point, Vector inHandle, Vector outHandle, CurveType curveType)
            {
                Point = point;
                InHandle = inHandle;
                OutHandle = outHandle;
                CurveType = curveType;
            }

            internal Vector Interpolate(float t, Vector nextPoint)
            {
                return CurveType switch
                {
                    CurveType.Cubic => CurveTools.Interpolate(Point, Point + InHandle, nextPoint + OutHandle, nextPoint, t),
                    CurveType.Quadratic => CurveTools.Interpolate(Point, Point + InHandle, nextPoint, t),
                    CurveType.Linear => Vector.Lerp(Point, nextPoint, t),
                    CurveType.Stepped => Point,

                    _ => throw new ArgumentException($"Unable to interpolate, invalid curve type.", nameof(CurveType)),
                };
            }

            internal Vector InterpolateDerivative(float t, Vector nextPoint)
            {
                return CurveType switch
                {
                    CurveType.Cubic => CurveTools.InterpolateDerivative(Point, Point + InHandle, nextPoint + OutHandle, nextPoint, t),
                    CurveType.Quadratic => CurveTools.InterpolateDerivative(Point, Point + InHandle, nextPoint, t),
                    CurveType.Linear => nextPoint - Point,
                    CurveType.Stepped => Vector.Zero,

                    _ => throw new ArgumentException($"Unable to interpolate, invalid curve type.", nameof(CurveType)),
                };
            }
        }
    }
}
