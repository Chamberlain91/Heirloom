using System;
using System.Collections.Generic;

namespace Heirloom.UI
{
    public static class Extensions
    {
        public static int FindNext<T>(this IList<T> @this, int startIndex, Predicate<T> predicate)
        {
            // Validate arguments
            if (@this is null) { throw new ArgumentNullException(nameof(@this)); }
            if (predicate is null) { throw new ArgumentNullException(nameof(predicate)); }
            if (startIndex < 0) { throw new ArgumentException($"{nameof(startIndex)} must be non-negative", nameof(startIndex)); }
            if (startIndex >= @this.Count) { throw new ArgumentException($"{nameof(startIndex)} must be less than length.", nameof(startIndex)); }

            for (var i = startIndex + 1; i < @this.Count; i++)
            {
                if (predicate(@this[i]))
                {
                    return i;
                }
            }

            return -1;
        }
    }
}
