namespace Heirloom.GLFW3
{
    public enum ErrorCode : int
    {
        None = 0,
        NotInitialized = 0x10001,
        NoCurrentContext = 0x10002,
        InvalidEnum = 0x10003,
        InvalidValue = 0x10004,
        OutOfMemory = 0x10005,
        ApiUnavailable = 0x10006,
        VersionUnavailable = 0x10007,
        PlatformError = 0x10008,
        FormatUnavailable = 0x10009,
        NoWindowContext = 0x1000A
    }
}
