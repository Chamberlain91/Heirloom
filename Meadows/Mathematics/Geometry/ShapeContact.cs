using System.Runtime.InteropServices;

namespace Meadows.Mathematics
{
    /// <summary>
    /// Represents a point of contact between two overlapping shapes.
    /// </summary>
    [StructLayout(LayoutKind.Sequential, Pack = 4)]
    public readonly struct ShapeContact
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

        internal ShapeContact(Vector position, Vector penetration)
        {
            Position = position;

            Depth = penetration.Length;
            if (Depth > 0) { Normal = penetration / Depth; }
            else { Normal = Vector.Up; }
        }

        #endregion 
    }
}
