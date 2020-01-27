using System;

using Heirloom.Desktop;
using Heirloom.Drawing;
using Heirloom.Math;

namespace Heirloom.Benchmark
{
    internal class Program
    {
        private static void Main(string[] args)
        {
            // var info = Hardware.ProcessorInfo;
            // Console.WriteLine($"Name: {info.Name}");
            // Console.WriteLine($"{info.ProcessorCount} Logical Processors @ {info.ClockSpeed / 1000F:N1}GHz");

            // 
            var benchmarkIndex = 0;
            var benchmarks = new Benchmark[]
            {
                 new AdventureBenchmark(),
                 new EmoteIconBenchmark(),
                 // new TileMapBenchmark()
            };

            var bounds = new Rectangle(0, 0, 0, 0);

            Application.Run(() =>
            {
                // Create fullscreen window
                var window = new Window("Heirloom Benchmark", new WindowCreationSettings { VSync = false });
                // window.SetFullscreen(Application.DefaultMonitor);

                // Compute bounds and initialize first benchmark
                bounds = (0, 0, window.FramebufferSize.Width, window.FramebufferSize.Height);
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
                    var results = "";
                    foreach (var benchmark in benchmarks)
                    {
                        results += $"{benchmark.Name}: {benchmark.Score}\n";
                    }

                    // Draw Results Stage
                    DrawInformation(gfx, results);
                }
            }
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
