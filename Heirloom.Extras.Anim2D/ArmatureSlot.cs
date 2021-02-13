using Heirloom.Drawing;
using Heirloom.Mathematics;

namespace Heirloom.Extras.Anim2D
{
    public abstract class ArmatureSlot
    {
        public abstract void Draw(GraphicsContext gfx, Matrix matrix);

        public abstract bool IsVisible { get; set; }
    }
}
