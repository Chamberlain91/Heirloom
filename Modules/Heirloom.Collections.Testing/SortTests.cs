using System;
using System.Collections.Generic;
using NUnit.Framework;

using static Heirloom.Collections.Testing.Utilities;

namespace Heirloom.Collections.Testing
{
    [TestFixture]
    public class SortTests
    {
        public delegate IEnumerable<T> SortingFunction<T>(IEnumerable<T> items);

        // Number of generated elements to sort
        private const int N = 16384;

        private static TestCaseData[] GetTestData()
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
                new TestCaseData((Array) CreatePartiallyRandomSortableArray(N)).SetName("{m}(Partially Random)"),
                new TestCaseData((Array) CreateRandomSortableArray(N)).SetName("{m}(Random)"),
            };
        }

        [Test, TestCaseSource(nameof(GetTestData))]
        public void StableSort<T>(IList<T> items) where T : IComparable<T>
        {
            MergeSort.StableSort(items);
            Assert.IsTrue(items.IsAscendingOrder(), "Items were not in ascending order after sort.");
        }
    }
}
