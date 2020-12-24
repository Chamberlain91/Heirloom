using System;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using System.Threading;

using Meadows.Mathematics;
using Meadows.Utilities;

namespace Meadows.Drawing.OpenGLES
{
    public abstract class ESGraphicsContext : GraphicsContext
    {
        private readonly ConsumerThread _thread;
        private bool _isThreadRunning;

        private byte _stencilReference;

        private ESTexture _atlasTexture;
        private bool _atlasTextureDirty;

        private readonly Dictionary<int, UniformData> _modifiedUniforms = new();

        private ESBatch _batch;
        private ESAtlas _atlas;

        #region Constructor

        internal ESGraphicsContext(ESGraphicsBackend backend, Screen screen)
            : base(backend, screen)
        {
            _thread = new ConsumerThread($"GLES Context Thread ({Id})");
            _thread.InvokeLater(() =>
            {
                // Assign the GL context to this thread.
                MakeCurrent();

                // Detect Device Capabilities
                Capabilities = new DeviceCapabilities
                {
                    MaxTextureSize = GLES.GetInteger(GetParameter.MaxTextureSize)
                };

                // Initialize batch and atlas systems
                _batch = new ESBatchHybrid(this);
                _atlas = new ESAtlasPacker(this);

                // Enable required GL features
                GLES.Enable(EnableCap.ScissorTest);
                GLES.Enable(EnableCap.Blend);

                // 
                Initialize();
            });

            // When thread exits, context is no longer initialized
            _thread.Exited += () => IsInitialized = false;
        }

        #endregion

        internal DeviceCapabilities Capabilities { get; private set; }

        protected override bool HasPendingWork => _batch.IsDirty;

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
                SpinWait.SpinUntil(() => IsInitialized);
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

        public override void Draw(Mesh mesh, Texture texture, Rectangle uvRect, Matrix matrix)
        {
            // todo: there is a logic error with the state changing flags...
            //       set blending, draw, set blending, draw should flush on the second blending?

            // if (StateFlags != 0) { Flush(); } 
            // if (_modifiedUniforms.Count > 0) { Flush(); }

            // Request es texture information
            RequestTextureInformation(texture, out var atlasTexture, out var atlasRect);

            // Map UV rect into atlas rect
            MapAndEncodeUV(texture, ref uvRect, in atlasRect);

            // Inconsistent atlas texture, mark to rebind.
            if (_atlasTexture != atlasTexture)
            {
                // We have work dependant on a prior texture, flush.
                if (_atlasTexture != null) { Flush(); }

                // Change atlas texture
                _atlasTexture = atlasTexture;
                _atlasTextureDirty = true;
            }

            // Combine composite with (proj * camera * world * object)
            Matrix.Multiply(CompositeMatrix, in matrix, ref matrix);

            // While unable to submit to batch, flush pending operations.
            while (!_batch.Submit(mesh, uvRect, matrix, Color))
            {
                Flush();
            }
        }

        public override void SetUniform<T>(string name, T value)
        {
            var uniform = Shader.GetUniform(name);

            // Update uniform
            _modifiedUniforms[uniform.Location] = new UniformData
            {
                Uniform = uniform,
                Value = value
            };
        }

        #region Stencil Methods

        public override void ClearStencil()
        {
            // stencil enable = false
            // stencil write = false
            // color write = true

            Invoke(() =>
            {
                // Commit any pending operations
                Flush();

                // Disable stencil testing
                GLES.Disable(EnableCap.StencilTest);

                // Disable stencil writes and enable color writes
                GLES.SetStencilMask(0x0);
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
                // Commit any pending operations
                Flush();

                // Enable stencil testing
                GLES.Enable(EnableCap.StencilTest);

                // Enable stencil writes, disable color writes
                GLES.SetStencilMask(0xFF);
                GLES.SetColorMask(false);

                // Update stencil reference number
                _stencilReference++;
                if (_stencilReference == byte.MaxValue)
                {
                    Log.Debug("Stencil Wrap");

                    // 
                    GLES.SetClearStencil(0xFF);
                    GLES.Clear(OpenGLES.ClearMask.Stencil);

                    // Set to zer0
                    _stencilReference = 0x1;
                }

                // Update stencil reference
                GLES.StencilOperation(StencilOperation.Keep, StencilOperation.Keep, StencilOperation.Replace);
                GLES.StencilFunction(StencilFunction.Always, _stencilReference, 0xFF);
            });
        }

        public override void EndStencil()
        {
            // stencil write = false
            // color write = true

            // todo: validate BeginStencil() has been called first

            Invoke(() =>
            {
                // Commit any pending operations
                Flush();

                // Disable stencil writes and enable color writes
                GLES.SetStencilMask(0x0);
                GLES.SetColorMask(true);

                // Update stencil reference
                GLES.StencilFunction(StencilFunction.Equal, _stencilReference, 0xFF);
                GLES.StencilOperation(StencilOperation.Keep, StencilOperation.Keep, StencilOperation.Keep);
            });
        }

        #endregion

        public override unsafe Image GrabPixels(IntRectangle region)
        {
            return Invoke(() =>
            {
                // Ensure all render jobs are submitted to the GPU
                Flush(block: false);

                // Validate region size is at least 1x1
                if (region.Width == 0 || region.Height == 0)
                {
                    throw new InvalidOperationException("Unable to grab pixels, region size is zero.");
                }

                // Flip Y axis to correct top-right to bottom-left coordinates
                region.Y = Surface.Height - region.Y - region.Height;

                // If the current surface is the default surface.
                if (Surface == Screen.Surface)
                {
                    // The current surface is the default (ie, window) framebuffer.
                    GLES.BindFramebuffer(FramebufferTarget.ReadFramebuffer, 0);
                }
                else
                {
                    // Get and wait for the surface to be ready
                    var esSurface = GetNativeObject<ESSurface>(Surface);
                    esSurface.BindForRead();
                }

                // Grab pixels from read buffer
                var pixels = GLES.ReadPixels(region.X, region.Y, region.Width, region.Height);
                fixed (uint* ptrPixels = pixels)
                {
                    // Copy grabbed pixels to image
                    var image = new Image(region.Width, region.Height);
                    Image.Copy((ColorBytes*) ptrPixels, region.Width, (IntVector.Zero, region.Size), image, IntVector.Zero);
                    image.Flip(Axis.Vertical); // because surface
                    return image;
                }
            });
        }

        protected override void Flush(bool block = false)
        {
            if (HasPendingWork)
            {
                Invoke(() =>
                {
                    // 
                    Performance.NotifyBatch();

                    if (StateFlags != 0) // state has been changed!
                    {
                        // Update each aspect if the respective flag is set
                        if (StateFlags.HasFlag(StateDirtyFlags.Blending))
                        {
                            StateFlags &= ~StateDirtyFlags.Blending;
                            UpdateBlending();
                        }

                        if (StateFlags.HasFlag(StateDirtyFlags.Viewport))
                        {
                            StateFlags &= ~StateDirtyFlags.Viewport;
                            UpdateViewport();
                        }

                        if (StateFlags.HasFlag(StateDirtyFlags.Surface))
                        {
                            StateFlags &= ~StateDirtyFlags.Surface;
                            UpdateSurface();
                        }

                        if (StateFlags.HasFlag(StateDirtyFlags.Shader))
                        {
                            StateFlags &= ~StateDirtyFlags.Shader;
                            UpdateShader();
                        }
                    }

                    // Commit changes to atlas
                    _atlas.Commit();

                    // todo: a more sophisticated way to use texture units?
                    if (_atlasTextureDirty)
                    {
                        GLES.ActiveTexture(0);
                        GLES.BindTexture(TextureTarget.Texture2D, _atlasTexture.Handle);
                        _atlasTextureDirty = false;
                    }

                    // Update uniforms
                    // todo: is this dictionary loop efficient enough?
                    foreach (var data in _modifiedUniforms.Values) { UpdateUniform(data.Uniform, data.Value); }
                    _modifiedUniforms.Clear();

                    // Commit drawing operations
                    _batch.Commit();

                    // Update the surface version number (marked dirty)
                    IncrementVersion(Surface);

                    // Commit GL operations
                    if (block) { GLES.Finish(); }
                    else { GLES.Flush(); }
                });
            }

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

                // Compute projection matrix to convert from pixel space to normalized coordinates
                var projectionMatrix = Matrix.RectangleProjection(0, 0, Viewport.Width, Viewport.Height);
                SetUniform("uProjection", projectionMatrix);
            }

            void UpdateSurface()
            {
                if (Surface.Screen == Screen)
                {
                    // The current surface is the default (ie, window) framebuffer.
                    GLES.BindFramebuffer(FramebufferTarget.DrawFramebuffer, 0);
                }
                else if (Surface.Screen != null)
                {
                    // Unable to read from or write to this surface, its a non-sharable resource.
                    throw new InvalidOperationException($"Unable to assign a screen bound surface. Belongs to a different {nameof(GraphicsContext)} context.");
                }
                else
                {
                    // Bind the framebuffer associated with this surface for writing.
                    var esSurface = GetNativeObject<ESSurface>(Surface);
                    esSurface.BindForDraw();
                }
            }

            void UpdateShader()
            {
                // Begin use of this shader program
                var esShader = Backend.GetNativeObject<ESShaderProgram>(Shader);
                GLES.UseProgram(esShader.Handle);

                // Bind uniform buffers
                foreach (var block in esShader.Blocks)
                {
                    var buffer = ESGraphicsBackend.Current.UniformBuffers[block.Name];
                    GLES.BindBufferBase(BufferTarget.UniformBuffer, block.Index, buffer.Handle);
                }
            }

            void UpdateBlending()
            {
                // todo: ensure blending modes are consistent and standardized for the kind of alpha (ie, "pre-multiplied" alpha or not)

                switch (BlendingMode)
                {
                    default:
                        throw new InvalidOperationException("Unable to set unknown blend mode.");

                    case BlendingMode.Opaque:
                        GLES.SetBlendEquation(BlendEquation.Add);
                        GLES.SetBlendFunction(BlendFunction.One, BlendFunction.Zero);
                        break;

                    case BlendingMode.Alpha:
                        GLES.SetBlendEquation(BlendEquation.Add, BlendEquation.Add);
                        GLES.SetBlendFunction(BlendFunction.SourceAlpha, BlendFunction.OneMinusSourceAlpha);
                        break;

                    case BlendingMode.Additive:
                        GLES.SetBlendEquation(BlendEquation.Add);
                        GLES.SetBlendFunction(BlendFunction.SourceAlpha, BlendFunction.One);
                        break;

                    case BlendingMode.Subtractive: // Opposite of Additive (DST - SRC)
                        GLES.SetBlendEquation(BlendEquation.ReverseSubtract);
                        // GL.SetBlendFunction(BlendFunction.One, BlendFunction.One, BlendFunction.OneMinusSourceAlpha, BlendFunction.One);
                        GLES.SetBlendFunction(BlendFunction.SourceAlpha, BlendFunction.One);
                        break;

                    case BlendingMode.Multiply:
                        GLES.SetBlendEquation(BlendEquation.Add);
                        GLES.SetBlendFunction(BlendFunction.DestinationColor, BlendFunction.OneMinusSourceAlpha);
                        break;

                    case BlendingMode.Invert:
                        GLES.SetBlendEquation(BlendEquation.Subtract);
                        GLES.SetBlendFunction(BlendFunction.One, BlendFunction.One, BlendFunction.One, BlendFunction.Zero);
                        break;
                }
            }
        }

        private struct UniformData
        {
            public Uniform Uniform;

            public object Value;
        }

        private unsafe void UpdateUniform(Uniform uniform, object value)
        {
            var location = uniform.Location;

            // Log.Debug($"Updating Uniform: {uniform.Name}");

            switch (uniform.Type)
            {
                #region Integer

                case UniformType.Integer when uniform.Dimensions == (1, 1):
                {
                    // Integer
                    if (value is int x)
                    {
                        GLES.Uniform1(location, x);
                    }
                    // Integer Array
                    else if (value is int[] arr)
                    {
                        GLES.Uniform1(location, arr);
                    }
                    else
                    {
                        goto BadUniformException;
                    }
                }
                break;

                case UniformType.Integer when uniform.Dimensions == (1, 2):
                {
                    // Tuple
                    if (value is (int x, int y))
                    {
                        GLES.Uniform2(location, x, y);
                    }
                    // IntVector
                    else if (value is IntVector vec)
                    {
                        GLES.Uniform2(location, vec.X, vec.Y);
                    }
                    // IntVector Array
                    else if (value is IntVector[] vecs)
                    {
                        fixed (IntVector* ptr = vecs)
                        {
                            GLES.Uniform2(location, vecs.Length, (int*) ptr);
                        }
                    }
                    // Integer Array
                    else if (value is int[] arr)
                    {
                        GLES.Uniform2(location, arr);
                    }
                    else
                    {
                        goto BadUniformException;
                    }
                }
                break;

                case UniformType.Integer when uniform.Dimensions == (1, 3):
                {
                    // Tuple
                    if (value is (int x, int y, int z))
                    {
                        GLES.Uniform3(location, x, y, z);
                    }
                    // Integer Array
                    else if (value is int[] arr)
                    {
                        GLES.Uniform3(location, arr);
                    }
                    else
                    {
                        goto BadUniformException;
                    }
                }
                break;

                case UniformType.Integer when uniform.Dimensions == (1, 4):
                {
                    // Tuple
                    if (value is (int x, int y, int z, int w))
                    {
                        GLES.Uniform4(location, x, y, z, w);
                    }
                    // Integer Array
                    else if (value is int[] arr)
                    {
                        GLES.Uniform4(location, arr);
                    }
                    else
                    {
                        goto BadUniformException;
                    }
                }
                break;

                #endregion

                #region Unsigned Integer

                case UniformType.UnsignedInteger when uniform.Dimensions == (1, 1):
                {
                    // Unsigned Integer
                    if (value is uint x)
                    {
                        GLES.Uniform1(location, x);
                    }
                    // Unsigned Integer Array
                    else if (value is uint[] arr)
                    {
                        GLES.Uniform1(location, arr);
                    }
                    else
                    {
                        goto BadUniformException;
                    }
                }
                break;

                case UniformType.UnsignedInteger when uniform.Dimensions == (1, 2):
                {
                    // Tuple
                    if (value is (uint x, uint y))
                    {
                        GLES.Uniform2(location, x, y);
                    }
                    // Unsigned Integer Array
                    else if (value is uint[] arr)
                    {
                        GLES.Uniform2(location, arr);
                    }
                    else
                    {
                        goto BadUniformException;
                    }
                }
                break;

                case UniformType.UnsignedInteger when uniform.Dimensions == (1, 3):
                {
                    // Tuple
                    if (value is (uint x, uint y, uint z))
                    {
                        GLES.Uniform3(location, x, y, z);
                    }
                    // Unsigned Integer Array
                    else if (value is uint[] arr)
                    {
                        GLES.Uniform3(location, arr);
                    }
                    else
                    {
                        goto BadUniformException;
                    }
                }
                break;

                case UniformType.UnsignedInteger when uniform.Dimensions == (1, 4):
                {
                    // Tuple
                    if (value is (uint x, uint y, uint z, uint w))
                    {
                        GLES.Uniform4(location, x, y, z, w);
                    }
                    // Unsigned Integer Array
                    else if (value is uint[] arr)
                    {
                        GLES.Uniform4(location, arr);
                    }
                    else
                    {
                        goto BadUniformException;
                    }
                }
                break;

                #endregion

                #region Boolean

                case UniformType.Bool when uniform.Dimensions == (1, 1):
                {
                    // Boolean
                    if (value is bool x)
                    {
                        GLES.Uniform1(location, x ? 1 : 0);
                    }
                    // Boolean Array
                    else if (value is bool[] arr)
                    {
                        // todo: ArrayPool<int> and copy...?
                        throw new NotSupportedException("Boolean arrays not (yet) supported! Poke the developer.");
                    }
                    else
                    {
                        goto BadUniformException;
                    }
                }
                break;

                case UniformType.Bool when uniform.Dimensions == (1, 2):
                    throw new NotSupportedException("Boolean Vec2 not (yet) supported! Poke the developer.");

                case UniformType.Bool when uniform.Dimensions == (1, 3):
                    throw new NotSupportedException("Boolean Vec3 not (yet) supported! Poke the developer.");

                case UniformType.Bool when uniform.Dimensions == (1, 4):
                    throw new NotSupportedException("Boolean Vec4 not (yet) supported! Poke the developer.");

                #endregion

                #region Float

                case UniformType.Float when uniform.Dimensions == (1, 1):
                {
                    // Float
                    if (value is float x)
                    {
                        GLES.Uniform1(location, x);
                    }
                    // Float Array
                    else if (value is float[] arr)
                    {
                        GLES.Uniform1(location, arr);
                    }
                    else
                    {
                        goto BadUniformException;
                    }
                }
                break;

                case UniformType.Float when uniform.Dimensions == (1, 2):
                {
                    // Tuple
                    if (value is (float x, float y))
                    {
                        GLES.Uniform2(location, x, y);
                    }
                    // Vector
                    else if (value is Vector vec)
                    {
                        GLES.Uniform2(location, vec.X, vec.Y);
                    }
                    // Vector Array
                    else if (value is Vector[] vecs)
                    {
                        fixed (Vector* ptr = vecs)
                        {
                            GLES.Uniform2(location, vecs.Length, (float*) ptr);
                        }
                    }
                    // Float Array
                    else if (value is float[] arr)
                    {
                        GLES.Uniform2(location, arr);
                    }
                    else
                    {
                        goto BadUniformException;
                    }
                }
                break;

                case UniformType.Float when uniform.Dimensions == (1, 3):
                {
                    // Tuple
                    if (value is (float x, float y, float z))
                    {
                        GLES.Uniform3(location, x, y, z);
                    }
                    // Float Array
                    else if (value is float[] arr)
                    {
                        GLES.Uniform3(location, arr);
                    }
                    else
                    {
                        goto BadUniformException;
                    }
                }
                break;

                case UniformType.Float when uniform.Dimensions == (1, 4):
                {
                    // Tuple
                    if (value is (float x, float y, float z, float w))
                    {
                        GLES.Uniform4(location, x, y, z, w);
                    }
                    // Color
                    else if (value is Color col)
                    {
                        GLES.Uniform4(location, col.R, col.G, col.B, col.A);
                    }
                    // Color Array
                    else if (value is Color[] cols)
                    {
                        fixed (Color* ptr = cols)
                        {
                            GLES.Uniform4(location, cols.Length, (float*) ptr);
                        }
                    }
                    // Rectangle
                    else if (value is Rectangle rect)
                    {
                        GLES.Uniform4(location, rect.X, rect.Y, rect.Width, rect.Height);
                    }
                    // Rectangle Array
                    else if (value is Rectangle[] rects)
                    {
                        fixed (Rectangle* ptr = rects)
                        {
                            GLES.Uniform4(location, rects.Length, (float*) ptr);
                        }
                    }
                    // Float Array
                    else if (value is float[] arr)
                    {
                        GLES.Uniform4(location, arr);
                    }
                    else
                    {
                        goto BadUniformException;
                    }
                }
                break;

                #endregion

                #region Matrix

                case UniformType.Float when uniform.IsMatrix:
                {
                    // Matrix
                    if (value is Matrix m)
                    {
                        GLES.UniformMatrix2x3(location, 1, (float*) &m);
                    }
                    // Matrix Array
                    else if (value is Matrix[] arr)
                    {
                        fixed (Matrix* ptr = arr)
                        {
                            GLES.UniformMatrix2x3(location, arr.Length, (float*) ptr);
                        }
                    }
                    // Float Array
                    else if (value is float[] xs)
                    {
                        fixed (float* ptr = xs)
                        {
                            GLES.UniformMatrix2x3(location, xs.Length / 6, ptr);
                        }
                    }
                    else
                    {
                        goto BadUniformException;
                    }
                }
                break;

                #endregion

                case UniformType.Image:
                {
                    if (value is Texture texture)
                    {
                        // Get the texture information for this image source
                        RequestTextureInformation(texture, out var esTexture, out var uvRect);

                        // Get texture unit for sampler2D uniform.
                        var unit = Shader.GetTextureUnit(uniform.Name);

                        // Shader should reserve X of the N allowable texture units available
                        // on the executing hardware. Thus if a shader requires the use of two
                        // textures it would claim two of the units available and prevent their
                        // use from the batching mechanism.

                        // todo: Associate uniform with texture unit
                        GLES.Uniform1(location, unit);

                        // Bind texture
                        // todo: only do this if the texture isn't already mapped
                        GLES.ActiveTexture(unit);
                        GLES.BindTexture(TextureTarget.Texture2D, esTexture.Handle);

                        // Check if associated atlas rectangle (uvrect) exists. If it
                        // does then we need to set that as well.
                        var uvRectUniformName = GetAtlasRectUniformName(uniform.Name);
                        if (Shader.HasUniform(uvRectUniformName))
                        {
                            var uvRectUniform = Shader.GetUniform(uvRectUniformName);
                            UpdateUniform(uvRectUniform, uvRect);
                        }
                    }
                    else
                    {
                        goto BadUniformException;
                    }
                }
                break;
            }

            // Uniform state has been updated.
            // Exit here to prevent falling into the exception below.
            return;

            // Uniform could not be set, a value of an invalid type was given.
            BadUniformException:
            throw new InvalidOperationException($"Unable to update uniform '{uniform.Name}' " +
                $"({uniform.Type}{uniform.Dimensions}[{uniform.ArraySize}]) to mismatched type {value.GetType()}.");

            static string GetAtlasRectUniformName(string uniform)
            {
                var suffix = "_UVRect";

                // Check if uniform name is an array (ie, 'uImage[2]')
                var lastBracket = uniform.LastIndexOf('[');
                if (lastBracket >= 0)
                {
                    // Extract non-array name (ie 'uImage' )
                    var name = uniform.Substring(0, lastBracket);
                    var index = uniform.Substring(lastBracket);

                    // Combine name with suffix (ie 'uImage_UVRect[2]')
                    return name + suffix + index;
                }
                else
                {
                    // Combine name with suffix (ie 'uImage_UVRect')
                    return uniform + suffix;
                }
            }
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
            uvRect.X -= (float) texture.Interpolation;
            uvRect.Y -= (float) texture.Repeat;
        }

        private void RequestTextureInformation(Texture texture, out ESTexture atlasTexture, out Rectangle atlasRect)
        {
            if (texture is Image image)
            {
                while (!_atlas.Submit(image, out atlasTexture, out atlasRect))
                {
                    // Unable to submit new image to atlas, we need to flush
                    // all pending work that depends on the current state of the atlas.
                    Flush();

                    // Evict atlas, we need space for this new texture.
                    _atlas.Evict();
                }
            }
            else if (texture is Surface surface)
            {
                if (surface.Screen != null) { throw new ArgumentException("Unable to use screen bound surface as a texture."); }

                // Get offscreen surface texture
                var esSurface = GetNativeObject<ESSurface>(surface);
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

        internal struct DeviceCapabilities
        {
            public int MaxTextureSize { get; init; }
        }

        protected override object GenerateNativeObject(GraphicsResource resource)
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
    }
}
