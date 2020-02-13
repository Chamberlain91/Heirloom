using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Runtime.CompilerServices;

using Heirloom.Math;

namespace Heirloom.Drawing
{
    public abstract partial class Graphics
    {
        private static readonly Mesh _quadMesh = Mesh.CreateQuad(1, 1);

        // graphics state stack
        private readonly Stack<GraphicsState> _stateStack;

        // statistics
        private readonly FrequencyCounter _framerate = new FrequencyCounter(1F);
        private readonly Stopwatch _statisticsStopwatch = new Stopwatch();
        private float _statisticsTimer;

        private FrameStatistics<float> _avgStatistics;
        private FrameStatistics<int> _accStatistics;
        private FrameStatistics<int> _statistics;

        #region Constructors

        protected Graphics(MultisampleQuality multisample)
        {
            _stateStack = new Stack<GraphicsState>();

            // Creates a dummy surface to represent the window surface
            DefaultSurface = new Surface(1, 1, multisample, false);

            // Begin measuring time.
            _statisticsStopwatch.Start();
        }

        ~Graphics()
        {
            // Do not change this code. Put cleanup code in Dispose(bool disposing) above.
            Dispose(false);
        }

        #endregion

        #region Properties

        /// <summary>
        /// Gets the queried capabilities (ie, limits) for the current device.
        /// </summary>
        public GraphicsCapabilities Capabilities => GraphicsAdapter.Capabilities;

        public FrameStatistics<float> AverageStatistics => _avgStatistics;

        public FrameStatistics<int> Statistics => _statistics;

        /// <summary>
        /// Gets how often the default surface is presented to the screen per second.
        /// </summary>
        public float CurrentFPS => _framerate.Average;

        /// <summary>
        /// Gets or sets a value that will enable or disable drawing the FPS overlay.
        /// </summary>
        public bool EnableStatisticsOverlay { get; set; } = false;

        /// <summary>
        /// Gets a value determining if this <see cref="Graphics"/> was disposed.
        /// </summary>
        public bool IsDisposed { get; private set; } = false;

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
        /// Gets the size of viewport in pixel coordinates.
        /// </summary>
        public abstract IntRectangle ViewportScreen { get; set; }

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
        public abstract void Clear(Color color);

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
        /// Sets <see cref="GlobalTransform"/> to mimic a 2D camera.
        /// </summary>
        public void SetCameraTransform(Vector center, float scale = 1F)
        {
            var offset = (Vector) ViewportScreen.Size / 2F;
            GlobalTransform = Matrix.CreateTransform(offset - center, 0, scale);
        }

        /// <summary>
        /// Present the drawing operations to the screen.
        /// </summary>
        public void RefreshScreen()
        {
            // 
            ProcessStatistics();

            // Commit all pending work
            Flush();

            // Causes the image to appear on screen
            SwapBuffers();
        }

        protected abstract void SwapBuffers();

        /// <summary>
        /// Force pending drawing operations to complete, useful for synchronization between contexts. <para/>
        /// Note: Currently untested for said synchronization.
        /// </summary>
        public abstract void Flush();

        #region Frame Statistics

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        private void ProcessStatistics()
        {
            // Gets the number of things drawn this frame
            GetFrameStatistics(ref _statistics);

            // Compute statistics (fps, batch count, etc)
            ComputeStatistics();

            // Draw the statistics overlay
            DrawStatisticsOverlay();

            // Reset this frames statistics
            ResetFrameStatistics();
        }

        protected abstract void GetFrameStatistics(ref FrameStatistics<int> statistics);

        protected abstract void ResetFrameStatistics();

        private void ComputeStatistics()
        {
            // Track Framerate
            _framerate.Tick();

            // Get the amount of time between frames
            var delta = _statisticsStopwatch.ElapsedTicks / (float) Stopwatch.Frequency;
            _statisticsStopwatch.Restart();

            // Accumulate statistics for average
            _accStatistics.BatchCount += _statistics.BatchCount;
            _accStatistics.DrawCount += _statistics.DrawCount;
            _accStatistics.TriCount += _statistics.TriCount;

            // Advance timer
            _statisticsTimer += delta;

            // If enough time has passed, compute average
            if (_statisticsTimer >= 1F)
            {
                // We want to wait another second
                _statisticsTimer = (_statisticsTimer - 1F) % 1F;

                // Computes average statistics
                _avgStatistics.BatchCount = _accStatistics.BatchCount / (float) _framerate.Samples;
                _avgStatistics.DrawCount = _accStatistics.DrawCount / (float) _framerate.Samples;
                _avgStatistics.TriCount = _accStatistics.TriCount / (float) _framerate.Samples;

                // Reset accumulated statistics
                _accStatistics.Reset();
            }
        }

        private void DrawStatisticsOverlay()
        {
            if (EnableStatisticsOverlay)
            {
                ResetState();

                // == Measure Step

                // Statistics overlay text
                // todo: Perhaps humanize the numbers such that "215,123" becomes "215K"
                //       to keep things short and easy to read.
                var text = $"Draws   : {Statistics.DrawCount:N0}\n" +
                           $"Batches : {Statistics.BatchCount:N0}\n" +
                           $"FPS     : {CurrentFPS:N2}";

                // Measure the rect needed to fit the text
                var textSize = TextLayout.Measure(text, Font.Default, 16);
                textSize.Inflate(8, 4);

                // Compute the text box at the top right of the screen
                var textBox = new Rectangle(Surface.Width - textSize.Width - 8, 8, textSize.Width, textSize.Height);

                // == Draw Step

                Color = CurrentFPS < 30 ? Color.Red : Color.DarkGray;

                // Draw textbox background
                DrawRect(textBox);

                Color = CurrentFPS < 30 ? Color.Black : Color.Pink;

                // Draw textbox border
                DrawRectOutline(textBox, 2);

                Color = CurrentFPS < 30 ? Color.Black : Color.Pink;

                // Draw text
                DrawText(text, new Vector(Surface.Width - textSize.Width, 12), Font.Default, 16);
            }
        }

        #endregion

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
