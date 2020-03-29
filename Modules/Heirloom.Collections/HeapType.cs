namespace Heirloom.Collections
{
    /// <summary>
    /// Describes the behaviour of a <see cref="Heap{T}"/>.
    /// </summary>
    public enum HeapType
    {
        /// <summary>
        /// A heap where the priority item is the comparably largest.
        /// </summary>
        Max,

        /// <summary>
        /// A heap where the priority item is the comparably smallest.
        /// </summary>
        Min
    }
}
