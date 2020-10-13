using Meadows.Drawing;
using Meadows.Mathematics;

namespace Meadows.Example.Sandbox
{
    internal sealed class Program
    {
        private static void Main(string[] args)
        {
            var gfx = new SoftwareGraphcsContext(512, 512);

            gfx.Clear(Color.DarkGray);
            gfx.DrawText("Hello World", (Vector) gfx.Surface.Size / 2F, Font.Default, 16, TextAlign.Center | TextAlign.Middle);

            // Write rendered image to disk
            var image = gfx.GrabPixels();
            image.Write("example.png");
        }
    }
}
