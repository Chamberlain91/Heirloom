using System;
using System.IO;

namespace Heirloom.Sound
{
    internal abstract class AudioImplementation : IDisposable
    {
        ~AudioImplementation()
        {
            Dispose(disposing: false);
        }

        internal abstract IAudioDecoder CreateDecoder(Stream stream);

        internal abstract AudioDevice GetDefaultPlaybackDevice();

        internal abstract AudioDevice GetDefaultCaptureDevice();

        internal abstract AudioDevice[] GetPlaybackDevices();

        internal abstract AudioDevice[] GetCaptureDevices();

        internal abstract void UsePlaybackDevice(AudioDevice device);

        internal abstract void UseCaptureDevice(AudioDevice device);

        private bool _isDisposed;

        protected virtual void Dispose(bool disposing)
        {
            if (!_isDisposed)
            {
                if (disposing)
                {
                    // 
                }

                _isDisposed = true;
            }
        }

        public void Dispose()
        {
            Dispose(disposing: true);
            GC.SuppressFinalize(this);
        }
    }
}
