using System;
using System.Collections.Generic;
using System.Runtime.CompilerServices;

using Meadows.Mathematics;

namespace Meadows.Drawing
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
        private static readonly RecyclePool<int> _idPool = new(() => _idCounter++);
        private static int _idCounter;

        protected internal readonly int Id = _idPool.Request();

        protected StateDirtyFlags StateFlags = StateDirtyFlags.All;

        private readonly Stack<RenderState> _stateStack = new();
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

            PixelPerfect = 1 << 4,

            // 
            All = Viewport | Surface | Blending | Shader | PixelPerfect
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

        #region Constructor

        protected GraphicsContext(GraphicsBackend backend, Screen screen)
        {
            Backend = backend ?? throw new ArgumentNullException(nameof(backend));
            Screen = screen ?? throw new ArgumentNullException(nameof(screen));

            Performance = new GraphicsPerformance();

            // 
            ResetState();
        }

        ~GraphicsContext()
        {
            Dispose(disposing: false);
        }

        #endregion

        public GraphicsPerformance Performance { get; }

        internal abstract bool IsInitialized { get; }

        internal bool IsDisposed => _isDisposed;

        protected internal GraphicsBackend Backend { get; }

        public Screen Screen { get; }

        #region Render State (Properties)

        public float ApproximatePixelScale
        {
            get
            {
                var scaleVec = 2F / CompositeMatrix.GetAffineScale();
                return Vector.GetMinComponent(scaleVec / (Vector) Viewport.Size);
            }
        }

        public bool PixelPerfect
        {
            get => _state.PixelPerfect;

            set
            {
                if (_state.PixelPerfect != value)
                {
                    _state.PixelPerfect = value;
                    StateFlags |= StateDirtyFlags.PixelPerfect;
                }
            }
        }

        public BlendingMode BlendingMode
        {
            get => _state.BlendingMode;

            set
            {
                if (_state.BlendingMode != value)
                {
                    _state.BlendingMode = value;
                    StateFlags |= StateDirtyFlags.Blending;
                }
            }
        }

        public Color Color { get; set; }

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

        public Matrix CompositeMatrix
        {
            get
            {
                if (_compositeDirty)
                {
                    // Compute new composite (proj * camera * world) matrix.
                    Matrix.Multiply(CameraMatrix, Transform, ref _compositeMatrix);

                    // Clear dirty flag
                    _compositeDirty = false;
                }

                return _compositeMatrix;
            }
        }

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

        private void SetViewport(IntRectangle viewport)
        {
            // Potentially update viewport state
            if (_state.Viewport != viewport)
            {
                _state.Viewport = viewport;

                // Mark viewport, projection matrix and transform matrix as dirty
                StateFlags |= StateDirtyFlags.Viewport;
            }
        }

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
                _state.Surface = surface;

                // Mark surface state dirty
                StateFlags |= StateDirtyFlags.Surface;
            }
        }

        public virtual void UseShader(Shader shader)
        {
            if (shader is null) { throw new ArgumentNullException(nameof(shader)); }

            if (_state.Shader != shader)
            {
                _state.Shader = shader;

                // Mark shader state dirty
                StateFlags |= StateDirtyFlags.Shader;
            }
        }

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
            // todo: pixel snapping

            // Restore prior render target
            SetRenderTarget(state.Surface, state.Viewport);

            // Restore prior matrices
            CameraMatrix = state.CameraMatrix;
            Transform = state.Transform;
        }

        public void ResetState()
        {
            // Set default blending and color
            BlendingMode = BlendingMode.Alpha;
            Color = Color.White;

            // Default shader configuration
            UseShader(Shader.Default);
            // todo: pixel snapping

            // Set default surface
            SetRenderTarget(Screen.Surface);

            // Set default matrices
            CameraMatrix = Matrix.Identity;
            Transform = Matrix.Identity;
        }

        #endregion

        #region Draw Methods

        public abstract void Clear(Color color);

        public abstract void Draw(Mesh mesh, Texture texture, Rectangle uvRegion, Matrix matrix);

        public abstract void SetUniform<T>(string name, T value);

        #region Fundamental Drawing

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public void Draw(Mesh mesh, Texture texture, Rectangle uvRegion)
        {
            Draw(mesh, texture, uvRegion, Matrix.Identity);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public void Draw(Mesh mesh, Texture texture, Matrix matrix)
        {
            Draw(mesh, texture, Rectangle.One, matrix);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
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
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public void DrawImage(Texture image, in Rectangle rectangle)
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

        internal void CompleteFrame()
        {
            // Flush pending work, the swap buffers call will
            // do the final block until the frame is actually drawn.
            Flush(block: false);

            // todo: compute per-frame statistics, etc
            Performance.NotifyFrame();

            // Exchange back and front buffers, causing the image to appear on screen.
            SwapBuffers();
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
