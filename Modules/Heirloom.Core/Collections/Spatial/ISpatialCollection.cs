using Heirloom.Geometry;

namespace Heirloom.Collections
{ 
    /// <summary>
    /// A spatial collection to store and query elements in 2D space.
    /// </summary>
    /// <category>Spatial Collections</category>
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
