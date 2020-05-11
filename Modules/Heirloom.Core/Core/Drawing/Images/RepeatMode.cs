namespace Heirloom
{
    /// <summary>
    /// Represents the behaviour when sampling an image outside its natural bounds.
    /// </summary>
    public enum RepeatMode
    {
        /// <summary>
        /// Sampling coordinates outside image return transparent black.
        /// </summary>
        Blank = 0,

        /// <summary>
        /// Sampling coordinates outside image bounds cause the image to be repeated.
        /// </summary>
        Repeat = 1,

        /// <summary>
        /// Sampling coordinates are clamped to image bounds.
        /// </summary>
        Clamp = 2
    }
}
