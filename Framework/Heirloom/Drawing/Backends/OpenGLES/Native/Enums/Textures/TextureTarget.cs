namespace Heirloom.Drawing.OpenGLES
{
    internal enum TextureTarget : uint
    {
        Texture2D = 0x0DE1,
        Texture2DMultisample = TextureImageTarget.Texture2DMultisample, // 3.1
        Texture2DArray = 0x8C1A,
        TextureCubeMap = 0x8513,
        Texture3D = 0x806F,
    }
}
