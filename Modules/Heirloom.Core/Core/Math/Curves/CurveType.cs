namespace Heirloom
{
    /// <summary>
    /// Represents the type of curve.
    /// </summary>
    public enum CurveType
    {
        /// <summary>
        /// A linear curve (aka, a line).
        /// </summary>
        /// <remarks>
        /// The handles in <see cref="Curve"/> do not affect this curve type.
        /// </remarks>
        Linear,

        /// <summary>
        /// A quadratic curve (aka, parabola).
        /// </summary>
        /// <remarks>
        /// Only the incoming handle in <see cref="Curve"/> affects this curve type.
        /// </remarks>
        Quadratic,

        /// <summary>
        /// A cubic curve.
        /// </summary>
        /// <remarks>
        /// Both handles in <see cref="Curve"/> affect this curve type.
        /// </remarks>
        Cubic,

        /// <summary>
        /// A stepped curve (ie, constant value).
        /// </summary>
        /// <remarks>
        /// The handles in <see cref="Curve"/> do not affect this curve type.
        /// </remarks>
        Stepped
    }
}
