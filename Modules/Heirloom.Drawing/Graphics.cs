using System;
using System.Collections.Generic;
using System.Diagnostics;

using Heirloom.Math;

namespace Heirloom.Drawing
{
    public abstract partial class Graphics
    {
        private const float FpsSampleDuration = 1F;

        private static readonly Mesh _quadMesh = Mesh.CreateQuad(1, 1);

        // graphics state stack
        private readonly Stack<GraphicsState> _stateStack;

        // framerate tracking
        private readonly Stopwatch _stopwatch;
        private float _fpsTime;
        private int _fpsCount;
          
        #region Constructors

        protected Graphics(GraphicsAdapter graphicsAdapter, MultisampleQuality multisample)
        {
            GraphicsAdapter = graphicsAdapter;

            _stateStack = new Stack<GraphicsState>();
            _stopwatch = Stopwatch.StartNew();

            DefaultSurface = new Surface(1, 1, multisample);
        }

        ~Graphics()
        {
            // Do not change this code. Put cleanup code in Dispose(bool disposing) above.
            Dispose(false);
        }

        #endregion

        #region Properties

        internal GraphicsAdapter GraphicsAdapter { get; }

        /// <summary>
        /// Gets the queried capabilities (ie, limits) for the current device.
        /// </summary>
        public GraphicsCapabilities Capabilities => GraphicsAdapter.Capabilities;

        /// <summary>
        /// Gets a value determining if this <see cref="Graphics"/> was disposed.
        /// </summary>
        public bool IsDisposed { get; private set; } = false;

        /// <summary>
        /// Gets how often the default surface is presented to the screen per second.
        /// </summary>
        public float CurrentFPS { get; private set; }

        /// <summary>
        /// Gets or sets a value that will enable or disable drawing the FPS overlay.
        /// </summary>
        public bool EnableFPSOverlay { get; set; } = false;

        /// <summary>
        /// Gets the default surface (ie, window) of this render context.
        /// </summary>
        public Surface DefaultSurface { get; }

        /// <summary>
        /// Gets or sets the current surface.
        /// </summary>
        public abstract Surface Surface { get; set; }

        /// <summary>
        /// Gets or sets the active shader.
        /// </summary>
        public abstract Shader Shader { get; set; }

        /// <summary>
        /// Gets or sets the viewport in normalized coordinates.
        /// </summary>
        public abstract Rectangle Viewport { get; set; }

        /// <summary>
        /// Get or sets the global transform.
        /// </summary>
        public abstract Matrix GlobalTransform { get; set; }

        /// <summary>
        /// Gets the inverse of the current global transform.
        /// </summary>
        public abstract Matrix InverseGlobalTransform { get; }

        /// <summary>
        /// Gets or sets the current blending mode.
        /// </summary>
        public abstract Blending Blending { get; set; }

        /// <summary>
        /// Gets or sets the current blending color.
        /// </summary>
        public abstract Color Color { get; set; }

        #endregion

        protected void SetDefaultSurfaceSize(IntSize size)
        {
            DefaultSurface.SetSize(size);
        }

        #region State Methods

        /// <summary>
        /// Reset current context state to defaults (default surface, full viewport, no transform, alpha and white).
        /// </summary>
        public void ResetState()
        {
            Shader = Shader.Default;
            Surface = DefaultSurface; // also adjusts viewport?
            Viewport = (0, 0, 1, 1);

            GlobalTransform = Matrix.Identity;
            Blending = Blending.Alpha;
            Color = Color.White;
        }

        /// <summary>
        /// Save the context state (push it on the state stack).
        /// </summary>
        public void PushState()
        {
            _stateStack.Push(new GraphicsState
            {
                Shader = Shader,
                Blending = Blending,
                Color = Color,
                Surface = Surface,
                Transform = GlobalTransform,
                Viewport = Viewport
            });
        }

        /// <summary>
        /// Restore the context state (pop from the state stack).
        /// </summary>
        public void PopState()
        {
            if (_stateStack.Count == 0)
            {
                // todo: Should this throw an exception instead?
                ResetState();
            }
            else
            {
                // Recover state values
                var state = _stateStack.Pop();

                Shader = state.Shader;
                Surface = state.Surface;
                Viewport = state.Viewport;
                GlobalTransform = state.Transform;
                Blending = state.Blending;
                Color = state.Color;
            }
        }

        #endregion

        #region Draw Methods

        /// <summary>
        /// Clears the current surface with the specified color.
        /// </summary>
        public abstract void Clear(in Color color);

        /// <summary>
        /// Draws a mesh with the given image to the current surface.
        /// </summary>
        /// <param name="mesh">Some mesh.</param>
        /// <param name="image">Some image.</param>
        /// <param name="transform">Some transform.</param>
        public abstract void DrawMesh(ImageSource image, Mesh mesh, in Matrix transform);

        #endregion

        #region Read Methods

        /// <summary>
        /// Grab the pixels from a subregion of the current surface and return that image. (ie, a screenshot)
        /// </summary>
        /// <param name="region">A region within the currently set surface.</param>
        /// <returns>An image with a copy of the pixels on the surface.</returns>
        public abstract Image GrabPixels(in IntRectangle region);

        /// <summary>
        /// Grab the pixels from the current surface and return that image. (ie, a screenshot)
        /// </summary>
        /// <returns>An image with a copy of the pixels on the surface.</returns>
        public Image GrabPixels()
        {
            return GrabPixels((0, 0, Surface.Width, Surface.Height));
        }

        #endregion

        protected static object GetNativeObject(Shader shader)
        {
            return shader.Native;
        }

        protected static object GetNativeObject(IDrawingResource resource)
        {
            return resource.NativeObject;
        }

        protected static bool HasDirtyUniform(Shader shader)
        {
            return shader.IsAnyUniformDirty;
        }

        /// <summary>
        /// Iterates over each mutated uniform in this shader and clears their dirty flag.
        /// </summary>
        protected static IEnumerable<(string name, object value)> EnumerateAndResetDirtyUniforms(Shader shader)
        {
            foreach (var (name, uniform) in shader.UniformStorageMap)
            {
                if (uniform.IsDirty)
                {
                    yield return (name, uniform.Value);
                }

                uniform.IsDirty = false;
            }

            // Mark shaders globally as processed
            shader.IsAnyUniformDirty = false;
        }

        /// <summary>
        /// Gets every uniform on this shader.
        /// </summary>
        protected static IEnumerable<(string name, object value)> GetUniforms(Shader shader)
        {
            foreach (var (name, uniform) in shader.UniformStorageMap)
            {
                yield return (name, uniform.Value);
            }
        }

        /// <summary>
        /// Present the drawing operations to the screen.
        /// </summary>
        public void RefreshScreen()
        {
            ComputeFPS();
            DrawFPSOverlay();
            Flush();

            // Low level swap buffers
            SwapBuffers();
        }
         
        protected abstract void SwapBuffers();

        /// <summary>
        /// Force pending drawing operations to complete, useful for synchronization between contexts. <para/>
        /// Note: Currently untested for said synchronization.
        /// </summary>
        public abstract void Flush();

        /// <summary>
        /// Updates the current surfaces version number.
        /// </summary>
        protected void UpdateSurfaceVersionNumber()
        {
            Surface.UpdateVersionNumber();
        }

        private void DrawFPSOverlay()
        {
            if (EnableFPSOverlay)
            {
                ResetState();

                var text = $"FPS: {CurrentFPS.ToString("0.00")}";
                var size = TextLayout.Measure(text, Font.Default, 16);

                Color = Color.DarkGray;
                DrawRect(new Rectangle(Surface.Width - 8 - size.Width - 3, 8, size.Width + 4, size.Height + 1));

                Color = Color.Pink;
                DrawText(text, new Vector(Surface.Width - 8, 8), Font.Default, 16, TextAlign.Right);
            }
        }

        private void ComputeFPS()
        {
            // Get elapsed time
            var delta = (float) _stopwatch.Elapsed.TotalSeconds;
            _stopwatch.Restart();

            _fpsTime += delta;
            _fpsCount++;

            if (_fpsTime >= FpsSampleDuration)
            {
                // hz, events/time
                CurrentFPS = _fpsCount / _fpsTime;

                _fpsCount = 0;
                _fpsTime = 0;
            }
        }

        #region IDisposable Support

        protected virtual void Dispose(bool disposing)
        {
            if (!IsDisposed)
            {
                if (disposing)
                {
                    // Managed
                }

                // Unmanaged

                IsDisposed = true;
            }
        }

        /// <summary>
        /// Dispose this graphics context, freeing any resources occupied by it.
        /// </summary>
        public void Dispose()
        {
            Dispose(true);
            GC.SuppressFinalize(this);
        }

        #endregion
    }
}
