using System;

using Heirloom.Collections;

using Xunit;
using Xunit.Abstractions;

namespace Heirloom.Testing.Unit
{
    public class GraphTest : TestingFixture
    {
        public GraphTest(ITestOutputHelper output)
            : base(output)
        { }

        #region Vertex Mutation

        [Fact(DisplayName = "Vertex - Add")]
        public void AddVertices()
        {
            // Create a graph, we expect it to be empty
            var graph = new Graph<int, int>();
            Assert.Equal(0, graph.Vertices.Count);

            // Add vertex '0', we expect one vertices
            graph.AddVertex(0);
            Assert.Equal(1, graph.Vertices.Count);

            // Add vertex '1', we expect two vertices
            graph.AddVertex(1);
            Assert.Equal(2, graph.Vertices.Count);

            // Add vertex '1', we expect an exception now.
            Assert.Throws<ArgumentException>(() => graph.AddVertex(1));
        }

        [Fact(DisplayName = "Vertex - Remove Basic")]
        public void RemoveVertices()
        {
            // Construct graph with 3 vertices
            var graph = new Graph<int, int>();
            graph.AddVertex(1);
            graph.AddVertex(2);
            graph.AddVertex(3);
            Assert.Equal(3, graph.Vertices.Count);

            // Remove vertex '2'
            Assert.True(graph.RemoveVertex(2));
            Assert.Equal(2, graph.Vertices.Count);

            // Remove vertex '2'
            Assert.False(graph.RemoveVertex(2));
            Assert.Equal(2, graph.Vertices.Count);
        }

        [Fact(DisplayName = "Vertex - Remove Directed Disconnect")]
        public void RemoveVertexDirectedDisconnect()
        {
            var graph = CreateTriangleGraph(directed: true);
            Assert.Equal(3, graph.Edges.Count);

            // Remove vertex
            graph.RemoveVertex(1);

            Assert.Equal(2, graph.Vertices.Count);
            Assert.Equal(1, graph.Edges.Count);
        }

        [Fact(DisplayName = "Vertex - Remove Undirected Disconnect")]
        public void RemoveVertexUndirectedDisconnect()
        {
            var graph = CreateTriangleGraph(directed: false);
            Assert.Equal(3, graph.Edges.Count);
            Assert.False(graph.IsDirected);

            // Remove vertex
            graph.RemoveVertex(1);

            Assert.Equal(2, graph.Vertices.Count);
            Assert.Equal(1, graph.Edges.Count);
        }

        #endregion

        #region Edge Mutation

        [Fact(DisplayName = "Edge - Add Undirected")]
        public void AddUndirectedEdges()
        {
            var graph = CreateTriangleGraph(directed: false);
            Assert.Equal(3, graph.Edges.Count);
            Assert.False(graph.IsDirected);

            // Assert that the edges exist regardless of order
            Assert.True(graph.ContainsEdge(1, 2));
            Assert.True(graph.ContainsEdge(2, 1));
            Assert.True(graph.ContainsEdge(2, 3));
            Assert.True(graph.ContainsEdge(3, 2));
            Assert.True(graph.ContainsEdge(3, 1));
            Assert.True(graph.ContainsEdge(1, 3));
        }

        [Fact(DisplayName = "Edge - Add Directed")]
        public void AddDirectedEdges()
        {
            var graph = CreateTriangleGraph(directed: true);
            Assert.Equal(3, graph.Edges.Count);
            Assert.True(graph.IsDirected);

            // Assert that the edges exist in the correct order
            Assert.True(graph.ContainsEdge(1, 2));
            Assert.False(graph.ContainsEdge(2, 1));
            Assert.True(graph.ContainsEdge(2, 3));
            Assert.False(graph.ContainsEdge(3, 2));
            Assert.True(graph.ContainsEdge(3, 1));
            Assert.False(graph.ContainsEdge(1, 3));
        }

        #endregion

        [Fact(DisplayName = "Graph - Clear")]
        public void ClearGraph()
        {
            var graph = CreateTriangleGraph(directed: false);
            Assert.Equal(3, graph.Edges.Count);

            graph.Clear();

            Assert.Equal(0, graph.Vertices.Count);
            Assert.Equal(0, graph.Edges.Count);
        }

        #region Topological Order

        [Fact(DisplayName = "Topological Ordering - Simple Tree")]
        public void BasicTopologicalOrder()
        {
            var graph = CreateTreeGraph();
            Assert.Equal(5, graph.Vertices.Count);
            Assert.Equal(4, graph.Edges.Count);
            Assert.True(graph.IsDirected);

            Assert.Collection(graph.GetTopologicalOrder(),
                x => Assert.Equal(1, x),
                x => Assert.Equal(2, x),
                x => Assert.Equal(3, x),
                x => Assert.Equal(4, x),
                x => Assert.Equal(5, x));
        }

        [Fact(DisplayName = "Topological Ordering - Undirected Exception")]
        public void UndirectedTopologicalOrderException()
        {
            var graph = CreateTriangleGraph(directed: false);
            Assert.False(graph.IsDirected);

            // A topological ordering is not possible with an undirected graph, so we expect an exception.
            Assert.Throws<InvalidOperationException>(() => graph.GetTopologicalOrder());
        }

        [Fact(DisplayName = "Topological Ordering - Cyclic Exception")]
        public void TopologicalOrderCycleException()
        {
            var graph = CreateTriangleGraph(directed: true);
            Assert.True(graph.IsDirected);

            // A topological ordering is not possible with a cyclic graph, so we expect an exception.
            Assert.Throws<InvalidOperationException>(() => graph.GetTopologicalOrder());
        }

        #endregion

        private static Graph<int, int> CreateTriangleGraph(bool directed)
        {
            var graph = new Graph<int, int>(directed);

            // Add 3 vertices
            graph.AddVertex(1);
            graph.AddVertex(2);
            graph.AddVertex(3);

            // 1 -> 2 -> 3 -┐
            // ^------------┘

            // Connect edges, makes a triangle (rotating)
            graph.AddEdge(1, 2);
            graph.AddEdge(2, 3);
            graph.AddEdge(3, 1);

            return graph;
        }

        private static Graph<int, int> CreateTreeGraph()
        {
            var graph = new Graph<int, int>(directed: true);

            // Add 5 vertices
            graph.AddVertex(1);
            graph.AddVertex(2);
            graph.AddVertex(3);
            graph.AddVertex(4);
            graph.AddVertex(5);

            // 
            // 1
            // ├── 2
            // │   └── 3
            // └── 4
            //     └── 5

            graph.AddEdge(1, 2);
            graph.AddEdge(1, 4);
            graph.AddEdge(2, 3);
            graph.AddEdge(4, 5);

            return graph;
        }
    }
}
