using System.Collections.Generic;

namespace Heirloom.Collections.Spatial
{
    /// <summary>
    /// A spatial collection to store and query elements in 2D space suitable for collision detection schemes.
    /// </summary>
    public interface IBroadPhase<T> : IReadOnlyCollection<T>, ISpatialQuery<T>
        where T : class, IBroadPhaseObject
    { 
        IReadOnlyList<T> Bodies { get; }

        void Add(T obj);
        void Remove(T obj);
        void Update(T obj);

        IEnumerable<BroadPhasePair<T>> GetCollisionCandidates();
        IEnumerable<T> Query(T body);
    }
}
