using Heirloom.Desktop;
using Heirloom.Drawing;

namespace Benchmark
{
    internal class Program : GameWindow
    {
        public BenchmarkApp App;

        public Program()
            : base("Heirloom Benchmark", vsync: false)
        {
            // SetFullscreen(Monitor.Default);
            Maximize();

            // Display FPS
            ShowFPSOverlay = true;

            // Create app instance
            App = new BenchmarkApp(60, 5, 10000);
        }

        protected override void Update(float dt)
        {
            App.Update(dt);
        }

        protected override void Draw(RenderContext ctx, float dt)
        {
            App.Render(ctx, dt);
        }

        private static void Main(string[] args)
        {
            Application.Run(() => new Program());
        }
    }
}
