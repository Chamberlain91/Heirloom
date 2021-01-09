namespace Heirloom.OpenGLES
{
    internal enum TextureWrap
    {
        /// <summary>
        /// Sampling clamps at the edge of the texture.
        /// </summary>
        Clamp = 0x812F,

        /// <summary>
        /// Sampling repeats beyond the edge of the texture.
        /// </summary>
        Repeat = 0x2901,

        /// <summary>
        /// Sampling repeats ( but mirrored ) beyond the edge of the texture.
        /// </summary>
        MirrorRepeat = 0x8370
    }
}
