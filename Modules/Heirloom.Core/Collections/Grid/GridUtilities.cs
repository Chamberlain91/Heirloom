using System.Collections.Generic;

namespace Heirloom.Collections
{
    /// <summary>
    /// Provides extra utilities for interacting with a grid.
    /// </summary>
    /// <category>Grids</category>
    public static class GridUtilities
    {
        #region GetNeighbors Extension Methods

        /// <summary>
        /// Gets the specified cell's neighbors.
        /// </summary>
        public static IEnumerable<T> GetNeighbors<T>(this IGrid<T> grid, IntVector co, GridNeighborType neighborType = GridNeighborType.Axis)
        {
            return GetNeighbors(grid, co.X, co.Y, neighborType);
        }

        /// <summary>
        /// Gets the specified cell's neighbors.
        /// </summary>
        public static IEnumerable<T> GetNeighbors<T>(this IGrid<T> grid, int x, int y, GridNeighborType neighborType = GridNeighborType.Axis)
        {
            foreach (var neighbor in GetNeighborCoordinates(grid, x, y, neighborType))
            {
                yield return grid[in neighbor];
            }
        }

        /// <summary>
        /// Gets the specified cell's neighbor coordinates.
        /// </summary>
        public static IEnumerable<IntVector> GetNeighborCoordinates<T>(this IGrid<T> grid, int x, int y, GridNeighborType neighborType = GridNeighborType.Axis)
        {
            foreach (var neighbor in GetNeighborCoordinates(x, y, neighborType))
            {
                if (grid.IsValidCoordinate(neighbor))
                {
                    yield return neighbor;
                }
            }
        }

        /// <summary>
        /// Gets the specified cell's neighbor coordinates.
        /// </summary>
        public static IEnumerable<IntVector> GetNeighborCoordinates<T>(this IGrid<T> grid, IntVector co, GridNeighborType neighborType = GridNeighborType.Axis)
        {
            return GetNeighborCoordinates(grid, co.X, co.Y, neighborType);
        }

        #endregion

        /// <summary>
        /// Gets neighboring grid coordinates relative to the specified input coordinates.
        /// </summary>
        public static IEnumerable<IntVector> GetNeighborCoordinates(IntVector co, GridNeighborType neighborType)
        {
            return GetNeighborCoordinates(co.X, co.Y, neighborType);
        }

        /// <summary>
        /// Gets neighboring grid coordinates relative to the specified input coordinates.
        /// </summary>
        public static IEnumerable<IntVector> GetNeighborCoordinates(int x, int y, GridNeighborType neighborType)
        {
            if (neighborType == GridNeighborType.Axis)
            {
                // Clockwise from three o'clock
                yield return (x + 1, y + 0);
                yield return (x + 0, y + 1);
                yield return (x - 1, y + 0);
                yield return (x + 0, y - 1);
            }
            else
            if (neighborType == GridNeighborType.Diagonal)
            {
                // Clockwise from half past one
                yield return (x + 1, y - 1);
                yield return (x + 1, y + 1);
                yield return (x - 1, y + 1);
                yield return (x - 1, y - 1);
            }
            else
            {
                // Clockwise from three o'clock
                yield return (x + 1, y + 0);
                yield return (x + 1, y + 1);
                yield return (x + 0, y + 1);
                yield return (x - 1, y + 1);
                yield return (x - 1, y + 0);
                yield return (x - 1, y - 1);
                yield return (x + 0, y - 1);
                yield return (x + 1, y - 1);
            }
        }
    }
}
