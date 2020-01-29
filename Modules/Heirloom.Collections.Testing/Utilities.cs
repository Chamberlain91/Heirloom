using System;
using System.Linq;

using Heirloom.Math;

namespace Heirloom.Collections.Testing
{
    public static class Utilities
    {
        #region Create Integers

        /// <summary>
        /// Constructs an array of n elements in the range of 0 to n.
        /// </summary>
        public static int[] CreateOrderedIntegerArray(int n)
        {
            return Enumerable.Range(0, n).ToArray();
        }

        /// <summary>
        /// Constructs an array of n elements in the range of 0 to n.
        /// </summary>
        public static int[] CreateReverseIntegerArray(int n)
        {
            return Enumerable.Range(0, n).Reverse().ToArray();
        }

        /// <summary>
        /// Constructs an array of n elements in the range of 0 to n, then is randomized.
        /// </summary>
        public static int[] CreateRandomIntegerArray(int n)
        {
            var arr = CreateOrderedIntegerArray(n);
            Randomize(arr);
            return arr;
        }

        /// <summary>
        /// Constructs an array of n elements in the range of 0 to n, then is partially randomized.
        /// </summary>
        public static int[] CreatePartiallyRandomIntegerArray(int n)
        {
            var arr = CreateOrderedIntegerArray(n);
            PartiallyRandomize(arr, (int) Calc.Max(1, n * 0.1F));
            return arr;
        }

        #endregion

        #region Create Sortable Objects

        /// <summary>
        /// Constructs an array of n elements in the range of 0 to n.
        /// </summary>
        public static SortableObject[] CreateOrderedSortableArray(int n)
        {
            return Enumerable.Range(0, n)
                .Select(x => new SortableObject(x))
                .ToArray();
        }

        /// <summary>
        /// Constructs an array of n elements in the range of 0 to n.
        /// </summary>
        public static SortableObject[] CreateReverseSortableArray(int n)
        {
            return Enumerable.Range(0, n)
                .Select(x => new SortableObject(x))
                .Reverse().ToArray();
        }

        /// <summary>
        /// Constructs an array of n elements in the range of 0 to n, then is randomized.
        /// </summary>
        public static SortableObject[] CreateRandomSortableArray(int n)
        {
            var arr = CreateOrderedSortableArray(n);
            Randomize(arr);
            return arr;
        }

        /// <summary>
        /// Constructs an array of n elements in the range of 0 to n, then is partially randomized.
        /// </summary>
        public static SortableObject[] CreatePartiallyRandomSortableArray(int n)
        {
            var arr = CreateOrderedSortableArray(n);
            PartiallyRandomize(arr, (int) Calc.Max(1, n * 0.1F));
            return arr;
        }

        #endregion

        #region Randomize

        /// <summary>
        /// Randomly swaps each element once.
        /// </summary>
        public static void Randomize<T>(T[] arr)
        {
            if (arr == null) { throw new ArgumentNullException(nameof(arr)); }

            var rnd = new Random(0xC0FFEE);

            // 
            for (var i = 0; i < arr.Length; i++)
            {
                var r = rnd.Next(0, arr.Length);
                Swap(ref arr[r], ref arr[i]);
            }
        }

        /// <summary>
        /// Randomly swaps n elements.
        /// </summary>
        public static void PartiallyRandomize<T>(T[] arr, int n)
        {
            if (arr == null) { throw new ArgumentNullException(nameof(arr)); }

            var rnd = new Random(0xC0FFEE);

            // 
            while (n-- > 0)
            {
                var a = rnd.Next(0, arr.Length);
                var b = rnd.Next(0, arr.Length);
                Swap(ref arr[b], ref arr[a]);
            }
        }

        #endregion

        public static void Swap<T>(ref T a, ref T b)
        {
            var t = a;
            a = b;
            b = t;
        }
    }
}
