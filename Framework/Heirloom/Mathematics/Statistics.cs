using System;
using System.Collections.Generic;

namespace Heirloom.Mathematics
{
    /// <summary>
    /// Represents statistics of some data.
    /// </summary>
    /// <category>Mathematics</category>
    public readonly struct Statistics : IEquatable<Statistics>, IFormattable
    {
        /// <summary>
        /// The average value. Also known as the mean or expected value.
        /// </summary>
        public readonly float Mean;

        /// <summary>
        /// The variance value.
        /// </summary>
        public readonly float Variance;

        /// <summary>
        /// The standard deviation.
        /// </summary>
        public readonly float Deviation;

        /// <summary>
        /// The range of values.
        /// </summary>
        public readonly Range Range;

        // todo: quantiles / median, etc?

        /// <summary>
        /// Constructs a new statistics instance.
        /// </summary>
        /// <param name="average">The average or mean value.</param>
        /// <param name="variance">The variance.</param>
        /// <param name="range">The extents of the data.</param>
        public Statistics(float average, float variance, Range range)
        {
            Mean = average;
            Variance = variance;
            Range = range;

            Deviation = Calc.Sqrt(variance);
        }

        /// <summary>
        /// Rescales some input value based on the mean and standard deviation to the target domain.
        /// </summary>
        /// <param name="x"></param>
        /// <param name="min"></param>
        /// <param name="max"></param>
        /// <param name="numDev"></param>
        /// <returns></returns>
        public float Rescale(float x, float min, float max, int numDev = 1)
        {
            if (numDev <= 0) { throw new ArgumentException("Must be greater than zero.", nameof(numDev)); }
            var stdMin = Calc.Max(Range.Min, Mean - Deviation * numDev);
            var stdMax = Calc.Min(Range.Max, Mean + Deviation * numDev);
            return Calc.Rescale(x, stdMin, stdMax, min, max);
        }

        /// <summary>
        /// Converts this <see cref="Statistics"/> into string representation.
        /// </summary>
        public override string ToString()
        {
            return $"{Mean} ± {Deviation}";
        }

        /// <summary>
        /// Converts this <see cref="Statistics"/> into string representation.
        /// </summary>
        public string ToString(string format, IFormatProvider formatProvider)
        {
            var _Average = Mean.ToString(format, formatProvider);
            var _Deviation = Deviation.ToString(format, formatProvider);
            return $"{_Average} ± {_Deviation}";
        }

        #region Equality

        /// <inheritdoc/>
        public override bool Equals(object obj)
        {
            return obj is Statistics statistics && Equals(statistics);
        }

        /// <inheritdoc/>
        public bool Equals(Statistics other)
        {
            return Calc.NearEquals(Mean, other.Mean) &&
                   Calc.NearEquals(Variance, other.Variance) &&
                   Calc.NearEquals(Deviation, other.Deviation);
        }

        /// <inheritdoc/>
        public override int GetHashCode()
        {
            return HashCode.Combine(Mean, Variance, Deviation);
        }

        /// <inheritdoc/>
        public static bool operator ==(Statistics left, Statistics right)
        {
            return left.Equals(right);
        }

        /// <inheritdoc/>
        public static bool operator !=(Statistics left, Statistics right)
        {
            return !(left == right);
        }

        #endregion

        #region Compute (Static)

        /// <summary>
        /// Computes new statistics from a collection of integers.
        /// </summary>
        public static Statistics Compute(IEnumerable<int> values)
        {
            var sum = 0F;
            var squareSum = 0F;
            var count = 0;

            var range = Range.Indeterminate;

            foreach (var v in values)
            {
                sum += v;
                squareSum += v * v;
                range.Include(v);

                count++;
            }

            return Compute(sum, squareSum, range, count);
        }

        /// <summary>
        /// Computes new statistics from a collection of numbers.
        /// </summary>
        public static Statistics Compute(IEnumerable<float> values)
        {
            var sum = 0F;
            var squareSum = 0F;
            var count = 0;

            var range = Range.Indeterminate;

            foreach (var v in values)
            {
                sum += v;
                squareSum += v * v;
                range.Include(v);
                count++;
            }

            return Compute(sum, squareSum, range, count);
        }

        /// <summary>
        /// Computes new statistics from a sum, squared sum, extents and count.
        /// </summary>
        public static Statistics Compute(float sum, float squareSum, Range range, int count)
        {
            if (count != 0)
            {
                // Compute mean
                var average = sum / count;

                // Compute variance
                var variance = (squareSum / count) - (average * average);

                // Return statistics
                return new Statistics(average, variance, range);
            }
            else
            {
                // throw new InvalidOperationException("Unable to compute statistics of zero samples");
                return default;
            }
        }

        #endregion
    }
}
