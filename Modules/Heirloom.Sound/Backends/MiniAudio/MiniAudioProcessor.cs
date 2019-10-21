using System;

using static Heirloom.Sound.Backends.MiniAudio.NativeApi;

namespace Heirloom.Sound.Backends.MiniAudio
{
    internal sealed unsafe class MiniAudioProcessor : AudioProcessor
    {
        private readonly void* _device;
        private readonly DataProcessCallback _dataProc;
        private bool _isDisposed;

        #region Constructors

        internal MiniAudioProcessor(AudioProcessorMode mode, int sampleRate)
            : base(mode, sampleRate)
        {
            DeviceType type = 0;
            if (mode.HasFlag(AudioProcessorMode.Playback)) { type |= DeviceType.Playback; }
            if (mode.HasFlag(AudioProcessorMode.Capture)) { type |= DeviceType.Capture; }

            // Allocate device config
            var deviceConfig = ma_ext_alloc_device_config(type, (uint) SampleRate,
                _dataProc = (void* pDevice, void* pOutput, void* pInput, uint frameCount) =>
                {
                    // Cast to short pointers (S16)
                    var pOutputSamples = (short*) pOutput;
                    var pInputSamples = (short*) pInput;

                    var sampleCount = (int) frameCount * Channels;

                    // Process audio (read, decode, etc)
                    if (pInputSamples != null) { OnMicrophoneInput(new Span<short>(pInputSamples, sampleCount)); }
                    if (pOutputSamples != null) { OnSpeakerOutput(new Span<short>(pOutputSamples, sampleCount)); }
                });

            // Allocate device data and initialize
            _device = ma_ext_alloc_device();
            var result = ma_device_init(null, deviceConfig, _device);
            if (result != Result.Success) { throw new InvalidOperationException("Unable to init device. " + result); }

            // Free device config, device has been initialized
            ma_ext_free(deviceConfig);

            // Start audio device
            result = ma_device_start(_device);
            if (result != Result.Success) { throw new InvalidOperationException("Unable to start device. " + result); }
        }

        ~MiniAudioProcessor()
        {
            Console.WriteLine("~AudioProcessor");
            Dispose(false);
        }

        #endregion

        #region Dispose

        private void Dispose(bool disposeManaged)
        {
            if (!_isDisposed)
            {
                if (disposeManaged)
                {
                    // TODO: dispose managed state (managed objects).
                }

                // MiniAudio.ma_device_uninit
                ma_device_stop(_device);
                ma_device_uninit(_device);
                ma_ext_free(_device);

                GC.KeepAlive(_dataProc);

                _isDisposed = true;
            }
        }

        public override void Dispose()
        {
            Dispose(true);
            GC.SuppressFinalize(this);
        }

        #endregion
    }
}
