using System;
using System.Collections.Generic;
using System.Linq;

namespace Heirloom.Collections
{
    /// <summary>
    /// An undirected graph implemented using adjacency lists.
    /// </summary>
    /// <tags>Graph, Undirected</tags>
    /// <category>Graph</category>
    public sealed class Graph<TVertex> : IGraph<TVertex>
    {
        private readonly Dictionary<TVertex, List<GraphEdge<TVertex>>> _neighbors;
        private readonly Dictionary<TVertex, float> _weights;
        private readonly EdgeCollection _edges;

        /// <summary>
        /// Constructs a new <see cref="Graph{T}"/>.
        /// </summary>
        public Graph()
        {
            _neighbors = new Dictionary<TVertex, List<GraphEdge<TVertex>>>();
            _weights = new Dictionary<TVertex, float>();
            _edges = new EdgeCollection();
        }

        /// <inheritdoc/>
        public IEnumerable<TVertex> Vertices => _neighbors.Keys;

        /// <inheritdoc/>
        public int VertexCount => _neighbors.Count;

        /// <inheritdoc/>
        public IEnumerable<(TVertex A, TVertex B)> Edges => _edges.Select(e => (e.A, e.B));

        /// <inheritdoc/>
        public int EdgeCount => _edges.Count;

        /// <inheritdoc/>
        public void Clear()
        {
            _neighbors.Clear();
            _weights.Clear();
            _edges.Clear();
        }

        #region Vertex Manipulation

        /// <inheritdoc/>
        public void AddVertex(TVertex v, float weight = 1F)
        {
            if (ContainsVertex(v))
            {
                throw new ArgumentException($"Graph already contained the vertex '{v}'.", nameof(v));
            }

            // Construct adjacency list for vertex
            _neighbors[v] = new List<GraphEdge<TVertex>>();
            _weights[v] = weight;
        }

        /// <inheritdoc/>
        public bool RemoveVertex(TVertex v)
        {
            // Do we know about this vertex?
            if (_neighbors.TryGetValue(v, out var outgoing))
            {
                // Disconnect outgoing edges connected to this vertex
                outgoing.Apply(e => RemoveEdge(e.A, e.B));
                _neighbors.Remove(v);

                // Remove weight record
                _weights.Remove(v);

                // Remove vertex and associated eges
                return true;
            }

            // Vertex was not contained, so return false.
            return false;
        }

        /// <inheritdoc/>
        public bool ContainsVertex(TVertex v)
        {
            return _neighbors.ContainsKey(v);
        }

        /// <inheritdoc/>
        public float GetVertexWeight(TVertex v)
        {
            if (_weights.TryGetValue(v, out var weight))
            {
                return weight;
            }
            else
            {
                throw new ArgumentException($"Unable to get vertex weight, vertex does not exist.", nameof(v));
            }
        }

        /// <inheritdoc/>
        public void SetVertexWeight(TVertex v, float weight)
        {
            if (!ContainsVertex(v)) { throw new ArgumentException($"Unable to set vertex weight, vertex does not exist.", nameof(v)); }
            _weights[v] = weight;
        }

        #endregion

        #region Edge Manipulation

        /// <inheritdoc/>
        public void AddEdge(TVertex a, TVertex b, float weight = 1F)
        {
            // Validate vertices
            if (!ContainsVertex(a)) { throw new ArgumentException($"Unable to add edge, vertex does not exist.", nameof(a)); }
            if (!ContainsVertex(b)) { throw new ArgumentException($"Unable to add edge, vertex does not exist.", nameof(b)); }
            if (Equals(a, b)) { throw new InvalidOperationException($"Unable to add edge, vertices must be different."); }

            // Insert edge into edge collection
            var edge = new GraphEdge<TVertex>(a, b, weight);
            _edges.AddEdge(edge);

            // Add edge to both
            _neighbors[a].Add(edge);
            _neighbors[b].Add(edge);
        }

        /// <inheritdoc/>
        public bool RemoveEdge(TVertex a, TVertex b)
        {
            // Validate vertices
            if (!ContainsVertex(a)) { throw new ArgumentException($"Unable to remove edge, vertex does not exist.", nameof(a)); }
            if (!ContainsVertex(b)) { throw new ArgumentException($"Unable to remove edge, vertex does not exist.", nameof(b)); }

            // Does edge exist?
            if (_edges.TryGetEdge(a, b, out var edge))
            {
                // Remove edge from edge collection
                _edges.RemoveEdge(edge);

                // Remove from adjacency lists
                _neighbors[a].Remove(edge);
                _neighbors[b].Remove(edge);

                // Edge was contained and removed
                return true;
            }

            // Edge was not contained, unable to remove
            return false;
        }

        /// <inheritdoc/>
        public bool ContainsEdge(TVertex a, TVertex b)
        {
            return _edges.Contains(a, b);
        }

        /// <inheritdoc/>
        public float GetEdgeWeight(TVertex a, TVertex b)
        {
            if (_edges.TryGetEdge(a, b, out var edge))
            {
                return edge.Weight;
            }
            else
            {
                throw new ArgumentException($"Unable to retreive edge weight, graph does not contain the specified edge.");
            }
        }

        /// <inheritdoc/>
        public void SetEdgeWeight(TVertex a, TVertex b, float weight)
        {
            if (_edges.TryGetEdge(a, b, out var edge))
            {
                edge.Weight = weight;
            }
            else
            {
                throw new ArgumentException($"Unable to set edge weight, graph does not contain the specified edge.");
            }
        }

        #endregion

        #region Neighbors

        /// <inheritdoc/>
        public IEnumerable<TVertex> GetNeighbors(TVertex v)
        {
            if (!ContainsVertex(v)) { throw new ArgumentException($"Unable to return neighbors, vertex does not exist.", nameof(v)); }

            // Return edge targets (ie, successors)
            return _neighbors[v].Select(e => e.GetOther(v));
        }

        /// <inheritdoc/>
        public int GetDegree(TVertex v)
        {
            if (!ContainsVertex(v)) { throw new ArgumentException($"Unable to return degree, vertex does not exist.", nameof(v)); }

            return _neighbors[v].Count;
        }

        #endregion

        #region Algorithms

        /// <inheritdoc/>
        public IReadOnlyList<TVertex> FindPath(TVertex start, TVertex goal, HeuristicCost<TVertex> heuristic)
        {
            if (!ContainsVertex(start)) { throw new ArgumentException($"Unable to find path, vertex does not exist.", nameof(start)); }
            if (!ContainsVertex(goal)) { throw new ArgumentException($"Unable to find path, vertex does not exist.", nameof(goal)); }
            if (heuristic is null) { throw new ArgumentNullException(nameof(heuristic)); }

            return Search.HeuristicSearch(start, goal, GetNeighbors, GetEdgeWeight, heuristic);
        }

        /// <inheritdoc/>
        public IReadOnlyList<TVertex> FindPath(TVertex start, Func<TVertex, bool> goalCondition, HeuristicCost<TVertex> heuristic)
        {
            if (!ContainsVertex(start)) { throw new ArgumentException($"Unable to find path, vertex does not exist.", nameof(start)); }
            if (goalCondition is null) { throw new ArgumentNullException(nameof(goalCondition)); }
            if (heuristic is null) { throw new ArgumentNullException(nameof(heuristic)); }

            return Search.HeuristicSearch(start, goalCondition, GetNeighbors, GetEdgeWeight, heuristic);
        }

        /// <inheritdoc/>
        public IEnumerable<TVertex> Traverse(TVertex start, TraversalMethod method)
        {
            if (!ContainsVertex(start)) { throw new ArgumentException($"Unable to traverse graph, vertex does not exist.", nameof(start)); }

            return method switch
            {
                TraversalMethod.BreadthFirst => Search.BreadthFirst(start, GetNeighbors),
                TraversalMethod.DepthFirst => Search.DepthFirst(start, GetNeighbors),

                _ => throw new ArgumentException($"Invalid traversal method.", nameof(method)),
            };
        }

        /// <inheritdoc/>
        public Graph<TVertex> FindMinimumSpanningTree()
        {
            // todo: recycle/optimize use of C E and Q?
            var C = new Dictionary<TVertex, float>();
            var E = new Dictionary<TVertex, TVertex>();
            var Q = new Heap<TVertex>((a, b) => C[a].CompareTo(C[b]));

            // Construct output graph (ie, the forest)
            var F = new Graph<TVertex>();

            // Initialize
            foreach (var v in Vertices)
            {
                C[v] = float.PositiveInfinity;
                Q.Add(v);
            }

            while (Q.Count > 0)
            {
                var v = Q.Remove();

                // Insert vertex into tree
                F.AddVertex(v);

                // Add best edge to tree, if a minimum weight is known.
                if (E.TryGetValue(v, out var e))
                {
                    F.AddEdge(e, v, C[v]);
                }

                // 
                foreach (var edge in _neighbors[v])
                {
                    var w = edge.GetOther(v);

                    if (Q.Contains(w))
                    {
                        if (edge.Weight < C[w])
                        {
                            C[w] = edge.Weight;
                            E[w] = v;

                            // Notify the queue the priority of the item has changed
                            Q.Update(w);
                        }
                    }
                }
            }

            return F;
        }

        /// <inheritdoc/>
        IGraph<TVertex> IGraph<TVertex>.FindMinimumSpanningTree()
        {
            return FindMinimumSpanningTree();
        }

        #endregion

        private class EdgeCollection : EdgeCollection<UnorderedPair<TVertex>, TVertex>
        {
            protected override UnorderedPair<TVertex> CreatePair(TVertex a, TVertex b)
            {
                return new UnorderedPair<TVertex>(a, b);
            }
        }
    }
}
