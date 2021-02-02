using System;
using System.Collections.Generic;
using System.Linq;

using Heirloom.Collections;

using Xunit;
using Xunit.Abstractions;

using static Heirloom.Testing.Unit.Utilities;

namespace Heirloom.Testing.Unit
{
    public class HeapTests : TestingFixture
    {
        public HeapTests(ITestOutputHelper output)
            : base(output)
        { }

        [Fact]
        public void ContainedMutated()
        {
            var rnd = new Random(223);

            for (var i = 0; i < 10; i++) // 10 attempts
            {
                var sortables = CreateRandomSortableArray(10);

                // Create heap with sortable objects
                var heap = new Heap<SortableObject>();
                heap.AddRange(sortables);

                // Mark a random item with a new value, changing its comparable state
                for (var q = 0; q < 5; q++)
                {
                    var _i = rnd.Next(0, sortables.Length);
                    var _r = rnd.Next(0, sortables.Length);
                    sortables[_i].Number = _r;

                    // Update the items position within the heap
                    heap.Update(sortables[_i]);
                }

                // Assert heap extraction is indeed in order
                var list = ExtractElements(heap);
                Assert.True(list.IsAscendingOrder(), "Items were not in ascending order after heap mutate");
            }
        }

        [Fact]
        public void InsertOne()
        {
            var heap = new Heap<int> { 1 };
            var data = ((IEnumerable<int>) heap).ToArray();

            // 
            Assert.True(heap.Count == data.Length, "Heap and IEnumerable.ToArray() should have the same count.");
            Assert.True(heap.Count == 1, $"The heap should only contain one element. {heap.Count} was found");
        }

        [Fact]
        public void RemoveOne()
        {
            var heap = new Heap<int> { 3, 1, 2 };

            // 
            Assert.True(heap.Remove() == 1, "First item removed from the heap should be 1.");
            Assert.True(heap.Count == 2, $"The heap should only contain two elements. Instead heap has {heap.Count}.");
        }

        [Fact]
        public void RemoveAll()
        {
            // Construct a heap of 16 integers in random order
            var heap = new Heap<int>();
            foreach (var x in CreateRandomIntegerArray(16))
            {
                heap.Add(x);
            }

            // We should have 16 elements
            Assert.True(heap.Count == 16, $"The heap should contain sixteen elements. Instead heap has {heap.Count}.");

            // We should be able to extract the elements and be in ascending order
            var list = ExtractElements(heap);
            Assert.True(list.IsAscendingOrder(), "Items were not in ascending order from enumerate");
        }

        [Fact]
        public void PeekOne()
        {
            var heap = new Heap<int>() { 3, 1, 2 };
            Assert.True(heap.Peek() == 1, "Top item should have been 1 when peeking the heap.");
            Assert.True(heap.Count == 3, $"The heap should contain three elements. Instead heap has {heap.Count}.");
        }

        [Fact]
        public void PeekDefaultEmptyException()
        {
            var heap = new Heap<int>();

            // Should throw exception because the heap is empty
            Assert.Throws<InvalidOperationException>(() => heap.Peek());
        }

        [Fact]
        public void PeekEmptyException()
        {
            var heap = new Heap<int>() { 1, 3, 5 };
            while (heap.Count > 0) { heap.Remove(); } // Remove each item

            // Should throw exception because the heap is empty
            Assert.Throws<InvalidOperationException>(() => heap.Peek());
        }

        [Fact]
        public void ToArray()
        {
            // Create heap with 16 elements
            var heap = new Heap<int>();
            heap.AddRange(CreateRandomIntegerArray(16));

            // 
            var array = heap.ToArray();
            Assert.True(array.IsAscendingOrder(), "Items were not in ascending order from enumerate");
        }

        private static List<T> ExtractElements<T>(Heap<T> heap)
        {
            var list = new List<T>();

            while (heap.Count > 0)
            {
                list.Add(heap.Remove());
            }

            return list;
        }
    }
}
