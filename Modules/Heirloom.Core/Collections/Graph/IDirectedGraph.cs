using System;
using System.Collections.Generic;

namespace Heirloom.Collections
{
    public interface IDirectedGraph<T> : IEnumerable<T>
    {
        IEnumerable<(T A, T B)> Arcs { get; }
        int ArcCount { get; }

        IEnumerable<T> Vertices { get; }
        int VertexCount { get; }

        void Clear();

        void AddVertex(T v);
        bool ContainsVertex(T v);
        bool RemoveVertex(T v);

        void AddArc(T a, T b, float weight);
        bool RemoveArc(T a, T b);
        bool ContainsArc(T a, T b);

        float GetArcWeight(T a, T b);
        void SetArcWeight(T a, T b, float weight);

        IEnumerable<T> GetSuccessors(T v);
        IEnumerable<T> GetPredecessors(T v);

        IReadOnlyList<T> FindPath(T start, T goal, HeuristicCost<T> heuristic);
        IReadOnlyList<T> FindPath(T start, Func<T, bool> goalCondition, HeuristicCost<T> heuristic);

        IEnumerable<T> Traverse(T start, TraversalMethod method);
    }
}
