using System.Collections.Generic;
using System.Linq;

using Heirloom.Drawing;
using Heirloom.Mathematics;
using Heirloom.Utilities;

namespace Heirloom.Benchmark
{
    public sealed class BenchmarkApp : GameWrapper
    {
        public static readonly Color Background = Color.Parse("FFB74D");

        private readonly List<BenchmarkScene> _scenes;
        private bool _mustInitialize = true;
        private int _sceneIndex;

        public BenchmarkApp(GraphicsContext graphics) : base(graphics)
        {
            // Construct (and load) benchmark scenes
            _scenes = new List<BenchmarkScene>
            {
                // Other benchmarks
                new TrianglesDynamicBenchmark(),
                new TrianglesStaticBenchmark(),
                new TextBenchmark(),

                // Particle benchmarks
                new AdventureParticleBenchmark(),
                // new CasinoParticleBenchmark(),
                new EmoteParticleBenchmark()
            };
        }

        public BenchmarkScene CurrentScene => _scenes[_sceneIndex];

        protected override void Update(float dt)
        {
            // Enable the performance overlay
            Graphics.Performance.ShowOverlay = true;

            // Render benchmark
            Graphics.ResetState();
            Graphics.Clear(Background);

            if (_sceneIndex < _scenes.Count)
            {
                // Initialize current benchmark
                if (_mustInitialize)
                {
                    _mustInitialize = false;

                    var bounds = new IntRectangle(0, 0, Graphics.Surface.Width, Graphics.Surface.Height);
                    CurrentScene.Initialize(bounds);
                }

                // Render benchmark
                CurrentScene.Update(Graphics, dt);

                // Advance to next scene
                if (CurrentScene.IsComplete)
                {
                    // Scene completed
                    _mustInitialize = true;
                    _sceneIndex++;
                }
            }
            else
            {
                // Draw results screen
                var text = "";
                var totalTime = 0F;
                var finalMean = 0F;
                var finalDev = 0F;

                foreach (var scene in _scenes)
                {
                    var stats = scene.GetStatistics();

                    // Compute scene average mean and devation
                    var mean = stats.Select(s => s.Mean).Average();
                    var dev = stats.Select(s => s.Deviation).Average();

                    // Sum mean
                    finalMean += mean;
                    finalDev += dev;

                    // Show contribution of each test
                    text += $"{scene.Name}:\n";

                    foreach (var stat in stats)
                    {
                        text += $"  -> {stat:N1} fps\n";
                    }

                    totalTime += scene.TotalTime;
                }

                // Compute final mean/dev
                finalMean /= _scenes.Count;
                finalDev /= _scenes.Count;

                text += $"--------------------\n";
                text += $"Elasped Time: {Time.GetEnglishTime(totalTime)}\n";
                text += $"Final Score: {finalMean:N1} ± {finalDev:N1}\n";

                // todo: emit text file for run

                // Draw text
                Graphics.Color = Color.Black;
                var box = Graphics.DrawText(text, (32, 32), Font.SansSerifBold, 18);
                Graphics.DrawRectOutline(Rectangle.Inflate(box, 4));
            }

            // Present graphics to screen
            Graphics.Screen.Refresh();
        }
    }
}
