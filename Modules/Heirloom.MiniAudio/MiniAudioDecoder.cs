using System;
using System.IO;

using Heirloom.Sound;

using static Heirloom.MiniAudio.NativeApi;

namespace Heirloom.MiniAudio
{
    /// <summary>
    /// An object to assist with converting audio formats into raw PCM frames.
    /// </summary>
    internal sealed unsafe class MiniAudioDecoder : IAudioDecoder
    {
        private readonly void* _decoder;
        private readonly DecoderReadCallback _readProc;
        private readonly DecoderSeekCallback _seekProc;
        private byte[] _readBuffer;
        private readonly Stream _stream;

        #region Constructors

        /// <summary>
        /// Constructs a new decoder from the given stream in one of the supported formats.
        /// </summary>
        /// <param name="stream">A stream to a file or streaming audio source in one of the supported formats.</param> 
        public MiniAudioDecoder(Stream stream)
        {
            _stream = stream;

            // We want signed 2 channel 16 bit audio
            var config = ma_decoder_config_init(SampleFormat.S16, (uint) AudioAdapter.Channels, (uint) AudioAdapter.SampleRate);

            // Construct decoder
            _decoder = ma_ext_alloc_decoder();
            var result = ma_decoder_init(_readProc = ReadBytes, _seekProc = SeekBytes, null, &config, _decoder);
            if (result != Result.Success)
            {
                throw new InvalidOperationException($"Unable to initialize decoder. (Error: {result})");
            }

            // The length is represented in pcm frames
            Length = (int) ma_decoder_get_length_in_pcm_frames(_decoder) * 2;
        }

        ~MiniAudioDecoder()
        {
            Dispose(false);
        }

        #endregion

        /// <inheritdoc/>
        public bool IsDisposed { get; private set; }

        /// <inheritdoc/>
        public int Length { get; }

        #region Decoder Callbacks

        private ulong ReadBytes(void* _, void* pWriteBuffer, ulong bytesToRead)
        {
            if (_stream.CanRead)
            {
                // Read the next chunk of bytes
                var size = (int) bytesToRead;
                if ((_readBuffer?.Length ?? 0) < size) { Array.Resize(ref _readBuffer, size); }
                var read = _stream.Read(_readBuffer, 0, size);

                // Copy from read buffer to write buffer
                fixed (byte* pReadBuffer = _readBuffer)
                {
                    Buffer.MemoryCopy(pReadBuffer, pWriteBuffer, size, read);
                }

                return (ulong) read;
            }
            else
            {
                // Was not able to read
                return 0;
            }
        }

        private int SeekBytes(void* _, int byteOffset, SeekOrigin origin)
        {
            if (_stream.CanSeek)
            {
                if (origin == SeekOrigin.FromCurrent) { _stream.Seek(byteOffset, System.IO.SeekOrigin.Current); }
                else { _stream.Seek(byteOffset, System.IO.SeekOrigin.Begin); }

                return 1;
            }
            else
            {
                // Unable to seek
                return 0;
            }
        }

        #endregion

        #region Decoding

        /// <summary>
        /// Decodes the next several samples.
        /// </summary>
        public int Decode(Span<short> samples)
        {
            var count = samples.Length;

            fixed (short* pSamples = samples)
            {
                return (int) ma_decoder_read_pcm_frames(_decoder, pSamples, (ulong) (count / AudioAdapter.Channels)) * AudioAdapter.Channels;
            }
        }

        /// <summary>
        /// Seek to start decoding at the given offset.
        /// </summary>
        public bool Seek(int offset)
        {
            var result = ma_decoder_seek_to_pcm_frame(_decoder, (ulong) offset);

            // Log error...
            if (result != Result.Success)
            {
                Log.Warning($"Unable to seek decoder: {result}");
            }

            // Return true on success
            return result == Result.Success;
        }

        #endregion

        #region Dispose

        private void Dispose(bool disposeManaged)
        {
            if (!IsDisposed)
            {
                if (disposeManaged)
                {
                    _stream.Dispose();
                }

                // 
                ma_decoder_uninit(_decoder);
                ma_ext_free(_decoder);

                GC.KeepAlive(_readProc);
                GC.KeepAlive(_seekProc);

                IsDisposed = true;
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
