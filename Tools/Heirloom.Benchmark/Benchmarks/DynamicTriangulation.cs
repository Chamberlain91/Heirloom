using Heirloom.Extras;
using Heirloom.Geometry;

namespace Heirloom.Benchmark
{
    public sealed class DynamicTriangulation : Benchmark
    {
        private const int MaxPointCount = 500;
        private const int Padding = 32;

        private const float EvaluationDuration = 10F;

        private readonly Vector[] _positions = new Vector[MaxPointCount];
        private readonly Vector[] _velocities = new Vector[MaxPointCount];

        public DynamicTriangulation()
            : base("Triangulation (Dynamic)")
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

                ref var vel = ref _velocities[i];
                vel.X = Calc.Random.NextFloat(-1, +1);
                vel.Y = Calc.Random.NextFloat(-1, +1);
                vel.Normalize();

                vel *= Calc.Random.NextFloat(10F, 50F);
            }
        }

        protected override void Update(GraphicsContext gfx, in Rectangle bounds, float delta)
        {
            // Move points semi-randomly
            for (var i = 0; i < _positions.Length; i++)
            {
                ref var point = ref _positions[i];
                ref var vel = ref _velocities[i];

                // Move point
                point += vel * delta;

                // Bounce off bounds
                if (point.X < bounds.Left + Padding) { vel.X *= -1; }
                if (point.Y < bounds.Top + Padding) { vel.Y *= -1; }
                if (point.X > bounds.Right - Padding) { vel.X *= -1; }
                if (point.Y > bounds.Bottom - Padding) { vel.Y *= -1; }
            }

            // Triangulate
            var triangles = GeometryTools.Triangulate(_positions);

            // Draw triangles
            foreach (var triangle in triangles)
            {
                var u = Calc.Between(triangle.Centroid.X, bounds.Left, bounds.Right);
                var v = Calc.Between(triangle.Centroid.Y, bounds.Top, bounds.Bottom);
                gfx.Color = new Color(u, v, 0F);
                gfx.DrawTriangle(triangle);
            }

            //
            foreach (var triangle in triangles)
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
