using System.Linq;

using Heirloom.Collections;

using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace Heirloom.Collection.Testing
{
    [TestClass]
    public class SortTest : CollectionTest
    {
        [TestMethod]
        public void InsertionSortOrdered()
        {
            var data = CreateRandomIntegerArray(16);
            data.InsertionSort(); // Extension function on Sort.InsertionSort
            Assert.IsTrue(data.IsAscendingOrder(), "Items were not in ascending order after insertion sort");
        }

        [TestMethod]
        public void InsertionSortRandom()
        {
            var data = CreateOrderedIntegerArray(16);
            data.InsertionSort(); // Extension function on Sort.InsertionSort
            Assert.IsTrue(data.IsAscendingOrder(), "Items were not in ascending order after insertion sort");
        }

        [TestMethod]
        public void HeapSortOrdered()
        {
            var items = Enumerable.Range(0, 32);
            Assert.IsTrue(Sort.HeapSort(items).IsAscendingOrder(), "Items were not in ascending order after heap sort");
        }

        [TestMethod]
        public void HeapSortReverse()
        {
            var items = Enumerable.Range(0, 32).Reverse();
            Assert.IsTrue(Sort.HeapSort(items).IsAscendingOrder(), "Items were not in ascending order after heap sort");
        }

        [TestMethod]
        public void HeapSortRandom()
        {
            var items = CreateRandomIntegerArray(32);
            Assert.IsTrue(Sort.HeapSort(items).IsAscendingOrder(), "Items were not in ascending order after heap sort");
        }
    }
}
