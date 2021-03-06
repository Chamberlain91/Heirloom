namespace Heirloom.Collections
{
    /// <summary>
    /// Describes the behaviour of a <see cref="Heap{T}"/>.
    /// </summary>
    /// <tags>Heap, Priority</tags>
    /// <category>Heap</category>
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
