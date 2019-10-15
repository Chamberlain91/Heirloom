using System.Collections.Generic;

using Heirloom.Collections.Spatial;
using Heirloom.Game;
using Heirloom.Math;

namespace Examples.Game
{
    public class Collider : Component, ISpatialObject
    {
        public Collider(Rectangle localBounds)
        {
            LocalBounds = localBounds;
        }

        public Rectangle LocalBounds { get; set; }

        public PhysicsComponent Physics => Entity?.GetComponent<PhysicsComponent>();

        public Rectangle Bounds
        {
            get
            {
                var pos = Physics?.Position ?? Transform.Position;

                return new Rectangle
                {
                    X = LocalBounds.X + pos.X,
                    Y = LocalBounds.Y + pos.Y,
                    Width = LocalBounds.Width,
                    Height = LocalBounds.Height
                };
            }
        }

        protected override void OnAdded()
        {
            Colliders.Add(this);
            Transform.Changed += Transform_Changed;
        }

        protected override void OnRemoved()
        {
            Transform.Changed -= Transform_Changed;
            Colliders.Remove(this);
        }

        private void Transform_Changed()
        {
            Colliders.Update(this);
        }

        // todo: IReadOnlyBroadPhase
        public static BroadPhase<Collider> Colliders { get; } = new SpatialBroadPhase<Collider>( );

        public static IEnumerable<Collider> GetColliders(Collider collider)
        {
            foreach (var other in Colliders.Query(collider.Bounds.Inflate(16)))
            {
                yield return other;
            }
        }
    }
}
