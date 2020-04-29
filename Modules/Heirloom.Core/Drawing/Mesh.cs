using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Runtime.CompilerServices;

using Heirloom.Geometry;

namespace Heirloom
{
    public sealed class Mesh
    {
        private readonly List<Vertex> _vertices;
        private readonly List<int> _indices;

        #region Constructors

        public Mesh()
        {
            _vertices = new List<Vertex>();
            _indices = new List<int>();
        }

        #endregion

        #region Properties

        /// <summary>
        /// Gets the vertices contained by this mesh.
        /// </summary>
        public IReadOnlyList<Vertex> Vertices => _vertices;

        /// <summary>
        /// Gets the (optional) indices defining triangles by index of the vertex list.
        /// </summary>
        public IReadOnlyList<int> Indices => _indices;

        /// <summary>
        /// Is this mesh constructed triangles specified by indexed?
        /// </summary>
        public bool IsIndexed => _indices.Count > 0;

        /// <summary>
        /// The version number of the mesh.
        /// Modifications to the mesh data increment this number.
        /// </summary>
        public uint Version { get; private set; }

        #endregion

        #region Manipulation Functions

        /// <summary>
        /// Clears the mesh data.
        /// </summary>
        public void Clear()
        {
            _vertices.Clear();
            _indices.Clear();

            UpdateVersionNumber();
        }

        /// <summary>
        /// Appends a vertex to this mesh.
        /// </summary>
        public void AddVertex(Vertex vertex)
        {
            if (_vertices.Count >= ushort.MaxValue)
            {
                throw new InvalidOperationException($"Unable to add vertex. Will cause mesh to have too many vertices! Must contain less than {ushort.MaxValue}.");
            }

            // Append vertex
            _vertices.Add(vertex);
            UpdateVersionNumber();
        }

        /// <summary>
        /// Appends multiple vertices to this mesh.
        /// </summary>
        public void AddVertices(IEnumerable<Vertex> vertices)
        {
            // todo: vertex capacity exception?

            // Append vertices
            _vertices.AddRange(vertices);
            UpdateVersionNumber();
        }

        /// <summary>
        /// Appends multiple vertices to this mesh.
        /// </summary>
        public void AddVertices(params Vertex[] vertices)
        {
            if ((_vertices.Count + vertices.Length) >= ushort.MaxValue)
            {
                throw new InvalidOperationException($"Unable to add vertices. Will cause mesh to have too many vertices! Must contain less than {ushort.MaxValue}.");
            }

            // Append vertices
            _vertices.AddRange(vertices);
            UpdateVersionNumber();
        }

        /// <summary>
        /// Appends a triangle index to this mesh. Until <see cref="Clear"/> is called, this mesh becomes indexed.
        /// </summary>
        /// <seealso cref="IsIndexed"/>
        public void AddIndex(int index)
        {
            CheckIndex(index);

            // Append index
            _indices.Add(index);
            UpdateVersionNumber();
        }

        /// <summary>
        /// Appends a triangle index to this mesh. Until <see cref="Clear"/> is called, this mesh becomes indexed.
        /// </summary>
        /// <seealso cref="IsIndexed"/>
        public void AddIndices(params int[] indices)
        {
            CheckIndices(indices);

            // Append indices
            _indices.AddRange(indices);
            UpdateVersionNumber();
        }

        /// <summary>
        /// Appends a triangle index to this mesh. Until <see cref="Clear"/> is called, this mesh becomes indexed.
        /// </summary>
        /// <seealso cref="IsIndexed"/>
        public void AddIndices(IEnumerable<int> indices)
        {
            CheckIndices(indices);

            // Append indices
            _indices.AddRange(indices);
            UpdateVersionNumber();
        }

        [Conditional("DEBUG")]
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        private void CheckIndices(IEnumerable<int> indices)
        {
            foreach (var index in indices)
            {
                CheckIndex(index);
            }
        }

        [Conditional("DEBUG")]
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        private void CheckIndex(int index)
        {
            if (index < 0 || index >= ushort.MaxValue)
            {
                throw new ArgumentOutOfRangeException($"Unable to add triangle indices. Indices must be non-negative and less than {ushort.MaxValue}.");
            }
        }

        #endregion

        #region Create Standard Shapes

        /// <summary>
        /// Constructs a mesh from the given polygon via <see cref="Polygon.Triangulate()"/>. <para/>
        /// UV coordinates are the normalized polygon within its own bounds.
        /// </summary>
        /// <param name="polygon">Some polygon.</param>
        /// <returns>A new mesh representign the 'filled' space of the polygon.</returns>
        public static Mesh CreateFromPolygon(Polygon polygon)
        {
            return CreateFromPolygon(polygon.Vertices);
        }

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
                mesh._vertices.Add(new Vertex(pt, uv));
            }

            // Add triangle indices
            foreach (var (a, b, c) in PolygonTools.TriangulateIndices(polygon))
            {
                mesh._indices.Add((ushort) a);
                mesh._indices.Add((ushort) b);
                mesh._indices.Add((ushort) c);
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
            mesh._vertices.AddRange(polygon.Select(v => new Vertex(v, (v - bounds.Min) / (Vector) bounds.Size)));

            // Add triangles (triangle fan style)
            for (var i = 1; i < mesh._vertices.Count; i++)
            {
                mesh._indices.Add(0);
                mesh._indices.Add((ushort) i);
                mesh._indices.Add((ushort) ((i + 1) % mesh._vertices.Count));
            }

            return mesh;
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
            mesh._indices.AddRange(new[] { 0, 1, 2, 0, 2, 3 });
            mesh._vertices.AddRange(new[] {
                new Vertex((0, 0), (0, 0)),
                new Vertex((w, 0), (1, 0)),
                new Vertex((w, h), (1, 1)),
                new Vertex((0, h), (0, 1))
            });

            return mesh;
        }

        #endregion

        private void UpdateVersionNumber()
        {
            Version++;

            // If hit the maximum version number, wrap around.
            if (Version == uint.MaxValue)
            {
                Version = 0;
            }
        }
    }
}
