using System.Collections.Generic;

using Heirloom.Math;

namespace Heirloom.Collections.Spatial
{
    /// <summary>
    /// An infinite, sparse grid of values.
    /// </summary>
    public sealed class SparseGrid<T> : IGrid<T>
    {
        private readonly Dictionary<IntVector, T> _data;

        #region Constructors

        public SparseGrid()
        {
            _data = new Dictionary<IntVector, T>();
        }

        #endregion

        #region Indexer

        /// <summary>
        /// Gets or sets the value at the specified coordinate.
        /// </summary>
        public T this[in IntVector co]
        {
            get => _data.TryGetValue(co, out var value) ? value : default;
            set
            {
                // If setting the default value remove the data from the cell
                if (Equals(value, default)) { _data.Remove(co); }
                else { _data[co] = value; }
            }
        }

        /// <summary>
        /// Gets or sets the value at the specified coordinate.
        /// </summary>
        public T this[in int x, in int y]
        {
            get => this[new IntVector(x, y)];
            set => this[new IntVector(x, y)] = value;
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
        /// Is the specified coordinate valid on this grid?
        /// </summary>
        public bool IsValidCoordinate(in int x, in int y)
        {
            // All cells are valid in the sparse grid
            return true;
        }

        /// <summary>
        /// Is the specified coordinate valid on this grid?
        /// </summary>
        public bool IsValidCoordinate(in IntVector co)
        {
            // All cells are valid in the sparse grid
            return true;
        }
    }
}
