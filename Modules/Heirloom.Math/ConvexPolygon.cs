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
        #region Constructors 

        public ConvexPolygon(IEnumerable<Vector> points)
            : this(Polygon.CreateConvexHull(points))
        { }

        private ConvexPolygon(Polygon polygon)
            : this(polygon, Enumerable.Range(0, polygon.Count))
        { }

        internal ConvexPolygon(Polygon polygon, IEnumerable<int> indices)
        {
            Polygon = polygon ?? throw new ArgumentNullException(nameof(polygon));
            Indices = indices?.ToArray() ?? throw new ArgumentNullException(nameof(indices));

            // 
            Polygon.ComputeMetrics(this, out var area, out var center, out var centroid);

            Area = area;
            Center = center;
            Centroid = centroid;

            Bounds = Rectangle.FromPoints(this);
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

        public float Area { get; }

        public Vector Center { get; }

        public Vector Centroid { get; }

        public Rectangle Bounds { get; }

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

        #endregion

        #region Closest Point

        public Vector ClosestPoint(in Vector point)
        {
            return Polygon.ClosestPoint(this, point);
        }

        #endregion

        #region Contains

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public bool Contains(in Vector point)
        {
            return Polygon.ContainsPoint(this, point);
        }

        #endregion

        #region Overlaps

        public bool Overlaps(IShape shape)
        {
            return Polygon.Overlaps(this, shape);
        }

        #endregion

        #region Raycast

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public bool Raycast(in Ray ray)
        {
            return Polygon.Raycast(this, in ray, out _);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public bool Raycast(in Ray ray, out Contact hit)
        {
            return Polygon.Raycast(this, in ray, out hit);
        }

        #endregion

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
