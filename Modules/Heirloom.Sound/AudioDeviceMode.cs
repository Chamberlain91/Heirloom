using System;

namespace Heirloom.Sound
{
    [Flags]
    public enum AudioDeviceMode
    {
        Playback, Capture,
        Both = Playback | Capture
    };
}
