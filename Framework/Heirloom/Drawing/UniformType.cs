namespace Heirloom.Drawing
{
    /// <summary>
    /// Represents the type of a uniform in a <see cref="Shader"/>.
    /// </summary>
    internal enum UniformType
    {
        /// <summary>
        /// The uniform is a float type.
        /// </summary>
        Float,

        /// <summary>
        /// The uniform is a integer type.
        /// </summary>
        Integer,

        /// <summary>
        /// The uniform is a unsigned integer type.
        /// </summary>
        UnsignedInteger,

        /// <summary>
        /// The uniform is a boolean type.
        /// </summary>
        Bool,

        /// <summary>
        /// The uniform is a image (ie, sampler2D) type.
        /// </summary>
        Sampler2D
    }
}
