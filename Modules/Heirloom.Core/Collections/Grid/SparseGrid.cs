using System.Collections.Generic;

namespace Heirloom.Collections
{
    /// <summary>
    /// An infinite, sparse grid of values.
    /// </summary>
    /// <category>Grids</category>
    public sealed class SparseGrid<T> : ISparseGrid<T>
    {
        private readonly Dictionary<IntVector, T> _data;

        #region Constructors

        /// <summary>
        /// Construcs a new instance of <see cref="SparseGrid{T}"/>.
        /// </summary>
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

        /// <inheritdocs/>
        public IEnumerable<IntVector> Keys => _data.Keys;

        /// <summary>
        /// Removes all values in the grid, marking everything as unoccupied.
        /// </summary>
        public void Clear()
        {
            _data.Clear();
        }

        /// <summary>
        /// Clears the assigned valueon this cell of the sparse grid.
        /// </summary>
        public void Remove(in int x, in int y)
        {
            Remove(new IntVector(x, y));
        }

        /// <summary>
        /// Clears the assigned valueon this cell of the sparse grid.
        /// </summary>
        public void Remove(in IntVector co)
        {
            _data.Remove(co);
        }

        /// <summary>
        /// Determines if a value has been set on this cell of the sparse grid.
        /// </summary>
        public bool HasValue(in int x, in int y)
        {
            return HasValue(new IntVector(x, y));
        }

        /// <summary>
        /// Determines if a value has been set on this cell of the sparse grid.
        /// </summary>
        public bool HasValue(in IntVector co)
        {
            return _data.ContainsKey(co);
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
