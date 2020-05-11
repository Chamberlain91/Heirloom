using System;

namespace Heirloom.OpenGLES
{
    internal sealed class FramebufferStorage
    {
        // public readonly Renderbuffer Renderbuffer;

        public readonly Texture MultisampleTexture;

        public readonly Texture Texture;

        public FramebufferStorage(Surface surface)
        {
            var format = surface.SurfaceType switch
            {
                SurfaceType.Float => TextureSizedFormat.RGBA16F,
                SurfaceType.UnsignedByte => TextureSizedFormat.RGBA8,

                _ => throw new ArgumentException("Unknown surface type.", nameof(surface.SurfaceType)),
            };

            Texture = new Texture(surface.Size, format);

            if (surface.Multisample > MultisampleQuality.None)
            {
                MultisampleTexture = new Texture(surface.Size, format, (int) surface.Multisample);
                // Renderbuffer = new Renderbuffer(surface);
            }
        }

        // public bool HasRenderbuffer => Renderbuffer != null;

        public bool HasMultisampleTarget => MultisampleTexture != null;
    }
}
