namespace Heirloom.Sound
{
    /// <summary>
    /// An audio effect that implements a band pass filter.
    /// </summary>
    public class BandPassFilter : AudioEffect
    {
        private readonly LowPassFilter _lo;
        private readonly HighPassFilter _hi;

        /// <summary>
        /// Constructs a new instance of <see cref="BandPassFilter"/>.
        /// </summary> 
        public BandPassFilter(float low, float high)
        {
            _hi = new HighPassFilter(low);
            _lo = new LowPassFilter(high);
        }

        /// <summary>
        /// Gets or sets the high frequency cutoff in hertz.
        /// </summary>
        /// <value>This value ranges from 0.0 to <see cref="AudioAdapter.SampleRate"/>.</value>
        public float HighFrequency
        {
            get => _hi.Frequency;
            set => _hi.Frequency = value;
        }

        /// <summary>
        /// Gets or sets the low frequency cutoff in hertz.
        /// </summary>
        /// <value>This value ranges from 0.0 to <see cref="AudioAdapter.SampleRate"/>.</value>
        public float LowFrequency
        {
            get => _lo.Frequency;
            set => _lo.Frequency = value;
        }

        /// <inheritdoc/>
        public override float Process(float sample, int channel)
        {
            sample = _hi.Process(sample, channel);
            sample = _lo.Process(sample, channel);
            return sample;
        }
    }
}
