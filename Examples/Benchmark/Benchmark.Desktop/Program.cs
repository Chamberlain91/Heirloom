using System;

using Heirloom.Desktop;
using Heirloom.Drawing;
using Heirloom.GLFW;

namespace Benchmark
{
    internal class Program : GameWindow
    {
        public BenchmarkApp App;

        public Program()
            : base("Heirloom Benchmark", vsync: false)
        {
            // Try fullscreen, otherwise maximize
            // Note: This might be useless, but one time on a mac it failed
            try { SetFullscreen(Monitor.Default); }
            catch (Exception) { Maximize(); }

            // Display FPS
            ShowFPSOverlay = true;

            // Create app instance
            App = new BenchmarkApp(60, RenderContext.DefaultSurface);
        }

        protected override void Update(float dt)
        {
            App.Update(dt);
        }

        protected override void Update(RenderContext ctx, float dt)
        {
            App.Render(ctx, dt);
        }

        protected override void OnKeyPressed(Key key, int scancode, ButtonAction action, KeyModifiers modifiers)
        {
            if (key == Key.Escape)
            {
                Close();
            }
        }

        private static void Main(string[] args)
        {
            Application.Run(() => new Program());
        }
    }
}
