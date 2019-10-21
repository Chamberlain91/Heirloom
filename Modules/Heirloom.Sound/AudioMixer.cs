using System;
using System.Collections.Generic;

namespace Heirloom.Sound
{
    public static class AudioMixer
    {
        internal static AudioProcessor Processor => AudioProcessor.Instance;

        private static readonly LinkedList<AudioMixerEffect> _mixerEffects = new LinkedList<AudioMixerEffect>();

        public static AudioProcessorMode ProcessorMode { get; private set; }

        public static int SampleRate { get; private set; }

        public static int Channels => 2;

        public static void Initialize(bool enableMicrophone, int sampleRate)
        {
            var mode = enableMicrophone ? AudioProcessorMode.Duplex : AudioProcessorMode.Playback;
            Initialize(mode, sampleRate);
        }

        private static void Initialize(AudioProcessorMode processorMode, int sampleRate)
        {
            ProcessorMode = processorMode;
            SampleRate = sampleRate;

            AudioProcessor.Initialize(processorMode, sampleRate);
        }

        public static void AddEffect(AudioMixerEffect effect)
        {
            _mixerEffects.AddLast(effect);
        }

        internal static void MixOutput(Span<float> output)
        {
            // Process effect chain
            foreach (var effect in _mixerEffects)
            {
                effect.Mix(output);
            }
        }

        /// <summary>
        /// Computes the number of samples needed for the specified time in seconds.
        /// </summary>
        public static int ComputeSampleSize(float time)
        {
            return (int) (time * SampleRate * Channels);
        }

        /// <summary>
        /// Computes soft clamping of audio ( -1.0 to +1.0 ).
        /// </summary>
        public static float SoftClip(float x)
        {
            x /= (float) Math.Sqrt(Math.Sqrt(1 + (x * x * x * x)));
            return x;
        }
    }
}
