using System;
using System.Collections.Generic;
using System.Linq;

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

        #region Find Minimal and Maximal

        /// <summary>
        /// Finds the index of the minimal element based on a scoring function.
        /// </summary>
        /// <param name="elements">Some elements.</param>
        /// <param name="getScore">A function that ranks an element.</param>
        public static int FindMinimalIndex<T, N>(this IReadOnlyList<T> elements, Func<T, N> getScore) where N : IComparable<N>
        {
            if (elements is null) { throw new ArgumentNullException(nameof(elements)); }
            if (getScore is null) { throw new ArgumentNullException(nameof(getScore)); }

            if (elements.Count > 0)
            {
                var first = elements[0];

                // Get the score of the first element
                var minScore = getScore(first);
                var minIndex = 0;

                // For each element (except the first)
                for (var i = 1; i < elements.Count; i++)
                {
                    var element = elements[i];

                    // Get the score for this element
                    var score = getScore(element);

                    // If this is the maximal score thus far, record it.
                    if (score.CompareTo(minScore) < 0)
                    {
                        minIndex = i;
                        minScore = score;
                    }
                }

                return minIndex;
            }
            else
            {
                throw new InvalidOperationException("No elements in sequence.");
            }
        }

        /// <summary>
        /// Finds the index of the minimal element based on a scoring function.
        /// </summary>
        /// <param name="elements">Some elements.</param>
        /// <param name="getScore">A function that ranks an element.</param>
        public static int FindMaximalIndex<T, N>(this IReadOnlyList<T> elements, Func<T, N> getScore) where N : IComparable<N>
        {
            if (elements is null) { throw new ArgumentNullException(nameof(elements)); }
            if (getScore is null) { throw new ArgumentNullException(nameof(getScore)); }

            if (elements.Count > 0)
            {
                var first = elements[0];

                // Get the score of the first element
                var maxScore = getScore(first);
                var maxIndex = 0;

                // For each element (except the first)
                for (var i = 1; i < elements.Count; i++)
                {
                    var element = elements[i];

                    // Get the score for this element
                    var score = getScore(element);

                    // If this is the maximal score thus far, record it.
                    if (score.CompareTo(maxScore) > 0)
                    {
                        maxIndex = i;
                        maxScore = score;
                    }
                }

                return maxIndex;
            }
            else
            {
                throw new InvalidOperationException("No elements in sequence.");
            }
        }

        /// <summary>
        /// Finds the minimal element based on a scoring function.
        /// </summary>
        /// <param name="elements">Some elements.</param>
        /// <param name="getScore">A function that ranks an element.</param>
        public static T FindMinimal<T, N>(this IEnumerable<T> elements, Func<T, N> getScore) where N : IComparable<N>
        {
            if (elements is null) { throw new ArgumentNullException(nameof(elements)); }
            if (getScore is null) { throw new ArgumentNullException(nameof(getScore)); }

            if (elements.Any())
            {
                var first = elements.First();

                // Get the score of the first element
                var maxScore = getScore(first);
                var maxElement = first;

                // For each element (except the first)
                foreach (var element in elements.Skip(1))
                {
                    // Get the score for this element
                    var score = getScore(element);

                    // If this is the maximal score thus far, record it.
                    if (score.CompareTo(maxScore) < 0)
                    {
                        maxElement = element;
                        maxScore = score;
                    }
                }

                return maxElement;
            }
            else
            {
                throw new InvalidOperationException("No elements in sequence.");
            }
        }

        /// <summary>
        /// Finds the maximal element based on a scoring function.
        /// </summary>
        /// <param name="elements">Some elements.</param>
        /// <param name="getScore">A function that ranks an element.</param>
        public static T FindMaximal<T, N>(this IEnumerable<T> elements, Func<T, N> getScore) where N : IComparable<N>
        {
            if (elements is null) { throw new ArgumentNullException(nameof(elements)); }
            if (getScore is null) { throw new ArgumentNullException(nameof(getScore)); }

            if (elements.Any())
            {
                var first = elements.First();

                // Get the score of the first element
                var maxScore = getScore(first);
                var maxElement = first;

                // For each element (except the first)
                foreach (var element in elements.Skip(1))
                {
                    // Get the score for this element
                    var score = getScore(element);

                    // If this is the maximal score thus far, record it.
                    if (score.CompareTo(maxScore) > 0)
                    {
                        maxElement = element;
                        maxScore = score;
                    }
                }

                return maxElement;
            }
            else
            {
                throw new InvalidOperationException("No elements in sequence.");
            }
        }

        #endregion
    }
}
