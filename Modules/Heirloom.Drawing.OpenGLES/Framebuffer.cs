using System;

using Heirloom.OpenGLES;

namespace Heirloom.Drawing.OpenGLES
{
    internal sealed class Framebuffer : IDisposable
    {
        private bool _isDisposed = false;

        #region Constructors

        public Framebuffer(OpenGLRenderContext context, Texture texture)
        {
            Texture = texture;

            Handle = context.Invoke(() =>
            {
                // Generate and bind framebuffer
                var handle = GL.GenFramebuffer();
                GL.BindFramebuffer(FramebufferTarget.Framebuffer, handle);

                // Attach texture to framebuffer
                GL.FramebufferTexture2D(FramebufferTarget.Framebuffer, FramebufferAttachment.Color0,
                      TextureImageTarget.Texture2D, Texture.Handle, 0);

                // Ensure framebuffer is valid
                var status = GL.CheckFramebufferStatus(FramebufferTarget.Framebuffer);
                if (status != FramebufferStatus.Complete)
                {
                    throw new InvalidOperationException($"Unable to initialzie framebuffer surface. {status}");
                }

                // Unbind framebuffer
                GL.BindFramebuffer(FramebufferTarget.Framebuffer, 0);

                return handle;
            });
        }

        ~Framebuffer()
        {
            Dispose(false);
        }

        #endregion

        #region Properties

        public uint Handle { get; }

        public Texture Texture { get; }

        #endregion

        #region Dispose

        private void Dispose(bool disposeManaged)
        {
            if (!_isDisposed)
            {
                if (disposeManaged)
                {
                    // TODO: dispose managed objects.
                }

                // TODO: free unmanaged resources
                Console.WriteLine("WARN: Disposing Framebuffer! OpenGL Resource Not Deleted.");

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
