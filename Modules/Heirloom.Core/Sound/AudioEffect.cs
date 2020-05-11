namespace Heirloom.Sound
{
    /// <summary>
    /// An audio effect. Implementations of this class mutate the audio for various effects.
    /// </summary>
    /// <seealso cref="LowPassFilter"/>
    /// <seealso cref="HighPassFilter"/>
    /// <seealso cref="BandPassFilter"/> 
    /// <seealso cref="ReverbEffect"/>
    public abstract class AudioEffect
    {
        public abstract float Process(float sample, int channel);
    }
}
