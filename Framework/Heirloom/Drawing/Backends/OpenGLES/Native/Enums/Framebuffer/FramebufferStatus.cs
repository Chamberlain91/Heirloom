namespace Heirloom.Drawing.OpenGLES
{
    internal enum FramebufferStatus
    {
        Complete = 0x8CD5,
        IncompleteAttachment = 0x8CD6,
        IncompleteDimensions = 0x8CD9,
        IncompleteMissingAttachment = 0x8CD7,
        IncompleteMultisample = 0x8D56,
        Unsupported = 0x8CDD
    }
}
