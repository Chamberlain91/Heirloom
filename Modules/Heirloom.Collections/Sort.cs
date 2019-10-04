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
        public static IEnumerable<T> InsertionSort<T>(this IEnumerable<T> enumerable)
        {
            var data = enumerable.ToArray();
            InsertionSort(data);
            return data;
        }

        /// <summary>
        /// Sort the list using insertion sort.
        /// </summary>
        public static void InsertionSort<T>(this IList<T> list)
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

        #region Merge Sort

        /// <summary>
        /// Sorts an enumerable by insertion sort (an array is created to sort).
        /// </summary>
        public static IEnumerable<T> MergeSort<T>(this IEnumerable<T> enumerable)
        {
            var data = enumerable.ToArray();
            MergeSort(data);
            return data;
        }

        /// <summary>
        /// Sort the list using merge sort.
        /// </summary>
        public static void MergeSort<T>(this IList<T> list)
        {
            MergeSort(list, Comparer<T>.Default.Compare);
        }

        /// <summary>
        /// Sort the list using merge sort.
        /// </summary>
        public static void MergeSort<T>(this IList<T> list, Comparison<T> comparison)
        // ref: https://www.geeksforgeeks.org/iterative-merge-sort/
        // todo: replace with personal implementation
        {
            // Merge subarrays in bottom up manner. First merge subarrays of 
            // size 1 to create sorted subarrays of size 2, then merge subarrays 
            // of size 2 to create sorted subarrays of size 4, and so on. 
            for (var curr_size = 1; curr_size < list.Count; curr_size *= 2)
            {
                // Pick starting point of different subarrays of current size 
                for (var left = 0; left < list.Count - 1; left += curr_size * 2)
                {
                    // Find ending point of left subarray. mid+1 is starting  
                    // point of right 
                    var middle = Math.Min(left + curr_size - 1, list.Count - 1);

                    var right = Math.Min(left + 2 * curr_size - 1, list.Count - 1);

                    // Merge Subarrays arr[left_start...mid] & arr[mid+1...right_end] 
                    merge(list, left, middle, right);
                }
            }

            /* Function to merge the two haves arr[l..m] and arr[m+1..r] of array arr[] */
            unsafe void merge(IList<T> arr, int l, int m, int r)
            {
                int i, j, k;
                var n1 = m - l + 1;
                var n2 = r - m;

                /* create temp arrays */
                var L = new T[n1];
                var R = new T[n2];

                /* Copy data to temp arrays L[] and R[] */
                for (i = 0; i < n1; i++)
                {
                    L[i] = arr[l + i];
                }

                for (j = 0; j < n2; j++)
                {
                    R[j] = arr[m + 1 + j];
                }

                /* Merge the temp arrays back into arr[l..r]*/
                i = 0;
                j = 0;
                k = l;
                while (i < n1 && j < n2)
                {
                    if (comparison(L[i], R[j]) <= 0)
                    {
                        arr[k] = L[i];
                        i++;
                    }
                    else
                    {
                        arr[k] = R[j];
                        j++;
                    }
                    k++;
                }

                /* Copy the remaining elements of L[], if there are any */
                while (i < n1)
                {
                    arr[k] = L[i];
                    i++;
                    k++;
                }

                /* Copy the remaining elements of R[], if there are any */
                while (j < n2)
                {
                    arr[k] = R[j];
                    j++;
                    k++;
                }
            }
        }

        #endregion

        #region Heap Sort

        /// <summary>
        /// Sorts items by exploiting <see cref="Heap{T}"/> in ascending order.
        /// </summary>
        public static IEnumerable<T> HeapSort<T>(IEnumerable<T> items)
        {
            return HeapSort(items, HeapType.Min);
        }

        /// <summary>
        /// Sorts items by exploiting <see cref="Heap{T}"/> in ascending order.
        /// </summary>
        public static IEnumerable<T> HeapSort<T>(IEnumerable<T> items, Comparison<T> comparison)
        {
            return HeapSort(items, comparison, HeapType.Min);
        }

        /// <summary>
        /// Sorts items by exploiting <see cref="Heap{T}"/> in ascending order (<see cref="HeapType.Min"/>) or descending order (<see cref="HeapType.Max"/>).
        /// </summary>
        public static IEnumerable<T> HeapSort<T>(IEnumerable<T> items, HeapType type)
        {
            return HeapSort(items, Comparer<T>.Default.Compare, type);
        }

        /// <summary>
        /// Sorts items by exploiting <see cref="Heap{T}"/> in ascending order (<see cref="HeapType.Min"/>) or descending order (<see cref="HeapType.Max"/>).
        /// </summary>
        public static IEnumerable<T> HeapSort<T>(IEnumerable<T> items, Comparison<T> comparison, HeapType type)
        {
            // Create min-heap
            var heap = new Heap<T>(comparison, type);
            heap.AddRange(items);

            // Remove items from the heap (in sorted order)
            while (heap.Count > 0)
            {
                yield return heap.Remove();
            }
        }

        #endregion
    }
}
