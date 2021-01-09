namespace Meadows.Drawing
{
    public enum BlendingMode
    {
        /// <summary>
        /// Drawn pixels are fully opaque and will replace existing pixels.
        /// </summary>
        Opaque,

        /// <summary>
        /// Draw pixels are blended based on their alpha values with existing pixels.
        /// </summary>
        Alpha,

        /// <summary>
        /// Drawn pixels are additively blended based on their alpha values with existing pixels.
        /// </summary>
        Additive,

        /// <summary>
        /// Drawn pixels are subtractively blended based on their alpha values with existing pixels.
        /// </summary>
        Subtractive,

        /// <summary>
        /// Drawn pixels are multiplicatively blended based on their alpha values with existing pixels.
        /// </summary>
        Multiply,

        /// <summary>
        /// Drawn pixels act like an inversion filter with existing pixels.
        /// </summary>
        Invert
    }
}
