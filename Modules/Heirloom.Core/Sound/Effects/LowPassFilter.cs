using System;

namespace Heirloom.Sound
{
    /// <summary>
    /// An audio effect that implements a low pass filter.
    /// </summary>
    public class LowPassFilter : AudioEffect
    {
        private readonly float[] _out;
        private float _cutoff;

        /// <summary>
        /// Constructs a new instance of <see cref="LowPassFilter"/>.
        /// </summary>
        /// <param name="cutoff">The frequency cutoff in hertz.</param>
        public LowPassFilter(float cutoff)
        {
            _out = new float[AudioAdapter.Channels];
            Frequency = cutoff;
        }

        /// <summary>
        /// Gets or sets the frequency cutoff in hertz.
        /// </summary>
        /// <value>This value ranges from 0.0 to <see cref="AudioAdapter.SampleRate"/>.</value>
        public float Frequency
        {
            get => _cutoff;

            set
            {
                if (value <= 0) { throw new ArgumentOutOfRangeException($"Filter cutoff must be greater than zero."); }
                _cutoff = value;
            }
        }

        /// <inheritdoc/>
        public override float Process(float sample, int channel)
        {
            // Compute alpha
            var dt = AudioAdapter.InverseSampleRate;
            var rc = 1F / (2 * MathF.PI * Frequency);
            var alpha = dt / (rc + dt);

            // Compute low pass
            _out[channel] += alpha * (sample - _out[channel]);

            // Return computed sample
            return _out[channel];
        }
    }
}
