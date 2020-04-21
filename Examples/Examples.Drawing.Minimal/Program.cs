using System.Runtime.InteropServices;

using Heirloom;
using Heirloom.Drawing;
using Heirloom.Desktop;

namespace Examples.Minimal
{
    internal static class Program
    {
        private const int FontSize = 32;

        private static void Main(string[] args)
        {
            Application.Run(() =>
            {
                // Create window
                var window = new Window("Minimal Example", (400, 200)) { IsResizable = false };

                // Create some text
                var text = $"Hello {GetOperatingSystem()}!";

                // Measure the vertical height of the text
                // todo: Implement feature, should be able to vertical align text without this step.
                var offset = new Vector(0, TextLayout.Measure(text, Font.Default, FontSize).Height / 2F);

                // Clear the window, draw text and push to the screen
                window.Graphics.Clear(Color.DarkGray);
                window.Graphics.DrawText(text, -offset + ((Vector) window.Graphics.Surface.Size) * 0.5F, Font.Default, FontSize, TextAlign.Center);
                window.Graphics.RefreshScreen();
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
