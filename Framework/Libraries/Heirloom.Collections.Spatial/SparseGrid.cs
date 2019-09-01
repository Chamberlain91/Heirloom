using System;
using System.Collections.Generic;

using Heirloom.Math;

namespace Heirloom.Collections.Spatial
{
    /// <summary>
    /// Represents an infinite sparse grid.
    /// </summary>
    public sealed class SparseGrid<T> : ISparseGrid<T>
    {
        private readonly Dictionary<IntVector, T> _data;

        public SparseGrid()
        {
            _data = new Dictionary<IntVector, T>();
        }

        #region Indexer

        public T this[IntVector co]
        {
            get => TryGetValue(co, out var value) ? value : default;
            set => _data[co] = value;
        }

        public T this[int x, int y]
        {
            get => this[(x, y)];
            set => this[(x, y)] = value;
        }

        #endregion

        /// <summary>
        /// Removes all values in the grid, marking everything as unoccupied.
        /// </summary>
        public void Clear()
        {
            _data.Clear();
        }

        /// <summary>
        /// Removes the value at the given coordinate, marking it as unoccupied.
        /// </summary>
        public bool Remove(IntVector co)
        {
            return _data.Remove(co);
        }

        /// <summary>
        /// Checks if the given coordinate is occupied.
        /// </summary>
        public bool ContainsAt(IntVector co)
        {
            return _data.ContainsKey(co);
        }

        /// <summary>
        /// This will try to get the value at the specified coordinate.
        /// If an item was set, this will return true and output the value.
        /// Otherwise false.
        /// </summary>
        public bool TryGetValue(IntVector co, out T value)
        {
            return _data.TryGetValue(co, out value);
        }

        /// <summary>
        /// Enumerates the neighboring coordinates.
        /// </summary>
        public IEnumerable<IntVector> FindNeighbors(IntVector co, GridNeighbors neighbors = GridNeighbors.FourAxis)
        {
            return GridUtilities.EnumerateNeighbors(co, neighbors);
        }

        /// <summary>
        /// Enumerates the neighboring coordinates that satisfy a condition.
        /// </summary>
        public IEnumerable<IntVector> FindNeighbors(IntVector co, Predicate<T> predicate, GridNeighbors neighbors = GridNeighbors.FourAxis)
        {
            foreach (var neighbor in GridUtilities.EnumerateNeighbors(co, neighbors))
            {
                // Within bound and matches the selection test
                if (predicate(this[neighbor]))
                {
                    yield return neighbor;
                }
            }
        }
    }
}
