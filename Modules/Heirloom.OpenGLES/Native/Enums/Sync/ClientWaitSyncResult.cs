namespace Heirloom.OpenGLES
{
    internal enum ClientWaitSyncResult : uint
    {
        ALREADY_SIGNALED = 0x911A,
        TIMEOUT_EXPIRED = 0x911B,
        CONDITION_SATISFIED = 0x911C,
        WAIT_FAILED = 0x911D
    }
}
