using System;
using System.Collections.Generic;

namespace Heirloom.Geometry
{
    public sealed class Curve
    {
        private readonly List<Segment> _segments;

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
        /// Computes the interpolated point.
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
        /// <param name="segment">Some index within the curve.</param>
        public void RemoveAt(int segment)
        {
            _segments.RemoveAt(segment);
        }

        public Vector GetPoint(int segment)
        {
            return _segments[segment].Point;
        }

        public void SetPoint(int segment, Vector point)
        {
            _segments[segment].Point = point;
        }

        public Vector GetInHandle(int segment)
        {
            return _segments[segment].InHandle;
        }

        public void SetInHandle(int segment, Vector handle)
        {
            _segments[segment].InHandle = handle;
        }

        public Vector GetOutHandle(int segment)
        {
            return _segments[segment].OutHandle;
        }

        public void SetOutHandle(int segment, Vector handle)
        {
            _segments[segment].OutHandle = handle;
        }

        public void SetCurveType(int segment, CurveType type)
        {
            _segments[segment].CurveType = type;
        }

        public CurveType GetCurveType(int segment)
        {
            return _segments[segment].CurveType;
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
