using Heirloom.Desktop;
using Heirloom.Drawing;
using Heirloom.Math;

namespace Examples.Minimal
{
    internal static class Program
    {
        private static void Main(string[] args)
        {
            const string Text = "Hello World!";

            Application.Run(() =>
            {
                // Create window
                var window = new Window(500, 300, "Minimal Example");
                window.IsResizable = false;

                // Draw hello world text
                window.RenderContext.ResetState(); // TODO: Bug, should not be needed here
                window.RenderContext.Clear(Color.DarkGray);

                // todo: feature, should be able to vertical align without this.
                var align = new Vector(0, Font.Default.MeasureText(Text, 48).Height / 2F);
                window.RenderContext.DrawText(Text, -align + ((Vector) window.FramebufferSize) * 0.5F, TextAlign.Center, Font.Default, 48, Color.White);

                window.RenderContext.SwapBuffers();
            });
        }
    }
}
