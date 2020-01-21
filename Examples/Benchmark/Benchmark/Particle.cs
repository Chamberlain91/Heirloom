using Heirloom.Drawing;
using Heirloom.Math;

namespace Benchmark
{
    public class Particle
    {
        public Image Image { get; }

        public Vector Position;

        public Vector Velocity;

        public float Rotation;

        public Matrix Transform => Matrix.CreateTransform(Position, Rotation, Vector.One);

        public float Time;

        public Particle(Image image)
        {
            Image = image;
        }

        public void Update(in Rectangle bounds, in float delta, in float scale)
        {
            Position += Velocity * delta;
            Time += delta;

            var w1 = Image.Width * scale * 0.8F;
            var w2 = Image.Width * scale * 0.2F;

            var h1 = Image.Height * scale * 0.8F;
            var h2 = Image.Height * scale * 0.2F;

            // Get position w/respect to origin
            var x = Position.X - Image.Origin.X;
            var y = Position.Y - Image.Origin.Y;

            // Off right edge
            if (x + w1 > bounds.Right && Velocity.X > 0)
            {
                Velocity.X *= -1;
                x = bounds.Right - w1;
            }

            // Off left edge
            if (x + w2 < bounds.Left && Velocity.X < 0)
            {
                Velocity.X *= -1;
                x = bounds.Left - w2;
            }

            // Off bottom
            if (y + h1 > bounds.Bottom && Velocity.Y > 0)
            {
                Velocity.Y *= -1;
                y = bounds.Bottom - h1;
            }

            // Off top
            if (y + h2 < bounds.Top && Velocity.Y < 0)
            {
                Velocity.Y *= -1;
                y = bounds.Top - h2;
            }
        }
    }
}
