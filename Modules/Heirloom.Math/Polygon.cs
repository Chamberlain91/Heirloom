using System;
using System.Collections;
using System.Collections.Generic;
using System.Diagnostics;
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

        #region Constructors

        public Polygon(IEnumerable<Vector> vertices)
        {
            // Store vertices
            _vertices = vertices?.ToArray() ?? throw new ArgumentNullException(nameof(vertices));

            // 
            ComputeMetrics(this, out var area, out var center, out var centroid);

            Area = area;
            Center = center;
            Centroid = centroid;

            Bounds = Rectangle.FromPoints(this);
        }

        #endregion

        #region Indexer

        public Vector this[int index] => _vertices[index];

        #endregion

        #region Properties

        /// <summary>
        /// Area of the polygon.
        /// </summary>
        public float Area { get; }

        public Vector Centroid { get; }

        public Vector Center { get; }

        public Rectangle Bounds { get; }

        /// <summary>
        /// The number of vertices in this polygon.
        /// </summary>
        public int Count => _vertices.Length;

        #endregion

        #region Closest Point

        public Vector ClosestPoint(in Vector point)
        {
            return ClosestPoint(this, in point);
        }

        #endregion

        #region Contains

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public bool Contains(in Vector point)
        {
            return ContainsPoint(this, point);
        }

        #endregion

        #region Overlaps

        public bool Overlaps(IShape shape)
        {
            return Overlaps(this, shape);
        }

        #endregion

        #region Raycast

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public bool Raycast(in Ray ray)
        {
            return Raycast(this, in ray, out _);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public bool Raycast(in Ray ray, out Contact hit)
        {
            return Raycast(this, in ray, out hit);
        }

        #endregion

        #region Decompose

        /// <summary>
        /// Decompose this polygon into convex parts.
        /// If this polygon is already convex, this will emit a single <see cref="ConvexPolygon"/> that maps 1:1 to this polygon.
        /// </summary>
        public IEnumerable<ConvexPolygon> Decompose()
        {
            foreach (var indices in DecomposeConvexIndices(this))
            {
                yield return new ConvexPolygon(this, indices);
            }
        }

        /// <summary>
        /// Decompose this polygon into triangles.
        /// </summary>
        public IEnumerable<Triangle> Triangulate()
        {
            foreach (var (a, b, c) in DecomposeTrianglesIndices(this))
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

        #region Convex Check

        /// <summary>
        /// Determines if the ith vertex is a convex (clockwise) vertex.
        /// </summary>
        public static bool IsConvex(IReadOnlyList<Vector> polygon, int i)
        {
            var p = GetVertex(polygon, i - 1);
            var c = GetVertex(polygon, i + 0);
            var n = GetVertex(polygon, i + 1);

            return IsConvex(p, c, n);
        }

        /// <summary>
        /// Determines if the polygon is considered convex (non-concave and oriented clockwise).
        /// </summary>
        public static bool IsConvex(IReadOnlyList<Vector> polygon)
        {
            for (var i = 0; i < polygon.Count; i++)
            {
                if (!IsConvex(polygon, i))
                {
                    return false;
                }
            }

            return true;
        }

        /// <summary>
        /// Determines if the vertex '<paramref name="c"/>' is convex (clockwise).
        /// </summary>
        public static bool IsConvex(in Vector p, in Vector c, in Vector n)
        {
            // Does it have to be normalized?
            // var d1 = Vector.Normalize(c - p);
            // var d2 = Vector.Normalize(n - c);

            var d1 = c - p;
            var d2 = n - c;

            // equivalent to d2.PerpendicularCCW?
            var n2 = new Vector(-d2.Y, d2.X);

            return Vector.Dot(d1, n2) <= 0f;
        }

        #endregion

        #region Triangle Decomposition

        /// <summary>
        /// Decomposes a simple polygon into constituent triangles.
        /// </summary>
        public static IEnumerable<Polygon> DecomposeTriangles(IReadOnlyList<Vector> poylgon)
        {
            // Convert triangulation to polygons
            return DecomposeTrianglesIndices(poylgon)
                .Select(tri => new Polygon(new[] {
                    poylgon[tri.a], poylgon[tri.b], poylgon[tri.c]
                }));
        }

        /// <summary>
        /// Decomposes a simple polygon into constituent triangles enumerated by indices of the original polygon.
        /// </summary>
        public static IEnumerable<(int a, int b, int c)> DecomposeTrianglesIndices(IEnumerable<Vector> polygon)
        {
            var points = new List<Vector>(polygon);
            var pointsMap = new List<int>(Enumerable.Range(0, points.Count));
            var earIndex = 0;

            // 
            while (points.Count >= 3)
            {
                // Find next ear
                if (FindNextEar())
                {
                    // Extract triangle
                    yield return (
                        pointsMap[Calc.Wrap(earIndex - 1, points.Count)],
                        pointsMap[Calc.Wrap(earIndex + 0, points.Count)],
                        pointsMap[Calc.Wrap(earIndex + 1, points.Count)]
                    );

                    // Clip ear
                    pointsMap.RemoveAt(earIndex);
                    points.RemoveAt(earIndex);
                }
                else
                {
                    throw new InvalidOperationException("Unable to find ear");
                }
            }

            bool FindNextEar()
            {
                earIndex += 2; // Arbitrarily offset index each time
                // This *seems* to produce better convex merging for some reason?
                // TODO: Talk to Jarrod or Dr. Kiel about why this may be the case?

                // Enumerate only the polygon worth of points
                for (var o = 0; o < points.Count; o++)
                {
                    // Wrap i to polygon range
                    var w = Calc.Wrap(earIndex + o, points.Count);

                    // 
                    if (IsEar(w))
                    {
                        earIndex = w;
                        return true;
                    }
                }

                return false;
            }

            /// <summary>
            /// Determines if the ith point is an 'ear' (convex and does not overlap vertices)
            /// </summary>
            bool IsEar(int i)
            {
                // Triangle is always an ear (always convex and non-overlapping)
                if (points.Count == 3) { return true; }

                // Line or Point aren't polygons
                if (points.Count < 3) { return false; }

                // An ear must be convex
                if (IsConvex(points, i))
                {
                    // The vertices of the test triangle
                    var c = GetVertex(points, i + 0);
                    var p = GetVertex(points, i - 1);
                    var n = GetVertex(points, i + 1);

                    // 
                    for (var q = 0; q < points.Count; q++)
                    {
                        // Avoid checking if containing test triangle
                        if (q == Calc.Wrap(i + 0, points.Count)) { continue; }
                        if (q == Calc.Wrap(i - 1, points.Count)) { continue; }
                        if (q == Calc.Wrap(i + 1, points.Count)) { continue; }

                        // Does the test triangle contain the test point
                        if (Triangle.ContainsPoint(p, c, n, points[q]))
                        {
                            // Overlaps somewhere
                            return false;
                        }
                    }

                    // Is an ear
                    return true;
                }

                // No valid candidate
                return false;
            }
        }

        #endregion

        #region Convex Decomposition

        /// <summary>
        /// Converts a simple polygon into one or more convex polygons.
        /// If the polygon is already convex, this simply clones it.
        /// </summary>
        public static IEnumerable<Polygon> DecomposeConvex(IReadOnlyList<Vector> polygon)
        {
            // Convert convex indices to polygons
            return DecomposeConvexIndices(polygon)
                .Select(indices => new Polygon(indices.Select(i => polygon[i])));
        }

        /// <summary>
        /// Converts a simple polygon into one or more convex polygons enumerated by indices of the original polygon.
        /// </summary>
        public static IEnumerable<IReadOnlyList<int>> DecomposeConvexIndices(IReadOnlyList<Vector> points)
        {
            // Check if already convex
            if (IsConvex(points)) // O(N)
            {
                // Clone this polygon (its already convex)
                yield return Enumerable.Range(0, points.Count).ToList();
            }
            // Not convex, we will break he polygon into triangles and
            // then attempt to merge the triangles back into convex bodies
            else
            {
                // The set of triangles
                var triangles = new List<(int a, int b, int c)>(DecomposeTrianglesIndices(points));

                // The set of polygons generated
                var polygons = new List<List<int>>();

                // While we have triangles
                while (triangles.Count > 0) // O(logN) ... O(N) ... Seems like its a minimum to the number of triangles/reflex points.
                {
                    var merge = false;

                    // Now try to merge every triangle we have
                    for (var t = triangles.Count - 1; t >= 0; t--) // O(N)
                    {
                        var (a, b, c) = triangles[t];

                        // For each known polygon, try merging
                        foreach (var poly in polygons) // O(M)
                        {
                            // Try to merge polygon, mark success and exit loop.
                            if (TryMergeTriangle(poly, a, b, c)) // O(1)
                            {
                                // Was able to merge the triangle
                                triangles.RemoveAt(t);
                                merge = true;

                                // Leave polygon loop, merged triangle
                                break;
                            }
                        }
                    }

                    // If no merge occured, we need a new polygon to grow from,
                    // so we will forcefully take the last available polygon.
                    // (because removing from the end of a list should be faster IIRC)
                    if (!merge && triangles.Count > 0)
                    {
                        // Get and remove a triangle
                        var last = triangles.Count - 1;
                        var (ta, tb, tc) = triangles[last];
                        triangles.RemoveAt(last);

                        // Append polygon into merge set
                        polygons.Add(new List<int> { ta, tb, tc });
                    }
                }

                // Emit all generated polygons
                foreach (var polygon in polygons)
                {
                    yield return polygon;
                }
            }

            /**
             * Attempts to merge a triangle into the given polygon.
             */
            bool TryMergeTriangle(List<int> polygon, int a, int b, int c)
            {
                // Is empty merge set, add triangle
                if (polygon.Count >= 0)
                {
                    // Find where to insert
                    if (FindSharedEdge(polygon, a, b, c, out var mi, out var vi))
                    {
                        // Potential reflex vertex 1 (left)
                        var vi_a = polygon[Calc.Wrap(mi - 1, polygon.Count)];
                        var vi_b = polygon[mi];
                        var vi_c = vi;

                        var mj = Calc.Wrap(mi + 1, polygon.Count); // Next in sequence

                        // Potential reflex vertex 2 (right)
                        var vj_a = vi;
                        var vj_b = polygon[mj];
                        var vj_c = polygon[Calc.Wrap(mj + 1, polygon.Count)];

                        // Will inserting the point cause the polygon to remain convex?
                        if (IsConvex(points[vi_a], points[vi_b], points[vi_c]) && IsConvex(points[vj_a], points[vj_b], points[vj_c]))
                        {
                            // Insert vertex into merge shape
                            polygon.Insert(mj, vi);
                            return true;
                        }
                    }
                }

                // Was not able to merge triangle into polygon
                return false;
            }
        }

        /// <summary>
        /// Finds a shared edge in the given polygon, returning true when a shared edge can be found.
        /// </summary>
        /// <param name="polygon">Some polygon represented as indices of a vertex set.</param>
        /// <param name="a">First vertex index of the triangle.</param>
        /// <param name="b">Second vertex index of the triangle.</param>
        /// <param name="c">Third vertex index of the triangle.</param>
        /// <param name="mi">The position within <paramref name="polygon"/> to insert <paramref name="vMerge"/> after.</param>
        /// <param name="vMerge">The vertex index to insert after <paramref name="mi"/>.</param>
        private static bool FindSharedEdge(List<int> polygon, int a, int b, int c, out int mi, out int vMerge) // O(M)
        {
            for (mi = 0; mi < polygon.Count; mi++)
            {
                var mj = (mi + 1) % polygon.Count;

                var vi = polygon[mi];
                var vj = polygon[mj];

                // TODO: Maybe there is a better/shorter way to implement this?

                if (vi == a)
                {
                    if (vj == b)
                    {
                        // Insert C between AB
                        vMerge = c;
                        return true;
                    }
                    else if (vj == c)
                    {
                        // Insert B between CA
                        vMerge = b;
                        return true;
                    }
                }
                else
                if (vi == b)
                {
                    if (vj == c)
                    {
                        // Insert A between BC
                        vMerge = a;
                        return true;
                    }
                    else if (vj == a)
                    {
                        // Insert C between AB
                        vMerge = c;
                        return true;
                    }
                }
                else
                if (vi == c)
                {
                    if (vj == a)
                    {
                        // Insert B between CA
                        vMerge = b;
                        return true;
                    }
                    else if (vj == b)
                    {
                        // Insert A between BC
                        vMerge = a;
                        return true;
                    }
                }
            }

            vMerge = -1;
            mi = -1;

            return false;
        }

        #endregion

        #region Convex Hull

        /// <summary>
        /// Constructs a convex hull to surround the point cloud provided.
        /// </summary>
        public static Polygon CreateConvexHull(IEnumerable<Vector> points)
        {
            return new Polygon(EnumerateConvexHull(points));
        }

        internal static IEnumerable<Vector> EnumerateConvexHull(IEnumerable<Vector> points)
        // Somewhat ported from https://www.geeksforgeeks.org/convex-hull-set-2-graham-scan/
        {
            if (points.Any())
            {
                var stack = new Stack<Vector>();

                // Convert points to list
                var _points = points.ToList();

                // Find minimum point
                var p0 = _points.Aggregate((min, vec) =>
                     // Is it lower, if equal is it left-most?
                     (vec.Y < min.Y) || (Calc.NearEquals(vec.Y, min.Y) && vec.X < min.X) ? vec : min);

                // Sorts the points by orientation n distance
                _points.Sort((p1, p2) =>
                {
                    var o = orientation(p0, p1, p2);

                    if (o == 0)
                    {
                        var d1 = Vector.DistanceSquared(p0, p1);
                        var d2 = Vector.DistanceSquared(p0, p2);
                        return (d2 >= d1) ? -1 : 1;
                    }

                    return o == 2 ? -1 : 1;
                });

                // NOTE: Algorithm specifies removing colinear orientations here except the
                // furthest from p0 but I've found it not to matter for the result, maybe
                // its simply to reduce the work of the next loop in theory?

                // For each point
                foreach (var p in _points)
                {
                    // If the orentation is not clockwise, remove from stack.
                    // This works because the points are ordered by orientation and distance
                    // around p0 for clockwise winding
                    while (stack.Count > 1 && orientation(next(), stack.Peek(), p) != 2)
                    {
                        stack.Pop();
                    }

                    // Push point onto stack
                    stack.Push(p);
                }

                // Gets the second to top item in the stack
                Vector next()
                {
                    var p = stack.Pop();
                    var n = stack.Peek();
                    stack.Push(p);
                    return n;
                }

                // TODO: Can I move into Vector as a 'GetWinding' function?
                // What is p q r in terms of shape?
                static int orientation(Vector p, Vector q, Vector r)
                {
                    var val = ((q.Y - p.Y) * (r.X - q.X))
                            - ((q.X - p.X) * (r.Y - q.Y));

                    // CW (2) or CCW (1)
                    if (Calc.NearZero(val)) { return 0; }
                    else { return (val > 0) ? 2 : 1; }
                }

                // 
                return stack;
            }
            else
            {
                // Not possible to make a hull...
                // Return empty or exception?
                return Enumerable.Empty<Vector>();
            }
        }

        #endregion

        #region Closest Point (IReadOnlyList<Vector>)

        public static Vector ClosestPoint(IReadOnlyList<Vector> polygon, in Vector point)
        // todo: can this be optimized?
        {
            if (ContainsPoint(polygon, point)) { return point; }
            else
            {
                var minD = float.MaxValue;
                var minP = Vector.Zero;

                for (var i = 0; i < polygon.Count; i++)
                {
                    var a = GetVertex(polygon, i + 0);
                    var b = GetVertex(polygon, i + 1);

                    var p = LineSegment.ClosestPoint(a, b, point, out var d);

                    if (d < minD)
                    {
                        minD = d;
                        minP = p;
                    }
                }

                return minP;
            }
        }

        #endregion

        #region Contains Point (IReadOnlyList<Vector>)

        public static bool ContainsPoint(IReadOnlyList<Vector> poly, Vector point)
        // ref: https://stackoverflow.com/a/34689268
        {
            // Check if a triangle or higher n-gon
            Debug.Assert(poly.Count >= 3);

            // Keep track of cross product sign changes
            var pos = 0;
            var neg = 0;

            for (var i = 0; i < poly.Count; i++)
            {
                var p0 = poly[i];
                var p1 = poly[i < poly.Count - 1 ? i + 1 : 0];

                var e0 = point - p0;
                var e1 = p1 - p0;

                var d = Vector.Cross(e0, e1);

                // 
                if (d > 0) { pos++; }
                if (d < 0) { neg++; }

                // If the sign changes, then point is outside
                if (pos > 0 && neg > 0)
                {
                    return false;
                }
            }

            // If no change in direction, then on same side of all segments, and thus inside
            return true;
        }

        #endregion

        #region Overlaps (IReadOnlyList<Vector>)

        public static bool Overlaps(IReadOnlyList<Vector> polygon, in IShape shape)
        {
            // pol - pol
            if (shape is IReadOnlyList<Vector> pol)
            {
                return Collisions.Overlaps(polygon, pol);
            }
            // pol - rec
            else if (shape is Rectangle rec)
            {
                pol = rec.GetTempPolygon(0);
                return Collisions.Overlaps(polygon, pol);
            }
            // pol - tri
            else if (shape is Triangle tri)
            {
                pol = tri.GetTempPolygon(0);
                return Collisions.Overlaps(polygon, pol);
            }
            // pol - cir
            else if (shape is Circle cir)
            {
                return Collisions.Overlaps(in cir, polygon);
            }
            // unknown case
            else
            {
                throw new InvalidOperationException("Unable to determine overlap, shape was not a known type.");
            }
        }

        #endregion

        #region Raycast (IReadOnlyList<Vector>)

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static bool Raycast(IReadOnlyList<Vector> polygon, in Ray ray)
        {
            return Raycast(polygon, in ray.Origin, in ray.Direction, out _);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static bool Raycast(IReadOnlyList<Vector> polygon, in Vector origin, in Vector direction)
        {
            return Raycast(polygon, in origin, in direction, out _);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static bool Raycast(IReadOnlyList<Vector> polygon, in Ray ray, out Contact contact)
        {
            return Raycast(polygon, in ray.Origin, in ray.Direction, out contact);
        }

        public static bool Raycast(IReadOnlyList<Vector> polygon, in Vector origin, in Vector direction, out Contact contact)
        // ref: https://github.com/RandyGaul/cute_headers/blob/master/cute_c2.h
        {
            var lo = 0F;
            var hi = float.MaxValue;
            var index = ~0;

            // For each edge in the polygon
            for (var i = 0; i < polygon.Count; ++i)
            {
                var nor = Polygon.GetScaledNormal(polygon, i);
                var num = Vector.Dot(nor, polygon[i] - origin);
                var den = Vector.Dot(nor, direction);

                // degenerate case (avoids divide by zero)
                if (Calc.NearZero(den) && num < 0)
                {
                    // ...?
                    contact = default;
                    return false;
                }
                else if (den < 0 && num < lo * den)
                {
                    lo = num / den;
                    index = i;
                }
                else if (den > 0 && num < hi * den)
                {
                    hi = num / den;
                }

                if (hi < lo)
                {
                    // ...?
                    contact = default;
                    return false;
                }
            }

            // Found an index
            if (index != ~0)
            {
                // Create contact
                contact = new Contact(
                    position: origin + (direction * lo),
                    normal: Polygon.GetNormal(polygon, index)
,
                    distance: lo);

                return true;
            }

            // No contact (complete miss)
            contact = default;
            return false;
        }

        #endregion

        internal static void ComputeMetrics(IReadOnlyList<Vector> polygon, out float area, out Vector center, out Vector centroid)
        {
            // 
            centroid = Vector.Zero;
            center = Vector.Zero;
            area = 0F;

            // 
            for (var i = 0; i < polygon.Count; i++)
            {
                var x = polygon[i + 0];
                var y = polygon[i + 1 == polygon.Count ? 0 : i + 1];

                var a = Vector.Cross(x, y);
                area += a;
                centroid += (x + y) * a;
                center += x;
            }

            area /= 2F;
            centroid /= area * 6F;
            center /= polygon.Count;
        }

        /// <summary>
        /// Vector perpendicular to the i-th edge scaled by the length of the edge.
        /// </summary>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        internal static Vector GetScaledNormal(IReadOnlyList<Vector> polygon, int i)
        {
            var v1 = polygon[i];
            var v2 = polygon[i + 1 == polygon.Count ? 0 : i + 1];

            return (v2 - v1).Perpendicular;
        }

        /// <summary>
        /// Vector perpendicular to the i-th edge.
        /// </summary>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Vector GetNormal(IReadOnlyList<Vector> polygon, int i)
        {
            return Vector.Normalize(GetScaledNormal(polygon, i));
        }

        /// <summary>
        /// Gets the i-th vertex but also wraps beyond the domain (ie, -1 will access last vertex).
        /// </summary>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static T GetVertex<T>(IReadOnlyList<T> data, int index)
        {
            // todo: test if statements vs wrap (modulus heavy) has considerable performance effect?
            // if (index >= data.Count) { }
            // if (index <=) { }
            return data[Calc.Wrap(index, data.Count)];
        }
    }
}
