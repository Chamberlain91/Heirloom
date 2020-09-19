namespace Meadows
{
    /// <summary>
    /// An enumeration of rectangle packing algorithms.
    /// </summary>
    /// <category>Utility</category>
    public enum PackingAlgorithm
    {
        /// <summary>
        /// Highest packing quality, very slow.
        /// </summary>
        Maxrects,

        /// <summary>
        /// Medium packing quality, adequate speed.
        /// </summary>
        Skyline,

        /// <summary>
        /// Worst packing quality, blistering fast.
        /// </summary>
        Shelf
    }
}
