using System;
using System.Collections.Generic;
using System.Linq;

namespace Heirloom.Collections
{
    /// <summary>
    /// A directed graph implemented using adjacency lists.
    /// </summary>
    /// <tags>Graph, Directed</tags>
    /// <category>Graph</category>
    public sealed class DirectedGraph<TVertex> : IDirectedGraph<TVertex>
    {
        private readonly Dictionary<TVertex, List<GraphEdge<TVertex>>> _outgoing;
        private readonly Dictionary<TVertex, List<GraphEdge<TVertex>>> _incoming;
        private readonly Dictionary<TVertex, float> _weights;
        private readonly ArcCollection _arcs;

        /// <summary>
        /// Constructs a new <see cref="DirectedGraph{T}"/>.
        /// </summary>
        public DirectedGraph()
        {
            _outgoing = new Dictionary<TVertex, List<GraphEdge<TVertex>>>();
            _incoming = new Dictionary<TVertex, List<GraphEdge<TVertex>>>();
            _weights = new Dictionary<TVertex, float>();
            _arcs = new ArcCollection();
        }

        /// <inheritdoc/>
        public IEnumerable<TVertex> Vertices => _outgoing.Keys;

        /// <inheritdoc/>
        public int VertexCount => _outgoing.Count;

        /// <inheritdoc/>
        public IEnumerable<(TVertex A, TVertex B)> Arcs => _arcs.Select(e => (e.A, e.B));

        /// <inheritdoc/>
        public int ArcCount => _arcs.Count;

        /// <inheritdoc/>
        public void Clear()
        {
            _outgoing.Clear();
            _incoming.Clear();
            _weights.Clear();
            _arcs.Clear();
        }

        #region Vertex Manipulation

        /// <inheritdoc/>
        public void AddVertex(TVertex v, float weight = 1F)
        {
            if (ContainsVertex(v))
            {
                throw new ArgumentException($"Graph already contained the vertex '{v}'.", nameof(v));
            }

            // Construct adjacency lists for vertex
            _incoming[v] = new List<GraphEdge<TVertex>>();
            _outgoing[v] = new List<GraphEdge<TVertex>>();
            _weights[v] = weight;
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

        /// <inheritdoc/>
        public bool RemoveVertex(TVertex v)
        {
            // Do we know about this vertex?
            if (_outgoing.TryGetValue(v, out var outgoing))
            {
                // Remove incoming arcs
                var incoming = _incoming[v];

                Console.WriteLine("Removing incoming arcs");

                // Disconnect incoming arcs connected to this vertex
                // incoming.Apply(e => RemoveArc(e.A, e.B));
                foreach (var e in incoming)
                {
                    RemoveArc(e.A, e.B);
                }

                Console.WriteLine("Removing outgoing arcs");

                // Disconnect outgoing arcs connected to this vertex
                // outgoing.Apply(e => RemoveArc(e.A, e.B));
                foreach (var e in outgoing)
                {
                    RemoveArc(e.A, e.B);
                }

                Console.WriteLine("Removing vertex properties");

                // Remove vertex info
                _outgoing.Remove(v);
                _incoming.Remove(v);
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
            return _outgoing.ContainsKey(v);
        }

        #endregion

        #region Arc Manipulation

        /// <inheritdoc/>
        public void AddArc(TVertex a, TVertex b, float weight = 1F)
        {
            // Validate vertices
            if (!ContainsVertex(a)) { throw new ArgumentException($"Unable to add arc, vertex does not exist.", nameof(a)); }
            if (!ContainsVertex(b)) { throw new ArgumentException($"Unable to add arc, vertex does not exist.", nameof(b)); }
            if (Equals(a, b)) { throw new InvalidOperationException($"Unable to add arc, vertices must be different."); }

            // Create and insert arc into arc lookup
            var arc = new GraphEdge<TVertex>(a, b, weight);
            _arcs.AddEdge(arc);

            // Add arc to adjacency lists
            _outgoing[a].Add(arc);
            _incoming[b].Add(arc);
        }

        /// <inheritdoc/>
        public bool RemoveArc(TVertex a, TVertex b)
        {
            // Validate vertices
            if (!ContainsVertex(a)) { throw new ArgumentException($"Unable to remove arc, vertex does not exist.", nameof(a)); }
            if (!ContainsVertex(b)) { throw new ArgumentException($"Unable to remove arc, vertex does not exist.", nameof(b)); }

            // Does arc exist?
            if (_arcs.TryGetEdge(a, b, out var arc))
            {
                // Remove arc from arc collection
                _arcs.RemoveEdge(arc);

                // Remove arc from adjacency list
                _outgoing[a].Remove(arc);
                _incoming[b].Remove(arc);

                // Arc was contained and removed
                return true;
            }

            // Arc was not contained, unable to remove
            return false;
        }

        /// <inheritdoc/>
        public bool ContainsArc(TVertex a, TVertex b)
        {
            return _arcs.Contains(a, b);
        }

        /// <inheritdoc/>
        public float GetArcWeight(TVertex a, TVertex b)
        {
            if (_arcs.TryGetEdge(a, b, out var arc))
            {
                return arc.Weight;
            }
            else
            {
                throw new ArgumentException($"Unable to retreive arc weight, graph does not contain the specified arc.");
            }
        }

        /// <inheritdoc/>
        public void SetArcWeight(TVertex a, TVertex b, float weight)
        {
            if (_arcs.TryGetEdge(a, b, out var arc))
            {
                arc.Weight = weight;
            }
            else
            {
                throw new ArgumentException($"Unable to set arc weight, graph does not contain the specified arc.");
            }
        }

        #endregion

        #region Successors and Predecessors

        /// <inheritdoc/>
        public IEnumerable<TVertex> GetSuccessors(TVertex v)
        {
            if (!ContainsVertex(v)) { throw new ArgumentException($"Unable to return neighbors, vertex does not exist.", nameof(v)); }

            // Return outgoing arc targets (ie, successors)
            return _outgoing[v].Select(e => e.GetOther(v)).ToArray();
        }

        /// <inheritdoc/>
        public IEnumerable<TVertex> GetPredecessors(TVertex v)
        {
            if (!ContainsVertex(v)) { throw new ArgumentException($"Unable to return neighbors, vertex does not exist.", nameof(v)); }

            // Return incoming arc sources (ie, predecessors)
            return _incoming[v].Select(e => e.GetOther(v)).ToArray();
        }

        /// <inheritdoc/>
        public int GetOutDegree(TVertex v)
        {
            if (!ContainsVertex(v)) { throw new ArgumentException($"Unable to return outgoing degree, vertex does not exist.", nameof(v)); }
            return _outgoing[v].Count;
        }

        /// <inheritdoc/>
        public int GetInDegree(TVertex v)
        {
            if (!ContainsVertex(v)) { throw new ArgumentException($"Unable to return incoming degree, vertex does not exist.", nameof(v)); }
            return _incoming[v].Count;
        }

        #endregion

        #region Algorithms

        /// <inheritdoc/>
        public IReadOnlyList<TVertex> FindPath(TVertex start, TVertex goal, HeuristicCost<TVertex> heuristic)
        {
            if (!ContainsVertex(start)) { throw new ArgumentException($"Unable to find path, vertex does not exist.", nameof(start)); }
            if (!ContainsVertex(goal)) { throw new ArgumentException($"Unable to find path, vertex does not exist.", nameof(goal)); }
            if (heuristic is null) { throw new ArgumentNullException(nameof(heuristic)); }

            return Search.HeuristicSearch(start, goal, GetSuccessors, GetArcWeight, heuristic);
        }

        /// <inheritdoc/>
        public IReadOnlyList<TVertex> FindPath(TVertex start, Func<TVertex, bool> goalCondition, HeuristicCost<TVertex> heuristic)
        {
            if (!ContainsVertex(start)) { throw new ArgumentException($"Unable to find path, vertex does not exist.", nameof(start)); }
            if (goalCondition is null) { throw new ArgumentNullException(nameof(goalCondition)); }
            if (heuristic is null) { throw new ArgumentNullException(nameof(heuristic)); }

            return Search.HeuristicSearch(start, goalCondition, GetSuccessors, GetArcWeight, heuristic);
        }

        /// <inheritdoc/>
        public IEnumerable<TVertex> Traverse(TVertex start, TraversalMethod method)
        {
            if (!ContainsVertex(start)) { throw new ArgumentException($"Unable to traverse graph, vertex does not exist.", nameof(start)); }

            return method switch
            {
                TraversalMethod.BreadthFirst => Search.BreadthFirst(start, GetSuccessors),
                TraversalMethod.DepthFirst => Search.DepthFirst(start, GetSuccessors),

                _ => throw new ArgumentException($"Invalid traversal method.", nameof(method)),
            };
        }

        #endregion

        private class ArcCollection : EdgeCollection<Pair<TVertex>, TVertex>
        {
            protected override Pair<TVertex> CreatePair(TVertex a, TVertex b)
            {
                return new Pair<TVertex>(a, b);
            }
        }
    }
}
