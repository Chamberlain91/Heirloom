using System.Runtime.InteropServices;

namespace Heirloom.Sound
{
    public unsafe static class NativeDecoder
    {
#pragma warning disable IDE1006 // Naming Styles
#pragma warning disable CS0649  // Default value null
#pragma warning disable CS0169  // Unassigned

        #region Dr Mp3

        [DllImport("decoder", EntryPoint = "drmp3_init_memory")]
        public static extern bool drmp3_init_memory(void* pMP3, void* pData, int dataSize, void* allocationCallbacks);

        [DllImport("decoder", EntryPoint = "drmp3_uninit")]
        public static extern void drmp3_uninit(void* pMP3);

        [DllImport("decoder", EntryPoint = "drmp3_read_pcm_frames_s16")]
        public static extern ulong drmp3_read_pcm_frames_s16(void* pMP3, ulong framesToRead, short* pBufferOut);

        [DllImport("decoder", EntryPoint = "drmp3_seek_to_pcm_frame")]
        public static extern bool drmp3_seek_to_pcm_frame(void* pMP3, ulong frameIndex);

        [DllImport("decoder", EntryPoint = "drmp3_get_pcm_frame_count")]
        public static extern ulong drmp3_get_pcm_frame_count(void* pMP3);

        #endregion

        #region STB Vorbis

        [DllImport("decoder", EntryPoint = "stb_vorbis_open_memory")]
        public static extern void* stb_vorbis_open_memory(void* data, int len, void* error, void* alloc);

        [DllImport("decoder", EntryPoint = "stb_vorbis_get_info")]
        public static extern stb_vorbis_info stb_vorbis_get_info(void* vorbis);

        [DllImport("decoder", EntryPoint = "stb_vorbis_stream_length_in_samples")]
        public static extern uint stb_vorbis_stream_length_in_samples(void* vorbis);

        [DllImport("decoder", EntryPoint = "stb_vorbis_get_samples_short_interleaved")]
        public static extern int stb_vorbis_get_samples_short_interleaved(void* vorbis, int channels, short* buffer, int count);

        [DllImport("decoder", EntryPoint = "stb_vorbis_seek")]
        public static extern bool stb_vorbis_seek(void* vorbis, int sample);

        #endregion

        #region Helper Functions

        [DllImport("decoder", EntryPoint = "alloc_mp3_struct")]
        public static extern void* alloc_mp3_struct();

        [DllImport("decoder", EntryPoint = "alloc_flac_struct")]
        public static extern void* alloc_flac_struct();

        [DllImport("decoder", EntryPoint = "alloc_wav_struct")]
        public static extern void* alloc_wav_struct();

        [DllImport("decoder", EntryPoint = "free_struct")]
        public static extern void free_struct(void* ptr);

        #endregion

        public struct stb_vorbis_info
        {
            public uint sample_rate;
            public int channels;

            public uint setup_memory_required;
            public uint setup_temp_memory_required;
            public uint temp_memory_required;

            public int max_frame_size;
        }

#pragma warning restore IDE1006 // Naming Styles
#pragma warning restore CS0649  // Default value null
#pragma warning restore CS0169  // Unassigned

    }
}
