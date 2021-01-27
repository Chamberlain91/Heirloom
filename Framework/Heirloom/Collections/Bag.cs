using System;
using System.Collections;
using System.Collections.Generic;

namespace Heirloom.Collections
{
    public sealed class Bag<T> : ICollection<T>
    {
        private readonly List<T> _items;

        #region Constructors

        public Bag()
        {
            _items = new List<T>();
        }

        public Bag(IEnumerable<T> items)
        {
            _items = new List<T>(items);
        }

        #endregion

        public int Count => _items.Count;

        bool ICollection<T>.IsReadOnly => false;

        #region Bag Operators

        public bool TryPeek(out T item)
        {
            if (Count > 0)
            {
                // Return last item
                item = _items[Count - 1];
                return true;
            }
            else
            {
                item = default;
                return false;
            }
        }

        public bool TryTake(out T item)
        {
            if (TryPeek(out item))
            {
                // Remove last item
                _items.RemoveAt(Count - 1);
                return true;
            }
            else
            {
                return false;
            }
        }

        public T Peek()
        {
            if (TryPeek(out var item))
            {
                return item;
            }
            else
            {
                throw new InvalidOperationException("Unable to peek into an empty bag");
            }
        }

        public T Take()
        {
            if (TryTake(out var item))
            {
                return item;
            }
            else
            {
                throw new InvalidOperationException("Unable to take from an empty bag");
            }
        }

        #endregion

        #region Collection Operators

        public void Add(T item)
        {
            _items.Add(item);
        }

        public void Clear()
        {
            _items.Clear();
        }

        public bool Contains(T item)
        {
            // todo: could double up with a HashSet to make Contains O(1) for ~2N memory
            return _items.Contains(item);
        }

        void ICollection<T>.CopyTo(T[] array, int arrayIndex)
        {
            _items.CopyTo(array, arrayIndex);
        }

        bool ICollection<T>.Remove(T item)
        {
            return _items.Remove(item);
        }

        #endregion

        public IEnumerator<T> GetEnumerator()
        {
            return _items.GetEnumerator();
        }

        IEnumerator IEnumerable.GetEnumerator()
        {
            return _items.GetEnumerator();
        }
    }
}
