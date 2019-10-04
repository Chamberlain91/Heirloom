using System;
using System.Collections.Generic;

using NUnit.Framework;

namespace Heirloom.Collections.Testing
{
    [TestFixture]
    public class SortTests : TestingBase
    {
        public delegate IEnumerable<T> SortingFunction<T>(IEnumerable<T> items);

        // Number of generated elements to sort
        private const int N = 16384 / 2;

        private static TestCaseData[] GetSortingData()
        {
            return new[] {

                // Integers
                new TestCaseData(CreateOrderedIntegerArray(N)).SetName("{m}(Ordered)"),
                new TestCaseData(CreateReverseIntegerArray(N)).SetName("{m}(Reverse)"),
                new TestCaseData(CreatePartiallyRandomIntegerArray(N)).SetName("{m}(Partially Random)"),
                new TestCaseData(CreateRandomIntegerArray(N)).SetName("{m}(Random)"),

                // Objects
                new TestCaseData((Array) CreateOrderedSortableArray(N)).SetName("{m}(Ordered)"),
                new TestCaseData((Array) CreateReverseSortableArray(N)).SetName("{m}(Reverse)"),
                new TestCaseData((Array) CreateRandomSortableArray(N)).SetName("{m}(Random)"),
            };
        }

        [Test, TestCaseSource(nameof(GetSortingData))]
        public void MergeSort<T>(IEnumerable<T> items) where T : IComparable<T>
        {
            items = Sort.MergeSort(items);
            Assert.IsTrue(items.IsAscendingOrder(), "Items were not in ascending order after insertion sort");
        }

        [Test, TestCaseSource(nameof(GetSortingData))]
        public void HeapSort<T>(IEnumerable<T> items) where T : IComparable<T>
        {
            items = Sort.HeapSort(items);
            Assert.IsTrue(items.IsAscendingOrder(), "Items were not in ascending order after insertion sort");
        }

        [Test, TestCaseSource(nameof(GetSortingData))]
        public void InsertionSort<T>(IEnumerable<T> items) where T : IComparable<T>
        {
            items = Sort.InsertionSort(items);
            Assert.IsTrue(items.IsAscendingOrder(), "Items were not in ascending order after insertion sort");
        }
    }
}
