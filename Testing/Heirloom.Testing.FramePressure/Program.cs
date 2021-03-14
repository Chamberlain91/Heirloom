using System;
using System.Diagnostics;

using Heirloom.Desktop;
using Heirloom.Drawing;
using Heirloom.Mathematics;

namespace Heirloom.Testing.FramePressure
{
    public static class Program
    {
        static void Main(string[] args)
        {
            const int MinSize = 16;
            const int MaxSize = 96;

            Application.Run(() =>
            {
                var window = new Window("Heirloom - First Frame Pressure", vsync: false);
                window.Maximize();

                // Begin monitoring elapsed time
                var watch = Stopwatch.StartNew();

                // Create a bunch of particles (ie, scattered images)
                var particles = new Particle[10000];
                for (var i = 0; i < particles.Length; i++)
                {
                    var image = CreateRandomImage(MinSize, MaxSize);
                    var bounds = new Rectangle(8, 8, window.Surface.Width - image.Width - 16, window.Surface.Height - image.Height - 16);
                    particles[i] = new Particle(image, Calc.Random.NextVector(bounds));
                }

                Log.Info($"Generated {particles.Length} Images: {Time.GetEnglishTime((float) watch.Elapsed.TotalSeconds)}");
                Log.Info($" - Image sizes range from {MinSize}x{MinSize} to {MaxSize}x{MaxSize}");
                Log.Info($" - The images are draw on the 10th frame to show the pressure caused by the atlas system");

                for (var i = 1; i <= 80; i++)
                {
                    watch.Restart();
                    Update(window.Graphics, particles, i >= 10);
                    Log.Info($"Frame {i,-2} -> { (float) watch.Elapsed.TotalSeconds * 1000F,6:N1}");
                }
            });

            static Image CreateRandomImage(int min, int max)
            {
                var imageW = Calc.Random.Next(min, max);
                var imageH = Calc.Random.Next(min, max);
                var image = Image.CreateColor(imageW, imageH, Calc.Random.NextColorHue());
                return image;
            }
        }

        private static void Update(GraphicsContext gfx, Particle[] particles, bool draw)
        {
            gfx.Clear(Color.DarkGray);
            gfx.ResetState();

            if (draw)
            {
                foreach (var particle in particles)
                {
                    gfx.DrawImage(particle.Image, Matrix.CreateTranslation(particle.Position));
                }
            }

            // gfx.Commit();
            gfx.Screen.Refresh();
        }

        private class Particle
        {
            public Vector Position;

            public Image Image;

            public Particle(Image image, Vector position)
            {
                Image = image ?? throw new ArgumentNullException(nameof(image));
                Position = position;
            }
        }
    }
}
