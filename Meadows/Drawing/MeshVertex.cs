using Meadows.Mathematics;

namespace Meadows.Drawing
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
    }
}
