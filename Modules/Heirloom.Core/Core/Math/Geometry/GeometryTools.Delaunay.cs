using System.Collections.Generic;
using System.Runtime.CompilerServices;

namespace Heirloom
{
    /// <summary>
    /// Provides utilities for generating and manipulating shapes.
    /// </summary>
    public static partial class GeometryTools
    {
        /// <summary>
        /// Constructs the Delaunay triangulation of a set of points.
        /// </summary>
        public static List<Triangle> Triangulate(this IEnumerable<Vector> points)
        {
            var triangles = new List<Triangle>();
            Triangulate(points, triangles);
            return triangles;
        }

        /// <summary>
        /// Constructs the Delaunay triangulation of a set of points.
        /// </summary>
        public static void Triangulate(this IEnumerable<Vector> points, List<Triangle> triangles)
        {
            Delaunay.Triangulate(points, triangles);
        }

        /// <summary>
        /// An implementation of delaunay triangulation.
        /// </summary>
        private static class Delaunay
        // todo: make thread safe with [ThreadStatic] and using some sort of struct/getter to initialize on each thread.
        {
            private static readonly HashSet<Triangle> _invalidTriangles = new HashSet<Triangle>();
            private static readonly HashSet<Triangle> _triangles = new HashSet<Triangle>();
            private static readonly HashSet<LineSegment> _edges = new HashSet<LineSegment>();

            /// <summary>
            /// Constructs the Delaunay triangulation of a set of points.
            /// </summary>
            internal static void Triangulate(IEnumerable<Vector> points, List<Triangle> triangles)
            {
                // Create super triangle
                var bounds = Rectangle.FromPoints(points);
                var super = CreateSuperTriangle(bounds);

                // Initialize triangle set
                _triangles.Clear();
                _triangles.Add(super);

                // Insert each vertex
                foreach (var point in points)
                {
                    _invalidTriangles.Clear();
                    _edges.Clear();

                    // == Invalidate and Remove triangles

                    foreach (var triangle in _triangles)
                    {
                        // Compute the circum circle
                        // too: perhaps precompute the circum circle?
                        var circle = Triangle.CreateCircumcircle(triangle.A, triangle.B, triangle.C);

                        // If the point is contained by this circle, invalidate the triangle
                        if (circle.Contains(point))
                        {
                            _invalidTriangles.Add(triangle);
                        }
                    }

                    // Remove invalid triangles
                    _triangles.ExceptWith(_invalidTriangles);

                    // == Find Polygon Hole

                    foreach (var triangle in _invalidTriangles)
                    {
                        // For each edge A->B, B->C then C->A
                        for (var i = 0; i < 3; i++)
                        {
                            // Get the ith edge
                            var edge = triangle.GetEdge(i);

                            // Insert into edge set. If we receive a duplicate edge,
                            // we will remove the duplicate edge. Since an edge can only
                            // share a max of two triangles (on each side), this is a safe.
                            if (!_edges.Add(edge)) { _edges.Remove(edge); }
                        }
                    }

                    // == Fill Polygon Hole

                    foreach (var edge in _edges)
                    {
                        // Constructs one of the triangles to fill the hole
                        var triangle = new Triangle(edge.A, edge.B, point);
                        _triangles.Add(triangle);
                    }
                }

                // == Emit generated triangles

                foreach (var tri in _triangles)
                {
                    // Skip triangles that connected to the vertices of the super triangle.
                    if (SharesVertex(in tri, in super.A)) { continue; }
                    if (SharesVertex(in tri, in super.B)) { continue; }
                    if (SharesVertex(in tri, in super.C)) { continue; }

                    // Emit triangle
                    triangles.Add(new Triangle(tri.A, tri.B, tri.C));
                }
            }

            /// <summary>
            /// Checks if this triangle shares the given vertex as one of its three vertices.
            /// </summary>
            [MethodImpl(MethodImplOptions.AggressiveInlining)]
            private static bool SharesVertex(in Triangle triangle, in Vector vertex)
            {
                if (triangle.A == vertex) { return true; }
                if (triangle.B == vertex) { return true; }
                if (triangle.C == vertex) { return true; }
                return false;
            }

            private static Triangle CreateSuperTriangle(in Rectangle bounds)
            {
                var dx = bounds.Width / 3F;
                var dy = bounds.Height / 3F;

                var v0 = new Vector(bounds.Left - dx, bounds.Top - dy * 5);
                var v1 = new Vector(bounds.Left - dx, bounds.Bottom + dy);
                var v2 = new Vector(bounds.Right + dx * 5, bounds.Bottom + dy);

                return new Triangle(v0, v1, v2);
            }
        }
    }
}
