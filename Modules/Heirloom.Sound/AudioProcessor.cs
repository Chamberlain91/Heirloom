using System;
using System.Collections.Generic;

using Heirloom.Sound.Backends.MiniAudio;

namespace Heirloom.Sound
{
    internal abstract class AudioProcessor : IDisposable
    {
        private readonly LinkedList<AudioSource> _activeSources;
        private readonly Queue<LinkedListNode<AudioSource>> _remSources;
        private readonly Queue<LinkedListNode<AudioSource>> _addSources;

        private readonly bool _init = false;

        private float[] _mixBuffer = Array.Empty<float>();

        #region Constructors

        internal AudioProcessor(AudioProcessorMode mode, int sampleRate)
        {
            //if (sampleRate < 8000) { throw new InvalidOperationException("Sample rate must greater or equal to 8000."); }

            SampleRate = sampleRate;
            Mode = mode;

            _addSources = new Queue<LinkedListNode<AudioSource>>();
            _remSources = new Queue<LinkedListNode<AudioSource>>();
            _activeSources = new LinkedList<AudioSource>();

            // 
            _init = true;
        }

        #endregion

        public AudioProcessorMode Mode { get; }

        public int SampleRate { get; }

        public int Channels => 2;

        #region Mixing

        internal void OnSpeakerOutput(Span<short> outputBuffer)
        {
            if (!_init) { return; } // Not ready!

            // Mutate active source list
            AddRemoveActiveSources();

            // Ensure mix buffer is large enough size
            if (_mixBuffer.Length < outputBuffer.Length) { Array.Resize(ref _mixBuffer, outputBuffer.Length); }
            var mixBuffer = new Span<float>(_mixBuffer, 0, outputBuffer.Length);

            // Process all active sources
            foreach (var source in _activeSources)
            {
                source.MixAudioToOutput(mixBuffer);
            }

            // Normalize audio
            for (var i = 0; i < outputBuffer.Length; i++)
            {
                _mixBuffer[i] /= short.MaxValue;
            }

            // Process mixer effects
            AudioMixer.MixOutput(mixBuffer);

            // Apply soft-clip and write into output
            for (var i = 0; i < outputBuffer.Length; i++)
            {
                // Write mixed audio to output buffer (soft clipped)
                outputBuffer[i] = (short) (AudioMixer.SoftClip(_mixBuffer[i]) * short.MaxValue);

                // Zero mix buffer at this positon
                _mixBuffer[i] = 0;
            }
        }

        internal void OnMicrophoneInput(Span<short> inputBuffer)
        {
            if (!_init) { return; } // Not ready! 

            // Ensure mix buffer is large enough size
            if (_mixBuffer.Length < inputBuffer.Length) { Array.Resize(ref _mixBuffer, inputBuffer.Length); }

            // 
            for (var i = 0; i < inputBuffer.Length; i++)
            {
                _mixBuffer[i] = inputBuffer[i];
            }
        }

        #endregion

        #region Active Source List Manipulation

        private unsafe void AddRemoveActiveSources()
        {
            // Add sources to active list
            foreach (var source in _addSources) { _activeSources.AddLast(source); }
            _addSources.Clear();

            // Remove sources from active list
            foreach (var node in _remSources) { _activeSources.Remove(node); }
            _remSources.Clear();
        }

        internal LinkedListNode<AudioSource> InsertSource(AudioSource source)
        {
            var node = new LinkedListNode<AudioSource>(source);
            _addSources.Enqueue(node);
            return node;
        }

        internal void RemoveSource(LinkedListNode<AudioSource> node)
        {
            _remSources.Enqueue(node);
        }

        #endregion

        public abstract void Dispose();

        #region Static / Singleton

        private static AudioProcessor _device;

        /// <summary>
        /// Gets the audio context instance.
        /// This will initialize with defaults if not explicitly initialized beforehand.
        /// </summary>
        internal static AudioProcessor Instance
        {
            get
            {
                // If no device
                if (_device == null)
                {
                    throw new InvalidOperationException("Mixer not initialized");
                }

                return _device;
            }
        }

        public static void Initialize(AudioProcessorMode deviceMode, int sampleRate)
        {
            if (_device == null)
            {
                // Create default
                _device = new MiniAudioProcessor(deviceMode, sampleRate);

                // Dispose device when process exits, finalizer isn't being called
                // but this reliably is called on Window .NET and Linux Mono 5.2
                // todo: see behaviour on Android, macOS
                AppDomain.CurrentDomain.ProcessExit += (s, e) => _device.Dispose();
            }
            else
            {
                throw new InvalidOperationException("Audio device already initialized");
            }
        }

        #endregion
    }
}
