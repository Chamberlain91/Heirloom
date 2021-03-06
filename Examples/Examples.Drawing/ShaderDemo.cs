using Heirloom;

namespace Examples.Drawing
{
    public sealed class ShaderDemo : Demo
    {
        private readonly Image _image, _noise;
        private readonly DistortionShader _distort;

        public ShaderDemo()
            : base("Distortion Shader")
        {
            _image = new Image("files/colored_castle.png");

            _noise = Image.CreateNoise(128, 128, 32);
            _noise.Interpolation = InterpolationMode.Linear;
            _noise.Repeat = RepeatMode.Repeat;

            // Load shader
            _distort = new DistortionShader(_noise) { Strength = 0.05F };
        }

        internal override void Draw(GraphicsContext ctx, Rectangle contentBounds)
        {
            _distort.Offset = new Vector(Time / 2, Time / 4);

            // Compute image centering
            var yScale = contentBounds.Height / _image.Height;
            var xScale = contentBounds.Width / _image.Width;
            var scale = Calc.Min(xScale, yScale);

            var xOffset = contentBounds.Min.X + (contentBounds.Width - (_image.Width * scale)) / 2F;
            var yOffset = contentBounds.Min.Y + (contentBounds.Height - (_image.Height * scale)) / 2F;

            // Draw image
            ctx.Shader = _distort;
            ctx.DrawImage(_image, Matrix.CreateTransform(xOffset, yOffset, 0, scale, scale));
        }
    }
}
