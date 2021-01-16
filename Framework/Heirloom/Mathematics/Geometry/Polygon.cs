using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;

namespace Heirloom.Mathematics
{
    /// <summary>
    /// Represents a simple polygon.
    /// </summary>
    public partial class Polygon : IShape, IReadOnlyPolygon
    // todo: find shared edges, and skip them in collision checks?
    // todo: IReadOnlyPolygon
    // This should help in the design of computing the contacts needed for collision response.
    {
        private readonly List<Vector> _vertices;
        private readonly List<Vector> _normals;
        private readonly VerticesWrapper _vertexWrapper;

        // todo: class ConvexPartition : IShape
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
            Normals = 1 << 4,

            Everything = IsConvex | ConvexPartitions | Metrics | Bounds | Normals
        }

        #region Constructors

        /// <summary>
        /// Constructs a new empty polygon.
        /// </summary>
        public Polygon()
        {
            _vertices = new List<Vector>();
            _normals = new List<Vector>();

            _vertexWrapper = new VerticesWrapper(this);
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

        #region Properties

        /// <summary>
        /// Gets the list vertices on this polygon.
        /// </summary>
        /// <remarks>
        /// Note: Adjusting the vertices will invalidate cached properties (normals, convexity, triangulation, etc).
        /// </remarks>
        public IList<Vector> Vertices => _vertexWrapper;

        IReadOnlyList<Vector> IReadOnlyPolygon.Vertices => _vertices;

        /// <summary>
        /// Gets a read-only view of the polygon's normals.
        /// </summary>
        public IReadOnlyList<Vector> Normals
        {
            get
            {
                LazyComputeNormals();
                return _normals;
            }
        }

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

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        private void LazyComputeNormals()
        {
            if (_dirty.HasFlag(Dirty.Normals))
            {
                _normals.Clear();

                // Compute normals
                for (var i = 0; i < Vertices.Count; i++)
                {
                    var normal = PolygonTools.GetNormal(_vertices, i);
                    _normals.Add(normal);
                }

                _dirty &= ~Dirty.Normals;
            }
        }

        #endregion

        #region Vertex List (Clear, Add, Insert, RemoveAt)

        /// <summary>
        /// Removes all vertices from the polygon.
        /// </summary>
        public void Clear()
        {
            _vertices.Clear();
            _dirty = Dirty.Everything;
        }

        /// <summary>
        /// Adds a vertex to the end of polygon's vertex list.
        /// </summary>
        /// <param name="item">The vertex to add.</param>
        public void Add(Vector item)
        {
            _vertices.Add(item);
            _dirty |= Dirty.Everything;
        }

        /// <summary>
        /// Inserts a vertex into the polygon's vertex list.
        /// </summary>
        /// <param name="index">The place in the vertex list to insert the vertex.</param>
        /// <param name="item">The vertex to insert.</param>
        public void Insert(int index, Vector item)
        {
            _vertices.Insert(index, item);
            _dirty |= Dirty.Everything;
        }

        /// <summary>
        /// Removes a vertex from the polygon's vertex list.
        /// </summary>
        /// <param name="index">The index of the vertex to remove.</param>
        public void RemoveAt(int index)
        {
            _dirty |= Dirty.Everything;
            _vertices.RemoveAt(index);
        }

        #endregion

        #region Nearest Point / Support

        /// <summary>
        /// Gets the nearest point on the polygon to the specified point.
        /// </summary>
        public Vector GetNearestPoint(Vector point)
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

        /// <inheritdoc/>
        public Vector GetSupport(Vector direction)
        {
            return PolygonTools.GetSupport(_vertices, direction);
        }

        #endregion

        #region Contains Point

        /// <summary>
        /// Determines if the specified point is contained by this polygon.
        /// </summary>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public bool Contains(Vector point)
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
            return Collision.CheckOverlap(this, shape);
        }

        #endregion

        #region Raycast

        /// <summary>
        /// Checks if a ray intersects this polygon.
        /// </summary>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public bool Raycast(Ray ray)
        {
            return PolygonTools.Raycast(_vertices, in ray, out _);
        }

        /// <summary>
        /// Checks if a ray intersects this polygon and outputs information on the contact point.
        /// </summary>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public bool Raycast(Ray ray, out RayContact hit)
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
            return PolygonTools.ComputeConvexHull(points);
        }

        #endregion

        #region Clip Polygon

        public static IEnumerable<Vector> Clip(IEnumerable<Vector> polygon, IEnumerable<Vector> clipPolygon)
        {
            return Clip(polygon, GetEdges(clipPolygon));
        }

        internal static IEnumerable<Vector> Clip(IEnumerable<Vector> polygon, IEnumerable<(Vector, Vector)> clipEdges)
        {
            var outputList = new List<Vector>(polygon);

            foreach (var (clipA, clipB) in clipEdges)
            {
                // todo: is there a good way to optimize the use of lists?
                var inputList = new List<Vector>(outputList);
                outputList.Clear();

                for (var i = 0; i < inputList.Count; i += 1)
                {
                    var current = inputList[i];
                    var previous = inputList[(i + inputList.Count - 1) % inputList.Count];

                    // Determine inside/outside via assumption of clockwise ordering
                    var clipEdge = clipB - clipA;

                    if (Vector.Cross(in clipEdge, current - clipA) >= 0)
                    {
                        // previous vertex is outside
                        if (Vector.Cross(in clipEdge, previous - clipA) < 0)
                        {
                            // Edge is clipped by prior vertex.
                            LineSegment.Intersects(previous, current, clipA, clipB, out Vector intersection, clampSegment: false);
                            outputList.Add(intersection);
                        }

                        // current vertex is contained
                        outputList.Add(current);
                    }
                    else if (Vector.Cross(in clipEdge, previous - clipA) >= 0)
                    {
                        // current vertex is outside
                        LineSegment.Intersects(previous, current, clipA, clipB, out Vector intersection, clampSegment: false);
                        outputList.Add(intersection);
                    }
                }
            }

            return outputList;
        }

        public static IEnumerable<(Vector, Vector)> GetEdges(IEnumerable<Vector> polygon)
        {
            var first = default(Vector);
            var prior = default(Vector);
            var hasFirst = false;

            foreach (var vertex in polygon)
            {
                if (hasFirst) { yield return (prior, vertex); }
                else
                {
                    hasFirst = true;
                    first = vertex;
                }

                prior = vertex;
            }

            yield return (prior, first);
        }

        #endregion

        /// <summary>
        /// Used to invalidate cached properties when the vertex list is manipulated.
        /// </summary>
        private sealed class VerticesWrapper : IList<Vector>
        {
            private readonly Polygon _polygon;

            public VerticesWrapper(Polygon polygon)
            {
                _polygon = polygon ?? throw new ArgumentNullException(nameof(polygon));
            }

            public bool IsReadOnly => ((ICollection<Vector>) _polygon._vertices).IsReadOnly;

            public int Count => _polygon._vertices.Count;

            public int IndexOf(Vector item)
            {
                return _polygon._vertices.IndexOf(item);
            }

            public void Insert(int index, Vector item)
            {
                _polygon._dirty |= Dirty.Everything;
                _polygon._vertices.Insert(index, item);
            }

            public void RemoveAt(int index)
            {
                _polygon._dirty |= Dirty.Everything;
                _polygon._vertices.RemoveAt(index);
            }

            public Vector this[int index]
            {
                get => _polygon._vertices[index];

                set
                {
                    _polygon._dirty |= Dirty.Everything;
                    _polygon._vertices[index] = value;
                }
            }

            public void Add(Vector item)
            {
                _polygon._dirty |= Dirty.Everything;
                _polygon._vertices.Add(item);
            }

            public void Clear()
            {
                _polygon._dirty |= Dirty.Everything;
                _polygon._vertices.Clear();
            }

            public bool Contains(Vector item)
            {
                return _polygon._vertices.Contains(item);
            }

            public void CopyTo(Vector[] array, int arrayIndex)
            {
                _polygon._vertices.CopyTo(array, arrayIndex);
            }

            public bool Remove(Vector item)
            {
                if (_polygon._vertices.Remove(item))
                {
                    _polygon._dirty |= Dirty.Everything;
                    return true;
                }
                else
                {
                    return false;
                }
            }

            public IEnumerator<Vector> GetEnumerator()
            {
                return _polygon._vertices.GetEnumerator();
            }

            System.Collections.IEnumerator System.Collections.IEnumerable.GetEnumerator()
            {
                return _polygon._vertices.GetEnumerator();
            }
        }
    }
}
