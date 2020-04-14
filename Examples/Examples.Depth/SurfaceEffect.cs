using System;

using Heirloom.Drawing;

namespace Examples.Depth
{
    public abstract class SurfaceEffect
    {
        public readonly int Downscale;

        public static readonly SurfaceEffect None = new NullEffect();

        protected SurfaceEffect(int downscale = 1)
        {
            if (downscale <= 0) { throw new ArgumentException("Downscale must be larger than zero."); }
            Downscale = downscale;
        }

        public void Apply(Graphics gfx, Surface surface)
        {
            if (surface == gfx.DefaultSurface)
            {
                throw new ArgumentException("Unable to apply effect to default surface");
            }

            gfx.PushState(true);
            Compose(gfx, surface);
            gfx.PopState();
        }

        protected abstract void Compose(Graphics gfx, Surface surface);

        private class NullEffect : SurfaceEffect
        {
            protected override void Compose(Graphics gfx, Surface surface)
            {
                // Do nothing!
            }
        }
    }
}
