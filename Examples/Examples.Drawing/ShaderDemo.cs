using Heirloom.Drawing;
using Heirloom.Math;

namespace Examples.Drawing
{
    public sealed class ShaderDemo : Demo
    {
        private readonly Image _image, _noise;
        private readonly Shader _shader;

        public ShaderDemo()
            : base("Distortion Shader")
        {
            _image = new Image("files/colored_castle.png");

            _noise = Image.CreateNoise(32, 32, 6);
            _noise.Interpolation = InterpolationMode.Linear;
            _noise.Repeat = RepeatMode.Repeat;

            // Load shader
            _shader = new Shader("files/distort.frag");
            _shader.SetUniform("uNoiseImage", _noise);
        }

        internal override void Draw(Graphics ctx, Rectangle contentBounds)
        {
            _shader.SetUniform("uTime", Time);

            // Compute image centering
            var yScale = contentBounds.Height / _image.Height;
            var xScale = contentBounds.Width / _image.Width;
            var scale = Calc.Min(xScale, yScale);

            var xOffset = contentBounds.Min.X + (contentBounds.Width - (_image.Width * scale)) / 2F;
            var yOffset = contentBounds.Min.Y + (contentBounds.Height - (_image.Height * scale)) / 2F;

            // Draw image
            ctx.Shader = _shader;
            ctx.DrawImage(_image, Matrix.CreateTransform(xOffset, yOffset, 0, scale, scale));
        }
    }
}
