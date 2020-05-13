namespace Heirloom
{
    /// <summary>
    /// The abstract representation of a particular surface effect.
    /// </summary>
    /// <remarks>A surface effect modifies the content of a surface through a shader based post processing step.</remarks>
    public abstract class SurfaceEffect
    {
        /// <summary>
        /// A surface effect that makes no changes.
        /// </summary>
        public static readonly SurfaceEffect None = new NullEffect();

        /// <summary>
        /// Called when the effect should be applied to the specified surface.
        /// </summary>
        /// <param name="gfx">The graphics context.</param>
        /// <param name="surface">The target surface to modify.</param>
        protected internal abstract void Apply(GraphicsContext gfx, Surface surface);

        private class NullEffect : SurfaceEffect
        {
            protected internal override void Apply(GraphicsContext gfx, Surface surface)
            {
                // Do nothing!
            }
        }
    }
}
