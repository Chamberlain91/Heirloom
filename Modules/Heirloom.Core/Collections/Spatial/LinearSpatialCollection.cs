using System;
using System.Collections;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;

using Heirloom.Geometry;

namespace Heirloom.Collections
{
    /// <summary>
    /// DO NOT USE! <para/>
    /// This is incredibly slow, but useful for behaviour testing against more complex implementions of <see cref="ISpatialCollection{T}"/>. <para/>
    /// It is effectively implemented as list of shapes and does not operate on any spatial structure.
    /// </summary>
    public sealed class LinearSpatialCollection<T> : ISpatialCollection<T>
    {
        private readonly List<IShape> _bounds;
        private readonly List<T> _items;

        #region Constructors

        public LinearSpatialCollection()
        {
            _bounds = new List<IShape>();
            _items = new List<T>();
        }

        #endregion

        #region Properties

        public int Count => throw new System.NotImplementedException();

        #endregion

        #region Collection Methods

        public void Clear()
        {
            _bounds.Clear();
            _items.Clear();
        }

        public void Add(in T item, in IShape boundingShape)
        {
            _bounds.Add(boundingShape);
            _items.Add(item);

            Debug.Assert(_items.Count == _bounds.Count);
        }

        public void Update(in T item, in IShape boundingShape)
        {
            var idx = _items.IndexOf(item);

            if (idx >= 0)
            {
                // Update respective bounds
                _bounds[idx] = boundingShape;
            }
            else
            {
                // Throw a fit, the item does not exist in the collection
                throw new ArgumentException("Unable to update spatial collection, item does not exist.");
            }
        }

        public bool Remove(in T item)
        {
            var index = _items.IndexOf(item);

            if (index >= 0)
            {
                // Remove item at index
                _bounds.RemoveAt(index);
                _items.RemoveAt(index);

                Debug.Assert(_items.Count == _bounds.Count);

                return true;
            }
            else
            {
                // Item was not contained
                return false;
            }
        }

        public bool Contains(in T item)
        {
            return _items.Contains(item);
        }

        #endregion

        #region Query Methods

        public IEnumerable<T> Query(IShape queryShape)
        {
            // Every rect that overlaps the query bounds
            return _items.Where((b, i) => _bounds[i].Overlaps(queryShape));
        }

        public IEnumerable<T> Query(Vector point)
        {
            // Every rect that contains the query point
            return _items.Where((b, i) => _bounds[i].Contains(point));
        }

        public IEnumerable<T> Query(Ray ray, float maxDistance = float.PositiveInfinity)
        {
            // Every rect that touches the ray within distance limit
            return _items.Where((b, i) => _bounds[i].Raycast(ray, out var c) && c.Distance < maxDistance);
        }

        #endregion

        #region IEnumerable<T>

        public IEnumerator<T> GetEnumerator()
        {
            return _items.GetEnumerator();
        }

        IEnumerator IEnumerable.GetEnumerator()
        {
            return GetEnumerator();
        }

        #endregion
    }
}
