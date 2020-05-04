using System;
using System.Collections.Generic;

namespace Heirloom
{
    public static partial class Extensions
    {
        public static void Randomize<T>(this IList<T> items)
        {
            Randomize(items, Calc.Random);
        }

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
