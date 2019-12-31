namespace Heirloom.Desktop
{
    internal enum ContextCreationApi
    {
        /// <summary>
        /// Use the platform defined method of creating an OpenGL context.
        /// </summary>
        Native = 0x00036001,

        /// <summary>
        /// Use EGL to create the OpenGL context (on Linux this is the default option).
        /// </summary>
        EGL = 0x00036002,

        /// <summary>
        /// A special OpenGL context created using OSMesa, and framebuffer must be accessed using OSMesa specific features.
        /// </summary>
        OSMesa = 0x00036003
    }
}
