using Meadows.Desktop;
using Meadows.Drawing;
using Meadows.Mathematics;
using Meadows.Utilities;

namespace Meadows.Examples.Stencil
{
    internal sealed class Program : Application
    {
        public readonly Window Window;

        public Image Image;

        public Program()
        {
            Image = new Image("zelda.jpg");

            // At this point desktop window, graphics and audio systems have been initialized.
            Window = new Window("Heirloom - Stencil Example", (1280, 320), MultisampleQuality.Medium) { IsResizable = false };
            RenderStencilTest(Window.Graphics, 10);

            // Write screengrab to disk
            var screenshot = Window.Graphics.GrabPixels();
            screenshot.Write("screengrab.jpg");

            var time = 0F;
            GameLoop.StartNew(dt =>
            {
                time += dt;
                RenderStencilTest(Window.Graphics, 4 + Calc.Sin(time) * 6);
                Window.Refresh();
            });
        }

        private void RenderStencilTest(GraphicsContext gfx, float angle)
        {
            var imageCenter = (IntVector) (Image.Size / 2F);

            gfx.InterpolationMode = InterpolationMode.Linear;
            gfx.Clear(Color.Yellow * Color.DarkGray);

            // Draw base image (darkened)
            gfx.Color = Color.DarkGray;
            DrawBackgroundImages(gfx, Image, imageCenter, angle);

            // Draw a stencil mask
            gfx.BeginDefineMask();
            gfx.PushState();
            {
                // Set camera pointed at zero and tilted.
                // Then draw text at zero to populate the stencil.
                gfx.SetCamera(Vector.Down * 66F, rotation: angle * 0.66F * Calc.ToRadians);
                gfx.DrawText("Princess Zelda", Vector.Zero, Font.Default, 200, TextAlign.Center | TextAlign.Middle);
            }
            gfx.PopState();
            gfx.EndDefineMask();

            // White for full brightness
            gfx.Color = Color.White;

            // Draw image (uses above stencil)
            DrawBackgroundImages(gfx, Image, imageCenter, angle);

            // Clear the stencil, back to regular drawing.
            gfx.ClearMask();

            // Draw regular text again
            gfx.Color = Color.White;
            //gfx.SetCamera(Matrix.Identity);
            gfx.DrawText("Heirloom 2D Graphics", (gfx.Surface.Width - 8, 8), Font.Default, 16, TextAlign.Top | TextAlign.Right);
        }

        private static void DrawBackgroundImages(GraphicsContext gfx, Image image, IntVector imageCenter, float angle)
        {
            var imageTransform2 = Matrix.CreateTranslation((Vector) (gfx.Surface.Size - (image.Size * 10)) / 2) * ComputeCenteredRotation(imageCenter, angle / 10F * Calc.ToRadians);
            gfx.DrawImage(image, imageTransform2 * Matrix.CreateScale(10));

            var imageTransform = Matrix.CreateTranslation((Vector) (gfx.Surface.Size - image.Size) / 2) * ComputeCenteredRotation(imageCenter, angle * Calc.ToRadians);
            gfx.DrawImage(image, imageTransform);
        }

        private static Matrix ComputeCenteredRotation(Vector center, float rotation)
        {
            return Matrix.CreateTranslation(center)
                 * Matrix.CreateRotation(rotation)
                 * Matrix.CreateTranslation(-center);
        }

        private static void Main(string[] args)
        {
            Run<Program>();
        }
    }
}
