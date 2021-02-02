using System;
using System.Collections;
using System.Collections.Generic;

namespace Heirloom.Collections
{
    /// <summary>
    /// Implements a priority queue backed by <see cref="Heap{T}"/>.
    /// </summary>
    /// <typeparam name="T">Some item to prioritize.</typeparam>
    /// <typeparam name="P">Some priority value.</typeparam>
    public class PriorityQueue<T, P> : IPriorityQueue<T, P> where P : IComparable<P>
    {
        private readonly Dictionary<T, Element> _elements = new Dictionary<T, Element>();
        private readonly Heap<Element> _heap = new Heap<Element>();

        /// <inheritdoc/>
        public int Count => _heap.Count;

        /// <inheritdoc/>
        public void Add(T item, P priority)
        {
            var element = new Element(item, priority);

            _elements[item] = element;
            _heap.Add(element);
        }

        /// <inheritdoc/>
        public void Update(T item, P priority)
        {
            var element = _elements[item];
            element.Priority = priority;

            _heap.Update(element);
        }

        /// <inheritdoc/>
        public T Pop()
        {
            return Pop(out _);
        }

        /// <inheritdoc/>
        public T Pop(out P priority)
        {
            var element = _heap.Remove();
            _elements.Remove(element.Item);

            priority = element.Priority;
            return element.Item;
        }

        /// <inheritdoc/>
        public bool Contains(T item)
        {
            return _elements.ContainsKey(item);
        }

        /// <inheritdoc/>
        public P PeekPriority()
        {
            return _heap.Peek().Priority;
        }

        /// <inheritdoc/>
        public T Peek()
        {
            return _heap.Peek().Item;
        }

        private class Element : IComparable<Element>, IEquatable<Element>
        {
            public readonly T Item;

            public P Priority;

            public Element(T item, P priority)
            {
                Item = item;
                Priority = priority;
            }

            public int CompareTo(Element other)
            {
                return Priority.CompareTo(other.Priority);
            }

            #region Equality

            public override bool Equals(object obj)
            {
                return obj is Element element && Equals(element);
            }

            public bool Equals(Element other)
            {
                return EqualityComparer<T>.Default.Equals(Item, other.Item);
            }

            public override int GetHashCode()
            {
                return HashCode.Combine(Item);
            }

            public static bool operator ==(Element left, Element right)
            {
                return left.Equals(right);
            }

            public static bool operator !=(Element left, Element right)
            {
                return !(left == right);
            }

            #endregion
        }

        /// <inheritdoc/>
        public IEnumerator<T> GetEnumerator()
        {
            foreach (var e in _heap)
            {
                yield return e.Item;
            }
        }

        IEnumerator IEnumerable.GetEnumerator()
        {
            return GetEnumerator();
        }
    }
}
