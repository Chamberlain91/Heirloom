using System;
using System.Collections;
using System.Collections.Generic;

namespace Heirloom.Collections
{
    /// <summary>
    /// Represents a directed or undirected graph.
    /// </summary>
    /// <typeparam name="V">Some vertex type. Must properly implement equality checks.</typeparam>
    /// <typeparam name="E">Some data type for giving values to edges.</typeparam>
    public class Graph<V, E> : IReadOnlyGraph<V, E> where E : struct
    {
        private readonly Dictionary<V, HashSet<V>> _outgoing; // null when undirected
        private readonly Dictionary<V, HashSet<V>> _incoming; // null when undirected
        private readonly Dictionary<V, HashSet<V>> _adjacent = new Dictionary<V, HashSet<V>>();

        private readonly IEdgeCollection _edges;
        private readonly E _defaultEdgeProperty;

        #region Constructor

        /// <summary>
        /// Constructs a new graph. It may be configured to be directed or undirected.
        /// </summary>
        /// <param name="directed">Configures the graph to have directional arcs or edges.</param>
        /// <param name="defaultEdgeProperty">The default property given to edges created with <see cref="AddEdge(V, V)"/>.</param>
        public Graph(bool directed = false, E defaultEdgeProperty = default)
        {
            IsDirected = directed;

            _defaultEdgeProperty = defaultEdgeProperty;

            if (IsDirected)
            {
                _edges = new ArcCollection();

                _outgoing = new Dictionary<V, HashSet<V>>();
                _incoming = new Dictionary<V, HashSet<V>>();
            }
            else
            {
                _edges = new EdgeCollection();

                _outgoing = null;
                _incoming = null;
            }
        }

        #endregion

        #region Properties

        /// <inheritdoc/>
        public bool IsDirected { get; }

        /// <inheritdoc/>
        public IReadOnlyCollection<V> Vertices => _adjacent.Keys;

        /// <inheritdoc/>
        public IReadOnlyCollection<(V, V)> Edges => _edges;

        #endregion

        /// <summary>
        /// Clears the graph, making it empty.
        /// </summary>
        public void Clear()
        {
            _adjacent.Clear();

            if (IsDirected)
            {
                _outgoing.Clear();
                _incoming.Clear();
            }

            _edges.Clear();
        }

        #region Manipulate Vertices

        /// <summary>
        /// Adds several vertices to the graph at once.
        /// </summary>
        public void AddVertices(IEnumerable<V> vertices)
        {
            if (vertices is null) { throw new ArgumentNullException(nameof(vertices)); }

            foreach (var vertex in vertices)
            {
                AddVertex(vertex);
            }
        }

        /// <summary>
        /// Add a vertex to the graph.
        /// </summary>
        /// <exception cref="ArgumentException">Thrown when the vertex already exists.</exception>
        public void AddVertex(V vtx)
        {
            if (ContainsVertex(vtx) == false)
            {
                // Add vertex (and neighbor collections)
                _adjacent.Add(vtx, new HashSet<V>());

                if (IsDirected)
                {
                    _outgoing.Add(vtx, new HashSet<V>());
                    _incoming.Add(vtx, new HashSet<V>());
                }
            }
            else
            {
                throw new ArgumentException("Unable to add vertex, already contained.");
            }
        }

        /// <summary>
        /// Removes a vertex (and associated edges).
        /// </summary> 
        /// <returns>Returns <see langword="true"/> if the vertex existed and was removed successfully.</returns>
        public bool RemoveVertex(V vtx)
        {
            if (ContainsVertex(vtx))
            {
                // Remove all edges associated to the vertex
                DisconnectVertex(vtx);

                // Remove vertex
                _outgoing?.Remove(vtx);
                _incoming?.Remove(vtx);
                _adjacent.Remove(vtx);

                return true;
            }
            else
            {
                return false;
            }
        }

        /// <inheritdoc/>
        public bool ContainsVertex(V vtx)
        {
            return _adjacent.ContainsKey(vtx);
        }

        private void DisconnectVertex(V vtx)
        {
            // successors are the same as adjacent for undirected
            foreach (var other in GetSuccessors(vtx))
            {
                _edges.ScheduleRemoval(_edges.GetEdge(vtx, other));
            }

            if (IsDirected)
            {
                foreach (var other in GetPredecessors(vtx))
                {
                    _edges.ScheduleRemoval(_edges.GetEdge(other, vtx));
                }
            }

            // Commit removal from edge collection, removing from neighbor sets as we go.
            foreach (var edge in _edges.CommitRemoval())
            {
                if (IsDirected)
                {
                    _outgoing[edge.A].Remove(edge.B);
                    _incoming[edge.B].Remove(edge.A);
                }

                // Remove adjacencies
                _adjacent[edge.A].Remove(edge.B);
                _adjacent[edge.B].Remove(edge.A);
            }
        }

        #endregion

        #region Manipulate Edges

        /// <summary>
        /// Adds several edges to the graph at once.
        /// </summary>
        public void AddEdges(IEnumerable<(V, V)> edges)
        {
            foreach (var (a, b) in edges)
            {
                AddEdge(a, b);
            }
        }

        /// <summary>
        /// Adds several edges (with properties) to the graph at once.
        /// </summary>
        public void AddEdges(IEnumerable<(V, V, E)> edges)
        {
            foreach (var (a, b, e) in edges)
            {
                AddEdge(a, b, e);
            }
        }

        /// <summary>
        /// Adds an edge to the graph (with default edge property).
        /// </summary>
        public void AddEdge(V a, V b)
        {
            AddEdge(a, b, _defaultEdgeProperty);
        }

        /// <summary>
        /// Adds an edge to the graph.
        /// </summary>
        public void AddEdge(V a, V b, E property)
        {
            if (!ContainsVertex(a)) { throw new KeyNotFoundException($"Unable to add edge, argument vertex '{nameof(a)}' was not contained."); }
            if (!ContainsVertex(b)) { throw new KeyNotFoundException($"Unable to add edge, argument vertex '{nameof(b)}' was not contained."); }

            if (_edges.Contains(a, b) == false)
            {
                // var edge = new Edge(a, b, property);
                _edges.AddEdge(a, b, property);

                // A and B are adjacent
                _adjacent[a].Add(b);
                _adjacent[b].Add(a);

                if (IsDirected)
                {
                    _outgoing[a].Add(b); // From A to B
                    _incoming[b].Add(a); // To B from A
                }
            }
            else
            {
                throw new InvalidOperationException("Unable to add edge, edge already exists.");
            }
        }

        /// <summary>
        /// Removes the specified edge from the graph.
        /// </summary>
        /// <param name="a">The first (near) side of the edge.</param>
        /// <param name="b">The second (far) side of the edge.</param>
        /// <returns>Returns <see langword="true"/> if the edge existed and was removed successfully.</returns>
        public bool RemoveEdge(V a, V b)
        {
            if (!ContainsVertex(a)) { throw new KeyNotFoundException($"Unable to remove edge, argument vertex '{nameof(a)}' was not contained."); }
            if (!ContainsVertex(b)) { throw new KeyNotFoundException($"Unable to remove edge, argument vertex '{nameof(b)}' was not contained."); }

            if (_edges.TryGetEdge(a, b, out var edge))
            {
                // Remove edge immediately
                _edges.ImmediateRemoval(edge);

                // edge was contained and removed successfully
                return true;
            }
            else
            {
                // edge was not contained
                return false;
            }
        }

        /// <inheritdoc/>
        public bool ContainsEdge(V a, V b)
        {
            return _edges.Contains(a, b);
        }

        /// <inheritdoc/>
        public E GetEdgeProperty(V a, V b)
        {
            if (_edges.TryGetEdge(a, b, out var edge))
            {
                return edge.Property;
            }
            else
            {
                throw new KeyNotFoundException("Unable to get edge property, edge does not exist.");
            }
        }

        /// <summary>
        /// Gets the associated data associated with some edge (ex, edge weight).
        /// </summary>
        /// <param name="a">The first (near) side of the edge.</param>
        /// <param name="b">The second (far) side of the edge.</param>
        /// <param name="property">The new data to associate with the edge.</param> 
        public void SetEdgeProperty(V a, V b, E property)
        {
            if (_edges.TryGetEdge(a, b, out var edge))
            {
                edge.Property = property;
            }
            else
            {
                throw new KeyNotFoundException("Unable to set edge property, edge does not exist.");
            }
        }

        /// <inheritdoc/>
        public IReadOnlyCollection<V> GetSuccessors(V vtx)
        {
            if (ContainsVertex(vtx))
            {
                if (IsDirected)
                {
                    return _outgoing[vtx];
                }
                else
                {
                    return _adjacent[vtx];
                }
            }
            else
            {
                return Array.Empty<V>();
                // throw new KeyNotFoundException("Unable to return successor vertices, specified vertex not found.");
            }
        }

        /// <inheritdoc/>
        public IReadOnlyCollection<V> GetPredecessors(V vtx)
        {
            if (ContainsVertex(vtx))
            {
                if (IsDirected)
                {
                    return _incoming[vtx];
                }
                else
                {
                    return _adjacent[vtx];
                }
            }
            else
            {
                return Array.Empty<V>();
                // throw new KeyNotFoundException("Unable to return successor vertices, specified vertex not found.");
            }
        }

        /// <inheritdoc/>
        public IReadOnlyCollection<V> GetNeighbors(V vtx)
        {
            if (ContainsVertex(vtx))
            {
                return _adjacent[vtx];
            }
            else
            {
                return Array.Empty<V>();
                // throw new KeyNotFoundException("Unable to return successor vertices, specified vertex not found.");
            }
        }

        #endregion

        #region Clone Graph & Subgraph

        /// <summary>
        /// Extracts a subgraph of the specified vertices.
        /// </summary>
        public Graph<V, E> CreateSubgraph(IEnumerable<V> vertices)
        {
            if (vertices is null) { throw new ArgumentNullException(nameof(vertices)); }

            var graph = new Graph<V, E>(directed: IsDirected);

            // Add all vertices
            graph.AddVertices(vertices);

            // Connect all edges between vertices
            foreach (var a in vertices)
            {
                foreach (var b in GetSuccessors(a))
                {
                    var property = GetEdgeProperty(a, b);
                    if (!graph.ContainsEdge(a, b))
                    {
                        graph.AddEdge(a, b, property);
                    }
                }
            }

            return graph;
        }

        IReadOnlyGraph<V, E> IReadOnlyGraph<V, E>.CreateSubgraph(IEnumerable<V> vertices)
        {
            return CreateSubgraph(vertices);
        }

        /// <summary>
        /// Duplicates this graph.
        /// </summary>
        public Graph<V, E> Clone()
        {
            var clone = new Graph<V, E>(directed: IsDirected);
            foreach (var vtx in Vertices) { clone.AddVertex(vtx); }
            foreach (var (a, b) in Edges)
            {
                if (!ContainsEdge(a, b))
                {
                    clone.AddEdge(a, b, GetEdgeProperty(a, b));
                }
            }

            return clone;
        }

        #endregion

        #region Edge Collection

        private sealed class Edge
        {
            public readonly V A;

            public readonly V B;

            public E Property;

            public Edge(V a, V b, E property)
            {
                A = a;
                B = b;

                Property = property;
            }

            public V GetOther(V x)
            {
                return Equals(A, x) ? B : A;
            }
        }

        private interface IEdgeCollection : IReadOnlyCollection<Edge>, IReadOnlyCollection<(V, V)>
        {
            void Clear();

            void AddEdge(V a, V b, E property);

            void ImmediateRemoval(Edge edge);

            void ScheduleRemoval(Edge edge);

            IEnumerable<Edge> CommitRemoval();

            bool Contains(V a, V b);

            bool TryGetEdge(V a, V b, out Edge edge);

            Edge GetEdge(V a, V b);
        }

        private abstract class EdgeCollection<P> : IEdgeCollection
        {
            private readonly Dictionary<P, Edge> _edges = new Dictionary<P, Edge>();
            private readonly HashSet<P> _removal = new HashSet<P>();

            public int Count => _edges.Count;

            protected abstract P CreatePair(V a, V b);

            public void Clear()
            {
                _edges.Clear();
            }

            public void AddEdge(V a, V b, E property)
            {
                var edge = new Edge(a, b, property);
                var pair = CreatePair(edge.A, edge.B);

                if (_edges.ContainsKey(pair))
                {
                    throw new InvalidOperationException($"Edge already exists between '{edge.A}' and '{edge.B}'.");
                }

                // Store edge
                _edges[pair] = edge;
            }

            public void ImmediateRemoval(Edge edge)
            {
                var pair = CreatePair(edge.A, edge.B);
                _edges.Remove(pair);
            }

            public void ScheduleRemoval(Edge edge)
            {
                var pair = CreatePair(edge.A, edge.B);
                _removal.Add(pair);
            }

            public IEnumerable<Edge> CommitRemoval()
            {
                foreach (var pair in _removal)
                {
                    // Emit edge
                    yield return _edges[pair];

                    // Remove edge
                    _edges.Remove(pair);
                }

                // Clear removal queue
                _removal.Clear();
            }

            public bool Contains(V a, V b)
            {
                var pair = CreatePair(a, b);
                return _edges.ContainsKey(pair);
            }

            public bool TryGetEdge(V a, V b, out Edge edge)
            {
                var pair = CreatePair(a, b);
                return _edges.TryGetValue(pair, out edge);
            }

            public Edge GetEdge(V a, V b)
            {
                if (TryGetEdge(a, b, out var edge))
                {
                    return edge;
                }

                // Unable to get edge
                throw new InvalidOperationException($"Edge already exists between '{a}' and '{b}'.");
            }

            public IEnumerator<Edge> GetEnumerator()
            {
                foreach (var (_, edge) in _edges)
                {
                    yield return edge;
                }
            }

            IEnumerator<(V, V)> IEnumerable<(V, V)>.GetEnumerator()
            {
                foreach (var (_, edge) in _edges)
                {
                    yield return (edge.A, edge.B);
                }
            }

            IEnumerator IEnumerable.GetEnumerator()
            {
                return GetEnumerator();
            }
        }

        private class EdgeCollection : EdgeCollection<UnorderedPair<V>>
        {
            protected override UnorderedPair<V> CreatePair(V a, V b)
            {
                return new UnorderedPair<V>(a, b);
            }
        }

        private class ArcCollection : EdgeCollection<OrderedPair<V>>
        {
            protected override OrderedPair<V> CreatePair(V a, V b)
            {
                return new OrderedPair<V>(a, b);
            }
        }

        #endregion
    }
}
