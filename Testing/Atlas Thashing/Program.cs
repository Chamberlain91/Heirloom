using Heirloom.Platforms.Desktop;
using Heirloom.Drawing;
using Heirloom.Drawing.Utilities;
using Heirloom.Math;

namespace Atlas_Thashing
{
    internal class Program
    {
        private static void Main(string[] args)
        {
            Application.Run(() =>
            {
                var window = new Window("Atlas Thrashing", vsync: false) { Size = (1280, 720) };
                window.Graphics.Performance.OverlayMode = PerformanceOverlayMode.Simple;
                window.MoveToCenter();

                var xcount = window.Size.Width / 64;
                var ycount = window.Size.Height / 64;

                // Using the shelf packer here for fast visualization sake.
                // The skyline is the actual packer used by the texture atlas system.
                var packer = new ShelfPacker<Image>(window.Size);

                // Create the main loop
                var loop = RenderLoop.Create(window.Graphics, (gfx, dt) =>
                {
                    packer.Clear();

                    window.Graphics.Clear(Color.White);

                    while (true)
                    {
                        var w = Calc.Random.Next(8, 128);
                        var h = Calc.Random.Next(8, 128);

                        var r = Calc.Random.NextFloat();
                        var g = Calc.Random.NextFloat();
                        var b = Calc.Random.NextFloat();

                        // Attempt to pack image, if unable break
                        var image = Image.CreateColor(w, h, new Color(r, g, b));
                        if (!packer.Add(image, image.Size)) { break; }

                        var packed = packer.GetRectangle(image);
                        window.Graphics.DrawImage(image, packed.Position);
                    }
                });

                // Begin main loop
                loop.Start();
            });
        }
    }
}
