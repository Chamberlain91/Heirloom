using System.Collections.Generic;
using System.Threading.Tasks;
using Heirloom.Desktop;
using Heirloom.Drawing;
using Heirloom.Math;

namespace StreamingAtlas
{
    internal class Program : SimpleApplication
    {
        public IReadOnlyList<Image> Sprites { get; }
        public int SpriteIndex;

        public IRectanglePacker<Image> Packer { get; }

        public Image[] AtlasImages;
        public int AtlasIndex;

        public Program()
            : base("Streaming Atlas", false)
        {
            Packer = new ShelfPacker<Image>(1024, 1024);
            AtlasImages = new Image[3]
            {
                new Image(1024, 1024),
                new Image(1024, 1024),
                new Image(1024, 1024)
            };

            Sprites = GenerateImages(1000);

            // 
            Window.Graphics.EnableFPSOverlay = true;
            Window.IsResizable = false;
            Window.Size = (768, 768);
        }

        protected override void OnMouseButtonEvent(MouseButtonEvent e)
        {
            // 
        }

        protected override void OnKeyEvent(KeyEvent e)
        {
            // 
        }

        protected override void OnFrameUpdate(Graphics gfx, float dt)
        {
            // 
            gfx.Clear(Color.DarkGray);

            // Console.WriteLine("---------");

            var x = 0;
            var y = 0;

            const float CellSize = 768 / 3F;

            // Attempt to pack images
            for (var i = 0; i < 100; i++)
            {
                // Unable to pack (batching barrier)
                if (!Register(Sprites[SpriteIndex]))
                {
                    var atlas = AtlasImages[AtlasIndex];

                    // Write image data
                    Parallel.ForEach(Packer.Elements, image =>
                    {
                        var rectangle = Packer.GetRectangle(image);
                        image.CopyTo(atlas, rectangle.Position);
                    });

                    // Draw texture
                    gfx.DrawImage(atlas, Matrix.CreateTransform((x * CellSize, y * CellSize), 0, CellSize / atlas.Width));
                    gfx.Flush(); // BUG: Changing the image pixels should cause this when DrawImage is called

                    // 
                    AtlasIndex = (AtlasIndex + 1) % AtlasImages.Length;

                    // Clear packing (couldn't hold more anyway)
                    Packer.Clear();

                    // Insert again
                    Register(Sprites[SpriteIndex]);

                    x++;
                    if (x >= (Window.FramebufferSize.Width / CellSize))
                    {
                        x = 0;
                        y++;
                    }
                }

                SpriteIndex++;
                if (SpriteIndex >= Sprites.Count) { SpriteIndex = 0; }
            }
        }

        public bool Register(Image image)
        {
            if (Packer.Contains(image)) { return true; } // contained
            // Try to insert
            else if (Packer.Add(image, image.Size))
            {
                // inserted
                return true;
            }
            else
            {
                // failure
                return false;
            }
        }

        private static List<Image> GenerateImages(int count)
        {
            var images = new List<Image>();

            for (var i = 0; i < count; i++)
            {
                // Generate randomized boxes that are "generally square"
                var scale = Calc.Random.Next(1, 12);
                var width = Calc.Random.Next(16, 32) * scale;
                var height = Calc.Random.Next(16, 32) * scale;

                // Generate a random color
                var color = Color.FromHSV(Calc.Random.Next(0, 360), 0.7F, 0.5F);

                // Create image
                var image = Image.CreateCheckerboardPattern(width, height, color);
                images.Add(image);
            }

            return images;
        }

        private static void Main(string[] args)
        {
            Start<Program>();
        }
    }
}
