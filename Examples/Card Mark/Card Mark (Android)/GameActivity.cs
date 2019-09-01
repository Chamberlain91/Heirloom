using System.Diagnostics;
using System.Threading;

using Android.App;
using Android.OS;

using Heirloom.Drawing;
using Heirloom.Math;
using Heirloom.Platforms.Android;

using ThreadPriority = System.Threading.ThreadPriority;

namespace Heirloom.Examples.CardMark
{
    public abstract class GameActivity : Activity
    {
        public HeirloomSurfaceView SurfaceView { get; private set; }

        public bool IsPaused { get; private set; }

        private Thread _thread;

        protected float FPS { get; private set; }

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

            // Set to highest priority
            Thread.CurrentThread.Priority = ThreadPriority.Highest;

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

                    //
                    SurfaceView.RenderContext.ResetState();
                    Render(SurfaceView.RenderContext, delta);

                    // 
                    SurfaceView.RenderContext.ResetState();
                    SurfaceView.RenderContext.DrawText($"FPS: {rateCounter.Rate}", (10, 10), TextAlign.Left, Font.Default, 13, Colors.FlatUI.Alizarin);
                    FPS = rateCounter.Rate;

                    // 
                    SurfaceView.RenderContext.SwapBuffers();
                    rateCounter.Tick();
                }
            }
        }

        internal abstract void Update(float delta);

        internal abstract void Render(RenderContext ctx, float delta);

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
