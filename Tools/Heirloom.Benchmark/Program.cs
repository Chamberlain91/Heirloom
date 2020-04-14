using System;
using System.IO;
using System.Threading;

using Heirloom.Desktop;
using Heirloom.Drawing;
using Heirloom.IO;
using Heirloom.Math;

namespace Heirloom.Benchmark
{
    internal class Program
    {
        private static void Main(string[] args)
        {
            // 
            var benchmarkIndex = 0;
            var benchmarks = new Benchmark[]
            {
                new DynamicTriangulation(),
                new StaticTriangulation(),
                new EmoteIconBenchmark(),
                new AdventureBenchmark(),
                new CasinoBenchmark()
            };

            var bounds = new Rectangle(0, 0, 0, 0);

            Window window;

            Application.Run(() =>
            {
                // Create fullscreen window
                window = new Window("Heirloom Benchmark", vsync: false);
                window.Graphics.Performance.OverlayMode = PerformanceOverlayMode.Simple;

                // Go fullscreen!
                window.BeginFullscreen(Application.DefaultMonitor);
                // window.Maximize();

                // Compute world bounds
                bounds = (0, 0, window.FramebufferSize.Width, window.FramebufferSize.Height);

                // When the framebuffer size changes, resize the application bounds.
                window.FramebufferResized += _ =>
                    bounds = (0, 0, window.FramebufferSize.Width, window.FramebufferSize.Height);

                // Initialize first benchmark
                benchmarks[0].Initialize(in bounds);

                // Launch render loop
                var loop = RenderLoop.Create(window.Graphics, Update);
                loop.Start();

                // Bind window events
                window.KeyPress += OnKeyPress;
            });

            static void OnKeyPress(Window w, KeyEvent e)
            {
                // Kill window
                if (e.Key == Key.Escape) { w.Close(); }
            }

            void GotoNextBenchmark()
            {
                // Move to next index (if possible)
                if (benchmarkIndex < benchmarks.Length) { benchmarkIndex++; }

                // If still a valid index, initialize stage
                if (benchmarkIndex < benchmarks.Length)
                {
                    var benchmark = benchmarks[benchmarkIndex];
                    benchmark.Initialize(in bounds);
                }
                else
                {
                    var cpu = Application.CpuInfo;
                    var gpu = Application.GpuInfo;

                    // Results
                    var results = new BenchmarkResults(gpu, cpu);
                    foreach (var benchmark in benchmarks)
                    {
                        results.Scores[benchmark.Name.ToIdentifier()] = benchmark.Score;
                    }

                    // Write
                    using var fs = new FileStream(results.GenerateFilename(), FileMode.Create);
                    using var wr = new StreamWriter(fs);
                    wr.Write(BenchmarkResults.ToJson(results));

                    // Leave fullscreen
                    window.Graphics.Performance.OverlayMode = PerformanceOverlayMode.Disabled;
                    window.BeginFullscreen(null);

                    // Size window
                    var rect = TextLayout.Measure(GetResultsText(benchmarks), Font.Default, 32);
                    window.Size = (IntSize) rect.Size + (32, 32);
                }
            }

            void Update(Graphics gfx, float dt)
            {
                // 
                gfx.Clear(Color.DarkGray);

                // 
                if (benchmarkIndex < benchmarks.Length)
                {
                    var benchmark = benchmarks[benchmarkIndex];

                    // Update and draw stage
                    benchmark.UpdateBenchmark(gfx, in bounds, dt);

                    // If the stage is complete, move to the next stage
                    if (benchmark.IsComplete)
                    {
                        Console.WriteLine($"{benchmark.Name}: {benchmark.Score}");

                        // 
                        GotoNextBenchmark();
                    }

                    // 
                    DrawInformation(gfx, $"\"{benchmark.Name}\" - {benchmark.Progress * 100F:N2}% - {gfx.CurrentFPS:N0} FPS");
                }
                else
                {
                    var results = GetResultsText(benchmarks);

                    // Draw Results Stage
                    DrawInformation(gfx, results);
                    Thread.Sleep(2); // force to render slower
                }
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

        private static void DrawInformation(Graphics gfx, string text)
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
