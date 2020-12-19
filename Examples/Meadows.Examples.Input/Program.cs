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

        public Image Icon = Image.CreateRadialGradient(16, 16, new Gradient { { 0F, Color.Magenta }, { 1F, Color.Cyan } });

        public Program()
        {
            Window = new Window("Heirloom - Input Example", (300, 400)) { IsResizable = false };

            // Launch loop
            GameLoop.StartNew(Update);
        }

        private void Update(float dt)
        {
            ImGui.Graphics = Window.Graphics;
            ImGui.Graphics.PixelPerfect = true;
            ImGui.Graphics.Clear(Color.Gray);

            // Set layout box to window
            ImGuiLayout.SetLayoutBox((16, 16, ImGui.Graphics.Surface.Width - 32, ImGui.Graphics.Surface.Height - 32));

            // todo: slider
            // todo: checkbox
            // todo: radio
            // todo: textbox
            // todo: window
            // todo: panel
            // todo: tabs
            // todo: collapse label
            // todo: combo box
            // todo: list view
            // todo: scroll panel

            ImGuiLayout.Label("Select Stage:");
            for (var i = 0; i < 10; i++)
            {
                if (ImGuiLayout.Button($"Stage {i}", i < 4 ? Icon : null))
                {
                    Console.WriteLine($"Clicked on {i}.");
                }
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
