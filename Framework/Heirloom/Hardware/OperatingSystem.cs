namespace Heirloom
{
    /// <summary>
    /// An enum for the kind of operating system.
    /// </summary>
    public enum OperatingSystem
    {
        /// <summary>
        /// The operating system is unknown.
        /// </summary>
        Unknown,

        /// <summary>
        /// The operating system Microsoft Windows.
        /// </summary>
        Windows,

        /// <summary>
        /// The operating system is some variant of Linux.
        /// </summary>
        Linux,

        /// <summary>
        /// The operating system is macOS.
        /// </summary>
        /// <remarks>
        /// Please note: macOS is not officially supported (unless someone can help circumvent the OpenGL deprecation.).
        /// </remarks>
        OSX,

        /// <summary>
        /// The operating system is Android.
        /// </summary>
        Android
    }
}
