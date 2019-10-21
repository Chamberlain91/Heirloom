using System;

namespace Heirloom.Sound
{
    public abstract class AudioMixerEffect
    {
        protected internal abstract void MixOutput(Span<float> samples);
    }
}
