using System;

namespace Meadows.Drawing.OpenGLES
{
    [Flags]
    internal enum ClientWaitSyncFlags : uint
    {
        SyncFlushCommands = 0x00000001
    }
}
