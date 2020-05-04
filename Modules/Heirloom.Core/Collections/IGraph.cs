using System;
using System.Collections.Generic;

namespace Heirloom.Collections
{
    public interface IGraph<T>
    {
        bool IsDirected { get; }

        IEnumerable<T> Vertices { get; }
        int VertexCount { get; }

        IEnumerable<(T A, T B)> Edges { get; }
        int EdgeCount { get; }

        void Clear();

        void Add(T v);
        bool Remove(T v);

        bool IsConnected(T a, T b);
        void Connect(T a, T b, float weight);
        void Disconnect(T a, T b);

        bool Contains(T v);

        IEnumerable<T> GetSuccessors(T v);

        float GetWeight(T a, T b);
        void SetWeight(T a, T b, float weight);

        IReadOnlyList<T> FindPath(T start, T goal, HeuristicCost<T> heuristic);
        IReadOnlyList<T> FindPath(T start, Func<T, bool> goal, HeuristicCost<T> heuristic);

        IEnumerable<T> Traverse(T start, TraversalMethod method);

        IGraph<T> FindMinimumSpanningTree();
    }
}
