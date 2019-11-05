using System.Collections;
using System.Collections.Generic;
using System.Runtime.CompilerServices;

using Heirloom.Math;

namespace Heirloom.Collections.Spatial
{
    public sealed class BroadPhase<T> : IBroadPhase<T>
        where T : class, IBroadPhaseObject
    {
        private readonly List<T> _bodyList;

        private readonly HashSet<BroadPhasePair<T>> _knownBroadPairs;

        private readonly ISpatialCollection<T> _collection;

        #region Constructors

        public BroadPhase()
            : this(new BoundingHierarchy<T>())
        { }

        public BroadPhase(ISpatialCollection<T> spatialCollection)
        {
            _collection = spatialCollection;

            _knownBroadPairs = new HashSet<BroadPhasePair<T>>();
            _bodyList = new List<T>();
        }

        #endregion

        #region Properties

        public IReadOnlyList<T> Bodies => _bodyList;

        public int Count => _bodyList.Count;

        #endregion

        #region Collection Methods

        public void Add(T obj)
        {
            // Insert into structure
            _collection.Add(obj, obj.Bounds);
            _bodyList.Add(obj);
        }

        public void Remove(T obj)
        {
            // Remove from structure
            _collection.Remove(obj);
            _bodyList.Remove(obj);
        }

        public void Update(T obj)
        {
            // Update structure
            _collection.Update(obj, obj.Bounds);
        }

        #endregion

        public IEnumerable<BroadPhasePair<T>> GetCollisionCandidates()
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
                    var pair = new BroadPhasePair<T>(a, b);

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
        public IEnumerable<T> Query(T body)
        {
            return Query(body.Bounds);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public IEnumerable<T> Query(Ray ray, float maxDistance = float.PositiveInfinity)
        {
            return _collection.Query(ray, maxDistance);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public IEnumerable<T> Query(Rectangle bounds)
        {
            return _collection.Query(bounds);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public IEnumerable<T> Query(Vector point)
        {
            return _collection.Query(point);
        }

        #endregion

        #region IEnumerable<T>

        public IEnumerator<T> GetEnumerator()
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
