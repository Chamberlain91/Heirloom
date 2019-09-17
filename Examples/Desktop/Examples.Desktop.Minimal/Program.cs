using System.Runtime.InteropServices;

using Heirloom.Desktop;
using Heirloom.Drawing;
using Heirloom.Math;

namespace Examples.Minimal
{
    internal static class Program
    {
        private static void Main(string[] args)
        {
            var text = $"Hello {GetOperatingSystem()}!";
            var fontSize = 48;

            Application.Run(() =>
            {
                // Create window
                var window = new Window(500, 300, "Minimal Example");
                window.IsResizable = false;

                // 
                var ctx = window.RenderContext;

                // Draw hello world text
                ctx.ResetState(); // bug: should not need to be called, but clear color doesn't show?!
                ctx.Clear(Color.DarkGray);

                // todo: Implement feature, should be able to vertical align text without this step.
                var align = new Vector(0, Font.Default.MeasureText(text, fontSize).Height / 2F);
                ctx.DrawText(text, -align + ((Vector) window.FramebufferSize) * 0.5F, Font.Default, fontSize, TextAlign.Center);

                // Put drawn image on screen
                ctx.SwapBuffers();
            });
        }

        private static string GetOperatingSystem()
        {
            if (RuntimeInformation.IsOSPlatform(OSPlatform.Windows)) { return "Windows"; }
            else if (RuntimeInformation.IsOSPlatform(OSPlatform.OSX)) { return "macOS"; }
            else if (RuntimeInformation.IsOSPlatform(OSPlatform.Linux)) { return "Linux"; }
            else { return "Computer?"; }
        }
    }
}
