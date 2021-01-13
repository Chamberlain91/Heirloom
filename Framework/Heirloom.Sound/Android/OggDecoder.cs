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

        public OggDecoder(byte[] data)
        {
            // Copy ogg file to unmanaged memory
            _data = (void*) Marshal.AllocHGlobal(data.Length);
            Marshal.Copy(data, 0, (IntPtr) _data, data.Length);
            _dataSize = data.Length;

            // Prepare stb vorbis decoder
            _ptr = NativeDecoder.stb_vorbis_open_memory(_data, _dataSize, null, null);
            if (_ptr == null)
            {
                throw new InvalidOperationException("Unable to initialize ogg decoder");
            }

            // Extract sample rate and channel info
            var info = NativeDecoder.stb_vorbis_get_info(_ptr);
            SampleRate = (int) info.sample_rate;
            Channels = info.channels;

            Log.Debug($"Creating OGG Decoder (rate: {SampleRate} channels: {Channels})");

            // Gets the number of samples
            _length = NativeDecoder.stb_vorbis_stream_length_in_samples(_ptr);
        }

        ~OggDecoder()
        {
            Dispose(disposing: false);
        }

        public int SampleRate { get; private set; }

        public int Channels { get; private set; }

        public int Length => (int) _length;

        public bool IsDisposed => _isDisposed;

        public bool Seek(int offset)
        {
            return NativeDecoder.stb_vorbis_seek(_ptr, offset);
        }

        public int Decode(Span<short> samples)
        {
            fixed (short* p_samples = samples)
            {
                return NativeDecoder.stb_vorbis_get_samples_short_interleaved(_ptr, AudioBackend.Channels, p_samples, samples.Length) * AudioBackend.Channels;
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

                Log.Warning("Disposing OGG Decoder");

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
