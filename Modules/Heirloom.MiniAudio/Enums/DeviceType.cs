namespace Heirloom.MiniAudio
{
    internal enum DeviceType
    {
        Playback = 1,
        Capture = 2,
        Duplex = Playback | Capture,
        Loopback = 4
    }
}
