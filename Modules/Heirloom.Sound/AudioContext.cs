using System;

using Heirloom.Sound.Backends.MiniAudio;

namespace Heirloom.Sound
{
    public abstract class AudioContext : IDisposable
    {
        private float[] _buffer = Array.Empty<float>();
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

        internal void OnSpeakerOutput(Span<short> output)
        {
            if (!_init) { return; } // Not ready!

            // Ensure buffer is large enough for output
            if (_buffer.Length < output.Length) { Array.Resize(ref _buffer, output.Length); }
            var buffer = new Span<float>(_buffer, 0, output.Length);

            // Process speaker output
            AudioMixer.Default.MixOutput(buffer);

            // Write buffer (float) to device (short)
            for (var i = 0; i < output.Length; i++)
            {
                output[i] = (short) (SoftClip(buffer[i]) * short.MaxValue);
                buffer[i] = 0;
            }
        }

        internal void OnMicrophoneInput(Span<short> input)
        {
            if (!_init) { return; } // Not ready! 

            // Process microphone input
            // TODO: Microphone support
        }

        /// <summary>
        /// Computes soft clamping of audio ( -1.0 to +1.0 ).
        /// </summary>
        private static float SoftClip(float x)
        {
            x /= (float) Math.Sqrt(Math.Sqrt(1 + (x * x * x * x)));
            return x;
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
