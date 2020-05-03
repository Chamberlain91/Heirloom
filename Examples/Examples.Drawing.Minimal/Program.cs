using System.Runtime.InteropServices;

using Heirloom;
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

                // Clear the window, draw text centered on screen
                window.Graphics.Clear(Color.DarkGray);
                window.Graphics.DrawText(text, ((Vector) window.Graphics.Surface.Size) * 0.5F, Font.Default, FontSize, TextAlign.Center | TextAlign.Middle);
                window.Refresh();
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
