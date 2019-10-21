using System;

using static Heirloom.Sound.Backends.MiniAudio.NativeApi;

namespace Heirloom.Sound.Backends.MiniAudio
{
    internal sealed unsafe class MiniAudioContext : AudioContext
    {
        private readonly void* _device;
        private readonly DataProcessCallback _dataProc;
        private bool _isDisposed;

        #region Constructors

        internal MiniAudioContext(int sampleRate)
            : base(sampleRate)
        {
            // Allocate device config
            var deviceConfig = ma_ext_alloc_device_config(DeviceType.Playback, (uint) SampleRate, _dataProc = DataProcessCallback);

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

        ~MiniAudioContext()
        {
            Dispose(false);
        }

        #endregion

        private void DataProcessCallback(void* pDevice, void* pOutput, void* pInput, uint frameCount)
        {
            // Cast to short pointers (S16)
            var pOutputSamples = (short*) pOutput;
            var pInputSamples = (short*) pInput;

            var sampleCount = (int) frameCount * Channels;

            // Process audio (read, decode, etc)
            if (pInputSamples != null) { OnMicrophoneInput(new Span<short>(pInputSamples, sampleCount)); }
            if (pOutputSamples != null) { OnSpeakerOutput(new Span<short>(pOutputSamples, sampleCount)); }
        }

        #region Dispose

        private void Dispose(bool disposeManaged)
        {
            if (!_isDisposed)
            {
                if (disposeManaged)
                {
                    // TODO: dispose managed state (managed objects).
                }

                // Stop and uninitialize device
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
