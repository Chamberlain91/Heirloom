using System;

using Meadows.Desktop;
using Meadows.Drawing;
using Meadows.IO;
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

        public float Brightness;

        public float Transparency;

        public string SomeText = "Hello World!";

        public string Message = "Lorem Ipsum";

        public Program()
        {
            Window = new Window("Heirloom - Input Example", (400, 600)) { IsResizable = false };

            Console.WriteLine(string.Join(", ", Files.EnumerateFiles()));

            // Launch loop
            GameLoop.StartNew(Update);
        }

        private void Update(float dt)
        {
            Window.Graphics.Clear(Color.Black);
            Window.Graphics.PixelPerfect = true;

            // Set layout box to window
            Gui.BeginFrame(Window.Graphics, dt);
            Gui.SetLayoutBox((16, 16, 250, Window.Surface.Height - 32));

            // todo: button (complete)
            // todo: slider (complete)
            // todo: checkbox
            // todo: radio
            // todo: textbox
            // todo: textarea

            // todo: panel
            // todo: collapse label
            // todo: tabs
            // todo: window

            // todo: scroll panel
            // todo: combo box
            // todo: list view

            Gui.Label("Choose Theme:");
            if (Gui.Button("Light")) { Gui.Theme = GuiTheme.Light; }
            if (Gui.Button("Dark")) { Gui.Theme = GuiTheme.Dark; }

            Gui.Space();

            if (Gui.Slider("Brightness", ref Brightness, step: 10))
            {
                Console.WriteLine($"Brightness: {Brightness:0.00}");
            }

            if (Gui.Slider("Transparency", ref Transparency))
            {
                Console.WriteLine($"Transparency: {Transparency:0.00}");
            }

            if (Gui.StringInput("Some Text", ref SomeText))
            {
                Console.WriteLine($"Text Box: {SomeText}");
            }

            if (Gui.StringInput("Message", ref Message))
            {
                Console.WriteLine($"Message: {Message}");
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
