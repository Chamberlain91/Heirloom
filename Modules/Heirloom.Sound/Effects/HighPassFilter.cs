using System;

namespace Heirloom.Sound.Effects
{
    /// <summary>
    /// An audio effect that implements a high pass filter.
    /// </summary>
    public class HighPassFilter : AudioEffect
    {
        private readonly float[] _k;
        private float _cutoff;

        private readonly AudioBuffer _buffer;

        public HighPassFilter(float cutoff)
        {
            _k = new float[AudioContext.Channels];
            Frequency = cutoff;

            _buffer = new AudioBuffer(1); // 1/2 second
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

        protected internal override void MixOutput(Span<float> samples)
        {
            // Ensure we can record enough audio for one extra time slice
            if (_buffer.Capacity < (samples.Length + AudioContext.Channels)) { _buffer.Resize(samples.Length + AudioContext.Channels); }

            // 
            _buffer.Record(samples);

            // 
            if (_buffer.Count < _buffer.Capacity) { samples.Fill(0); }
            else
            {
                // Compute alpha
                var dt = 1F / AudioContext.SampleRate;
                var rc = 1F / (2 * MathF.PI * Frequency);
                var alpha = rc / (rc + dt);

                // 
                for (var i = 0; i < samples.Length; i++)
                {
                    var c = i % AudioContext.Channels;
                    _k[c] = alpha * (_k[c] + _buffer.GetSample(i + AudioContext.Channels) - _buffer.GetSample(i));
                    samples[i] = _k[c];
                }
            }
        }
    }
}
