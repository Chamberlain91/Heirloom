using Heirloom.Desktop;
using Heirloom.Drawing;
using Heirloom.Mathematics;

namespace Heirloom.Sandbox.Triangulation
{
    internal sealed class Program
    {
        public Window Window { get; }

        public TriangulationApp Game;

        public Program()
        {
            // Create window and go fullscreen
            Window = new Window("Triangulation Sandbox", (512, 544), vsync: true, multisample: MultisampleQuality.Beautiful) { IsResizable = false };
            Window.Position = (IntVector) (Display.Primary.Size - Window.Size) / 2; // Center on display

            // Create benchmark app
            Game = new TriangulationApp(Window.Graphics);

            // Bind loop close and begin loop
            Window.Closed += w => Game.Loop.Stop();
            Game.Loop.Start();
        }

        private static void Main(string[] args)
        {
            Application.Run<Program>();
        }
    }
}
