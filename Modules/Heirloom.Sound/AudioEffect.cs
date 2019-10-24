using System;

namespace Heirloom.Sound
{
    public abstract class AudioEffect
    {
        protected internal abstract void MixOutput(Span<float> samples);

        protected static float Interpolate(float a, float b, float t)
        {
            return a + (b - a) * t;
        }
    }
}
