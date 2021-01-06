using System;
using System.Collections.Generic;

namespace Meadows.Collections
{
    /// <summary>
    /// Represents an read-only interface for a weighted collection (discrete distribution) of items.
    /// </summary>
    public interface IReadOnlyWeightedCollection<T> : IReadOnlyCollection<(T, float)>
    {
        /// <summary>
        /// Determines if the specified item exists in the collection.
        /// </summary>
        /// <param name="value">Some item that may be in the collection.</param>
        /// <returns>Return true if the item does exist in the collection.</returns>
        bool Contains(T value);

        /// <summary>
        /// Attempts to get the weight of an item in the collection.
        /// </summary>
        /// <param name="value">Some item that may be in the collection.</param>
        /// <param name="weight">An output of the weight of the item, if exists.</param>
        /// <returns>Return true if the item does exist in the collection and the weight is retrieved successfully.</returns>
        bool TryGetWeight(T value, out float weight);

        /// <summary>
        /// Gets the weights of an item in the collection.
        /// </summary>
        /// <param name="value">Some item that exists in the collection.</param>
        /// <returns>Return the weight of the item.</returns>
        /// <exception cref="ArgumentException">Thrown when the item does exist in the collection.</exception>
        float GetWeight(T value);

        /// <summary>
        /// Gets a random item from the collection, complying with the implicit distribution.
        /// </summary>
        /// <param name="random">Some instance of ranom</param>
        /// <returns></returns>
        /// <exception cref="ArgumentNullException">Thrown when <paramref name="random"/> is null.</exception>
        T GetValue(Random random);
    }
}
