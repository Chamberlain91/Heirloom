namespace Meadows.Drawing.OpenGLES
{
    internal enum BlendEquation : uint
    {
        /// <summary>
        /// Result = Source + Destination
        /// </summary>
        Add = 0x8006,

        /// <summary>
        /// Result = Source - Destination
        /// </summary>
        Subtract = 0x800A,

        /// <summary>
        /// Result = Destination - Source
        /// </summary>
        ReverseSubtract = 0x800B
    }
}
