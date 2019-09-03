namespace Heirloom.Collections
{
    /// <summary>
    /// Configuration parameters for setting up a graph.
    /// </summary>
    public struct GraphConfiguration
    {
        private bool? _allowSelfLoops;

        private bool? _allowParallelEdges;

        private bool? _allowNegativeWeight;

        private bool? _isWeighted;

        private bool? _isDirected;

        /// <summary>
        /// Allow the graph to have self looping edges.
        /// </summary>
        public bool AllowSelfLoops
        {
            get => _allowSelfLoops ?? false;
            set => _allowSelfLoops = value;
        }

        /// <summary>
        /// Allow the graph to have two or more edges between the same nodes.
        /// </summary>
        public bool AllowParallelEdges
        {
            get => _allowParallelEdges ?? false;
            set => _allowParallelEdges = value;
        }

        /// <summary>
        /// Allow the graph to have negative weights on edges.
        /// </summary>
        public bool AllowNegativeWeight
        {
            get => _allowNegativeWeight ?? false;
            set => _allowNegativeWeight = value;
        }

        /// <summary>
        /// Allow the graph to have edges with weights differing from 1.
        /// </summary>
        public bool IsWeighted
        {
            get => _isWeighted ?? true;
            set => _isWeighted = value;
        }

        /// <summary>
        /// Allow the graph to have directionality. Where A to B is different than B to A.
        /// </summary>
        public bool IsDirected
        {
            get => _isDirected ?? true;
            set => _isDirected = value;
        }
    }
}
