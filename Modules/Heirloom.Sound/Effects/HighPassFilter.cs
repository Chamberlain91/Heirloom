using System;

namespace Heirloom.Sound.Effects
{
    /// <summary>
    /// An audio effect that implements a high pass filter.
    /// </summary>
    public class HighPassFilter : AudioEffect
    {
        private readonly float[] _pre;
        private readonly float[] _mem;
        private float[] _out;

        private float _frequency;

        public HighPassFilter(float cutoff)
        {
            _pre = new float[AudioContext.Channels];
            _mem = new float[AudioContext.Channels];

            _out = Array.Empty<float>();

            Frequency = cutoff;
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

        protected internal override void MixOutput(Span<float> samples)
        {
            // Ensure output buffer is large enough
            if (_out.Length < samples.Length) { Array.Resize(ref _out, samples.Length); }

            // Compute alpha
            var dt = 1F / AudioContext.SampleRate;
            var rc = 1F / (2 * MathF.PI * Frequency);
            var alpha = rc / (rc + dt);

            // Process each sample
            for (var i = 0; i < samples.Length; i++)
            {
                var c = i % AudioContext.Channels; // Channel index
                var j = i - AudioContext.Channels; // Previous sample offset

                // Get previous sample
                var prev = j < 0 ? _mem[i] : samples[j];

                // 
                _pre[c] = alpha * (_pre[c] + samples[i] - prev);
                _out[i] = _pre[c];
            }

            // Store last sample frame as memory for next update
            for (var i = 0; i < AudioContext.Channels; i++)
            {
                _mem[i] = samples[samples.Length - AudioContext.Channels + i];
            }

            // Copy computed output into samples
            for (var i = 0; i < samples.Length; i++)
            {
                samples[i] = _out[i];
            }
        }
    }
}
