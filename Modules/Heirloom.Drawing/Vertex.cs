using System.Runtime.InteropServices;

using Heirloom.Math;

namespace Heirloom.Drawing
{
    [StructLayout(LayoutKind.Sequential, Pack = 4, Size = 16)]
    public struct Vertex
    {
        /// <summary>
        /// The world-space coordinate.
        /// </summary>
        public Vector Position;

        /// <summary>
        /// The texture-space coordinate.
        /// </summary>
        public Vector UV;

        public Vertex(Vector position, Vector uv)
        {
            Position = position;
            UV = uv;
        }
    }
}
