using System;

using Heirloom.OpenGLES;

namespace Heirloom.Drawing.OpenGL
{
    internal sealed class GLFramebuffer : IDisposable
    {
        private bool _isDisposed = false;

        #region Constructors

        public GLFramebuffer(OpenGLRenderContext context, GLTexture texture)
        {
            RenderingContext = context;
            Texture = texture;

            // Generate and bind framebuffer
            Handle = GL.GenFramebuffer();
            GL.BindFramebuffer(FramebufferTarget.Framebuffer, Handle);

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
        }

        ~GLFramebuffer()
        {
            Dispose(false);
        }

        #endregion

        #region Properties

        public uint Handle { get; }

        public GLTexture Texture { get; }

        public OpenGLRenderContext RenderingContext { get; }

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
