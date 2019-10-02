﻿using System;
using System.Linq;

using NUnit.Framework;

namespace Heirloom.Collections.Testing
{
    public abstract class CollectionTests
    {
        public Random Random { get; private set; }

        [OneTimeSetUp]
        public void InitializeTest()
        {
            Random = new Random(0xC0FFEE);
        }

        /// <summary>
        /// Constructs an array of n elements in the range of 0 to n, then randomized.
        /// </summary>
        public int[] CreateRandomIntegerArray(int n)
        {
            var arr = CreateOrderedIntegerArray(n);
            Randomize(arr);
            return arr;
        }

        /// <summary>
        /// Constructs an array of n elements in the range of 0 to n.
        /// </summary>
        public int[] CreateOrderedIntegerArray(int n)
        {
            return Enumerable.Range(0, n).ToArray();
        }

        /// <summary>
        /// Constructs an array of n elements in the range of 0 to n, then randomized.
        /// </summary>
        public SortableObject[] CreateRandomSortableArray(int n)
        {
            var arr = CreateOrderedSortableArray(n);
            Randomize(arr);
            return arr;
        }

        /// <summary>
        /// Constructs an array of n elements in the range of 0 to n.
        /// </summary>
        public SortableObject[] CreateOrderedSortableArray(int n)
        {
            return Enumerable.Range(0, n)
                .Select(x => new SortableObject(x))
                .ToArray();
        }

        /// <summary>
        /// Randomly swaps each element once.
        /// </summary>
        public void Randomize<T>(T[] arr)
        {
            if (arr == null) { throw new ArgumentNullException(nameof(arr)); }

            // 
            for (var i = 0; i < arr.Length; i++)
            {
                var r = Random.Next(0, arr.Length);
                Swap(ref arr[r], ref arr[i]);
            }
        }

        public sealed class SortableObject : IComparable<SortableObject>
        {
            public SortableObject(int number)
            {
                Number = number;
            }

            public int Number { get; set; }

            public int CompareTo(SortableObject other)
            {
                return Number.CompareTo(other.Number);
            }
        }

        public static void Swap<T>(ref T a, ref T b)
        {
            var t = a;
            a = b;
            b = t;
        }

    }
}