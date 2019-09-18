using Heirloom.Drawing;
using Heirloom.IO;
using Heirloom.Math;

namespace Examples.Drawing
{
    public sealed class ImageDemo : Demo
    {
        private readonly Image _image;

        public ImageDemo()
            : base("Image")
        {
            // CC0 from https://pixabay.com/users/nara_kim-279055/
            _image = new Image(Files.OpenStream("files/rabbit.png"));
        }

        internal override void Draw(RenderContext ctx)
        {
            // Compute image centering
            var yScale = (ctx.Surface.Height - 300) / (float) _image.Height;
            var xScale = (ctx.Surface.Width - 100) / (float) _image.Width;
            var scale = Calc.Min(xScale, yScale);

            var xOffset = (ctx.Surface.Width - (_image.Width * scale)) / 2F;
            var yOffset = (ctx.Surface.Height - (_image.Height * scale)) / 2F;

            // Draw image
            ctx.DrawImage(_image, Matrix.CreateTransform(xOffset, yOffset, 0, scale, scale));
        }
    }
}
