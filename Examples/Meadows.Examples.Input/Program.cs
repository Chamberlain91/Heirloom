using System;

using Meadows.Desktop;
using Meadows.Drawing;
using Meadows.IO;
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

        public string Message = "Lorem ipsum dolor sit amet, consectetur adipiscing elit. Nam sed metus pulvinar, dictum tellus non, varius lorem. Nunc vehicula urna non consectetur malesuada.";

        public Program()
        {
            Window = new Window("Heirloom - Input Example", (256 + 32, 400), MultisampleQuality.None, vsync: false) { IsResizable = false };
            Window.Graphics.Performance.ShowOverlay = true;

            Console.WriteLine(string.Join("\n", Files.EnumerateFiles()));

            // Launch loop
            GameLoop.StartNew(Update);
        }

        private void Update(float dt)
        {
            Window.Graphics.Clear(Color.Black);
            Window.Graphics.ResetState();

            // Set layout box to window
            Gui.BeginFrame(Window.Graphics, dt);
            Gui.SetLayoutBox((16, 16, 256, Window.Surface.Height - 32));

            // todo: button (complete)
            // todo: checkbox
            // todo: radio
            // todo: slider (complete)
            // todo: text input
            // todo: + single line (en progress)
            // todo: + multi line (en progress)
            // todo: label
            // todo: title

            // todo: panel
            // todo: scroll panel
            // todo: collapse label
            // todo: tabs
            // todo: window

            // todo: layout row
            // todo: + number of splits (ie, 3 for three splits)
            // todo: + ratio of splits  (ie: [3, 1] for two but first is 3x larger)
            // todo: + concat elements
            // todo: layout column (default)

            // todo: combo box
            // todo: list view
            // todo: tree view
            // todo: grid view

            // todo: disable section

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

            if (Gui.TextInput("Some Text", ref SomeText))
            {
                Console.WriteLine($"Text Box: {SomeText}");
            }

            if (Gui.TextInput("Message", ref Message, multiline: true))
            {
                Console.WriteLine($"Message: {Message}");
            }

            if (Gui.TextInput("Some Text 2", ref SomeText))
            {
                Console.WriteLine($"Text Box: {SomeText}");
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
