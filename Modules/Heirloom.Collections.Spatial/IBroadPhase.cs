using System.Collections.Generic;

namespace Heirloom.Collections.Spatial
{
    /// <summary>
    /// A spatial collection to store and query elements in 2D space suitable for collision detection schemes.
    /// </summary>
    public interface IBroadPhase<B> : IReadOnlyCollection<B>, ISpatialQuery<B>
        where B : class, IBroadPhaseObject
    { 
        IReadOnlyList<B> Bodies { get; }

        void Add(B obj);
        void Remove(B obj);
        void Update(B obj);

        IEnumerable<BroadPhasePair<B>> GetCollisionCandidates();
        IEnumerable<B> Query(B body);
    }
}
