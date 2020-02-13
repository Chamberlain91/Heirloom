using System.Diagnostics;

namespace Heirloom.Drawing
{
    internal sealed class FrequencyCounter
    {
        private static readonly double _ticksToSeconds = 1.0 / Stopwatch.Frequency;

        private readonly Stopwatch _stopwatch;
        private readonly float _samplingDuration;

        private double _time;
        private int _counter;

        public float Average { get; private set; }

        public int Samples { get; private set; }

        public FrequencyCounter(float samplingDuration)
        {
            _samplingDuration = samplingDuration;
            _stopwatch = new Stopwatch();
            _stopwatch.Start();
        }

        public void Tick()
        {
            // Get elapsed time
            var delta = _stopwatch.ElapsedTicks * _ticksToSeconds;
            _stopwatch.Restart();

            // Tick
            _time += delta;
            _counter++;

            // Elapsed time
            if (_time >= _samplingDuration)
            {
                // Compute average samples
                Average = (float) (_counter / _time);
                Samples = _counter;

                // Reset for next sampling period
                _time = (_time - _samplingDuration) % _samplingDuration;
                _counter = 0;
            }
        }
    }
}
