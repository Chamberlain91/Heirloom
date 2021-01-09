namespace Heirloom.Extras.Aseprite
{
    /// <summary>
    /// Represents animation direction options.
    /// </summary>
    /// <category>Drawing</category>
    public enum AnimationDirection : byte
    {
        /// <summary>
        /// The animation will play from zero to the end.
        /// </summary>
        Forward,

        /// <summary>
        /// The animation will play from the end to zero.
        /// </summary>
        Reverse,

        /// <summary>
        /// The animation bounce between zero, the end and back to zero.
        /// </summary>
        PingPong
    }
}
