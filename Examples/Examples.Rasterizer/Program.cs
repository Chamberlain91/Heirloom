using System.Collections.Generic;
using System.Threading.Tasks;
using Heirloom.Desktop;
using Heirloom.Drawing;
using Heirloom.Math;
using Heirloom.Math.Extras;

namespace Examples.CpuRasterizer
{
    class Program
    {
        public Window Window;

        public Image Image;

        public Program()
        {
            // Create window
            Window = new Window("CPU Rasterizer")
            {
                Size = (512, 512),
                IsResizable = false
            };

            // 
            Image = new Image(Window.FramebufferSize / 2);
            Image.Clear(Color.DarkGray);

            // Rasterize into image (CPU rendering)
            FillPixels(Rasterizer.Rectangle(Image.Width / 4, 0, Image.Height / 2, Image.Height), Color.Gray);
            FillPixels(Rasterizer.Circle(Image.Width / 2, Image.Height * 2 / 3, 96), Color.Pink);
            FillPixels(Rasterizer.Circle(Image.Width / 2, Image.Height / 2, 64), Color.Cyan);
            FillPixels(Rasterizer.Circle(Image.Width / 2, Image.Height / 3, 32), Color.White);
        }

        private void FillPixels(IEnumerable<IntVector> positions, Color color)
        {
            foreach (var co in positions)
            {
                if (co.X >= 0 && co.Y >= 0 && co.X < Image.Width && co.Y < Image.Height)
                {
                    Image.SetPixel(co, color);
                }
            }
        }

        private void Update()
        {
            // Draw image to screen
            Window.Graphics.ResetState();
            Window.Graphics.DrawImage(Image, new Rectangle(Vector.Zero, Window.Size));
            Window.Graphics.RefreshScreen();
        }

        static void Main(string[] args)
        {
            Application.Run(() =>
            {
                var program = new Program();
                program.Update(); //
            });
        }
    }
}
