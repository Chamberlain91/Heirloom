using Heirloom.Desktop;
using Heirloom.Drawing;
using Heirloom.Math;

namespace Examples.Minimal
{
    internal static class Program
    {
        private static void Main(string[] args)
        {
            Application.Run(() =>
            {
                // Create window
                var window = new Window(500, 300, "Minimal Example");

                // Draw hello world text
                window.RenderContext.ResetState(); // TODO: Bug, should not be needed here
                window.RenderContext.Clear(Color.DarkGray);
                window.RenderContext.DrawText("Hello World!", ((Vector) window.FramebufferSize) * 0.5F, TextAlign.Center, Font.Default, 48, Color.White);
                window.RenderContext.SwapBuffers();
            });
        }
    }
}
