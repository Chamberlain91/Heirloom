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
