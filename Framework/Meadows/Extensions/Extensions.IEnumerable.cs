using Meadows.Drawing;
using Meadows.Mathematics;

using System;
using System.Collections.Generic;
using System.Linq;

namespace Meadows
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

        #region Sum (Vector, Color)

        /// <summary>
        /// Computes the sum vector of a sequence of vectors.
        /// </summary>
        public static Vector Sum(this IEnumerable<Vector> vectors)
        {
            var sum = Vector.Zero;

            foreach (var v in vectors)
            {
                sum += v;
            }

            return sum;
        }

        /// <summary>
        /// Computes the sum (additive color) of a sequence of colors.
        /// </summary>
        public static Color Sum(this IEnumerable<Color> colors)
        {
            var sum = Color.Transparent;

            foreach (var color in colors)
            {
                sum += color;
            }

            return sum;
        }

        #endregion

        #region Average (Vector, Color)

        /// <summary>
        /// Computes the average vector of a sequence of vectors.
        /// </summary>
        public static Vector Average(this IEnumerable<Vector> vectors)
        {
            var sum = Vector.Zero;
            var count = 0;

            foreach (var vec in vectors)
            {
                sum += vec;
                count++;
            }

            if (count > 0) { return sum / count; }
            else { return sum; }
        }

        /// <summary>
        /// Computes the average color of a sequence of colors.
        /// </summary>
        public static Color Average(this IEnumerable<Color> colors)
        {
            var sum = Color.Transparent;
            var count = 0;

            foreach (var color in colors)
            {
                sum += color;
                count++;
            }

            if (count > 0) { return sum / count; }
            else { return sum; }
        }

        #endregion

        #region Min / Max (Vector)

        /// <summary>
        /// Computes the maximum component-wise vector of a sequence of vectors.
        /// </summary>
        public static Vector Max(this IEnumerable<Vector> vectors)
        {
            var max = Vector.One * float.NegativeInfinity;

            foreach (var vec in vectors)
            {
                max = Vector.Max(max, vec);
            }

            return max;
        }

        /// <summary>
        /// Computes the minimum component-wise vector of a sequence of vectors.
        /// </summary>
        public static Vector Min(this IEnumerable<Vector> vectors)
        {
            var min = Vector.One * float.PositiveInfinity;

            foreach (var vec in vectors)
            {
                min = Vector.Min(min, vec);
            }

            return min;
        }

        #endregion
    }
}
