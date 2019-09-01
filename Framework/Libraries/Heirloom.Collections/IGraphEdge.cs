using System;

namespace Heirloom.Collections
{
    /// <summary>
    /// An edge between two vertices.
    /// </summary>
    /// <typeparam name="TKey">Some name/key to lookup vertices.</typeparam>
    public interface IGraphEdge<TKey>
    {
        /// <summary>
        /// The name/key of the source vertex.
        /// </summary>
        TKey Source { get; }

        /// <summary>
        /// The name/key of the target vertex.
        /// </summary>
        TKey Target { get; }

        /// <summary>
        /// The cost/weight of this edge.
        /// </summary>
        float Weight { get; set; }

        /// <summary>
        /// Returns the other key.
        /// </summary>
        /// <param name="key"> Must be either <see cref="Source"/> or <see cref="Target"/>. </param>
        /// <returns> The opposite key. </returns>
        /// <exception cref="ArgumentException">Exception thrown if key is not either <see cref="Source"/> or <see cref="Target"/>.</exception>
        /// <exception cref="ArgumentNullException">Thrown if argument is null.</exception>
        TKey GetOtherKey(TKey key);
    }
}
