namespace Heirloom.OpenGLES
{
    public enum StencilOperation : uint
    {
        /// <summary>
        /// Keeps the current value.
        /// </summary>
        Keep = 0x1E00,

        /// <summary>
        /// Sets the stencil buffer value to 0.
        /// </summary>
        Zero = 0,

        /// <summary>
        /// Sets the stencil buffer value to ref, as specified by <see cref="GL.StencilFunc(StencilFunction, int, uint)"/>.
        /// </summary>
        Replace = 0x1E01,

        /// <summary>
        /// Increments the current stencil buffer value. Clamps to the maximum representable unsigned value.
        /// </summary>
        Increment = 0x1E02,

        /// <summary>
        /// Increments the current stencil buffer value. Wraps stencil buffer value to zero when incrementing the maximum representable unsigned value.
        /// </summary>
        IncrementWrap = 0x8507,

        /// <summary>
        /// Decrements the current stencil buffer value. Clamps to 0.
        /// </summary>
        Decrement = 0x1E03,

        /// <summary>
        /// Decrements the current stencil buffer value. Wraps stencil buffer value to the maximum representable unsigned value when decrementing a stencil buffer value of zero.
        /// </summary>
        DecrementWrap = 0x8508,

        /// <summary>
        /// Bitwise inverts the current stencil buffer value.
        /// </summary>
        Invert = 0x150A
    }
}
