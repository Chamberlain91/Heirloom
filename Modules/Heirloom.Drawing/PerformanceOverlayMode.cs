namespace Heirloom.Drawing
{
    /// <summary>
    /// Controls showing the performance overlay on a <see cref="Graphics"/> object.
    /// </summary>
    public enum PerformanceOverlayMode
    {
        /// <summary>
        /// Hides the performance overlay.
        /// </summary>
        Disabled,

        /// <summary>
        /// Displays FPS and batch count as a short string.
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
