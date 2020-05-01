namespace Heirloom
{
    public abstract class SurfaceEffect
    {
        /// <summary>
        /// A surface effect that makes no changes.
        /// </summary>
        public static readonly SurfaceEffect None = new NullEffect();
         
        protected internal abstract void Apply(Graphics gfx, Surface surface);

        private class NullEffect : SurfaceEffect
        {
            protected internal override void Apply(Graphics gfx, Surface surface)
            {
                // Do nothing!
            }
        }
    }
}
