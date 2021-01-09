using System;

namespace Heirloom.Drawing.OpenGLES
{
    [Flags]
    internal enum ClientWaitSyncFlags : uint
    {
        SyncFlushCommands = 0x00000001
    }
}
