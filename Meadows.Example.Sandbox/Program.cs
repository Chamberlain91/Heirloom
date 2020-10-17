using Meadows.Drawing;
using Meadows.Drawing.Software;
using Meadows.Mathematics;

namespace Meadows.Example.Sandbox
{
    internal sealed class Program
    {
        private static void Main(string[] args)
        {
            var gfx = new SoftwareGraphicsContext(1280, 320);
            RenderStencilTest(gfx);
        }

        private static void RenderStencilTest(GraphicsContext gfx)
        {
            // Load image and set its origin to the center...
            var image = new Image("colorful.jpg");
            var imageCenter = (IntVector) (image.Size / 2F);

            // 
            gfx.Clear(Color.Yellow * Color.DarkGray);
            gfx.InterpolationMode = InterpolationMode.Linear;

            // Draw base image (darkened)
            gfx.Color = Color.DarkGray;
            var imageTransform = Matrix.CreateTranslation((Vector) (gfx.Surface.Size - image.Size) / 2) * ComputeCenteredRotation(imageCenter, 10 * Calc.ToRadians);
            gfx.DrawImage(image, imageTransform);

            // Draw a stencil mask
            gfx.BeginStencil();
            gfx.PushState();
            {
                // Set camera pointed at zero and tilted.
                // Then draw text at zero to populate the stencil.
                gfx.SetCamera(Vector.Down * 66F, rotation: 6 * Calc.ToRadians);
                gfx.DrawText("Stencil Text", Vector.Zero, Font.Default, 256, TextAlign.Center | TextAlign.Middle);
            }
            gfx.PopState();
            gfx.EndStencil();

            // Draw image (uses above stencil)
            gfx.Color = Color.White;
            gfx.DrawImage(image, imageTransform);

            // Clear the stencil, back to regular drawing.
            gfx.ClearStencil();

            // Draw regular text again
            gfx.Color = Color.Red;
            gfx.DrawText("Heirloom 2D Graphics", (gfx.Surface.Width - 8, 0), Font.Default, 24, TextAlign.Top | TextAlign.Right);

            // Write rendered image to disk
            var screenshot = gfx.GrabPixels();
            screenshot.Write("example.png");
        }

        private static Matrix ComputeCenteredRotation(Vector center, float rotation)
        {
            return Matrix.CreateTranslation(center)
                 * Matrix.CreateRotation(rotation)
                 * Matrix.CreateTranslation(-center);
        }
    }
}
