using System.Collections.Generic;

using Meadows.Drawing;
using Meadows.Mathematics;
using Meadows.Utilities;

namespace Meadows.Benchmark
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
                new CasinoParticleBenchmark(),
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
                var total = 0F;
                foreach (var scene in _scenes)
                {
                    var name = scene.GetType().Name;
                    text += $"{name}: {scene.Score}\n";
                    total += scene.Score;
                }

                text += $"--------------------\n";
                text += $"Final Score: {total}";

                // Draw text
                Graphics.Color = Color.Black;
                Graphics.DrawText(text, (10, 10), Font.SansSerifBold, 24);
            }

            // Present graphics to screen
            Graphics.Screen.Refresh();
        }
    }
}
