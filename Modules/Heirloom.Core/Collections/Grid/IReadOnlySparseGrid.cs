using System.Collections.Generic;

namespace Heirloom.Collections
{
    /// <summary>
    /// A sparse 2D grid of values.
    /// </summary>
    /// <category>Grids</category>
    public interface IReadOnlySparseGrid<T> : IReadOnlyGrid<T>
    {
        /// <summary>
        /// Gets a collection containing the keys of the sparse grid.
        /// </summary>
        IEnumerable<IntVector> Keys { get; }

        /// <summary>
        /// Determines if a value has been set on this cell of the sparse grid.
        /// </summary>
        bool HasValue(in int x, in int y);

        /// <summary>
        /// Determines if a value has been set on this cell of the sparse grid.
        /// </summary>
        bool HasValue(in IntVector co);
    }
}
