﻿using Heirloom.Desktop;
using Heirloom.Drawing;
using Heirloom.IO;
using Heirloom.Math;

namespace Examples.Shaders
{
    public static class Program
    {
        public static Shader GrayscaleShader;
        public static Shader InvertShader;

        public static Image Image;

        public static float Time;

        private static void Main(string[] args)
        {
            Application.Run(() =>
            {
                // Loads the inverted color shader
                GrayscaleShader = new Shader("files/grayscale.frag");
                InvertShader = new Shader("files/invert.frag");

                // Load queen of hearts image
                Image = new Image(Files.OpenStream("files/cardHeartsQ.png"));

                // Create Window and fits it around 3 cards
                var window = new Window("Window Information and Events");
                window.Size = (32 + (Image.Width + 32) * 3, Image.Height + 64);

                // 
                var loop = RenderLoop.Create(window.Graphics, Update);
                loop.Start();
            });
        }

        private static void Update(Graphics gfx, float dt)
        {
            Time += dt;

            // Clear the frame
            gfx.Clear(Color.DarkGray);

            // Update grayscale shader to use time.
            // Its important to note that updating a uniform effectively breaks the batching mechanism.
            // So you should try to engineer your shaders to minimally update uniforms.
            GrayscaleShader.SetUniform("uStrength", 0.5F + Calc.Cos(Time * Calc.TwoPi) * 0.5F);

            // Draws w/ grayscale shader
            gfx.Shader = GrayscaleShader;
            gfx.DrawImage(Image, Matrix.CreateTranslation(32 + (32 + Image.Width) * 2, 32));

            // Draws w/ inversion shader
            gfx.Shader = InvertShader;
            gfx.DrawImage(Image, Matrix.CreateTranslation(32 + (32 + Image.Width) * 1, 32));

            // Draws w/ default shader
            gfx.Shader = Shader.Default;
            gfx.DrawImage(Image, Matrix.CreateTranslation(32 + (32 + Image.Width) * 0, 32));
        }
    }
}