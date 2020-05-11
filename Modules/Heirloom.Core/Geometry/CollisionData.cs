using System;

namespace Heirloom.Geometry
{
    /// <summary>
    /// Contains the results of a collision function in <see cref="Collision"/>.
    /// </summary>
    public struct CollisionData
    {
        /// <summary>
        /// Gets the measure of how much overlap the shapes had.
        /// </summary>
        public float Penetration { get; internal set; }

        /// <summary>
        /// Gets the normal vector of the collision.
        /// </summary>
        public Vector Normal { get; internal set; }

        private Vector _contact0;
        private Vector _contact1;

        /// <summary>
        /// Gets the number of contact points.
        /// </summary>
        public int Count { get; internal set; }

        /// <summary>
        /// Gets a contact point.
        /// </summary>
        public Vector GetPoint(int i)
        {
            if (Count >= 1 && i == 0) { return _contact0; }
            if (Count >= 2 && i == 1) { return _contact1; }
            throw new ArgumentOutOfRangeException(nameof(i));
        }

        internal void AddContact(Vector contact)
        {
            if (Count == 2) { throw new InvalidOperationException("Unable to add contact, already at max (2)."); }
            else
            {
                if (Count == 0) { _contact0 = contact; }
                if (Count == 1) { _contact1 = contact; }
                Count++;
            }
        }
    }
}
