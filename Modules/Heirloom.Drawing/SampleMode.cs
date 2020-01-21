namespace Heirloom.Drawing
{
    public enum SampleMode
    {
        /// <summary>
        /// Color is sampled using rounding to the nearest pixel.
        /// </summary>
        Nearest,

        /// <summary>
        /// Color is sampled using interpolation across nearest pixels.
        /// </summary>
        Linear
    }
}
