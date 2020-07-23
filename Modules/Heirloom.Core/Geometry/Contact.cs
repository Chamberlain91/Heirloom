using System.Runtime.InteropServices;

namespace Heirloom.Geometry
{
    /// <summary>
    /// Represents a point of contact between two overlapping shapes.
    /// </summary>
    [StructLayout(LayoutKind.Sequential, Pack = 4)]
    public readonly struct Contact
    {
        /// <summary>
        /// The point of contact (of the second shape on first).
        /// </summary>
        public readonly Vector Position;

        /// <summary>
        /// The contact normal.
        /// </summary>
        public readonly Vector Normal;

        /// <summary>
        /// The contact depth (ie, penetration distance)
        /// </summary>
        public readonly float Depth;

        #region Constructors

        internal Contact(Vector position, Vector penetration)
        {
            Position = position;

            Depth = penetration.Length;
            if (Depth > 0) { Normal = penetration / Depth; }
            else { Normal = Vector.Up; }
        }

        #endregion 
    }
}
