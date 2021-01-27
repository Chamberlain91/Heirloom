namespace Heirloom.Collections
{
    /// <summary>
    /// Represents a choice of traversing a graph.
    /// </summary>
    /// <tags>Graph, Traverse, Traversal</tags>
    /// <category>Graph</category>
    public enum TraversalOrder

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
