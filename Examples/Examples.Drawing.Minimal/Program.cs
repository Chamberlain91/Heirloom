using System;
using System.Runtime.InteropServices;

using Heirloom.Desktop;
using Heirloom.Drawing;
using Heirloom.Math;

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
                var window = new Window("Minimal Example") { Size = (400, 200) };

                // Loop
                var loop = RenderLoop.Create(window.Graphics, OnDraw);
                loop.Start();
            });
        }

        private static void OnDraw(Graphics ctx, float dt)
        {
            ctx.Clear(Color.DarkGray);

            // Generate hello message
            var text = $"Hello {GetOperatingSystem()}!";

            // todo: Implement feature, should be able to vertical align text without this step.
            var align = new Vector(0, TextLayout.Measure(text, Font.Default, FontSize).Height / 2F);
            ctx.DrawText(text, -align + ((Vector) ctx.Surface.Size) * 0.5F, Font.Default, FontSize, TextAlign.Center);
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
