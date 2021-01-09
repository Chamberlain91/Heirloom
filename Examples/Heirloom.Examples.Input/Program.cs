using System;

using Heirloom.Desktop;
using Heirloom.Drawing;
using Heirloom.IO;
using Heirloom.Mathematics;
using Heirloom.UI;
using Heirloom.Utilities;

namespace Heirloom.Examples.UserInput
{
    public sealed class Program : Application
    {
        public readonly Window Window;

        public Image Icon = Image.CreateCheckerboardPattern(16, 16, Color.Red, 4);

        public float Brightness;

        public float Transparency;

        public string SomeText = "Hello World!";

        public string Message = "Lorem ipsum dolor sit amet, consectetur adipiscing elit. Nam sed metus pulvinar, dictum tellus non, varius lorem. Nunc vehicula urna non consectetur malesuada.";

        public Program()
        {
            Console.WriteLine(string.Join("\n", Files.EnumerateFiles()));

            // 
            Window = new Window("Heirloom - Input Example", (256 + 32, 450), MultisampleQuality.None, vsync: false) { IsResizable = false };
            Window.Graphics.Performance.ShowOverlay = true;

            // Launch loop
            GameLoop.StartNew(Update);
        }

        private void Update(float dt)
        {
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

            // todo: make individual contexts
            Gui.BeginFrame(Window.Graphics, dt);

            Gui.Panel("IMGUI Example", (IntVector.Zero, Window.Surface.Size), () =>
            {
                Gui.Label("Choose Style");

                if (Gui.Button("Light")) { Gui.Style = GuiStyle.Light; }
                if (Gui.Button("Dark")) { Gui.Style = GuiStyle.Dark; }

                Gui.Divider();

                if (Gui.Button("Warning", Icon)) { Log.Warning("A warning"); }

                Gui.Divider();

                if (Gui.Slider("Brightness", ref Brightness, step: 10))
                {
                    Console.WriteLine($"Brightness: {Brightness:0.00}");
                }

                if (Gui.Slider("Transparency", ref Transparency))
                {
                    Console.WriteLine($"Transparency: {Transparency:0.00}");
                }

                Gui.Divider();

                if (Gui.TextInput("Some Text", ref SomeText))
                {
                    Console.WriteLine($"Text Box: {SomeText}");
                }

                //Gui.Layout(-1); // Push to bottom of container
                if (Gui.TextInput("Message", ref Message, multiline: true))
                {
                    Console.WriteLine($"Message: {Message}");
                }
            });

            //var gui = new GuiContext(Window.Graphics);
            //gui.Label("Quick Travel");
            //gui.BeginScroll("quick_travel");
            //for (var i = 0; i < 10; i++)
            //{
            //    if (gui.Button($"Area {i}"))
            //    {
            //        // Go to area
            //    }
            //}
            //gui.EndScroll();

            Window.Refresh();
        }

        static void Main(string[] args)
        {
            Run<Program>();
        }
    }
}
