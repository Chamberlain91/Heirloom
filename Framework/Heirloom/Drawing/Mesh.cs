using System;
using System.Collections.Generic;

namespace Heirloom.Drawing
{
    public sealed class Mesh : GraphicsResource
    {
        private readonly List<MeshVertex> _vertices;

        public Mesh()
        {
            _vertices = new List<MeshVertex>();
        }

        public Mesh(IEnumerable<MeshVertex> vertices) : this()
        {
            if (vertices is null) { throw new ArgumentNullException(nameof(vertices)); }
            SetVertices(vertices);
        }

        public IReadOnlyList<MeshVertex> Vertices => _vertices;

        #region Manipulation Functions

        /// <summary>
        /// Clears the mesh data.
        /// </summary>
        public void Clear()
        {
            _vertices.Clear();

            IncrementVersion();
        }

        /// <summary>
        /// Appends a vertex to this mesh.
        /// </summary>
        public void AddVertex(MeshVertex vertex)
        {
            if (_vertices.Count >= ushort.MaxValue)
            {
                throw new InvalidOperationException($"Unable to add vertex. Will cause mesh to have too many vertices! Must contain less than {ushort.MaxValue}.");
            }

            // Append vertex
            _vertices.Add(vertex);

            IncrementVersion();
        }

        /// <summary>
        /// Appends a triangle face to this mesh.
        /// </summary>
        public void AddVertices(MeshTriangle triangle)
        {
            AddVertex(triangle.A);
            AddVertex(triangle.B);
            AddVertex(triangle.C);
        }

        /// <summary>
        /// Appends multiple vertices to this mesh.
        /// </summary>
        public void AddVertices(IEnumerable<MeshVertex> vertices)
        {
            // todo: vertex capacity exception?

            // Append vertices
            _vertices.AddRange(vertices);

            IncrementVersion();
        }

        public void SetVertex(int i, MeshVertex vertex)
        {
            _vertices[i] = vertex;

            IncrementVersion();
        }

        public void SetVertices(IEnumerable<MeshVertex> vertices)
        {
            _vertices.Clear();
            _vertices.AddRange(vertices);

            IncrementVersion();
        }

        #endregion

        internal static readonly Mesh QuadMesh = CreateQuadMesh();

        private static Mesh CreateQuadMesh()
        {
            return new Mesh(new[]
            {
                new MeshVertex((0,0), (0,0), Color.White), // 0
                new MeshVertex((1,0), (1,0), Color.White), // 1
                new MeshVertex((1,1), (1,1), Color.White), // 2

                new MeshVertex((0,0), (0,0), Color.White), // 0
                new MeshVertex((1,1), (1,1), Color.White), // 2
                new MeshVertex((0,1), (0,1), Color.White), // 3
            });
        }
    }
}
