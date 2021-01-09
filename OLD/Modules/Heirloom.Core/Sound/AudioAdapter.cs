using System;
using System.IO;

namespace Heirloom.Sound
{
    /// <summary>
    /// A delegate for a callback when audio samples are captured by a input device.
    /// </summary>
    public delegate void AudioCaptureCallback(Span<float> inputSamples);

    /// <summary>
    /// The abstraction of a low level audio system.
    /// </summary>
    public abstract class AudioAdapter : IDisposable
    {
        /// <summary>
        /// The default sample rate used unless configured otherwise.
        /// </summary>
        public const int DefaultSampleRate = 44100;

        // used because OnSpeakerOutput or OnMicrophoneInput may be called concurrently
        private readonly bool _init = false;

        private float[] _buffer = Array.Empty<float>();

        private readonly bool _enableAudioCapture;
        private readonly float _invSampleRate;
        private readonly int _sampleRate;

        #region Constructors

        /// <summary>
        /// Constructs a new instance of <see cref="AudioAdapter"/>.
        /// </summary>
        /// <param name="sampleRate">The sampling rate used by the adapter.</param>
        /// <param name="enableAudioCapture">Should this adapter allow use of microphone?</param>
        protected internal AudioAdapter(int sampleRate, bool enableAudioCapture)
        {
            if (sampleRate <= 0) { throw new InvalidOperationException("Sample rate must greater or equal to 1."); }

            _enableAudioCapture = enableAudioCapture;

            _invSampleRate = 1F / sampleRate;
            _sampleRate = sampleRate;

            _init = true;
        }

        /// <summary>
        /// Cleans up any resources before this object gets collected.
        /// </summary>
        ~AudioAdapter()
        {
            Dispose(false);
        }

        #endregion

        #region Properties

        /// <summary>
        /// Gets a value that determines of the <see cref="AudioAdapter"/> (and associated audio resources) have been initialized.
        /// </summary>
        public bool IsInitialized { get; private set; }

        /// <summary>
        /// Gets a value determining if audio capture (ie, microphone) has been enabled.
        /// </summary>
        public static bool IsAudioCaptureEnabled => Instance._enableAudioCapture;

        /// <summary>
        /// Gets the configured sample rate (ie, samples per second).
        /// </summary>
        public static int SampleRate => Instance._sampleRate;

        /// <summary>
        /// Gets the the inverse of the configured sample rate (ie, seconds per sample)
        /// </summary>
        public static float InverseSampleRate => Instance._invSampleRate;

        /// <summary>
        /// Gets the number of configured audio channels.
        /// </summary>
        public static int Channels => 2;

        /// <summary>
        /// Gets the audio context instance.
        /// </summary>
        internal static AudioAdapter Instance { get; private set; }

        #endregion

        internal void Initialize()
        {
            if (Instance != null)
            {
                throw new InvalidOperationException($"Unable to initialize a second instance of {nameof(AudioAdapter)}. Dispose the first instance.");
            }

            // Mark instance
            IsInitialized = false;
            Instance = this;

            // Initialize secondary resources

            // Mark that the audio system has initialized
            IsInitialized = true;
        }

        /// <summary>
        /// This event is raised when samples are captured from the input device.
        /// </summary>
        public static event AudioCaptureCallback AudioCaptured;

        /// <summary>
        /// Constructs a decoder specific to the implementation.
        /// </summary>
        /// <param name="stream">Some stream pointed at an audio file.</param>
        /// <returns>An instance of a decoder for the specified stream.</returns>
        protected internal abstract IAudioDecoder CreateDecoder(Stream stream);

        #region Mixing

        /// <summary>
        /// Called by an implementation when the audio output requres more samples.
        /// After calling this function, samples should be copied from <paramref name="output"/> to target audio device.
        /// </summary>
        /// <param name="output">The final mixed samples to write to an audio device.</param>
        protected internal void OnSpeakerOutput(Span<short> output)
        {
            if (!_init) { return; } // Not ready!

            // Ensure buffer is large enough for output
            if (_buffer.Length < output.Length) { Array.Resize(ref _buffer, output.Length); }
            var buffer = new Span<float>(_buffer, 0, output.Length);

            // Process speaker output
            AudioGroup.Default.MixOutput(buffer);

            // Write buffer (float) to device (short)
            for (var i = 0; i < output.Length; i++)
            {
                output[i] = (short) (SoftClip(buffer[i]) * short.MaxValue);
                buffer[i] = 0;
            }
        }

        /// <summary>
        /// Called by an implementation when the audio input has captured samples.
        /// </summary>
        /// <param name="input">The incoming samples captured from the audio device.</param>
        protected internal void OnMicrophoneInput(Span<short> input)
        {
            if (!_init) { return; } // Not ready! 

            // Ensure buffer is large enough for input
            if (_buffer.Length < input.Length) { Array.Resize(ref _buffer, input.Length); }
            var buffer = new Span<float>(_buffer, 0, input.Length);

            // Process microphone input
            AudioCaptured?.Invoke(buffer);
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

        #region IDisposable Support

        private bool _isDisposed = false; // To detect redundant calls

        /// <summary>
        /// Implements the dispose pattern to selectively dispose managed resources along with the unmanaged resources.
        /// </summary>
        protected virtual void Dispose(bool disposeManaged)
        {
            if (!_isDisposed)
            {
                if (disposeManaged)
                {
                    // Clean managed
                }

                // Clean unmanaged

                _isDisposed = true;
            }
        }

        /// <summary>
        /// Disposes and performs a clean up of any unmanaged resources.
        /// </summary>
        public void Dispose()
        {
            Dispose(true);
            GC.SuppressFinalize(this);
        }

        #endregion
    }
}
