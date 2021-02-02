using System.Collections.Generic;
using System.Linq;

using Heirloom.Collections;

using Xunit;
using Xunit.Abstractions;

namespace Heirloom.Testing.Unit
{
    public class SearchTests : TestingFixture
    {
        public SearchTests(ITestOutputHelper output)
            : base(output)
        { }

        [Fact]
        public void BreadthFirstTraversal()
        {
            var tree = CreateTreeGraph();

            // 1
            // ├── 5
            // └── 2
            //     ├── 4
            //     └── 3

            // note: order seems to be something like "reverse successor order" dependant on edge insertion order.
            Assert.Collection(Search.BreadthFirst(tree.Vertices.First(), tree.GetSuccessors),
                x => Assert.Equal(1, x),
                x => Assert.Equal(5, x),
                x => Assert.Equal(2, x),
                x => Assert.Equal(4, x),
                x => Assert.Equal(3, x));
        }

        [Fact]
        public void DepthFirstTraversal()
        {
            var graph = CreateTreeGraph();

            // 1
            // ├── 5
            // └── 2
            //     ├── 4
            //     └── 3

            // note: order seems to be something like "reverse successor order" dependant on edge insertion order.
            Assert.Collection(Search.DepthFirst(graph.Vertices.First(), graph.GetSuccessors),
                x => Assert.Equal(1, x),
                x => Assert.Equal(2, x),
                x => Assert.Equal(3, x),
                x => Assert.Equal(4, x),
                x => Assert.Equal(5, x));
        }

        [Theory(DisplayName = "Detect Cycles")]
        [MemberData(nameof(GenerateCyclicGraphs))]
        public void DetectCyclicGraph(Graph<int, int> graph, bool cycleExpected)
        {
            Assert.Equal(Search.DetectCyclicGraph(graph.Vertices.First(), graph.GetSuccessors), cycleExpected);
        }

        public static IEnumerable<object[]> GenerateCyclicGraphs()
        {
            yield return new object[] { CreateTriangleGraph(directed: true), true };
            yield return new object[] { CreateTriangleGraph(directed: false), true };
            yield return new object[] { CreateTreeGraph(), false };
        }
    }
}
