namespace Heirloom.Benchmark
{
    public sealed class Particle
    {
        public Vector Position;

        public Vector Velocity;

        public Image Image;

        public Particle(Image image)
        {
            Image = image;
        }

        public void Randomize(in Rectangle bounds)
        {
            // Randomize position
            Position.X = Calc.Random.NextFloat(bounds.X, bounds.X + bounds.Width);
            Position.Y = Calc.Random.NextFloat(bounds.Y, bounds.Y + bounds.Height);

            // Randomize motion
            Velocity.X = Calc.Random.NextFloat(-1, +1);
            Velocity.Y = Calc.Random.NextFloat(-1, +1);
            Velocity = Velocity.Normalized * ((Vector) Image.Size).Length;
        }

        public void Update(in Rectangle bounds, in float dt)
        {
            // Compute stage bounds
            var sl = bounds.X;
            var sr = bounds.X + bounds.Width;
            var st = bounds.Y;
            var sb = bounds.Y + bounds.Height;

            // Compute particle bounds
            var pl = Position.X - Image.Origin.X;
            var pt = Position.Y - Image.Origin.Y;
            var pr = pl + Image.Width;
            var pb = pt + Image.Height;

            // Bounce off walls
            if (pl < sl && Velocity.X < 0) { Velocity.X *= -1; }
            if (pr > sr && Velocity.X > 0) { Velocity.X *= -1; }
            if (pt < st && Velocity.Y < 0) { Velocity.Y *= -1; }
            if (pb > sb && Velocity.Y > 0) { Velocity.Y *= -1; }

            // Update motion
            Position += Velocity * dt;
        }
    }
}
