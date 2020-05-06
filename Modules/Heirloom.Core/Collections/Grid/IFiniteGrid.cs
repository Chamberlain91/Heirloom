namespace Heirloom.Collections
{
    /// <summary>
    /// A finite grid (bounded by <see cref="Width"/> and <see cref="Height"/>).
    /// </summary>
    /// <category>Grids</category>
    public interface IFiniteGrid<T> : IGrid<T>
    {
        /// <summary>
        /// The width of this grid.
        /// </summary>
        int Width { get; }

        /// <summary>
        /// The height of this grid.
        /// </summary>
        int Height { get; }

        /// <summary>
        /// Clear the entire grid to some value.
        /// </summary>
        void Clear(T val);
    }
}
