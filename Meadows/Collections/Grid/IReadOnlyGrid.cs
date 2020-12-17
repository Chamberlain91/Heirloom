using Meadows.Mathematics;

namespace Meadows.Collections
{
    /// <summary>
    /// A read-only view of a 2D grid of values.
    /// </summary>
    /// <category>Grids</category>
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
}
