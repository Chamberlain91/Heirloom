using System;
using System.Runtime.CompilerServices;
using System.Threading;

using Meadows.Mathematics;
using Meadows.Utilities;

namespace Meadows.Drawing.OpenGLES
{
    public abstract class ESGraphicsContext : GraphicsContext
    {
        private readonly ConsumerThread _thread;
        private bool _isGLESInitialized;
        private bool _isThreadRunning;

        private byte _stencilReference;

        private ESTexture _texture;
        private bool _textureDirty;

        private ESBatch _batch;
        private ESAtlas _atlas;

        private DeviceCapabilities _capabilities;

        #region Constructor

        protected ESGraphicsContext(Screen screen)
            : base(screen)
        {
            _thread = new ConsumerThread($"GLES Context Thread ({Id})");
            _thread.InvokeLater(() =>
            {
                // Assign the GL context to this thread.
                MakeCurrent();

                // Detect Device Capabilities
                _capabilities = new DeviceCapabilities
                {
                    MaxTextureSize = GLES.GetInteger(GetParameter.MaxTextureSize)
                };

                // Initialize batch and atlas systems
                _batch = new ESBatchStreaming(this);
                _atlas = new ESAtlasSimple(this);

                // Enable required GL features
                GLES.Enable(EnableCap.ScissorTest);
                GLES.Enable(EnableCap.Blend);

                // Mark context as initialized
                _isGLESInitialized = true;
            });

            // When thread exits, context is no longer initialized
            _thread.Exited += () => _isGLESInitialized = false;
        }

        #endregion

        #region GL Context Methods

        /// <summary>
        /// Launches the dedicated OpenGL thread associated with this context.
        /// </summary>
        protected void StartThread()
        {
            if (!_isThreadRunning)
            {
                // Mark thread as running
                _isThreadRunning = true;

                // Begin consumer thread
                _thread.Start();

                // Wait For GL to truly be ready
                SpinWait.SpinUntil(() => _isGLESInitialized);
            }
            else
            {
                throw new InvalidOperationException("OpenGL thread already running.");
            }
        }

        protected abstract void MakeCurrent();

        #region Invoke

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        protected internal void Invoke(Action action, bool blocking = true)
        {
            if (!_isThreadRunning) { throw new InvalidOperationException("Unable to invoke, context thread not started."); }
            if (blocking) { _thread.Invoke(action); }
            else { _thread.InvokeLater(action); }
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        protected internal T Invoke<T>(Func<T> action)
        {
            if (!_isThreadRunning) { throw new InvalidOperationException("Unable to invoke, context thread not started."); }
            return _thread.Invoke(action);
        }

        #endregion

        #endregion

        public override void Clear(Color color)
        {
            _batch.Clear(color);
        }

        public override void Draw(Texture texture, Rectangle uvRect, Mesh mesh, Matrix matrix)
        {
            // Request es texture information
            RequestTextureInformation(texture, out var atlasTexture, out var atlasRect);

            // Map UV rect into atlas rect
            MapAndEncodeUV(texture, ref uvRect, in atlasRect);

            // Inconsistent texture, mark to rebind
            if (_texture != atlasTexture)
            {
                Flush();

                _texture = atlasTexture;
                _textureDirty = true;
            }

            // If the shader state has changed, we need to flush.
            // if (_shader.IsDirty) { Flush(); }

            // While unable to submit to batch, flush pending operations.
            while (!_batch.Submit(mesh, atlasRect, matrix, Color)) { Flush(); }
        }

        #region Stencil Methods

        public override void ClearStencil()
        {
            // stencil enable = false
            // stencil write = false
            // color write = true

            Invoke(() =>
            {
                // todo: make part of batch queue (may optimize)?

                // Commit any pending operations
                Flush();

                GLES.Disable(EnableCap.StencilTest);
                GLES.SetStencilMask(0); // todo: probably not needed since is disabled
                GLES.SetColorMask(true);
            });
        }

        public override void BeginStencil()
        {
            // stencil enable = true
            // stencil write = true
            // color write = false

            Invoke(() =>
            {
                // todo: make part of batch queue (may optimize)?

                // Commit any pending operations
                Flush();

                GLES.Enable(EnableCap.StencilTest);
                GLES.SetStencilMask(0xFF);
                GLES.SetColorMask(false);

                // Update stencil reference number
                _stencilReference++;
                if (_stencilReference == byte.MaxValue)
                {
                    _stencilReference = 0;
                }

                // Update stencil reference
                GLES.StencilOperation(StencilOperation.Keep, StencilOperation.Keep, StencilOperation.Replace);
                GLES.StencilFunction(StencilFunction.Always, _stencilReference, 0xFF);
            });

            throw new NotImplementedException();
        }

        public override void EndStencil()
        {
            // stencil write = false
            // color write = true

            // todo: validate BeginStencil() has been called first

            Invoke(() =>
            {
                // todo: make part of batch queue (may optimize)?

                // Commit any pending operations
                Flush();

                GLES.SetStencilMask(0x0);
                GLES.SetColorMask(true);

                // Update stencil reference
                GLES.StencilOperation(StencilOperation.Keep, StencilOperation.Keep, StencilOperation.Keep);
                GLES.StencilFunction(StencilFunction.Equal, _stencilReference, 0xFF);
            });

            throw new NotImplementedException();
        }

        #endregion

        public override unsafe Image GrabPixels(IntRectangle region)
        {
            return Invoke(() =>
            {
                // Ensure all render jobs are submitted to the GPU
                Flush();

                // If the current surface is the default surface.
                if (Surface == Screen.Surface)
                {
                    // The current surface is the default (ie, window) framebuffer.
                    GLES.BindFramebuffer(FramebufferTarget.ReadFramebuffer, 0);
                }
                else
                {
                    // Get and wait for the surface to be ready
                    var esSurface = GetContextNativeObject<ESSurface>(Surface);
                    esSurface.BindForRead();
                }

                // Grab pixels from read buffer
                var pixels = GLES.ReadPixels(region.X, region.Y, region.Width, region.Height);
                fixed (uint* ptrPixels = pixels)
                {
                    // Copy grabbed pixels to image
                    var image = new Image(region.Width, region.Height);
                    Image.Copy((ColorBytes*) ptrPixels, region.Width, (IntVector.Zero, region.Size), image, IntVector.Zero);
                    return image;
                }
            });
        }

        protected override void Flush(bool block = false)
        {
            Invoke(() =>
            {
                if (StateFlags != 0) // state has been changed!
                {
                    // Update each aspect if the respective flag is set
                    if (StateFlags.HasFlag(StateDirtyFlags.Viewport)) { UpdateViewport(); }
                    if (StateFlags.HasFlag(StateDirtyFlags.Surface)) { UpdateSurface(); }
                    if (StateFlags.HasFlag(StateDirtyFlags.Shader)) { UpdateShader(); }
                    if (StateFlags.HasFlag(StateDirtyFlags.Blending)) { UpdateBlending(); }
                    if (StateFlags.HasFlag(StateDirtyFlags.Interpolation)) { UpdateInterpolation(); }
                    if (StateFlags.HasFlag(StateDirtyFlags.Camera)) { UpdateCamera(); }

                    // Clear state change flags
                    StateFlags = 0;
                }

                // Commit changes to atlas
                _atlas.Commit();

                // todo: a more sophisticated way to use texture units?
                if (_textureDirty)
                {
                    GLES.ActiveTexture(0);
                    GLES.BindTexture(TextureTarget.Texture2D, _texture.Handle);
                    _textureDirty = false;
                }

                // todo: process shader uniforms

                // Commit drawing operations
                _batch.Commit();

                // Update the surface version number (marked dirty)
                IncrementVersion(Surface);

                // Commit GL operations
                if (block) { GLES.Finish(); }
                else { GLES.Flush(); }
            });

            void UpdateViewport()
            {
                var x = Viewport.X;
                var y = Viewport.Y;
                var w = Viewport.Width;
                var h = Viewport.Height;

                // Flip to match the top-left coordiantes
                y = Surface.Height - h - y;

                // Set GL Viewport
                GLES.SetViewport(x, y, w, h);
                GLES.SetScissor(x, y, w, h);
            }

            void UpdateSurface()
            {
                throw new NotImplementedException();
            }

            void UpdateShader()
            {
                throw new NotImplementedException();
            }

            void UpdateBlending()
            {
                throw new NotImplementedException();
            }

            void UpdateInterpolation()
            {
                throw new NotImplementedException();
            }

            void UpdateCamera()
            {
                throw new NotImplementedException();
            }
        }

        protected struct DeviceCapabilities
        {
            public int MaxTextureSize { get; init; }
        }

        #region Texture System (Atlas & Units)

        private void MapAndEncodeUV(Texture texture, ref Rectangle uvRect, in Rectangle atlasRect)
        {
            // Map incoming uv region to the atlas region
            uvRect.X = atlasRect.X + (uvRect.X * atlasRect.Width);
            uvRect.Y = atlasRect.Y + (uvRect.Y * atlasRect.Height);
            uvRect.Width *= atlasRect.Width;
            uvRect.Height *= atlasRect.Height;

            // A simple meta data encoding trick. Because the UV rectangle describes a
            // non-negative zero-to-one domain, we can use negative values to encode
            // special meaning into each component of the rect.
            // - X component encodes interpolation mode
            // - Y component encodes repeat mode
            // - Z component has no encoding currently.
            // - W component encodes vertical flip (to compensate for framebuffers).
            uvRect.X -= (float) InterpolationMode;
            uvRect.Y -= (float) texture.Repeat;
        }

        private void RequestTextureInformation(Texture texture, out ESTexture atlasTexture, out Rectangle atlasRect)
        {
            if (texture is Image image)
            {
                while (!_atlas.Submit(image, out atlasTexture, out atlasRect))
                {
                    // Submit pending operations
                    Flush();

                    // Evict atlas, we need space for this texture
                    _atlas.Evict();
                }
            }
            else if (texture is Surface surface)
            {
                if (surface.Screen != null) { throw new ArgumentException("Unable to use screen bound surface as a texture."); }

                // Get offscreen surface texture
                var esSurface = GetContextNativeObject<ESSurface>(surface);
                if (esSurface.IsDirty)
                {
                    Invoke(() => esSurface.BlitToTexture());
                }

                // 
                atlasTexture = esSurface.Texture;
                atlasRect = (0, 0, 1, -1);
            }
            else
            {
                // fatal error?!
                throw new ArgumentException("Texture object was not of a known type.", nameof(texture));
            }
        }

        #endregion

        #region Native Object Access

        protected override object GenerateGlobalNativeObject(NativeResource resource)
        {
            return Invoke<object>(() => resource switch
            {
                Surface surface => new ESSurfaceStorage(surface),
                Image image => new ESTexture(image.Size),

                _ => throw new InvalidOperationException($"Unable to generate native reresentation of {resource}"),
            });
        }

        protected override object GenerateContextNativeObject(NativeResource resource)
        {
            return Invoke<object>(() =>
            {
                return resource switch
                {
                    Surface surface => new ESSurface(this, surface),

                    _ => throw new InvalidOperationException($"Unable to generate native reresentation of {resource}"),
                };
            });
        }

        #endregion

        protected internal static void InvokeOnSomeThread(Action action)
        {
            // todo: keep track of alive and disposed contexts
            throw new NotImplementedException("INVOKE ON SOME GL THREAD");
        }
    }
}
