using System;

using Heirloom.OpenGLES;

namespace Heirloom.Drawing.OpenGLES
{
    internal sealed class Framebuffer : IDisposable
    {
        private bool _isDisposed = false;

        #region Constructors

        public Framebuffer(OpenGLRenderContext ctx, Surface surface)
        {
            Surface = surface;

            // Create texture buffer
            TextureBuffer = new TextureTarget(ctx, surface);

            // If surface is multisampled, create a multisample buffer too
            if (surface.Multisample != MultisampleQuality.None)
            {
                MultisampleBuffer = new MultisampleTarget(ctx, surface);
            }
        }

        ~Framebuffer()
        {
            Dispose(false);
        }

        #endregion

        #region Properties

        public Surface Surface { get; }

        public Texture Texture => TextureBuffer.Texture;

        public TextureTarget TextureBuffer { get; }

        public MultisampleTarget MultisampleBuffer { get; }

        public uint Version { get; private set; }

        #endregion

        public void Update(OpenGLRenderContext ctx)
        {
            ctx.Invoke(() =>
            {
                // Is this surface multisampled?
                if (MultisampleBuffer != null)
                {
                    // Get current draw framebuffer
                    var drawBuffer = (uint) GL.GetInteger(GetParameter.DrawFramebufferBinding);

                    // Set read and draw framebuffers for the blit
                    GL.BindFramebuffer(FramebufferTarget.ReadFramebuffer, MultisampleBuffer.Handle);
                    GL.BindFramebuffer(FramebufferTarget.DrawFramebuffer, TextureBuffer.Handle);

                    // Blit from multisampled buffer to texture buffer
                    GL.BlitFramebuffer(0, 0, Surface.Width, Surface.Height, 0, 0, Surface.Width, Surface.Height, FramebufferBlitMask.Color, FramebufferBlitFilter.Nearest);

                    // Restore draw framebuffer
                    GL.BindFramebuffer(FramebufferTarget.DrawFramebuffer, drawBuffer);
                }

                // Generate mips (also updates texture version)
                var texture = TextureBuffer.Texture;
                texture.GenerateMips(Surface.Version);
            });

            // We should be up to date with surface at this point
            Version = Surface.Version;
        }

        internal void Bind()
        {
            if (MultisampleBuffer == null)
            {
                // Render directly into texture buffer
                GL.BindFramebuffer(FramebufferTarget.DrawFramebuffer, TextureBuffer.Handle);
            }
            else
            {
                // Render into multisample buffer, later blitted into texture buffer for read operations
                GL.BindFramebuffer(FramebufferTarget.DrawFramebuffer, MultisampleBuffer.Handle);
            }
        }

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

        internal class MultisampleTarget
        {
            public uint Handle { get; private set; }

            public MultisampleTarget(OpenGLRenderContext ctx, Surface surface)
            {
                Console.WriteLine($"Creating multisample framebuffer.");

                Handle = ctx.Invoke(() =>
                {
                    // Generate framebuffer
                    var handle = GL.GenFramebuffer();
                    GL.BindFramebuffer(FramebufferTarget.Framebuffer, handle);

                    // Construct a multisampled render buffer
                    var renderBuffer = GL.GenRenderbuffer();
                    GL.BindRenderbuffer(renderBuffer);
                    GL.RenderbufferStorage(RenderbufferFormat.RGBA8, surface.Width, surface.Height, (int) surface.Multisample);
                    GL.BindRenderbuffer(0);

                    // Attach to framebuffer
                    GL.FramebufferRenderbuffer(FramebufferTarget.Framebuffer, FramebufferAttachment.Color0, renderBuffer);

                    // Ensure framebuffer is valid
                    var status = GL.CheckFramebufferStatus(FramebufferTarget.Framebuffer);
                    if (status != FramebufferStatus.Complete)
                    {
                        throw new InvalidOperationException($"Unable to initialize multisample framebuffer. {status}");
                    }

                    // Unbind framebuffer
                    GL.BindFramebuffer(FramebufferTarget.Framebuffer, 0);

                    return handle;
                });
            }
        }

        internal class TextureTarget
        {
            public uint Handle { get; private set; }

            public Texture Texture { get; private set; }

            public TextureTarget(OpenGLRenderContext ctx, Surface surface)
            {
                Texture = new Texture(ctx, surface.Size);

                Console.WriteLine($"Creating texture framebuffer.");

                // 
                Handle = ctx.Invoke(() =>
                {
                    // Generate framebuffer
                    var handle = GL.GenFramebuffer();
                    GL.BindFramebuffer(FramebufferTarget.Framebuffer, handle);

                    // Attach to framebuffer
                    GL.FramebufferTexture2D(FramebufferTarget.Framebuffer, FramebufferAttachment.Color0, TextureImageTarget.Texture2D, Texture.Handle, 0);

                    // Ensure framebuffer is valid
                    var status = GL.CheckFramebufferStatus(FramebufferTarget.Framebuffer);
                    if (status != FramebufferStatus.Complete)
                    {
                        throw new InvalidOperationException($"Unable to initialize texture framebuffer. {status}");
                    }

                    // Unbind framebuffer
                    GL.BindFramebuffer(FramebufferTarget.Framebuffer, 0);

                    return handle;
                });
            }
        }
    }
}
