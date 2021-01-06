namespace Meadows.Drawing.OpenGLES
{
    internal enum ClientWaitSyncResult : uint
    {
        AlreadySignaled = 0x911A,
        TimeoutExpired = 0x911B,
        ConditionSatisfied = 0x911C,
        WaitFailed = 0x911D
    }
}
