using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;

using Meadows.Drawing;
using Meadows.Mathematics;

using static Meadows.Mathematics.Calc;
using static Meadows.Mathematics.Vector;

namespace Meadows.Collections
{
    /// <summary>
    /// Implements <see cref="ISpatialCollection{T}"/> using a <see cref="SparseGrid{T}"/>.
    /// </summary>
    /// <category>Spatial Collections</category>
    public sealed class SparseGridSpatialCollection<T> : ISpatialCollection<T>
    {
        private readonly SparseGrid<HashSet<T>> _grid = new SparseGrid<HashSet<T>>();
        private readonly Dictionary<T, IntRectangle> _itemRects = new Dictionary<T, IntRectangle>();
        private readonly HashSet<T> _items = new HashSet<T>();

        private readonly int _cellSize;

        /// <summary>
        /// Constructs new instance of <see cref="SparseGridSpatialCollection{T}"/>.
        /// </summary>
        /// <param name="cellSize">The size of a square grid cell.</param>
        public SparseGridSpatialCollection(int cellSize)
        {
            _cellSize = cellSize;
        }

        /// <inheritdoc/>
        public int Count => _itemRects.Count;

        /// <inheritdoc/>
        public void Clear()
        {
            _items.Clear();
            _itemRects.Clear();
            _grid.Clear();
        }

        /// <inheritdoc/>
        public void Add(in T item, in IShape boundingShape)
        {
            if (_items.Add(item))
            {
                var rect = GetRect(boundingShape);
                PopulateItems(item, rect);
            }
            else
            {
                throw new InvalidOperationException("Item already exists.");
            }
        }

        /// <inheritdoc/>
        public void Update(in T item, in IShape boundingShape)
        {
            Erase(item); // remove item from cells and populate new cells
            PopulateItems(item, GetRect(boundingShape.Bounds));
        }

        /// <inheritdoc/>
        public bool Remove(in T item)
        {
            if (_items.Remove(item))
            {
                Erase(item);
                return true;
            }
            else
            {
                // Item did not exist
                return false;
            }
        }

        private void Erase(T item)
        {
            var rect = _itemRects[item];

            // Remove from item set
            _itemRects.Remove(item);

            // Remove from cells
            foreach (var co in Rasterizer.Rectangle(rect))
            {
                _grid[co].Remove(item);
                if (_grid[co].Count == 0) { _grid.Remove(co); }
            }
        }

        private void PopulateItems(T item, IntRectangle rect)
        {
            // Store cell rect
            _itemRects[item] = rect;

            // Populate cell rect
            foreach (var co in Rasterizer.Rectangle(rect))
            {
                if (_grid.HasValue(co) == false) { _grid[co] = new HashSet<T>(); }
                _grid[co].Add(item);
            }
        }

        /// <inheritdoc/>
        public bool Contains(in T item)
        {
            return _items.Contains(item);
        }

        /// <inheritdoc/>
        public IEnumerable<T> Query(Vector point)
        {
            return Iterate().Distinct();

            IEnumerable<T> Iterate()
            {
                var co = GetCoord(point);

                if (_grid.HasValue(co))
                {
                    foreach (var item in _grid[co])
                    {
                        yield return item;
                    }
                }
            }
        }

        /// <inheritdoc/>
        public IEnumerable<T> Query(IShape queryShape)
        {
            return Iterate().Distinct();

            IEnumerable<T> Iterate()
            {
                var rect = GetRect(queryShape.Bounds);
                foreach (var co in Rasterizer.Rectangle(rect))
                {
                    if (_grid.HasValue(co))
                    {
                        foreach (var item in _grid[co])
                        {
                            yield return item;
                        }
                    }
                }
            }
        }

        /// <inheritdoc/>
        public IEnumerable<T> Query(Ray ray, float maxDistance)
        {
            if (!float.IsFinite(maxDistance)) { throw new NotImplementedException("Must specify a finite distance with ray queries."); }
            else
            {
                return Iterate().Distinct();

                IEnumerable<T> Iterate()
                {
                    var c1 = GetCoord(ray.Origin);
                    var c2 = GetCoord(ray.GetPoint(maxDistance));
                    foreach (var co in Rasterizer.Line(c1, c2))
                    {
                        if (_grid.HasValue(co))
                        {
                            foreach (var item in _grid[co])
                            {
                                yield return item;
                            }
                        }
                    }
                }
            }
        }

        private IntVector GetCoord(Vector bounds)
        {
            var x = Floor(bounds.X / _cellSize);
            var y = Floor(bounds.Y / _cellSize);
            return new IntVector(x, y);
        }

        private IntRectangle GetRect(IShape boundingShape)
        {
            var bounds = boundingShape.Bounds;

            var x = Floor(bounds.X / _cellSize) - 1;
            var y = Floor(bounds.Y / _cellSize) - 1;
            var w = Ceil(bounds.Width / _cellSize) + 1;
            var h = Ceil(bounds.Height / _cellSize) + 1;

            return new IntRectangle(x, y, w, h);
        }

        /// <inheritdoc/>
        public IEnumerator<T> GetEnumerator()
        {
            return _items.GetEnumerator();
        }

        IEnumerator IEnumerable.GetEnumerator()
        {
            return GetEnumerator();
        }
    }
}
