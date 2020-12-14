using System;
using System.IO;
using System.Threading;

using Meadows.Desktop;
using Meadows.Drawing;
using Meadows.Hardware;
using Meadows.Mathematics;
using Meadows.Utilities;

namespace Meadows.Benchmark
{
    internal class Program : Application
    {
        private readonly Benchmark[] _benchmarks;
        private int _benchmarkIndex;

        private Rectangle _bounds;

        public Window Window { get; }

        public GameLoop Loop { get; }

        public Program()
        {
            Window = new Window("Meadows Benchmark", (512, 512), vsync: false);

            // Go fullscreen!
            Window.BeginFullscreen(Display.Primary);
            // Window.Maximize();

            // Compute world bounds and when the framebuffer size changes, resize the application bounds.
            _bounds = (0, 0, Window.Surface.Width, Window.Surface.Height);

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

            // 
            Loop = new GameLoop(Update);
            Window.Closed += w => Loop.Stop();
            Loop.Start();
        }

        private static void Main(string[] args)
        {
            Run<Program>();
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
                var cpu = SystemInformation.CpuInfo;
                var gpu = SystemInformation.GpuInfo;

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
                Window.EndFullscreen();

                // Size window
                var rect = TextLayout.Measure(GetResultsText(_benchmarks), Font.Default, 32);
                Window.Size = (IntSize) rect.Size + (32, 32);
            }
        }

        private void Update(float dt)
        {
            var gfx = Window.Graphics;

            gfx.SetRenderTarget(Window.Surface);
            gfx.Clear(Color.DarkGray);

            // 
            if (_benchmarkIndex < _benchmarks.Length)
            {
                var benchmark = _benchmarks[_benchmarkIndex];

                // Update and draw stage
                benchmark.UpdateBenchmark(gfx, in _bounds, dt);

                // If the stage is complete, move to the next stage
                if (benchmark.IsComplete)
                {
                    Console.WriteLine($"{benchmark.Name}: {benchmark.Score}");

                    // 
                    GotoNextBenchmark();
                }

                // 
                DrawInformation(gfx, $"\"{benchmark.Name}\" - {benchmark.Progress * 100F:N2}% - {gfx.Performance.FrameRate:N0} FPS");
            }
            else
            {
                var results = GetResultsText(_benchmarks);

                // Draw Results Stage
                DrawInformation(gfx, results);
                Thread.Sleep(2); // force to render slower
            }

            // 
            Window.Refresh();
        }

        private static string GetResultsText(Benchmark[] benchmarks)
        {
            var results = "";

            // Machine info
            results += SystemInformation.GpuInfo + "\n";
            results += SystemInformation.CpuInfo + "\n";
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
