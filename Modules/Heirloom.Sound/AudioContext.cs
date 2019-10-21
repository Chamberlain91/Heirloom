using System;

using Heirloom.Sound.Backends.MiniAudio;

namespace Heirloom.Sound
{
    public abstract class AudioContext : IDisposable
    {
        private readonly bool _init = false;

        #region Constructors

        internal AudioContext(int sampleRate)
        {
            //if (sampleRate < 8000) { throw new InvalidOperationException("Sample rate must greater or equal to 8000."); }

            SampleRate = sampleRate;

            // 
            _init = true;
        }

        #endregion

        public int SampleRate { get; }

        public int Channels => 2;

        #region Mixing

        internal void OnSpeakerOutput(Span<short> outputBuffer)
        {
            if (!_init) { return; } // Not ready!

            // Process mixer output
            AudioMixer.Master.MixOutput(outputBuffer);
        }

        internal void OnMicrophoneInput(Span<short> inputBuffer)
        {
            if (!_init) { return; } // Not ready! 

            // TODO: Microphone support
        }

        #endregion

        public abstract void Dispose();

        #region Static / Singleton

        private static AudioContext _context;

        /// <summary>
        /// Gets the audio context instance.
        /// This will initialize with defaults if not explicitly initialized beforehand.
        /// </summary>
        internal static AudioContext Instance
        {
            get
            {
                // If no contxt has been initialized, initialize a default.
                if (_context == null) { Initialize(44100); }
                return _context;
            }
        }

        public static void Initialize(int sampleRate)
        {
            if (_context == null)
            {
                // Create default
                _context = new MiniAudioContext(sampleRate);

                // Dispose device when process exits, finalizer isn't being called
                // but this reliably is called on Window .NET and Linux Mono 5.2
                // todo: see behaviour on Android, macOS
                AppDomain.CurrentDomain.ProcessExit += (s, e) => _context.Dispose();
            }
            else
            {
                throw new InvalidOperationException("Audio device already initialized");
            }
        }

        #endregion
    }
}
