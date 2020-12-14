using Meadows.Desktop;
using Meadows.Drawing;
using Meadows.Drawing.Software;
using Meadows.Mathematics;

namespace Meadows.Example.Sandbox
{
    internal sealed class Program : Application
    {
        public readonly Window Window;

        public Program()
        {
            // At this point desktop window, graphics and audio systems have been initialized.
            Window = new Window("Meadows Example", (1280, 320), MultisampleQuality.Medium) { IsResizable = false };
            RenderStencilTest(Window.Graphics, "hardware.jpg");
            Window.Refresh();
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
                RenderStencilTest(context, "software.jpg");
            }
        }

        private static void RenderStencilTest(GraphicsContext gfx, string fileName)
        {
            // Load a few images
            var image = new Image("zelda.jpg");
            var imageCenter = (IntVector) (image.Size / 2F);

            gfx.InterpolationMode = InterpolationMode.Linear;
            gfx.Clear(Color.Yellow * Color.DarkGray);

            // Draw base image (darkened)
            gfx.Color = Color.DarkGray;
            DrawBackgroundImages(gfx, image, imageCenter);

            // Draw a stencil mask
            gfx.BeginDefineMask();
            gfx.PushState();
            {
                // Set camera pointed at zero and tilted.
                // Then draw text at zero to populate the stencil.
                gfx.SetCamera(Vector.Down * 66F, rotation: 6 * Calc.ToRadians);
                gfx.DrawText("Princess Zelda", Vector.Zero, Font.Default, 200, TextAlign.Center | TextAlign.Middle);
            }
            gfx.PopState();
            gfx.EndDefineMask();

            // White for full brightness
            gfx.Color = Color.White;

            // Draw image (uses above stencil)
            DrawBackgroundImages(gfx, image, imageCenter);

            // Clear the stencil, back to regular drawing.
            gfx.ClearMask();

            // Draw regular text again
            gfx.Color = Color.White;
            gfx.SetCamera(Matrix.Identity);
            gfx.DrawText("Heirloom 2D Graphics", (gfx.Surface.Width - 8, 8), Font.Default, 16, TextAlign.Top | TextAlign.Right);

            // Write rendered image to disk
            var screenshot = gfx.GrabPixels();
            screenshot.Write(fileName);
        }

        private static void DrawBackgroundImages(GraphicsContext gfx, Image image, IntVector imageCenter)
        {
            var imageTransform2 = Matrix.CreateTranslation((Vector) (gfx.Surface.Size - (image.Size * 10)) / 2) * ComputeCenteredRotation(imageCenter, 10 * Calc.ToRadians);
            gfx.DrawImage(image, imageTransform2 * Matrix.CreateScale(10));

            var imageTransform = Matrix.CreateTranslation((Vector) (gfx.Surface.Size - image.Size) / 2) * ComputeCenteredRotation(imageCenter, 10 * Calc.ToRadians);
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
