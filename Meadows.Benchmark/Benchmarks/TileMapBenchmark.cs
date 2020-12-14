using Meadows.Drawing;
using Meadows.Mathematics;

namespace Meadows.Benchmark
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

        protected override void Update(GraphicsContext gfx, in Rectangle bounds, float dt)
        {
            if (Time > 3) { IsComplete = true; }
        }
    }
}
