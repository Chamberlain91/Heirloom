using System;
using System.Collections;
using System.Collections.Generic;

using Heirloom.Mathematics;

namespace Heirloom.Drawing
{
    public sealed class Mesh : GraphicsResource
    {
        public readonly ValueList<MeshVertex> Vertices;

        public readonly ValueList<int> Indices;

        #region Constructors

        public Mesh()
        {
            Vertices = new ValueList<MeshVertex>(this, 256);
            Indices = new ValueList<int>(this, 256);
        }

        public Mesh(IEnumerable<MeshVertex> vertices)
            : this()
        {
            if (vertices is null) { throw new ArgumentNullException(nameof(vertices)); }
            AddVertices(vertices);
        }

        public Mesh(IEnumerable<MeshVertex> vertices, IEnumerable<int> indices)
            : this(vertices)
        {
            if (indices is null) { throw new ArgumentNullException(nameof(indices)); }
            AddIndices(indices);
        }

        #endregion

        /// <summary>
        /// Gets a value that determines if this mesh is indexed.
        /// </summary>
        public bool IsIndexed => Indices.Count > 0;

        /// <summary>
        /// Clears the mesh data.
        /// </summary>
        public void Clear()
        {
            Vertices.Count = 0;
            Indices.Count = 0;

            IncrementVersion();
        }

        #region Vertex Manipulation

        /// <summary>
        /// Appends a vertex to this mesh.
        /// </summary>
        public void AddVertex(MeshVertex vertex)
        {
            if (Vertices.Count >= ushort.MaxValue)
            {
                throw new InvalidOperationException($"Unable to add vertex. Will cause mesh to have too many vertices! Must contain less than {ushort.MaxValue}.");
            }

            // Append vertex
            Vertices.Add(vertex);
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
            Vertices.AddRange(vertices);
            IncrementVersion();
        }

        /// <summary>
        /// Forcibly sets the vertex capacity. Values not yet assigned are zero'd.
        /// </summary>
        public void SetVertexCount(int count)
        {
            Vertices.EnsureCapacity(count);
            Vertices.Count = count;
        }

        #endregion

        #region Index Manipulation

        public void AddIndex(int vertex)
        {
            Indices.Add(vertex);
            IncrementVersion();
        }

        public void AddIndices(IEnumerable<int> vertices)
        {
            Indices.AddRange(vertices);
            IncrementVersion();
        }

        /// <summary>
        /// Forcibly sets the index capacity. Values not yet assigned are zero'd.
        /// </summary>
        public void SetIndexCount(int count)
        {
            Indices.EnsureCapacity(count);
            Indices.Count = count;
        }

        #endregion

        internal static readonly Mesh QuadMesh = CreateQuadMesh();

        private static Mesh CreateQuadMesh()
        {
            return new Mesh(
                indices: new[] { 0, 1, 2, 0, 2, 3 },
                vertices: new[]
                {
                    new MeshVertex((0,0), (0,0), Color.White), // 0
                    new MeshVertex((1,0), (1,0), Color.White), // 1
                    new MeshVertex((1,1), (1,1), Color.White), // 2 
                    new MeshVertex((0,1), (0,1), Color.White), // 3
                });
        }

        /// <summary>
        /// Represents a list of values on some <see cref="Mesh"/>.
        /// </summary>
        public sealed class ValueList<T> : IReadOnlyList<T> where T : struct
        {
            internal readonly Mesh Mesh;
            internal T[] Data;

            internal ValueList(Mesh mesh, int capacity)
            {
                Mesh = mesh;

                Data = new T[capacity];
                Count = 0;
            }

            /// <summary>
            /// Get or set an element in this list as some index.
            /// </summary>
            public T this[int index]
            {
                get => Data[index];

                set
                {
                    Data[index] = value;
                    Mesh.IncrementVersion();
                }
            }

            /// <summary>
            /// Gets the number of elements in this list.
            /// </summary>
            public int Count { get; internal set; }

            internal void Add(T vertex)
            {
                Anticipate(1);

                // Insert vertex
                Data[Count] = vertex;
                Count++;
            }

            internal void AddRange(IEnumerable<T> vertices)
            {
                if (vertices is ICollection collection)
                {
                    Anticipate(collection.Count);
                    collection.CopyTo(Data, Count);
                    Count += collection.Count;
                }
                else
                if (vertices is ValueList<T> vlist)
                {
                    Anticipate(vlist.Count);
                    Array.Copy(vlist.Data, 0, Data, Count, vlist.Count);
                    Count += vlist.Count;
                }
                else
                {
                    // Anticipate one element at a time
                    foreach (var v in vertices)
                    {
                        Add(v);
                    }
                }
            }

            internal void Anticipate(int future)
            {
                if ((Count + future) >= Data.Length)
                {
                    var newCapacity = Calc.Max(Data.Length + future, Data.Length * 2);
                    EnsureCapacity(newCapacity);
                }
            }

            internal void EnsureCapacity(int capacity)
            {
                if (capacity > Data.Length)
                {
                    Array.Resize(ref Data, capacity);
                }
            }

            /// <inheritdoc/>
            public IEnumerator<T> GetEnumerator()
            {
                // todo: concurrent modification exception?

                var segment = new ArraySegment<T>(Data, 0, Count);
                return segment.GetEnumerator();
            }

            IEnumerator IEnumerable.GetEnumerator()
            {
                return GetEnumerator();
            }
        }
    }
}
