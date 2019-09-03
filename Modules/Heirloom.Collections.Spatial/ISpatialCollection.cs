using System.Collections.Generic;

using Heirloom.Math;

namespace Heirloom.Collections.Spatial
{
    public interface ISpatialCollection<T> : IReadOnlyCollection<T>
    {
        // Mutate
        void Add(in T item, in Rectangle bounds);
        void Update(in T item, in Rectangle bounds);
        bool Remove(in T item);
        void Clear();

        // Contains
        bool Contains(in T item);

        // Query
        IEnumerable<T> Query(Vector point);
        IEnumerable<T> Query(Rectangle bounds);
        IEnumerable<T> Query(Ray ray, float maxDistance = float.PositiveInfinity);
    }
}
