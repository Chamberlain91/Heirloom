using System;

namespace Heirloom.Collections
{
    /// <summary>
    /// A finite grid (bounded by size) of values.
    /// </summary>
    /// <category>Grids</category>
    public sealed class Grid<T> : IFiniteGrid<T>
    {
        private readonly T[] _data;

        #region Constructors

        /// <summary>
        /// Construcs a new instance of <see cref="Grid{T}"/> with the specified dimensions.
        /// </summary>
        /// <param name="width">The width of the grid.</param>
        /// <param name="height">The height of the grid.</param>
        public Grid(int width, int height)
        {
            if (width <= 0) { throw new ArgumentException($"Must be greater than zero.", nameof(width)); }
            if (height <= 0) { throw new ArgumentException($"Must be greater than zero.", nameof(height)); }

            Width = width;
            Height = height;

            _data = new T[Width * Height];
        }

        #endregion

        #region Properties

        /// <summary>
        /// The width of this grid.
        /// </summary>
        public int Width { get; }

        /// <summary>
        /// The height of this grid.
        /// </summary>
        public int Height { get; }

        #endregion

        #region Indexer

        /// <summary>
        /// Gets or sets the value at the specified coordinate.
        /// </summary>
        public T this[in int x, in int y]
        {
            get
            {
                if (x < 0 || x >= Width) { return default; }
                if (y < 0 || y >= Height) { return default; }

                return _data[x + (y * Width)];
            }

            set => _data[x + (y * Width)] = value;
        }

        /// <summary>
        /// Gets or sets the value at the specified coordinate.
        /// </summary>
        public T this[in IntVector co]
        {
            get => this[in co.X, in co.Y];
            set => this[in co.X, in co.Y] = value;
        }

        #endregion

        /// <summary>
        /// Sets all values in the grid to default for type <typeparamref name="T"/>.
        /// </summary>
        public void Clear()
        {
            Clear(default);
        }

        /// <summary>
        /// Sets all values in the grid to some value of <typeparamref name="T"/>.
        /// </summary>
        public void Clear(T val)
        {
            Array.Fill(_data, val);
        }

        /// <summary>
        /// Determines if the specified coordinate is a valid coordinate within the grid.
        /// </summary>
        public bool IsValidCoordinate(in int x, in int y)
        {
            if (x < 0 || x >= Width) { return false; }
            if (y < 0 || y >= Height) { return false; }

            return true;
        }

        /// <summary>
        /// Determines if the specified coordinate is a valid coordinate within the grid.
        /// </summary>
        public bool IsValidCoordinate(in IntVector co)
        {
            return IsValidCoordinate(in co.X, in co.Y);
        }
    }
}
