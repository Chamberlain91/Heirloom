using System;

namespace Meadows.Collections
{
    /// <summary>
    /// Represents an mutable interface for a weighted collection (discrete distribution) of items.
    /// </summary>
    public interface IWeightedCollection<T> : IReadOnlyWeightedCollection<T>
    {
        /// <summary>
        /// Removes all items from this collection.
        /// </summary>
        void Clear();

        /// <summary>
        /// Adds a weighted item to this collection.
        /// </summary>
        /// <param name="value">Some value to insert into the collection.</param>
        /// <param name="weight">The weight of the value.</param>
        /// <exception cref="ArgumentException">Thrown when the value already exists in the collection.</exception>
        void Add(T value, float weight);

        /// <summary>
        /// Updates the weight of an item to this collection.
        /// </summary>
        /// <param name="value">Some value existing in the collection.</param>
        /// <param name="weight">The weight of the value.</param>
        /// <exception cref="ArgumentException">Thrown when the value does not exist in the collection.</exception>
        void Update(T value, float weight);

        /// <summary>
        /// Removes an item from the collection.
        /// </summary>
        /// <param name="value">Some value existing in the collection.</param>
        /// <returns>True if the item exists, otherwise false.</returns>
        bool Remove(T value);
    }
}
