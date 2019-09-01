using Heirloom.Drawing;
using Heirloom.Math;

namespace Heirloom.Runtime
{
    public class Camera
    {
        public Color Color { get; set; }

        public Vector Position { get; set; }

        public float Scale { get; set; } = 1F;

        public float Angle { get; set; }

        internal Matrix ComputeMatrix(RenderContext ctx)
        {
            return Matrix.CreateTranslation((Vector) ctx.Surface.Size / 2F) *
                   Matrix.Inverse(Matrix.CreateTransform(Position, Angle, (Scale, Scale)));
        }
    }
}
