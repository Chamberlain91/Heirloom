namespace Heirloom.Collections
{
    /// <summary>
    /// Represents a choice of traversing a graph.
    /// </summary>
    public enum TraversalMethod
    {
        /// <summary>
        /// Depth-first traversal, prioritizing children.
        /// </summary>
        DepthFirst,

        /// <summary>
        /// Breadth-first traversal, prioritizing siblings.
        /// </summary>
        BreadthFirst
    }
}
