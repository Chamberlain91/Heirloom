using System.Collections.Generic;

namespace Heirloom
{
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
