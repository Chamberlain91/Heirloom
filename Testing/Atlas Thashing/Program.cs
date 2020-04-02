using System.Threading.Tasks;

using Heirloom.Desktop;
using Heirloom.Drawing;
using Heirloom.Drawing.OpenGLES.Utilities;
using Heirloom.Math;

namespace Atlas_Thashing
{
    class Program
    {
        static void Main(string[] args)
        {
            Application.Run(() =>
            {
                var window = new Window("Atlas Thrashing", vsync: false) { Size = (640, 480), IsResizable = false };
                window.Graphics.Performance.OverlayMode = PerformanceOverlayMode.Full;

                var xcount = window.FramebufferSize.Width / 64;
                var ycount = window.FramebufferSize.Height / 64;

                var packer = new ShelfPacker<Image>(window.Size);

                Task.Run(() =>
                {
                    while (true)
                    {
                        window.Graphics.Clear(Color.White);
                        window.Graphics.ResetState();
                        packer.Clear();

                        while (true)
                        {
                            var w = Calc.Random.Next(16, 64);
                            var h = Calc.Random.Next(16, 64);

                            var r = Calc.Random.NextFloat();
                            var g = Calc.Random.NextFloat();
                            var b = Calc.Random.NextFloat();

                            // Attempt to pack image, if unable break
                            var image = Image.CreateColor(w, h, new Color(r, g, b));
                            if (!packer.Add(image, image.Size)) { break; }

                            // Acquire packed image and draw to screen
                            var packed = packer.GetRectangle(image);
                            window.Graphics.DrawImage(image, packed.Position);
                        }

                        window.Graphics.RefreshScreen();
                    }
                });
            });
        }
    }
}
