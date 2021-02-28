namespace Heirloom.Drawing
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
        /// Converts colors into LAB color space before interpolation.
        /// </summary>
        LAB
    };
}
