using Heirloom.Drawing;

namespace Heirloom.Game
{
    public abstract class Renderer : DrawableComponent
    {
        /// <summary>
        /// Gets or set the blending color.
        /// </summary>
        public Color Color { get; set; } = Color.White;

        /// <summary>
        /// Gets or set the blending mode.
        /// </summary>
        public Blending Blending { get; set; } = Blending.Alpha;
    }
}
