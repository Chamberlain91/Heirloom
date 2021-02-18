using System;
using System.Collections.Generic;
using System.Runtime.CompilerServices;

using Heirloom.Mathematics;

namespace Heirloom.Drawing
{
    public abstract partial class GraphicsContext : IDisposable
    /**
    * When a shader is created, it requests from an active context for it to be compiled.
    * This then can defer to whatever backend is running the context... this is to prevent a lazy compile of the shader.
    * 
    * Shader uniforms are apparently per-program, so once updated they should be the same across contexts. Only trick is making sure
    * synchronization is predictable (as most parts of the GL backend)
    */
    {
        private static readonly RecyclePool<int> _idPool = new RecyclePool<int>(() => _idCounter++);
        private static int _idCounter;

        protected internal readonly int Id = _idPool.Request();

        protected StateDirtyFlags StateFlags = StateDirtyFlags.All;

        private readonly Stack<RenderState> _stateStack = new Stack<RenderState>();
        private RenderState _state;

        private Matrix _compositeMatrix;

        private Matrix _inverseCompositeMatrix;
        private Matrix _inverseTransform;
        private Matrix _inverseCamera;

        private bool _compositeDirty;
        private bool _inverseCompositeDirty;
        private bool _inverseTransformDirty;
        private bool _inverseCameraDirty;

        private bool _isDisposed;

        [Flags]
        protected enum StateDirtyFlags
        {
            Viewport = 1 << 0,
            Surface = 1 << 1,

            Blending = 1 << 2,
            Shader = 1 << 3,

            // 
            All = Viewport | Surface | Blending | Shader
        }

        protected struct RenderState
        {
            public BlendingMode BlendingMode;
            public Shader Shader;

            public IntRectangle Viewport;
            public Surface Surface;

            public Matrix Transform;
            public Matrix CameraMatrix;

            public Color Color;

            public bool PixelPerfect;
        }

        private void MarkStateDirty(StateDirtyFlags flag)
        {
            if (StateFlags.HasFlag(flag) && HasPendingWork) { Flush(); }
            StateFlags |= flag;
        }

        #region Constructor

        protected GraphicsContext(GraphicsBackend backend, IScreen screen)
        {
            Backend = backend ?? throw new ArgumentNullException(nameof(backend));
            Screen = screen ?? throw new ArgumentNullException(nameof(screen));

            Performance = new GraphicsPerformance();
        }

        ~GraphicsContext()
        {
            Dispose(disposing: false);
        }

        #endregion

        public GraphicsPerformance Performance { get; }

        public IScreen Screen { get; }

        protected internal bool IsInitialized { get; protected set; }

        protected internal GraphicsBackend Backend { get; }

        protected abstract bool HasPendingWork { get; }

        internal bool IsDisposed => _isDisposed;

        public IShape ClipShape { get; set; }

        #region Render State (Properties)

        public float ApproximatePixelScale => Vector.GetMinComponent(1F / CompositeMatrix.GetAffineScale());

        public BlendingMode BlendingMode
        {
            get => _state.BlendingMode;

            set
            {
                if (_state.BlendingMode != value)
                {
                    MarkStateDirty(StateDirtyFlags.Blending);
                    _state.BlendingMode = value;
                }
            }
        }

        public Color Color
        {
            get => _state.Color;
            set => _state.Color = value;
        }

        public IntRectangle Viewport => _state.Viewport;

        public Surface Surface => _state.Surface;

        public Shader Shader => _state.Shader;

        #region Transform Matrix

        public Matrix Transform
        {
            get => _state.Transform;

            set
            {
                _state.Transform = value;

                // Mark respective matrices as dirty
                _inverseTransformDirty = true;
                _inverseCompositeDirty = true;
                _compositeDirty = true;
            }
        }

        public Matrix InverseTransform
        {
            get
            {
                if (_inverseTransformDirty)
                {
                    // Compute the inverse of the camera matrix
                    Matrix.Inverse(Transform, ref _inverseTransform);

                    // Clear dirty flag
                    _inverseTransformDirty = false;
                }

                return _inverseTransform;
            }
        }

        #endregion

        #region Camera Matrix

        public Matrix CameraMatrix
        {
            get => _state.CameraMatrix;
            set
            {
                _state.CameraMatrix = value;

                // Mark respective matrices as dirty
                _inverseCameraDirty = true;
                _inverseCompositeDirty = true;
                _compositeDirty = true;
            }
        }

        public Matrix InverseCameraMatrix
        {
            get
            {
                if (_inverseCameraDirty)
                {
                    // Compute the inverse of the camera matrix
                    Matrix.Inverse(CameraMatrix, ref _inverseCamera);

                    // Clear dirty flag
                    _inverseCameraDirty = false;
                }

                return _inverseCamera;
            }
        }

        #endregion

        #region Composite Matrix

        /// <summary>
        /// The combined camera and transform matrix. 
        /// </summary>
        public Matrix CompositeMatrix
        {
            get
            {
                if (_compositeDirty)
                {
                    // Compute new composite (camera * world) matrix.
                    Matrix.Multiply(CameraMatrix, Transform, ref _compositeMatrix);

                    // Clear dirty flag
                    _compositeDirty = false;
                }

                return _compositeMatrix;
            }
        }

        /// <summary>
        /// The inverse of <see cref="CompositeMatrix"/>.
        /// </summary>
        public Matrix InverseCompositeMatrix
        {
            get
            {
                if (_inverseCompositeDirty)
                {
                    // Compute the inverse of the composite matrix
                    Matrix.Inverse(CompositeMatrix, ref _inverseCompositeMatrix);

                    // Clear dirty flag
                    _inverseCompositeDirty = false;
                }

                return _inverseCompositeMatrix;
            }
        }

        #endregion

        #endregion

        #region Render State (Methods)

        public void SetCamera(Vector center, float scale = 1F, float rotation = 0F)
        {
            var offset = (Vector) Viewport.Size / (2F * scale);

            // Set translation
            var camera = Matrix.CreateTranslation(offset - center);

            // If a scale is given, apply a scale transform
            if (!Calc.NearEquals(scale, 1F))
            {
                camera = Matrix.CreateScale(scale) * camera;
            }

            // If a rotation is given, apply a rotation transform
            if (!Calc.NearZero(rotation))
            {
                camera *= Matrix.CreateRotation(rotation);
            }

            //
            CameraMatrix = camera;
        }

        private void SetViewport(IntRectangle viewport)
        {
            // Potentially update viewport state
            if (_state.Viewport != viewport)
            {
                // Mark viewport state as dirty
                MarkStateDirty(StateDirtyFlags.Viewport);
                _state.Viewport = viewport;
            }
        }

        public virtual void SetRenderTarget(Surface surface, IntRectangle? viewport = null)
        {
            if (surface is null)
            {
                throw new ArgumentNullException(nameof(surface));
            }

            // If viewport is not specified, use the surface size.
            viewport ??= (IntVector.Zero, surface.Size);
            SetViewport(viewport.Value);

            // Potentially update surface (render target)
            if (_state.Surface != surface)
            {
                MarkStateDirty(StateDirtyFlags.Surface);
                _state.Surface = surface;
            }
        }

        public virtual void UseShader(Shader shader)
        {
            if (shader is null) { throw new ArgumentNullException(nameof(shader)); }

            if (_state.Shader != shader)
            {
                MarkStateDirty(StateDirtyFlags.Shader);
                _state.Shader = shader;
            }
        }

        #region State Stack

        public void PushState()
        {
            _stateStack.Push(_state);
        }

        public void PopState()
        {
            var state = _stateStack.Pop();

            // Set prior blending and color
            BlendingMode = state.BlendingMode;
            Color = state.Color;

            // Restore prior shader
            UseShader(state.Shader);

            // Restore prior render target
            SetRenderTarget(state.Surface, state.Viewport);

            // Restore prior matrices
            CameraMatrix = state.CameraMatrix;
            Transform = state.Transform;
        }

        public void ResetState()
        {
            // todo: clear stencil?!

            // Set default blending and color
            BlendingMode = BlendingMode.Alpha;
            Color = Color.White;

            // Default shader configuration
            UseShader(Shader.Default);

            // Set default surface
            SetRenderTarget(Screen.Surface);

            // Set default matrices
            CameraMatrix = Matrix.Identity;
            Transform = Matrix.Identity;
        }

        #endregion

        #endregion

        #region Draw Methods

        public abstract void Clear(Color color);

        public abstract void Draw(Mesh mesh, Texture texture, Rectangle uvRegion, Matrix matrix);

        public abstract void SetUniform<T>(string name, T value);

        #region Fundamental Drawing

        /*[MethodImpl(MethodImplOptions.AggressiveInlining)]*/
        public void Draw(Mesh mesh, Texture texture, Rectangle uvRegion)
        {
            Draw(mesh, texture, uvRegion, Matrix.Identity);
        }

        /*[MethodImpl(MethodImplOptions.AggressiveInlining)]*/
        public void Draw(Mesh mesh, Texture texture, Matrix matrix)
        {
            Draw(mesh, texture, Rectangle.One, matrix);
        }

        /*[MethodImpl(MethodImplOptions.AggressiveInlining)]*/
        public void Draw(Mesh mesh, Texture texture)
        {
            Draw(mesh, texture, Rectangle.One);
        }

        #endregion

        #region Basic Image Drawing

        // draw image
        public unsafe void DrawImage(Texture texture, Matrix transform)
        {
            var w = (float) texture.Width;
            var h = (float) texture.Height;

            // Scale to image dimensions
            transform.M0 *= w;
            transform.M3 *= w;
            transform.M1 *= h;
            transform.M4 *= h;

            // Submit draw
            Draw(Mesh.QuadMesh, texture, transform);
        }

        /// <summary>
        /// Draws an image stretched to fill a rectangular region.
        /// </summary> 
        /// <param name="image">Some image.</param>
        /// <param name="rectangle">The bounds of the drawn image.</param>
        /*[MethodImpl(MethodImplOptions.AggressiveInlining)]*/
        public void DrawImage(Texture image, Rectangle rectangle)
        {
            var transform = Matrix.CreateTransform(rectangle.Position, 0, (Vector) rectangle.Size);
            Draw(Mesh.QuadMesh, image, transform);
        }

        // draw partial image
        public void DrawImage(Texture texture, IntRectangle region, Matrix matrix)
        {
            var w = (float) texture.Width;
            var h = (float) texture.Height;

            // Compute uv rectangle
            var uvRect = Rectangle.One;
            uvRect.X = region.X / w;
            uvRect.Y = region.Y / h;
            uvRect.Width = region.Width / w;
            uvRect.Height = region.Height / h;

            // Scale to image dimensions
            matrix.M0 *= w;
            matrix.M3 *= w;
            matrix.M1 *= h;
            matrix.M4 *= h;

            // Submit draw
            Draw(Mesh.QuadMesh, texture, uvRect, matrix);
        }

        #endregion

        #region Stencil

        // todo: possibly implement, should determine if the stencil was ever written to?
        // public abstract bool HasStencil { get; }

        public abstract void ClearStencil();

        public abstract void BeginStencil();

        public abstract void EndStencil();

        #endregion

        #endregion

        #region Read Methods

        /// <summary>
        /// Grab the pixels from a subregion of the current surface and return that image. (ie, a screenshot)
        /// </summary>
        /// <param name="region">A region within the currently set surface.</param>
        /// <returns>An image with a copy of the pixels on the surface.</returns>
        public abstract Image GrabPixels(IntRectangle region);

        /// <summary>
        /// Grab the pixels from the current surface and return that image. (ie, a screenshot)
        /// </summary>
        /// <returns>An image with a copy of the pixels on the surface.</returns>
        public Image GrabPixels()
        {
            return GrabPixels((0, 0, Surface.Width, Surface.Height));
        }

        #endregion

        protected void Initialize()
        {
            ResetState();
            IsInitialized = true;
        }

        internal void CompleteFrame()
        {
            // If performance overlay is visible, draw it now.
            if (Performance.ShowOverlay) { DrawPerformanceOverlay(); }

            // Flush pending work, the swap buffers call will
            // do the final block until the frame is actually drawn.
            Flush(block: false);

            // Computes per-frame statistics. Includes:
            // * average frames per second
            // * average batches per frame
            Performance.NotifyFrame();

            // Exchange back and front buffers, causing the image to appear on screen.
            SwapBuffers();
        }

        private void DrawPerformanceOverlay()
        {
            const float PaddingX = 8;
            const float PaddingY = 4;

            PushState();
            {
                ResetState();

                var anchor = new Vector(Screen.Surface.Width - 10 - PaddingX, Screen.Surface.Height - 10 - PaddingY);
                var text = $"FPS: {Performance.FPS,5:N1}\nBatches: {Performance.Batches,5:N1}";

                // Measure text
                var measure = TextLayout.Measure(text, Font.Default, 16);
                measure.Position += (anchor.X - measure.Width, anchor.Y - measure.Height);
                measure = Rectangle.Inflate(measure, PaddingX, PaddingY);

                // Draw frame
                Color = Color.DarkGray;
                DrawRect(measure);
                Color = Color.Black;
                DrawRectOutline(measure);

                // Draw text
                Color = Color.LightGray;
                this.DrawText(text, measure.BottomRight - (PaddingX, PaddingY), Font.Default, 16, TextAlign.Right | TextAlign.Bottom);
            }
            PopState();
        }

        /// <summary>
        /// Causes the back and front buffers to be swapped.
        /// </summary>
        protected abstract void SwapBuffers();

        /// <summary>
        /// Submit all pending drawing operations, optionally blocking for completion.
        /// </summary>
        protected abstract void Flush(bool block = false);

        /// <summary>
        /// Commits pending drawing operations, blocking until all operations complete.
        /// </summary>
        public void Commit()
        {
            Flush(true);
        }

        #region Dispose

        protected virtual void Dispose(bool disposing)
        {
            if (!_isDisposed)
            {
                if (disposing)
                {
                    // TODO: dispose managed state (managed objects)
                }

                // Recycle context id
                _idPool.Recycle(Id);
                _isDisposed = true;
            }
        }

        public void Dispose()
        {
            Dispose(disposing: true);
            GC.SuppressFinalize(this);
        }

        #endregion

        #region Native Resource Access

        protected abstract object GenerateNativeObject(GraphicsResource resource);

        protected internal T GetNativeObject<T>(GraphicsResource resource) where T : class, IDisposable
        {
            if (resource.GetContextNativeObject(this) is not T obj)
            {
                // No context native object is known for this resources, we must now create one.
                obj = GenerateNativeObject(resource) as T;
                if (obj == null) { throw new InvalidOperationException("Generated a context native object that does not match the requested type!"); }

                // Store the context object for next time
                SetNativeObject(resource, obj);
            }

            return obj;
        }

        protected internal void SetNativeObject<T>(GraphicsResource resource, T obj) where T : class, IDisposable
        {
            resource.SetContextNativeObject(this, obj);
        }

        protected internal uint GetResourceVersion(GraphicsResource resource)
        {
            return resource.Version;
        }

        protected internal void IncrementVersion(GraphicsResource resource)
        {
            resource.IncrementVersion();
        }

        #endregion
    }
}
