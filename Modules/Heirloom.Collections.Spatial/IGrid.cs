using System;
using System.Collections.Generic;

using Heirloom.Math;

namespace Heirloom.Collections.Spatial
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
        T this[in IntVector co] { get; set; }

        /// <summary>
        /// Gets or sets the value at the specified coordinate.
        /// </summary>
        T this[in int x, in int y] { get; set; }

        /// <summary>
        /// Is the specified coordinate valid on this grid?
        /// </summary>
        bool IsValidCoordinate(in IntVector co);

        /// <summary>
        /// Enumerates the neighboring coordinates.
        /// </summary>
        IEnumerable<IntVector> GetNeighborCoordinates(IntVector co, GridNeighborType neighborType = GridNeighborType.Axis)
        {
            return GridUtilities.EnumerateNeighbors(co, neighborType);
        }

        /// <summary>
        /// Enumerates the neighboring coordinates that satisfy a condition.
        /// </summary>
        IEnumerable<IntVector> GetNeighborCoordinates(IntVector co, Predicate<T> predicate, GridNeighborType neighborType = GridNeighborType.Axis)
        {
            foreach (var neighbor in GridUtilities.EnumerateNeighbors(co, neighborType))
            {
                // Within bound and matches the selection test
                if (IsValidCoordinate(neighbor) && predicate(this[neighbor]))
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
