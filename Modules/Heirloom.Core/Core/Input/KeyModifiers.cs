using System;

namespace Heirloom
{
    [Flags]
    public enum KeyModifiers
    {
        Shift = 1 << 0,
        Control = 1 << 2,
        Alt = 1 << 3,
        Super = 1 << 4,
        CapsLock = 1 << 5,
        NumLock = 1 << 6
    }
}
