using Heirloom.Drawing;
using Heirloom.Game;

namespace Examples.Game
{
    public class BroadPhaseVisualization : DrawableComponent
    {
        public Collider Collider;

        public BroadPhaseVisualization(Collider collider)
        {
            Collider = collider;
        }

        protected override void Draw(Graphics ctx)
        {
            ctx.Color = Color.Pink;

            foreach (var colliders in Collider.GetColliders(Collider))
            {
                ctx.DrawRectOutline(colliders.Bounds);
            }
        }
    }
}
