using Heirloom.Drawing;
using Heirloom.Math;
using Heirloom.Platforms.Desktop;

namespace Heirloom.Examples.SplitScreen
{
    internal class Example : GameWindow
    {
        public Image Background;
        public Image Sprite;

        public Example()
            : base("Split Screen")
        {
            Sprite = Image.CreateCircle(16, Colors.FlatUI.Orange, Colors.FlatUI.Pumpkin, 2);
            Background = Image.CreateCheckerboardPattern(512, 512, Pixel.Gray);

            // 
            Size = new IntSize(512, 256);
            IsResizeable = false;
        }

        protected override void Update()
        {
            // Do Nothing
        }

        protected override void Render(RenderContext context)
        {
            // Compute path along circle
            var x = (Calc.Sin(Time) * 0.5F + 0.5F) * -(Background.Width - 256);
            var y = (Calc.Cos(Time) * 0.5F + 0.5F) * -(Background.Height - 256);

            // Clear background
            context.Surface = context.DefaultSurface;
            context.Clear(Color.DarkGray);

            // Draw 'player 1' view of the world in viewport 1
            context.Viewport = (0, 0, 0.5F, 1F);
            context.Transform = Matrix.CreateTranslation(x, y);
            DrawWorld(context);

            // Draw 'player 2' view of the world in viewport 2
            context.Viewport = (0.5F, 0, 0.5F, 1F);
            context.Transform = Matrix.CreateTranslation(-64 + x * 0.2F, -64 + y * 0.2F);
            DrawWorld(context);
        }

        private void DrawWorld(RenderContext context)
        {
            context.Draw(Background, Matrix.Identity);

            var center = (Vector) Background.Size / 2F;

            // Draw ring of sprites at center of world
            for (var i = 0; i < 16; i++)
            {
                var angle = (i / 16F) * Calc.TwoPi;
                var x = center.X - 8 + Calc.Cos(angle) * 64;
                var y = center.Y - 8 + Calc.Sin(angle) * 64;
                var transform = Matrix.CreateTranslation(x, y);
                context.Draw(Sprite, transform);
            }
        }

        private static void Main(string[] args)
        {
            Run(new Example());
        }
    }
}
