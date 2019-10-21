using System;

namespace Heirloom.Sound
{
    public abstract class AudioMixerEffect
    {
        private int _time;

        internal void MixAudioToOutput(Span<float> samples)
        {
            Mix(samples);

            _time += samples.Length;
        }

        protected internal abstract void Mix(Span<float> samples);

        /// <summary>
        /// Computes the time in seconds.
        /// </summary>
        /// <param name="offset">The offset into the current time slice relative to <see cref="_time"/>.</param> 
        protected float GetSeconds(int offset)
        {
            return (_time + offset) / (float) AudioMixer.SampleRate;
        }
    }
}
