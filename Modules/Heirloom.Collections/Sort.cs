using System;
using System.Collections.Generic;
using System.Linq;

namespace Heirloom.Collections
{
    public static class Sort
    {
        #region Insertion Sort

        /// <summary>
        /// Sorts an enumerable by insertion sort (an array is created to sort).
        /// </summary>
        public static IEnumerable<T> InsertionSort<T>(this IEnumerable<T> enumerable) where T : IComparable<T>
        {
            var data = enumerable.ToArray();
            data.InsertionSort();
            return data;
        }

        /// <summary>
        /// Sort the list using insertion sort.
        /// </summary>
        public static void InsertionSort<T>(this IList<T> list) where T : IComparable<T>
        {
            InsertionSort(list, Comparer<T>.Default.Compare);
        }

        /// <summary>
        /// Sort the list using insertion sort.
        /// </summary>
        public static void InsertionSort<T>(this IList<T> list, Comparison<T> comparison)
        {
            if (list == null) { throw new ArgumentNullException(nameof(list)); }

            // 
            for (var i = 1; i < list.Count; i++)
            {
                var x = list[i];
                var j = i - 1;

                while (j >= 0 && comparison(list[j], x) > 0)
                {
                    list[j + 1] = list[j];
                    j--;
                }

                list[j + 1] = x;
            }
        }

        #endregion

        #region Heap Sort

        /// <summary>
        /// Sorts items by exploiting <see cref="Heap{T}"/> in ascending order.
        /// </summary>
        public static IEnumerable<T> HeapSort<T>(IEnumerable<T> items)
        {
            return HeapSort(HeapType.Min, items);
        }

        /// <summary>
        /// Sorts items by exploiting <see cref="Heap{T}"/> in descending order.
        /// </summary>
        public static IEnumerable<T> HeapSortDescending<T>(IEnumerable<T> items)
        {
            return HeapSort(HeapType.Max, items);
        }

        private static IEnumerable<T> HeapSort<T>(HeapType type, IEnumerable<T> items)
        {
            // Create min-heap
            var heap = new Heap<T>(type);
            heap.AddRange(items);

            // Remove items from the heap (in sorted order)
            while (heap.Count > 0)
            {
                yield return heap.Remove();
            }
        }

        #endregion

        public static void Shuffle<T>(this IList<T> items, Random random)
        {
            for (var i = 0; i < items.Count; i++)
            {
                var r = random.Next(items.Count);

                // Swap
                var t = items[r];
                items[r] = items[i];
                items[i] = t;
            }
        }

        public static void Radix(int[] arr)
        // TODO: Find source/implement myself/better design
        // Fast linear time sort, but limited use case.
        // ref: ?
        {
            var tmp = new int[arr.Length];
            for (var shift = 31; shift > -1; --shift)
            {
                var j = 0;

                for (var i = 0; i < arr.Length; ++i)
                {
                    var move = (arr[i] << shift) >= 0;
                    if (shift == 0 ? !move : move)
                    {
                        arr[i - j] = arr[i];
                    }
                    else
                    {
                        tmp[j++] = arr[i];
                    }
                }

                Array.Copy(tmp, 0, arr, arr.Length - j, j);
            }
        }
    }
}
