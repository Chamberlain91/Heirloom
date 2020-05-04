using System;
using System.Collections.Generic;
using System.Linq;

namespace Heirloom.Collections
{
    public sealed class UndirectedGraph<T> : IGraph<T>
    {
        private readonly Dictionary<T, List<Edge>> _vertices;
        private readonly Dictionary<Pair, Edge> _edges;

        public UndirectedGraph()
        {
            _vertices = new Dictionary<T, List<Edge>>();
            _edges = new Dictionary<Pair, Edge>();
        }

        public bool IsDirected => false;

        public IEnumerable<T> Vertices => _vertices.Keys;

        public IEnumerable<(T A, T B)> Edges => _edges.Keys.Select(e => (e.A, e.B));

        public int VertexCount => _vertices.Count;

        public int EdgeCount => _edges.Count;

        public void Clear()
        {
            _vertices.Clear();
            _edges.Clear();
        }

        public void Add(T v)
        {
            if (_vertices.ContainsKey(v))
            {
                throw new ArgumentException($"Graph already contained the vertex '{v}'.", nameof(v));
            }

            // 
            _vertices[v] = new List<Edge>();
        }

        public bool Remove(T v)
        {
            if (_vertices.TryGetValue(v, out var edges))
            {
                // Disconnect edges connected to this vertex
                edges.Apply(e => Disconnect(e.Vertices.A, e.Vertices.B));
                return _vertices.Remove(v);
            }
            else
            {
                // Vertex was not contained, so return false.
                return false;
            }
        }

        public bool IsConnected(T a, T b)
        {
            var vertexPair = new Pair(a, b);
            return _edges.ContainsKey(vertexPair);
        }

        public void Connect(T a, T b, float weight)
        {
            var vertexPair = new Pair(a, b);

            if (_edges.ContainsKey(vertexPair))
            {
                throw new InvalidOperationException($"Edge already exists between '{a}' and '{b}'.");
            }

            // Construct edge
            var edge = new Edge(vertexPair, weight);
            _edges.Add(vertexPair, edge);

            // Add edge to each vertex
            _vertices[a].Add(edge);
            _vertices[b].Add(edge);
        }

        public void Disconnect(T a, T b)
        {
            var vertexPair = new Pair(a, b);

            if (!_edges.ContainsKey(vertexPair))
            {
                throw new InvalidOperationException($"Edge does not exist between '{a}' and '{b}'.");
            }

            // Get edge and remove from edge set
            var edge = _edges[vertexPair];
            _edges.Remove(vertexPair);

            // Remove edge from each vertex
            _vertices[a].Remove(edge);
            _vertices[b].Remove(edge);
        }

        public IEnumerable<T> GetSuccessors(T v)
        {
            return _vertices[v].Select(e => e.GetOther(v));
        }

        public float GetWeight(T a, T b)
        {
            var vertexPair = new Pair(a, b);

            if (!_edges.ContainsKey(vertexPair)) { throw new InvalidOperationException($"Edge does not exist between '{a}' and '{b}'."); }
            if (!_edges.ContainsKey(new Pair(b, a))) { throw new InvalidOperationException($"Edge does not exist between '{b}' and '{a}'."); }

            var edge = _edges[vertexPair];
            return edge.Weight;
        }

        public void SetWeight(T a, T b, float weight)
        {
            var vertexPair = new Pair(a, b);

            if (!_edges.ContainsKey(vertexPair))
            {
                throw new InvalidOperationException($"Edge does not exist between '{a}' and '{b}'.");
            }

            var edge = _edges[vertexPair];
            edge.Weight = weight;
        }

        public bool Contains(T v)
        {
            return _vertices.ContainsKey(v);
        }

        #region Algorithms

        /// <summary>
        /// Finds a minimum spanning tree using Prim's algorithm.
        /// </summary>
        public UndirectedGraph<T> FindMinimumSpanningTree()
        {
            var C = new Dictionary<T, float>();
            var E = new Dictionary<T, T>();

            var Q = new Heap<T>((a, b) => C[a].CompareTo(C[b]));
            var F = new UndirectedGraph<T>();

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
                F.Add(v);

                // Add best edge to tree, if a minimum weight is known.
                if (E.TryGetValue(v, out var e))
                {
                    F.Connect(e, v, C[v]);
                }

                // 
                foreach (var edge in _vertices[v])
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

        IGraph<T> IGraph<T>.FindMinimumSpanningTree()
        {
            return FindMinimumSpanningTree();
        }

        public IReadOnlyList<T> FindPath(T start, T goal, HeuristicCost<T> heuristic)
        {
            return Search.HeuristicSearch(start, goal, GetSuccessors, GetWeight, heuristic);
        }

        public IReadOnlyList<T> FindPath(T start, Func<T, bool> goalCondition, HeuristicCost<T> heuristic)
        {
            return Search.HeuristicSearch(start, goalCondition, GetSuccessors, GetWeight, heuristic);
        }

        public IEnumerable<T> Traverse(T start, TraversalMethod method)
        {
            return method switch
            {
                TraversalMethod.BreadthFirst => Search.BreadthFirst(start, GetSuccessors),
                TraversalMethod.DepthFirst => Search.DepthFirst(start, GetSuccessors),

                _ => throw new ArgumentException($"Invalid traversal method.", nameof(method)),
            };
        }

        #endregion

        #region Edge & Pair

        private class Edge
        {
            public readonly Pair Vertices;
            public float Weight;

            public Edge(Pair vertices, float weight)
            {
                Vertices = vertices;
                Weight = weight;
            }

            public T GetOther(T x)
            {
                return Equals(Vertices.A, x) ? Vertices.B : Vertices.A;
            }
        }

        private readonly struct Pair : IEquatable<Pair>
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
                // Match either ordering
                return (A.Equals(other.A) && B.Equals(other.B))
                    || (A.Equals(other.B) && B.Equals(other.A));
            }

            public override int GetHashCode()
            {
                // HashCode.Combine(A, B); // Failed to equality check...
                return (A.GetHashCode() * 13) ^ B.GetHashCode();
            }

            public static bool operator ==(Pair left, Pair right)
            {
                return left.Equals(right);
            }

            public static bool operator !=(Pair left, Pair right)
            {
                return !(left == right);
            }
        }

        #endregion
    }
}
