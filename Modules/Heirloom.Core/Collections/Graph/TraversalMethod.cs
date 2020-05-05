namespace Heirloom.Collections
{
    /// <summary>
    /// Represents a choice of traversing a graph.
    /// </summary>
    public enum TraversalMethod
    {
        /// <summary>
        /// Depth-first traversal, using descendant priority.
        /// </summary>
        DepthFirst,

        /// <summary>
        /// Breadth-first traversal, using sibling priority.
        /// </summary>
        BreadthFirst
    }
}
