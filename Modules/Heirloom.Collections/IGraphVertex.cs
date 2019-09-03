using System.Collections.Generic;

namespace Heirloom.Collections
{
    /// <summary>
    /// A vertex representing a node on a graph.
    /// </summary>
    /// <typeparam name="TKey"></typeparam>
    /// <typeparam name="TValue"></typeparam>
    public interface IGraphVertex<TKey, TValue>
    {
        /// <summary>
        /// The name/key of this vertex.
        /// </summary>
        TKey Key { get; }

        /// <summary>
        /// The data/value of this vertex.
        /// </summary>
        TValue Value { get; set; }

        /// <summary>
        /// The list incoming edges in a directed graph ( no edges when undirected ).
        /// </summary>
        IReadOnlyList<IGraphEdge<TKey>> IncomingEdges { get; }

        /// <summary>
        /// The list of outgoing edges in a directed graph ( all edges when undirected ).
        /// </summary>
        IReadOnlyList<IGraphEdge<TKey>> Edges { get; }
    }
}
