namespace Heirloom
{
    /// <summary>
    /// An audio effect. Implementations of this class mutate the audio for various effects.
    /// </summary>
    /// <seealso cref="AudioEffects.LowPassFilter"/>
    /// <seealso cref="AudioEffects.HighPassFilter"/>
    /// <seealso cref="AudioEffects.BandPassFilter"/>
    /// <seealso cref="AudioEffects.BitCrushEffect"/>
    /// <seealso cref="AudioEffects.ReverbEffect"/>
    public abstract class AudioEffect
    {
        public abstract float Process(float sample, int channel);
    }
}
