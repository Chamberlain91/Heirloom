using System.Collections.Generic;

using Heirloom.Drawing;
using Heirloom.Mathematics;

namespace Heirloom.Benchmark
{
    public sealed class TrianglesStaticBenchmark : BenchmarkScene
    {
        private Vector[] _particles;

        private IReadOnlyList<Triangle> _triangles;

        public TrianglesStaticBenchmark()
            : base("Triangles (Static)")
        { }

        protected override void InitializeScene()
        {
            // Randomize point positions
            _particles = new Vector[500];
            for (var i = 0; i < _particles.Length; i++)
            {
                var x = Calc.Random.NextFloat(Bounds.Left, Bounds.Right);
                var y = Calc.Random.NextFloat(Bounds.Top, Bounds.Height);

                _particles[i] = new Vector(x, y);
            }

            // Triangulate
            _triangles = GeometryTools.TriangulatePoints(_particles);
        }

        protected override void UpdateScene(GraphicsContext gfx, float dt)
        {
            // Draw triangles
            foreach (var triangle in _triangles)
            {
                var u = Calc.Between(triangle.Centroid.X, Bounds.Left, Bounds.Right);
                var v = Calc.Between(triangle.Centroid.Y, Bounds.Top, Bounds.Bottom);
                gfx.Color = new Color(u, v, 0F);
                gfx.DrawTriangle(triangle);
            }

            // Draw triangle outlines
            foreach (var triangle in _triangles)
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
