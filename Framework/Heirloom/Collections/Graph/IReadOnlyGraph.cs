using System.Collections.Generic;

namespace Heirloom.Collections
{
    /// <summary>
    /// A read only view of a graph.
    /// </summary>
    /// <typeparam name="V">Some vertex type. Must properly implement equality checks.</typeparam>
    /// <typeparam name="E">Some data type for giving values to edges.</typeparam>
    public interface IReadOnlyGraph<V, E> where E : struct
    {
        /// <summary>
        /// Determines if edges are directed or undirected in this graph.
        /// </summary>
        bool IsDirected { get; }

        /// <summary>
        /// Gets the read-only collection of vertices contained in this graph.
        /// </summary>
        IReadOnlyCollection<V> Vertices { get; }

        /// <summary>
        /// Gets the read-only collection of edges contained in this graph.
        /// </summary>
        IReadOnlyCollection<(V, V)> Edges { get; }

        /// <summary>
        /// Determines if the specified vertex is contained in this graph.
        /// </summary>
        /// <param name="vtx">Some vertex potentially contained in the graph.</param> 
        /// <returns>Will return <see langword="true"/> if the edge exists.</returns>
        bool ContainsVertex(V vtx);

        /// <summary>
        /// Determines if an edge exists between <paramref name="a"/> and <paramref name="b"/>.
        /// </summary>
        /// <param name="a">The first (near) side of the edge.</param>
        /// <param name="b">The second (far) side of the edge.</param>
        /// <returns>Will return <see langword="true"/> if the edge exists.</returns>
        bool ContainsEdge(V a, V b);

        /// <summary>
        /// Gets the data associated with some edge (ex, edge weight).
        /// </summary>
        /// <param name="a">The first (near) side of the edge.</param>
        /// <param name="b">The second (far) side of the edge.</param>
        /// <returns>The data associated with the edge.</returns>
        E GetEdgeProperty(V a, V b);

        /// <summary>
        /// Gets the collection of adjacent vertices for the specified vertex.
        /// This returns all neighbors regardless if the graph is directed or undirected.
        /// </summary>
        IReadOnlyCollection<V> GetNeighbors(V vtx);

        /// <summary>
        /// Gets the collection of predecessors for the specified vertex.
        /// This is equivalent to <see cref="GetNeighbors(V)"/> if the graph is undirected.
        /// </summary>
        IReadOnlyCollection<V> GetPredecessors(V vtx);

        /// <summary>
        /// Gets the collection of successors for the specified vertex.
        /// This is equivalent to <see cref="GetNeighbors(V)"/> if the graph is undirected.
        /// </summary>
        IReadOnlyCollection<V> GetSuccessors(V vtx);

        /// <summary>
        /// Extracts a read-only view of a subgraph of the specified vertices.
        /// </summary>
        IReadOnlyGraph<V, E> CreateSubgraph(IEnumerable<V> vertices);
    }
}
