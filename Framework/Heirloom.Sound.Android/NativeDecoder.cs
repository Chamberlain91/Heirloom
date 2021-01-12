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

#pragma warning restore IDE1006 // Naming Styles
#pragma warning restore CS0649  // Default value null
#pragma warning restore CS0169  // Unassigned
    }
}
