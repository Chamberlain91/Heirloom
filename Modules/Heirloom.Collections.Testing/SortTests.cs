using System.Linq;

using Heirloom.Collections;

using NUnit.Framework;

namespace Heirloom.Collections.Testing
{
    [TestFixture]
    public class SortTests : CollectionTests
    {
        [Test]
        public void InsertionSortOrdered()
        {
            var data = CreateRandomIntegerArray(16);
            data.InsertionSort(); // Extension function on Sort.InsertionSort
            Assert.IsTrue(data.IsAscendingOrder(), "Items were not in ascending order after insertion sort");
        }

        [Test]
        public void InsertionSortRandom()
        {
            var data = CreateOrderedIntegerArray(16);
            data.InsertionSort(); // Extension function on Sort.InsertionSort
            Assert.IsTrue(data.IsAscendingOrder(), "Items were not in ascending order after insertion sort");
        }

        [Test]
        public void HeapSortOrdered()
        {
            var items = Enumerable.Range(0, 32);
            Assert.IsTrue(Sort.HeapSort(items).IsAscendingOrder(), "Items were not in ascending order after heap sort");
        }

        [Test]
        public void HeapSortReverse()
        {
            var items = Enumerable.Range(0, 32).Reverse();
            Assert.IsTrue(Sort.HeapSort(items).IsAscendingOrder(), "Items were not in ascending order after heap sort");
        }

        [Test]
        public void HeapSortRandom()
        {
            var items = CreateRandomIntegerArray(32);
            Assert.IsTrue(Sort.HeapSort(items).IsAscendingOrder(), "Items were not in ascending order after heap sort");
        }
    }
}
