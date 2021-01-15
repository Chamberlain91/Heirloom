using Heirloom.Desktop;
using Heirloom.Drawing;
using Heirloom.Mathematics;

namespace Heirloom.Testing.Lines
{
    public sealed class Program
    {
        public readonly Window Window;

        public Program()
        {
            Window = new Window("Heirloom - Line Test", (300, 300)) { IsResizable = false };

            // Set additive blending, this helps visualize overlap.
            Window.Graphics.BlendingMode = BlendingMode.Additive;
            Window.Graphics.Clear(Color.Black);

            // Draw horizontal lines
            Window.Graphics.Color = Color.Red;
            Window.Graphics.DrawLine((1, 1), (8, 1));
            Window.Graphics.DrawLine((1, 8), (8, 8));

            // Draw crossing lines
            Window.Graphics.Color = Color.Blue;
            Window.Graphics.DrawLine((1, 1), (8, 8));

            // Draw vertical lines
            Window.Graphics.Color = Color.Green;
            Window.Graphics.DrawLine((1, 1), (1, 8));
            Window.Graphics.DrawLine((8, 1), (8, 8));

            // Grab the drawn image
            var image = Window.Graphics.GrabPixels((0, 0, 10, 10));

            // Display image in window
            Window.Graphics.Clear(Color.Black);
            Window.Graphics.ResetState();
            Window.Graphics.DrawImage(image, (Vector.Zero, Window.Surface.Size));
            Window.Refresh();
        }

        static void Main(string[] args)
        {
            Application.Run<Program>();
        }
    }
}
