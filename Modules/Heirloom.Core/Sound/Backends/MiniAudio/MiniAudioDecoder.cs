using System;
using System.IO;

using static Heirloom.Backends.MiniAudio.NativeApi;

namespace Heirloom.Backends.MiniAudio
{

    /// <summary>
    /// An object to assist with converting audio formats into raw PCM frames.
    /// </summary>
    internal sealed unsafe class MiniAudioDecoder : AudioDecoder
    {
        private readonly void* _decoder;
        private readonly DecoderReadCallback _readProc;
        private readonly DecoderSeekCallback _seekProc;
        private byte[] _readBuffer;

        #region Constructors

        /// <summary>
        /// Constructs a new decoder from the given stream in one of the supported formats.
        /// </summary>
        /// <param name="stream">A stream to a file or streaming audio source in one of the supported formats.</param> 
        public MiniAudioDecoder(Stream stream)
            : base(stream)
        {
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

        /// <summary>
        /// Constructs a new decoder instance from an in memory copy of one of the supported file formats.
        /// </summary>
        /// <param name="file">An in memory copy of a file in one of the supported formats.</param>
        public MiniAudioDecoder(byte[] file)
            : this(new MemoryStream(file))
        { }

        #endregion

        #region Decoder Callbacks

        private ulong ReadBytes(void* _, void* pWriteBuffer, ulong bytesToRead)
        {
            if (Stream.CanRead)
            {
                // Read the next chunk of bytes
                var size = (int) bytesToRead;
                if ((_readBuffer?.Length ?? 0) < size) { Array.Resize(ref _readBuffer, size); }
                var read = Stream.Read(_readBuffer, 0, size);

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

        private int SeekBytes(void* _, int byteOffset, Backends.MiniAudio.SeekOrigin origin)
        {
            if (Stream.CanSeek)
            {
                if (origin == Backends.MiniAudio.SeekOrigin.FromCurrent) { Stream.Seek(byteOffset, System.IO.SeekOrigin.Current); }
                else { Stream.Seek(byteOffset, System.IO.SeekOrigin.Begin); }

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
        public override int Decode(short[] samples, long offset, long count)
        {
            fixed (short* pSamples = samples)
            {
                return (int) ma_decoder_read_pcm_frames(_decoder, pSamples + offset, (ulong) (count / AudioAdapter.Channels)) * AudioAdapter.Channels;
            }
        }

        /// <summary>
        /// Seek to start decoding at the given offset.
        /// </summary>
        public override bool Seek(int offset)
        {
            var result = ma_decoder_seek_to_pcm_frame(_decoder, (ulong) offset);

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

        protected override void Dispose(bool disposeManaged)
        {
            if (!IsDisposed)
            {
                if (disposeManaged)
                {
                    Stream.Dispose();
                }

                // 
                ma_decoder_uninit(_decoder);
                ma_ext_free(_decoder);

                GC.KeepAlive(_readProc);
                GC.KeepAlive(_seekProc);

                IsDisposed = true;
            }
        }

        #endregion 
    }
}
