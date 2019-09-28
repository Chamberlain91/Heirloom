
using Heirloom.Drawing;

namespace Heirloom.Desktop.Game
{
    public abstract class Renderer : Component
    {
        /// <summary>
        /// Gets or sets if this component will draw.
        /// </summary>
        public bool IsVisible { get; set; } = true;

        protected internal abstract void Draw(RenderContext ctx);
    }
}
