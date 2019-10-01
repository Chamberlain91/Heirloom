using System;
using System.Diagnostics;
using System.Threading;

using Heirloom.Drawing;
using Heirloom.GLFW;
using Heirloom.Math;

namespace Heirloom.Desktop
{
    public class RenderLoop
    {
        public delegate void UpdateFunction(RenderContext ctx, float dt);

        private readonly UpdateFunction _update;
        private Thread _thread;

        public RenderLoop(RenderContext context, UpdateFunction update)
        {
            _update = update ?? throw new ArgumentNullException(nameof(update));
            Context = context;
        }

        public RenderContext Context { get; }

        public bool IsRunning { get; set; }

        public void Start()
        {
            if (IsRunning) { throw new InvalidOperationException($"{nameof(RenderLoop)} has already started."); }

            // Create and start thread
            IsRunning = true;
            _thread = new Thread(ThreadBody) { IsBackground = true, Name = "Render Thread" };
            _thread.Start();
        }

        public void Stop()
        {
            if (!IsRunning) { throw new InvalidOperationException($"{nameof(RenderLoop)} has already stopped."); }

            IsRunning = false;
            _thread.Join();
        }

        private void ThreadBody()
        {
            var stopwatch = Stopwatch.StartNew();

            while (IsRunning)
            {
                // == Compute Delta

                var delta = (float) stopwatch.Elapsed.TotalSeconds;
                stopwatch.Restart();

                // == Render Phase

                Context.ResetState();
                _update(Context, delta);
                Context.SwapBuffers();
            }
        }
    }

    /// <summary>
    /// A window with an embedded thread running a game loop.
    /// </summary>
    public abstract class GameWindow : Window
    {
        private const float FpsSampleDuration = 1F;

        private float _fpsTime;
        private int _fpsCount;

        private RenderLoop _renderLoop;

        #region Constructors

        protected GameWindow(string title = "Heirloom Game Window")
            : this(WindowCreationSettings.Default, title)
        { }

        protected GameWindow(WindowCreationSettings settings, string title = "Heirloom Game Window")
            : base(settings, title)
        {
            _renderLoop = new RenderLoop(RenderContext, RenderUpdate);
        }

        #endregion

        #region Properties

        /// <summary>
        /// Enable or disable drawing the FPS overlay.
        /// </summary>
        public bool ShowFPSOverlay { get; set; } = false;

        /// <summary>
        /// The FPS of the game loop.
        /// </summary>
        public float FrameRate { get; private set; }

        /// <summary>
        /// Is the game thread running?
        /// </summary>
        public bool IsRunning => _renderLoop.IsRunning;

        #endregion

        /// <summary>
        /// Run the game thread. 
        /// Should be invoked from within the callback of <see cref="Application.Run(Action)"/>.
        /// </summary>
        public void Run()
        {
            _renderLoop.Start();
        }

        private void RenderUpdate(RenderContext ctx, float dt)
        {
            Update(RenderContext, dt);

            // == Render FPS Overlay

            if (ShowFPSOverlay)
            {
                RenderContext.ResetState();

                var text = $"FPS: {FrameRate.ToString("0.00")}";
                var size = Font.Default.MeasureText(text, 16);

                RenderContext.Color = Color.DarkGray;
                RenderContext.DrawRect(new Rectangle(FramebufferSize.Width - 8 - size.Width - 3, 8, size.Width + 4, size.Height + 1));

                RenderContext.Color = Color.Pink;
                RenderContext.DrawText(text, new Vector(FramebufferSize.Width - 8, 8), Font.Default, 16, TextAlign.Right);
            }

            // == Compute Timing Metrics

            ComputeFPS(dt);
        }

        protected override void Dispose(bool disposeManaged)
        {
            // Stop render thread
            _renderLoop.Stop();

            // Dispose window
            base.Dispose(disposeManaged);
        }

        private void ComputeFPS(float delta)
        {
            _fpsTime += delta;
            _fpsCount++;

            if (_fpsTime >= FpsSampleDuration)
            {
                // hz, events/time
                FrameRate = _fpsCount / _fpsTime;

                _fpsCount = 0;
                _fpsTime = 0;
            }
        }

        protected abstract void Update(RenderContext ctx, float dt);
    }
}
