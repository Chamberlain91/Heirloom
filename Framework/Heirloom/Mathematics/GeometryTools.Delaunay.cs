using System;
using System.Collections.Generic;
using System.Runtime.CompilerServices;

namespace Heirloom.Mathematics
{
    /// <summary>
    /// Provides utilities for generating and manipulating shapes.
    /// </summary>
    public static partial class GeometryTools
    {
        /// <summary>
        /// Computes the Delaunay triangulation of a set of points (via bowyer-watson).
        /// </summary>
        public static List<Triangle> TriangulatePoints(this IEnumerable<Vector> points)
        {
            return new List<Triangle>(Delaunay.Triangulate(points));
        }

        /// <summary>
        /// An implementation of delaunay triangulation.
        /// </summary>
        private static class Delaunay
        {
            /// <summary>
            /// Constructs the Delaunay triangulation of a set of points.
            /// </summary>
            internal static IEnumerable<Triangle> Triangulate(IEnumerable<Vector> inPoints, float error = 0.01F)
            {
                // Compute super triangle
                var super = CreateSuperTriangle(inPoints);

                // Create point set
                var points = new List<Vector> { super.A, super.B, super.C };
                points.AddRange(inPoints);

                // Initialize triangle set
                var triangles = new HashSet<TriangleIndex>() {
                    new TriangleIndex(0, 1, 2, points, error)
                };

                // Insert each vertex
                for (var i = 3; i < points.Count; i++)
                {
                    var point = points[i];

                    var invalidTriangles = new HashSet<TriangleIndex>();
                    var edges = new HashSet<EdgeIndex>();

                    // == Invalidate and Remove triangles

                    foreach (var triangle in triangles)
                    {
                        var a = points[triangle.A];
                        var b = points[triangle.B];
                        var c = points[triangle.C];

                        // If the point is contained by this circle, invalidate the triangle
                        if (triangle.Circle.Contains(point))
                        {
                            invalidTriangles.Add(triangle);
                        }
                    }

                    // Remove invalid triangles
                    triangles.ExceptWith(invalidTriangles);

                    // == Find Polygon Hole

                    foreach (var triangle in invalidTriangles)
                    {
                        // For each edge A->B, B->C then C->A
                        for (var e = 0; e < 3; e++)
                        {
                            // Get the ith edge
                            var edge = triangle.GetEdge(e);

                            // Insert into edge set. If we receive a duplicate edge,
                            // we will remove the duplicate edge. Since an edge can only
                            // share a max of two triangles (on each side), this is safe.
                            if (!edges.Add(edge)) { edges.Remove(edge); }
                        }
                    }

                    // == Fill Polygon Hole

                    // Triangulates convex hole (triangle-fan)
                    foreach (var edge in edges)
                    {
                        var triangle = new TriangleIndex(edge.A, edge.B, i, points, error);
                        triangles.Add(triangle);
                    }
                }

                // == Emit generated triangles

                foreach (var triangle in triangles)
                {
                    // Skip triangles that connected to the vertices of the super triangle.
                    if (triangle.SharesVertex(0)) { continue; }
                    if (triangle.SharesVertex(1)) { continue; }
                    if (triangle.SharesVertex(2)) { continue; }

                    var a = points[triangle.A];
                    var b = points[triangle.B];
                    var c = points[triangle.C];

                    yield return new Triangle(a, b, c);
                }
            }

            private static Triangle CreateSuperTriangle(IEnumerable<Vector> points)
            {
                var bounds = Rectangle.FromPoints(points);

                var m = Calc.Max(bounds.Width, bounds.Height);
                var mx = bounds.Center.X;
                var my = bounds.Center.Y;

                var v0 = new Vector(mx - (20 * m), my - m);
                var v1 = new Vector(mx, my + (20 * m));
                var v2 = new Vector(mx + (20 * m), my - m);

                return new Triangle(v0, v1, v2);
            }

            #region Helper Structures

            private readonly struct TriangleIndex : IEquatable<TriangleIndex>
            {
                public readonly int A;

                public readonly int B;

                public readonly int C;

                public readonly Circle Circle;

                public TriangleIndex(int a, int b, int c, IList<Vector> points, float error)
                {
                    A = a;
                    B = b;
                    C = c;

                    // Compute the circumcircle
                    Circle = Triangle.CreateCircumcircle(points[a], points[b], points[c]);
                    Circle.Radius += error; // intentional error to correct behaviour on circular point sets
                }

                /// <summary>
                /// Checks if this triangle shares the given vertex as one of its three vertices.
                /// </summary>
                internal bool SharesVertex(int vertex)
                {
                    if (A == vertex) { return true; }
                    if (B == vertex) { return true; }
                    if (C == vertex) { return true; }

                    return false;
                }

                internal EdgeIndex GetEdge(int edgeIndex)
                {
                    if (edgeIndex == 0) { return new EdgeIndex(A, B); }
                    else
                    if (edgeIndex == 1) { return new EdgeIndex(B, C); }
                    else
                    if (edgeIndex == 2) { return new EdgeIndex(C, A); }
                    else
                    {
                        throw new ArgumentOutOfRangeException(nameof(edgeIndex));
                    }
                }

                #region Equality

                public override bool Equals(object obj)
                {
                    return obj is TriangleIndex edge
                        && Equals(edge);
                }

                public bool Equals(TriangleIndex other)
                {
                    return (A == other.A || A == other.B || A == other.C)
                        && (B == other.A || B == other.B || B == other.C)
                        && (C == other.A || C == other.B || C == other.C);
                }

                public override int GetHashCode()
                {
                    // might be bad for hash-collisions, but makes it "symmetric"
                    var hashA = A.GetHashCode();
                    var hashB = B.GetHashCode();
                    var hashC = C.GetHashCode();

                    return hashA ^ hashB ^ hashC;
                }

                public static bool operator ==(TriangleIndex left, TriangleIndex right)
                {
                    return left.Equals(right);
                }

                public static bool operator !=(TriangleIndex left, TriangleIndex right)
                {
                    return !(left == right);
                }

                #endregion
            }

            private readonly struct EdgeIndex : IEquatable<EdgeIndex>
            {
                public readonly int A;

                public readonly int B;

                public EdgeIndex(int a, int b)
                {
                    A = a;
                    B = b;
                }

                #region Equality

                public override bool Equals(object obj)
                {
                    return obj is EdgeIndex edge
                        && Equals(edge);
                }

                public bool Equals(EdgeIndex other)
                {
                    return (A == other.A && B == other.B)
                        || (B == other.A && A == other.B);
                }

                public override int GetHashCode()
                {
                    // might be bad for hash-collisions, but makes it "symmetric"
                    var hashA = A.GetHashCode();
                    var hashB = B.GetHashCode();

                    return hashA ^ hashB;
                }

                public static bool operator ==(EdgeIndex left, EdgeIndex right)
                {
                    return left.Equals(right);
                }

                public static bool operator !=(EdgeIndex left, EdgeIndex right)
                {
                    return !(left == right);
                }

                #endregion
            }

            #endregion
        }
    }
}
