using System;
using System.Runtime.InteropServices;

namespace Heirloom.Sound.Android
{
#if ANDROID
    public unsafe sealed class OggDecoder : IAudioDecoder
    {
        private readonly void* _data;
        private readonly int _dataSize;

        private readonly void* _ptr;
        private readonly ulong _length;

        /*
            stb_vorbis vorbis;
            fixed (byte* b = data)
            {
                vorbis = stb_vorbis_open_memory(b, data.Length, null, null);
            }

            var info = stb_vorbis_get_info(vorbis);
            var length = stb_vorbis_stream_length_in_samples(vorbis);
            Log.Warning($"OGG is {length} samples.");

            var samples = new short[length];
            fixed (short* p_samples = samples)
            {
                var count = stb_vorbis_get_samples_short_interleaved(vorbis, 2, p_samples, (int) length);
                Log.Warning($"OGG read {count} samples.");
            }

            return samples;
         */

        public OggDecoder(byte[] data)
        {
            // Copy mp3 file to unmanaged memory
            _data = (void*) Marshal.AllocHGlobal(data.Length);
            Marshal.Copy(data, 0, (IntPtr) _data, data.Length);
            _dataSize = data.Length;

            // Initialize MP3 system
            _ptr = NativeDecoder.alloc_wav_struct();
            if (!NativeDecoder.drmp3_init_memory(_ptr, _data, _dataSize, null))
            {
                throw new InvalidOperationException("Unable to initialize mp3 decoder");
            }

            // Gets the number of frames
            _length = NativeDecoder.drmp3_get_pcm_frame_count(_ptr);
        }

        ~OggDecoder()
        {
            Dispose(disposing: false);
        }

        public int Length => (int) _length;

        public bool IsDisposed => _isDisposed;

        public bool Seek(int offset)
        {
            return NativeDecoder.drmp3_seek_to_pcm_frame(_ptr, (ulong) offset);
        }

        public int Decode(Span<short> samples)
        {
            fixed (short* p_samples = samples)
            {
                return (int) NativeDecoder.drmp3_read_pcm_frames_s16(_ptr, (ulong) samples.Length, p_samples);
            }
        }

        private bool _isDisposed;

        private void Dispose(bool disposing)
        {
            if (!_isDisposed)
            {
                if (disposing)
                {
                    // Nothing
                }

                Log.Warning("Disposing Mp3 Decoder");

                // Free native memory
                Marshal.FreeHGlobal((IntPtr) _data);
                NativeDecoder.free_struct(_ptr);

                _isDisposed = true;
            }
        }

        public void Dispose()
        {
            Dispose(disposing: true);
            GC.SuppressFinalize(this);
        }
    }
#endif
}
