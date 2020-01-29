using System;

namespace Heirloom.Sound
{
    /// <summary>
    /// An audio effect. Implementations of this class mutate the audio for various effects.
    /// </summary>
    /// <seealso cref="Effects.LowPassFilter"/>
    /// <seealso cref="Effects.HighPassFilter"/>
    /// <seealso cref="Effects.BandPassFilter"/>
    /// <seealso cref="Effects.BitCrushEffect"/>
    /// <seealso cref="Effects.ReverbEffect"/>
    public abstract class AudioEffect
    {
        public abstract float Process(float sample, int channel);
    }
}
