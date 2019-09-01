using System;
using System.Collections.Generic;
using System.Linq;

using Heirloom.Collections;

using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace Heirloom.Collection.Testing
{
    [TestClass]
    public class HeapTest : CollectionTest
    {
        [TestMethod]
        public void ContainedMutated()
        {
            for (var i = 0; i < 10; i++) // 10 attempts
            {
                var sortables = CreateRandomSortableArray(10);

                // Create heap with sortable objects
                var heap = new Heap<SortableObject>();
                heap.AddRange(sortables);

                // Mark a random item with a new value, changing its comparable state
                for (var q = 0; q < 5; q++)
                {
                    var _i = Random.Next(0, sortables.Length);
                    var _r = Random.Next(0, sortables.Length);
                    sortables[_i].Number = _r;

                    // Update the items position within the heap
                    heap.Update(sortables[_i]);
                }

                // Assert heap extraction is indeed in order
                var list = ExtractElements(heap);
                Assert.IsTrue(list.IsAscendingOrder(), "Items were not in ascending order after heap mutate");
            }
        }

        [TestMethod]
        public void InsertOne()
        {
            var heap = new Heap<int> { 1 };
            var data = ((IEnumerable<int>) heap).ToArray();

            // 
            Assert.IsTrue(heap.Count == data.Length, "Heap and IEnumerable.ToArray() should have the same count.");
            Assert.IsTrue(heap.Count == 1, $"The heap should only contain one element. {heap.Count} was found");
        }

        [TestMethod]
        public void RemoveOne()
        {
            var heap = new Heap<int> { 3, 1, 2 };

            // 
            Assert.IsTrue(heap.Remove() == 1, "First item removed from the heap should be 1.");
            Assert.IsTrue(heap.Count == 2, $"The heap should only contain two elements. Instead heap has {heap.Count}.");
        }

        [TestMethod]
        public void RemoveAll()
        {
            // Construct a heap of 16 integers in random order
            var heap = new Heap<int>();
            foreach (var x in CreateRandomIntegerArray(16))
            {
                heap.Add(x);
            }

            // We should have 16 elements
            Assert.IsTrue(heap.Count == 16, $"The heap should contain sixteen elements. Instead heap has {heap.Count}.");

            // We should be able to extract the elements and be in ascending order
            var list = ExtractElements(heap);
            Assert.IsTrue(list.IsAscendingOrder(), "Items were not in ascending order from enumerate");
        }

        [TestMethod]
        public void PeekOne()
        {
            var heap = new Heap<int>() { 3, 1, 2 };
            Assert.IsTrue(heap.Peek() == 1, "Top item should have been 1 when peeking the heap.");
            Assert.IsTrue(heap.Count == 3, $"The heap should contain three elements. Instead heap has {heap.Count}.");
        }

        [TestMethod]
        [ExpectedException(typeof(InvalidOperationException))]
        public void PeekDefaultEmptyException()
        {
            var heap = new Heap<int>();
            heap.Peek(); // Should throw exception because the heap is empty
        }

        [TestMethod]
        [ExpectedException(typeof(InvalidOperationException))]
        public void PeekEmptyException()
        {
            var heap = new Heap<int>() { 1, 3, 5 };
            while (heap.Count > 0) { heap.Remove(); } // Remove each item
            heap.Peek(); // Should throw exception because the heap is empty
        }

        [TestMethod]
        public void ToArray()
        {
            // Create heap with 16 elements
            var heap = new Heap<int>();
            heap.AddRange(CreateRandomIntegerArray(16));

            // 
            var array = heap.ToArray();
            Assert.IsTrue(array.IsAscendingOrder(), "Items were not in ascending order from enumerate");
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
