using Heirloom.Desktop;
using Heirloom.Drawing;
using Heirloom.Drawing.Software;
using Heirloom.Mathematics;
using Heirloom.Utilities;

namespace Heirloom.Examples.Stencil
{
    internal sealed class Program
    {
        public readonly Window Window;

        public static Image Image;

        public Program()
        {
            // At this point desktop window, graphics and audio systems have been initialized.
            Window = new Window("Heirloom - Stencil Example", (1280, 320), MultisampleQuality.None) { IsResizable = false };
            Window.Graphics.Performance.ShowOverlay = true;

            // Write hardware rendered image to disk. This is to compare with the software
            // implementation. See Main() below.
            RenderStencilTest(Window.Graphics, 10);
            var screenshot = Window.Graphics.GrabPixels();
            screenshot.Write("hardware.png");

            var time = 0F;
            GameLoop.StartNew(dt =>
            {
                time += dt;
                RenderStencilTest(Window.Graphics, 4 + Calc.Sin(time) * 6);
                Window.Refresh();
            });
        }

        private static void RenderStencilTest(GraphicsContext gfx, float angle)
        {
            gfx.Clear(Color.Yellow * Color.DarkGray);

            // Draw base image (darkened)
            gfx.Color = Color.DarkGray;
            DrawBackgroundImages(gfx, Image, angle);

            // Draw a stencil mask
            gfx.BeginDefineMask();
            gfx.PushState();
            {
                var center = (Vector) gfx.Surface.Size / 2F;
                gfx.Transform = CreateRotationCenter(angle / 2F * Calc.ToRadians, center);
                gfx.DrawText("Princess Zelda", center, Font.Default, 200, TextAlign.Center | TextAlign.Middle);
            }
            gfx.PopState();
            gfx.EndDefineMask();

            // White for full brightness
            gfx.Color = Color.White;

            // Draw image (uses above stencil)
            DrawBackgroundImages(gfx, Image, angle);

            // Clear the stencil, back to regular drawing.
            gfx.ClearMask();

            // Draw regular text again
            gfx.DrawText($"Heirloom 2D Graphics", (gfx.Surface.Width - 8, 8), Font.Default, 16, TextAlign.Top | TextAlign.Right);
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
            // Load our image
            Image = new Image("zelda.jpg") { Interpolation = InterpolationMode.Linear };

            var useSoftwareRenderer = !true;

            // Please Note: These are MUTUALLY EXCLUSIVE. You cannot switch between backends for the lifetime of an application.
            // Either render a single frame with the software renderer (slow, can only render to in memory bitmaps)
            if (useSoftwareRenderer) { RenderWithSoftwareImplementation(); }
            // or render with hardware acceleration into a window.
            else { Application.Run<Program>(); }
        }

        private static void RenderWithSoftwareImplementation()
        {
            var softwareBackend = new SoftwareGraphicsBackend();

            // Construct and render with software context
            var graphics = softwareBackend.CreateContext(1280, 320);
            RenderStencilTest(graphics, 10);

            // Write software image to disk
            var screengrab = graphics.GrabPixels();
            screengrab.Write("software.png");
        }
    }
}
