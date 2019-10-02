using System;
using System.Diagnostics;
using System.Threading;

using Heirloom.Math;

namespace Heirloom.Drawing
{
    public abstract class RenderLoop
    {
        public delegate void UpdateFunction(RenderContext ctx, float dt);

        private const float FpsSampleDuration = 1F;

        private float _fpsTime;
        private int _fpsCount;

        private Thread _thread;

        public RenderLoop(RenderContext context)
        {
            RenderContext = context;
        }

        public RenderContext RenderContext { get; }

        public bool IsRunning { get; private set; }

        public float FrameRate { get; private set; }

        /// <summary>
        /// Gets or sets a value that will enable or disable drawing the FPS overlay.
        /// </summary>
        public bool ShowFPSOverlay { get; set; } = false;

        protected abstract void Update(RenderContext renderContext, float delta);

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

                // Lock the render context to have 'exclusive control' and prevent it
                // from being disposed of when it is needed to render.
                lock (RenderContext)
                {
                    // Render context was disposed of already (we can't render anymore)
                    // so we will shutdown and exit this thread.
                    if (RenderContext.IsDisposed)
                    {
                        Stop();
                        break;
                    }

                    // Draw Application
                    RenderContext.ResetState();
                    Update(RenderContext, delta);

                    // Draw Debug Overlays
                    DrawFPSOverlay();

                    // Push pixels to screen
                    RenderContext.SwapBuffers();
                }

                // == Compute Timing Metrics

                ComputeFPS(delta);
            }
        }

        private void DrawFPSOverlay()
        {
            if (ShowFPSOverlay)
            {
                RenderContext.ResetState();

                var text = $"FPS: {FrameRate.ToString("0.00")}";
                var size = Font.Default.MeasureText(text, 16);

                RenderContext.Color = Color.DarkGray;
                RenderContext.DrawRect(new Rectangle(RenderContext.Surface.Width - 8 - size.Width - 3, 8, size.Width + 4, size.Height + 1));

                RenderContext.Color = Color.Pink;
                RenderContext.DrawText(text, new Vector(RenderContext.Surface.Width - 8, 8), Font.Default, 16, TextAlign.Right);
            }
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

        public static RenderLoop CreateDefault(RenderContext ctx, UpdateFunction update)
        {
            return new DefaultRenderLoop(ctx, update);
        }

        private sealed class DefaultRenderLoop : RenderLoop
        {
            private readonly UpdateFunction _update;

            public DefaultRenderLoop(RenderContext context, UpdateFunction update)
                : base(context)
            {
                _update = update ?? throw new ArgumentNullException(nameof(update));
            }

            protected override void Update(RenderContext ctx, float delta)
            {
                _update(ctx, delta);
            }
        }
    }
}
