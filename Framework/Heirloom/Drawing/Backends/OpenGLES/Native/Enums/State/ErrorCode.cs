namespace Heirloom.Drawing.OpenGLES
{
    internal enum ErrorCode : uint
    {
        NoError = 0,
        InvalidEnum = 0x0500,
        InvalidValue = 0x0501,
        InvalidOperation = 0x0502,
        InvalidFramebufferOperation = 0x0506,
        OutOfMemory = 0x0505
    }
}
