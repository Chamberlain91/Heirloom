using Meadows.Mathematics;

namespace Meadows.Collections
{
    /// <summary>
    /// A sparse 2D grid of values.
    /// </summary>
    /// <category>Grids</category>
    public interface ISparseGrid<T> : IReadOnlySparseGrid<T>, IGrid<T>
    {
        /// <summary>
        /// Clears the assigned valueon this cell of the sparse grid.
        /// </summary>
        void Remove(in int x, in int y);

        /// <summary>
        /// Clears the assigned valueon this cell of the sparse grid.
        /// </summary>
        void Remove(in IntVector co);
    }
}
