using System.Collections.Generic;

using Heirloom.Geometry;

namespace Heirloom.Collections
{
    /// <summary>
    /// Provides methods for querying elements in 2D space.
    /// </summary>
    /// <category>Spatial Collections</category>
    public interface ISpatialQuery<T>
    {
        /// <summary>
        /// Finds spatial elements that overlap the specified point.
        /// </summary>
        IEnumerable<T> Query(Vector point);

        /// <summary>
        /// Finds spatial elements that overlap the specified rectangle.
        /// </summary>
        IEnumerable<T> Query(IShape queryShape);

        /// <summary>
        /// Finds spatial elements that intersect the specified ray.
        /// </summary>
        IEnumerable<T> Query(Ray ray, float maxDistance = float.PositiveInfinity);
    }
}
