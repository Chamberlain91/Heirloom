using System.Runtime.InteropServices;
using System.Security;

namespace Heirloom.Sound.Backends.MiniAudio
{
    internal static unsafe class NativeApi
    {
#pragma warning disable IDE1006 // Naming Styles

        public const int MA_MIN_PCM_SAMPLE_SIZE_IN_BYTES = 1;
        public const int MA_MAX_PCM_SAMPLE_SIZE_IN_BYTES = 8;
        public const int MA_MIN_CHANNELS = 1;
        public const int MA_MAX_CHANNELS = 32;
        public const int MA_MIN_SAMPLE_RATE = (int) SampleRate.RATE_8000;
        public const int MA_MAX_SAMPLE_RATE = (int) SampleRate.RATE_384000;
        public const int MA_SRC_SINC_MIN_WINDOW_WIDTH = 2;
        public const int MA_SRC_SINC_MAX_WINDOW_WIDTH = 32;
        public const int MA_SRC_SINC_DEFAULT_WINDOW_WIDTH = 32;
        public const int MA_SRC_SINC_LOOKUP_TABLE_RESOLUTION = 8;

        [SuppressUnmanagedCodeSecurity]
        [UnmanagedFunctionPointer(CallingConvention.Cdecl)]
        public delegate void DataProcessCallback(void* pDevice, void* pOutput, void* pInput, uint frameCount);

        [SuppressUnmanagedCodeSecurity]
        [UnmanagedFunctionPointer(CallingConvention.Cdecl)]
        public delegate ulong DecoderReadCallback(void* pDecoder, void* pBufferOut, ulong bytesToRead);

        [SuppressUnmanagedCodeSecurity]
        [UnmanagedFunctionPointer(CallingConvention.Cdecl)]
        public delegate int DecoderSeekCallback(void* pDecoder, int byteOffset, SeekOrigin origin);

        [DllImport("miniaudio", CallingConvention = CallingConvention.Cdecl)]
        public static extern DecoderConfig ma_decoder_config_init(SampleFormat outputFormat, uint outputChannels, uint outputSampleRate);

        [DllImport("miniaudio", CallingConvention = CallingConvention.Cdecl)]
        public static extern Result ma_decoder_init_memory(void* data, ulong dataLen, DecoderConfig* pConfig, void* pDecoder);

        [DllImport("miniaudio", CallingConvention = CallingConvention.Cdecl)]
        public static extern Result ma_decoder_init(DecoderReadCallback onRead, DecoderSeekCallback onSeek, void* pUserData, DecoderConfig* pConfig, void* pDecoder);

        [DllImport("miniaudio", CallingConvention = CallingConvention.Cdecl)]
        public static extern Result ma_decoder_uninit(void* pDecoder);

        [DllImport("miniaudio", CallingConvention = CallingConvention.Cdecl)]
        public static extern ulong ma_decoder_read_pcm_frames(void* decoder, void* framesOut, ulong frameCount);

        [DllImport("miniaudio", CallingConvention = CallingConvention.Cdecl)]
        public static extern Result ma_decoder_seek_to_pcm_frame(void* decoder, ulong frame);

        [DllImport("miniaudio", CallingConvention = CallingConvention.Cdecl)]
        public static extern ulong ma_decoder_get_length_in_pcm_frames(void* decoder);

        [DllImport("miniaudio", CallingConvention = CallingConvention.Cdecl)]
        public static extern Result ma_device_init(void* context, void* config, void* device);

        [DllImport("miniaudio", CallingConvention = CallingConvention.Cdecl)]
        public static extern void ma_device_uninit(void* device);

        [DllImport("miniaudio", CallingConvention = CallingConvention.Cdecl)]
        public static extern Result ma_device_start(void* device);

        [DllImport("miniaudio", CallingConvention = CallingConvention.Cdecl)]
        public static extern Result ma_device_stop(void* device);

        #region MiniAudio Extensions

        [DllImport("miniaudio", CallingConvention = CallingConvention.Cdecl)]
        public static extern void* ma_ext_alloc_decoder();

        [DllImport("miniaudio", CallingConvention = CallingConvention.Cdecl)]
        public static extern void* ma_ext_alloc_device();

        [DllImport("miniaudio", CallingConvention = CallingConvention.Cdecl)]
        public static extern void* ma_ext_alloc_device_config(DeviceType deviceType, uint sampleRate, DataProcessCallback dataCallback);

        [DllImport("miniaudio", CallingConvention = CallingConvention.Cdecl)]
        public static extern void ma_ext_free(void* ptr);

        [DllImport("miniaudio", CallingConvention = CallingConvention.Cdecl)]
        public static extern uint ma_ext_get_decoder_sample_rate(void* decoder);

        [DllImport("miniaudio", CallingConvention = CallingConvention.Cdecl)]
        public static extern uint ma_ext_get_decoder_channels(void* decoder);

        [DllImport("miniaudio", CallingConvention = CallingConvention.Cdecl)]
        public static extern SampleFormat ma_ext_get_decoder_format(void* decoder);


        #endregion

#pragma warning restore IDE1006 // Naming Styles
    }
}
