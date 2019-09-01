namespace Heirloom.Collections.Spatial
{
    public enum GridNeighbors
    {
        /// <summary>
        /// The four neighbors to north, east, south, west.
        /// </summary>
        FourAxis,

        /// <summary>
        /// The four neighbors to north-east, south-east, south-west, north-west.
        /// </summary>
        FourDiagonal,

        /// <summary>
        /// All eight neiboring tiles (combines <see cref="FourAxis"/> and <see cref="FourDiagonal"/>).
        /// </summary>
        Eight
    }
}
