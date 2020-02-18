using System;
using System.Collections.Generic;
using System.Runtime.CompilerServices;

using Heirloom.Math;

using Range = Heirloom.Math.Range;

namespace Heirloom.Drawing
{
    public abstract partial class Graphics
    {
        private static readonly Mesh _quadMesh = Mesh.CreateQuad(1, 1);

        private readonly Stack<GraphicsState> _stateStack;

        #region Constructors

        protected Graphics(MultisampleQuality multisample)
        {
            // 
            _stateStack = new Stack<GraphicsState>();

            // Create performance tracking object
            Performance = new DrawingPerformance();

            // Creates a dummy surface to represent the window surface
            DefaultSurface = new Surface(1, 1, multisample, false);
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

        /// <summary>
        /// Gets how often the default surface is presented to the screen per second.
        /// </summary>
        public float CurrentFPS => Performance.FrameRate.Average;

        /// <summary>
        /// Gets drawing performance information.
        /// </summary>
        public DrawingPerformance Performance { get; }

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
        /// Gets or sets the interpolation mode.
        /// </summary>
        public abstract InterpolationMode InterpolationMode { get; set; }

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
        /// Force pending drawing operations to complete, useful for synchronization between contexts. <para/>
        /// Note: Currently untested for said synchronization.
        /// </summary>
        public abstract void Flush();

        /// <summary>
        /// Present the drawing operations to the screen.
        /// </summary>
        public void RefreshScreen()
        {
            // Commit all pending work
            Flush();

            // Computes statistics (possibly drawing overlay)
            ProcessStatistics();

            // Causes the image to appear on screen
            SwapBuffers();

            // Mark the end of a frame
            EndFrame();
        }

        #region Frame Statistics

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        private void ProcessStatistics()
        {
            // Compute statistics (fps, batch count, etc)
            Performance.ComputeStatistics(GetDrawCounts());

            // Draw the statistics overlay
            DrawStatisticsOverlay();
        }

        protected abstract DrawCounts GetDrawCounts();

        private void DrawStatisticsOverlay()
        {
            if (Performance.OverlayMode != PerformanceOverlayMode.Disabled)
            {
                ResetState();

                // == Measure Step

                // Statistics overlay text
                var text = GetPerformanceOverlayText();

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

                // Have to flush again...
                Flush();
            }
        }

        private string GetPerformanceOverlayText()
        {
            // todo: Perhaps humanize the numbers such that "215,123" becomes "215K"
            //       to keep things short and easy to read.
            return Performance.OverlayMode switch
            {
                PerformanceOverlayMode.Simple =>
                    $"FPS : {Performance.FrameRate.Average:N0} ({Performance.BatchCount.Average})",

                PerformanceOverlayMode.Standard =>
                    $"Draws   : {Performance.DrawCount.Average,8:N0}\n" +
                    $"Batches : {Performance.BatchCount.Average,8:N0}\n" +
                    $"FPS     : {Performance.FrameRate.Average,8:N0}",

                PerformanceOverlayMode.Full =>
                    $"Draws   : {Performance.DrawCount.Average,8:N0} ± {Performance.DrawCount.Deviation,-8:N0}\n" +
                    $"Batches : {Performance.BatchCount.Average,8:N0} ± {Performance.BatchCount.Deviation,-8:N0}\n" +
                    $"FPS     : {Performance.FrameRate.Average,8:N0} ± {Performance.FrameRate.Deviation,-8:N0}",

                _ => throw new InvalidOperationException(),
            };
        }

        #endregion

        protected abstract void SwapBuffers();

        protected abstract void EndFrame();

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

        /// <summary>
        /// Sets <see cref="GlobalTransform"/> to mimic a 2D camera.
        /// </summary>
        public void SetCameraTransform(Vector center, float scale = 1F)
        {
            var offset = (Vector) ViewportScreen.Size / 2F;
            GlobalTransform = Matrix.CreateTransform(offset - center, 0, scale);
        }

        protected internal struct DrawCounts
        {
            public int BatchCount;
            public int DrawCount;
            public int TriangleCount;
        }
    }

    public sealed class DrawingPerformance
    {
        private readonly Timer _timer;

        private readonly StatisticsHelper _triangleCount;
        private readonly StatisticsHelper _batchCount;
        private readonly StatisticsHelper _drawCount;
        private readonly StatisticsHelper _frameRate;

        internal DrawingPerformance()
        {
            // 
            _triangleCount = new StatisticsHelper();
            _batchCount = new StatisticsHelper();
            _drawCount = new StatisticsHelper();
            _frameRate = new StatisticsHelper();

            // 
            _timer = new Timer(1F);
        }

        /// <summary>
        /// Gets or sets a value that will enable or disable drawing the performance overlay.
        /// </summary>
        public PerformanceOverlayMode OverlayMode { get; set; } = PerformanceOverlayMode.Disabled;

        public Statistics TriangleCount { get; private set; }

        public Statistics BatchCount { get; private set; }

        public Statistics DrawCount { get; private set; }

        public Statistics FrameRate { get; private set; }

        internal void ComputeStatistics(Graphics.DrawCounts info)
        {
            // If enough time has passed, compute average
            if (_timer.Tick(out _))
            {
                // Compute statistics
                TriangleCount = _triangleCount.Compute();
                BatchCount = _batchCount.Compute();
                DrawCount = _drawCount.Compute();
                FrameRate = _frameRate.Compute();

                // Clear samples
                _triangleCount.ClearSamples();
                _batchCount.ClearSamples();
                _drawCount.ClearSamples();
                _frameRate.ClearSamples();
            }

            // Record sample
            _triangleCount.AddSample(info.TriangleCount);
            _batchCount.AddSample(info.BatchCount);
            _drawCount.AddSample(info.DrawCount);
            _frameRate.AddSample(1F / _timer.Delta);
        }

        private sealed class StatisticsHelper
        {
            private float _sum, _squareSum;
            private Range _range;
            private int _count;

            public StatisticsHelper()
            {
                ClearSamples();
            }

            public void AddSample(float value)
            {
                // Accumulate value (sum values)
                _range.Include(value);
                _squareSum += value * value;
                _sum += value;

                // Count sample 
                _count++;
            }

            public void ClearSamples()
            {
                _range = Range.Indeterminate;
                _squareSum = _sum = 0;
                _count = 0;
            }

            public Statistics Compute()
            {
                return Statistics.Compute(_sum, _squareSum, _range, _count);
            }
        }
    }
}
