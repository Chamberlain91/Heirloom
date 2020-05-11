using System;
using System.Collections.Generic;

using Heirloom.Collections;

namespace Heirloom
{
    /// <summary>
    /// Provides extension methods various types within Heirloom.
    /// </summary>
    public static partial class Extensions
    {
        /// <summary>
        /// Applies a function to each item in the enumerable.
        /// </summary>
        public static void Apply<T>(this IEnumerable<T> list, Action<T> action)
        {
            foreach (var item in list)
            {
                action(item);
            }
        }

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

        #region Validate Enumerable Order

        /// <summary>
        /// Checks if the sequence is in ascending order (sequential equivalent items are considered in order).
        /// </summary>
        public static bool IsAscendingOrder<T>(this IEnumerable<T> seq) where T : IComparable<T>
        {
            var hasPredecessor = false;
            var predecessor = default(T);

            foreach (var current in seq)
            {
                // Is predecessor value larger than current? Not in ascending order!
                if (hasPredecessor && predecessor.CompareTo(current) > 0) { return false; }

                hasPredecessor = true;
                predecessor = current;
            }

            return true;
        }

        /// <summary>
        /// Checks if the sequence is in descending order (sequential equivalent items are considered in order).
        /// </summary>
        public static bool IsDescendingOrder<T>(this IEnumerable<T> seq) where T : IComparable<T>
        {
            var hasPredecessor = false;
            var predecessor = default(T);

            foreach (var current in seq)
            {
                // Is predecessor value smaller than current? Not in descending order!
                if (hasPredecessor && predecessor.CompareTo(current) < 0) { return false; }

                hasPredecessor = true;
                predecessor = current;
            }

            return true;
        }

        #endregion
    }
}
