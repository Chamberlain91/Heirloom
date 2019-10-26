using System;

namespace Heirloom.Sound.Effects
{
    /// <summary>
    /// An audio effect that implements a band pass filter.
    /// </summary>
    public class BandPassFilter : AudioEffect
    {
        private readonly LowPassFilter _lp;
        private readonly HighPassFilter _hp;

        public BandPassFilter(float low, float high)
        {
            _hp = new HighPassFilter(low);
            _lp = new LowPassFilter(high);
        }

        /// <summary>
        /// Gets or sets the filter cutoff in hertz.
        /// </summary>
        public float MinFrequency
        {
            get => _hp.Frequency;
            set => _hp.Frequency = value;
        }

        /// <summary>
        /// Gets or sets the filter cutoff in hertz.
        /// </summary>
        public float Cutoff
        {
            get => _lp.Frequency;
            set => _lp.Frequency = value;
        }
        protected internal override void MixOutput(Span<float> samples)
        {
            _hp.MixOutput(samples);
            _lp.MixOutput(samples);
        }
    }
}
