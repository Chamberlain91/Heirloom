using System;

using Heirloom.Sound.LowLevel.Backends.MiniAudio;

using static Heirloom.Sound.LowLevel.Backends.MiniAudio.NativeApi;

namespace Heirloom.Sound.LowLevel
{
    public abstract unsafe class AudioProcessor : IDisposable
    {
        private readonly void* _device;
        private readonly DataProcessCallback _dataProc;

        #region Constructors

        public AudioProcessor(AudioDeviceMode mode, uint sampleRate = 22050)
        {
            SampleRate = sampleRate;

            DeviceType type = 0;
            if (mode.HasFlag(AudioDeviceMode.Playback)) { type |= DeviceType.Playback; }
            if (mode.HasFlag(AudioDeviceMode.Capture)) { type |= DeviceType.Capture; }

            // Allocate device config
            var deviceConfig = ma_ext_alloc_device_config(type, SampleRate,
                _dataProc = (void* pDevice, void* pOutput, void* pInput, uint frameCount) =>
                { 
                    // Cast to short pointers (S16)
                    var pOutputFrames = (short*) pOutput;
                    var pInputFrames = (short*) pInput;

                    // Process audio (read, decode, etc)
                    UpdateAudioStream(pOutputFrames, pInputFrames, frameCount);
                });

            // Allocate device data and initialize
            _device = ma_ext_alloc_device();
            var result = ma_device_init(null, deviceConfig, _device);
            if (result != Result.Success) { throw new InvalidOperationException("Unable to init device. " + result); }

            // Free device config, device has been initialized
            ma_ext_free(deviceConfig);
        }

        ~AudioProcessor()
        {
            Console.WriteLine("~AudioProcessor");
            Dispose(false);
        }

        #endregion

        #region Properties

        /// <summary>
        /// Has this device been disposed?
        /// Once disposed, this instance is useless.
        /// </summary>
        public bool IsDisposed { get; set; } = false;

        /// <summary>
        /// The sample rate this device was configured to use.
        /// </summary>
        public uint SampleRate { get; }

        /// <summary>
        /// The number of channels this device supports.
        /// </summary>
        public uint Channels => 2;

        #endregion

        #region Start / Stop Device

        public void Start()
        {
            // Start audio device
            var result = ma_device_start(_device);
            if (result != Result.Success)
            {
                throw new InvalidOperationException("Unable to start device. " + result);
            }
        }

        public void Stop()
        {
            // Start audio device
            var result = ma_device_stop(_device);
            if (result != Result.Success)
            {
                throw new InvalidOperationException("Unable to stop device. " + result);
            }
        }

        #endregion

        protected abstract void UpdateAudioStream(short* pOutputFrames, short* pInputFrames, uint frameCount);

        #region Dispose

        protected virtual void Dispose(bool disposeManaged)
        {
            if (!IsDisposed)
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

                IsDisposed = true;
            }
        }

        // This code added to correctly implement the disposable pattern.
        public void Dispose()
        {
            Dispose(true);
            GC.SuppressFinalize(this);
        }

        #endregion
    }
}
