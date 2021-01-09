using Meadows.Desktop;
using Meadows.Mathematics;

namespace Meadows.Benchmark.Desktop
{
    internal class Program : Application
    {
        public Window Window { get; }

        public BenchmarkApp Game;

        public Program()
        {
            // Create window and go fullscreen
            Window = new Window("Meadows Benchmark", (1600, 960), vsync: false) { IsResizable = false };
            Window.Position = (IntVector) (Display.Primary.Size - Window.Size) / 2; // Center on display

            // Create benchmark app
            Game = new BenchmarkApp(Window.Graphics);

            // Bind loop close and begin loop
            Window.Closed += w => Game.Loop.Stop();
            Game.Loop.Start();
        }

        private static void Main(string[] args)
        {
            Run<Program>();
        }
    }
}
