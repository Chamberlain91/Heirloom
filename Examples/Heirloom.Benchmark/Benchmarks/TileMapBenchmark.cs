using Heirloom.Drawing;
using Heirloom.Math;

namespace Heirloom.Benchmark
{
    internal class TileMapBenchmark : Benchmark
    {
        public TileMapBenchmark()
            : base("Tile Map")
        { }

        public override void Initialize(in Rectangle bounds)
        {
            // 
        }

        protected override void Update(Graphics gfx, in Rectangle bounds, float dt)
        {
            if (Time > 3) { IsComplete = true; }
        }
    }
}
