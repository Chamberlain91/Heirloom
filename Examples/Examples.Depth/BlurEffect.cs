using Heirloom.Drawing;
using Heirloom.Math;

namespace Examples.Depth
{
    public sealed class BlurEffect : SurfaceEffect
    {
        public readonly Shader Shader;

        public readonly float Strength;

        public BlurEffect(float strength)
            : base(downscale: 2)
        {
            Shader = new Shader("files/blur.frag");
            Strength = strength;
        }

        protected override void Compose(Graphics gfx, Surface surface)
        {
            // Will compose images without blending
            gfx.Blending = Blending.Opaque;
            gfx.Shader = Shader;

            // Set texel size to shader
            Shader.SetUniform("uTexelSize", 1F / (Vector) surface.Size);

            // Request a temporary surface (we need to multipass)
            var temp = SurfacePool.Request(surface.Size);
            {
                // Horizontal pass (surface to temp) 
                gfx.Surface = temp;
                Shader.SetUniform("uVector", Vector.Right * Strength);
                gfx.DrawImage(surface, Vector.Zero);

                // Vertical pass (temp to surface)
                gfx.Surface = surface;
                Shader.SetUniform("uVector", Vector.Down * Strength);
                gfx.DrawImage(temp, Vector.Zero);
            }
            SurfacePool.Recycle(temp);
        }
    }
}
