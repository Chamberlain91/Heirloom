using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;

using Heirloom.Math;

namespace Heirloom.Drawing
{
    public class Mesh
    {
        public List<Vertex> Vertices { get; }

        public List<ushort> Indices { get; }

        public Mesh()
        {
            Vertices = new List<Vertex>();
            Indices = new List<ushort>();
        }

        #region Create Standard Shapes

        /// <summary>
        /// Constructs a mesh from the given polygon via <see cref="Polygon.Triangulate()"/>. <para/>
        /// UV coordinates are the normalized polygon within its own bounds.
        /// </summary>
        /// <param name="polygon">Some polygon.</param>
        /// <returns>A new mesh representign the 'filled' space of the polygon.</returns>
        public static Mesh CreateFromPolygon(IReadOnlyList<Vector> polygon)
        {
            if (polygon is null) { throw new ArgumentNullException(nameof(polygon)); }

            var mesh = new Mesh();

            // Compute polygon bounds
            var bounds = Rectangle.FromPoints(polygon);

            // Add polygon vertices
            foreach (var pt in polygon)
            {
                var uv = (pt - bounds.Min) / (Vector) bounds.Size;
                mesh.Vertices.Add(new Vertex(pt, uv));
            }

            // Add triangle indices
            foreach (var (a, b, c) in Polygon.DecomposeTrianglesIndices(polygon))
            {
                mesh.Indices.Add((ushort) a);
                mesh.Indices.Add((ushort) b);
                mesh.Indices.Add((ushort) c);
            }

            return mesh;
        }

        /// <summary>
        /// Constructs a mesh from the given convex polygon. <para/>
        /// UV coordinates are the normalized polygon within its own bounds.
        /// </summary>
        /// <param name="polygon">Some convex polygon.</param>
        /// <returns>A new mesh representign the 'filled' space of the polygon.</returns>
        public static Mesh CreateFromConvexPolygon(IEnumerable<Vector> polygon)
        {
            if (polygon is null) { throw new ArgumentNullException(nameof(polygon)); }

            var mesh = new Mesh();

            // Compute polygon bounds
            var bounds = Rectangle.FromPoints(polygon);

            // Add vertices
            mesh.Vertices.AddRange(polygon.Select(v => new Vertex(v, (v - bounds.Min) / (Vector) bounds.Size)));

            // Add triangles (triangle fan style)
            for (var i = 1; i < mesh.Vertices.Count; i++)
            {
                mesh.Indices.Add(0);
                mesh.Indices.Add((ushort) i);
                mesh.Indices.Add((ushort) ((i + 1) % mesh.Vertices.Count));
            }

            return mesh;
        }

        /// <summary>
        /// Constructs a mesh from a regular polygon.
        /// </summary>
        /// <param name="sides">Number of sides.</param>
        /// <param name="radius">Radius if the regular polygon.</param> 
        /// <returns>A new mesh representign the 'filled' space of the polygon.</returns>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Mesh CreateRegularPolygon(int sides, float radius)
        {
            var regularPolygon = Polygon.GetRegularPolygonPoints(Vector.Zero, sides, radius);
            return CreateFromConvexPolygon(regularPolygon);
        }

        /// <summary>
        /// Creates a simple quad mesh.
        /// </summary>
        /// <param name="w">Width of the quad mesh.</param>
        /// <param name="h">Height of the quad mesh.</param>
        /// <returns>A new mesh representign the 'filled' space of the quad.</returns>
        public static Mesh CreateQuad(float w, float h)
        {
            var mesh = new Mesh();
            mesh.Indices.AddRange(new ushort[] { 0, 1, 2, 0, 2, 3 });
            mesh.Vertices.AddRange(new[] {
                new Vertex((0, 0), (0, 0)),
                new Vertex((w, 0), (1, 0)),
                new Vertex((w, h), (1, 1)),
                new Vertex((0, h), (0, 1))
            });

            return mesh;
        }

        #endregion
    }
}
