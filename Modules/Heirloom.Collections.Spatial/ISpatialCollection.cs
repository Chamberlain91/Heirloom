using System.Collections.Generic;

using Heirloom.Math;

namespace Heirloom.Collections.Spatial
{
    /// <summary>
    /// A spatial collection to store and query elements in 2D space.
    /// </summary>
    public interface ISpatialCollection<T> : IReadOnlyCollection<T>
    {
        /// <summary>
        /// Gets the total bounds of all elements in the collection.
        /// </summary>
        Rectangle Bounds { get; }

        /// <summary>
        /// Clears all elements from this spatial collection.
        /// </summary>
        void Clear();

        /// <summary>
        /// Adds an element with rectangle bounds into this spatial collection.
        /// </summary>
        void Add(in T item, in Rectangle bounds);

        /// <summary>
        /// Updates an exising element with new bounds in the collection.
        /// </summary>
        void Update(in T item, in Rectangle bounds);

        /// <summary>
        /// Removes an element from this spatial collection.
        /// </summary>
        bool Remove(in T item);

        /// <summary>
        /// Determines if the specified element exists in this collection.
        /// </summary>
        bool Contains(in T item);

        /// <summary>
        /// Queries the spatial collection and returns the elements with bounds that overlap the specified point.
        /// </summary>
        IEnumerable<T> Query(Vector point);

        /// <summary>
        /// Queries the spatial collection and returns the elements with bounds that overlap the specified rectangle.
        /// </summary>
        IEnumerable<T> Query(Rectangle bounds);
        
        /// <summary>
        /// Queries the spatial collection and returns the elements with bounds that intersect the specified ray.
        /// </summary>
        IEnumerable<T> Query(Ray ray, float maxDistance = float.PositiveInfinity);
    }
}
