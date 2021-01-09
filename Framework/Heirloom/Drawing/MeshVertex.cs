using Heirloom.Mathematics;

namespace Heirloom.Drawing
{
    public struct MeshVertex
    {
        public Vector Position;

        public Vector UV;

        public Color Color;

        public MeshVertex(Vector position, Vector uv)
            : this(position, uv, Color.White)
        { }

        public MeshVertex(Vector position, Vector uv, Color color)
        {
            Position = position;
            UV = uv;
            Color = color;
        }

        /// <summary>
        /// Computes an interpolation of two mesh vertices.
        /// </summary>
        public static MeshVertex Lerp(MeshVertex a, MeshVertex b, float t)
        {
            return new MeshVertex
            {
                Position = Vector.Lerp(a.Position, b.Position, t),
                Color = Color.Lerp(a.Color, b.Color, t),
                UV = Vector.Lerp(a.UV, b.UV, t)
            };
        }
    }
}
