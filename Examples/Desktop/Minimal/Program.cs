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

                var ctx = window.RenderContext;

                // Draw hello world text
                ctx.Clear(Color.DarkGray);

                // todo: feature, should be able to vertical align without this.
                var align = new Vector(0, Font.Default.MeasureText(Text, 48).Height / 2F);
                ctx.DrawText(Text, -align + ((Vector) window.FramebufferSize) * 0.5F, Font.Default, 48, TextAlign.Center);

                ctx.SwapBuffers();
            });
        }
    }
}
