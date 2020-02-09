using System.Collections.Generic;
using System.Linq;
using Heirloom.Drawing;
using Heirloom.Math;

namespace Heirloom.Benchmark
{
    public sealed class StaticTriangulation : Benchmark
    {
        private const int MaxPointCount = 1000;
        private const int Padding = 32;

        private const float EvaluationDuration = 20F;

        private readonly Vector[] _positions = new Vector[MaxPointCount];

        private IReadOnlyList<Triangle> _triangles;

        public StaticTriangulation()
            : base("Triangulation (Static)")
        {
            Units = "frames";
        }

        public override void Initialize(in Rectangle bounds)
        {
            // Randomize point positions
            for (var i = 0; i < MaxPointCount; i++)
            {
                ref var point = ref _positions[i];
                point.X = Calc.Random.NextFloat(Padding, bounds.Width - Padding);
                point.Y = Calc.Random.NextFloat(Padding, bounds.Height - Padding);

            }

            // Triangulate
            _triangles = Delaunay.Triangulate(_positions);
        }

        protected override void Update(Graphics gfx, in Rectangle bounds, float delta)
        {
            // Draw triangles
            foreach (var triangle in _triangles)
            {
                var u = Calc.Between(triangle.Centroid.X, bounds.Left, bounds.Right);
                var v = Calc.Between(triangle.Centroid.Y, bounds.Top, bounds.Bottom);
                gfx.Color = new Color(u, v, 0F);
                gfx.DrawTriangle(triangle);
            }

            //
            foreach (var triangle in _triangles)
            {
                gfx.Color = Color.Black;
                gfx.DrawTriangleOutline(triangle, 2);
            }

            // 
            Progress = Time / EvaluationDuration;
            Score++;

            // 
            if (Time > EvaluationDuration)
            {
                Score /= EvaluationDuration;
                IsComplete = true;
            }
        }
    }
}
