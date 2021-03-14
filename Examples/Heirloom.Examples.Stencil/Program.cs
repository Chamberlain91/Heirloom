using Heirloom.Desktop;
using Heirloom.Drawing;
using Heirloom.Mathematics;
using Heirloom.Utilities;

namespace Heirloom.Examples.Stencil
{
    internal sealed class Program
    {
        public readonly Window Window;

        public Image Image;

        public Program()
        {
            // At this point desktop window, graphics and audio systems have been initialized.
            Window = new Window("Heirloom - Stencil Example", (1280, 320), MultisampleQuality.Beautiful) { IsResizable = false };
            Window.Graphics.Performance.ShowOverlay = true;

            // Load our image
            Image = new Image("zelda.jpg") { Interpolation = InterpolationMode.Linear };

            var time = 0F;
            GameLoop.StartNew(dt =>
            {
                time += dt;

                var angle = 4 + (Calc.Sin(time) * 6);

                Window.Graphics.Clear(Color.Yellow * Color.DarkGray);

                // Draw base image (darkened)
                Window.Graphics.Color = Color.DarkGray;
                DrawBackgroundImages(Window.Graphics, Image, angle);

                // Draw a stencil mask
                Window.Graphics.BeginDefineMask();
                Window.Graphics.PushState();
                {
                    var center = (Vector) Window.Graphics.Surface.Size / 2F;
                    Window.Graphics.Transform = CreateRotationCenter(angle / 2F * Calc.ToRadians, center);
                    Window.Graphics.DrawText("Princess Zelda", center, Font.Default, 200, TextAlign.Center | TextAlign.Middle);
                }
                Window.Graphics.PopState();
                Window.Graphics.EndDefineMask();

                // White for full brightness
                Window.Graphics.Color = Color.White;

                // Draw image (uses above stencil)
                DrawBackgroundImages(Window.Graphics, Image, angle);

                // Clear the stencil, back to regular drawing.
                Window.Graphics.ClearMask();

                // Draw regular text again
                Window.Graphics.DrawText($"Heirloom 2D Graphics", (Window.Graphics.Surface.Width - 8, 8), Font.Default, 16, TextAlign.Top | TextAlign.Right);

                // Update the screen
                Window.Refresh();
            });
        }

        private static void DrawBackgroundImages(GraphicsContext gfx, Image image, float angle)
        {
            var center = (IntVector) (image.Size / 2F);

            var tranformA = Matrix.CreateTranslation((Vector) (gfx.Surface.Size - (image.Size * 10)) / 2) * CreateRotationCenter(angle / 10F * Calc.ToRadians, center);
            gfx.DrawImage(image, tranformA * Matrix.CreateScale(10));

            var transformB = Matrix.CreateTranslation((Vector) (gfx.Surface.Size - image.Size) / 2) * CreateRotationCenter(angle * Calc.ToRadians, center);
            gfx.DrawImage(image, transformB);
        }

        private static Matrix CreateRotationCenter(float angle, Vector center)
        {
            return Matrix.CreateTranslation(center)
                 * Matrix.CreateRotation(angle)
                 * Matrix.CreateTranslation(-center);
        }

        private static void Main(string[] args)
        {
            Application.Run<Program>();
        }
    }
}
