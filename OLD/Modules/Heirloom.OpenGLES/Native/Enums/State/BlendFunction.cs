namespace Heirloom.OpenGLES
{
    internal enum BlendFunction : uint
    {
        Zero = 0,
        One = 1,

        SourceColor = 0x0300,
        OneMinusSourceColor = 0x0301,

        DestinationColor = 0x0306,
        OneMinusDestinationColor = 0x0307,

        SourceAlpha = 0x0302,
        OneMinusSourceAlpha = 0x0303,

        DestinationAlpha = 0x0304,
        OneMinusDestinationAlpha = 0x0305,

        ConstantColor = 0x8001,
        OneMinusConstantColor = 0x8002,

        ConstantAlpha = 0x8003,
        OneMinusConstantAlpha = 0x8004,

        SourceAlphaSaturate = 0x0308
    }
}
