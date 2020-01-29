namespace Heirloom.Collections.Spatial
{
    /// <summary>
    /// Describes the choice of neighbors in a grid.
    /// </summary>
    public enum GridNeighborType
    {
        /// <summary>
        /// The four neighbors to north, east, south, west.
        /// </summary>
        Axis,

        /// <summary>
        /// The four neighbors to north-east, south-east, south-west, north-west.
        /// </summary>
        Diagonal,

        /// <summary>
        /// All eight neiboring tiles (combines <see cref="Axis"/> and <see cref="Diagonal"/>).
        /// </summary>
        All
    }
}
