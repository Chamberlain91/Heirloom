using System;
using System.Diagnostics;

namespace Heirloom.Runtime
{
    public static class Time
    {
        private static Stopwatch _timer;

        private static RateCounter _fps;

        internal static void Initialize()
        {
            FixedDelta = 1 / 60F;

            _timer = Stopwatch.StartNew();
            _fps = new RateCounter();
        }

        static internal void BeginFrame()
        {
            var now = Now;
            Delta = now - Frame;
            Frame = now;

            // 
            _fps.Tick(Delta);
        }

        /// <summary>
        /// Time in seconds since start.
        /// </summary>
        public static float Now => _timer.ElapsedTicks / (float) Stopwatch.Frequency;

        /// <summary>
        /// Time in seconds since start at the beginning of the current frame.
        /// </summary>
        public static float Frame { get; internal set; }

        /// <summary>
        /// Time in seconds since the last frame.
        /// </summary>
        public static float Delta { get; internal set; }

        /// <summary>
        /// Time in seconds since the last fixed frame.
        /// </summary>
        public static float FixedDelta { get; internal set; }

        /// <summary>
        /// Number of frames per second (inverse delta time).
        /// </summary>
        public static float InstantFrameRate => 1F / Delta;

        /// <summary>
        /// Number of frames per second (counts per second).
        /// </summary>
        public static float FrameRate => _fps.Rate;

        #region Convert Time

        /// <summary>
        /// Converts 1 second of time to another time unit.
        /// </summary> 
        /// <param name="unit"> Some representation of units of time ( return ). </param>
        /// <returns> Some conversion from <paramref name="source"/> to <paramref name="unit"/> units. </returns>
        public static double Convert(TimeUnit unit)
        {
            return Convert(1.0, TimeUnit.Seconds, unit);
        }

        /// <summary>
        /// Converts 1 source unit of time to another.
        /// </summary> 
        /// <param name="source"> Some representation of units of time ( input ). </param>
        /// <param name="target"> Some representation of units of time ( return ). </param>
        /// <returns> Some conversion from <paramref name="source"/> to <paramref name="target"/> units. </returns>
        public static double Convert(TimeUnit source, TimeUnit target)
        {
            return Convert(1.0, source, target);
        }

        /// <summary>
        /// Converts time from unit to another.
        /// </summary>
        /// <param name="value"> Some value of time in <paramref name="source"/> units. </param>
        /// <param name="source"> Some representation of units of time ( input ). </param>
        /// <param name="target"> Some representation of units of time ( return ). </param>
        /// <returns> Some conversion from <paramref name="source"/> to <paramref name="target"/> units. </returns>
        public static double Convert(double value, TimeUnit source, TimeUnit target)
        {
            switch (source)
            {
                case TimeUnit.Seconds:
                    switch (target)
                    {
                        case TimeUnit.Seconds: return value;
                        case TimeUnit.Milliseconds: return value * 1e+3;
                        case TimeUnit.Microseconds: return value * 1e+6;
                        case TimeUnit.Nanoseconds: return value * 1e+9;
                    }
                    break;

                case TimeUnit.Milliseconds:
                    switch (target)
                    {
                        case TimeUnit.Seconds: return value / 1e+3;
                        case TimeUnit.Milliseconds: return value;
                        case TimeUnit.Microseconds: return value * 1e+3;
                        case TimeUnit.Nanoseconds: return value * 1e+6;
                    }
                    break;

                case TimeUnit.Microseconds:
                    switch (target)
                    {
                        case TimeUnit.Seconds: return value / 1e+6;
                        case TimeUnit.Milliseconds: return value / 1e+3;
                        case TimeUnit.Microseconds: return value;
                        case TimeUnit.Nanoseconds: return value * 1e+3;
                    }
                    break;

                case TimeUnit.Nanoseconds:
                    switch (target)
                    {
                        case TimeUnit.Seconds: return value / 1e+9;
                        case TimeUnit.Milliseconds: return value / 1e+6;
                        case TimeUnit.Microseconds: return value / 1e+3;
                        case TimeUnit.Nanoseconds: return value;
                    }
                    break;
            }

            throw new InvalidOperationException($"Invalid enum values must have been passed to {nameof(Convert)}.");
        }

        #endregion

        private class RateCounter
        {
            private readonly float _duration;

            private float _time;
            private int _count;

            public RateCounter(float duration = 1F)
            {
                _duration = duration;
            }

            public float Rate { get; private set; }

            public void Tick(float delta)
            {
                // Advance time
                _time += delta;
                _count++;

                if (_time > _duration)
                {
                    Rate = _count / _duration;
                    _count = 0;

                    _time -= _duration;
                }
            }
        }
    }
}
