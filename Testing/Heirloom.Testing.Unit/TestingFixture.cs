using System;

using Heirloom.Collections;

using Xunit.Abstractions;

namespace Heirloom.Testing.Unit
{
    public abstract class TestingFixture
    {
        public TestingFixture(ITestOutputHelper output)
        {
            Output = output ?? throw new ArgumentNullException(nameof(output));
        }

        public ITestOutputHelper Output { get; }

        #region Graph Utilities

        /// <summary>
        /// Creates a triangular graph (if directed, cyclic as 123 then 1).
        /// </summary>
        protected static Graph<int, int> CreateTriangleGraph(bool directed)
        {
            var graph = new Graph<int, int>(directed);

            // Add 3 vertices
            graph.AddVertex(1);
            graph.AddVertex(2);
            graph.AddVertex(3);

            // 1 ─> 2 ─> 3 ─┐
            // └───────────<┘

            // Connect edges, makes a triangle (rotating)
            graph.AddEdge(1, 2);
            graph.AddEdge(2, 3);
            graph.AddEdge(3, 1);

            return graph;
        }

        /// <summary>
        /// Creates a simple binary tree.
        /// </summary>
        protected static Graph<int, int> CreateTreeGraph()
        {
            var graph = new Graph<int, int>(directed: true);

            // Add 5 vertices
            graph.AddVertex(1);
            graph.AddVertex(2);
            graph.AddVertex(3);
            graph.AddVertex(4);
            graph.AddVertex(5);

            // 1
            // ├── 5
            // └── 2
            //     ├── 4
            //     └── 3

            graph.AddEdge(1, 5);
            graph.AddEdge(1, 2);
            graph.AddEdge(2, 4);
            graph.AddEdge(2, 3);

            return graph;
        }

        #endregion
    }
}
