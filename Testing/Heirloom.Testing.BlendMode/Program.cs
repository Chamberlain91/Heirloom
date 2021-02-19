using Heirloom.Desktop;
using Heirloom.Drawing;
using Heirloom.Mathematics;

namespace Heirloom.Testing.BlendMode
{
    public sealed class Program
    {
        public readonly Window Window;

        public readonly Color[] Backgrounds = new[]
        {
            Color.Black,
            Color.White,
            Color.Red,
            Color.Blue,
            Color.Green,
        };

        public static readonly Color TransparentBlack = new Color(0, 0, 0, 1F / 128);
        public static readonly Color TransparentWhite = new Color(1, 1, 1, 1F / 128);

        public Program()
        {
            var modes = System.Enum.GetValues<BlendingMode>();
            var windowSize1 = 150 * modes.Length;
            var windowSize2 = 150 * Backgrounds.Length;

            Window = new Window("Heirloom - Blend Mode Test", (windowSize1, windowSize2)) { IsResizable = false };

            var overlay = GenerateOverlay(Window.Graphics);
            overlay.Write("overlay.png");

            Window.Graphics.Clear(Color.Black);
            Window.Graphics.ResetState();

            for (var j = 0; j < Backgrounds.Length; j++)
            {
                var background = Backgrounds[j];
                var y = j * 150;

                Window.Graphics.Color = background;
                Window.Graphics.BlendingMode = BlendingMode.Alpha;
                Window.Graphics.DrawRect((0, y, windowSize1, 150));

                for (var i = 0; i < modes.Length; i++)
                {
                    var mode = modes[i];
                    var x = i * 150;

                    Window.Graphics.Color = Color.White;
                    Window.Graphics.BlendingMode = mode;
                    Window.Graphics.DrawImage(overlay, Matrix.CreateTranslation(x, y));

                    Window.Graphics.Color = Color.Pink;
                    Window.Graphics.BlendingMode = BlendingMode.Alpha;
                    Window.Graphics.DrawText($"{mode}", (x + 10, y + 10), Font.Default, 16);
                    Window.Graphics.Color = Color.Black;
                    Window.Graphics.DrawRectOutline((x, y, 150, 150), 3);
                }
            }

            Window.Refresh();
        }

        static void Main(string[] args)
        {
            Application.Run<Program>();
        }

        public static Image GenerateOverlay(GraphicsContext gfx)
        {
            var surface = new Surface(150, 150);

            gfx.ResetState();
            gfx.SetSurface(surface);
            gfx.Clear(Color.TransparentBlack);

            var grayscaleRamp = new Gradient { { 0F, Color.White }, { 1F, Color.Black } };
            var gradient = Image.CreateGradient(150, 50, grayscaleRamp, Axis.Horizontal);
            gfx.DrawImage(gradient, Matrix.CreateTranslation(0, 0));

            gfx.Color = Color.Red;
            gfx.DrawRect((0, 50, 50, 50));

            gfx.Color = Color.Blue;
            gfx.DrawRect((50, 50, 50, 50));

            gfx.Color = Color.Green;
            gfx.DrawRect((100, 50, 50, 50));

            gfx.Color = Color.White;
            var alphaRamp1 = new Gradient(GradientMode.RGB) { { 0F, Color.White }, { 1F, TransparentWhite } };
            var alphaRamp2 = new Gradient(GradientMode.RGB) { { 0F, TransparentBlack }, { 1F, Color.Black } };
            var gradient1 = Image.CreateGradient(75, 50, alphaRamp1, Axis.Horizontal);
            var gradient2 = Image.CreateGradient(75, 50, alphaRamp2, Axis.Horizontal);
            gfx.DrawImage(gradient1, Matrix.CreateTranslation(0, 100));
            gfx.DrawImage(gradient2, Matrix.CreateTranslation(75, 100));

            // Extract surface to image
            return gfx.GrabPixels();
        }
    }
}
