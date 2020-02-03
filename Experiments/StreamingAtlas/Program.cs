
using Heirloom.Desktop;
using Heirloom.Drawing;
using Heirloom.Drawing.Extras;
using Heirloom.Math;

namespace StreamingAtlas
{
    internal class Program : SimpleApplication
    {
        public readonly BinPacker<Image> Packer;
        public readonly Image Storage;

        public Program()
            : base("Streaming Atlas")
        {
            Storage = new Image(512, 512);
            Storage.Clear(Color.Black);

            Packer = new BinPacker<Image>(Storage.Size);

            // 
            Window.IsResizable = false;
        }

        protected override void OnMouseButtonEvent(MouseButtonEvent e)
        {
            // 
        }

        protected override void OnKeyEvent(KeyEvent e)
        {
            if (e.Action != ButtonAction.Release)
            {
                lock (Packer)
                {
                    if (e.Key == Key.Space)
                    {
                        var w = Calc.Random.Next(8, 256);
                        var h = Calc.Random.Next(8, 256);
                        InsertColoredRect(w, h);
                    }

                    if (e.Key == Key.R)
                    {
                        // Repack (might optimize space)
                        Packer.Repack();

                        // Copy images back in to atlas
                        Storage.Clear(Color.Black);
                        foreach (var image in Packer.Elements)
                        {
                            var rect = Packer.GetRectangle(image);
                            CopyToStorage(image, rect.Min);
                        }
                    }
                }
            }
        }

        private void InsertColoredRect(int w, int h)
        {
            var color = Color.FromHSV(Calc.Random.Next(0, 360), 0.7F, 0.8F);
            var image = Image.CreateCheckerboardPattern(w, h, color);

            if (Packer.Add(image, image.Size, out var rect))
            {
                CopyToStorage(image, rect.Position);
            }
            else
            {
                // throw new InvalidOperationException("Unable to fit");
            }
        }

        protected override void OnFrameUpdate(Graphics gfx, float dt)
        {
            // 
            gfx.Clear(Color.DarkGray);

            // 
            gfx.DrawImage(Storage, Matrix.Identity);
        }

        private void CopyToStorage(Image image, IntVector offset)
        {
            foreach (var (x, y) in Rasterizer.Rectangle(image.Size))
            {
                Storage.SetPixel(offset.X + x, offset.Y + y, image.GetPixel(x, y));
            }
        }

        private static void Main(string[] args)
        {
            Start<Program>();
        }
    }
}
