using Android.App;
using Android.Content.PM;
using Android.OS;

using Heirloom.Android;
using Heirloom.Drawing;
using Heirloom.IO;
using Heirloom.Math;

namespace Examples.Android.Slideshow
{
    [Activity(
          Immersive = true,
          Theme = "@android:style/Theme.NoTitleBar.Fullscreen",
          ConfigurationChanges = ConfigChanges.Orientation | ConfigChanges.ScreenSize | ConfigChanges.Keyboard,
          ScreenOrientation = ScreenOrientation.SensorLandscape,
          MainLauncher = true)]
    public class MainActivity : GameActivity
    {
        private const int SlideshowTimeout = 5;

        private static readonly Color _background = Color.Parse("111");

        private Image[] _images;
        private int _current = 0;
        private float _time;

        protected override void OnCreate(Bundle bundle)
        {
            ShowFPSOverlay = true;
            base.OnCreate(bundle);

            // CC0 from https://pixabay.com/users/nara_kim-279055/
            _images = new[] {
                new Image(Files.OpenStream("files/rabbit.png")),
                new Image(Files.OpenStream("files/chick.png")),
                new Image(Files.OpenStream("files/whale.png"))
            };
        }

        protected override void Update(RenderContext ctx, float dt)
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

            ctx.Clear(_background);

            var surfaceSize = ctx.Surface.Size;
            var image = _images[_current];

            // Compute slideshow fade
            var blend = Calc.Clamp(Calc.Pow(1F - Calc.Sin(_time / 5F * Calc.Pi), 10), 0F, 1F);
            var color = Color.Lerp(Color.White, Color.Transparent, blend);

            // Compute image centering
            var yScale = (surfaceSize.Height - 64) / (float) image.Height;
            var xScale = (surfaceSize.Width - 64) / (float) image.Width;
            var scale = Calc.Min(xScale, yScale);

            var xOffset = (surfaceSize.Width - (image.Width * scale)) / 2F;
            var yOffset = (surfaceSize.Height - (image.Height * scale)) / 2F;

            // Draw image
            ctx.Color = color;
            ctx.DrawImage(image, Matrix.CreateTransform(xOffset, yOffset, 0, scale, scale));
        }
    }
}
