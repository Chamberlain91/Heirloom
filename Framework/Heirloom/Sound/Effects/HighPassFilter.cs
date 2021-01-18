using System;

namespace Heirloom.Sound
{
    /// <summary>
    /// An audio effect that implements a high pass filter.
    /// </summary>
    /// <category>Effects and Filters</category>
    public class HighPassFilter : AudioEffect
    {
        private readonly float[] _out;
        private readonly float[] _mem;

        private float _frequency;

        /// <summary>
        /// Constructs a new instance of <see cref="HighPassFilter"/>.
        /// </summary>
        /// <param name="cutoff">The frequency cutoff in hertz.</param>
        public HighPassFilter(float cutoff)
        {
            Frequency = cutoff;

            _out = new float[AudioBackend.Channels];
            _mem = new float[AudioBackend.Channels];
        }

        /// <summary>
        /// Gets or sets the frequency cutoff frequency in hertz.
        /// </summary>
        /// <value>This value ranges from 0.0 to <see cref="AudioBackend.SampleRate"/>.</value>
        public float Frequency
        {
            get => _frequency;

            set
            {
                if (value <= 0) { throw new ArgumentOutOfRangeException($"Filter cutoff frequency must be greater than zero."); }
                _frequency = value;
            }
        }

        /// <inheritdoc/>
        public override float Process(float sample, int channel)
        {
            // Compute alpha
            var dt = AudioBackend.InverseSampleRate;
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
