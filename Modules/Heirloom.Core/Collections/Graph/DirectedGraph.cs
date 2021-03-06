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
    public sealed class DirectedGraph<T> : IDirectedGraph<T>
    {
        private readonly Dictionary<T, List<Edge<T>>> _outgoing;
        private readonly Dictionary<T, List<Edge<T>>> _incoming;
        private readonly EdgeCollection _arcs;

        /// <summary>
        /// Constructs a new <see cref="DirectedGraph{T}"/>.
        /// </summary>
        public DirectedGraph()
        {
            _outgoing = new Dictionary<T, List<Edge<T>>>();
            _incoming = new Dictionary<T, List<Edge<T>>>();
            _arcs = new EdgeCollection();
        }

        /// <inheritdoc/>
        public IEnumerable<T> Vertices => _outgoing.Keys;

        /// <inheritdoc/>
        public int VertexCount => _outgoing.Count;

        /// <inheritdoc/>
        public IEnumerable<(T A, T B)> Arcs => _arcs.Select(e => (e.A, e.B));

        /// <inheritdoc/>
        public int ArcCount => _arcs.Count;

        /// <inheritdoc/>
        public void Clear()
        {
            _outgoing.Clear();
            _incoming.Clear();
            _arcs.Clear();
        }

        #region Vertex Manipulation

        /// <inheritdoc/>
        public void AddVertex(T v)
        {
            if (ContainsVertex(v))
            {
                throw new ArgumentException($"Graph already contained the vertex '{v}'.", nameof(v));
            }

            // Construct adjacency lists for vertex
            _incoming[v] = new List<Edge<T>>();
            _outgoing[v] = new List<Edge<T>>();
        }

        /// <inheritdoc/>
        public bool RemoveVertex(T v)
        {
            // Do we know about this vertex?
            if (_outgoing.TryGetValue(v, out var outgoing))
            {
                // Remove incoming arcs
                var incoming = _incoming[v];

                // Disconnect incoming arcs connected to this vertex
                incoming.Apply(e => RemoveArc(e.A, e.B));
                _incoming.Remove(v);

                // Disconnect outgoing arcs connected to this vertex
                outgoing.Apply(e => RemoveArc(e.A, e.B));
                _outgoing.Remove(v);

                // Remove vertex and associated eges
                return true;
            }

            // Vertex was not contained, so return false.
            return false;
        }

        /// <inheritdoc/>
        public bool ContainsVertex(T v)
        {
            return _outgoing.ContainsKey(v);
        }

        #endregion

        #region Arc Manipulation

        /// <inheritdoc/>
        public void AddArc(T a, T b, float weight)
        {
            // Validate vertices
            if (!ContainsVertex(a)) { throw new ArgumentException($"Unable to add arc, vertex does not exist.", nameof(a)); }
            if (!ContainsVertex(b)) { throw new ArgumentException($"Unable to add arc, vertex does not exist.", nameof(b)); }
            if (Equals(a, b)) { throw new InvalidOperationException($"Unable to add arc, vertices must be different."); }

            // Create and insert arc into arc lookup
            var arc = new Edge<T>(a, b, weight);
            _arcs.AddEdge(arc);

            // Add arc to adjacency lists
            _outgoing[a].Add(arc);
            _incoming[b].Add(arc);
        }

        /// <inheritdoc/>
        public bool RemoveArc(T a, T b)
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
        public bool ContainsArc(T a, T b)
        {
            return _arcs.Contains(a, b);
        }

        /// <inheritdoc/>
        public float GetArcWeight(T a, T b)
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
        public void SetArcWeight(T a, T b, float weight)
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

        /// <inheritdoc/>
        public IEnumerable<T> GetSuccessors(T v)
        {
            if (!ContainsVertex(v)) { throw new ArgumentException($"Unable to return neighbors, vertex does not exist.", nameof(v)); }

            // Return outgoing arc targets (ie, successors)
            return _outgoing[v].Select(e => e.GetOther(v));
        }

        /// <inheritdoc/>
        public IEnumerable<T> GetPredecessors(T v)
        {
            if (!ContainsVertex(v)) { throw new ArgumentException($"Unable to return neighbors, vertex does not exist.", nameof(v)); }

            // Return incoming arc sources (ie, predecessors)
            return _incoming[v].Select(e => e.GetOther(v));
        }

        #region Algorithms

        /// <inheritdoc/>
        public IReadOnlyList<T> FindPath(T start, T goal, HeuristicCost<T> heuristic)
        {
            if (!ContainsVertex(start)) { throw new ArgumentException($"Unable to find path, vertex does not exist.", nameof(start)); }
            if (!ContainsVertex(goal)) { throw new ArgumentException($"Unable to find path, vertex does not exist.", nameof(goal)); }
            if (heuristic is null) { throw new ArgumentNullException(nameof(heuristic)); }

            return Search.HeuristicSearch(start, goal, GetSuccessors, GetArcWeight, heuristic);
        }

        /// <inheritdoc/>
        public IReadOnlyList<T> FindPath(T start, Func<T, bool> goalCondition, HeuristicCost<T> heuristic)
        {
            if (!ContainsVertex(start)) { throw new ArgumentException($"Unable to find path, vertex does not exist.", nameof(start)); }
            if (goalCondition is null) { throw new ArgumentNullException(nameof(goalCondition)); }
            if (heuristic is null) { throw new ArgumentNullException(nameof(heuristic)); }

            return Search.HeuristicSearch(start, goalCondition, GetSuccessors, GetArcWeight, heuristic);
        }

        /// <inheritdoc/>
        public IEnumerable<T> Traverse(T start, TraversalMethod method)
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

        private class EdgeCollection : EdgeCollection<EdgeCollection.Pair, T>
        {
            protected override Pair CreatePair(T a, T b)
            {
                return new Pair(a, b);
            }

            internal readonly struct Pair : IEquatable<Pair>
            {
                public readonly T A;

                public readonly T B;

                public Pair(T a, T b)
                {
                    A = a;
                    B = b;
                }

                public override bool Equals(object obj)
                {
                    return obj is Pair pair
                        && Equals(pair);
                }

                public bool Equals(Pair other)
                {
                    return A.Equals(other.A) && B.Equals(other.B);
                }

                public override int GetHashCode()
                {
                    return HashCode.Combine(A, B);
                }
            }
        }
    }
}
