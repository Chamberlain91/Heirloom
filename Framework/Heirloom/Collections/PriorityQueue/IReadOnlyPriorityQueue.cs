using System;
using System.Collections.Generic;

namespace Heirloom.Collections
{
    public interface IReadOnlyPriorityQueue<T, P> : IReadOnlyCollection<T> where P : IComparable<P>
    {
        /// <summary>
        /// Does this priority queue contain the specified item?
        /// </summary>
        /// <param name="item">Some object.</param>
        /// <returns>True, if the item was contained.</returns>
        bool Contains(T item);

        /// <summary>
        /// Gets the next priority item's priority value.
        /// </summary>
        P PeekPriority();

        /// <summary>
        /// Gets the next priority item without removing it.
        /// </summary>
        T Peek();
    }
}
