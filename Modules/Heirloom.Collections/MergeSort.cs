using System;
using System.Collections.Generic;
using System.Linq;

namespace Heirloom.Collections
{
    public static class MergeSort
    {
        /// <summary>
        /// Sorts the elements of an enumerable with a stable sort (note: an array is created).
        /// </summary>
        public static IEnumerable<T> StableSort<T>(this IEnumerable<T> enumerable)
        {
            var data = enumerable.ToArray();
            StableSort(data);
            return data;
        }

        /// <summary>
        /// Sorts the elements of the list using a stable sort.
        /// </summary>
        public static void StableSort<T>(this IList<T> list)
        {
            StableSort(list, Comparer<T>.Default.Compare);
        }

        /// <summary>
        /// Sorts the elements of the list using a stable sort.
        /// </summary>
        public static void StableSort<T>(this IList<T> items, Comparison<T> comparison)
        {
            if (items.Count < 2)
            {
                // A one element list is already sorted :)
                return;
            }
            else
            {
                // Optimization: Check if already in order
                // Makes the time complexity nlogn + n, but empirical testing seems
                // to show improvement for already sorted data with minimal impact to random data.
                if (CheckAlreadyInOrder(items, comparison)) { return; }

                // Iterate over block sizes until larger than the entire list
                for (var step = 1; step < items.Count; step *= 2)
                {
                    var iL = 0;
                    var iM = iL + step;
                    var iR = iM + step;

                    // Walk across array in step sized blocks
                    while (iR <= items.Count)
                    {
                        // Merge left and right blocks
                        merge(iL, iM, iR);

                        // Advance ranges
                        iL = iR;
                        iM = iL + step;
                        iR = iM + step;
                    }

                    // Non power of 2 tail elements
                    if (iM < items.Count)
                    {
                        iR = items.Count;
                        merge(iL, iM, iR);
                    }
                }
            }

            void merge(int iL, int iM, int iR)
            {
                var right = clone(iM, iR - iM);
                var left = clone(iL, iM - iL);

                int l = 0, r = 0;

                // For each element in the block (iL to iR)
                for (var i = iL; i < iR; i++)
                {
                    // Has the left list been exhausted?
                    if (l >= left.Length)
                    {
                        // Copy remainder of right list
                        copy(right, r, right.Length, items, i);
                        break;
                    }
                    else
                    // Has the right list been exhausted?
                    if (r >= right.Length)
                    {
                        // Copy remainder of left list
                        copy(left, l, left.Length, items, i);
                        break;
                    }
                    // Neither list has been exhausted
                    else
                    {
                        // Weave left and right arrays together
                        var lesserThan = comparison(left[l], right[r]) <= 0;
                        items[i] = lesserThan ? left[l++] : right[r++];
                    }
                }
            }

            // Create copy array elements from start to start+len
            T[] clone(int start, int len)
            {
                var arr = new T[len];
                copy(items, start, start + len, arr, 0);
                return arr;
            }

            // Copy source to destination in s1 to s2 range at offset d1
            static void copy(IList<T> s, int s1, int s2, IList<T> d, int d1)
            {
                while (s1 < s2)
                {
                    d[d1++] = s[s1++];
                }
            }
        }

        private static bool CheckAlreadyInOrder<T>(IList<T> items, Comparison<T> comparison)
        {
            for (var i = 1; i < items.Count; i++)
            {
                var a = items[i - 1];
                var b = items[i + 0];

                if (comparison(a, b) > 0)
                {
                    // Was not in order
                    return false;
                }
            }

            // All elements are in order
            return true;
        }
    }
}
