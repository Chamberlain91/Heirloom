using System;

using Heirloom.Drawing;
using Heirloom.Math;
using Heirloom.Platforms.Desktop;

namespace Heirloom.Examples.Offscreen
{
    internal class Example : GameWindow
    {
        private readonly Image _background;
        private readonly Image _lightSprite;
        private readonly Image _boxSprite;

        private readonly BounceSprite[] _sprites;

        private readonly Surface _worldSurface;
        private readonly Surface _lightSurface;

        public Example()
            : base(512, 512, "Offscreen Surface Rendering")
        {
            // 
            _worldSurface = RenderContext.CreateSurface(256, 256);
            _lightSurface = RenderContext.CreateSurface(256, 256);

            // 
            _boxSprite = Image.CreateCheckerboardPattern(16, 16, Pixel.White, 16);
            _lightSprite = Image.CreateRadialGradient((64, 64), Color.White, Color.Black);
            _background = Image.CreateCheckerboardPattern(256, 256, Pixel.DarkGray);

            // 
            _sprites = new BounceSprite[4 * Color.Rainbow.Length];
            for (var i = 0; i < _sprites.Length; i++)
            {
                var color = Color.Rainbow[i % Color.Rainbow.Length];
                _sprites[i] = new BounceSprite(_boxSprite, color);
            }

            // Make window constant size
            IsResizeable = false;
        }

        protected override void Update()
        {
            // Update each bouncing sprite
            foreach (var sprite in _sprites)
            {
                UpdateSprite(sprite);
            }
        }

        protected override void Render(RenderContext context)
        {
            // Render to offscreen
            context.Surface = _worldSurface;
            context.Clear(Color.Magenta);

            context.Draw(_background, Matrix.Identity);
            foreach (var sprite in _sprites)
            {
                sprite.Render(context);
            }

            // Render light buffer
            context.Surface = _lightSurface;
            context.Clear(Color.Black);

            context.BlendMode = BlendMode.Additive;
            foreach (var sprite in _sprites)
            {
                var pos = sprite.Position - (24, 24);
                context.Draw(_lightSprite, Matrix.CreateTranslation(pos), sprite.Color);
            }

            // Blend light buffer onto world
            context.Surface = _worldSurface;
            context.BlendMode = BlendMode.Multiply;
            context.Draw(_lightSurface, Matrix.Identity);

            // Draw offscreen on window
            context.Surface = context.DefaultSurface;
            context.BlendMode = BlendMode.Alpha;
            context.Clear(Color.Magenta);

            context.Draw(_worldSurface, (0, 0, context.Surface.Width, context.Surface.Height), Color.White);
        }

        private void UpdateSprite(BounceSprite sprite)
        {
            // Adjust position
            sprite.Position += sprite.Velocity * Delta;

            // Bounce of edges
            if (sprite.Position.X < 0)
            {
                sprite.Position = (0, sprite.Position.Y);
                sprite.Velocity *= (-1, 1);
            }

            if (sprite.Position.Y < 0)
            {
                sprite.Position = (sprite.Position.X, 0);
                sprite.Velocity *= (1, -1);
            }

            if ((sprite.Position.X + sprite.Image.Width) >= _worldSurface.Width)
            {
                sprite.Position = (_worldSurface.Width - sprite.Image.Width, sprite.Position.Y);
                sprite.Velocity *= (-1, 1);
            }

            if (sprite.Position.Y + sprite.Image.Height >= _worldSurface.Height)
            {
                sprite.Position = (sprite.Position.X, _worldSurface.Height - sprite.Image.Height);
                sprite.Velocity *= (1, -1);
            }
        }

        private class BounceSprite
        {
            public BounceSprite(Image image, Color color)
            {
                Image = image ?? throw new ArgumentNullException(nameof(image));
                Color = color;

                // Set initial random position
                var x = Calc.Random.Next(0, 256);
                var y = Calc.Random.Next(0, 256);
                Position = new Vector(x, y);

                // Set initial random angle
                var angle = Calc.Random.NextFloat(0, Calc.TwoPi);
                Velocity = Vector.FromAngle(angle) * 50;
            }

            public Image Image { get; }

            public Color Color { get; }

            public Vector Position { get; set; }

            public Vector Velocity { get; set; }

            public void Render(RenderContext context)
            {
                var transform = Matrix.CreateTranslation(Position);
                context.Draw(Image, transform, Color);
            }
        }

        private static void Main(string[] args)
        {
            Run(new Example());
        }
    }
}
