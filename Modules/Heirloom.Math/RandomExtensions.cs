using System;
using System.Collections.Generic;
using System.Runtime.CompilerServices;

namespace Heirloom.Math
{
    /// <summary>
    /// Provides extension methods for <see cref="Random"/> and other related random operations.
    /// </summary>
    public static class RandomExtensions
    {
        #region Next Float / Double

        /// <summary> 
        /// Returns a random floating-point number that is greater than or equal to 0.0, and less than 1.0.
        /// </summary>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static float NextFloat(this Random @this)
        {
            return (float) @this.NextDouble();
        }

        /// <summary> 
        /// Returns a random floating-point number that is within the specified range.
        /// </summary>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static float NextFloat(this Random @this, float min, float max)
        {
            return min + (max - min) * @this.NextFloat();
        }

        /// <summary> 
        /// Returns a random floating-point number that is within the specified range.
        /// </summary>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static double NextDouble(this Random @this, double min, double max)
        {
            return min + (max - min) * @this.NextDouble();
        }

        #endregion

        #region Range / IntRange

        /// <summary> 
        /// Returns a random floating-point number that is within the specified range.
        /// </summary>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static float NextFloat(this Random @this, Range range)
        {
            return NextFloat(@this, range.Min, range.Max);
        }

        /// <summary> 
        /// Returns a random integer number that is within the specified range.
        /// </summary>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static float Next(this Random @this, IntRange range)
        {
            return @this.Next(range.Min, range.Max);
        }

        #endregion

        #region Random Vectors

        /// <summary>
        /// Returns a random point within a unit circle.
        /// </summary>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Vector NextVectorDisk(this Random @this)
        {
            return NextVectorDisk(@this, 1F);
        }

        /// <summary>
        /// Returns a random point within a circle.
        /// </summary>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Vector NextVectorDisk(this Random @this, float r)
        {
            // poisson distribution?
            var theta = Calc.TwoPi * NextFloat(@this);
            var rho = r * Calc.Sqrt(NextFloat(@this));

            // 
            var x = rho * Calc.Cos(theta);
            var y = rho * Calc.Sin(theta);

            return new Vector(x, y);
        }

        /// <summary>
        /// Returns a random unit vector (point on edge of unit circle).
        /// </summary>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Vector NextUnitVector(this Random @this)
        {
            var theta = Calc.TwoPi * NextFloat(@this);
            return Vector.FromAngle(theta);
        }

        /// <summary>
        /// Returns a random point within the specified rectangular domain.
        /// </summary>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Vector NextVector(this Random @this, in Rectangle domain)
        {
            var x = NextFloat(@this, domain.Left, domain.Right);
            var y = NextFloat(@this, domain.Top, domain.Bottom);
            return new Vector(x, y);
        }

        #endregion

        #region Chance and Choice

        /// <summary>
        /// Randomly return true for occurrences with the specified probability.
        /// </summary>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static bool Chance(this Random @this, float probability)
        {
            return @this.NextDouble() < probability;
        }

        /// <summary>
        /// Randomly select one of the specified items.
        /// </summary>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static T Choose<T>(this Random @this, IReadOnlyList<T> items)
        {
            var index = @this.Next(0, items.Count);
            return items[index];
        }

        /// <summary>
        /// Randomly select one of the specified items.
        /// </summary>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static T Choose<T>(this Random @this, params T[] items)
        {
            return Choose(@this, (IReadOnlyList<T>) items);
        }

        #endregion

        #region Shuffle

        /// <summary>
        /// Shuffles all elements in the list randomly.
        /// </summary>
        public static void Shuffle<T>(this Random @this, IList<T> items)
        {
            items.Shuffle(@this);
        }

        /// <summary>
        /// Shuffles all elements in the list randomly.
        /// </summary>
        public static void Shuffle<T>(this IList<T> @this, Random random)
        {
            for (var i = 0; i < @this.Count; i++)
            {
                var r = random.Next(@this.Count);

                // Swap
                var t = @this[r];
                @this[r] = @this[i];
                @this[i] = t;
            }
        }

        #endregion
    }
}
