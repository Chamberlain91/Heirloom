using System;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using System.Runtime.InteropServices;
using System.Threading;

using Heirloom.IO;
using Heirloom.Math;
using Heirloom.OpenGLES;

namespace Heirloom.Drawing.OpenGLES
{
    internal abstract class OpenGLGraphics : Graphics
    {
        private readonly ConsumerThread _thread;
        private bool _isRunning = false;

        internal OpenGLCapabilities Capabilities;

        // == Cross Context Resources - TODO: Share across contexts...
        private readonly Dictionary<string, UniformBuffer> _uniformBuffers = new Dictionary<string, UniformBuffer>();
        private AtlasTechnique _atlasTechnique;

        // == Per Context Resources
        private readonly ConditionalWeakTable<Surface, Framebuffer> _framebuffers;
        private BatchingTechnique _batchingTechnique;

        // 
        private Matrix _viewMatrix;
        private Matrix _viewMatrixInverse;
        private bool _viewMatrixInverseDirty;

        // Surface State
        private Surface _surface;

        // Viewport state
        private IntRectangle _viewportRect;
        private Rectangle _viewport;
        private bool _viewportDirty;

        // Shader State
        private ShaderProgram _shaderProgram;
        private Shader _shader;

        // Texture State
        private bool _textureBindDirty;
        private Texture _texture;

        // Blending State
        private Blending _blendMode;
        private Color _blendColor;

        #region Constructors

        protected internal OpenGLGraphics(MultisampleQuality multisample)
            : base(multisample)
        {
            _framebuffers = new ConditionalWeakTable<Surface, Framebuffer>();

            // Create and start new task runner
            _thread = new ConsumerThread("GL Consumer");
            _thread.InvokeLater(() =>
            {
                // Assign the GL context to this thread.
                MakeCurrent();

                // Query extra capability info
                Capabilities = new OpenGLCapabilities
                {
                    MaxTextureSize = GL.GetInteger(GetParameter.MaxTextureSize)
                };

                // Create batching utilities
                _batchingTechnique = new HybridBatchingTechnique();
                _atlasTechnique = new PackerAtlasTechnique(this);

                // Set default OpenGL state
                GL.Enable(EnableCap.ScissorTest);
                GL.Enable(EnableCap.Blend);
                ResetState();
            });
        }

        /// <summary>
        /// Launches the dedicated OpenGL thread associated with this context.
        /// </summary>
        protected void StartThread()
        {
            if (!_isRunning)
            {
                // Begin consumer thread
                _isRunning = true;
                _thread.Start();

                // Wait For GL
                SpinWait.SpinUntil(() => _batchingTechnique != null);
            }
            else
            {
                throw new InvalidOperationException("OpenGL thread already running.");
            }
        }

        #endregion

        #region Invoke

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        protected internal void Invoke(Action action, bool blocking = true)
        {
            if (!_isRunning) { throw new InvalidOperationException("Unable to invoke, thread not started."); }
            if (blocking) { _thread.Invoke(action); }
            else { _thread.InvokeLater(action); }
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        protected internal T Invoke<T>(Func<T> action)
        {
            if (!_isRunning) { throw new InvalidOperationException("Unable to invoke, thread not started."); }
            return _thread.Invoke(action);
        }

        #endregion

        protected abstract void MakeCurrent();

        #region Global Transform

        public override Matrix GlobalTransform
        {
            get => _viewMatrix;

            set
            {
                if (!Equals(_viewMatrix, value))
                {
                    // Complete previous work
                    Flush();

                    // Store view matrix
                    _viewMatrixInverseDirty = true;
                    _viewMatrix = value;
                }
            }
        }

        public override Matrix InverseGlobalTransform
        {
            get
            {
                if (_viewMatrixInverseDirty)
                {
                    // Compute inverted view matrix
                    Matrix.Inverse(in _viewMatrix, ref _viewMatrixInverse);
                    _viewMatrixInverseDirty = false;
                }

                return _viewMatrixInverse;
            }
        }

        #endregion

        #region Viewport

        public override IntRectangle ViewportScreen
        {
            get => _viewportRect;

            set
            {
                // Compute normalized viewport rect
                var x = value.X / (float) Surface.Width;
                var y = value.Y / (float) Surface.Height;
                var w = value.Width / (float) Surface.Width;
                var h = value.Height / (float) Surface.Height;

                // Assign normalized viewport
                Viewport = new Rectangle(x, y, w, h);
            }
        }

        public override Rectangle Viewport
        {
            get => _viewport;

            set
            {
                if (!Equals(_viewport, value))
                {
                    // Complete previous work
                    Flush();

                    // Assign normalzied viewprot
                    _viewport = value;

                    // Compute viewport rect
                    ComputeViewportRect();
                }
            }
        }

        protected void ComputeViewportRect()
        {
            // Set viewport and scissor box
            var w = (int) (_viewport.Width * Surface.Width);
            var h = (int) (_viewport.Height * Surface.Height);
            var x = (int) (_viewport.X * Surface.Width);
            var y = (int) (_viewport.Y * Surface.Height);
            y = Surface.Height - h - y; // todo: is this correct...?

            // Store actual computed viewport and mark as dirty to update GL counter part
            _viewportRect = new IntRectangle(x, y, w, h);
            _viewportDirty = true;
        }

        private unsafe void UpdateViewportScissor()
        {
            // Update viewport and scissor
            if (_viewportDirty)
            {
                var x = _viewportRect.X;
                var y = _viewportRect.Y;
                var w = _viewportRect.Width;
                var h = _viewportRect.Height;

                // 
                GL.SetViewport(x, y, w, h);
                GL.SetScissor(x, y, w, h);

                // 
                _viewportDirty = false;
            }
        }

        #endregion

        #region Blending

        public override Blending Blending
        {
            get => _blendMode;
            set => UseBlending(value);
        }

        public override Color Color
        {
            get => _blendColor;
            set => _blendColor = value;
        }

        private void UseBlending(Blending blending)
        {
            if (_blendMode != blending)
            {
                Invoke(() =>
                {
                    // Complete previous work
                    Flush();

                    // Store mode
                    _blendMode = blending;

                    // 
                    switch (_blendMode)
                    {
                        default:
                            throw new InvalidOperationException("Unable to set unknown blend mode.");

                        case Blending.Opaque:
                            GL.SetBlendEquation(BlendEquation.Add);
                            GL.SetBlendFunction(BlendFunction.One, BlendFunction.Zero);
                            break;

                        case Blending.Alpha:
                            GL.SetBlendEquation(BlendEquation.Add, BlendEquation.Add);
                            GL.SetBlendFunction(BlendFunction.SourceAlpha, BlendFunction.OneMinusSourceAlpha, BlendFunction.One, BlendFunction.OneMinusSourceAlpha);
                            break;

                        case Blending.Additive:
                            GL.SetBlendEquation(BlendEquation.Add);
                            GL.SetBlendFunction(BlendFunction.One, BlendFunction.One, BlendFunction.One, BlendFunction.OneMinusSourceAlpha);
                            break;

                        case Blending.Subtractive: // Opposite of Additive (DST - SRC)
                            GL.SetBlendEquation(BlendEquation.ReverseSubtract);
                            GL.SetBlendFunction(BlendFunction.One, BlendFunction.One, BlendFunction.OneMinusSourceAlpha, BlendFunction.One);
                            break;

                        case Blending.Multiply:
                            GL.SetBlendEquation(BlendEquation.Add);
                            GL.SetBlendFunction(BlendFunction.DestinationColor, BlendFunction.OneMinusSourceAlpha);
                            break;

                        case Blending.Invert:
                            GL.SetBlendEquation(BlendEquation.Subtract);
                            GL.SetBlendFunction(BlendFunction.One, BlendFunction.One, BlendFunction.One, BlendFunction.Zero);
                            break;
                    }
                }, false);
            }
        }

        #endregion

        #region Surface

        public override Surface Surface
        {
            get => _surface;
            set => UseSurface(value);
        }

        private void UseSurface(Surface surface)
        {
            // Wasn't the same surface as before, flush everything
            if (_surface != surface)
            {
                // Complete pending work
                Flush();

                // Set new surface
                _surface = surface;

                // We are changing surfaces, reset viewport to full surface
                _viewportRect = new IntRectangle(0, 0, surface.Width, surface.Height);
                _viewport = new Rectangle(0, 0, 1, 1);

                Invoke(blocking: false, action: () =>
                {
                    // Set and prepare the surface (ie, bind framebuffer)
                    if (_surface == DefaultSurface)
                    {
                        // The current surface is the default (ie, window) framebuffer.
                        GL.BindFramebuffer(FramebufferTarget.DrawFramebuffer, 0);
                    }
                    else
                    {
                        // Bind the associated framebuffer of the current surface.
                        var framebuffer = GetFramebuffer(_surface);
                        framebuffer.BindToDraw();
                    }
                });
            }
        }

        #endregion

        #region Shader

        public override Shader Shader
        {
            get => _shader;
            set => UseShader(value);
        }

        private void UseShader(Shader shader)
        {
            if (_shader != shader)
            {
                Invoke(() =>
                {
                    // Complete any previous render work
                    Flush();

                    // 
                    _shaderProgram = shader.Native as ShaderProgram;
                    _shader = shader;

                    // Use this shader program
                    GL.UseProgram(_shaderProgram.Handle);

                    // Bind uniform buffers
                    foreach (var block in _shaderProgram.Blocks)
                    {
                        var buffer = GetUniformBuffer(block);
                        GL.BindBufferBase(BufferTarget.UniformBuffer, block.Index, buffer.Handle);
                    }
                });
            }
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        private UniformBuffer GetUniformBuffer(ActiveUniformBlock block)
        {
            // Try to get uniform buffer for block name
            if (_uniformBuffers.TryGetValue(block.Name, out var buffer) == false)
            {
                // Create the buffer
                buffer = Invoke(() => new UniformBuffer((uint) block.DataSize));
                _uniformBuffers[block.Name] = buffer;
            }

            return buffer;
        }

        private void UpdateShaderUniforms()
        {
            // Enumerate each mutated uniform and set 
            foreach (var (name, storage) in _shader.UniformStorageMap)
            {
                // Note: We are always updating image uniforms as a "quick fix" to the potential error
                // that can occur when the atlas restructures or an image somehow gets relocated. There
                // is probably a better solution to this problem.
                if (storage.IsDirty || (storage.Info.Type == UniformType.Image && storage.Value != null))
                {
                    SetUniform(name, storage.Value);

                    // Mark as no longer dirty
                    storage.IsDirty = false;
                }
            }

            // We handled the dirty uniforms
            _shader.IsDirty = false;
        }

        private unsafe void SetUniform(string name, object value)
        {
            var location = _shaderProgram.GetUniformLocation(name);
            var uniform = _shaderProgram.GetUniform(name);

            if (uniform.BlockInfo == null)
            {
                var info = uniform.Info;

                switch (info.Type)
                {
                    #region Integer

                    case ActiveUniformType.Integer:
                    {
                        if (value is int x)
                        {
                            GL.Uniform1(location, x);
                        }
                        else if (value is int[] arr)
                        {
                            GL.Uniform1(location, arr);
                        }
                        else
                        {
                            throw new InvalidOperationException($"Unable to set shader uniform '{name}' to mismatched type.");
                        }
                    }
                    break;

                    case ActiveUniformType.IntVec2:
                    {
                        if (value is IntVector vec)
                        {
                            GL.Uniform2(location, vec.X, vec.Y);
                        }
                        else if (value is IntVector[] vecs)
                        {
                            fixed (IntVector* ptr = vecs)
                            {
                                GL.Uniform2(location, vecs.Length, (int*) ptr);
                            }
                        }
                        else if (value is int[] arr)
                        {
                            GL.Uniform2(location, arr);
                        }
                        else
                        {
                            throw new InvalidOperationException($"Unable to set shader uniform '{name}' to mismatched type.");
                        }
                    }
                    break;

                    case ActiveUniformType.IntVec3:
                    {
                        if (value is int[] arr)
                        {
                            GL.Uniform3(location, arr);
                        }
                        else
                        {
                            throw new InvalidOperationException($"Unable to set shader uniform '{name}' to mismatched type.");
                        }
                    }
                    break;

                    case ActiveUniformType.IntVec4:
                    {
                        if (value is int[] arr)
                        {
                            GL.Uniform4(location, arr);
                        }
                        else
                        {
                            throw new InvalidOperationException($"Unable to set shader uniform '{name}' to mismatched type.");
                        }
                    }
                    break;

                    #endregion

                    #region Unsigned Integer

                    case ActiveUniformType.UnsignedInteger:
                    {
                        if (value is uint x)
                        {
                            GL.Uniform1(location, x);
                        }
                        else if (value is uint[] arr)
                        {
                            GL.Uniform1(location, arr);
                        }
                        else
                        {
                            throw new InvalidOperationException($"Unable to set shader uniform '{name}' to mismatched type.");
                        }
                    }
                    break;

                    case ActiveUniformType.UnsignedIntVec2:
                    {
                        if (value is uint[] arr)
                        {
                            GL.Uniform2(location, arr);
                        }
                        else
                        {
                            throw new InvalidOperationException($"Unable to set shader uniform '{name}' to mismatched type.");
                        }
                    }
                    break;

                    case ActiveUniformType.UnsignedIntVec3:
                    {
                        if (value is uint[] arr)
                        {
                            GL.Uniform3(location, arr);
                        }
                        else
                        {
                            throw new InvalidOperationException($"Unable to set shader uniform '{name}' to mismatched type.");
                        }
                    }
                    break;

                    case ActiveUniformType.UnsignedIntVec4:
                    {
                        if (value is uint[] arr)
                        {
                            GL.Uniform4(location, arr);
                        }
                        else
                        {
                            throw new InvalidOperationException($"Unable to set shader uniform '{name}' to mismatched type.");
                        }
                    }
                    break;

                    #endregion

                    #region Float

                    case ActiveUniformType.Float:
                    {
                        if (value is float x)
                        {
                            GL.Uniform1(location, x);
                        }
                        else if (value is float[] arr)
                        {
                            GL.Uniform1(location, arr);
                        }
                        else
                        {
                            throw new InvalidOperationException($"Unable to set shader uniform '{name}' to mismatched type.");
                        }
                    }
                    break;

                    case ActiveUniformType.FloatVec2:
                    {
                        if (value is Vector vec)
                        {
                            GL.Uniform2(location, vec.X, vec.Y);
                        }
                        else if (value is Vector[] vecs)
                        {
                            fixed (Vector* ptr = vecs)
                            {
                                GL.Uniform2(location, vecs.Length, (float*) ptr);
                            }
                        }
                        else if (value is float[] arr)
                        {
                            GL.Uniform2(location, arr);
                        }
                        else
                        {
                            throw new InvalidOperationException($"Unable to set shader uniform '{name}' to mismatched type.");
                        }
                    }
                    break;

                    case ActiveUniformType.FloatVec3:
                    {
                        if (value is float[] arr)
                        {
                            GL.Uniform3(location, arr);
                        }
                        else
                        {
                            throw new InvalidOperationException($"Unable to set shader uniform '{name}' to mismatched type.");
                        }
                    }
                    break;

                    case ActiveUniformType.FloatVec4:
                    {
                        if (value is Color col)
                        {
                            GL.Uniform4(location, col.R, col.G, col.B, col.A);
                        }
                        else if (value is Color[] cols)
                        {
                            fixed (Color* ptr = cols)
                            {
                                GL.Uniform4(location, cols.Length, (float*) ptr);
                            }
                        }
                        else if (value is float[] arr)
                        {
                            GL.Uniform4(location, arr);
                        }
                        else if (value is Rectangle rect)
                        {
                            GL.Uniform4(location, rect.X, rect.Y, rect.Width, rect.Height);
                        }
                        else
                        {
                            throw new InvalidOperationException($"Unable to set shader uniform '{name}' to mismatched type.");
                        }
                    }
                    break;

                    #endregion

                    #region Boolean

                    case ActiveUniformType.Bool:
                    {
                        if (value is bool x)
                        {
                            GL.Uniform1(location, x ? 1 : 0);
                        }
                        else if (value is bool[] arr)
                        {
                            throw new NotSupportedException("Boolean arrays not (yet) supported! Poke the developer.");
                        }
                        else
                        {
                            throw new InvalidOperationException($"Unable to set shader uniform '{name}' to mismatched type.");
                        }
                    }
                    break;

                    case ActiveUniformType.BoolVec2:
                        throw new NotSupportedException("Boolean Vec2 not (yet) supported! Poke the developer.");

                    case ActiveUniformType.BoolVec3:
                        throw new NotSupportedException("Boolean Vec3 not (yet) supported! Poke the developer.");

                    case ActiveUniformType.BoolVec4:
                        throw new NotSupportedException("Boolean Vec4 not (yet) supported! Poke the developer.");

                    #endregion

                    #region Matrix

                    case ActiveUniformType.Matrix2x3:
                    {
                        if (value is Matrix m)
                        {
                            GL.UniformMatrix2x3(location, 1, (float*) &m);
                        }
                        else if (value is Matrix[] arr)
                        {
                            fixed (Matrix* ptr = arr)
                            {
                                GL.UniformMatrix2x3(location, arr.Length, (float*) ptr);
                            }
                        }
                        else if (value is float[] xs)
                        {
                            fixed (float* ptr = xs)
                            {
                                GL.UniformMatrix2x3(location, xs.Length / 6, ptr);
                            }
                        }
                        else
                        {
                            throw new InvalidOperationException($"Unable to set shader uniform '{name}' to mismatched type.");
                        }
                    }
                    break;

                    #endregion

                    case ActiveUniformType.Sampler2D:
                    {
                        if (value is ImageSource source)
                        {
                            // Find texture for the current image source
                            RequestTextureInformation(source, out var texture, out var uvRect);

                            // Get texture unit for sampler2D uniform.
                            var unit = _shaderProgram.GetTextureUnit(name);

                            // Bind texture
                            GL.ActiveTexture(unit);
                            GL.BindTexture(TextureTarget.Texture2D, texture.Handle);

                            // Check if associated uvRect exists then set that as well.
                            var uvRectUniform = $"{name}_UVRect";
                            if (_shaderProgram.HasUniform(uvRectUniform))
                            {
                                SetUniform(uvRectUniform, uvRect);
                            }
                        }
                        else
                        {
                            throw new InvalidOperationException($"Unable to set shader uniform '{name}' to mismatched type.");
                        }
                    }
                    break;
                }
            }
            else
            {
                throw new InvalidOperationException("Unable to set uniform.");
            }
        }

        public unsafe void SetBufferUniform<T>(string name, T data) where T : struct
        {
            var pin = GCHandle.Alloc(data, GCHandleType.Pinned);
            var size = Marshal.SizeOf<T>();
            SetBufferUniform(name, (void*) pin.AddrOfPinnedObject(), 0, size);
            pin.Free();
        }

        public unsafe void SetBufferUniform(string name, void* data, int offset, int size)
        {
            // Get uniform
            var uniform = _shaderProgram.GetUniform(name);

            // Get uniform buffer for relevant block
            var buffer = GetUniformBuffer(uniform.BlockInfo);

            // Update data in buffer
            Invoke(() => buffer.Update(data, uniform.Info.Offset + offset, size), false);
        }

        /// <summary>
        /// Writes a matrix to the given address with each row aligned to 16 bytes.
        /// </summary>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public unsafe void SetBufferUniform(string name, Matrix matrix)
        {
            var p = (float*) &matrix;
            SetBufferUniform(name, p + 0, 0, 12);
            SetBufferUniform(name, p + 3, 16, 12);
        }

        #endregion

        #region Read Pixels

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public override unsafe Image GrabPixels(IntRectangle region)
        {
            return Invoke(() =>
            {
                // Complete pending work
                Flush();

                // If the current surface is the default surface.
                if (_surface == DefaultSurface)
                {
                    // The current surface is the default (ie, window) framebuffer.
                    GL.BindFramebuffer(FramebufferTarget.ReadFramebuffer, 0);
                }
                else
                {
                    // Bind the associated framebuffer of the current surface.
                    var framebuffer = GetFramebuffer(_surface);
                    framebuffer.BindToRead();
                }

                // Grab pixels from read buffer
                var pixels = GL.ReadPixels(region.X, region.Y, region.Width, region.Height);
                fixed (uint* ptrPixels = pixels)
                {
                    // Copy grabbed pixels to image
                    var image = new Image(region.Width, region.Height);
                    Image.Copy((ColorBytes*) ptrPixels, region.Width, (IntVector.Zero, region.Size), image, IntVector.Zero);
                    return image;
                }
            });
        }

        #endregion

        #region Clear & Draw

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public override void Clear(Color color)
        {
            Invoke(() =>
            {
                // Update viewport and scissor
                UpdateViewportScissor();

                // Set color and clear
                GL.SetClearColor(color.R, color.G, color.B, color.A);
                GL.Clear(ClearMask.Color);
            });
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public override void DrawMesh(ImageSource source, Mesh mesh, in Matrix transform)
        {
            // Request texture information for the given input image
            RequestTextureInformation(source, out var texture, out var uvRect);

            // Inconsistent texture, flush and update state
            if (_texture != texture)
            {
                // Complete pending work
                Flush();

                // Mark that we need to update the texture binding
                _textureBindDirty = true;

                // Store new texture reference
                _texture = texture;
            }

            // Submit the mesh to draw
            while (!_batchingTechnique.Submit(mesh, in uvRect, in transform, in _blendColor)) { Flush(); }

            // If the shader's state has changed, we need to forcefully flush.
            if (_shader.IsDirty) { Flush(); }
        }

        #endregion

        #region Flush & Statistics

        /// <inheritdoc/>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public override unsafe void Flush()
        {
            // If the renderer has any batched work
            if (_batchingTechnique.IsDirty)
            {
                Invoke(() =>
                {
                    // Update viewport and scissor
                    UpdateViewportScissor();

                    // Compute view-projection matrix
                    var projMatrix = Matrix.RectangleProjection(0, 0, _viewportRect.Width, _viewportRect.Height);
                    Matrix.Multiply(in projMatrix, in _viewMatrix, ref projMatrix);

                    // Write into uniform buffer
                    SetBufferUniform("uMatrix", projMatrix);

                    // Update any mutated uniforms
                    UpdateShaderUniforms();

                    // Commit changes to textures
                    _atlasTechnique.CommitChanges();

                    // 
                    if (_textureBindDirty)
                    {
                        GL.ActiveTexture(0);
                        GL.BindTexture(TextureTarget.Texture2D, _texture.Handle);

                        _textureBindDirty = false;
                    }

                    // Draw batched geometry 
                    _batchingTechnique.DrawBatch();

                    // Mark surface as dirty
                    _surface.IncrementVersion();
                });
            }
        }

        /// <inheritdoc/>
        protected override DrawCounts GetDrawCounts()
        {
            return new DrawCounts
            {
                TriangleCount = _batchingTechnique.TriCount,
                BatchCount = _batchingTechnique.BatchCount,
                DrawCount = _batchingTechnique.DrawCount
            };
        }

        /// <inheritdoc/>
        protected override void EndFrame()
        {
            _batchingTechnique.ResetCounts();
        }

        #endregion

        #region Texture & Framebuffer

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        private void RequestTextureInformation(ImageSource source, out Texture texture, out Rectangle uvRect)
        {
            //if (source.Native != null)
            //{
            //    var r = source.Native as TextureResidence;
            //    texture = r.Texture;
            //    uvRect = r.UVRect;
            //}
            //else
            //{
            switch (source)
            {
                case Image image:
                    while (!_atlasTechnique.Submit(image, out texture, out uvRect))
                    {
                        //Log.Warning("Encountered an image that cannot take residence in the atlas.");
                        Flush();

                        //Log.Warning("Evicting Texture Atlas");
                        _atlasTechnique.Evict();
                    }
                    break;

                case Surface surface:
                    // Get framebuffer (possibly blitting)
                    var framebuffer = GetFramebuffer(surface);
                    if (framebuffer.IsDirty)
                    {
                        Invoke(() => framebuffer.BlitToTexture());
                    }

                    // Emit surface texture
                    texture = framebuffer.Texture;
                    uvRect = (0, 0, 1, -1);
                    break;

                default:
                    throw new InvalidOperationException("Image source was not a valid type");
            }

            // We encode special meaning into negative values
            if (source.Interpolation == InterpolationMode.Linear) { uvRect.X -= 1; }
            if (source.Repeat == RepeatMode.Repeat) { uvRect.Y -= 1; }

            //// 
            //var residence = new TextureResidence(texture, uvRect);
            //source.Native = residence;
            //}
        }

        private Framebuffer GetFramebuffer(Surface surface)
        {
            // Try to get known framebuffer instance. Framebuffers are "container objects" and
            // need to be uniquely created for each graphics context.
            if (!_framebuffers.TryGetValue(surface, out var framebuffer))
            {
                // Was not known, we will now create it 
                framebuffer = Invoke(() => new Framebuffer(surface));

                // Store framebuffer
                _framebuffers.Add(surface, framebuffer);
            }

            // Return framebuffer associated with surface
            return framebuffer;
        }

        #endregion

        #region Dispose

        protected override void Dispose(bool disposeManaged)
        {
            // This lock is here to have 'exclusive control' of the render context.
            // The application should lock the context during its render loop
            // to prevent the context from being disposed of when rendering.
            lock (this)
            {
                _isRunning = false;

                // Terminate consumer thread
                _thread.Stop(true);

                if (disposeManaged)
                {
                    // Dispose managed objects...
                }

                // Call base dispose (to cleanup non-OpenGL resources)
                base.Dispose(disposeManaged);
            }
        }

        #endregion
    }

    internal class TextureResidence
    {
        public Texture Texture;
        public Rectangle UVRect;

        public TextureResidence(Texture texture, Rectangle uVRect)
        {
            Texture = texture ?? throw new ArgumentNullException(nameof(texture));
            UVRect = uVRect;
        }
    }
}
