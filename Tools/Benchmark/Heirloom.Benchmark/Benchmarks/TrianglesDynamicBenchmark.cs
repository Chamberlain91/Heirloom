using System.Linq;

using Heirloom.Drawing;
using Heirloom.Mathematics;

namespace Heirloom.Benchmark
{
    public sealed class TrianglesDynamicBenchmark : BenchmarkScene
    {
        private Particle[] _particles;

        public TrianglesDynamicBenchmark()
            : base("Triangles (Dynamic)")
        { }

        protected override void InitializeScene()
        {
            // Randomize point positions
            _particles = new Particle[500];
            for (var i = 0; i < _particles.Length; i++)
            {
                var x = Calc.Random.NextFloat(Bounds.Left, Bounds.Right);
                var y = Calc.Random.NextFloat(Bounds.Top, Bounds.Height);

                _particles[i] = new Particle
                {
                    Velocity = Calc.Random.NextUnitVector() * Calc.Random.NextFloat(10F, 50F),
                    Position = new Vector(x, y)
                };
            }
        }

        protected override void UpdateScene(GraphicsContext gfx, float dt)
        {
            // Move points semi-randomly
            for (var i = 0; i < _particles.Length; i++)
            {
                var particle = _particles[i];

                // Move point
                particle.Position += particle.Velocity * dt;

                // Bounce off bounds
                if (particle.Position.X < Bounds.Left) { particle.Velocity.X *= -1; }
                if (particle.Position.Y < Bounds.Top) { particle.Velocity.Y *= -1; }
                if (particle.Position.X > Bounds.Right) { particle.Velocity.X *= -1; }
                if (particle.Position.Y > Bounds.Bottom) { particle.Velocity.Y *= -1; }
            }

            // Triangulate
            var triangles = GeometryTools.TriangulatePoints(_particles.Select(x => x.Position));

            // Draw triangles
            foreach (var triangle in triangles)
            {
                var u = Calc.Between(triangle.Centroid.X, Bounds.Left, Bounds.Right);
                var v = Calc.Between(triangle.Centroid.Y, Bounds.Top, Bounds.Bottom);
                gfx.Color = new Color(u, v, 0F);
                gfx.DrawTriangle(triangle);
            }

            // Draw triangle outlines
            foreach (var triangle in triangles)
            {
                gfx.Color = Color.Black;
                gfx.DrawTriangleOutline(triangle, 2);
            }

            IsComplete = Time >= 10F;
            if (IsComplete) { SubmitStatisticsBlock(); }
        }

        private sealed class Particle
        {
            public Vector Position;
            public Vector Velocity;
        }
    }
}
