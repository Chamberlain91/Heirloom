namespace Heirloom.Sound.Backends.MiniAudio
{
    internal enum ChannelMixMode
    {
        Rectangular = 0,   /* Simple averaging based on the plane(s) the channel is sitting on. */
        Simple,            /* Drop excess channels; zeroed out extra channels. */
        CustomWeights,     /* Use custom weights specified in ma_channel_router_config. */
        PlanarBlend = Rectangular,
        Default = PlanarBlend
    }
}
