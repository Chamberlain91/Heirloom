using System;
using System.IO;
using System.Threading;

using Heirloom.Desktop;

namespace Heirloom.Benchmark
{
    internal class Program : GameLoop
    {
        private readonly Benchmark[] _benchmarks;
        private int _benchmarkIndex;

        private Rectangle _bounds;

        public Program()
            : base(new Window("Heirloom Benchmark", vsync: false))
        {
            Graphics.Performance.OverlayMode = PerformanceOverlayMode.Simple;

            // Go fullscreen!
            Window.BeginFullscreen(Application.DefaultMonitor);
            // window.Maximize();

            // Compute world bounds and when the framebuffer size changes, resize the application bounds.
            _bounds = (0, 0, Window.Surface.Width, Window.Surface.Height);
            Window.FramebufferResized += (f, s) =>
            {
                _bounds = (0, 0, Window.Surface.Width, Window.Surface.Height);
            };

            // Create benchmarks
            _benchmarks = new Benchmark[]
            {
                new DynamicTriangulation(),
                new StaticTriangulation(),
                new EmoteIconBenchmark(),
                new AdventureBenchmark(),
                new CasinoBenchmark()
            };

            // Initialize first benchmark
            _benchmarks[0].Initialize(in _bounds);

            // Bind window events
            Window.KeyPressed += OnKeyPress;
        }

        public Window Window => Screen as Window;

        private static void Main(string[] args)
        {
            Application.Run<Program>();
        }

        private static void OnKeyPress(Screen s, KeyEvent e)
        {
            // Kill window
            if (e.Key == Key.Escape) { s.Close(); }
        }

        private void GotoNextBenchmark()
        {
            // Move to next index (if possible)
            if (_benchmarkIndex < _benchmarks.Length) { _benchmarkIndex++; }

            // If still a valid index, initialize stage
            if (_benchmarkIndex < _benchmarks.Length)
            {
                var benchmark = _benchmarks[_benchmarkIndex];
                benchmark.Initialize(in _bounds);
            }
            else
            {
                var cpu = Application.CpuInfo;
                var gpu = Application.GpuInfo;

                // Results
                var results = new BenchmarkResults(gpu, cpu);
                foreach (var benchmark in _benchmarks)
                {
                    results.Scores[benchmark.Name.ToIdentifier()] = benchmark.Score;
                }

                // Write
                using var fs = new FileStream(results.GenerateFilename(), FileMode.Create);
                using var wr = new StreamWriter(fs);
                wr.Write(BenchmarkResults.ToJson(results));

                // Leave fullscreen
                Window.Graphics.Performance.OverlayMode = PerformanceOverlayMode.Disabled;
                Window.EndFullscreen();

                // Size window
                var rect = TextLayout.Measure(GetResultsText(_benchmarks), Font.Default, 32);
                Window.Size = (IntSize) rect.Size + (32, 32);
            }
        }
        protected override void Update(float dt)
        {
            Graphics.Clear(Color.DarkGray);

            // 
            if (_benchmarkIndex < _benchmarks.Length)
            {
                var benchmark = _benchmarks[_benchmarkIndex];

                // Update and draw stage
                benchmark.UpdateBenchmark(Graphics, in _bounds, dt);

                // If the stage is complete, move to the next stage
                if (benchmark.IsComplete)
                {
                    Console.WriteLine($"{benchmark.Name}: {benchmark.Score}");

                    // 
                    GotoNextBenchmark();
                }

                // 
                DrawInformation(Graphics, $"\"{benchmark.Name}\" - {benchmark.Progress * 100F:N2}% - {Graphics.CurrentFPS:N0} FPS");
            }
            else
            {
                var results = GetResultsText(_benchmarks);

                // Draw Results Stage
                DrawInformation(Graphics, results);
                Thread.Sleep(2); // force to render slower
            }
        }

        private static string GetResultsText(Benchmark[] benchmarks)
        {
            var results = "";

            // Machine info
            results += Application.GpuInfo + "\n";
            results += Application.CpuInfo + "\n";
            results += "\n";

            // Results
            foreach (var benchmark in benchmarks)
            {
                results += $"{benchmark.Name.ToIdentifier()}: {benchmark.Score} {benchmark.Units}\n";
            }

            return results;
        }

        private static void DrawInformation(GraphicsContext gfx, string text)
        {
            var rect = TextLayout.Measure(text, Font.Default, 32);
            rect.Inflate(6);
            rect.Offset(16, 16);

            gfx.Color = Color.DarkGray;
            gfx.DrawRect(rect);

            gfx.Color = Color.Gray;
            gfx.DrawRectOutline(rect, 2);

            gfx.Color = Color.White;
            gfx.DrawText(text, (16, 16), Font.Default, 32);
        }
    }
}
