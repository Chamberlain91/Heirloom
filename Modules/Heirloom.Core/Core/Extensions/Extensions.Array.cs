using System;
using System.Collections.Generic;

namespace Heirloom
{
    /// <summary>
    /// Provides extension methods various types within Heirloom.
    /// </summary>
    /// <category>Extension Methods</category>
    public static partial class Extensions
    {
        /// <summary>
        /// Scrambles the items in a list into a randomized order.
        /// </summary>
        public static void Randomize<T>(this IList<T> items)
        {
            Randomize(items, Calc.Random);
        }

        /// <summary>
        /// Scrambles the items in a list into a randomized order.
        /// </summary>
        public static void Randomize<T>(this IList<T> items, Random random)
        {
            for (var i = 0; i < items.Count; i++)
            {
                var r = random.Next(0, items.Count);
                Calc.Swap(items, i, r);
            }
        }
    }
}
