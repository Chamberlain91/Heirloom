using System;
using System.Runtime.InteropServices;

namespace Heirloom.Geometry
{
    /// <summary>
    /// Represents the result of a ray to shape intersection.
    /// </summary>
    [StructLayout(LayoutKind.Sequential, Pack = 4)]
    public readonly struct RayContact : IEquatable<RayContact>
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
        public readonly float Distance;

        internal RayContact(Vector position, Vector normal, float distance)
        {
            Distance = distance;
            Position = position;
            Normal = normal;
        }

        #region Comparison Operators

        public static bool operator ==(RayContact left, RayContact right)
        {
            return left.Equals(right);
        }

        public static bool operator !=(RayContact left, RayContact right)
        {
            return !(left == right);
        }

        #endregion

        #region Equality

        public override bool Equals(object obj)
        {
            return obj is RayContact contact
                && Equals(contact);
        }

        public bool Equals(RayContact other)
        {
            return Position.Equals(other.Position)
                && Normal.Equals(other.Normal)
                && Distance == other.Distance;
        }

        public override int GetHashCode()
        {
            return HashCode.Combine(Position, Normal, Distance);
        }

        #endregion
    }
}
