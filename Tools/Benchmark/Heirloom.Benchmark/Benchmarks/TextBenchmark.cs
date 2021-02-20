using Heirloom.Drawing;
using Heirloom.IO;
using Heirloom.Mathematics;

namespace Heirloom.Benchmark
{
    public sealed class TextBenchmark : BenchmarkScene
    {
        private readonly string _text;
        private float _time;

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
            _time += dt;

            var w = gfx.Surface.Width / 2 + Calc.Sin(_time * 1.5F) * 200;
            var x = (gfx.Surface.Width - w) / 2F;

            // Draw text layout box
            var layoutBox = new Rectangle(x, 20, w, gfx.Surface.Height - 40);
            gfx.Color = Color.White;
            gfx.DrawRect(layoutBox);
            gfx.Color = Color.DarkGray;
            gfx.DrawRectOutline(layoutBox);

            // Shrink rectangle slightly to give padding to the layout box
            layoutBox = Rectangle.Inflate(layoutBox, -12);
            gfx.DrawText(_text, layoutBox, Font.SansSerifBold, 14);

            IsComplete = Time >= 10F;
            if (IsComplete) { SubmitStatisticsBlock(); }
        }
    }
}
