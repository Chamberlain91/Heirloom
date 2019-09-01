using System;
using System.Collections.Generic;
using System.Runtime.CompilerServices;

namespace Heirloom.Math
{
    public static class RandomExtensions
    {
        #region Next Float / Double

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static float NextFloat(this Random @this)
        {
            return (float) @this.NextDouble();
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static float NextFloat(this Random @this, float min, float max)
        {
            return min + (max - min) * @this.NextFloat();
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static double NextDouble(this Random @this, double min, double max)
        {
            return min + (max - min) * @this.NextDouble();
        }

        #endregion

        #region Random Vectors

        /// <summary>
        /// Gets a random point within a unit circle.
        /// </summary>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Vector NextVectorDisk(this Random @this)
        {
            return NextVectorDisk(@this, 1F);
        }

        /// <summary>
        /// Gets a random point within a circle.
        /// </summary>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Vector NextVectorDisk(this Random @this, float r)
        {
            // poisson distribution?
            var theta = Calc.TwoPi * NextFloat(@this);
            var rho = r * Calc.Sqrt(NextFloat(@this));

            // 
            return new Vector(
                rho * Calc.Cos(theta),
                rho * Calc.Sin(theta));
        }

        /// <summary>
        /// Gets a random unit vector (point on edge of unit circle).
        /// </summary>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Vector NextUnitVector(this Random @this)
        {
            var theta = Calc.TwoPi * NextFloat(@this);
            return Vector.FromAngle(theta);
        }

        /// <summary>
        /// Gets a random point within the specified rectangular domain.
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
        /// Randomly return true for occurrences with specified probability.
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
    }
}
