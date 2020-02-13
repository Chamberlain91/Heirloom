using System;
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

        public Atlas Atlas;

        readonly private LinkedList<DrawCommand> _commands = new LinkedList<DrawCommand>();

        public Program()
            : base("Streaming Atlas", false)
        {
            // 
            Atlas = new Atlas(1024, 1024);

            // 
            Sprites = GenerateImages(1000);

            // 
            Window.Graphics.EnableStatisticsOverlay = true;
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
            // Attempt to pack images
            for (var i = 0; i < 200; i++)
            {
                // Simulate drawing something
                var sprite = Sprites[(SpriteIndex + i) % Sprites.Count];
                SubmitDraw(sprite, Vector.Zero);
            }

            // Rotate through sprites (advance 33 images are ~133 temporally consistent)
            SpriteIndex = (SpriteIndex + 33) % Sprites.Count;

            // 
            gfx.Clear(Color.DarkGray);
            gfx.PushState();
            {
                // Process draw commands
                while (_commands.Count > 0)
                {
                    var command = _commands.First.Value;

                    // Register image with atlas
                    if (Atlas.Register(command.Image))
                    {
                        // Command accepted remove from list
                        _commands.RemoveFirst();

                        // Submit to batch
                    }
                    else
                    {
                        // Render into atlas
                        Atlas.CommitChanges(gfx);

                        // Draw batched geometry

                        // 
                        var scale = gfx.Surface.Width / (float) Atlas.Surface.Width;
                        gfx.DrawImage(Atlas.Surface, Matrix.CreateScale(scale));
                    }
                }
            }
            gfx.PopState();
            gfx.DrawImage(Atlas.Surface, Matrix.CreateScale(Window.FramebufferSize.Width / (float) Atlas.Surface.Width));
        }

        private void SubmitDraw(Image image, Vector position)
        {
            _commands.AddLast(new DrawCommand
            {
                Image = image,
                Position = position
            });
        }

        struct DrawCommand
        {
            public Image Image;
            public Vector Position;
        }

        private static List<Image> GenerateImages(int count)
        {
            var images = new List<Image>();

            Parallel.For(0, count, i =>
            {
                // Generate randomized boxes that are "generally square"
                var scale = Calc.Random.Next(1, 10);
                var width = Calc.Random.Next(16, 32) * scale;
                var height = Calc.Random.Next(16, 32) * scale;

                // Generate a random color
                var color = Color.FromHSV(Calc.Random.Next(0, 360), 0.7F, 0.5F);

                // Create image
                var image = Image.CreateCheckerboardPattern(width, height, color);
                images.Add(image);
            });

            return images;
        }

        private static void Main(string[] args)
        {
            Start<Program>();
        }
    }
}
