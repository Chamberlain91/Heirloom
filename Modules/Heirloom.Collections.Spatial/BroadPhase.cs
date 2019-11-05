using System.Collections;
using System.Collections.Generic;
using System.Runtime.CompilerServices;

using Heirloom.Math;

namespace Heirloom.Collections.Spatial
{
    public sealed class BroadPhase<B> : IBroadPhase<B>
        where B : class, IBroadPhaseObject
    {
        private readonly List<B> _bodyList;

        private readonly HashSet<BroadPhasePair<B>> _knownBroadPairs;

        private readonly ISpatialCollection<B> _collection;

        #region Constructors

        public BroadPhase()
            : this(new BoundingHierarchy<B>())
        { }

        public BroadPhase(ISpatialCollection<B> spatialCollection)
        {
            _collection = spatialCollection;

            _knownBroadPairs = new HashSet<BroadPhasePair<B>>();
            _bodyList = new List<B>();
        }

        #endregion

        #region Properties

        public IReadOnlyList<B> Bodies => _bodyList;

        public int Count => _bodyList.Count;

        #endregion

        #region Collection Methods

        public void Add(B obj)
        {
            // Insert into structure
            _collection.Add(obj, obj.Bounds);
            _bodyList.Add(obj);
        }

        public void Remove(B obj)
        {
            // Remove from structure
            _collection.Remove(obj);
            _bodyList.Remove(obj);
        }

        public void Update(B obj)
        {
            // Update structure
            _collection.Update(obj, obj.Bounds);
        }

        #endregion

        public IEnumerable<BroadPhasePair<B>> GetCollisionCandidates()
        {
            // Clear previously known pairs
            _knownBroadPairs.Clear();

            // for each body `a`
            foreach (var a in this)
            {
                // For every body `b` relevant to `a`
                foreach (var b in Query(a))
                {
                    // 
                    var pair = new BroadPhasePair<B>(a, b);

                    // Does the pair exist yet?
                    if (_knownBroadPairs.Add(pair))
                    {
                        // Emit collision pair
                        yield return pair;
                    }
                }
            }
        }

        #region Query Methods

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public IEnumerable<B> Query(B body)
        {
            return Query(body.Bounds);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public IEnumerable<B> Query(Ray ray, float maxDistance = float.PositiveInfinity)
        {
            return _collection.Query(ray, maxDistance);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public IEnumerable<B> Query(Rectangle bounds)
        {
            return _collection.Query(bounds);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public IEnumerable<B> Query(Vector point)
        {
            return _collection.Query(point);
        }

        #endregion

        #region IEnumerable<T>

        public IEnumerator<B> GetEnumerator()
        {
            return _bodyList.GetEnumerator();
        }

        IEnumerator IEnumerable.GetEnumerator()
        {
            return _bodyList.GetEnumerator();
        }

        #endregion
    }
}
