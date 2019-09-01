using System;
using System.Collections.Generic;

namespace Heirloom.Sound
{
    /// <summary>
    /// A high-level audio device, handles processing instances of <see cref="AudioSource"/>.
    /// </summary>
    internal sealed class AudioDevice : LowLevel.AudioProcessor
    {
        private readonly LinkedList<AudioSource> _activeSources;
        private readonly Queue<LinkedListNode<AudioSource>> _remSources;
        private readonly Queue<LinkedListNode<AudioSource>> _addSources;
        private readonly bool _init = false;

        private float[] _outMixBuffer = new float[0];

        #region Constructors

        /// <summary>
        /// Constructs a new instance of <see cref="AudioDevice"/>.
        /// </summary>
        /// <param name="sampleRate">The number of samples processed per second.</param>
        internal AudioDevice(uint sampleRate = 22050)
            : this(AudioDeviceMode.Playback, sampleRate)
        {
            // Should we, by default, start this device?
            if (AutomaticStartDevice) { Start(); }
        }

        // todo: expose as public once I figure out how to incorperate microphone.
        internal AudioDevice(AudioDeviceMode mode, uint sampleRate = 22050)
            : base(mode, sampleRate)
        {
            _addSources = new Queue<LinkedListNode<AudioSource>>();
            _remSources = new Queue<LinkedListNode<AudioSource>>();
            _activeSources = new LinkedList<AudioSource>();

            // 
            _init = true;
        }

        #endregion

        #region Mixing

        protected override unsafe void UpdateAudioStream(short* pOutputFrames, short* pInputFrames, uint frameCount)
        {
            if (!_init) { return; } // Not ready!

            // Mutate active source list
            AddRemoveActiveSources();

            // Process audio
            UpdateOutputStream(pOutputFrames, frameCount);
            UpdateInputStream(pInputFrames, frameCount);
        }

        private unsafe void UpdateOutputStream(short* pOutputFrames, uint frameCount)
        {
            // If we have an output stream to write into, process output
            if (pOutputFrames != null)
            {
                // Ensure mix buffer is large enough size
                if (_outMixBuffer.Length < frameCount * Channels) { Array.Resize(ref _outMixBuffer, (int) (frameCount * Channels)); }

                // Process all active sources
                foreach (var source in _activeSources)
                {
                    source.ProcessAudioOutput(_outMixBuffer, (int) frameCount);
                }

                // Apply anti-clipping and write into output
                for (var i = 0; i < _outMixBuffer.Length; i++)
                {
                    // clip and output final sample
                    pOutputFrames[i] = ClipSample(_outMixBuffer[i] / short.MaxValue);
                    _outMixBuffer[i] = 0; // clear for next iteration
                }
            }
        }

        private unsafe void UpdateInputStream(short* pInputFrames, uint frameCount)
        {
            // If we have an input stream to read from, process input
            if (pInputFrames != null)
            {
                // todo: process input
            }
        }

        // 
        private short ClipSample(float x)
        {
            x /= (float) Math.Sqrt(Math.Sqrt(1 + (x * x * x * x)));
            return (short) (x * short.MaxValue);
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

        #region Static / Singleton

        private static AudioDevice _device;

        /// <summary>
        /// Should the audio device automatically start when created? Default is true.
        /// </summary>
        public static bool AutomaticStartDevice { get; set; } = true;

        /// <summary>
        /// Gets the audio context instance.
        /// This will initialize with defaults if not explicitly initialized beforehand.
        /// </summary>
        internal static AudioDevice Instance
        {
            get
            {
                // If no device
                if (_device == null)
                {
                    // Initialize context with defaults
                    Initialize();
                }

                return _device;
            }
        }

        public static void Initialize(uint sampleRate = 22050)
        {
            if (_device == null)
            {
                // Create singleton
                _device = new AudioDevice(sampleRate);

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
