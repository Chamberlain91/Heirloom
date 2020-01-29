using Heirloom.Drawing;
using Heirloom.Math;

namespace Heirloom.Benchmark
{
    public abstract class Benchmark
    {
        protected Benchmark(string name)
        {
            Name = name;
        }

        public string Name { get; }

        public float Time { get; private set; }

        public bool IsComplete { get; protected set; }

        public float Progress { get; protected set; }

        public float Score { get; protected set; }

        public abstract void Initialize(in Rectangle bounds);

        public void UpdateBenchmark(Graphics gfx, in Rectangle bounds, float delta)
        {
            Time += delta;

            Update(gfx, in bounds, delta);
        }

        protected abstract void Update(Graphics gfx, in Rectangle bounds, float delta);
    }
}
