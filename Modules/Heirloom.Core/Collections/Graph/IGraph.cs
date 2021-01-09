using System;
using System.Collections.Generic;

namespace Heirloom.Collections
{
    /// <summary>
    /// An interface that represents a graph.
    /// </summary>
    /// <tags>Graph, Undirected</tags>
    /// <category>Graph</category>
    public interface IGraph<TVertex>
    {
        /// <summary>
        /// Gets the vertices in the graph.
        /// </summary>
        IEnumerable<TVertex> Vertices { get; }

        /// <summary>
        /// Gets the number of vertices in the graph.
        /// </summary>
        int VertexCount { get; }

        /// <summary>
        /// Gets the edges in the graph.
        /// </summary>
        IEnumerable<(TVertex A, TVertex B)> Edges { get; }

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
        /// <exception cref="ArgumentException">Thrown when <paramref name="v"/> already exists.</exception>
        void AddVertex(TVertex v, float weight = 1F);

        /// <summary>
        /// Removes a vertex from the graph.
        /// </summary>
        /// <returns>True, if the item was successfully removed.</returns>
        bool RemoveVertex(TVertex v);

        /// <summary>
        /// Determines if the graph contains the specified vertex.
        /// </summary>
        /// <returns>True, if the item was contained.</returns>
        bool ContainsVertex(TVertex v);

        /// <summary>
        /// Gets the weight of some vertex.
        /// </summary>
        /// <exception cref="ArgumentException">Thrown when the vertex <paramref name="v"/> does not exists.</exception>
        float GetVertexWeight(TVertex v);

        /// <summary>
        /// Sets the weight of some vertex.
        /// </summary>
        /// <exception cref="ArgumentException">Thrown when the vertex <paramref name="v"/> does not exists.</exception>
        void SetVertexWeight(TVertex a, float weight);

        /// <summary>
        /// Inserts a new edge into the graph.
        /// </summary>
        /// <exception cref="ArgumentException">Thrown when either <paramref name="a"/> or <paramref name="b"/> does not exists.</exception>
        /// <exception cref="InvalidOperationException">Thrown when <paramref name="a"/> is the same as <paramref name="b"/>.</exception>
        void AddEdge(TVertex a, TVertex b, float weight = 1F);

        /// <summary>
        /// Removes an edge from the graph.
        /// </summary>
        /// <exception cref="ArgumentException">Thrown when either <paramref name="a"/> or <paramref name="b"/> does not exists.</exception>
        /// <returns>True, if the item was successfully removed.</returns>
        bool RemoveEdge(TVertex a, TVertex b);

        /// <summary>
        /// Determines if the graph contains the specified edge.
        /// </summary>
        /// <returns>True, if the item was contained.</returns>
        bool ContainsEdge(TVertex a, TVertex b);

        /// <summary>
        /// Gets the weight of some edge.
        /// </summary>
        /// <exception cref="ArgumentException">Thrown when the edge (<paramref name="a"/>, <paramref name="b"/>) does not exists.</exception>
        float GetEdgeWeight(TVertex a, TVertex b);

        /// <summary>
        /// Sets the weight of some edge.
        /// </summary>
        /// <exception cref="ArgumentException">Thrown when the edge (<paramref name="a"/>, <paramref name="b"/>) does not exists.</exception>
        void SetEdgeWeight(TVertex a, TVertex b, float weight);

        /// <summary>
        /// Gets the neighboring vertices.
        /// </summary>
        /// <exception cref="ArgumentException">Thrown when the vertex <paramref name="v"/> does not exists.</exception>
        IEnumerable<TVertex> GetNeighbors(TVertex v);

        /// <summary>
        /// Gets the number of neighboring vertices.
        /// </summary>
        /// <exception cref="ArgumentException">Thrown when the vertex <paramref name="v"/> does not exists.</exception>
        int GetDegree(TVertex v);

        /// <summary>
        /// Attempts to finds a path between <paramref name="start"/> and <paramref name="goal"/> vertices using the specified <paramref name="heuristic"/>.
        /// </summary>
        /// <param name="start">Some starting vertex.</param>
        /// <param name="goal">Some goal vertex.</param>
        /// <param name="heuristic">Some heuristic evaluation of the cost to the goal.</param>
        /// <returns>If exists, the path from <paramref name="start"/> to <paramref name="goal"/>, otherwise null.</returns>
        /// <exception cref="ArgumentException">Thrown when <paramref name="start"/> vertex does not exist.</exception>
        /// <exception cref="ArgumentException">Thrown when <paramref name="goal"/> vertex does not exist.</exception>
        /// <exception cref="ArgumentNullException">Thrown when <paramref name="heuristic"/> is null.</exception>
        IReadOnlyList<TVertex> FindPath(TVertex start, TVertex goal, HeuristicCost<TVertex> heuristic);

        /// <summary>
        /// Attempts to finds a path between <paramref name="start"/> until the first vertex to satisfy the <paramref name="goalCondition"/> using the specified <paramref name="heuristic"/>.
        /// </summary>
        /// <param name="start">Some starting vertex.</param>
        /// <param name="goalCondition">Some goal condition.</param>
        /// <param name="heuristic">Some heuristic evaluation of the cost to the goal.</param>
        /// <returns>If exists, the path from <paramref name="start"/> to the vertex that satisfied the <paramref name="goalCondition"/>, otherwise null.</returns>
        /// <exception cref="ArgumentException">Thrown when <paramref name="start"/> vertex does not exist.</exception>
        /// <exception cref="ArgumentNullException">Thrown when <paramref name="goalCondition"/> is null.</exception>
        /// <exception cref="ArgumentNullException">Thrown when <paramref name="heuristic"/> is null.</exception>
        IReadOnlyList<TVertex> FindPath(TVertex start, Func<TVertex, bool> goalCondition, HeuristicCost<TVertex> heuristic);

        /// <summary>
        /// Traverses the graph by the specified method.
        /// </summary>
        /// <param name="start">Some starting veretx.</param>
        /// <param name="method">The desired traveral method.</param>
        /// <returns>A traveral of vertices in the graph.</returns>
        /// <exception cref="ArgumentException">Thrown when the vertex <paramref name="start"/> does not exists.</exception>
        IEnumerable<TVertex> Traverse(TVertex start, TraversalMethod method);

        /// <summary>
        /// Finds and returns a minimum spanning tree.
        /// </summary>
        IGraph<TVertex> FindMinimumSpanningTree();
    }
}
