using Heirloom.Drawing;
using Heirloom.Math;

namespace Benchmark
{
    public class Particle
    {
        public Image Image { get; }

        public Vector Position;

        public Vector Velocity;

        public Matrix Transform => Matrix.CreateTranslation(Position);

        public Particle(Image image)
        {
            Image = image;
        }

        public void Update(in Rectangle bounds, in float delta, in float scale)
        {
            Position += Velocity * delta;

            // Off right edge
            if (Position.X + Image.Width * scale > bounds.Right && Velocity.X > 0)
            {
                Velocity.X *= -1;
                Position.X = bounds.Right - Image.Width * scale;
            }

            // Off left edge
            if (Position.X < bounds.Left && Velocity.X < 0)
            {
                Velocity.X *= -1;
                Position.X = bounds.Left;
            }

            // Off bottom
            if (Position.Y + Image.Height * scale > bounds.Bottom && Velocity.Y > 0)
            {
                Velocity.Y *= -1;
                Position.Y = bounds.Bottom - Image.Height * scale;
            }

            // Off top
            if (Position.Y < bounds.Top && Velocity.Y < 0)
            {
                Velocity.Y *= -1;
                Position.Y = bounds.Top;
            }
        }
    }
}
