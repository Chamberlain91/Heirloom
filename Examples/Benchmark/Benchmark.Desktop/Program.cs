using System;

using Heirloom.Desktop;
using Heirloom.Desktop;

namespace Benchmark
{
    internal static class Program
    {
        private static void Main(string[] args)
        {
            Application.Run(() =>
            {
                // 
                var win = new Window("Heirloom Benchmark", new WindowCreationSettings { VSync = false });

                // Try fullscreen, otherwise maximize
                // Note: This might be useless, but one time on a macbook it failed.
                try { win.SetFullscreen(Monitor.Default); }
                catch (Exception) { win.Maximize(); }

                // Create app instance
                var app = new BenchmarkApp(60, win.Graphics);
                app.Start();

                // Bind escape key
                win.KeyPress += (_, ev) =>
                {
                    if (ev.Key == Key.Escape)
                    {
                        // Note: must stop the render loop and window in this order, otherwise the render loop
                        // may try to draw on the already closed window, causing an exception
                        app.Stop();  // terminate rendering
                        win.Close(); // terminate window
                    }
                };
            });
        }
    }
}
