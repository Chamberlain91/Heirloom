using System;
using System.IO;

namespace Heirloom
{
    internal abstract class AudioDecoder : IDisposable
    {
        protected readonly Stream Stream;

        #region Constructor

        /// <summary>
        /// Constructs a new decoder instance from an in memory copy of one of the supported file formats.
        /// </summary>
        /// <param name="file">An in memory copy of a file in one of the supported formats.</param>
        protected AudioDecoder(byte[] file)
            : this(new MemoryStream(file))
        { }

        /// <summary>
        /// Constructs a new decoder from the given stream in one of the supported formats.
        /// </summary>
        /// <param name="stream">A stream to a file or streaming audio source in one of the supported formats.</param> 
        protected AudioDecoder(Stream stream)
        {
            Stream = stream ?? throw new ArgumentNullException(nameof(stream));
        }

        ~AudioDecoder()
        {
            Dispose(false);
        }

        #endregion

        #region Properties

        /// <summary>
        /// Has this decoder been disposed?
        /// Once disposed, this instance is useless.
        /// </summary>
        public bool IsDisposed { get; protected set; } = false;

        /// <summary>
        /// Length of the pcm frames known to the decoder. May be zero if a stream or an unknown to the audio format.
        /// </summary>
        public int Length { get; protected set; }

        #endregion

        public abstract bool Seek(int offset);

        public abstract int Decode(short[] samples, long offset, long count);

        protected abstract void Dispose(bool disposeManaged);

        public void Dispose()
        {
            Dispose(true);
            GC.SuppressFinalize(this);
        }
    }
}
