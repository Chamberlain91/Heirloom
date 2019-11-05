using System.Collections.Generic;

namespace Heirloom.Collections
{
    /// <summary>
    /// Represents a heap data structure.
    /// Allowing the access and removal of items by a priority ordering.
    /// </summary>
    /// <typeparam name="T">Type of the elements.</typeparam>
    public interface IHeap<T> : IReadOnlyHeap<T>
    {
        /// <summary>
        /// Adds an item to the heap.
        /// </summary>
        bool Add(T item);

        /// <summary>
        /// Adds multiple items to the heap.
        /// </summary>
        void AddRange(IEnumerable<T> items);

        /// <summary>
        /// Removes a specific item from the heap.
        /// </summary>
        bool Remove(T item);

        /// <summary>
        /// Removes and returns the next priority item in the heap.
        /// </summary>
        T Remove();

        /// <summary>
        /// Alerts the heap to update the position the element within the heap.
        /// </summary>
        void Update(T item);
    }

    /// <summary>
    /// Represents a read-only view of a <see cref="Heap{T}"/>.
    /// </summary>
    public interface IReadOnlyHeap<T> : IReadOnlyCollection<T> // TODO: Implement all of ICollection<T>
    {
        /// <summary>
        /// The comparer used to compare item priority.
        /// </summary>
        Comparer<T> Comparer { get; }

        /// <summary>
        /// Does this heap contain the specified item?
        /// </summary>
        /// <param name="item">Some object.</param>
        /// <returns>True, if the item was contained.</returns>
        bool Contains(T item);

        /// <summary>
        /// Gets the next item in the heap to be removed.
        /// </summary>
        T Peek();
    }
}
