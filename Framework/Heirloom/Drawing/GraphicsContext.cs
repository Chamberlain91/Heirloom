using System;
using System.Collections.Generic;

using Heirloom.Collections;
using Heirloom.Mathematics;

namespace Heirloom.Drawing
{
    /// <summary>
    /// Represents a graphical context, providing the ability to draw onto surfaces.
    /// </summary>
    public abstract partial class GraphicsContext : IDisposable
    {
        private static readonly IdentifierPool _idPool = new IdentifierPool();

        /// <summary>
        /// The unique integer representing this graphics context.
        /// </summary>
        internal readonly int Id = _idPool.GetNextIdentifier();

        internal StateDirtyFlags StateFlags = StateDirtyFlags.All;

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
        internal enum StateDirtyFlags
        {
            Viewport = 1 << 0,
            Surface = 1 << 1,

            Blending = 1 << 2,
            Shader = 1 << 3,

            // 
            All = Viewport | Surface | Blending | Shader
        }

        internal struct RenderState
        {
            public BlendingMode BlendingMode;
            public Shader Shader;

            public IntRectangle Viewport;
            public Surface Surface;

            public Matrix Transform;
            public Matrix CameraMatrix;

            public Color Color;

            public IShape ClipShape;
        }

        #region Constructor

        internal GraphicsContext(GraphicsBackend backend, IScreen screen)
        {
            Backend = backend ?? throw new ArgumentNullException(nameof(backend));
            Screen = screen ?? throw new ArgumentNullException(nameof(screen));

            Performance = new GraphicsPerformance();
        }

        /// <inheritdoc/>
        ~GraphicsContext()
        {
            Dispose(disposing: false);
        }

        #endregion

        /// <summary>
        /// Gets render performance information.
        /// </summary>
        public GraphicsPerformance Performance { get; }

        /// <summary>
        /// Gets the screen associated with the <see cref="GraphicsContext"/>.
        /// </summary>
        public IScreen Screen { get; }

        internal bool IsInitialized { get; set; }

        internal GraphicsBackend Backend { get; }

        internal abstract bool HasPendingWork { get; }

        internal bool IsDisposed => _isDisposed;

        #region Render State (Properties)

        /// <summary>
        /// Gets the approximate scale of a pixel compared to default.
        /// </summary>
        public float ApproximatePixelScale => Vector.GetMinComponent(1F / CompositeMatrix.GetAffineScale());

        /// <summary>
        /// Gets or sets the current blending mode.
        /// </summary>
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

        /// <summary>
        /// Gets the current viewport.
        /// </summary>
        public IntRectangle Viewport => _state.Viewport;

        /// <summary>
        /// Gets the current surface.
        /// </summary>
        public Surface Surface => _state.Surface;

        /// <summary>
        /// Gets or sets the current shader program applied to drawing operations.
        /// </summary>
        public Shader Shader
        {
            get => _state.Shader;

            set
            {
                if (value is null) { throw new ArgumentNullException(nameof(value)); }

                if (_state.Shader != value)
                {
                    MarkStateDirty(StateDirtyFlags.Shader);
                    _state.Shader = value;
                }
            }
        }

        #region Transform Matrix

        /// <summary>
        /// Gets or sets the global transform.
        /// </summary>
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

        /// <summary>
        /// Gets the inverse of the global tranform.
        /// </summary>
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

        /// <summary>
        /// Gets or sets the camera transform matrix.
        /// </summary>
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

        /// <summary>
        /// Gets the inverse of the camera transform matrix.
        /// </summary>
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

        /// <summary>
        /// Gets or sets the software clipping shape. Set this value to <see langword="null"/> to disable software clipping.
        /// </summary>
        public IShape ClipShape
        {
            get => _state.ClipShape;
            set => _state.ClipShape = value;
        }

        /// <summary>
        /// Gets or sets the current draw color.
        /// </summary>
        public Color Color
        {
            get => _state.Color;
            set => _state.Color = value;
        }

        #endregion

        #region Render State (Methods)

        /// <summary>
        /// Configures <see cref="CameraMatrix"/> to mimic a 2D camera centered on some location.
        /// </summary>
        /// <param name="center">Some location for the camera to look at.</param>
        /// <param name="scale">The scaling factor to apply. For example, <c>0.5</c> is "zoomed out".</param>
        /// <param name="rotation">The rotation of the camera, in radians.</param>
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

        /// <summary>
        /// Changes the surface this graphics context will draw onto.
        /// </summary>
        /// <param name="surface">Some surface.</param>
        /// <param name="viewport">The viewport (think split screen) or null to use the full surface.</param>
        public virtual void SetSurface(Surface surface, IntRectangle? viewport = null)
        {
            if (surface is null) { throw new ArgumentNullException(nameof(surface)); }

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

        #region State Stack

        /// <summary>
        /// Push the current render state onto the stack.
        /// Please note, this does not include the stencil.
        /// </summary>
        public void PushState()
        {
            _stateStack.Push(_state);
        }

        /// <summary>
        /// Pops the prior render state off the stack, restoring its configuration.
        /// </summary>
        public void PopState()
        {
            var state = _stateStack.Pop();

            // Set prior blending and color
            BlendingMode = state.BlendingMode;
            Color = state.Color;

            // Restore prior shader
            Shader = state.Shader;

            // Restore prior render target
            SetSurface(state.Surface, state.Viewport);

            // Restore prior matrices
            CameraMatrix = state.CameraMatrix;
            Transform = state.Transform;
        }

        /// <summary>
        /// Resets the render state to defaults.
        /// </summary>
        public void ResetState()
        {
            // todo: clear stencil?!

            // Set default blending and color
            BlendingMode = BlendingMode.Alpha;
            Color = Color.White;

            // Default shader configuration
            Shader = Shader.Default;

            // Set default surface
            SetSurface(Screen.Surface);

            // Set default matrices
            CameraMatrix = Matrix.Identity;
            Transform = Matrix.Identity;
        }

        #endregion

        #endregion

        #region Draw Methods

        /// <summary>
        /// Clear the currently bound surface with the specified color.
        /// </summary>
        public abstract void Clear(Color color);

        /// <summary>
        /// Draws the specified mesh and texture to the currently bound surface.
        /// </summary>
        /// <param name="mesh">Some mesh to draw.</param>
        /// <param name="texture">Some texture to apply to the mesh.</param>
        /// <param name="uvRegion">Some subregion of the texture to remap UV coordinates.</param>
        /// <param name="transform">Some transformation matrix to apply before rendering the mesh.</param>
        public abstract void Draw(Mesh mesh, Texture texture, Rectangle uvRegion, Matrix transform);

        #region Fundamental Drawing

        /// <summary>
        /// Draws the specified mesh and texture to the currently bound surface.
        /// </summary>
        /// <param name="mesh">Some mesh to draw.</param>
        /// <param name="texture">Some texture to apply to the mesh.</param>
        /// <param name="uvRegion">Some subregion of the texture to remap UV coordinates.</param>
        public void Draw(Mesh mesh, Texture texture, Rectangle uvRegion)
        {
            Draw(mesh, texture, uvRegion, Matrix.Identity);
        }

        /// <summary>
        /// Draws the specified mesh and texture to the currently bound surface.
        /// </summary>
        /// <param name="mesh">Some mesh to draw.</param>
        /// <param name="texture">Some texture to apply to the mesh.</param>
        /// <param name="transform">Some transformation matrix to apply before rendering the mesh.</param>
        public void Draw(Mesh mesh, Texture texture, Matrix transform)
        {
            Draw(mesh, texture, Rectangle.One, transform);
        }

        /// <summary>
        /// Draws the specified mesh and texture to the currently bound surface.
        /// </summary>
        /// <param name="mesh">Some mesh to draw.</param>
        /// <param name="texture">Some texture to apply to the mesh.</param>
        public void Draw(Mesh mesh, Texture texture)
        {
            Draw(mesh, texture, Rectangle.One);
        }

        #endregion

        #region Basic Image Drawing

        /// <summary>
        /// Draws an image to the currently bound surface.
        /// </summary>
        /// <param name="texture">Some image.</param>
        /// <param name="transform">Some transformation matrix to apply before rendering the image.</param>
        public unsafe void DrawImage(Texture texture, Matrix transform)
        {
            if (texture is null) { throw new ArgumentNullException(nameof(texture)); }

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
        /// Draws an image stretched to fill a rectangular region to the currently bound surface.
        /// </summary> 
        /// <param name="texture">Some image.</param>
        /// <param name="rectangle">The bounds of the drawn image.</param>
        public void DrawImage(Texture texture, Rectangle rectangle)
        {
            var transform = Matrix.CreateTransform(rectangle.Position, 0, (Vector) rectangle.Size);
            Draw(Mesh.QuadMesh, texture, transform);
        }

        /// <summary>
        /// Draws a subregion of an image to the currently bound surface.
        /// </summary>
        /// <param name="texture">Some image.</param>
        /// <param name="region">Some subregion of the image (specified in pixels).</param>
        /// <param name="transform">Some transformation matrix to apply before rendering the image.</param>
        public void DrawImage(Texture texture, IntRectangle region, Matrix transform)
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
            transform.M0 *= w;
            transform.M3 *= w;
            transform.M1 *= h;
            transform.M4 *= h;

            // Submit draw
            Draw(Mesh.QuadMesh, texture, uvRect, transform);
        }

        #endregion

        #region Stencil

        // todo: possibly implement, should determine if the stencil was ever written to?
        // public abstract bool HasStencil { get; }

        /// <summary>
        /// Clears the stencil mask, effectively disabling it.
        /// </summary>
        public abstract void ClearMask();

        /// <summary>
        /// Enables definition of the stencil mask, used to render "cookie cutter" style.
        /// </summary>
        /// <param name="alphaCutoff">The alpha threshold to discard rendered fragments.</param>
        public abstract void BeginDefineMask(float alphaCutoff = 1 / 200F);

        /// <summary>
        /// Ends definition of the stencil mask, subsequent drawing operations are subject to this mask.
        /// </summary>
        public abstract void EndDefineMask();

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

        /// <summary>
        /// Commits pending drawing operations, blocking until all operations complete.
        /// </summary>
        public void Commit()
        {
            Flush(true);
        }

        #region Internal Methods

        internal void Initialize()
        {
            ResetState();
            IsInitialized = true;
        }

        private void MarkStateDirty(StateDirtyFlags flag)
        {
            if (StateFlags.HasFlag(flag) && HasPendingWork) { Flush(); }
            StateFlags |= flag;
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
                var text = $"FPS: {Performance.AverageFrameRate,5:N1}\nBatches: {Performance.Batches,5:N1}";

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
                this.DrawText(text, measure.BottomRight - (PaddingX, PaddingY),  Font.Default, 16, TextAlign.Right | TextAlign.Bottom);
            }
            PopState();
        }

        /// <summary>
        /// Causes the back and front buffers to be swapped.
        /// </summary>
        internal abstract void SwapBuffers();

        /// <summary>
        /// Submit all pending drawing operations, optionally blocking for completion.
        /// </summary>
        internal abstract void Flush(bool block = false);

        #endregion

        #region Dispose

        internal virtual void Dispose(bool disposing)
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

        /// <summary>
        /// Dispose of this graphical context, cleaning up resources, effectively discarding it.
        /// </summary>
        public void Dispose()
        {
            Dispose(disposing: true);
            GC.SuppressFinalize(this);
        }

        #endregion

        #region Native Resource Access

        internal abstract object GenerateNativeObject(GraphicsResource resource);

        internal T GetNativeObject<T>(GraphicsResource resource) where T : class, IDisposable
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

        internal void SetNativeObject<T>(GraphicsResource resource, T obj) where T : class, IDisposable
        {
            resource.SetContextNativeObject(this, obj);
        }

        internal uint GetResourceVersion(GraphicsResource resource)
        {
            return resource.Version;
        }

        internal void IncrementVersion(GraphicsResource resource)
        {
            resource.IncrementVersion();
        }

        #endregion
    }
}
