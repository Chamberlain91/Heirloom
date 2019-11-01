using System.Collections.Generic;

namespace Heirloom.Collections
{
    /// <summary>
    /// A read-only view of a 2D grid of values.
    /// </summary>
    public interface IReadOnlyGrid<T>
    // todo: search / path finding
    {
        /// <summary>
        /// Gets or sets the value at the specified coordinate.
        /// </summary>
        T this[in (int X, int Y) co]
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
        bool IsValidCoordinate(in (int X, int Y) co)
        {
            return IsValidCoordinate(co.X, co.Y);
        }

        /// <summary>
        /// Is the specified coordinate valid on this grid?
        /// </summary>
        bool IsValidCoordinate(in int x, in int y);

        /// <summary>
        /// Enumerates the neighboring valid coordinates.
        /// </summary>
        /// <seealso cref="GridUtilities.GetNeighboringCoordinates(IntVector, GridNeighborType)"/>
        IEnumerable<(int x, int y)> GetNeighborCoordinates((int x, int y) co, GridNeighborType neighborType = GridNeighborType.Axis)
        {
            foreach (var neighbor in GridUtilities.GetNeighboringCoordinates(co, neighborType))
            {
                if (IsValidCoordinate(neighbor))
                {
                    yield return neighbor;
                }
            }
        }
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
