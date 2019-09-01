using System;
using System.Collections.Generic;

using Heirloom.Math;

namespace Heirloom.Collections.Spatial
{
    public sealed class Grid<T> : IFiniteGrid<T>
    {
        private readonly T[,] _data;

        public Grid(int width, int height)
        {
            Width = width;
            Height = height;

            // todo: reorder access as new T[Height, Width] because defining array is  y first {{..},{..},..}
            _data = new T[Width, Height];
        }

        #region Properties

        public int Width { get; }

        public int Height { get; }

        #endregion

        #region Indexer

        public T this[int x, int y]
        {
            get => this[(x, y)];
            set => this[(x, y)] = value;
        }

        public T this[IntVector co]
        {
            get
            {
                if (co.X < 0 || co.X >= Width) { return default; }
                if (co.Y < 0 || co.Y >= Height) { return default; }
                return _data[co.X, co.Y];
            }

            set => _data[co.X, co.Y] = value;
        }

        #endregion

        /// <summary>
        /// Sets all values in the grid to default for type <typeparamref name="T"/>.
        /// </summary>
        public void Clear()
        {
            // Does this actually clear all dimensions?
            Array.Clear(_data, 0, _data.Length);
        }

        /// <summary>
        /// Returns the valid neighboring coordinates for the specified coordinate.
        /// </summary>
        public IEnumerable<IntVector> FindNeighbors(IntVector co, GridNeighbors neighbors = GridNeighbors.FourAxis)
        {
            return FindNeighbors(co, _ => true, neighbors);
        }

        /// <summary>
        /// Returns the valid neighboring coordinates for the specified coordinate that also match the given predicate.
        /// </summary>
        public IEnumerable<IntVector> FindNeighbors(IntVector co, Predicate<T> predicate, GridNeighbors neighbors = GridNeighbors.FourAxis)
        {
            foreach (var neighbor in GridUtilities.EnumerateNeighbors(co, neighbors))
            {
                // Within bound and matches the selection test
                if (IsValidCoordinate(neighbor) && predicate(this[neighbor]))
                {
                    yield return neighbor;
                }
            }
        }

        /// <summary>
        /// Determines if the specified coordinate is a valid coordinate within the grid.
        /// </summary>
        public bool IsValidCoordinate(in IntVector co)
        {
            if (co.X < 0 || co.X >= Width) { return false; }
            if (co.Y < 0 || co.Y >= Height) { return false; }
            return true;
        }
    }
}
