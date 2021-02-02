using System;
using System.Collections.Generic;

using Xunit;
using Xunit.Abstractions;

using static Heirloom.Testing.Unit.Utilities;

namespace Heirloom.Testing.Unit
{
    public class MergeSortTest : TestingFixture
    {
        public delegate IEnumerable<T> SortingFunction<T>(IEnumerable<T> items);

        // Number of generated elements to sort
        private const int N = 16384;

        public MergeSortTest(ITestOutputHelper output)
            : base(output)
        { }

        public static IEnumerable<object[]> GetTestData()
        {
            // Integers
            yield return new object[] { CreateOrderedIntegerArray(N) };
            yield return new object[] { CreateReverseIntegerArray(N) };
            yield return new object[] { CreatePartiallyRandomIntegerArray(N) };
            yield return new object[] { CreateRandomIntegerArray(N) };

            // Objects
            yield return new object[] { CreateOrderedSortableArray(N) };
            yield return new object[] { CreateReverseSortableArray(N) };
            yield return new object[] { CreatePartiallyRandomSortableArray(N) };
            yield return new object[] { CreateRandomSortableArray(N) };
        }

        [Theory(DisplayName = "Stable Sort")]
        [MemberData(nameof(GetTestData))]
        public void StableSort<T>(IList<T> items) where T : IComparable<T>
        {
            MergeSort.StableSort(items);
            Assert.True(items.IsAscendingOrder(), "Items were not in ascending order after sort.");
        }
    }
}
