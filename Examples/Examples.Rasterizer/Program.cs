using System.Collections.Generic;
using System.Threading.Tasks;

using Heirloom;
using Heirloom.Desktop;

namespace Examples.CpuRasterizer
{
    internal class Program : GameLoop
    {
        public readonly Window Window;

        public readonly Image Image;

        public Program(Window window)
            : base(window.Graphics)
        {
            Window = window;

            // 
            Image = new Image(Window.Surface.Size);
            Image.Clear(Color.DarkGray);

            // Rasterize into image (CPU rendering)
            FillPixels(Rasterizer.Rectangle(Image.Width / 4, 0, Image.Height / 2, Image.Height), Color.Gray);
            FillPixels(Rasterizer.Circle(Image.Width / 2, Image.Height * 2 / 3, 96), Color.Pink);
            FillPixels(Rasterizer.Circle(Image.Width / 2, Image.Height / 2, 64), Color.Cyan);
            FillPixels(Rasterizer.Circle(Image.Width / 2, Image.Height / 3, 48), Color.White);
        }

        private void FillPixels(IEnumerable<IntVector> positions, Color color)
        {
            Parallel.ForEach(positions, co =>
            {
                if (co.X >= 0 && co.Y >= 0 && co.X < Image.Width && co.Y < Image.Height)
                {
                    Image.SetPixel(co, color);
                }
            });
        }

        protected override void Update(Graphics gfx, float dt)
        {
            Window.Graphics.DrawImage(Image, new Rectangle(Vector.Zero, Window.Size));
        }

        private static void Main(string[] args)
        {
            Application.Run(() =>
            {
                // Create window
                var window = new Window("CPU Rasterizer")
                {
                    Size = (512, 512),
                    IsResizable = false
                };

                // 
                var program = new Program(window);
                program.Start();
            });
        }
    }
}
