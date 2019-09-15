using System.Diagnostics;
using System.Threading;

using Android.App;
using Android.OS;

using Heirloom.Drawing;
using Heirloom.Math;

namespace Heirloom.Android
{
    public abstract class GameActivity : Activity
    {
        public HeirloomSurfaceView SurfaceView { get; private set; }

        public bool IsPaused { get; private set; }

        private Thread _thread;

        /// <summary>
        /// Enable or disable drawing the FPS overlay.
        /// </summary>
        public bool ShowFPSOverlay { get; set; } = false;

        /// <summary>
        /// The FPS of the game loop.
        /// </summary>
        protected float FrameRate { get; private set; }

        protected override void OnCreate(Bundle savedInstanceState)
        {
            base.OnCreate(savedInstanceState);

            // Run fullscreen
            AndroidHelper.SetWindowFullscreen(this);

            // 
            SurfaceView = new HeirloomSurfaceView(this);
            SurfaceView.Resume(); // todo: put inside surfaceview on OnSurfaceCreate with boolean trap, definitely rename.
            SetContentView(SurfaceView);

            // Start game thread
            _thread = new Thread(MainLoop) { IsBackground = true, Name = "Game Thread" };
            _thread.Start();
        }

        public Vector ConvertScreenToCanvas(float x, float y)
        {
            x *= SurfaceView.RenderContext.DefaultSurface.Width / (float) SurfaceView.Width;
            y *= SurfaceView.RenderContext.DefaultSurface.Height / (float) SurfaceView.Height;
            return new Vector(x, y);
        }

        protected override void OnResume()
        {
            IsPaused = false;
            base.OnResume();
        }

        protected override void OnPause()
        {
            IsPaused = true;
            base.OnPause();
        }

        private void MainLoop()
        {
            const float DesiredFrameRate = 60F;
            const float ExpectedDelta = 1F / DesiredFrameRate;

            var rateCounter = new RateCounter(DesiredFrameRate);
            var loopWatch = new Stopwatch();

            var stopwatchInvalid = true;

            while (true)
            {
                if (IsPaused)
                {
                    // Application is paused, we should wait
                    stopwatchInvalid = true;
                    Thread.Sleep(250);
                    continue;
                }

                // == Process Phase

                if (SurfaceView.CanRender)
                {
                    // Get time since last frame
                    var delta = stopwatchInvalid ? ExpectedDelta : loopWatch.ElapsedTicks / (float) Stopwatch.Frequency;
                    stopwatchInvalid = false;

                    // (Re)Start measuring time    
                    loopWatch.Restart();

                    Update(delta);

                    var ctx = SurfaceView.RenderContext;

                    //
                    ctx.ResetState();
                    Render(ctx, delta);

                    FrameRate = rateCounter.Rate;

                    if (ShowFPSOverlay)
                    {
                        ctx.ResetState();

                        var surfaceWidth = ctx.Surface.Width;

                        var text = $"FPS: {FrameRate.ToString("0.00")}";
                        var size = Font.Default.MeasureText(text, 16);

                        ctx.Color = Color.DarkGray;
                        ctx.DrawRect(new Rectangle(surfaceWidth - 16 - size.Width - 3, 16, size.Width + 4, size.Height + 1));

                        ctx.Color = Color.Pink;
                        ctx.DrawText(text, new Vector(surfaceWidth - 16, 16), Font.Default, 16, TextAlign.Right);
                    }

                    // 
                    ctx.SwapBuffers();
                    rateCounter.Tick();
                }
            }
        }

        protected abstract void Update(float delta);

        protected abstract void Render(RenderContext ctx, float delta);

        private sealed class RateCounter
        {
            private const double Duration = 0.5F;

            private double _frames, _time, _average;
            private readonly Stopwatch _sw;

            public float Rate => (float) _average;

            public RateCounter(float initial)
            {
                _average = initial;
                _sw = Stopwatch.StartNew();
            }

            public bool Tick()
            {
                // Get the elapsed time in ticks
                var delta = _sw.ElapsedTicks / (double) Stopwatch.Frequency + double.Epsilon;
                _sw.Restart();

                // Accumulate time
                _time += delta;
                _frames++;

                // 
                if (_time > Duration)
                {
                    _average = _frames / Duration;
                    _time -= Duration;
                    _frames = 0;

                    return true;
                }

                return false;
            }
        }
    }
}
