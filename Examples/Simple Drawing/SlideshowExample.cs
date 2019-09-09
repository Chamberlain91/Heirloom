using Heirloom.Desktop;
using Heirloom.Drawing;
using Heirloom.IO;
using Heirloom.Math;

namespace Examples.SimpleDrawing
{
    internal class SlideshowExample : GameWindow
    {
        private const int SlideshowTimeout = 5;

        private static readonly Color _background = Color.Parse("111");

        private readonly Image[] _images;
        private int _current = 0;
        private float _time;

        public SlideshowExample()
            : base(800, 450, "Example Game", vsync: false)
        {
            ShowFPSOverlay = true;
            Maximize();

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

            // Enough time has elapsed
            if (_time >= SlideshowTimeout)
            {
                _time -= SlideshowTimeout;

                // Move to next image
                _current++;
                if (_current >= _images.Length)
                {
                    _current = 0;
                }
            }
        }

        protected override void Draw(RenderContext ctx, float dt)
        {
            ctx.Clear(_background);

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
    }
}
