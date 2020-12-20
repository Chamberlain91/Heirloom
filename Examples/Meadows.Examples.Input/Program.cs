using System;

using Meadows.Desktop;
using Meadows.Drawing;
using Meadows.Mathematics;
using Meadows.UI;
using Meadows.Utilities;

namespace Meadows.Examples.UserInput
{
    public sealed class Program : Application
    {
        public readonly Window Window;

        public GraphicsContext Graphics => Window.Graphics;

        public Image Icon = Image.CreateCheckerboardPattern(16, 16, Color.Red, 4);

        public float SliderValue;

        public Program()
        {
            Window = new Window("Heirloom - Input Example", (300, 400)) { IsResizable = false };

            // Launch loop
            GameLoop.StartNew(Update);
        }

        private void Update(float dt)
        {
            Gui.Graphics = Window.Graphics;
            Gui.Graphics.PixelPerfect = true;
            Gui.Graphics.Clear(Color.White);

            // Set layout box to window
            Gui.SetLayoutBox((16, 16, Gui.Graphics.Surface.Width - 32, Gui.Graphics.Surface.Height - 32));

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

            Gui.Label("Select Stage:");
            for (var i = 0; i < 10; i++)
            {
                if (Gui.Button($"Stage {i}", i < 4 ? Icon : null))
                {
                    Console.WriteLine($"Clicked on {i}.");
                }
            }

            if (Gui.Slider("Brightness", ref SliderValue))
            {
                Console.WriteLine($"Slider: {SliderValue:0.00}");
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
