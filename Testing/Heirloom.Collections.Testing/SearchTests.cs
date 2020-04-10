using NUnit.Framework;

namespace Heirloom.Collections.Testing
{
    [TestFixture]
    public class SearchTests
    {
        [Test]
        public void HeuristicSearchCostCall()
        {
            Assert.Inconclusive("No Test Implemented Yet");

            //var costFunctionCalled = false;

            //var s = new IntVector(0, 0);
            //var g = new IntVector(5, 5);

            //Search.HeuristicSearch(s, g, x => x.Children, (a, b) =>
            //{
            //    costFunctionCalled = true;
            //    return IntVector.Distance(a, b);
            //});

            //// 
            //Assert.IsTrue(costFunctionCalled, $"Cost function was not called during {nameof(Search.HeuristicSearch)}.");
        }

        [Test]
        public void HeuristicSearchHeuristicCall()
        {
            // See HeuristicSearchCostCall for similar implementation notes
            Assert.Inconclusive("No Test Implemented Yet");
        }

        [Test]
        public void HeuristicSearchExpectedPath()
        {
            Assert.Inconclusive("No Test Implemented Yet");
        }

        [Test]
        public void BreadthFirstTraversal()
        {
            // Search.BreadthFirst()
            Assert.Inconclusive("No Test Implemented Yet");
        }

        [Test]
        public void DepthFirstTraversal()
        {
            // Search.DepthFirst()
            Assert.Inconclusive("No Test Implemented Yet");
        }
    }
}
