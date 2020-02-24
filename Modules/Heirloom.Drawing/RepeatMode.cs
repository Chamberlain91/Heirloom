namespace Heirloom.Drawing
{
    public enum RepeatMode
    {
        /// <summary>
        /// Sampling coordinates are clamped to image bounds.
        /// </summary>
        Clamp,

        /// <summary>
        /// Sampling coordinates outside image bounds cause the image to be repeated.
        /// </summary>
        Repeat
    }
}
