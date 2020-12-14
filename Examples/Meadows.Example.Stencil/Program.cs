using Meadows.Desktop;
using Meadows.Drawing;
using Meadows.Drawing.Software;
using Meadows.Mathematics;
using Meadows.Utilities;

namespace Meadows.Example.Stencil
{
    internal sealed class Program : Application
    {
        public readonly Window Window;

        readonly public GameLoop Loop;

        public Program()
        {
            // At this point desktop window, graphics and audio systems have been initialized.
            Window = new Window("Heirloom - Stencil Example", (1280, 320), MultisampleQuality.Medium) { IsResizable = false };
            RenderStencilTest(Window.Graphics, 10);

            // Write hardware image to disk
            var screenshot = Window.Graphics.GrabPixels();
            screenshot.Write("hardware.jpg");

            var time = 0F;
            Loop = new GameLoop(dt =>
            {
                time += dt;
                RenderStencilTest(Window.Graphics, 4 + Calc.Sin(time) * 6);
                Window.Refresh();
            });
            Loop.Start();
        }

        private static void Main(string[] args)
        {
            var renderHardware = true;

            // If running on hardware, we will run the application.
            // Otherwise we will use a software renderer.
            if (renderHardware) { Run<Program>(); }
            else
            {
                // Initialize graphics system with a software backend.
                // Only one backend can be established at a time.
                using var backend = new SoftwareGraphicsBackend();
                var context = backend.CreateContext(1280, 320);
                RenderStencilTest(context, 10);

                // Write software image to disk
                var screenshot = context.GrabPixels();
                screenshot.Write("software.jpg");
            }
        }

        private static void RenderStencilTest(GraphicsContext gfx, float angle)
        {
            // Load a few images
            var image = new Image("zelda.jpg");
            var imageCenter = (IntVector) (image.Size / 2F);

            gfx.InterpolationMode = InterpolationMode.Linear;
            gfx.Clear(Color.Yellow * Color.DarkGray);

            // Draw base image (darkened)
            gfx.Color = Color.DarkGray;
            DrawBackgroundImages(gfx, image, imageCenter, angle);

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
            DrawBackgroundImages(gfx, image, imageCenter, angle);

            // Clear the stencil, back to regular drawing.
            gfx.ClearMask();

            // Draw regular text again
            gfx.Color = Color.White;
            gfx.SetCamera(Matrix.Identity);
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
    }
}
