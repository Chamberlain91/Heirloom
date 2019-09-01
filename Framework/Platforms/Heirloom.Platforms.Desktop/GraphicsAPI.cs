namespace Heirloom.Platforms.Desktop
{
    internal enum GraphicsAPI
    // todo: make public somehow when implemented vulkan
    {
        /// <summary>
        /// Automatically chooses for the platform.
        /// </summary>
        Automatic,

        /// <summary>
        /// Rendering powered by OpenGL
        /// </summary>
        OpenGL,

        /// <summary>
        /// Rendering powered by Vulkan
        /// </summary>
        Vulkan
    };
}
