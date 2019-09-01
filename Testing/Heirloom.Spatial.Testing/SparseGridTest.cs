using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace Heirloom.Collections.Spatial.Testing
{
    [TestClass]
    public class SparseGridTest
    {
        [TestMethod]
        public void ContainsAt()
        {
            var grid = new SparseGrid<char>();
            var stateBefore = grid.ContainsAt((0, 0));
            grid[(0, 0)] = ' ';
            var stateAfter = grid.ContainsAt((0, 0));

            // 
            Assert.AreNotEqual(stateBefore, stateAfter,
                $"Setting a value into an empty sparse grid should change the returm of {nameof(grid.ContainsAt)}.");
        }
    }
}
