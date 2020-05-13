using System;
using System.Collections;
using System.Collections.Generic;

namespace Heirloom.Collections
{
    internal abstract class EdgeCollection<P, T> : IEnumerable<Edge<T>>
    {
        private readonly Dictionary<P, Edge<T>> _edges;

        protected EdgeCollection()
        {
            _edges = new Dictionary<P, Edge<T>>();
        }

        public int Count => _edges.Count;

        protected abstract P CreatePair(T a, T b);

        public void Clear()
        {
            _edges.Clear();
        }

        public void AddEdge(Edge<T> edge)
        {
            var vertexPair = CreatePair(edge.A, edge.B);

            if (_edges.ContainsKey(vertexPair))
            {
                throw new InvalidOperationException($"Edge already exists between '{edge.A}' and '{edge.B}'.");
            }

            // Store edge
            _edges[vertexPair] = edge;
        }

        public bool RemoveEdge(Edge<T> edge)
        {
            var vertexPair = CreatePair(edge.A, edge.B);
            return _edges.Remove(vertexPair);
        }

        public bool Contains(T a, T b)
        {
            var vertexPair = CreatePair(a, b);
            return _edges.ContainsKey(vertexPair);
        }

        public bool TryGetEdge(T a, T b, out Edge<T> edge)
        {
            var vertexPair = CreatePair(a, b);
            return _edges.TryGetValue(vertexPair, out edge);
        }

        public Edge<T> GetEdge(T a, T b)
        {
            if (TryGetEdge(a, b, out var edge))
            {
                return edge;
            }

            // Unable to get edge
            throw new InvalidOperationException($"Edge already exists between '{a}' and '{b}'.");
        }

        public IEnumerator<Edge<T>> GetEnumerator()
        {
            foreach (var (_, edge) in _edges)
            {
                yield return edge;
            }
        }

        IEnumerator IEnumerable.GetEnumerator()
        {
            return GetEnumerator();
        }
    }
}
