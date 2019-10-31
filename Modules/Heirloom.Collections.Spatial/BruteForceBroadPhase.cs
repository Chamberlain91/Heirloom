using System.Collections.Generic;
using System.Linq;

using Heirloom.Math;

namespace Heirloom.Collections.Spatial
{
    /// <summary>
    /// DO NOT USE! <para/>
    /// This is incredibly slow, but useful for behaviour testing against more complex implementions of <see cref="BroadPhase{B}"/>. <para/>
    /// With only a moderate number of objects this becomes very slow, but should be bullet proof since it iterates *every* object.
    /// </summary>
    /// <typeparam name="B"></typeparam>
    public sealed class BruteForceBroadPhase<B> : BroadPhase<B> where B : class, IBroadPhaseObject
    {
        public BruteForceBroadPhase()
        {
            // Nothing
        }

        protected override void InsertStructure(B body)
        {
            // Nothing
        }

        protected override void RemoveStructure(B body)
        {
            // Nothing
        }

        protected override void UpdateStructure(B obj)
        {
            // Nothing
        }

        public override IEnumerable<B> Query(B body)
        {
            // Everything touching the body bounds, ignoring the query body
            return Query(body.Bounds)
                  .Where(other => !Equals(other, body));
        }

        public override IEnumerable<B> Query(Rectangle region)
        {
            // Every body that overlaps the query region
            return Bodies.Where(b => b.Bounds.Overlaps(region));
        }

        public override IEnumerable<B> Query(Vector point)
        {
            // Every body that contains the query point
            return Bodies.Where(b => b.Bounds.Contains(point));
        }

        public override IEnumerable<B> Query(Ray ray, float maxDistance)
        {
            return Bodies;
        }
    }
}
