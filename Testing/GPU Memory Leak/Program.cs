using Heirloom.Desktop;
using Heirloom.Drawing;
using Heirloom.Math;

namespace GPU_Memory_Leak
{
    internal class Program
    {
        private static void Main(string[] args)
        {
            Application.Run(() =>
            {
                var window = new Window("GPU Memory Leak", (512, 512), vsync: false) { IsResizable = false };
                window.Graphics.Performance.OverlayMode = PerformanceOverlayMode.Simple;

                var hue = 0;
                var loop = RenderLoop.Create(window.Graphics, (gfx, dt) =>
                {
                    // Get next color
                    var color = Color.FromHSV(hue, 1, 1);
                    hue = (hue + 1) % 360;

                    // Create Image
                    var image = Image.CreateCheckerboardPattern(window.FramebufferSize, color);

                    // Image -> Surface
                    var surface = new Surface(window.FramebufferSize, MultisampleQuality.None);
                    gfx.Surface = surface;
                    gfx.DrawImage(image, Vector.Zero);

                    // Surface -> Window
                    gfx.Surface = gfx.DefaultSurface;
                    gfx.DrawImage(surface, Vector.Zero);
                });

                // Begin!
                loop.Start();
            });
        }
    }
}
