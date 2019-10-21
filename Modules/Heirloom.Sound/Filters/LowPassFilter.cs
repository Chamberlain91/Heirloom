using System;

namespace Heirloom.Sound.Filters
{
    /// <summary>
    /// A mixer effect that implements a low pass filter.
    /// </summary>
    public class LowPassFilter : AudioMixerEffect
    {
        private readonly float[] _k;
        private float _cutoff;

        public LowPassFilter(float cutoff)
        {
            _k = new float[AudioMixer.Channels];
            Cutoff = cutoff;
        }

        /// <summary>
        /// Gets or sets the filter cutoff in hertz.
        /// </summary>
        public float Cutoff
        {
            get => _cutoff;

            set
            {
                if (value <= 0) { throw new ArgumentOutOfRangeException($"Filter cutoff must be greater than zero."); }
                _cutoff = value;
            }
        }

        protected internal override void Mix(Span<float> samples)
        {
            // Compute alpha
            var dt = 1F / AudioMixer.SampleRate;
            var rc = 1F / (2 * MathF.PI * Cutoff);
            var alpha = dt / (rc + dt);

            // 
            for (var i = 0; i < samples.Length; i++)
            {
                var c = i % AudioMixer.Channels;

                _k[c] += alpha * (samples[i] - _k[c]);
                samples[i] = _k[c];
            }
        }
    }
}
