namespace Meadows.Drawing
{
    /// <summary>
    /// Configures how a <see cref="Gradient"/> will interpolate colors.
    /// </summary>
    public enum GradientMode
    {
        /// <summary>
        /// A simple linear interpolation of RGB components.
        /// </summary>
        RGB,

        /// <summary>
        /// Converts interpolated colors into CIELab space for interpolation.
        /// </summary>
        CIELab
    };
}
