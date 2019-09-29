using Heirloom.Drawing;

namespace Heirloom.Game
{
    public abstract class DrawableComponent : Component
    {
        /// <summary>
        /// Gets or sets if this component will draw.
        /// </summary>
        public bool IsVisible { get; set; } = true;

        protected internal abstract void Draw(RenderContext ctx);
    }
}
