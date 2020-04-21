namespace Heirloom.Backends.MiniAudio
{
    internal unsafe struct DecoderConfig
    {
        public SampleFormat Format; /* Set to 0 or ma_format_unknown to use the stream's internal format. */
        public uint Channels;       /* Set to 0 to use the stream's internal channels. */
        public uint SampleRate;     /* Set to 0 to use the stream's internal sample rate. */
        public fixed byte ChannelMap[NativeApi.MA_MAX_CHANNELS];
        public ChannelMixMode ChannelMixMode;
        public DitherMode DitherMode;
        public SrcAlgorithm SrcAlgorithm;
        public void* Sinc; // ??
    }
}
