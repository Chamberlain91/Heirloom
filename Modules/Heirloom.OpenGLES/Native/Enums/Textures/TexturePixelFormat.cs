namespace Heirloom.OpenGLES
{
    internal enum TexturePixelFormat : uint
    {
        R = 0x1903,
        RG = 0x8227,
        RGB = 0x1907,
        RGBA = 0x1908,

        RInteger = 0x8D94,
        RGInteger = 0x8228,
        RGBInteger = 0x8D98,
        RGBAInteger = 0x8D99,

        Alpha = 0x1906,
        Luminance = 0x1909,
        LuminanceAlpha = 0x190A,
        Depth = 0x1902,
        DepthStencil = 0x84F9,
    }
}
