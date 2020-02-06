using System;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using System.Runtime.InteropServices;
using System.Threading;
using Heirloom.Drawing.OpenGLES.Utilities;
using Heirloom.Math;
using Heirloom.OpenGLES;

namespace Heirloom.Drawing.OpenGLES
{
    public abstract class OpenGLGraphics : Graphics
    {
        private Surface _currentSurface;
        private Renderer _renderer;

        private readonly ConsumerThread _thread;
        private bool _isRunning = false;

        private ShaderProgram _shaderProgram;
        private Shader _shader;

        private Dictionary<string, Buffer> _uniformBuffers;

        private Rectangle _viewport;
        private Matrix _viewMatrix;
        private Matrix _inverseViewMatrix;
        private float _approxPixelScale = 1;

        private Blending _blendMode;
        private Color _blendColor;

        #region Constructors

        protected internal OpenGLGraphics(GraphicsAdapter adapter, MultisampleQuality multisample)
            : base(adapter, multisample)
        {
            _isRunning = true;

            // Create and start new task runner
            _thread = new ConsumerThread("GL Consumer");
            _thread.InvokeLater(() =>
            {
                MakeCurrent();

                //  
                Version = ParseVersion();
                Log.Info(Version);

                // 
                GL.Enable(EnableCap.ScissorTest);
                GL.Enable(EnableCap.Blend);

                // TODO: Share buffers in ResourceManager?
                _uniformBuffers = new Dictionary<string, Buffer>();

                // Construct the instancing batching renderer
                _renderer = new InstancingRenderer(this);

                // 
                ResetState();
            });

            // Begin consumer thread
            _thread.Start();
        }

        #endregion

        #region Properties 

        /// <summary>
        /// Gets the detected OpenGL version of this platform.
        /// </summary>
        public OpenGLVersion Version { get; private set; }

        #endregion

        #region Thread Callbacks

        protected abstract void MakeCurrent();

        private OpenGLVersion ParseVersion()
        // ref: https://hackage.haskell.org/package/bindings-GLFW-3.1.2.2/src/glfw/src/context.c
        {
            var vendor = GL.GetString(StringParameter.Vendor);
            var renderer = GL.GetString(StringParameter.Renderer);
            var version = GL.GetString(StringParameter.Version);
            var embedded = false;

            var embeddedPrefixes = new[]
            {
                "OpenGL ES ",
                "OpenGL ES-CM ",
                "OpenGL ES-CL "
            };

            // Try to detect OpenGL ES
            foreach (var prefix in embeddedPrefixes)
            {
                if (version.StartsWith(prefix))
                {
                    // Strip prefix
                    version = version.Substring(prefix.Length);
                    embedded = true;
                    break;
                }
            }

            //
            var split = version.IndexOf(" ");
            if (split >= 0) { version = version.Substring(0, split); }

            // Find dots (A.B.C or A.B)
            var minDot = version.IndexOf('.');
            var revDot = version.IndexOf('.', minDot + 1);
            if (revDot < 0) { revDot = version.Length; }

            // Extract and parse version number substrings
            var major = int.Parse(version.Substring(0, minDot));
            var minor = int.Parse(version.Substring(minDot + 1, revDot - minDot - 1));

            return new OpenGLVersion(vendor, renderer, major, minor, embedded);
        }

        #endregion

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

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        private Buffer GetUniformBuffer(ActiveUniformBlock block)
        {
            // Try to get uniform buffer for block name
            if (_uniformBuffers.TryGetValue(block.Name, out var buffer) == false)
            {
                // Create the buffer
                buffer = Invoke(() => new Buffer(BufferTarget.UniformBuffer, (uint) block.DataSize));
                _uniformBuffers[block.Name] = buffer;
            }

            return buffer;
        }

        private unsafe void SetViewportAndScissor(out int w, out int h)
        {
            // Set viewport and scissor box
            // todo: only set if a viewport dirty flag was set to prevent calling this on every flush
            w = (int) (_viewport.Width * Surface.Width);
            h = (int) (_viewport.Height * Surface.Height);
            var x = (int) (_viewport.X * Surface.Width);
            var y = (int) (_viewport.Y * Surface.Height);
            y = Surface.Height - h - y;

            GL.SetViewport(x, y, w, h);
            GL.SetScissor(x, y, w, h);
        }

        #region Shaders

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
                    _shaderProgram = GetNativeObject(shader) as ShaderProgram;
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

        private void UpdateShaderUniforms()
        {
            // Enumerate each mutated uniform and set 
            foreach (var (name, value) in EnumerateAndResetDirtyUniforms(_shader))
            {
                SetUniform(name, value);
            }
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
                        if (value is ImageSource image)
                        {
                            var (texture, textureRect) = ResourceManager.GetTextureInfo(this, image);

                            // Bind texture
                            GL.ActiveTexture(_shaderProgram.GetTextureUnit(name));
                            GL.BindTexture(TextureTarget.Texture2D, texture.Handle);
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

        public unsafe void SetShaderParameter<T>(string name, T data) where T : struct
        {
            var pin = GCHandle.Alloc(data, GCHandleType.Pinned);
            var size = Marshal.SizeOf<T>();
            SetShaderParameter(name, (void*) pin.AddrOfPinnedObject(), 0, size);
            pin.Free();
        }

        public unsafe void SetShaderParameter(string name, void* data, int offset, int size)
        {
            // Get uniform
            var uniform = _shaderProgram.GetUniform(name);

            // Get uniform buffer for relevant block
            var buffer = GetUniformBuffer(uniform.BlockInfo);

            // Update data in buffer
            Invoke(() => buffer.Update(data, uniform.Info.Offset + offset, size), !!false);
        }

        /// <summary>
        /// Writes a matrix to the given address with each row aligned to 16 bytes.
        /// </summary>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public unsafe void SetShaderParameter(string name, Matrix matrix)
        {
            var p = (float*) &matrix;
            SetShaderParameter(name, p + 0, 0, 12);
            SetShaderParameter(name, p + 3, 16, 12);
        }

        #endregion

        #region Surface

        public override Surface Surface
        {
            get => _currentSurface;

            set
            {
                // Wasn't the same surface as before, flush everything
                if (value != _currentSurface)
                {
                    // Only flush we have a valid surface
                    Flush();

                    _currentSurface = value;

                    Invoke(() =>
                    {
                        // Set and prepare the surface (ie, bind framebuffer)
                        if (value == DefaultSurface)
                        {
                            // Bind window surface (default for context)
                            GL.BindFramebuffer(FramebufferTarget.DrawFramebuffer, 0);
                        }
                        else
                        {
                            // Bind surface framebuffer
                            ResourceManager.GetFramebuffer(this, value).Bind();
                        }
                    }, !!false);
                }
            }
        }

        /// <summary>
        /// Mark the current surface dirty (increments version number).
        /// </summary>
        internal void MarkSurfaceDirty()
        {
            UpdateSurfaceVersionNumber();
        }

        #endregion

        #region View State

        public override Matrix GlobalTransform
        {
            get => _viewMatrix;

            set
            {
                Flush(); // if different, draw

                // store view matrix
                _viewMatrix = value;

                // compute inverted view matrix
                Matrix.Inverse(in value, ref _inverseViewMatrix);

                // approximation for pixel scale
                var invViewScale = _inverseViewMatrix.GetAffineScale();
                _approxPixelScale = (invViewScale.X + invViewScale.Y) / 2F;
            }
        }

        public override Matrix InverseGlobalTransform => _inverseViewMatrix;

        public override Rectangle Viewport
        {
            get => _viewport;

            set
            {
                // if dirty, flush
                if (_renderer.IsDirty)
                {
                    // This will complete anything to draw, and
                    // then adjust viewports.
                    Flush();
                }
                else
                {
                    // Nothing to draw, so we will just set the viewport
                    Invoke(() => SetViewportAndScissor(out var _, out var _), !!false);
                }

                _viewport = value;
            }
        }

        #endregion

        #region Blending and Color

        public override Color Color
        {
            get => _blendColor;
            set => _blendColor = value;
        }

        public override Blending Blending
        {
            get => _blendMode;

            set
            {
                if (_blendMode != value)
                {
                    Invoke(() =>
                    {
                        // 
                        Flush();

                        // Store mode
                        _blendMode = value;

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
                    }, !!false);
                }
            }
        }

        #endregion

        #region Draw

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public override void Clear(in Color color)
        {
            var col = color;

            Invoke(() =>
            {
                // Set color and clear
                GL.SetClearColor(col.R, col.G, col.B, col.A);
                GL.Clear(ClearMask.Color);
            }, !!false);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public override void DrawMesh(ImageSource image, Mesh mesh, in Matrix transform)
        {
            _renderer.Submit(image, mesh, in transform, _blendColor);
            if (HasDirtyUniform(_shader)) { Flush(); }
        }

        #endregion

        #region Capture

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public override unsafe Image GrabPixels(in IntRectangle region)
        {
            var rec = region;

            return Invoke(() =>
            {
                // 
                Flush();

                // If not the default surface and multisampled
                if (Surface != DefaultSurface)
                {
                    var framebuffer = ResourceManager.GetFramebuffer(this, Surface);

                    // If a multisampled surface, cause the framebuffer to blit into texture
                    if (framebuffer.MultisampleBuffer != null) { framebuffer.Update(this); }

                    // Read pixels from surface texture
                    GL.BindFramebuffer(FramebufferTarget.ReadFramebuffer, framebuffer.TextureBuffer.Handle);
                }
                else
                {
                    // Read from default buffer (window, etc)
                    GL.BindFramebuffer(FramebufferTarget.ReadFramebuffer, 0);
                }

                // Grab pixels from framebuffer
                var image = new Image(rec.Width, rec.Height);
                image.SetPixels(GL.ReadPixels(rec.X, rec.Y, rec.Width, rec.Height));
                return image;
            });
        }

        #endregion

        #region Flush

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public override unsafe void Flush()
        {
            // Exit, bad configuration
            if (_currentSurface == null) { return; }

            // If the renderer has any batched work
            if (_renderer.IsDirty)
            {
                Invoke(() =>
                {
                    // Update any mutated uniforms
                    UpdateShaderUniforms();

                    // Compute view-projection matrix
                    SetViewportAndScissor(out var w, out var h);
                    var projMatrix = Matrix.RectangleProjection(0, 0, w, h);
                    Matrix.Multiply(in projMatrix, in _viewMatrix, ref projMatrix);

                    // Write into uniform buffer
                    SetShaderParameter("uMatrix", projMatrix);

                    //// Synchronize GPU with operations before drawing
                    //var sync = GL.FenceSync(SyncFenceCondition.SyncGpuCommandsComplete, 0);
                    //GL.WaitSync(sync, 0); // Wait GPU for buffer writes, etc
                    //GL.DeleteSync(sync);  // Remove sync object

                    // Flush pending batch
                    _renderer.FlushPendingBatch();
                });
            }
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
}
