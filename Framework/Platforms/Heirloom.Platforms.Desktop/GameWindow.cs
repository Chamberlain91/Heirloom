using System.Diagnostics;
using System.Linq;

using Heirloom.Drawing;
using Heirloom.Math;

namespace Heirloom.Platforms.Desktop
{
    public abstract class GameWindow : Window
    {
        private readonly RateCounter _rateCounter;
        private readonly Stopwatch _stopwatch;

        public bool ShowDiagnostics { get; set; } = false;

        public GameWindow(string title, bool transparent = false, bool vsync = true)
            : this(800, 600, title, vsync, transparent)
        { }

        public GameWindow(int width, int height, string title, bool transparent = false, bool vsync = true)
            : base(width, height, title, vsync, transparent)
        {
            _rateCounter = new RateCounter(60);
            _stopwatch = Stopwatch.StartNew();
        }

        protected float Time { get; private set; }

        protected float Delta { get; private set; }

        public float RefreshRate => _rateCounter.Rate;

        public void Process()
        {
            // == Begin Frame

            Delta = (float) _stopwatch.Elapsed.TotalSeconds;
            Time += Delta;

            _stopwatch.Restart();

            // == Update Phase

            Update();

            // == Render Phase

            RenderContext.ResetState();

            Render(RenderContext);

            if (ShowDiagnostics)
            {
                DrawFPSOverlay(RenderContext);
            }

            // == End Frame

            RenderContext.SwapBuffers();
            _rateCounter.Tick();
        }

        private void DrawFPSOverlay(RenderContext context)
        {
            context.ResetState();

            // 
            var text = $"FPS: {_rateCounter.Rate}";
            var size = Font.Default.MeasureText(text, 16) + (4, 2);
            var left = context.Surface.Width - size.Width - 2;

            // Draw's an rectangle
            context.Draw(Image.White, Matrix.CreateTransform((left, 2), 0, (Vector) size));
            context.DrawText(text, (left + 2, 2), TextAlign.Left, Font.Default, 16, Color.DarkGray);
        }

        protected abstract void Update();

        protected abstract void Render(RenderContext context);

        public static void Run(GameWindow window)
        {
            Run(new[] { window });
        }

        public static void Run(GameWindow[] windows)
        {
            // While some window is open
            while (windows.Any(w => !w.IsClosed))
            {
                // Poll for window events
                PollEvents();

                // For each open window, process the example
                foreach (var window in windows.Where(w => !w.IsClosed))
                {
                    window.Process();
                }
            }
        }

        private sealed class RateCounter
        {
            private const double Duration = 1F;

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
