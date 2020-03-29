namespace Heirloom.Sound.Effects
{
    /// <summary>
    /// An audio effect that implements a band pass filter.
    /// </summary>
    public class BandPassFilter : AudioEffect
    {
        private readonly LowPassFilter _lo;
        private readonly HighPassFilter _hi;

        public BandPassFilter(float low, float high)
        {
            _hi = new HighPassFilter(low);
            _lo = new LowPassFilter(high);
        }

        /// <summary>
        /// Gets or sets the filter cutoff in hertz.
        /// </summary>
        public float MinFrequency
        {
            get => _hi.Frequency;
            set => _hi.Frequency = value;
        }

        /// <summary>
        /// Gets or sets the filter cutoff in hertz.
        /// </summary>
        public float Cutoff
        {
            get => _lo.Frequency;
            set => _lo.Frequency = value;
        }

        public override float Process(float sample, int channel)
        {
            sample = _hi.Process(sample, channel);
            sample = _lo.Process(sample, channel);
            return sample;
        }
    }
}
