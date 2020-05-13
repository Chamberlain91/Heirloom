using System;

using Heirloom.IO;

namespace Heirloom.OpenGLES
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
        public readonly TextureTarget MultisampleFBO;

        /// <summary>
        /// The framebuffer associated with the surface texture.
        /// </summary>
        public readonly TextureTarget TextureFBO;

        /// <summary>
        /// The texture associated with <see cref="TextureFBO"/>.
        /// </summary>
        public Texture Texture => TextureFBO.Texture;

        #region Constructors

        public Framebuffer(OpenGLGraphics graphics, Surface surface)
        {
            if (graphics is null)
            {
                throw new ArgumentNullException(nameof(graphics));
            }

            // Get the surface storage
            _storage = surface.Native as FramebufferStorage;
            if (_storage == null) { throw new InvalidOperationException($"Framebufer storage was unexpectedly null."); }

            _surface = surface ?? throw new ArgumentNullException(nameof(surface));

            // Construct target
            TextureFBO = new TextureTarget(graphics, _storage.Texture);

            if (_storage.HasMultisampleTarget)
            {
                // Construct msaa target
                MultisampleFBO = new TextureTarget(graphics, _storage.MultisampleTexture);
                // RenderbufferFBO = new RenderbufferTarget(graphics, _storage.Renderbuffer);
            }
        }

        ~Framebuffer()
        {
            Dispose(false);
        }

        #endregion

        #region Properties

        /// <summary>
        /// Does this framebuffer support multisampling?
        /// </summary>
        public bool HasMultisampleTarget => _storage.HasMultisampleTarget;

        /// <summary>
        /// Is this framebuffer out of date?
        /// </summary>
        public bool IsDirty => _version != _surface.Version;

        #endregion

        #region Bind (Draw & Read)

        /// <summary>
        /// Binds the framebuffer for drawing.
        /// </summary>
        internal void BindToDraw()
        {
            if (HasMultisampleTarget)
            {
                // Render into multisample buffer, later blitted into texture buffer for read operations
                GL.BindFramebuffer(FramebufferTarget.DrawFramebuffer, MultisampleFBO.Handle);
            }
            else
            {
                // Render directly into texture buffer
                GL.BindFramebuffer(FramebufferTarget.DrawFramebuffer, TextureFBO.Handle);
            }
        }

        /// <summary>
        /// Binds the framebuffer for reading.
        /// </summary>
        internal void BindToRead()
        {
            // Transfer renderbuffer FBO to texture FBO
            BlitToTexture();

            // Read from texture
            GL.BindFramebuffer(FramebufferTarget.ReadFramebuffer, TextureFBO.Handle);
        }

        #endregion

        /// <summary>
        /// Blits the renderbuffer to the texture (if needed/exists).
        /// </summary>
        public void BlitToTexture()
        {
            if (IsDirty)
            {
                // Copy and resolve renderbuffer (if exists) to texture
                if (HasMultisampleTarget)
                {
                    // Get current draw framebuffer
                    var drawBuffer = (uint) GL.GetInteger(GetParameter.DrawFramebufferBinding);

                    // Set read and draw framebuffers for the blit
                    GL.BindFramebuffer(FramebufferTarget.ReadFramebuffer, MultisampleFBO.Handle);
                    GL.BindFramebuffer(FramebufferTarget.DrawFramebuffer, TextureFBO.Handle);

                    // Blit from multisampled buffer to texture buffer
                    GL.BlitFramebuffer(0, 0, Texture.Width, Texture.Height,
                                       0, 0, Texture.Width, Texture.Height,
                                       FramebufferBlitMask.Color, FramebufferBlitFilter.Nearest);

                    // Restore draw framebuffer
                    GL.BindFramebuffer(FramebufferTarget.DrawFramebuffer, drawBuffer);
                }

                // We should be up to date with the surface now
                Texture.Version = _surface.Version;
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

                // Dispose framebuffers
                MultisampleFBO?.Dispose();
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

            private readonly OpenGLGraphics _graphics;

            public Renderbuffer Renderbuffer;

            public uint Handle;

            #region Constructor

            public RenderbufferTarget(OpenGLGraphics graphics, Renderbuffer renderbuffer)
            {
                _graphics = graphics;

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
                    _graphics.Invoke(() =>
                    {
                        Log.Debug($"[Dispose] Framebuffer MSAA ({Handle})");
                        GL.DeleteFramebuffer(Handle);
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

        internal sealed class TextureTarget : IDisposable
        {
            private bool _isDisposed = false;

            private readonly OpenGLGraphics _graphics;

            public Texture Texture;

            public uint Handle;

            #region Constructor

            public TextureTarget(OpenGLGraphics graphics, Texture texture)
            {
                _graphics = graphics;

                Texture = texture;

                // Generate framebuffer
                Handle = GL.GenFramebuffer();
                GL.BindFramebuffer(FramebufferTarget.Framebuffer, Handle);

                // Attach to framebuffer
                GL.FramebufferTexture2D(FramebufferTarget.Framebuffer, FramebufferAttachment.Color0, (TextureImageTarget) texture.Target, Texture.Handle, 0);

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
                    _graphics.Invoke(() =>
                    {
                        Log.Debug($"[Dispose] Framebuffer Texture ({Handle})");
                        GL.DeleteFramebuffer(Handle);
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
}
