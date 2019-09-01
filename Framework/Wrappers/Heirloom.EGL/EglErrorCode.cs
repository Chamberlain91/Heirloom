namespace Heirloom.OpenGLES.Platform
{
    public enum EglErrorCode : uint
    {
        Success = 0x3000,
        NotInitialized = 0x3001,
        BadAccess = 0x3002,
        BadAllocation = 0x3003,
        BadAttribute = 0x3004,
        BadContext = 0x3006,
        BadConfig = 0x3005,
        BadCurrentSurface = 0x3007,
        BadDisplay = 0x3008,
        BadSurface = 0x300D,
        BadMatch = 0x3009,
        BadParameter = 0x300C,
        BadNativePixmap = 0x300A,
        BadNativeWindow = 0x300B,
        ContextLost = 0x300E
    }
}
