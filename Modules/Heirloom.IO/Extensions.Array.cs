using System;
using System.Collections.Generic;

namespace Heirloom.IO
{
    /// <summary>
    /// Provides extensions to array, enumerable or lists.
    /// </summary>
    public static class ArrayExtensions
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
    }
}
