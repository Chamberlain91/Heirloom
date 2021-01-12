using System;
using System.IO;

namespace Heirloom.Sound
{
    public static class AudioBackend
    {
#if ANDROID
        public const int SampleRate = 22050;
#else
        public const int SampleRate = 44100;
#endif
        public const float InverseSampleRate = 1F / SampleRate;

        public const int Channels = 2;

        private static AudioImplementation _implementation;

        private static float[] _buffer = Array.Empty<float>();

        /// <summary>
        /// This event is raised when samples are captured from the input device.
        /// </summary>
        public static event AudioCaptureCallback AudioCaptured;

        static AudioBackend()
        {
            Resume();
        }

        /// <summary>
        /// Instantiate the audio backend (if not already created).
        /// </summary>
        public static void Resume()
        {
            if (_implementation == null)
            {
#if ANDROID
                _implementation = new Android.AndroidAudioImplementation();
#else
                _implementation = new Desktop.DesktopAudioImplementation();
#endif
            }
        }

        /// <summary>
        /// Dispose of the audio backend, pausing global playback.
        /// </summary>
        public static void Pause()
        {
            _implementation?.Dispose();
            _implementation = null;
        }

        #region Final Mix Functions

        internal static void GatherOutputSamples(Span<short> output)
        {
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

        internal static void GatherInputSamples(Span<short> input)
        {
            // Ensure buffer is large enough for input
            if (_buffer.Length < input.Length) { Array.Resize(ref _buffer, input.Length); }
            var buffer = new Span<float>(_buffer, 0, input.Length);

            // Process microphone input
            AudioCaptured?.Invoke(buffer);
        }

        #endregion

        #region Device Enumeration

        // todo: make public when ready
        internal static AudioDevice[] GetPlaybackDevices()
        {
            return _implementation.GetPlaybackDevices();
        }

        // todo: make public when ready
        internal static AudioDevice[] GetCaptureDevices()
        {
            return _implementation.GetCaptureDevices();
        }

        // todo: make public when ready
        internal static void UsePlaybackDevice(AudioDevice device)
        {
            device ??= _implementation.GetDefaultPlaybackDevice();
            _implementation.UsePlaybackDevice(device);
        }

        // todo: make public when ready
        internal static void UseCaptureDevice(AudioDevice device)
        {
            device ??= _implementation.GetDefaultCaptureDevice();
            _implementation.UseCaptureDevice(device);
        }

        #endregion

        internal static IAudioDecoder CreateDecoder(Stream stream)
        {
            return _implementation.CreateDecoder(stream);
        }

        /// <summary>
        /// Computes soft clamping of audio ( -1.0 to +1.0 ).
        /// </summary>
        private static float SoftClip(float x)
        {
            // Assumes audio is already in the floating point domain
            x /= (float) Math.Sqrt(Math.Sqrt(1 + (x * x * x * x)));
            return x;
        }

        /// <summary>
        /// A delegate for a callback when audio samples are captured by a input device.
        /// </summary>
        public delegate void AudioCaptureCallback(Span<float> inputSamples);
    }
}
