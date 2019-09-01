using System.Runtime.InteropServices;

using Heirloom.Math;

namespace Heirloom.Drawing
{
    [StructLayout(LayoutKind.Sequential, Pack = 4, Size = 16)]
    public struct Vertex
    {
        public Vector Position;

        public Vector UV;

        public Vertex(Vector position, Vector uv)
        {
            Position = position;
            UV = uv;
        }
    }
}
