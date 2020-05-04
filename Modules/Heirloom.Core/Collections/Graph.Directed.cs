using System;
using System.Collections.Generic;
using System.Linq;

namespace Heirloom.Collections
{
    public sealed class DirectedGraph<T> : IGraph<T>
    {
        private readonly Dictionary<T, List<Edge>> _vertices;
        private readonly Dictionary<OrderedPair, Edge> _edges;

        public DirectedGraph()
        {
            _vertices = new Dictionary<T, List<Edge>>();
            _edges = new Dictionary<OrderedPair, Edge>();
        }

        public bool IsDirected => true;

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
            var vertexPair = new OrderedPair(a, b);
            return _edges.ContainsKey(vertexPair);
        }

        public void Connect(T a, T b, float weight)
        {
            var vertexPair = new OrderedPair(a, b);

            if (_edges.ContainsKey(vertexPair))
            {
                throw new InvalidOperationException($"Edge already exists between '{a}' and '{b}'.");
            }

            // Construct edge
            var edge = new Edge(vertexPair, weight);
            _edges[vertexPair] = edge;

            // Add edge to vertex
            _vertices[a].Add(edge);
        }

        public void Disconnect(T a, T b)
        {
            var vertexPair = new OrderedPair(a, b);

            if (!_edges.ContainsKey(vertexPair))
            {
                throw new InvalidOperationException($"Edge does not exist between '{a}' and '{b}'.");
            }

            // Get edge and remove from edge set
            var edge = _edges[vertexPair];
            _edges.Remove(vertexPair);

            // Remove edge from vertex
            _vertices[a].Remove(edge);
        }

        public IEnumerable<T> GetSuccessors(T v)
        {
            return _vertices[v].Select(e => e.Vertices.B);
        }

        public float GetWeight(T a, T b)
        {
            var vertexPair = new OrderedPair(a, b);

            if (!_edges.ContainsKey(vertexPair))
            {
                throw new InvalidOperationException($"Edge does not exist between '{a}' and '{b}'.");
            }

            var edge = _edges[vertexPair];
            return edge.Weight;
        }

        public void SetWeight(T a, T b, float weight)
        {
            var vertexPair = new OrderedPair(a, b);

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

        IGraph<T> IGraph<T>.FindMinimumSpanningTree()
        {
            throw new InvalidOperationException("Unable to find minimum spanning tree, graph is a directed graph.");
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
            public readonly OrderedPair Vertices;
            public float Weight;

            public Edge(OrderedPair vertices, float weight)
            {
                Vertices = vertices;
                Weight = weight;
            }

            public T GetOther(T x)
            {
                return Equals(Vertices.A, x) ? Vertices.B : Vertices.A;
            }
        }

        private readonly struct OrderedPair : IEquatable<OrderedPair>
        {
            public readonly T A;
            public readonly T B;

            public OrderedPair(T a, T b)
            {
                A = a;
                B = b;
            }

            public override bool Equals(object obj)
            {
                return obj is OrderedPair pair
                    && Equals(pair);
            }

            public bool Equals(OrderedPair other)
            {
                // Match sequential ordering
                return A.Equals(other.A) && B.Equals(other.B);
            }

            public override int GetHashCode()
            {
                // HashCode.Combine(A, B); // Failed to equality check...
                return (A.GetHashCode() * 13) ^ B.GetHashCode();
            }

            public static bool operator ==(OrderedPair left, OrderedPair right)
            {
                return left.Equals(right);
            }

            public static bool operator !=(OrderedPair left, OrderedPair right)
            {
                return !(left == right);
            }
        }

        #endregion

    }
}
