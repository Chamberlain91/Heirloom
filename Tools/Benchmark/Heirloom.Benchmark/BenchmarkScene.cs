using System;

using Heirloom.Drawing;
using Heirloom.Mathematics;

namespace Heirloom.Benchmark
{
    public abstract class BenchmarkScene
    {
        protected BenchmarkScene(string name)
        {
            Name = name ?? throw new ArgumentNullException(nameof(name));
        }

        public string Name { get; }

        public float Score { get; private set; }

        public bool IsComplete { get; protected set; }

        public float Time { get; protected set; }

        public float TotalTime { get; private set; }

        public Rectangle Bounds { get; private set; }

        public void Initialize(Rectangle bounds)
        {
            Bounds = bounds;
            Score = 0;
            TotalTime = 0;
            Time = 0;

            // Ensure randomization produces same results
            Calc.SetRandomSeed(0);

            // 
            InitializeScene();
        }

        public void Update(GraphicsContext gfx, float dt)
        {
            TotalTime += dt;
            Time += dt;
            Score++;

            // Process scene
            UpdateScene(gfx, dt);
        }

        protected abstract void InitializeScene();

        protected abstract void UpdateScene(GraphicsContext gfx, float dt);
    }
}
