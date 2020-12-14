using System;

namespace Meadows.Drawing.OpenGLES
{
    // holds the framebuffers for a surface
    internal sealed class ESSurface : IDisposable
    {
        private readonly ESSurfaceStorage _storage;
        private readonly Surface _surface;

        private uint _version;

        private bool _isDisposed;

        /// <summary>
        /// The framebuffer associated with the multisampled renderbuffer.
        /// </summary>
        public readonly Framebuffer MultisampleFramebuffer;

        /// <summary>
        /// The framebuffer associated with the surface texture.
        /// </summary>
        public readonly Framebuffer TextureFramebuffer;

        #region Constructors

        public ESSurface(ESGraphicsContext context, Surface surface)
        {
            if (context is null) { throw new ArgumentNullException(nameof(context)); }

            // Store a reference to the surface
            _surface = surface ?? throw new ArgumentNullException(nameof(surface));

            // Get surface storage (the backing ES textures)
            _storage = context.Backend.GetNativeObject<ESSurfaceStorage>(surface);
            if (_storage == null) { throw new InvalidOperationException($"Framebufer storage was unexpectedly null."); }

            // If surface is multisampled...
            if (_storage.HasMultisampleTarget)
            {
                // Construct the multisample configuration
                MultisampleFramebuffer = new Framebuffer(context, _storage.MultisampleTexture, _storage.StencilTexture);
                TextureFramebuffer = new Framebuffer(context, _storage.Texture);
            }
            else
            {
                // Construct plain texture configuration
                TextureFramebuffer = new Framebuffer(context, _storage.Texture, _storage.StencilTexture);
            }
        }

        ~ESSurface()
        {
            Dispose(false);
        }

        #endregion

        /// <summary>
        /// The texture associated with <see cref="TextureFramebuffer"/>.
        /// </summary>
        public ESTexture Texture => _storage.Texture;

        /// <summary>
        /// Does this framebuffer support multisampling?
        /// </summary>
        public bool HasMultisampleTarget => _storage.HasMultisampleTarget;

        /// <summary>
        /// Is this framebuffer out of date?
        /// </summary>
        public bool IsDirty => _version != _surface.Version;

        #region Bind (Draw & Read)

        /// <summary>
        /// Binds the framebuffer for drawing.
        /// </summary>
        internal void BindForDraw()
        {
            if (_storage.HasMultisampleTarget)
            {
                // Render into multisample buffer, later blitted into texture buffer for read operations
                GLES.BindFramebuffer(FramebufferTarget.DrawFramebuffer, MultisampleFramebuffer.Handle);
            }
            else
            {
                // Render directly into texture buffer
                GLES.BindFramebuffer(FramebufferTarget.DrawFramebuffer, TextureFramebuffer.Handle);
            }
        }

        internal void BindForRead()
        {
            // Transfer renderbuffer FBO to texture FBO
            BlitToTexture();

            // Read from texture
            GLES.BindFramebuffer(FramebufferTarget.ReadFramebuffer, TextureFramebuffer.Handle);
        }

        #endregion

        #region Blit

        /// <summary>
        /// Blits the renderbuffer to the texture (if needed/exists).
        /// </summary>
        public void BlitToTexture()
        {
            if (IsDirty)
            {
                // Copy and resolve renderbuffer (if exists) to texture
                if (_storage.HasMultisampleTarget)
                {
                    // Get current draw framebuffer
                    var drawBuffer = (uint) GLES.GetInteger(GetParameter.DrawFramebufferBinding);

                    // Set read and draw framebuffers for the blit
                    GLES.BindFramebuffer(FramebufferTarget.ReadFramebuffer, MultisampleFramebuffer.Handle);
                    GLES.BindFramebuffer(FramebufferTarget.DrawFramebuffer, TextureFramebuffer.Handle);

                    // Blit from multisampled buffer to texture buffer
                    GLES.BlitFramebuffer(0, 0, Texture.Width, Texture.Height,
                                         0, 0, Texture.Width, Texture.Height,
                                         FramebufferBlitMask.Color, FramebufferBlitFilter.Nearest);

                    // Restore draw framebuffer
                    GLES.BindFramebuffer(FramebufferTarget.DrawFramebuffer, drawBuffer);
                }

                // We should be up to date with the surface now
                Texture.Version = _surface.Version;
                _version = _surface.Version;
            }
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

                // Dispose framebuffers
                MultisampleFramebuffer?.Dispose();
                TextureFramebuffer.Dispose();

                _isDisposed = true;
            }
        }

        public void Dispose()
        {
            Dispose(true);
            GC.SuppressFinalize(this);
        }

        #endregion

        internal sealed class Framebuffer : IDisposable
        {
            private readonly ESGraphicsContext _context;

            private bool _isDisposed;

            public uint Handle;

            #region Constructor

            public Framebuffer(ESGraphicsContext context, ESTexture texture, ESTexture stencilTexture = null)
            {
                _context = context;

                // Generate framebuffer
                Handle = GLES.GenFramebuffer();
                GLES.BindFramebuffer(FramebufferTarget.Framebuffer, Handle);

                // Attach color texture
                GLES.FramebufferTexture2D(FramebufferTarget.Framebuffer, FramebufferAttachment.Color0, (TextureImageTarget) texture.Target, texture.Handle, 0);

                if (stencilTexture != null)
                {
                    // Attach stencil buffer
                    GLES.FramebufferTexture2D(FramebufferTarget.Framebuffer, FramebufferAttachment.Stencil, (TextureImageTarget) stencilTexture.Target, stencilTexture.Handle, 0);
                }

                // Ensure framebuffer is valid
                var status = GLES.CheckFramebufferStatus(FramebufferTarget.Framebuffer);
                if (status != FramebufferStatus.Complete)
                {
                    throw new InvalidOperationException($"Unable to initialize framebuffer. {status}");
                }

                // Unbind framebuffer
                GLES.BindFramebuffer(FramebufferTarget.Framebuffer, 0);
            }

            ~Framebuffer()
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

                    // Schedule for deletion on associated GL thread
                    // todo: if context is disposed of, this might pose a problem
                    _context.Invoke(() =>
                    {
                        Log.Debug($"[Dispose] Framebuffer Texture ({Handle})");
                        GLES.DeleteFramebuffer(Handle);
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
