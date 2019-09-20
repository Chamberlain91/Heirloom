using System;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using System.Runtime.InteropServices;

using Heirloom.Drawing.OpenGLES.Utilities;
using Heirloom.Math;
using Heirloom.OpenGLES;

namespace Heirloom.Drawing.OpenGLES
{
    public abstract class OpenGLRenderContext : RenderContext
    {
        private ShaderFactory _shaderFactory;

        private readonly GLDefaultSurface _defaultSurface;
        private GLSurface _currentSurface;
        private Renderer _renderer;

        private ConsumerThread _thread;
        private bool _isRunning = false;

        private readonly ConditionalWeakTable<Texture, Framebuffer> _framebuffers;
        private ShaderProgram _shader;

        private Dictionary<string, Buffer> _uniformBuffers;

        private Rectangle _viewport;
        private Matrix _viewMatrix;
        private Matrix _inverseViewMatrix;
        private float _approxPixelScale = 1;

        private Blending _blendMode;
        private Color _blendColor;

        #region Constructors

        protected internal OpenGLRenderContext()
        {
            // 
            _defaultSurface = new GLDefaultSurface();
            _framebuffers = new ConditionalWeakTable<Texture, Framebuffer>();
        }

        ~OpenGLRenderContext()
        {
            Dispose(false);
        }

        #endregion

        #region Properties

        public bool IsDisposed { get; private set; } = false;

        public VersionInfo Info { get; private set; }

        #endregion

        #region Thread Callbacks

        protected abstract void PrepareContext();    // make current, etc

        protected void SetDefaultSurfaceSize(IntSize size)
        {
            _defaultSurface.SetSize(size);
        }

        private unsafe void InitializeContext()
        {
            // Make current, etc
            PrepareContext();

            //  
            Console.WriteLine(Info = ParseVersion());

            // 
            GL.Enable(EnableCap.ScissorTest);
            GL.Enable(EnableCap.Blend);

            // TODO: Share buffers in ResourceManager?
            _uniformBuffers = new Dictionary<string, Buffer>();
            _shaderFactory = new ShaderFactory(this);

            // Construct the instancing batching renderer
            var maxTextureImageUnits = GL.GetInteger(GetParameter.MaxTextureImageUnits);
            _renderer = new InstancingRenderer(this, maxTextureImageUnits); // todo: reserve 4 for effect units?

            Console.WriteLine($"Max Texture Units: {maxTextureImageUnits}");

            // Set the default shader
            var defaultShader = _shaderFactory.GetShaderProgram("shader.vert", "shader.frag");
            SetShaderProgram(defaultShader);

            // Bind uniform buffers
            foreach (var block in _shader.GetBlocks())
            {
                var buffer = GetUniformBuffer(block);
                GL.BindBufferBase(BufferTarget.UniformBuffer, block.Index, buffer.Handle);
            }
        }

        private VersionInfo ParseVersion()
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

            return new VersionInfo
            {
                Vendor = vendor,
                Renderer = renderer,

                Major = major,
                Minor = minor,

                IsEmbedded = embedded,
            };
        }

        #endregion

        public void StartThread()
        {
            if (!_isRunning)
            {
                _isRunning = true;

                // Create and start new task runner
                _thread = new ConsumerThread("GL Consumer");
                _thread.InvokeLater(InitializeContext);
                _thread.InvokeLater(ResetState);
                _thread.Start();
            }
        }

        //public void StopThread()
        //{
        //    if (_isRunning)
        //    {
        //        // Terminate task runner
        //        _asyncRunner.Stop();
        //        _isRunning = false;
        //    }
        //}

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

        internal Framebuffer GetFramebuffer(Texture texture)
        {
            // This framebuffer is not configured, need to initialize
            if (_framebuffers.TryGetValue(texture, out var framebuffer) == false)
            {
                // Generate and bind framebuffer
                framebuffer = new Framebuffer(this, texture);

                // Store newly created framebuffer
                _framebuffers.Add(texture, framebuffer);
            }

            return framebuffer;
        }

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

        #region Shader Parameters

        private void SetShaderProgram(ShaderProgram shader)
        {
            if (_shader != shader)
            {
                Invoke(() =>
                {
                    // Complete any previous render work
                    Flush();

                    // Set the new shader
                    _shader = shader;

                    // Use this shader program
                    GL.UseProgram(_shader.Handle);

                    // Bind uniform buffers
                    foreach (var block in _shader.GetBlocks())
                    {
                        var buffer = GetUniformBuffer(block);
                        GL.BindBufferBase(BufferTarget.UniformBuffer, block.Index, buffer.Handle);
                    }
                });
            }
        }

        public unsafe void SetShaderImage(int index, ImageSource image)
        {
            throw new NotImplementedException();
        }

        public unsafe void SetShaderParamter<T>(string name, T data) where T : struct
        {
            var pin = GCHandle.Alloc(data, GCHandleType.Pinned);
            var size = Marshal.SizeOf<T>();
            SetShaderParamter(name, (void*) pin.AddrOfPinnedObject(), 0, size);
            pin.Free();
        }

        public unsafe void SetShaderParamter(string name, void* data, int offset, int size)
        {
            // Get uniform
            var uniform = _shader.GetUniform(name);

            // Get uniform buffer for relevant block
            var buffer = GetUniformBuffer(uniform.BlockInfo);

            // Update data in buffer
            Invoke(() => buffer.Update(data, uniform.Info.Offset + offset, size));
        }

        /// <summary>
        /// Writes a matrix to the given address with each row aligned to 16 bytes.
        /// </summary>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public unsafe void SetShaderParamter(string name, Matrix matrix)
        {
            var p = (float*) &matrix;
            SetShaderParamter(name, p + 0, 0, 12);
            SetShaderParamter(name, p + 3, 16, 12);
        }

        #endregion

        #region Surface

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public override Surface CreateSurface(int width, int height)
        {
            return Invoke(() => new GLFramebufferSurface(width, height));
        }

        public override Surface DefaultSurface => _defaultSurface;

        public override Surface Surface
        {
            get => _currentSurface;

            set
            {
                // Wasn't the same surface as before, flush everything
                if (value != _currentSurface)
                {
                    // Invoke the rest of the method on the rendering thread
                    Invoke(() =>
                    {
                        // 
                        Flush();

                        // Set and prepare the surface (ie, bind framebuffer)
                        _currentSurface = value as GLSurface;
                        _currentSurface.Prepare(this);
                    });
                }
            }
        }

        /// <summary>
        /// Mark the current surface dirty (increments version number).
        /// </summary>
        internal void MarkSurfaceDirty()
        {
            _currentSurface.UpdateVersionNumber();
        }

        #endregion

        #region View State

        public override Matrix Transform
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

        public override Matrix InverseTransform => _inverseViewMatrix;

        public override float ApproximatePixelScale => _approxPixelScale;

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
                    Invoke(() => SetViewportAndScissor(out var _, out var _));
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
                    });
                }
            }
        }

        #endregion

        #region Draw

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public override void Clear(Color color)
        {
            Invoke(() =>
            {
                // Set color and clear
                GL.SetClearColor(color.R, color.G, color.B, color.A);
                GL.Clear(ClearMask.Color);
            });
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public override void DrawMesh(ImageSource image, Mesh mesh, Matrix transform)
        {
            _renderer.Submit(image, mesh, in transform, _blendColor);
        }

        #endregion

        #region Capture

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public override unsafe Image GrabPixels(IntRectangle region)
        {
            return Invoke(() =>
            {
                // 
                Flush();

                // Grab pixels from framebuffer
                var image = new Image(region.Width, region.Height);
                image.SetPixels(GL.ReadPixels(region.X, region.Y, region.Width, region.Height));
                return image;
            });
        }

        #endregion

        #region Flush

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public override unsafe void Flush()
        {
            // If the renderer has batched work
            if (_renderer.IsDirty)
            {
                Invoke(() =>
                {
                    // 
                    SetViewportAndScissor(out var w, out var h);

                    // Compute view-projection matrix
                    var projMatrix = Matrix.RectangleProjection(0, 0, w, h);
                    Matrix.Multiply(in projMatrix, in _viewMatrix, ref projMatrix);

                    // Write into uniform buffer
                    SetShaderParamter("uMatrix", projMatrix);

                    // Synchronize GPU with operations before drawing
                    var sync = GL.FenceSync(SyncFenceCondition.SyncGpuCommandsComplete, 0);
                    GL.WaitSync(sync, 0); // Wait GPU for buffer writes, etc
                    GL.DeleteSync(sync);  // Remove sync object

                    // Draw batch
                    _renderer.Flush();
                });
            }
        }

        #endregion

        #region Dispose

        private void Dispose(bool disposeManaged)
        {
            if (!IsDisposed)
            {
                IsDisposed = true;

                // 
                if (disposeManaged)
                {
                    _thread.Stop(false);
                    _defaultSurface.Dispose();
                }

                // Dispose Unmanaged
            }
        }

        public override void Dispose()
        {
            Dispose(true);
            GC.SuppressFinalize(this);
        }

        #endregion

        public struct VersionInfo
        {
            public string Vendor;
            public string Renderer;

            public int Major;
            public int Minor;

            public bool IsEmbedded;

            public override string ToString()
            {
                if (IsEmbedded) { return $"OpenGL ES {Major}.{Minor} - {Vendor} - {Renderer}"; }
                else { return $"OpenGL {Major}.{Minor} - {Vendor} - {Renderer}"; }
            }
        }
    }
}
