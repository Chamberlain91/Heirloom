using System;
using System.Collections.Generic;
using System.Linq;

namespace Heirloom.Collections
{
    public class GraphNegativeWeightException : InvalidOperationException
    {
        internal GraphNegativeWeightException(string message)
            : base(message) { }
    }

    public class GraphSelfLoopException : InvalidOperationException
    {
        internal GraphSelfLoopException(string message)
            : base(message) { }
    }

    /// <summary>
    /// A configurable adjacency list based graph.
    /// </summary>
    /// <typeparam name="TKey">Some name/key of each stored element.</typeparam>
    /// <typeparam name="TValue">Some element type to store in the graph.</typeparam>
    public class Graph<TKey, TValue> : IGraph<TKey, TValue, Graph<TKey, TValue>>
    {
        /// <summary>
        /// Was this graph allowed to have negative edges weights?
        /// </summary>
        public bool AllowNegativeWeight => Configuration.AllowNegativeWeight;

        /// <summary>
        /// Was this graph allowed to have multiple parallel edges?
        /// </summary>
        public bool AllowParallelEdges => Configuration.AllowParallelEdges;

        /// <summary>
        /// Was this graph allowed to have self connecting loops ( Ex, 'A' connected to 'A' ).
        /// </summary>
        public bool AllowSelfLoops => Configuration.AllowSelfLoops;

        /// <summary>
        /// Is this graph allowed to have weighted edges? ( If not, all weights are clamped to 1.0 )
        /// </summary>
        public bool IsWeighted => Configuration.IsWeighted;

        /// <summary>
        /// Is this graph configured to have directed edges?
        /// </summary>
        public bool IsDirected => Configuration.IsDirected;

        /// <summary>
        /// An enumeration of all vertices within the graph.
        /// </summary>
        public IEnumerable<IGraphVertex<TKey, TValue>> Vertices => _vertices.Values;

        /// <summary>
        /// An enumeration of all edges within the graph.
        /// </summary>
        public IEnumerable<IGraphEdge<TKey>> Edges => _edges;

        /// <summary>
        /// The number of vertices / elements stored in the graph.
        /// </summary>
        public int VertexCount => _vertices.Count;

        /// <summary>
        /// The number of edges stored in the graph.
        /// </summary>
        public int EdgeCount => _edges.Count;

        /// <summary>
        /// An enumeration of all the names/keys of the vertices in the graph.
        /// </summary>
        public IEnumerable<TKey> Keys => _vertices.Keys;

        /// <summary>
        /// An enumeration of all the elements stored in the vertices in the graph.
        /// </summary>
        public IEnumerable<TValue> Values => _vertices.Values.Select(x => x.Value);

        private readonly Dictionary<TKey, Vertex> _vertices;
        private readonly List<Edge> _edges;

        /// <summary>
        /// Creates a new graph.
        /// </summary>
        public Graph()
            : this(default)
        { }

        /// <summary>
        /// Creates a new graph with a custom configuration.
        /// </summary>
        /// <param name="config">Some configuration of the map.</param>
        public Graph(GraphConfiguration config)
        {
            // todo: force this correction or throw configuration exception?
            if (!config.IsWeighted) { config.AllowNegativeWeight = false; }

            // Store configuration
            Configuration = config;

            // 
            _vertices = new Dictionary<TKey, Vertex>();
            _edges = new List<Edge>();
        }

        /// <summary>
        /// Gets a clone of this graph's configuration.
        /// </summary>
        public GraphConfiguration Configuration { get; }

        /// <summary>
        /// Add an edge between two nodes in the graph.
        /// </summary>
        /// <param name="source">Some name of a source node within the graph.</param>
        /// <param name="target">Some name of a target node within the graph.</param>
        /// <param name="weight">Some weight/cost to assign to the newly connected edge.</param>
        /// <returns></returns>
        public bool AddEdge(TKey source, TKey target, float weight = 1F)
        {
            // 
            if ((AllowSelfLoops == false) && Equals(source, target))
            {
                throw new GraphSelfLoopException($"Unable to add a self-connecting edge to '{source}' Disallowed by graph configuration.");
            }

            // 
            if (IsWeighted && (AllowNegativeWeight == false) && weight < 0F)
            {
                throw new GraphNegativeWeightException($"Unable to add a negatively weighted edge ({weight}) between '{source}' and '{target}'. Disallowed by graph configuration.");
            }

            // 
            if (!TryGetVertex(source, out var vertexA))
            {
                throw new KeyNotFoundException($"Unable to find vertex '{source}'.");
            }

            if (!TryGetVertex(target, out var vertexB))
            {
                throw new KeyNotFoundException($"Unable to find vertex '{target}'.");
            }

            // Does the pairing already exist?
            if (ContainsEdge(source, target))
            {
                return false;
            }
            else
            {
                // Forces weight to 1 if not weights
                if (!IsWeighted)
                {
                    weight = 1F;
                }

                // TODO: Maybe throw exception instead?
                // if( weight != 1F ) throw new ArgumentException( nameof( weight ) );

                // 
                var edge = new Edge(vertexA, vertexB, weight);

                // Add edge [ a -> b ]
                vertexA.AddEdge(edge);

                // Add edge [ b -> a ]
                if (IsDirected == false)
                {
                    vertexB.AddEdge(edge);
                }

                // Add to graph
                _edges.Add(edge);

                return true;
            }
        }

        /// <summary>
        /// Adds a vertex to the given graph via the given name/key.
        /// </summary>
        /// <param name="key">The name/key to identify the element.</param>
        /// <param name="element">Some element to store in the graph.</param>
        /// <returns>True, if the element could be added...?</returns>
        public bool AddVertex(TKey key, TValue element)
        {
            if (_vertices.ContainsKey(key))
            {
                // Should this update the value?
                return false;
            }
            else
            {
                var vertex = new Vertex(key, element, this);
                _vertices.Add(key, vertex);
                return true;
            }
        }

        /// <summary>
        /// Removes all vertices and edges within the graph.
        /// </summary>
        public void Clear()
        {
            // Remove all edges
            ClearEdges();

            // Clear vertices
            _vertices.Clear();
        }

        /// <summary>
        /// Removes all edges within the graph.
        /// </summary>
        public void ClearEdges()
        {
            // Do we have vertices?
            if (_vertices.Count > 0)
            {
                // Clears the edges from all vertices
                foreach (var vertex in _vertices)
                {
                    vertex.Value.ClearEdges();
                }
            }

            // Clear edges known by the graph
            _edges.Clear();
        }

        /// <summary>
        /// Determines if this graph contains the element ( by name ) requested.
        /// </summary>
        /// <param name="key">Some key/name of an element possibly within the graph.</param>
        /// <returns>True, if the element was contained.</returns>
        public bool ContainsVertex(TKey key)
        {
            return _vertices.ContainsKey(key);
        }

        /// <summary>
        /// Determines if the graph contains an edge bewtween source and target vertices.
        /// </summary>
        /// <param name="source">Some name of a source node within the graph.</param>
        /// <param name="target">Some name of a target node within the graph.</param>
        /// <returns>True, if the edge was contained.</returns>
        public bool ContainsEdge(TKey source, TKey target)
        {
            if (!TryGetVertex(source, out var vertexA))
            {
                return false;
            }

            if (!TryGetVertex(target, out var vertexB))
            {
                return false;
            }

            // Determine if edge is contained ( directed )
            var containsEdge = vertexA.ContainsEdge(target);

            // Determine if edge is contained ( undirected )
            if (!containsEdge && IsDirected == false)
            {
                containsEdge = vertexB.ContainsEdge(source);
            }

            return containsEdge;
        }

        /// <summary>
        /// Determines if this graph contains the element requested.
        /// </summary>
        /// <param name="value">Some element possibly within the graph.</param>
        /// <returns>True, if the element was contained.</returns>
        public bool ContainsValue(TValue value)
        {
            // Better way? This'd be a bit slow ( Memory vs Performance )
            foreach (var val in _vertices.Select(k => k.Value.Value))
            {
                if (Equals(value, val))
                {
                    return true;
                }
            }

            return false;
        }

        /// <summary>
        /// Gets an edge in the graph.
        /// </summary>
        /// <param name="source">Some name of a source node within the graph.</param>
        /// <param name="target">Some name of a target node within the graph.</param>
        /// <returns>An edge representing the connection between source and target vertices.</returns>
        public IGraphEdge<TKey> GetEdge(TKey source, TKey target)
        {
            // 
            if (!TryGetVertex(source, out var vertexA))
            {
                throw new KeyNotFoundException($"Unable to find vertex '{source}'.");
            }

            if (!TryGetVertex(target, out var vertexB))
            {
                throw new KeyNotFoundException($"Unable to find vertex '{target}'.");
            }

            // 
            if (IsDirected)
            {
                return vertexA.FindEdge(target);
            }
            else
            {
                // Since an undirected edge may come from both sides, we must try both keys.
                var edge = vertexA.FindEdge(target);
                if (edge == null)
                {
                    edge = vertexA.FindEdge(source);
                }

                return edge;
            }
        }

        /// <summary>
        /// Gets a vertex identified with the given key.
        /// </summary>
        /// <param name="key"> Some known key in the graph. </param>
        /// <returns> An instance of the vertex created when a key-value pair was added to the graph. </returns>
        /// <exception cref="KeyNotFoundException"> If the key is unknown. </exception>
        public IGraphVertex<TKey, TValue> GetVertex(TKey key)
        {
            if (!TryGetVertex(key, out var vertex))
            {
                throw new KeyNotFoundException($"Unable to find vertex '{key}'.");
            }

            return vertex;
        }

        /// <summary>
        /// Removes an edge between two vertices in the graph.
        /// </summary>
        /// <param name="source">Some name of a source node within the graph.</param>
        /// <param name="target">Some name of a target node within the graph.</param>
        /// <returns></returns>
        public bool RemoveEdge(TKey source, TKey target)
        {
            //
            if (!TryGetVertex(source, out var vertexA))
            {
                throw new KeyNotFoundException($"Unable to find vertex '{source}'.");
            }

            if (!TryGetVertex(target, out var vertexB))
            {
                throw new KeyNotFoundException($"Unable to find vertex '{target}'.");
            }

            // 
            if (GetEdge(source, target) is Edge edge)
            {
                // 
                vertexA.RemoveEdge(edge);

                // If an undirected graph, vertex B also loses this edge
                if (!IsDirected)
                {
                    vertexB.RemoveEdge(edge);
                }

                // 
                _edges.Remove(edge);

                return true;
            }
            else
            {
                // Edge did not exist
                return false;
            }
        }

        /// <summary>
        /// Removes the given vertex from the graph ( also disconnects associated edges ).
        /// </summary>
        /// <param name="key">Some name of a node within the graph.</param>
        /// <returns>True, if the element existed and was removed.</returns>
        public bool RemoveVertex(TKey key)
        {
            if (!TryGetVertex(key, out var vertex))
            {
                throw new KeyNotFoundException($"Unable to find vertex '{key}'.");
            }

            // Clear edges from graph that are connected to this vertex 
            foreach (var edge in vertex.Edges)
            {
                _edges.Remove(edge);
            }

            // Remove edges from neighboring vertices
            foreach (var edge in vertex.Edges)
            {
                // Remove edge from target, if directed removing by source 
                // vertex key. If undirected, removing by edge reference
                if (IsDirected)
                {
                    // Will remove any edge that connects back to the vertex
                    if (edge.TargetVertex.ContainsEdge(key))
                    {
                        edge.TargetVertex.RemoveEdge(key);
                    }
                }
                else
                {
                    // 
                    edge.TargetVertex.RemoveEdge(edge);
                }
            }

            // Clear vertex edges
            vertex.ClearEdges();

            // Remove vertex from graph
            return _vertices.Remove(key);
        }

        private bool TryGetVertex(TKey key, out Vertex vertex)
        {
            if (_vertices.ContainsKey(key))
            {
                vertex = _vertices[key];
                return true;
            }

            // No luck
            vertex = null;
            return false;
        }

        private class Edge : IGraphEdge<TKey>
        {
            public TKey Source => SourceVertex.Key;

            public TKey Target => TargetVertex.Key;

            public readonly Vertex SourceVertex;
            public readonly Vertex TargetVertex;

            public float Weight { get; set; }

            public Edge(Vertex source, Vertex target, float weight)
            {
                SourceVertex = source;
                TargetVertex = target;
                Weight = weight;
            }

            public TKey GetOtherKey(TKey key)
            {
                if (key == null)
                {
                    throw new ArgumentNullException(nameof(key));
                }

                if (Equals(Source, key))
                {
                    return Target;
                }

                if (Equals(Target, key))
                {
                    return Source;
                }

                throw new ArgumentException($"Unable to get opposite key on edge. Key given was not either source or target.");
            }
        }

        private class Vertex : IGraphVertex<TKey, TValue>
        {
            public TKey Key { get; private set; }

            public TValue Value { get; set; }

            IReadOnlyList<IGraphEdge<TKey>> IGraphVertex<TKey, TValue>.IncomingEdges => IncomingEdges;

            IReadOnlyList<IGraphEdge<TKey>> IGraphVertex<TKey, TValue>.Edges => Edges;

            public IReadOnlyList<Edge> IncomingEdges => _inEdges ?? throw new InvalidOperationException("With an undirected graph, there are no incoming edges.");

            public IReadOnlyList<Edge> Edges => _edges;

            private readonly List<Edge> _inEdges;
            private readonly List<Edge> _edges;

            private readonly Graph<TKey, TValue> _graph;

            public Vertex(TKey key, TValue value, Graph<TKey, TValue> graph)
            {
                // 
                _graph = graph;

                //
                _edges = new List<Edge>();

                // 
                if (_graph.IsDirected)
                {
                    _inEdges = new List<Edge>();
                }

                // 
                Value = value;
                Key = key;
            }

            public bool ContainsEdge(TKey target)
            {
                var idx = _edges.FindIndex(e => Equals(e.Target, target));
                if (idx >= 0)
                {
                    return true;
                }
                else
                {
                    return false;
                }
            }

            public IGraphEdge<TKey> FindEdge(TKey target)
            {
                var idx = _edges.FindIndex(edge => Equals(edge.Target, target));
                if (idx >= 0)
                {
                    return _edges[idx];
                }
                else
                {
                    return null;
                }
            }

            public void AddEdge(Edge edge)
            {
                // Add to outgoing edge set ( local )
                _edges.Add(edge);

                // Add to the incoming edge set ( other )
                if (_graph.IsDirected)
                {
                    var other = edge.TargetVertex;
                    other._inEdges.Add(edge);
                }
            }

            public void RemoveEdge(Edge edge)
            {
                if (edge == null)
                {
                    throw new ArgumentNullException(nameof(edge));
                }

                // Remove from the outgoing edge set ( local )
                _edges.Remove(edge);

                // Remove from the incoming edge set ( other ) 
                if (_graph.IsDirected)
                {
                    var other = edge.TargetVertex;
                    other._inEdges.Remove(edge);
                }
            }

            public void RemoveEdge(TKey target)
            {
                var edge = FindEdge(target);
                if (edge == null)
                {
                    throw new InvalidOperationException($"Unable to find edge with target vertex '{target}'.");
                }

                RemoveEdge(edge as Edge);
            }

            public void ClearEdges()
            {
                // For outgoing edges ( child edges )
                foreach (var edge in Edges)
                {
                    // Removes ( A -> B ) on B
                    var other = edge.TargetVertex;
                    other.RemoveEdge(edge);
                }

                if (_graph.IsDirected)
                {
                    // For incoming edges ( parent edges )
                    foreach (var edge in IncomingEdges)
                    {
                        // Removes ( B -> A ) on B
                        var other = edge.SourceVertex;
                        other.RemoveEdge(edge);
                    }
                }

                // Clears outgoing edges 
                _inEdges?.Clear();
                _edges.Clear();
            }
        }
    }
}
