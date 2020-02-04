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

        public Program()
            : base("Streaming Atlas", false)
        {
            // 
            Atlas = new Atlas(1024, 1024);

            // 
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
            gfx.PushState();
            {
                // Attempt to pack images
                for (var i = 0; i < 100; i++)
                {
                    // Unable to pack (batching barrier)
                    Atlas.Register(Sprites[SpriteIndex], out var _);

                    // Rotate through sprites
                    SpriteIndex = (SpriteIndex + 1) % Sprites.Count;
                }

                // Called before glDrawElements/glFlush
                Atlas.CommitChanges(gfx);
            }
            
            // Draw w/ atlas texture
            gfx.PopState();
            gfx.DrawImage(Atlas.Surface, Matrix.CreateScale(Window.FramebufferSize.Width / Atlas.Surface.Width));
        }

        private static List<Image> GenerateImages(int count)
        {
            var images = new List<Image>();

            Parallel.For(0, count, i =>
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
            });

            return images;
        }

        private static void Main(string[] args)
        {
            Start<Program>();
        }
    }
}
