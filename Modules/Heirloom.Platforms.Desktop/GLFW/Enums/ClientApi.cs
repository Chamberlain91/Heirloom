namespace Heirloom.Platforms.Desktop
{
    internal enum ClientApi
    {
        /// <summary>
        /// Window is created without an OpenGL context (useful for Vulkan windows).
        /// </summary>
        None = 0,

        /// <summary>
        /// Window is to be created with an OpenGL context.
        /// </summary>
        OpenGL = 0x00030001,

        /// <summary>
        /// Window is to be created with an OpenGL ES context.
        /// </summary>
        OpenGLES = 0x00030002,
    }
}
