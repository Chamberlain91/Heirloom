using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Runtime.CompilerServices;

namespace Heirloom.Mathematics
{
    /// <summary>
    /// Provides several operations for polygons represented as a read-only list of vectors.
    /// </summary>
    internal static class PolygonTools
    {
        #region Nearest Point (IReadOnlyList<Vector>)

        /// <summary>
        /// Gets the nearest point on the polygon to the specified point.
        /// </summary>
        public static Vector GetNearestPoint(IReadOnlyList<Vector> polygon, in Vector point)
        {
            var minD = float.MaxValue;
            var minP = Vector.Zero;

            for (var i = 0; i < polygon.Count; i++) // O(n)
            {
                var a = polygon[i];
                var b = polygon[i + 1 == polygon.Count ? 0 : i + 1];

                var p = LineSegment.ClosestPoint(a, b, point, out var d);

                if (d < minD)
                {
                    minD = d;
                    minP = p;
                }
            }

            return minP;
        }

        #endregion

        #region Contains Point (IReadOnlyList<Vector>)

        /// <summary>
        /// Assuming the polygon is convex, checks if the point is contained.
        /// </summary>
        public static bool ContainsPoint(IReadOnlyList<Vector> convex, in Vector point)
        // todo: perhaps change to IEnumerable and use a memory/generator of the first point?
        // ref: https://stackoverflow.com/a/34689268
        {
            // Check if a triangle or higher n-gon
            Debug.Assert(convex.Count >= 3);

            // Keep track of cross product sign changes
            var pos = 0;
            var neg = 0;

            for (var i = 0; i < convex.Count; i++)
            {
                var p0 = convex[i];
                var p1 = convex[i < convex.Count - 1 ? i + 1 : 0];

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

        #region Raycast (IReadOnlyList<Vector>)

        /// <summary>
        /// Checks if a ray intersects this polygon and outputs information on the contact point.
        /// </summary>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static bool Raycast(IReadOnlyList<Vector> polygon, in Ray ray, out RayContact contact)
        {
            // todo: perhaps implement against LineSegment.Intersects? somehow comparitively see what is better
            // ref: https://github.com/RandyGaul/cute_headers/blob/master/cute_c2.h

            var lo = 0F;
            var hi = float.MaxValue;
            var index = ~0;

            // For each edge in the polygon
            for (var i = 0; i < polygon.Count; ++i)
            {
                var nor = GetScaledNormal(polygon, i);
                var num = Vector.Dot(nor, polygon[i] - ray.Origin);
                var den = Vector.Dot(nor, ray.Direction);

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
                contact = new RayContact(
                    position: ray.Origin + (ray.Direction * lo),
                    normal: GetNormal(polygon, index),
                    distance: lo);

                return true;
            }

            // No contact (complete miss)
            contact = default;
            return false;
        }

        #endregion

        #region Compute Convex Hull

        internal static Polygon ComputeConvexHull(IEnumerable<Vector> points)
        // Somewhat ported from https://www.geeksforgeeks.org/convex-hull-set-2-graham-scan/
        // todo: determine if there is a fast algorthim and record the time complexity.
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
                return new Polygon(stack);
            }
            else
            {
                // Not possible to make a hull...
                throw new InvalidOperationException("Unable to construct convex hull");
            }
        }

        #endregion

        #region Convex Decomposition

        /// <summary>
        /// Converts a simple polygon into one or more convex polygons enumerated by indices of the original polygon.
        /// </summary>
        public static IEnumerable<IReadOnlyList<int>> DecomposeConvexIndices(IReadOnlyList<Vector> points)
        // todo: possibly use ArrayPool<T> to prevent allocations of temporary lists?
        {
            // Check if already convex
            if (IsConvexPolygon(points)) // O(N)
            {
                // Clone this polygon (its already convex)
                yield return Enumerable.Range(0, points.Count).ToList();
            }
            // Not convex, we will break he polygon into triangles and
            // then attempt to merge the triangles back into convex bodies
            else
            {
                // The set of triangles
                var triangles = new List<(int a, int b, int c)>(TriangulateIndices(points));

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

            // Attempts to merge a triangle into the given polygon.
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
                        if (IsClockwise(points[vi_a], points[vi_b], points[vi_c]) && IsClockwise(points[vj_a], points[vj_b], points[vj_c]))
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

            /// <summary>
            /// Finds a shared edge in the given polygon, returning true when a shared edge can be found.
            /// </summary>
            /// <param name="polygon">Some polygon represented as indices of a vertex set.</param>
            /// <param name="a">First vertex index of the triangle.</param>
            /// <param name="b">Second vertex index of the triangle.</param>
            /// <param name="c">Third vertex index of the triangle.</param>
            /// <param name="mi">The position within <paramref name="polygon"/> to insert <paramref name="vMerge"/> after.</param>
            /// <param name="vMerge">The vertex index to insert after <paramref name="mi"/>.</param>
            static bool FindSharedEdge(List<int> polygon, int a, int b, int c, out int mi, out int vMerge) // O(M)
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
        }

        #endregion

        #region Triangle Decomposition

        /// <summary>
        /// Decomposes a simple polygon into constituent triangles.
        /// </summary>
        public static IEnumerable<Triangle> Triangulate(IReadOnlyList<Vector> poylgon)
        {
            // Convert triangulation to polygons
            return TriangulateIndices(poylgon)
                  .Select(tri => new Triangle(poylgon[tri.a], poylgon[tri.b], poylgon[tri.c]));
        }

        /// <summary>
        /// Decomposes a simple polygon into constituent triangles enumerated by indices of the original polygon.
        /// </summary>
        public static IEnumerable<(int a, int b, int c)> TriangulateIndices(IEnumerable<Vector> polygon)
        // todo: possibly use ArrayPool<T> to prevent allocations of temporary lists?
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
                // This *seems* to produce better convex merging for some reason?
                earIndex += 2; // Arbitrarily offset index each time

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

            // Determines if the ith point is an 'ear' (convex and does not overlap vertices)
            bool IsEar(int i)
            {
                // Triangle is always an ear (always convex and non-overlapping)
                if (points.Count == 3) { return true; }

                // Line or Point aren't polygons
                if (points.Count < 3) { return false; }

                // An ear must be convex
                if (IsClockwise(points, i))
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

        #region Convex & Clockwise Checks

        /// <summary>
        /// Determines if the polygon is convex.
        /// </summary>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static bool IsConvexPolygon(IReadOnlyList<Vector> polygon)
        {
            int pos = 0, neg = 0;
            for (var i = 0; i < polygon.Count; i++)
            {
                if (IsClockwise(polygon, i)) { pos++; }
                else { neg++; }
            }

            // Polygon must bend only in one direction to be convex.
            // If both values are positive, the polygon is concave.
            return pos <= 0 || neg <= 0;
        }

        /// <summary>
        /// Determines if the ith vertex is a convex (clockwise) vertex.
        /// </summary>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static bool IsClockwise(IReadOnlyList<Vector> polygon, int i)
        {
            var p = GetVertex(polygon, i - 1);
            var c = GetVertex(polygon, i + 0);
            var n = GetVertex(polygon, i + 1);

            return IsClockwise(in p, in c, in n);
        }

        /// <summary>
        /// Determines if the vertex '<paramref name="vCurr"/>' is convex (clockwise).
        /// </summary>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static bool IsClockwise(in Vector vPrev, in Vector vCurr, in Vector vNext)
        // todo: can be implemented against Vector.Cross, that's pretty much what is happening below.
        {
            // Does it have to be normalized?
            // var d1 = Vector.Normalize(c - p);
            // var d2 = Vector.Normalize(n - c);

            var d1 = vCurr - vPrev;
            var d2 = vNext - vCurr;

            // equivalent to d2.PerpendicularCCW?
            // var n2 = new Vector(-d2.Y, d2.X);
            d2.Set(-d2.Y, d2.X);

            return Vector.Dot(in d1, in d2) <= 0f;
        }

        #endregion

        #region Compute Metrics

        /// <summary>
        /// Computes general metrics about the specified polygon. Outputs the <paramref name="area"/>, <paramref name="center"/> and <paramref name="centroid"/>.
        /// </summary>
        /// <param name="polygon">Some polygon.</param>
        /// <param name="area">The area occupied by the polygon.</param>
        /// <param name="center">The center of the polygon by average.</param>
        /// <param name="centroid">The center of the polygon weighted by area.</param>
        public static void ComputeMetrics(IReadOnlyList<Vector> polygon, out float area, out Vector center, out Vector centroid)
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

        #endregion

        #region Get Normal

        /// <summary>
        /// Vector perpendicular to the i-th edge.
        /// </summary>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Vector GetNormal(IReadOnlyList<Vector> polygon, int i)
        {
            return Vector.Normalize(GetScaledNormal(polygon, i));
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

        #endregion

        /// <summary>
        /// Gets the support point (ie, deepest) in the specified direction.
        /// </summary>
        public static Vector GetSupport(IEnumerable<Vector> polygon, Vector direction)
        {
            return polygon.FindMaximal(v => Vector.Dot(v, direction));
        }

        /// <summary>
        /// Gets the i-th vertex but also wraps beyond the domain (ie, -1 will access last vertex).
        /// </summary>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        internal static T GetVertex<T>(IReadOnlyList<T> data, int index)
        {
            // todo: test if statements vs wrap (modulus heavy) has considerable performance effect?
            // if (index >= data.Count) { }
            // if (index <=) { }
            return data[Calc.Wrap(index, data.Count)];
        }
    }
}
