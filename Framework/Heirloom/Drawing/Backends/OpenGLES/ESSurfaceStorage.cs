using System;

namespace Heirloom.Drawing.OpenGLES
{
    // holds textures for a surface
    internal sealed class ESSurfaceStorage : IDisposable
    {
        public ESTexture MultisampleTexture;

        public ESTexture Texture;

        public ESTexture StencilTexture;

        public ESSurfaceStorage(Surface surface)
        {
            var textureFormat = surface.Format switch
            {
                SurfaceFormat.Float => TextureSizedFormat.RGBA16F,
                SurfaceFormat.UnsignedByte => TextureSizedFormat.RGBA8,

                _ => throw new ArgumentException("Unknown surface type.", nameof(surface)),
            };

            // Create a standard texture
            Texture = new ESTexture(surface.Size, textureFormat);

            // todo: detect support for and use index8 to reduce memory usage
            // #define GL_STENCIL_INDEX8  0x8D48

            // If multisampling is specifed...
            if (surface.Multisample > MultisampleQuality.None)
            {
                // We must create a multisampled textures
                MultisampleTexture = new ESTexture(surface.Size, textureFormat, (int) surface.Multisample);
                StencilTexture = new ESTexture(surface.Size, TextureSizedFormat.Depth24_Stencil8, (int) surface.Multisample);
            }
            else
            {
                // Create stencil buffer
                StencilTexture = new ESTexture(surface.Size, TextureSizedFormat.Depth24_Stencil8);
            }
        }

        ~ESSurfaceStorage()
        {
            Dispose(disposing: false);
        }

        public bool HasMultisampleTarget => MultisampleTexture != null;

        private bool _isDisposed;

        private void Dispose(bool disposing)
        {
            if (!_isDisposed)
            {
                if (disposing)
                {
                    // no managed to dispose
                }

                // Dispose textures
                MultisampleTexture?.Dispose();
                StencilTexture?.Dispose();
                Texture?.Dispose();

                _isDisposed = true;
            }
        }

        public void Dispose()
        {
            GC.SuppressFinalize(this);
            Dispose(disposing: true);
        }
    }
}
