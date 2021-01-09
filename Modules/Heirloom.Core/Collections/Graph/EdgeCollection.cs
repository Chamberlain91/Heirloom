using System;
using System.Collections;
using System.Collections.Generic;

namespace Heirloom.Collections
{
    internal abstract class EdgeCollection<TPair, TVertex> : IEnumerable<GraphEdge<TVertex>>
    {
        private readonly Dictionary<TPair, GraphEdge<TVertex>> _edges;

        protected EdgeCollection()
        {
            _edges = new Dictionary<TPair, GraphEdge<TVertex>>();
        }

        public int Count => _edges.Count;

        protected abstract TPair CreatePair(TVertex a, TVertex b);

        public void Clear()
        {
            _edges.Clear();
        }

        public void AddEdge(GraphEdge<TVertex> edge)
        {
            var vertexPair = CreatePair(edge.A, edge.B);

            if (_edges.ContainsKey(vertexPair))
            {
                throw new InvalidOperationException($"Edge already exists between '{edge.A}' and '{edge.B}'.");
            }

            // Store edge
            _edges[vertexPair] = edge;
        }

        public bool RemoveEdge(GraphEdge<TVertex> edge)
        {
            var vertexPair = CreatePair(edge.A, edge.B);
            return _edges.Remove(vertexPair);
        }

        public bool Contains(TVertex a, TVertex b)
        {
            var vertexPair = CreatePair(a, b);
            return _edges.ContainsKey(vertexPair);
        }

        public bool TryGetEdge(TVertex a, TVertex b, out GraphEdge<TVertex> edge)
        {
            var vertexPair = CreatePair(a, b);
            return _edges.TryGetValue(vertexPair, out edge);
        }

        public GraphEdge<TVertex> GetEdge(TVertex a, TVertex b)
        {
            if (TryGetEdge(a, b, out var edge))
            {
                return edge;
            }

            // Unable to get edge
            throw new InvalidOperationException($"Edge already exists between '{a}' and '{b}'.");
        }

        public IEnumerator<GraphEdge<TVertex>> GetEnumerator()
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
