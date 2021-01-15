using Heirloom.Desktop;
using Heirloom.Drawing;
using Heirloom.Mathematics;
using Heirloom.Utilities;

namespace Heirloom.Testing.MemoryLeak
{
    public sealed class Program
    {
        public Program()
        {
            var window = new Window("Atlas Thrashing", (1280, 720), vsync: false) { IsResizable = false };
            window.Position = (IntVector) (Display.Primary.Size - window.Size) / 2; // Center on display

            window.Graphics.Performance.ShowOverlay = true;

            var xcount = window.Size.Width / 64;
            var ycount = window.Size.Height / 64;

            var hue = 0;
            var loop = new GameLoop(dt =>
            {
                // Get next color
                var color = Color.FromHSV(hue, 1, 1);
                hue = (hue + 1) % 360;

                // Create Image
                var image = Image.CreateCheckerboardPattern(window.Surface.Size, color);

                // Create Surface
                var surface = new Surface(window.Surface.Size, MultisampleQuality.None);

                // Draw image to surface
                window.Graphics.SetRenderTarget(surface);
                window.Graphics.DrawImage(image, Matrix.Identity);

                // Draw surface to screen
                window.Graphics.SetRenderTarget(window.Surface);
                window.Graphics.DrawImage(surface, Matrix.Identity);

                // Present to screen
                window.Refresh();
            });

            loop.Start();
        }

        private static void Main(string[] args)
        {
            Application.Run<Program>();
        }
    }
}
