namespace Heirloom.Sound
{
    /// <summary>
    /// An abstarct representation of an audio effect.
    /// Implementations of this class mutate the audio for various effects.
    /// </summary>
    /// <seealso cref="LowPassFilter"/>
    /// <seealso cref="HighPassFilter"/>
    /// <seealso cref="BandPassFilter"/> 
    /// <seealso cref="ReverbEffect"/>
    public abstract class AudioEffect
    {
        /// <summary>
        /// This function is called to alter a sample for some implementation of an effect.
        /// </summary>
        /// <param name="sample">The incoming sample to alter.</param>
        /// <param name="channel">The channel number this sample belongs to.</param>
        /// <returns>The altered sample.</returns>
        public abstract float Process(float sample, int channel);
    }
}
