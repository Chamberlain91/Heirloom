using Heirloom.Drawing;
using Heirloom.IO;
using Heirloom.Math;
using Heirloom.Platforms.Desktop;

namespace Heirloom.Examples.SimpleDrawing
{
    internal class Example : GameWindow
    {
        public Image Image;

        public static Color Transparent = new Color(0.1F, 0.1F, 0.2F, 0.8F);

        public Example()
            : base("Simple Drawing", transparent: true)
        {
            // Image by https://pixabay.com/users/nara_kim/
            Image = new Image(Files.ReadBytes("files/image.png"));

            // 
            Size = (800, 500);
        }

        protected override void Update()
        {
            // Do Nothing
        }

        protected override void Render(RenderContext ctx)
        {
            // 
            ctx.Clear(Transparent);

            // Draws the image horizontally centered while vertically 
            // scaling the image to fit.
            var scale = (ctx.DefaultSurface.Height - 64) / (float) Image.Height;
            var x = (ctx.Surface.Width - (Image.Width * scale)) / 2F;
            ctx.Draw(Image, Matrix.CreateTransform(x, 32, 0, scale, scale));
        }

        private static void Main(string[] _)
        {
            Run(new Example());
        }
    }
}
