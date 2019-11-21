namespace Heirloom.Collections.Spatial
{
    /// <summary>
    /// A finite grid (bounded by <see cref="Width"/> and <see cref="Height"/>).
    /// </summary>
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
    }
}
