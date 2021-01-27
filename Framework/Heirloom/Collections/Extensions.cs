using System;
using System.Collections.Generic;

namespace Heirloom.Collections
{
    public static class Extensions
    {
        #region Enumerable To Heap

        /// <summary>
        /// Constructs a new <see cref="Heap{T}"/> from an <see cref="IEnumerable{T}"/>
        /// </summary>
        public static Heap<T> ToHeap<T>(this IEnumerable<T> items, HeapType type = HeapType.Min) where T : IComparable<T>
        {
            var heap = new Heap<T>(type);
            heap.AddRange(items);
            return heap;
        }

        /// <summary>
        /// Constructs a new <see cref="Heap{T}"/> from an <see cref="IEnumerable{T}"/>
        /// </summary>
        public static Heap<T> ToHeap<T>(this IEnumerable<T> items, Comparison<T> comparison, HeapType type = HeapType.Min) where T : IComparable<T>
        {
            var heap = new Heap<T>(comparison, type);
            heap.AddRange(items);
            return heap;
        }

        #endregion 
    }
}
