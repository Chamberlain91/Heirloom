using System;
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
    // todo: find shared edges, and skip them in collision checks?
    // This should help in the design of computing the contacts needed for collision response.
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
        /// Gets the list of convex partitions.
        /// If this polygon is already convex, there is only one convex partition that maps one-to-one with the original.
        /// </summary>
        public IReadOnlyList<IReadOnlyList<Vector>> ConvexPartitions
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
                    foreach (var indices in PolygonTools.DecomposeConvexIndices(_vertices))
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

                foreach (var convex in ConvexPartitions)
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
        public bool Contains(in Vector point)
        {
            // Check for containment in each convex fragment
            foreach (var convex in ConvexPartitions)
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
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public bool Overlaps(IShape shape)
        {
            // For each convex partition on this polygon,
            foreach (var partition in ConvexPartitions)
            {
                // check if this partition overlaps the shape.
                if (PolygonTools.Overlaps(partition, shape))
                {
                    // An overlap was detected
                    return true;
                }
            }

            // No overlap was detected
            return false;
        }

        /// <summary>
        /// Determines if this polygon overlaps the specified rectangle.
        /// </summary>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public bool Overlaps(in Rectangle rectangle)
        {
            // For each convex partition on this polygon,
            foreach (var partition in ConvexPartitions)
            {
                // check if this partition overlaps the shape.
                if (rectangle.Overlaps(partition))
                {
                    // An overlap was detected
                    return true;
                }
            }

            // No overlap was detected
            return false;
        }

        /// <summary>
        /// Determines if this polygon overlaps the specified circle.
        /// </summary>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public bool Overlaps(in Circle circle)
        {
            // For each convex partition on this polygon,
            foreach (var partition in ConvexPartitions)
            {
                // check if this partition overlaps the shape.
                if (circle.Overlaps(partition))
                {
                    // An overlap was detected
                    return true;
                }
            }

            // No overlap was detected
            return false;
        }

        /// <summary>
        /// Determines if this polygon overlaps the specified triangle.
        /// </summary>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public bool Overlaps(in Triangle triangle)
        {
            // For each convex partition on this polygon,
            foreach (var partition in ConvexPartitions)
            {
                // check if this partition overlaps the shape.
                if (triangle.Overlaps(partition))
                {
                    // An overlap was detected
                    return true;
                }
            }

            // No overlap was detected
            return false;
        }

        /// <summary>
        /// Determines if this polygon overlaps the specified triangle.
        /// </summary>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public bool Overlaps(IReadOnlyList<Vector> polygon)
        {
            // For each convex partition on this polygon,
            foreach (var partition in ConvexPartitions)
            {
                // check if this partition overlaps the shape.
                if (SeparatingAxis.Overlaps(polygon, partition))
                {
                    // An overlap was detected
                    return true;
                }
            }

            // No overlap was detected
            return false;
        }

        #endregion

        #region Axis Projection

        /// <summary>
        /// Project this polygon onto the specified axis.
        /// </summary>
        public Range Project(in Vector axis)
        {
            return PolygonTools.Project(_vertices, in axis);
        }

        #endregion

        #region Raycast

        /// <summary>
        /// Checks if a ray intersects this polygon.
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
        public bool Raycast(in Ray ray, out RayContact hit)
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
            return PolygonTools.Triangulate(_vertices);
        }

        #endregion

        #region Create (From Shape)

        /// <summary>
        /// Constructs a polygon representation of the specified shape.
        /// </summary>
        public static Polygon CreateFromShape(IShape shape)
        {
            return shape switch
            {
                Circle circle => CreateFromShape(in circle),
                Triangle triangle => CreateFromShape(in triangle),
                Rectangle rectangle => CreateFromShape(in rectangle),

                // Clones the input polygon
                Polygon polygon => new Polygon(polygon.Vertices),

                // Unable to create from shape
                _ => throw new InvalidOperationException("Unable to create polygon from shape, was not a known type."),
            };
        }

        /// <summary>
        /// Constructs a polygon representation of the specified triangle.
        /// </summary>
        public static Polygon CreateFromShape(in Triangle triangle)
        {
            return triangle.ToPolygon();
        }

        /// <summary>
        /// Constructs a polygon representation of the specified rectangle.
        /// </summary>
        public static Polygon CreateFromShape(in Rectangle rectangle)
        {
            return rectangle.ToPolygon();
        }

        /// <summary>
        /// Constructs a polygon representation of the specified circle.
        /// </summary>
        public static Polygon CreateFromShape(in Circle circle)
        {
            return circle.ToPolygon();
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

        #region Create (Regular Polygon)

        /// <summary>
        /// Construct a regular polygon.
        /// </summary>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Polygon CreateRegularPolygon(Vector center, int segments, float radius)
        {
            var points = GenerateRegularPolygon(center, segments, radius);
            return new Polygon(points);
        }

        /// <summary>
        /// Construct a regular polygon.
        /// </summary>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Polygon CreateRegularPolygon(int segments, float radius)
        {
            return CreateRegularPolygon(Vector.Zero, segments, radius);
        }

        #endregion

        #region Create (Star)

        /// <summary>
        /// Creates a polygon shaped like a standard 5 point star centered on the origin.
        /// </summary>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Polygon CreateStar(float radius)
        {
            return CreateStar(Vector.Zero, 5, radius);
        }

        /// <summary>
        /// Creates a polygon shaped like a standard 5 point star.
        /// </summary>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Polygon CreateStar(Vector center, float radius)
        {
            return CreateStar(center, 5, radius);
        }

        /// <summary>
        /// Creates a polygon shaped like a star centered on the origin.
        /// </summary>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Polygon CreateStar(int numPoints, float radius)
        {
            return CreateStar(Vector.Zero, numPoints, radius);
        }

        /// <summary>
        /// Creates a polygon shaped like a star.
        /// </summary>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Polygon CreateStar(Vector center, int numPoints, float radius)
        {
            return CreateStar(center, numPoints, radius * 0.6F, radius);
        }

        /// <summary>
        /// Creates a polygon shaped like a star.
        /// </summary>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Polygon CreateStar(Vector center, int numPoints, float innerRadius, float outerRadius)
        {
            var points = GenerateStar(center, numPoints, innerRadius, outerRadius);
            return new Polygon(points);
        }

        #endregion
    }
}
