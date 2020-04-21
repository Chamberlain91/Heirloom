using System.Collections.Generic;

using Heirloom;

namespace Heirloom.Collections
{
    /// <summary>
    /// A read-only view of a spatial collection to query elements in 2D space.
    /// </summary>
    public interface IReadOnlySpatialCollection<T> : ISpatialQuery<T>, IReadOnlyCollection<T>
    {
        /// <summary>
        /// Determines if the specified element exists in this collection.
        /// </summary>
        bool Contains(in T item);
    }

    /// <summary>
    /// A spatial collection to store and query elements in 2D space.
    /// </summary>
    public interface ISpatialCollection<T> : IReadOnlySpatialCollection<T>
    {
        /// <summary>
        /// Clears all elements from this spatial collection.
        /// </summary>
        void Clear();

        /// <summary>
        /// Adds an element with rectangle bounds into this spatial collection.
        /// </summary>
        void Add(in T item, in IShape boundingShape);

        /// <summary>
        /// Updates an exising element with new bounds in the collection.
        /// </summary>
        void Update(in T item, in IShape boundingShape);

        /// <summary>
        /// Removes an element from this spatial collection.
        /// </summary>
        bool Remove(in T item);
    }
}
