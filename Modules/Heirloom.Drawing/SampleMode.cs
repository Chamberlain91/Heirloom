namespace Heirloom.Drawing
{
    public enum SampleMode
    {
        /// <summary>
        /// Color is sampled using rounding to the nearest pixel.
        /// </summary>
        Point,

        /// <summary>
        /// Color is sampled using interpolation across nearest pixels.
        /// </summary>
        Linear
    }
}
