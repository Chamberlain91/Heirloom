namespace Heirloom.Platforms.Desktop
{
    internal enum OpenGLProfile
    {
        /// <summary>
        /// Use whatever OpenGL profile is available.
        /// </summary>
        Any = 0,

        /// <summary>
        /// Use an OpenGL core profile.
        /// </summary>
        Core = 0x00032001,

        /// <summary>
        /// Use an OpenGL compatibility profile.
        /// </summary>
        Compatibility = 0x00032002
    }
}
