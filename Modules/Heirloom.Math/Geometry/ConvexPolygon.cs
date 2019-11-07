using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;

namespace Heirloom.Math
{
    /// <summary>
    /// Represents a convex polygon, may represent a convex hull of points or a convex partition of a <see cref="Polygon"/>.
    /// </summary>
    public sealed class ConvexPolygon : IPolygon
    {
        private bool _computedMetrics = false;
        private Vector _centroid, _center;
        private float _area;

        private bool _computedBounds = false;
        private Rectangle _bounds;

        #region Constructors 

        public ConvexPolygon(IEnumerable<Vector> points)
            : this(Polygon.CreateConvexHull(points))
        { }

        internal ConvexPolygon(Polygon polygon)
            : this(polygon, Enumerable.Range(0, polygon.Count))
        { }

        internal ConvexPolygon(Polygon polygon, IEnumerable<int> indices)
        {
            Polygon = polygon ?? throw new ArgumentNullException(nameof(polygon));
            Indices = indices?.ToArray() ?? throw new ArgumentNullException(nameof(indices));
        }

        #endregion

        #region Indexer

        /// <summary>
        /// Gets the nth vertex using <see cref="Indices"/> and <see cref="Polygon"/>.
        /// </summary>
        public Vector this[int index]
            => Polygon[Indices[Calc.Wrap(index, Indices.Count)]];

        #endregion

        #region Properties

        /// <summary>
        /// Number of indices in this convex polygon.
        /// </summary>
        public int Count => Indices.Count;

        /// <summary>
        /// The representation of this convex hull by indexing into <see cref="Polygon"/>.
        /// </summary>
        public IReadOnlyList<int> Indices { get; }

        /// <summary>
        /// The original polygon this convex hull is associated with, may map 1:1
        /// </summary>
        public Polygon Polygon { get; }

        /// <summary>
        /// Gets the area of the polygon.
        /// </summary>
        public float Area
        {
            get
            {
                LazyComputeMetrics();
                return _area;
            }
        }

        /// <summary>
        /// Gets the center (point mean) of the polygon.
        /// </summary>
        public Vector Center
        {
            get
            {
                LazyComputeMetrics();
                return _center;
            }
        }

        /// <summary>
        /// Gets the centroid (area weighted) of the polygon.
        /// </summary>
        public Vector Centroid
        {
            get
            {
                LazyComputeMetrics();
                return _centroid;
            }
        }

        /// <summary>
        /// Gets the bounding rectangle of this polygon.
        /// </summary>
        public Rectangle Bounds
        {
            get
            {
                if (!_computedBounds)
                {
                    // Lazy evaluation of bounds
                    _bounds = Rectangle.FromPoints(this);
                    _computedBounds = true;
                }

                return _bounds;
            }
        }

        /// <summary>
        /// Gets a value determining if this polygon is convex. 
        /// This type (ie, <see cref="ConvexPolygon"/>) is, by definition, always convex.
        /// </summary>
        public bool IsConvex => true;

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        private void LazyComputeMetrics()
        {
            if (_computedMetrics)
            {
                PolygonTools.ComputeMetrics(this, out _area, out _center, out _centroid);
                _computedMetrics = true;
            }
        }

        #endregion

        #region Closest Point

        /// <summary>
        /// Gets the nearest point on the polygon to the specified point.
        /// </summary>
        public Vector GetClosestPoint(in Vector point)
        {
            return PolygonTools.GetClosestPoint(this, point);
        }

        #endregion

        #region Contains

        /// <summary>
        /// Determines if the specified point is contained by this polygon.
        /// </summary>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public bool Contains(in Vector point)
        {
            return PolygonTools.ContainsPoint(this, point);
        }

        #endregion

        #region Overlaps

        public bool Overlaps(IShape shape)
        {
            return PolygonTools.Overlaps(this, shape);
        }

        #endregion

        #region Raycast

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public bool Raycast(in Ray ray)
        {
            return PolygonTools.Raycast(this, in ray, out _);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public bool Raycast(in Ray ray, out Contact hit)
        {
            return PolygonTools.Raycast(this, in ray, out hit);
        }

        #endregion

        internal void MarkLazyRecompute()
        {
            _computedMetrics = false;
            _computedBounds = false;
        }

        public IEnumerator<Vector> GetEnumerator()
        {
            return Indices.Select(i => Polygon[i])
                .GetEnumerator();
        }

        IEnumerator IEnumerable.GetEnumerator()
        {
            return GetEnumerator();
        }
    }
}
