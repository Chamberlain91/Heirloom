using Meadows.Drawing;
using Meadows.IO;

namespace Meadows.Benchmark
{
    public sealed class TextBenchmark : BenchmarkScene
    {
        private readonly string _text;

        public TextBenchmark()
            : base("Alice in Wonderland")
        {
            _text = Files.ReadText("files/alice_in_wonderland.txt");
        }

        protected override void InitializeScene()
        {
            // Nothing to do
        }

        protected override void UpdateScene(GraphicsContext gfx, float dt)
        {
            // Simply draw text
            gfx.Color = Color.DarkGray;
            gfx.DrawText(_text, (20, 20, gfx.Surface.Width - 40, gfx.Surface.Height - 40), Font.SansSerif, 15);

            Score++; // Count Frames
            IsComplete = Time >= 10F;
        }
    }
}
