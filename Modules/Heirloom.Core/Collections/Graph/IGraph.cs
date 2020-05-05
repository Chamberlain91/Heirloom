using System;
using System.Collections.Generic;

namespace Heirloom.Collections
{
    /// <summary>
    /// An interface that represents a graph.
    /// </summary>
    public interface IGraph<T> : IEnumerable<T>
    {
        /// <summary>
        /// Gets the vertices in the graph.
        /// </summary>
        IEnumerable<T> Vertices { get; }

        /// <summary>
        /// Gets the number of vertices in the graph.
        /// </summary>
        int VertexCount { get; }

        /// <summary>
        /// Gets the edges in the graph.
        /// </summary>
        IEnumerable<(T A, T B)> Edges { get; }

        /// <summary>
        /// Gets the number of edges in the graph.
        /// </summary>
        int EdgeCount { get; }

        /// <summary>
        /// Clears the graph. Removing all vertices and edges.
        /// </summary>
        void Clear();

        /// <summary>
        /// Inserts a vertex into the graph.
        /// </summary>
        void AddVertex(T v);

        /// <summary>
        /// Removes a vertex from the graph.
        /// </summary>
        bool RemoveVertex(T v);

        /// <summary>
        /// Determines if the graph contains the specified vertex.
        /// </summary>
        bool ContainsVertex(T v);

        /// <summary>
        /// Inserts a new edge into the graph.
        /// </summary>
        void AddEdge(T a, T b, float weight);

        /// <summary>
        /// Removes an edge from the graph.
        /// </summary>
        bool RemoveEdge(T a, T b);

        /// <summary>
        /// Determines of the graph contains the specified edge.
        /// </summary>
        /// <param name="a"></param>
        /// <param name="b"></param>
        /// <returns></returns>
        bool ContainsEdge(T a, T b);

        /// <summary>
        /// Gets the weight of some edge.
        /// </summary>
        float GetEdgeWeight(T a, T b);

        /// <summary>
        /// Sets the weight of some edge.
        /// </summary>
        void SetEdgeWeight(T a, T b, float weight);

        /// <summary>
        /// Gets the successor (outgoing neighbor) vertices.
        /// </summary>
        IEnumerable<T> GetSuccessors(T v);

        /// <summary>
        /// Attempts to finds a path between <paramref name="start"/> and <paramref name="goal"/> vertices using the specified <paramref name="heuristic"/>.
        /// </summary>
        /// <param name="start">Some starting vertex.</param>
        /// <param name="goal">Some goal vertex.</param>
        /// <param name="heuristic">Some heuristic evaluation of the cost to the goal.</param>
        /// <returns>If exists, the path from <paramref name="start"/> to <paramref name="goal"/>, otherwise null.</returns>
        IReadOnlyList<T> FindPath(T start, T goal, HeuristicCost<T> heuristic);

        /// <summary>
        /// Attempts to finds a path between <paramref name="start"/> and <paramref name="goal"/> vertices using the specified <paramref name="heuristic"/>.
        /// </summary>
        /// <param name="start">Some starting vertex.</param>
        /// <param name="goalCondition">Some goal condition.</param>
        /// <param name="heuristic">Some heuristic evaluation of the cost to the goal.</param>
        /// <returns>If exists, the path from <paramref name="start"/> to the vertex that satisfied the <paramref name="goalCondition"/>, otherwise null.</returns>
        IReadOnlyList<T> FindPath(T start, Func<T, bool> goalCondition, HeuristicCost<T> heuristic);

        /// <summary>
        /// Traverses the graph by the specified method.
        /// </summary>
        /// <param name="start">Some starting veretx.</param>
        /// <param name="method">The desired traveral method.</param>
        /// <returns>A traveral of vertices in the graph.</returns>
        IEnumerable<T> Traverse(T start, TraversalMethod method);

        /// <summary>
        /// Finds and returns a minimum spanning tree.
        /// If the graph is directed, this will throw <see cref="NotImplementedException"/>.
        /// </summary>
        IGraph<T> FindMinimumSpanningTree();
    }
}
