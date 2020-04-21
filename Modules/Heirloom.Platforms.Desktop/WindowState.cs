namespace Heirloom.Platforms.Desktop
{
    /// <summary>
    /// Describes the size state a window can be in.
    /// </summary>
    public enum WindowState
    {
        /// <summary>
        /// Window is neither minimized or maximized.
        /// </summary>
        Normal,

        /// <summary>
        /// Window has been minimized.
        /// </summary>
        Minimized,

        /// <summary>
        /// Window has been maximized.
        /// </summary>
        Maximized,

        /// <summary>
        /// Window is fullscreen.
        /// </summary>
        Fullscreen
    }
}
