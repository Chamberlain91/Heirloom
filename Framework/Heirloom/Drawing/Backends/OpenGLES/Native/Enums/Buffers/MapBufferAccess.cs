using System;

namespace Heirloom.Drawing.OpenGLES
{
    [Flags]
    internal enum MapBufferAccess : uint
    {
        Read = 0x0001,
        Write = 0x0002,
        InvalidateRange = 0x0004,
        InvalidateBuffer = 0x0008,
        FlushExplicit = 0x0010,
        Unsynchronized = 0x0020
    }
}
