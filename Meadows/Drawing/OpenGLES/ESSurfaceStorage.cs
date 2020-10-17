using System;

namespace Meadows.Drawing.OpenGLES
{
    // holds textures for a surface
    internal sealed class ESSurfaceStorage
    {
        public ESTexture MultisampleTexture;

        public ESTexture Texture;

        public ESSurfaceStorage(Surface surface)
        {
            var textureFormat = surface.Format switch
            {
                SurfaceFormat.Float => TextureSizedFormat.RGBA16F,
                SurfaceFormat.UnsignedByte => TextureSizedFormat.RGBA8,

                _ => throw new ArgumentException("Unknown surface type.", nameof(surface.Format)),
            };

            // Create a standard texture
            Texture = new ESTexture(surface.Size, textureFormat);

            // If multisampling is specifed...
            if (surface.Multisample > MultisampleQuality.None)
            {
                // We must create a multisampled texture
                MultisampleTexture = new ESTexture(surface.Size, textureFormat, (int) surface.Multisample);
            }
        }

        public bool HasMultisampleTarget => MultisampleTexture != null;
    }
}
