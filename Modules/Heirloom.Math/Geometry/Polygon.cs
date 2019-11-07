using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;

namespace Heirloom.Math
{
    /// <summary>
    /// Represents a simple polygon, some operations assume the polygon is convex.
    /// </summary>
    public partial class Polygon : IPolygon
    {
        private readonly Vector[] _vertices;

        private bool _computedMetrics = false;
        private Vector _centroid, _center;
        private float _area;

        private bool _computedBounds = false;
        private Rectangle _bounds;

        private bool _computedConvexFragments = false;
        private List<ConvexPolygon> _convexFragments;

        private bool _computedIsConvex = false;
        private bool _isConvex;

        #region Constructors

        public Polygon(IEnumerable<Vector> vertices)
        {
            // Store a shallow clone of the vertices
            _vertices = vertices?.ToArray() ?? throw new ArgumentNullException(nameof(vertices));
        }

        #endregion

        #region Indexer

        public Vector this[int index] => _vertices[index];

        #endregion

        #region Properties

        /// <summary>
        /// Gets the number of vertices contained in this polygon.
        /// </summary>
        public int Count => _vertices.Length;

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
        /// Gets a value determining if this polygon is convex (in clockwise ordering).
        /// </summary>
        public bool IsConvex
        {
            get
            {
                if (!_computedIsConvex)
                {
                    _isConvex = PolygonTools.IsConvexPolygon(this);
                    _computedIsConvex = true;
                }

                return _isConvex;
            }
        }

        /// <summary>
        /// Gets the list of convex fragments.
        /// If this polygon is already convex, there is only one convex fragment that mimics the original.
        /// </summary>
        public IReadOnlyList<ConvexPolygon> ConvexFragments
        {
            get
            {
                if (!_computedConvexFragments)
                {
                    // Clear previous list (just to be safe)
                    _convexFragments = new List<ConvexPolygon>();

                    if (IsConvex)
                    {
                        // Create the convex mimic from this polygon
                        _convexFragments.Add(new ConvexPolygon(this));
                    }
                    else
                    {
                        // Compute fragments
                        foreach (var indices in PolygonTools.DecomposeConvexIndices(this))
                        {
                            // Create a convex fragment from this polygon
                            _convexFragments.Add(new ConvexPolygon(this, indices));
                        }
                    }

                    _computedConvexFragments = true;
                }

                return _convexFragments;
            }
        }

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

        public void Transform(Matrix matrix)
        {
            // Mark lazy values to be recomputed
            // TODO: Convex properties should be true after a linear transformation, right?
            // _computedConvexFragments = false;
            // _computedIsConvex = false;
            _computedMetrics = false;
            _computedBounds = false;

            // Transform vertices
            for (var i = 0; i < _vertices.Length; i++)
            {
                _vertices[i] = matrix * _vertices[i];
            }
        }

        #region Closest Point

        /// <summary>
        /// Gets the nearest point on the polygon to the specified point.
        /// </summary>
        public Vector GetClosestPoint(in Vector point)
        {
            // Is convex, simple test
            if (IsConvex) { return PolygonTools.GetClosestPoint(this, in point); }
            else
            {
                var nearestDistance = float.PositiveInfinity;
                var nearest = point;

                foreach (var convex in ConvexFragments)
                {
                    var v = convex.GetClosestPoint(in point);
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

        #region Contains

        /// <summary>
        /// Determines if the specified point is contained by this polygon.
        /// </summary>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public bool Contains(in Vector point)
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
            return PolygonTools.Overlaps(this, shape);
        }

        #endregion

        #region Raycast

        /// <summary>
        /// Checks if a ray intersects this shape.
        /// </summary>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public bool Raycast(in Ray ray)
        {
            return PolygonTools.Raycast(this, in ray, out _);
        }

        /// <summary>
        /// Checks if a ray intersects this polygon and outputs information on the contact point.
        /// </summary>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public bool Raycast(in Ray ray, out Contact hit)
        {
            return PolygonTools.Raycast(this, in ray, out hit);
        }

        #endregion

        #region Triangulate

        /// <summary>
        /// Decompose this polygon into triangles.
        /// </summary>
        public IEnumerable<Triangle> Triangulate()
        {
            // todo: would it be faster to triangle-fan the convex fragments if they've been computed already?
            foreach (var (a, b, c) in PolygonTools.DecomposeTrianglesIndices(this))
            {
                yield return new Triangle(this[a], this[b], this[c]);
            }
        }

        #endregion

        #region Enumerator

        public IEnumerator<Vector> GetEnumerator()
        {
            return ((IEnumerable<Vector>) _vertices).GetEnumerator();
        }

        IEnumerator IEnumerable.GetEnumerator()
        {
            return _vertices.GetEnumerator();
        }

        #endregion

        #region Decompose (IReadOnlyList<Vector>)

        /// <summary>
        /// Decomposes a simple polygon into constituent triangles.
        /// </summary>
        public static IEnumerable<Triangle> DecomposeTriangles(IReadOnlyList<Vector> poylgon)
        {
            // Convert triangulation to polygons
            return PolygonTools.DecomposeTrianglesIndices(poylgon)
                .Select(tri => new Triangle(poylgon[tri.a], poylgon[tri.b], poylgon[tri.c]));
        }

        /// <summary>
        /// Converts a simple polygon into one or more convex polygons.
        /// If the polygon is already convex, this simply clones it.
        /// </summary>
        public static IEnumerable<Polygon> DecomposeConvex(IReadOnlyList<Vector> polygon)
        {
            // Convert convex indices to polygons
            return PolygonTools.DecomposeConvexIndices(polygon)
                .Select(indices => new Polygon(indices.Select(i => polygon[i])));
        }

        /// <summary>
        /// Constructs a convex polygon representing the convex hull of the specified point cloud.
        /// </summary>
        /// <seealso cref="ConvexPolygon"/>
        public static Polygon CreateConvexHull(IEnumerable<Vector> points)
        {
            return new Polygon(PolygonTools.EnumerateConvexHull(points));
        }

        #endregion

        #region Create (Rectangle)

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Polygon CreateRectangle(Vector center, float width, float height)
        {
            return new Polygon(PolygonTools.GetRectanglePoints(center, width, height));
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

        #region Create (Star)

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Polygon CreateStar(Vector center, int numPoints, float radius)
        {
            return CreateStar(center, numPoints, radius * 0.66F, radius);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Polygon CreateStar(Vector center, int numPoints, float innerRadius, float outerRadius)
        {
            return new Polygon(PolygonTools.GetStarPoints(center, numPoints, innerRadius, outerRadius));
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Polygon CreateStar(int numPoints, float radius)
        {
            return CreateStar(Vector.Zero, numPoints, radius);
        }

        #endregion

        #region Create (Regular Polygon)

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Polygon CreateRegularPolygon(Vector center, int segments, float radius)
        {
            return new Polygon(PolygonTools.GetRegularPolygonPoints(center, segments, radius));
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Polygon CreateRegularPolygon(int segments, float radius)
        {
            return CreateRegularPolygon(Vector.Zero, segments, radius);
        }

        #endregion
    }
}
