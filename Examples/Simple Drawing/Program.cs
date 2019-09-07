using Heirloom.Desktop;
using Heirloom.Drawing;
using Heirloom.IO;
using Heirloom.Math;

namespace Examples.SimpleDrawing
{
    internal class Program : GameWindow
    {
        static readonly public Color Background = Color.Parse("E111");

        private readonly Image[] _images;
        private int _current = 0;
        private float _time;

        public Program()
            : base(800, 450, "Example Game", transparent: true, vsync: false)
        {
            ShowFPSOverlay = true;

            // CC0 from https://pixabay.com/users/nara_kim-279055/
            _images = new[] {
                new Image(Files.OpenStream("files/rabbit.png")),
                new Image(Files.OpenStream("files/chick.png")),
                new Image(Files.OpenStream("files/whale.png"))
            };
        }

        protected override void Update(float dt)
        {
            _time += dt;

            // 
            if (_time >= 5)
            {
                _time -= 5;

                // 
                _current++;
                if (_current >= _images.Length) { _current = 0; }
            }
        }

        protected override void Draw(RenderContext ctx, float dt)
        {
            ctx.Clear(Background);

            var image = _images[_current];

            // Compute slideshow fade
            var blend = Calc.Clamp(Calc.Pow(1F - Calc.Sin(_time / 5F * Calc.Pi), 10), 0F, 1F);
            var color = Color.Lerp(Color.White, Color.Transparent, blend);

            // Compute image centering
            var yScale = (FramebufferSize.Height - 64) / (float) image.Height;
            var xScale = (FramebufferSize.Width - 64) / (float) image.Width;
            var scale = Calc.Min(xScale, yScale);

            var xOffset = (FramebufferSize.Width - (image.Width * scale)) / 2F;
            var yOffset = (FramebufferSize.Height - (image.Height * scale)) / 2F;

            // Draw image
            ctx.Draw(image, Matrix.CreateTransform(xOffset, yOffset, 0, scale, scale), color);
        }

        private static void Main(string[] _)
        {
            Application.Run(() =>
            {
                var game = new Program();
                // game.SetFullscreen(Monitor.Default);
                game.Run(); // begin game
            });
        }
    }
}
