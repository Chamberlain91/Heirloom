using System.Collections.Generic;

namespace Heirloom.Runtime
{
    internal class DelayedMutateSet<T>
    {
        private readonly HashSet<T> _addItems;
        private readonly HashSet<T> _remItems;
        private readonly HashSet<T> _items;

        public DelayedMutateSet()
        {
            _addItems = new HashSet<T>();
            _remItems = new HashSet<T>();
            _items = new HashSet<T>();
        }

        public int Count => _items.Count + _addItems.Count;

        public IEnumerable<T> Items => _items;

        public IEnumerable<T> ItemsToAdd => _addItems;

        public IEnumerable<T> ItemsToRemove => _remItems;

        public IEnumerable<T> ProcessAddedItems()
        {
            foreach (var item in _addItems)
            {
                _items.Add(item);
                yield return item;
            }

            _addItems.Clear();
        }

        public IEnumerable<T> ProcessRemovedItems()
        {
            foreach (var item in _remItems)
            {
                _items.Remove(item);
                yield return item;
            }

            _remItems.Clear();
        }

        public bool Contains(T item)
        {
            return _items.Contains(item)
                || _addItems.Contains(item);
        }

        public bool Add(T item)
        {
            if (!_items.Contains(item))
            {
                _remItems.Remove(item);
                return _addItems.Add(item);
            }

            return false;
        }

        public bool Remove(T item)
        {
            if (_items.Contains(item))
            {
                _addItems.Remove(item);
                return _remItems.Add(item);
            }

            return false;
        }
    }
}
