using System.Collections;
using System.Collections.Generic;

using Heirloom.Math; 

namespace Heirloom.Collections.Spatial
{
    // todo: BroadPhase<B> probably should not act like a list itself? Change to IEnumerable?
    public abstract class BroadPhase<B> : IReadOnlyList<B> where B : class, ISpatialObject
    {
        private readonly HashSet<B> _bodySet;
        private readonly List<B> _bodyList;

        private readonly HashSet<BroadPhasePair<B>> _knownBroadPairs;

        protected BroadPhase()
        {
            _bodySet = new HashSet<B>();
            _bodyList = new List<B>();

            _knownBroadPairs = new HashSet<BroadPhasePair<B>>();
        }

        public B this[int index] => _bodyList[index];

        public int Count => _bodyList.Count;

        public IReadOnlyList<B> Bodies => _bodyList;

        public void Add(B obj)
        {
            if (_bodySet.Add(obj))
            {
                // Insert into structure
                InsertStructure(obj);

                // 
                _bodyList.Add(obj);
            }
        }

        public void Remove(B obj)
        {
            if (_bodySet.Remove(obj))
            {
                // Remove from structure
                RemoveStructure(obj);

                // 
                _bodyList.Remove(obj);
            }
        }

        public void Update(B obj)
        {
            // Was not contained, insert?
            // or should it throw exception?
            // todo: make decision ^
            if (!_bodySet.Contains(obj))
            {
                Add(obj);
            }

            // Remove from structure
            UpdateStructure(obj);
        }

        protected abstract void InsertStructure(B obj);

        protected abstract void RemoveStructure(B obj);

        protected abstract void UpdateStructure(B obj);

        public IEnumerable<BroadPhasePair<B>> QueryCollisionCandidates()
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

        public abstract IEnumerable<B> Query(B body);

        public abstract IEnumerable<B> Query(Ray ray, float maxDistance);

        public abstract IEnumerable<B> Query(Rectangle region);

        public abstract IEnumerable<B> Query(Vector point);

        public IEnumerator<B> GetEnumerator()
        {
            return _bodyList.GetEnumerator();
        }

        IEnumerator IEnumerable.GetEnumerator()
        {
            return _bodyList.GetEnumerator();
        }
    }
}
