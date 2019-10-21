using System;
using System.Collections.Generic;

namespace Heirloom.Sound
{
    public class AudioMixer
    {
        private readonly LinkedList<AudioSource> _sources;
        private readonly List<AudioMixerEffect> _effects;

        private float[] _mixBuffer = Array.Empty<float>();

        public AudioMixer()
        {
            _sources = new LinkedList<AudioSource>();
            _effects = new List<AudioMixerEffect>();
        }

        public void AddEffect(AudioMixerEffect effect)
        {
            lock (_effects)
            {
                _effects.Add(effect);
            }
        }

        public void RemoveEffect(AudioMixerEffect effect)
        {
            lock (_effects)
            {
                _effects.Remove(effect);
            }
        }

        internal void MixOutput(Span<short> outSamples)
        {
            // Ensure mix buffer is a large enough size
            if (_mixBuffer.Length < outSamples.Length) { Array.Resize(ref _mixBuffer, outSamples.Length); }
            var mixBuffer = new Span<float>(_mixBuffer, 0, outSamples.Length);

            lock (_sources)
            {
                // Mix output from audio source
                foreach (var source in _sources)
                {
                    source.MixOutput(mixBuffer);
                }
            }

            // Normalize audio
            for (var i = 0; i < outSamples.Length; i++)
            {
                mixBuffer[i] /= short.MaxValue;
            }

            lock (_effects)
            {
                // Process effect chain
                foreach (var effect in _effects)
                {
                    effect.MixOutput(mixBuffer);
                }
            }

            // Apply soft-clip and write into output
            for (var i = 0; i < outSamples.Length; i++)
            {
                // Write mixed audio to output buffer (soft clipped)
                outSamples[i] = (short) (SoftClip(mixBuffer[i]) * short.MaxValue);

                // Zero mix buffer at this positon
                mixBuffer[i] = 0;
            }
        }

        #region Active Source List Manipulation

        internal LinkedListNode<AudioSource> InsertSource(AudioSource source)
        {
            lock (_sources)
            {
                var node = new LinkedListNode<AudioSource>(source);
                _sources.AddLast(node);
                return node;
            }
        }

        internal void RemoveSource(LinkedListNode<AudioSource> node)
        {
            lock (_sources)
            {
                _sources.Remove(node);
            }
        }

        #endregion

        internal static AudioContext Context => AudioContext.Instance;

        /// <summary>
        /// Gets the default mixer object.
        /// </summary>
        public static AudioMixer Master { get; } = new AudioMixer();

        /// <summary>
        /// Gets the audio sample rate.
        /// </summary>
        public static int SampleRate => Context.SampleRate;

        /// <summary>
        /// Gets the number of audio channels.
        /// </summary>
        public static int Channels => Context.Channels;

        /// <summary>
        /// Computes soft clamping of audio ( -1.0 to +1.0 ).
        /// </summary>
        private static float SoftClip(float x)
        {
            x /= (float) Math.Sqrt(Math.Sqrt(1 + (x * x * x * x)));
            return x;
        }
    }
}
