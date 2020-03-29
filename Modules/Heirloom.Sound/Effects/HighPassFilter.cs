using System;

namespace Heirloom.Sound.Effects
{
    /// <summary>
    /// An audio effect that implements a high pass filter.
    /// </summary>
    public class HighPassFilter : AudioEffect
    {
        private readonly float[] _out;
        private readonly float[] _mem;

        private float _frequency;

        public HighPassFilter(float cutoff)
        {
            Frequency = cutoff;

            _out = new float[AudioContext.Channels];
            _mem = new float[AudioContext.Channels];
        }

        /// <summary>
        /// Gets or sets the filter cutoff frequency in hertz.
        /// </summary>
        public float Frequency
        {
            get => _frequency;

            set
            {
                if (value <= 0) { throw new ArgumentOutOfRangeException($"Filter cutoff frequency must be greater than zero."); }
                _frequency = value;
            }
        }

        public override float Process(float sample, int channel)
        {
            // Compute alpha
            var dt = AudioContext.InverseSampleRate;
            var rc = 1F / (2 * MathF.PI * Frequency);
            var alpha = rc / (rc + dt);

            // Compute low pass
            _out[channel] = alpha * (_out[channel] + sample - _mem[channel]);

            // Store raw sample frame as memory for next update
            _mem[channel] = sample;

            // Return computed sample
            return _out[channel];
        }
    }
}
