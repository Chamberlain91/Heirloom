namespace Heirloom
{
    /// <summary>
    /// Controls showing the performance overlay on a <see cref="GraphicsContext"/> object.
    /// </summary>
    public enum PerformanceOverlayMode
    {
        /// <summary>
        /// Hides the performance overlay.
        /// </summary>
        Disabled,

        /// <summary>
        /// Displays FPS as a short string.
        /// </summary>
        Simple,

        /// <summary>
        /// Displays FPS, batch count and draw count. 
        /// </summary>
        Standard,

        /// <summary>
        /// Displays FPS, batch count and draw count with std deviation.
        /// </summary>
        Full
    }
}
