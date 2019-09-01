using System;
using System.IO;

using Heirloom.Sound.LowLevel.Backends.MiniAudio;

using static Heirloom.Sound.LowLevel.Backends.MiniAudio.NativeApi;

namespace Heirloom.Sound.LowLevel
{
    /// <summary>
    /// An object to assist with converting audio formats into raw PCM frames.
    /// </summary>
    public unsafe sealed class AudioDecoder : IDisposable
    {
        private const int StereoChannelCount = 2;

        private readonly void* _decoder;
        private readonly DecoderReadCallback _readProc;
        private readonly DecoderSeekCallback _seekProc;
        private readonly Stream _stream;

        private byte[] _readBuffer;

        #region Constructors

        /// <summary>
        /// Constructs a new decoder from the given stream in one of the supported formats.
        /// </summary>
        /// <param name="stream">A stream to a file or streaming audio source in one of the supported formats.</param>
        /// <param name="sampleRate">The number of samples per second to decode this stream, will resample if not matching source.</param>
        public AudioDecoder(Stream stream, uint sampleRate = 22050)
        {
            SampleRate = sampleRate;

            _stream = stream;

            // We want signed 2 channel 16 bit audio
            var config = ma_decoder_config_init(SampleFormat.S16, StereoChannelCount, SampleRate);

            // Construct decoder
            _decoder = ma_ext_alloc_decoder();
            var result = ma_decoder_init(_readProc = ReadBytes, _seekProc = SeekBytes, null, &config, _decoder);
            if (result != Result.Success)
            {
                throw new InvalidOperationException($"Unable to initialize decoder. (Error: {result})");
            }

            // The length is represented in pcm frames
            Length = ma_decoder_get_length_in_pcm_frames(_decoder);
        }

        /// <summary>
        /// Constructs a new decoder instance from an in memory copy of one of the supported file formats.
        /// </summary>
        /// <param name="file">An in memory copy of a file in one of the supported formats.</param>
        /// <param name="sampleRate">The number of samples per second to decode this stream, will resample if not matching source.</param>
        public AudioDecoder(byte[] file, uint sampleRate = 22050)
            : this(new MemoryStream(file), sampleRate)
        { }

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
        public bool IsDisposed { get; set; } = false;

        /// <summary>
        /// The sample rate this decoder was configured to use.
        /// </summary>
        public uint SampleRate { get; }

        /// <summary>
        /// The number of channels this decoder supports.
        /// </summary>
        public uint Channels => StereoChannelCount;

        /// <summary>
        /// Length of the pcm frames known to the decoder. May be zero if a stream or an unknown to the audio format.
        /// </summary>
        public ulong Length { get; }

        #endregion

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

        private int SeekBytes(void* _, int byteOffset, LowLevel.Backends.MiniAudio.SeekOrigin origin)
        {
            if (_stream.CanSeek)
            {
                if (origin == LowLevel.Backends.MiniAudio.SeekOrigin.FromCurrent) { _stream.Seek(byteOffset, System.IO.SeekOrigin.Current); }
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
        /// Decodes the next several frames.
        /// </summary>
        public ulong DecodeFrames(short* samples, ulong count)
        {
            return ma_decoder_read_pcm_frames(_decoder, samples, count);
        }

        /// <summary>
        /// Decodes the next several frames.
        /// </summary>
        public ulong DecodeFrames(short[] samples, ulong offset, ulong count)
        {
            fixed (short* pSamples = samples)
            {
                return DecodeFrames(pSamples + offset, count);
            }
        }

        /// <summary>
        /// Seek to start decoding at the given frame offset.
        /// </summary>
        public bool SeekToFrame(ulong frameOffset)
        {
            var result = ma_decoder_seek_to_pcm_frame(_decoder, frameOffset);

            // Log error...
            if (result != Result.Success)
            {
                Console.WriteLine($"Unable to seek decoder: {result}");
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
