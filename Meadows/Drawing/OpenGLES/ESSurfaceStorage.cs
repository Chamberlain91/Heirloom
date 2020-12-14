using System;

namespace Meadows.Drawing.OpenGLES
{
    // holds textures for a surface
    internal sealed class ESSurfaceStorage : IDisposable
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
