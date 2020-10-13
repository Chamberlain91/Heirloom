using System;
using System.Collections.Generic;
using System.Runtime.CompilerServices;

using Meadows.Mathematics;

namespace Meadows.Drawing
{
    public abstract class GraphicsContext : IDisposable
    {
        private static readonly RecyclePool<int> _idPool = new RecyclePool<int>(() => _idCounter++);
        private static int _idCounter;

        protected internal readonly int Id = _idPool.Request();

        protected StateDirtyFlags StateFlags = ~(StateDirtyFlags) 0;

        private readonly Stack<RenderState> _stateStack = new Stack<RenderState>();
        private RenderState _state;

        private bool _isDisposed;

        [Flags]
        protected enum StateDirtyFlags
        {
            Viewport = 1 << 0,
            Surface = 1 << 1,

            Interpolation = 1 << 2,
            Blending = 1 << 3,
            Shader = 1 << 4,

            Camera = 1 << 5,

            // 
            All = Viewport | Surface | Interpolation | Blending | Shader | Camera
        }

        protected struct RenderState
        {
            public InterpolationMode InterpolationMode;
            public BlendingMode BlendingMode;
            public Shader Shader;

            public IntRectangle Viewport;
            public Surface Surface;

            public Matrix Camera;

            public Color Color;
        }

        #region Constructor

        protected GraphicsContext(Screen screen)
        {
            Screen = screen ?? throw new ArgumentNullException(nameof(screen));

            // 
            SetDefaultState();
        }

        ~GraphicsContext()
        {
            Dispose(disposing: false);
        }

        #endregion

        public Screen Screen { get; }

        #region Render State (Properties)

        public InterpolationMode InterpolationMode
        {
            get => _state.InterpolationMode;

            set
            {
                if (_state.InterpolationMode != value)
                {
                    _state.InterpolationMode = value;
                    StateFlags |= StateDirtyFlags.Interpolation;
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

        public Shader Shader
        {
            get => _state.Shader;

            set
            {
                if (_state.Shader != value)
                {
                    _state.Shader = value;
                    StateFlags |= StateDirtyFlags.Shader;
                }
            }
        }

        protected Matrix CameraMatrix => _state.Camera;

        // todo: camera inverse matrix?

        public Color Color { get; set; }

        public IntRectangle Viewport => _state.Viewport;

        public Surface Surface => _state.Surface;

        #endregion

        #region Render State (Methods)

        public void SetCamera(Matrix matrix)
        {
            _state.Camera = matrix;
            StateFlags |= StateDirtyFlags.Camera;
        }

        public void SetCamera(Vector center, float scale = 1F, float rotation = 0F)
        {
            var offset = (Vector) Viewport.Size / (2F * scale);

            // Set translation
            _state.Camera = Matrix.CreateTranslation(offset - center);

            // If a scale is given, apply a scale transform
            if (!Calc.NearEquals(scale, 1F))
            {
                _state.Camera = Matrix.CreateScale(scale) * _state.Camera;
            }

            // If a rotation is given, apply a rotation transform
            if (!Calc.NearZero(rotation))
            {
                _state.Camera = Matrix.CreateRotation(rotation) * _state.Camera;
            }

            // Mark camera as dirty
            StateFlags |= StateDirtyFlags.Camera;
        }

        public virtual void SetRenderTarget(Surface surface, IntRectangle? viewport = null)
        {
            if (surface is null)
            {
                throw new ArgumentNullException(nameof(surface));
            }

            // If viewport is not specified, use the surface size.
            viewport ??= (IntVector.Zero, surface.Size);

            // Potentially update viewport state
            if (_state.Viewport != viewport.Value)
            {
                _state.Viewport = viewport.Value;
                StateFlags |= StateDirtyFlags.Viewport;
            }

            // Potentially update surface (render target)
            if (_state.Surface != surface)
            {
                _state.Surface = surface;
                StateFlags |= StateDirtyFlags.Surface;
            }
        }

        public void PushState()
        {
            _stateStack.Push(_state);
        }

        public void PopState()
        {
            var state = _stateStack.Pop();

            // Set prior render state
            InterpolationMode = state.InterpolationMode;
            BlendingMode = state.BlendingMode;
            Shader = state.Shader;

            // Set prior render target
            SetRenderTarget(state.Surface, state.Viewport);
            SetCamera(state.Camera);

            // 
            Color = state.Color;
        }

        protected void SetDefaultState()
        {
            InterpolationMode = InterpolationMode.Nearest;
            BlendingMode = BlendingMode.Alpha;
            Shader = Shader.Default;
            Color = Color.White;

            SetRenderTarget(Screen.Surface);
            SetCamera(Matrix.Identity);
        }

        #endregion

        #region Draw Methods

        public abstract void Clear(Color color);

        public abstract void Draw(Texture texture, in Rectangle uvRegion, in Mesh mesh, in Matrix matrix);

        #region Basic Image Drawing

        // draw mesh
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public void Draw(Texture texture, in Mesh mesh, in Matrix matrix)
        {
            Draw(texture, Rectangle.One, in mesh, in matrix);
        }

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
            Draw(texture, Mesh.QuadMesh, in transform);
        }

        // draw partial image
        public void DrawSubImage(Texture texture, IntRectangle region, Matrix matrix)
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
            Draw(texture, uvRect, Mesh.QuadMesh, in matrix);
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

        protected abstract object GenerateNativeResource(NativeResource resource);

        protected internal T GetNativeResource<T>(NativeResource resource) where T : class
        {
            var native = resource.GetNativeResource<T>(this);

            if (native == null)
            {
                // No native resource is known for this object, we must now create one
                native = GenerateNativeResource(resource) as T;
                if (native == null) { throw new InvalidOperationException("Context generated a native resource that does not match the requesting type!"); }
                SetNativeResource<T>(resource, native);
            }

            return native;
        }

        protected internal void SetNativeResource<T>(NativeResource resource, T native) where T : class
        {
            resource.SetNativeResource(this, native);
        }

        protected internal uint GetResourceVersion(NativeResource resource)
        {
            return resource.Version;
        }

        protected internal void IncrementResourceVersion(NativeResource resource)
        {
            resource.IncrementVersion();
        }

        #endregion
    }
}
