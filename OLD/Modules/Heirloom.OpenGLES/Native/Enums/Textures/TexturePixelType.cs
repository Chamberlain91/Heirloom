namespace Heirloom.OpenGLES
{
    internal enum TexturePixelType : uint
    {
        Byte = 0x1400,
        Short = 0x1402,
        Integer = 0x1404,

        UnsignedByte = 0x1401,
        UnsignedShort = 0x1403,
        UnsignedInteger = 0x1405,

        UnsignedShort_565 = 0x8363,
        UnsignedShort_4444 = 0x8033,
        UnsignedShort_5551 = 0x8034,

        Float = 0x1406,
        HalfFloat = 0x140B,

        // DepthStencil
        UnsignedInteger24_8 = 0x8CAD,
    }
}
