using Heirloom.Drawing;

namespace Heirloom.Game
{
    public abstract class DrawableComponent : Component
    {
        private int _depth = 0;

        /// <summary>
        /// Controls the drawing order, higher values are drawn on top of lower values.
        /// </summary>
        /// <remarks>
        /// Default depth is zero.
        /// </remarks>
        public int Depth
        {
            get => _depth;

            set
            {
                if (_depth != value)
                {
                    _depth = value;
                    Scene.NotifyDrawableDepthChange();
                }
            }
        }

        /// <summary>
        /// Gets or set the blending color.
        /// </summary>
        public Color Color { get; set; } = Color.White;

        /// <summary>
        /// Gets or set the blending mode.
        /// </summary>
        public Blending Blending { get; set; } = Blending.Alpha;

        internal void InternalDraw(Graphics ctx)
        {
            ctx.PushState();

            ctx.Blending = Blending;
            ctx.Color = Color;

            Draw(ctx);

            ctx.PopState();
        }

        protected abstract void Draw(Graphics ctx);
    }
}
