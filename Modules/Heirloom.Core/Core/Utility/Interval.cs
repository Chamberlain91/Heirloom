using System.Diagnostics;

namespace Heirloom
{
    /// <summary>
    /// A utility object to check if an interval of time has occured.
    /// </summary>
    public sealed class Interval
    {
        private static readonly double _ticksToSeconds = 1.0 / Stopwatch.Frequency;

        private readonly Stopwatch _stopwatch;

        private readonly float _duration;
        private double _time;

        /// <summary>
        /// Constructs a new interval timer.
        /// </summary>
        public Interval(float duration)
        {
            _duration = duration;

            // 
            _stopwatch = new Stopwatch();
            _stopwatch.Start();
        }

        /// <summary>
        /// The time since when <see cref="Check(out float)"/> or <see cref="Check()"/> was last called.
        /// </summary>
        public float Delta { get; private set; }

        /// <summary>
        /// Returns true when enough time has elapsed.
        /// </summary>
        public bool Check()
        {
            return Check(out _);
        }

        /// <summary>
        /// Returns true when enough time has elapsed. Outputs the elasped time in seconds.
        /// </summary>
        public bool Check(out float elapsed)
        {
            // Get elapsed time
            var delta = _stopwatch.ElapsedTicks * _ticksToSeconds;
            _stopwatch.Restart();

            // 
            Delta = (float) delta;

            // Accumulate time
            _time += delta;

            // Elapsed time
            if (_time >= _duration)
            {
                // Emit elapsed time
                elapsed = (float) _time;

                // Reset for next sampling period
                // Using modulus to keep timing within duration cycle.
                _time = (_time - _duration) % _duration;

                return true;
            }
            else
            {
                // Not enough time has elapsed
                elapsed = default;
                return false;
            }
        }
    }
}
