using System.Diagnostics;

namespace Heirloom.Runtime
{
    public static class StopwatchExtensions
    {
        /// <summary>
        /// Converts ticks to seconds
        /// </summary>
        public static double ElapsedSeconds(this Stopwatch watch)
        {
            return watch.ElapsedTicks / (double) Stopwatch.Frequency;
        }

        /// <summary>
        /// Converts ticks to nanoseconds
        /// </summary>
        public static long ElapsedNanoseconds(this Stopwatch watch)
        {
            return (long) Time.Convert(ElapsedSeconds(watch), TimeUnit.Seconds, TimeUnit.Nanoseconds);
        }

        /// <summary>
        /// Converts ticks to microseconds
        /// </summary>
        public static long ElapsedMicroseconds(this Stopwatch watch)
        {
            return (long) Time.Convert(ElapsedSeconds(watch), TimeUnit.Seconds, TimeUnit.Nanoseconds);
        }
    }
}
