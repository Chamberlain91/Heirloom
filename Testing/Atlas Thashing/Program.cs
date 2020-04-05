using System.Threading.Tasks;

using Heirloom.Desktop;
using Heirloom.Drawing;
using Heirloom.Drawing.Utilities;
using Heirloom.Math;

namespace Atlas_Thashing
{
    class Program
    {
        static void Main(string[] args)
        {
            Application.Run(() =>
            {
                var window = new Window("Atlas Thrashing", vsync: false) { Size = (1280, 720) };
                window.Graphics.Performance.OverlayMode = PerformanceOverlayMode.Simple;
                window.MoveToCenter();

                var xcount = window.Size.Width / 64;
                var ycount = window.Size.Height / 64;

                var packer = new SkylinePacker<Image>(window.Size);

                Task.Run(() =>
                {
                    while (true)
                    {
                        window.Graphics.ResetState();
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

                        // 
                        window.Graphics.RefreshScreen();
                    }
                });
            });
        }
    }
}
