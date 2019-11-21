namespace Heirloom.OpenGLES
{
    public enum ReadPixelsType : uint
    {
        UnsignedByte = 0x1401,
        UnsignedInteger = 0x1405,
        Byte = 0x1400,
        HalfFloat = 0x140B,
        Float = 0x1406,

        // 
        UnsignedShort565 = 0x8363,
        UnsignedShort4444 = 0x8033,
        UnsignedShort5551 = 0x8034,

        // ??
        UnsignedInt2101010Rev = 0x8368,
        UnsignedInt10F11F11FRev = 0x8C3B,
        UnsignedInt5999Rev = 0x8C3E
    }
}
