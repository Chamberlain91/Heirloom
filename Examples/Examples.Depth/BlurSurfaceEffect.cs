using Heirloom.Drawing;
using Heirloom.Math;

namespace Examples.Depth
{
    public sealed class BlurSurfaceEffect : SurfaceEffect
    {
        private readonly VectorBlurShader _blurShader;

        public float Strength;

        public BlurSurfaceEffect(int blurQuality, float strength = 4)
        {
            // Load shader for each kernel size
            _blurShader = new VectorBlurShader(blurQuality);
            Strength = strength;
        }

        protected override void Apply(Graphics gfx, Surface surface)
        {
            // Will compose images without blending
            gfx.Blending = Blending.Opaque;
            gfx.Shader = _blurShader;

            // Request a temporary surface (we need to multipass)
            var temp = SurfacePool.Request(surface.Size);
            {
                // Horizontal pass (surface to temp) 
                gfx.Surface = temp;
                _blurShader.Vector = Vector.Right * Strength;
                gfx.DrawImage(surface, Vector.Zero);

                // Vertical pass (temp to surface)
                gfx.Surface = surface;
                _blurShader.Vector = Vector.Down * Strength;
                gfx.DrawImage(temp, Vector.Zero);
            }
            SurfacePool.Recycle(temp);
        }
    }
}
