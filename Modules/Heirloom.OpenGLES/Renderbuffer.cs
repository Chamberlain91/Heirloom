using System;

namespace Heirloom.OpenGLES
{
    internal sealed class Renderbuffer : IDisposable
    {
        private bool _isDisposed = false;

        public readonly uint Handle;

        #region Constructors

        public Renderbuffer(Surface surface)
        {
            if (surface.Multisample < MultisampleQuality.None)
            {
                throw new ArgumentException("Sample count must be greater than zero.", nameof(surface.Multisample));
            }

            var samples = (int) surface.Multisample;
            Log.Debug($"Creating Renderbuffer ({surface.Size} w/ {samples} samples)");

            // Generate render buffer handle
            Handle = GL.GenRenderbuffer();

            // Configure render buffer for multisampled storage
            GL.BindRenderbuffer(Handle);
            GL.RenderbufferStorage(RenderbufferFormat.RGBA8, surface.Width, surface.Height, samples);
            GL.BindRenderbuffer(0);
        }

        ~Renderbuffer()
        {
            Dispose(false);
        }

        #endregion

        #region Dispose 

        private void Dispose(bool disposeManaged)
        {
            if (!_isDisposed)
            {
                if (disposeManaged)
                {
                    // Nothing
                }

                // Schedule for deletion on a GL thread.
                OpenGLGraphicsAdapter.Schedule(() =>
                {
                    Log.Debug($"[Dispose] Renderbuffer ({Handle})");
                    GL.DeleteRenderbuffer(Handle);
                });

                _isDisposed = true;
            }
        }

        public void Dispose()
        {
            Dispose(true);
            GC.SuppressFinalize(this);
        }

        #endregion
    }
}
