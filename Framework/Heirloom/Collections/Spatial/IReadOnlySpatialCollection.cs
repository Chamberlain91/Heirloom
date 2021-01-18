using System.Collections.Generic;

namespace Heirloom.Collections
{
    /// <summary>
    /// A read-only view of a spatial collection to query elements in 2D space.
    /// </summary>
    /// <category>Spatial Collections</category>
    public interface IReadOnlySpatialCollection<T> : ISpatialQuery<T>, IReadOnlyCollection<T>
    {
        /// <summary>
        /// Determines if the specified element exists in this collection.
        /// </summary>
        bool Contains(T item);
    }
}
