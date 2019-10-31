using System.Collections.Generic;
using System.Linq;

using Heirloom.Math;

namespace Heirloom.Collections.Spatial
{
    public sealed class SpatialBroadPhase<B> : BroadPhase<B> where B : class, IBroadPhaseObject
    {
        public readonly SpatialCollection<B> Collection;

        public SpatialBroadPhase(float margin = 0.05F)
        {
            Collection = new SpatialCollection<B>(margin);
        }

        protected override void InsertStructure(B obj)
        {
            Collection.Add(obj, obj.Bounds);
        }

        protected override void RemoveStructure(B obj)
        {
            Collection.Remove(obj);
        }

        protected override void UpdateStructure(B obj)
        {
            Collection.Update(obj, obj.Bounds);
        }

        public override IEnumerable<B> Query(B obj)
        {
            return Collection.Query(obj.Bounds)
                             .Where(b => !Equals(b, obj));
        }

        public override IEnumerable<B> Query(Ray ray, float maxDistance)
        {
            return Collection.Query(ray, maxDistance);
        }

        public override IEnumerable<B> Query(Rectangle region)
        {
            return Collection.Query(region);
        }

        public override IEnumerable<B> Query(Vector point)
        {
            return Collection.Query(point);
        }
    }
}
