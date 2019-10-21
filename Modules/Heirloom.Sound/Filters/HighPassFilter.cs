using System;

namespace Heirloom.Sound.Filters
{
    /// <summary>
    /// A mixer effect that implements a high pass filter.
    /// </summary>
    public class HighPassFilter : AudioMixerEffect
    {
        private readonly float[] _k;
        private float _cutoff;

        private AudioBuffer _buffer;

        public HighPassFilter(float cutoff)
        {
            _k = new float[AudioMixer.Channels];
            Cutoff = cutoff;

            _buffer = new AudioBuffer(1); // 1/2 second
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
            // Ensure we can record enough audio for one extra time slice
            if (_buffer.Capacity < (samples.Length + AudioMixer.Channels)) { _buffer.Resize(samples.Length + AudioMixer.Channels); }

            // 
            _buffer.Record(samples);

            // 
            if (_buffer.Count < _buffer.Capacity) { samples.Fill(0); }
            else
            {
                // Compute alpha
                var dt = 1F / AudioMixer.SampleRate;
                var rc = 1F / (2 * MathF.PI * Cutoff);
                var alpha = rc / (rc + dt);

                // 
                for (var i = 0; i < samples.Length; i++)
                {
                    var c = i % AudioMixer.Channels;
                    _k[c] = alpha * (_k[c] + _buffer.GetSample(i + AudioMixer.Channels) - _buffer.GetSample(i));
                    samples[i] = _k[c];
                }
            }
        }
    }
}
