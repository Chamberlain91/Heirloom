namespace Heirloom.OpenGLES
{
    internal enum ReadPixelsFormat : uint
    {
        R = 0x1903,
        R_Integer = 0x8D94,
        RG = 0x8227,
        RG_Integer = 0x8228,
        RGB = 0x1907,
        RGB_Integer = 0x8D98,
        RGBA = 0x1908,
        RGBAInteger = 0x8D99,
        //
        Luminance = 0x1909,
        LuminanceAlpha = 0x190A,
        ALPHA = 0x1906,
    }
}
