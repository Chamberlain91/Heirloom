using System.Runtime.InteropServices;

namespace Heirloom
{
    /// <summary>
    /// Represents a vertex of <see cref="Mesh"/>.
    /// </summary>
    /// <category>Drawing</category>
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

        /// <summary>
        /// Creates a new vertex.
        /// </summary>
        /// <param name="position">The position of the vertex.</param>
        /// <param name="uv">The UV of the vertex.</param>
        public Vertex(Vector position, Vector uv)
        {
            Position = position;
            UV = uv;
        }
    }
}
