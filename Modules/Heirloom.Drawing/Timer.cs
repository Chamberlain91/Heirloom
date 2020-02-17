using System.Diagnostics;

namespace Heirloom.Drawing
{
    internal sealed class Timer
    {
        private static readonly double _ticksToSeconds = 1.0 / Stopwatch.Frequency;

        private readonly Stopwatch _stopwatch;

        private readonly float _duration;
        private double _time;

        /// <summary>
        /// Constructs a new timer.
        /// </summary>
        public Timer(float duration)
        {
            _duration = duration;

            // 
            _stopwatch = new Stopwatch();
            _stopwatch.Start();
        }

        public float Delta { get; private set; }

        /// <summary>
        /// Checks if enough time has passed, returning true when the duration has been exceeded.
        /// </summary>
        public bool Tick(out float elapsed)
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
