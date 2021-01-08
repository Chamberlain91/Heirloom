using Meadows.Desktop;

namespace Meadows.Benchmark.Desktop
{
    internal class Program : Application
    {
        public Window Window { get; }

        public BenchmarkApp Game;

        public Program()
        {
            // Create window and go fullscreen
            Window = new Window("Meadows Benchmark", vsync: false);

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
