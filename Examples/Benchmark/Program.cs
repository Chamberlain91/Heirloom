using System;
using System.IO;

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
                 new EmoteIconBenchmark(),
                 new AdventureBenchmark(),
                 new CasinoBenchmark()
            };

            var bounds = new Rectangle(0, 0, 0, 0);

            Application.Run(() =>
            {
                // Create fullscreen window
                var window = new Window("Heirloom Benchmark", vsync: false);
                window.SetFullscreen(Application.DefaultMonitor);

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
                    var gpu = GraphicsAdapter.Capabilities.AdapterName;
                    var speed = $"{Hardware.ProcessorInfo.ClockSpeed / 1000F:N2}ghz";
                    var cpu = Hardware.ProcessorInfo.Name;

                    var identifier = $"benchmark_{gpu}_{cpu}".ToIdentifier();

                    using var fs = new FileStream($"{identifier}.txt", FileMode.Create);
                    using var wr = new StreamWriter(fs);

                    // Hit the end, save results text
                    var text = GetResultsText(benchmarks);
                    wr.WriteLine(text);
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
                    DrawInformation(gfx, $"\"{benchmark.Name}\" - {benchmark.Progress * 100F:N2}%");
                }
                else
                {
                    var results = GetResultsText(benchmarks);

                    // Draw Results Stage
                    DrawInformation(gfx, results);
                }
            }
        }

        private static string GetResultsText(Benchmark[] benchmarks)
        {
            var results = "";

            // Machine info
            results += GraphicsAdapter.Capabilities.AdapterVendor + " - " + GraphicsAdapter.Capabilities.AdapterName + "\n";
            results += Hardware.ProcessorInfo.Name + "\n";
            results += Hardware.ProcessorInfo.ProcessorCount + " threads @ " + Hardware.ProcessorInfo.ClockSpeed + "mhz\n";
            results += "\n";

            // Results
            foreach (var benchmark in benchmarks)
            {
                results += $"{benchmark.Name.ToIdentifier()}: {benchmark.Score}\n";
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
