using System;

namespace Heirloom.Sound.Effects
{
    /// <summary>
    /// An audio effect that implements a low pass filter.
    /// </summary>
    public class LowPassFilter : AudioEffect
    {
        private readonly float[] _out;
        private float _cutoff;

        public LowPassFilter(float cutoff)
        {
            _out = new float[AudioContext.Channels];
            Frequency = cutoff;
        }

        /// <summary>
        /// Gets or sets the filter cutoff in hertz.
        /// </summary>
        public float Frequency
        {
            get => _cutoff;

            set
            {
                if (value <= 0) { throw new ArgumentOutOfRangeException($"Filter cutoff must be greater than zero."); }
                _cutoff = value;
            }
        }

        public override float Process(float sample, int channel)
        {
            // Compute alpha
            var dt = AudioContext.InverseSampleRate;
            var rc = 1F / (2 * MathF.PI * Frequency);
            var alpha = dt / (rc + dt);

            // Compute low pass
            _out[channel] += alpha * (sample - _out[channel]);

            // Return computed sample
            return _out[channel];
        }
    }
}
