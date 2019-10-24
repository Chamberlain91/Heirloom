using System;
using System.Collections.Generic;

namespace Heirloom.Sound
{
    public class AudioMixer
    {
        private readonly LinkedList<AudioSource> _sources;
        private readonly LinkedList<AudioSource> _sourcesRemove;
        private readonly LinkedList<AudioSource> _sourcesAdd;

        private float[] _mixBuffer = Array.Empty<float>();

        public AudioMixer()
        {
            _sources = new LinkedList<AudioSource>();
            _sourcesRemove = new LinkedList<AudioSource>();
            _sourcesAdd = new LinkedList<AudioSource>();

            Effects = new EffectChain();
        }

        public EffectChain Effects { get; }

        internal void MixOutput(Span<short> outSamples)
        {
            // Ensure mix buffer is a large enough size
            if (_mixBuffer.Length < outSamples.Length) { Array.Resize(ref _mixBuffer, outSamples.Length); }
            var mixBuffer = new Span<float>(_mixBuffer, 0, outSamples.Length);

            lock (_sources)
            {
                // Process source list mutation
                foreach (var node in _sourcesRemove) { _sources.Remove(node); }
                foreach (var node in _sourcesAdd) { _sources.AddLast(node); }
                _sourcesRemove.Clear();
                _sourcesAdd.Clear();

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

            // Process efect chain
            Effects.MixOutput(mixBuffer);

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
                return _sourcesAdd.AddLast(source);
            }
        }

        internal void RemoveSource(LinkedListNode<AudioSource> node)
        {
            lock (_sources)
            {
                _sourcesRemove.AddLast(node);
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
