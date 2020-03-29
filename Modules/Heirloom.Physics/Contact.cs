using System.Runtime.InteropServices;

using Heirloom.Math;

namespace Heirloom.Physics
{
    /// <summary>
    /// Represents a point of contact as a result of collision detection or raycasting.
    /// </summary>
    [StructLayout(LayoutKind.Sequential, Pack = 4)]
    public readonly struct Contact
    {
        /// <summary>
        /// The point of contact from a collision or ray.
        /// </summary>
        public readonly Vector Position;

        /// <summary>
        /// The normal direction of the contacted surface.
        /// </summary>
        public readonly Vector Normal;

        /// <summary>
        /// The separating distance from the point of contact.
        /// </summary>
        public readonly float Depth;

        public Contact(Vector position, Vector normal, float distance)
        {
            Depth = distance;
            Position = position;
            Normal = normal;
        }
    }
}
