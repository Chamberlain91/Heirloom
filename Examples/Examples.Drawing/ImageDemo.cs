using Heirloom;
using Heirloom.IO;

namespace Examples.Drawing
{
    public sealed class ImageDemo : Demo
    {
        private readonly Image _image;

        public ImageDemo()
            : base("Simple Image")
        {
            _image = new Image(Files.OpenStream("files/colored_castle.png"));
        }

        internal override void Draw(Graphics ctx, Rectangle contentBounds)
        {
            // Compute image centering
            var yScale = contentBounds.Height / _image.Height;
            var xScale = contentBounds.Width / _image.Width;
            var scale = Calc.Min(xScale, yScale);

            var xOffset = contentBounds.Min.X + (contentBounds.Width - (_image.Width * scale)) / 2F;
            var yOffset = contentBounds.Min.Y + (contentBounds.Height - (_image.Height * scale)) / 2F;

            // Draw image
            ctx.DrawImage(_image, Matrix.CreateTransform(xOffset, yOffset, 0, scale, scale));
        }
    }
}
