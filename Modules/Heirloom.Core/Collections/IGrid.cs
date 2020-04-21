using Heirloom.Math;

namespace Heirloom.Collections
{
    /// <summary>
    /// A read-only view of a 2D grid of values.
    /// </summary>
    public interface IReadOnlyGrid<T>
    {
        /// <summary>
        /// Gets or sets the value at the specified coordinate.
        /// </summary>
        T this[in IntVector co]
        {
            get => this[co.X, co.Y];
            set => this[co.X, co.Y] = value;
        }

        /// <summary>
        /// Gets or sets the value at the specified coordinate.
        /// </summary>
        T this[in int x, in int y] { get; set; }

        /// <summary>
        /// Is the specified coordinate valid on this grid?
        /// </summary>
        bool IsValidCoordinate(in int x, in int y);

        /// <summary>
        /// Is the specified coordinate valid on this grid?
        /// </summary>
        bool IsValidCoordinate(in IntVector co);
    }

    /// <summary>
    /// A 2D grid of values.
    /// </summary>
    public interface IGrid<T> : IReadOnlyGrid<T>
    {
        /// <summary>
        /// Clear the grid (ie, set each cell to the default value of the element type).
        /// </summary>
        void Clear();
    }
}
