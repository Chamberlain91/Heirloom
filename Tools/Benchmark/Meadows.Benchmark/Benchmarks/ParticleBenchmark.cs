using System.Linq;

using Meadows.Drawing;
using Meadows.IO;
using Meadows.Mathematics;

namespace Meadows.Benchmark
{
    public abstract class ParticleBenchmark : BenchmarkScene
    {
        private readonly Image[] _images;
        private Particle[] _particles;

        private readonly int[] _densities = new int[] { 100, 1000, 10000, 50000 };
        private int _densityIndex;

        public ParticleBenchmark(string name, Image[] images)
            : base(name)
        {
            _images = images;
        }

        protected static Image[] LoadImages(string pattern)
        {
            var files = Files.EnumerateFiles(pattern);
            return files.Select(path => new Image(path)).ToArray();
        }

        protected override void InitializeScene()
        {
            // Allocates and randomize the particles
            _particles = new Particle[ushort.MaxValue]; // 65K
            for (var i = 0; i < _particles.Length; i++)
            {
                var image = _images[i % _images.Length];

                var x = Calc.Random.NextFloat(Bounds.Left, Bounds.Right - image.Width);
                var y = Calc.Random.NextFloat(Bounds.Top, Bounds.Bottom - image.Height);

                _particles[i] = new Particle
                {
                    Velocity = Calc.Random.NextUnitVector() * ((Vector) image.Size).Length,
                    Position = new Vector(x, y),
                    Image = image
                };
            }

            // 
            _densityIndex = 0;
        }

        protected override void UpdateScene(GraphicsContext gfx, float dt)
        {
            var mtx = Matrix.Identity;

            // Advance to the next density index
            if (Time >= 10)
            {
                _densityIndex++;
                Time -= 10F;
            }

            Score++; // Count Frames
            if (_densityIndex >= _densities.Length)
            {
                IsComplete = true;
                return;
            }

            // 
            var particleCount = _densities[_densityIndex];
            for (var i = 0; i < particleCount; i++)
            {
                var particle = _particles[i];

                // Move
                particle.Position += particle.Velocity * dt;

                // Bounce
                if (particle.Position.X < Bounds.Left)
                {
                    particle.Position.X = Bounds.Left;
                    particle.Velocity.X = -particle.Velocity.X;
                }

                if (particle.Position.X >= Bounds.Right - particle.Image.Width)
                {
                    particle.Position.X = Bounds.Right - particle.Image.Width;
                    particle.Velocity.X = -particle.Velocity.X;
                }

                if (particle.Position.Y < Bounds.Top)
                {
                    particle.Position.Y = Bounds.Top;
                    particle.Velocity.Y = -particle.Velocity.Y;
                }

                if (particle.Position.Y >= Bounds.Bottom - particle.Image.Height)
                {
                    particle.Position.Y = Bounds.Bottom - particle.Image.Height;
                    particle.Velocity.Y = -particle.Velocity.Y;
                }

                // Set translation
                mtx.M2 = particle.Position.X;
                mtx.M5 = particle.Position.Y;

                // Draw image
                gfx.Color = Color.White;
                gfx.DrawImage(particle.Image, mtx);
            }

            // 
            var text = $"Particles: {particleCount}";
            var center = (Vector) gfx.Surface.Size / 2F;

            var rect = TextLayout.Measure(text, Font.SansSerifBold, 32);
            rect.Position = center - ((Vector) rect.Size / 2F);
            rect = Rectangle.Inflate(rect, 10);

            gfx.Color = Color.White;
            gfx.DrawRect(rect);

            gfx.Color = Color.Black;
            gfx.DrawRectOutline(rect);
            gfx.DrawText(text, center, Font.SansSerifBold, 32, TextAlign.Center | TextAlign.Middle);
        }

        private sealed class Particle
        {
            public Image Image;

            public Vector Position;

            public Vector Velocity;
        }
    }
}
