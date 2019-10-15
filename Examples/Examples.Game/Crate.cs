
using Heirloom.Drawing;
using Heirloom.Game;

using static Heirloom.Game.AssetDatabase;

namespace Examples.Game
{
    public class Crate : Entity
    {
        public readonly ImageComponent ImageComponent;

        public readonly PhysicsComponent Physics;

        public readonly Collider Collider;

        public Crate()
        {
            // Add image component
            ImageComponent = AddComponent(new ImageComponent(GetAsset<Image>("tile.crate")));

            // Add physics and collider components
            Collider = AddComponent(new Collider(ImageComponent.Image.Bounds));
            Physics = AddComponent(new PhysicsComponent());
        }

        protected override void FixedUpdate(float ft)
        {
            if (Physics.HasGroundCollision)
            {
                // Not correct friction, but a sliding stop.
                Physics.Velocity -= (Physics.Velocity.X * 0.125F, 0);
            }
        }
    }
}
