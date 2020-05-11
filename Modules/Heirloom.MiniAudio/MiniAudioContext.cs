using System;
using System.IO;

using Heirloom.Sound;

using static Heirloom.MiniAudio.NativeApi;

namespace Heirloom.MiniAudio
{
    internal sealed unsafe class MiniAudioContext : AudioAdapter
    {
        private readonly void* _device;
        private readonly DataProcessCallback _dataProc;

        internal MiniAudioContext(int sampleRate, bool enableAudioCapture)
            : base(sampleRate, enableAudioCapture)
        {
            // Build device type flags
            var deviceType = DeviceType.Playback;
            if (enableAudioCapture) { deviceType |= DeviceType.Capture; }

            // Allocate device config
            var deviceConfig = ma_ext_alloc_device_config(deviceType, (uint) sampleRate, _dataProc = DataProcessCallback);

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

        protected override void Dispose(bool disposing)
        {
            base.Dispose(disposing);

            // Stop and uninitialize device
            ma_device_stop(_device);
            ma_device_uninit(_device);
            ma_ext_free(_device);

            GC.KeepAlive(_dataProc);
        }

        internal override AudioDecoder CreateDecoder(Stream stream)
        {
            return new MiniAudioDecoder(stream);
        }
    }
}
