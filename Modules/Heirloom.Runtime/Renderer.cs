using Heirloom.Drawing;

namespace Heirloom.Runtime
{
    public abstract class Renderer : Component
    {
        public virtual Color Color { get; set; } = Color.White;
    }
}
