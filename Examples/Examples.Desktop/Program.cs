using System;

using Heirloom.Desktop;
using Heirloom.Drawing;
using Heirloom.IO;
using Heirloom.Math;

namespace Examples.Desktop
{
    internal class Program
    {
        public static Shader InvertShader;

        public static Image Image;

        private static void Main(string[] args)
        {
            Application.Run(() =>
            {
                // Loads the inverted color shader
                InvertShader = new Shader("files/invert.frag");

                // 
                Image = new Image(Files.OpenStream("files/cardHeartsQ.png"));

                // Create Window
                var window = new Window("Window Information and Events");
                window.KeyRelease += Window_Key;
                window.KeyPress += Window_Key;

                // 
                var loop = RenderLoop.Create(window.Graphics, Update);
                loop.Start();
            });
        }

        private static void Window_Key(Window win, KeyEvent ev)
        {
            if (ev.Action == ButtonAction.Press)
            {
                var key = ev.Key.ToString().ToSmartDisplayName().Replace(' ', '_').ToLower();

                var nameA = Keyboard.GetPrintableKeyName(ev.Key);
                var nameB = Keyboard.GetPrintableKeyName(ev.ScanCode);
                Console.WriteLine($"{key}: '{nameA}' . '{nameB}'");
            }
            else
            {

            }
        }

        private static void Update(Graphics gfx, float dt)
        {
            gfx.Clear(Color.DarkGray);

            gfx.Shader = InvertShader;

            InvertShader.SetUniform("uStrength", Calc.Random.NextFloat());
            gfx.DrawImage(Image, Matrix.CreateTranslation(230, 30));

            InvertShader.SetUniform("uStrength", 1.0F);
            gfx.DrawImage(Image, Matrix.CreateTranslation(430, 30));

            gfx.Shader = Shader.Default;
            gfx.DrawImage(Image, Matrix.CreateTranslation(30, 30));
        }
    }
}
