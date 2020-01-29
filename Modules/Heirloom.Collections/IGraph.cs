using System.Collections.Generic;

namespace Heirloom.Collections
{
    public interface IGraph<TKey, TValue, TGraph> where TGraph : IGraph<TKey, TValue, TGraph>
    {
        /// <summary>
        /// Is this graph undirected?
        /// </summary>
        bool IsUndirected { get; }

        /// <summary>
        /// Are self looping edges allowed?
        /// </summary>
        bool AllowSelfLoops { get; }

        /// <summary>
        /// Are edges allowed to have a negative weight?
        /// </summary>
        bool AllowNegativeWeight { get; }

        /// <summary>
        /// The number of vertices within this graph.
        /// </summary>
        int VertexCount { get; }

        /// <summary>
        /// The number of edges within this graph.
        /// </summary>
        int EdgeCount { get; }

        /// <summary>
        /// The edges contained within this graph.
        /// </summary>
        IEnumerable<IGraphEdge<TKey>> Edges { get; }

        /// <summary>
        /// The vertices contained within this graph.
        /// </summary>
        IEnumerable<IGraphVertex<TKey, TValue>> Vertices { get; }

        /// <summary>
        /// The data/values contained by the vertices in this graph.
        /// </summary>
        IEnumerable<TValue> Values { get; }

        /// <summary>
        /// The names/keys used to lookup the vertices in this graph.
        /// </summary>
        IEnumerable<TKey> Keys { get; }

        /// <summary>
        /// Removes all vertices and edges from the graph.
        /// </summary>
        void Clear();

        /// <summary>
        /// Disconnects all edges from all vertices.
        /// </summary>
        void ClearEdges();

        /// <summary>
        /// Determine if a vertex with the given name/key exists within the graph.
        /// </summary>
        /// <param name="key">The name/key of the vertex.</param>
        /// <returns>True, if the vertex exists.</returns>
        bool ContainsVertex(TKey key);

        /// <summary>
        /// Determine if an edge exists between two existing vertices.
        /// </summary>
        /// <param name="keyA">The name/key of the start vertex.</param>
        /// <param name="keyB">The name/key of the end vertex.</param>
        /// <returns>True, if the vertex exists.</returns>
        bool ContainsEdge(TKey keyA, TKey keyB);

        /// <summary>
        /// Determines if the graph contains the value.
        /// </summary>
        /// <param name="value">Some data/value contained by a vertex in the graph.</param>
        /// <returns>True, if the vertex containing this value exists.</returns>
        bool ContainsValue(TValue value);

        /// <summary>
        /// Adds a vertex to the graph.
        /// </summary>
        /// <param name="key">The name/key of a vertex.</param>
        /// <param name="value">The data/value of the graph.</param>
        /// <returns>True, if the name/key was unique and the vertex was added.</returns>
        bool AddVertex(TKey key, TValue value);

        /// <summary>
        /// Removes a vertex from the graph.
        /// </summary>
        /// <param name="key">The name/key of a vertex.</param>
        /// <returns>True, if the vertex existed and was removed.</returns>
        bool RemoveVertex(TKey key);

        /// <summary>
        /// Returns the vertex with the given name/key.
        /// </summary>
        /// <param name="key">The name/key of a vertex.</param>
        /// <returns>A representation of the vertex with the given name.</returns>
        IGraphVertex<TKey, TValue> GetVertex(TKey key);

        /// <summary>
        /// Connects two vertices by an edge in the graph.
        /// </summary>
        /// <param name="keyA">The name/key of the start vertex.</param>
        /// <param name="keyB">The name/key of the end vertex.</param>
        /// <param name="weight">The cost/weight of the edge.</param>
        /// <returns>True, if the edge complies with <see cref="AllowParallelEdges"/> and vertices existed.</returns>
        bool AddEdge(TKey keyA, TKey keyB, float weight);

        /// <summary>
        /// Removes an edge between two vertices in the graph.
        /// </summary>
        /// <param name="keyA">The name/key of the start vertex.</param>
        /// <param name="keyB">The name/key of the end vertex.</param>
        /// <returns>True, if the edge existed and was removed. This may return true many times if <see cref="AllowParallelEdges"/> was set.</returns>
        bool RemoveEdge(TKey keyA, TKey keyB);

        /// <summary>
        /// Returns the edge between two vertices.
        /// </summary>
        /// <param name="keyA">The name/key of the start vertex.</param>
        /// <param name="keyB">The name/key of the end vertex.</param>
        /// <returns>A representation of the edge between the vertices.</returns>
        IGraphEdge<TKey> GetEdge(TKey keyA, TKey keyB);
    }
}
