using System;
using System.IO;

namespace Heirloom
{
    public delegate void AudioCaptureCallback(Span<float> inputSamples);

    internal abstract class AudioAdapter : IDisposable
    {
        public const int DefaultSampleRate = 44100;

        // used because OnSpeakerOutput or OnMicrophoneInput may be called concurrently
        private readonly bool _init = false;

        private float[] _buffer = Array.Empty<float>();

        private readonly bool _enableAudioCapture;
        private readonly float _invSampleRate;
        private readonly int _sampleRate;

        #region Constructors

        internal AudioAdapter(int sampleRate, bool enableAudioCapture)
        {
            if (sampleRate <= 0) { throw new InvalidOperationException("Sample rate must greater or equal to 1."); }

            _enableAudioCapture = enableAudioCapture;

            _invSampleRate = 1F / sampleRate;
            _sampleRate = sampleRate;

            _init = true;
        }

        ~AudioAdapter()
        {
            Dispose(false);
        }

        #endregion

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
        /// Gets the number of configured channels.
        /// </summary>
        public static int Channels => 2;

        /// <summary>
        /// Gets the audio context instance.
        /// </summary>
        internal static AudioAdapter Instance { get; private set; }

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

        internal abstract AudioDecoder CreateDecoder(Stream stream);

        /// <summary>
        /// Event invoked when a chunk of audio data is captured by the microphone.
        /// </summary>
        public static event AudioCaptureCallback AudioCaptured;

        #region Mixing

        internal void OnSpeakerOutput(Span<short> output)
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

        internal void OnMicrophoneInput(Span<short> input)
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

        protected virtual void Dispose(bool disposing)
        {
            if (!_isDisposed)
            {
                if (disposing)
                {
                    // Clean managed
                }

                // Clean unmanaged

                _isDisposed = true;
            }
        }

        public void Dispose()
        {
            Dispose(true);
            GC.SuppressFinalize(this);
        }

        #endregion
    }
}
