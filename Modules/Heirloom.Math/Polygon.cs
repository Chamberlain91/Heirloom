﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;

using static Heirloom.Math.ProceduralShapes;

namespace Heirloom.Math
{
    /// <summary>
    /// Represents a simple polygon.
    /// </summary>
    public partial class Polygon : IShape
    {
        private readonly List<Vector> _vertices;

        private List<Vector[]> _convexPartitions;
        private bool _isConvex;

        private Rectangle _bounds;
        private Vector _centroid, _center;
        private float _area;

        private Dirty _dirty;

        [Flags]
        private enum Dirty
        {
            IsConvex = 1 << 0,
            ConvexPartitions = 1 << 1,
            Metrics = 1 << 2,
            Bounds = 1 << 3,

            Everything = IsConvex | ConvexPartitions | Metrics | Bounds
        }

        #region Constructors

        /// <summary>
        /// Constructs a new empty polygon.
        /// </summary>
        public Polygon()
        {
            _vertices = new List<Vector>();
        }

        /// <summary>
        /// Constructs a new simple polygon (assumes points are defined clockwise and describe non-crossing edges).
        /// </summary>
        public Polygon(IEnumerable<Vector> points) : this()
        {
            // Add the given points to the polygon
            foreach (var point in points ?? throw new ArgumentNullException(nameof(points)))
            {
                Add(point);
            }
        }

        #endregion

        #region Indexer

        public Vector this[int index]
        {
            get => _vertices[index];

            set
            {
                _dirty |= Dirty.Everything;
                _vertices[index] = value;
            }
        }

        #endregion

        #region Properties

        /// <summary>
        /// Gets a read-only view of this polygon's vertices.
        /// </summary>
        public IReadOnlyList<Vector> Vertices => _vertices;

        /// <summary>
        /// Gets the number of vertices in this polygon.
        /// </summary>
        public int Count => _vertices.Count;

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
                LazyComputeBounds();
                return _bounds;
            }
        }

        /// <summary>
        /// Gets a value determining if this polygon is convex (in clockwise ordering).
        /// </summary>
        public bool IsConvex
        {
            get
            {
                LazyComputeConvexStatus();
                return _isConvex;
            }
        }

        /// <summary>
        /// Gets the list of convex fragments.
        /// If this polygon is already convex, there is only one convex fragment that mimics the original.
        /// </summary>
        public IReadOnlyList<IReadOnlyList<Vector>> ConvexFragments
        {
            get
            {
                LazyComputeConvexPartitions();
                return _convexPartitions;
            }
        }

        #endregion

        #region Lazy Compute Parameters

        private void LazyComputeConvexStatus()
        {
            if (_dirty.HasFlag(Dirty.IsConvex))
            {
                _isConvex = PolygonTools.IsConvexPolygon(_vertices);
                _dirty &= ~Dirty.IsConvex;
            }
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        private void LazyComputeBounds()
        {
            if (_dirty.HasFlag(Dirty.Bounds))
            {
                // Lazy evaluation of bounds
                _bounds = Rectangle.FromPoints(_vertices);
                _dirty &= ~Dirty.Bounds;
            }
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        private void LazyComputeMetrics()
        {
            if (_dirty.HasFlag(Dirty.Metrics))
            {
                PolygonTools.ComputeMetrics(_vertices, out _area, out _center, out _centroid);
                _dirty &= ~Dirty.Metrics;
            }
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        private void LazyComputeConvexPartitions()
        {
            if (_dirty.HasFlag(Dirty.ConvexPartitions))
            {
                // Clear previous list (just to be safe)
                _convexPartitions = new List<Vector[]>();

                if (IsConvex)
                {
                    // Create the convex mimic from this polygon
                    _convexPartitions.Add(_vertices.ToArray());
                }
                else
                {
                    // Compute fragments
                    foreach (var indices in PolygonTools.DecomposeConvex(_vertices))
                    {
                        // Create a convex fragment from this polygon
                        var vertices = indices.Select(i => _vertices[i]);
                        _convexPartitions.Add(vertices.ToArray());
                    }
                }

                _dirty &= ~Dirty.ConvexPartitions;
            }
        }

        #endregion

        #region Vertex List (Clear, Add, Insert, RemoveAt)

        public void Clear()
        {
            _vertices.Clear();
            _dirty = Dirty.Everything;
        }

        public void Add(Vector item)
        {
            _vertices.Add(item);
            _dirty |= Dirty.Everything;
        }

        public void Insert(int index, Vector item)
        {
            _vertices.Insert(index, item);
            _dirty |= Dirty.Everything;
        }

        public void RemoveAt(int index)
        {
            _dirty |= Dirty.Everything;
            _vertices.RemoveAt(index);
        }

        #endregion

        #region Closest Point

        /// <summary>
        /// Gets the nearest point on the polygon to the specified point.
        /// </summary>
        public Vector GetClosestPoint(in Vector point)
        {
            // Is convex, simple test
            if (IsConvex) { return PolygonTools.GetClosestPoint(_vertices, in point); }
            else
            {
                var nearestDistance = float.PositiveInfinity;
                var nearest = point;

                foreach (var convex in ConvexFragments)
                {
                    var v = PolygonTools.GetClosestPoint(convex, in point);
                    var d = Vector.DistanceSquared(in v, in point);

                    // Keep nearest found
                    if (d < nearestDistance)
                    {
                        nearestDistance = d;
                        nearest = v;
                    }
                }

                return nearest;
            }
        }

        #endregion

        #region Contains Point

        /// <summary>
        /// Determines if the specified point is contained by this polygon.
        /// </summary>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public bool ContainsPoint(in Vector point)
        {
            // Check for containment in each convex fragment
            foreach (var convex in ConvexFragments)
            {
                if (PolygonTools.ContainsPoint(convex, in point)) { return true; }
            }

            return false;
        }

        #endregion

        #region Overlaps

        /// <summary>
        /// Checks for an overlap between this polygon and another shape.
        /// </summary>
        public bool Overlaps(IShape shape)
        {
            return PolygonTools.Overlaps(_vertices, shape);
        }

        #endregion

        #region Raycast

        /// <summary>
        /// Checks if a ray intersects this shape.
        /// </summary>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public bool Raycast(in Ray ray)
        {
            return PolygonTools.Raycast(_vertices, in ray, out _);
        }

        /// <summary>
        /// Checks if a ray intersects this polygon and outputs information on the contact point.
        /// </summary>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public bool Raycast(in Ray ray, out Contact hit)
        {
            return PolygonTools.Raycast(_vertices, in ray, out hit);
        }

        #endregion

        #region Triangulate

        /// <summary>
        /// Decompose this polygon into triangles.
        /// </summary>
        public IEnumerable<Triangle> Triangulate()
        {
            // todo: would it be faster to triangle-fan the convex fragments if they've been computed already?
            foreach (var (a, b, c) in PolygonTools.Triangulate(_vertices))
            {
                yield return new Triangle(this[a], this[b], this[c]);
            }
        }

        #endregion

        #region Decompose (IReadOnlyList<Vector>)

        /// <summary>
        /// Decomposes a simple polygon into constituent triangles.
        /// </summary>
        public static IEnumerable<Triangle> Triangulate(IReadOnlyList<Vector> poylgon)
        {
            // Convert triangulation to polygons
            return PolygonTools.Triangulate(poylgon)
                               .Select(tri => new Triangle(poylgon[tri.a], poylgon[tri.b], poylgon[tri.c]));
        }

        /// <summary>
        /// Converts a simple polygon into one or more convex polygons.
        /// If the polygon is already convex, this simply clones it.
        /// </summary>
        public static IEnumerable<Polygon> DecomposeConvex(IReadOnlyList<Vector> polygon)
        {
            // Convert convex indices to polygons
            return PolygonTools.DecomposeConvex(polygon)
                               .Select(indices => new Polygon(indices.Select(i => polygon[i])));
        }

        #endregion

        #region Create (Rectangle)

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Polygon CreateRectangle(Vector center, float width, float height)
        {
            return new Polygon(GenerateRectangle(center, width, height));
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Polygon CreateRectangle(float width, float height)
        {
            return CreateRectangle(Vector.Zero, width, height);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Polygon CreateRectangle(Rectangle rect)
        {
            return CreateRectangle(rect.Center, rect.Width, rect.Height);
        }

        #endregion

        #region Create (Regular Polygon)

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Polygon CreateRegularPolygon(Vector center, int segments, float radius)
        {
            return new Polygon(GenerateRegularPolygon(center, segments, radius));
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Polygon CreateRegularPolygon(int segments, float radius)
        {
            return CreateRegularPolygon(Vector.Zero, segments, radius);
        }

        #endregion

        #region Create (Star)

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Polygon CreateStar(Vector center, int numPoints, float radius)
        {
            return CreateStar(center, numPoints, radius * 0.66F, radius);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Polygon CreateStar(Vector center, int numPoints, float innerRadius, float outerRadius)
        {
            return new Polygon(GenerateStar(center, numPoints, innerRadius, outerRadius));
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Polygon CreateStar(int numPoints, float radius)
        {
            return CreateStar(Vector.Zero, numPoints, radius);
        }

        #endregion

        #region Create (Convex Hull)

        /// <summary>
        /// Constructs a convex polygon representing the convex hull of the specified point cloud.
        /// </summary>
        public static Polygon CreateConvexHull(IEnumerable<Vector> points)
        {
            return new Polygon(PolygonTools.ComputeConvexHull(points));
        }

        #endregion
    }
}
