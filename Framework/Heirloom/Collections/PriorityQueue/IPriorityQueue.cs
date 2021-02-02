using System;
using System.Collections.Generic;

namespace Heirloom.Collections
{
    public interface IPriorityQueue<T, P> : IReadOnlyPriorityQueue<T, P> where P : IComparable<P>
    {
        /// <summary>
        /// Inserts a new item into the priority queue.
        /// </summary>
        void Add(T item, P priority);

        /// <summary>
        /// Updates an items priority value.
        /// </summary>
        void Update(T item, P priority);

        /// <summary>
        /// Gets the next priority item and removes it. Additionally returning the item's priority value.
        /// </summary>
        T Pop(out P priority);

        /// <summary>
        /// Gets the next priority item and removes it.
        /// </summary>
        T Pop();
    }
}
