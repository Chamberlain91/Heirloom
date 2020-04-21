using System;

namespace Heirloom.OpenGLES
{
    [Flags]
    internal enum ClientWaitSyncFlags : uint
    {
        SYNC_FLUSH_COMMANDS_BIT = 0x00000001
    }
}
