namespace Heirloom.Collections
{
    /// <summary>
    /// A 2D grid of values.
    /// </summary>
    /// <category>Grids</category>
    public interface IGrid<T> : IReadOnlyGrid<T>
    {
        /// <summary>
        /// Clear the grid (ie, set each cell to the default value of the element type).
        /// </summary>
        void Clear();
    }
}
