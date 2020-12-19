using System;

using Meadows.Desktop;
using Meadows.Drawing;
using Meadows.UI;
using Meadows.Utilities;

namespace Meadows.Examples.UserInput
{
    public sealed class Program : Application
    {
        public readonly Window Window;

        public GraphicsContext Graphics => Window.Graphics;

        public Program()
        {
            Window = new Window("Heirloom - Input Example", (512, 512)) { IsResizable = false };

            // Launch loop
            GameLoop.StartNew(Update);
        }

        private void Update(float dt)
        {
            ImGui.Graphics = Window.Graphics;
            ImGui.Graphics.PixelPerfect = true;

            if (ImGui.Button((16, 16, 100, 24), "Click Me"))
            {
                Console.WriteLine("Clicked!");
            }

            //
            Window.Refresh();
        }

        static void Main(string[] args)
        {
            Run<Program>();
        }
    }
}
