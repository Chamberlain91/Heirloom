using System;
using System.Collections.Generic;

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

        public bool IsComplete { get; protected set; }

        public float Time { get; protected set; }

        public float TotalTime { get; private set; }

        public Rectangle Bounds { get; private set; }

        private List<float> _frames = new List<float>();
        private List<Statistics> _stats = new List<Statistics>();

        public void Initialize(Rectangle bounds)
        {
            Bounds = bounds;
            TotalTime = 0;
            Time = 0;

            // 
            _frames.Clear();
            _stats.Clear();

            // Ensure randomization produces same results
            Calc.SetRandomSeed(0);

            // 
            InitializeScene();
        }

        public void Update(GraphicsContext gfx, float dt)
        {
            TotalTime += dt;
            Time += dt;

            // Append frame time
            if (dt > Calc.Epsilon) { _frames.Add(dt * 1000); }

            // Process scene
            UpdateScene(gfx, dt);
        }

        protected void SubmitStatisticsBlock()
        {
            // Drop first frame to help avoid the large spike that seems to exist...
            // todo: properly handle this large frame time
            _frames.RemoveAt(0);

            _stats.Add(Statistics.Compute(_frames));
            _frames.Clear();
        }

        public IEnumerable<Statistics> GetStatistics()
        {
            return _stats;
        }

        protected abstract void InitializeScene();

        protected abstract void UpdateScene(GraphicsContext gfx, float dt);
    }
}
