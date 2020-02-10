using System;

using Heirloom.OpenGLES;

namespace Heirloom.Drawing.OpenGLES
{
    internal sealed class Framebuffer : IDisposable
    {
        private readonly FramebufferStorage _storage;
        private readonly Surface _surface;
        private uint _version;

        private bool _isDisposed = false;

        /// <summary>
        /// The framebuffer associated with the multisampled renderbuffer.
        /// </summary>
        public readonly RenderbufferTarget RenderbufferFBO;

        /// <summary>
        /// The framebuffer associated with the surface texture.
        /// </summary>
        public readonly TextureTarget TextureFBO;

        #region Constructors

        public Framebuffer(Surface surface)
        {
            _storage = surface.Native as FramebufferStorage;
            _surface = surface;

            // Construct texture FBO
            TextureFBO = new TextureTarget(_storage.Texture);

            if (_storage.HasRenderbuffer)
            {
                // Construct renderbuffer FBO (MSAA)
                RenderbufferFBO = new RenderbufferTarget(_storage.Renderbuffer);
            }
        }

        ~Framebuffer()
        {
            Dispose(false);
        }

        #endregion

        /// <summary>
        /// Does this framebuffer support multisampling?
        /// </summary>
        public bool HasRenderbuffer => RenderbufferFBO != null;

        /// <summary>
        /// Binds the framebuffer.
        /// </summary>
        internal void Bind()
        {
            if (HasRenderbuffer)
            {
                // Render into multisample buffer, later blitted into texture buffer for read operations
                GL.BindFramebuffer(FramebufferTarget.DrawFramebuffer, RenderbufferFBO.Handle);
            }
            else
            {
                // Render directly into texture buffer
                GL.BindFramebuffer(FramebufferTarget.DrawFramebuffer, TextureFBO.Handle);
            }
        }

        /// <summary>
        /// Blits the renderbuffer to the texture (if needed/exists) and updates the texture.
        /// </summary>
        public void BlitAndUpdate()
        {
            if (_version != _surface.Version)
            {
                // Copy and resolve renderbuffer (if exists) to texture
                if (HasRenderbuffer)
                {
                    // Get current draw framebuffer
                    var drawBuffer = (uint) GL.GetInteger(GetParameter.DrawFramebufferBinding);

                    // Set read and draw framebuffers for the blit
                    GL.BindFramebuffer(FramebufferTarget.ReadFramebuffer, RenderbufferFBO.Handle);
                    GL.BindFramebuffer(FramebufferTarget.DrawFramebuffer, TextureFBO.Handle);

                    // Blit from multisampled buffer to texture buffer
                    GL.BlitFramebuffer(0, 0, TextureFBO.Texture.Width, TextureFBO.Texture.Height,
                                       0, 0, TextureFBO.Texture.Width, TextureFBO.Texture.Height,
                                       FramebufferBlitMask.Color, FramebufferBlitFilter.Nearest);

                    // Restore draw framebuffer
                    GL.BindFramebuffer(FramebufferTarget.DrawFramebuffer, drawBuffer);
                }

                // Generate mips (also updates texture version)
                var texture = TextureFBO.Texture;
                texture.Update(_surface);

                // We should be up to date with the surface now
                _version = _surface.Version;
            }
        }

        #region Dispose

        private void Dispose(bool disposeManaged)
        {
            if (!_isDisposed)
            {
                if (disposeManaged)
                {
                    // Nothing
                }

                // 
                RenderbufferFBO.Dispose();
                TextureFBO.Dispose();

                _isDisposed = true;
            }
        }

        public void Dispose()
        {
            Dispose(true);
            GC.SuppressFinalize(this);
        }

        #endregion

        internal sealed class RenderbufferTarget : IDisposable
        {
            private bool _isDisposed = false;

            public Renderbuffer Renderbuffer;

            public uint Handle;

            #region Constructor

            public RenderbufferTarget(Renderbuffer renderbuffer)
            {
                Renderbuffer = renderbuffer;

                // Generate framebuffer
                Handle = GL.GenFramebuffer();
                GL.BindFramebuffer(FramebufferTarget.Framebuffer, Handle);

                // Attach to framebuffer
                GL.FramebufferRenderbuffer(FramebufferTarget.Framebuffer, FramebufferAttachment.Color0, Renderbuffer.Handle);

                // Ensure framebuffer is valid
                var status = GL.CheckFramebufferStatus(FramebufferTarget.Framebuffer);
                if (status != FramebufferStatus.Complete)
                {
                    throw new InvalidOperationException($"Unable to initialize multisample framebuffer. {status}");
                }

                // Unbind framebuffer
                GL.BindFramebuffer(FramebufferTarget.Framebuffer, 0);
            }

            ~RenderbufferTarget()
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
                    OpenGLGraphicsAdapter.Schedule(() => GL.DeleteFramebuffer(Handle));

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

        internal sealed class TextureTarget : IDisposable
        {
            private bool _isDisposed = false;

            public Texture Texture;

            public uint Handle;

            #region Constructor

            public TextureTarget(Texture texture)
            {
                Texture = texture;

                // Generate framebuffer
                Handle = GL.GenFramebuffer();
                GL.BindFramebuffer(FramebufferTarget.Framebuffer, Handle);

                // Attach to framebuffer
                GL.FramebufferTexture2D(FramebufferTarget.Framebuffer, FramebufferAttachment.Color0, TextureImageTarget.Texture2D, Texture.Handle, 0);

                // Ensure framebuffer is valid
                var status = GL.CheckFramebufferStatus(FramebufferTarget.Framebuffer);
                if (status != FramebufferStatus.Complete)
                {
                    throw new InvalidOperationException($"Unable to initialize multisample framebuffer. {status}");
                }

                // Unbind framebuffer
                GL.BindFramebuffer(FramebufferTarget.Framebuffer, 0);
            }

            ~TextureTarget()
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
                    OpenGLGraphicsAdapter.Schedule(() => GL.DeleteFramebuffer(Handle));

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
}
