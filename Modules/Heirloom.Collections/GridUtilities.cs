using System.Collections.Generic;

namespace Heirloom.Collections
{
    /// <summary>
    /// Provides extra utilities for interacting with a grid.
    /// </summary>
    public static class GridUtilities
    {

        /// <summary>
        /// Gets neighboring grid coordinates relative to the specified input coordinates.
        /// </summary>
        public static IEnumerable<(int X, int Y)> GetNeighboringCoordinates((int X, int Y) co, GridNeighborType neighborType)
        {
            return GetNeighboringCoordinates(co.X, co.Y, neighborType);
        }

        /// <summary>
        /// Gets neighboring grid coordinates relative to the specified input coordinates.
        /// </summary>
        public static IEnumerable<(int X, int Y)> GetNeighboringCoordinates(int x, int y, GridNeighborType neighborType)
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
