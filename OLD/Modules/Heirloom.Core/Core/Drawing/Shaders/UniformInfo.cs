namespace Heirloom
{
    /// <summary>
    /// Contains information of a uniform from a <see cref="Shader"/>.
    /// </summary>
    /// <category>Shaders and Effects</category>
    public sealed class UniformInfo
    {
        internal UniformInfo(string name, UniformType type, IntSize dimensions, int arraySize)
        {
            Name = name;
            Type = type;
            Dimensions = dimensions;
            ArraySize = arraySize;
        }

        /// <summary>
        /// The name of this uniform.
        /// </summary>
        public string Name { get; }

        /// <summary>
        /// The type of this uniform.
        /// </summary>
        public UniformType Type { get; }

        /// <summary>
        /// The dimensions of this uniform.
        /// </summary>
        public IntSize Dimensions { get; }

        /// <summary>
        /// The array size of this uniform.
        /// </summary>
        public int ArraySize { get; }

        /// <summary>
        /// Is this uniform a vector?
        /// </summary>
        public bool IsVector => Dimensions.Width == 1 && Dimensions.Height > 1;

        /// <summary>
        /// Is this uniform a matrix?
        /// </summary>
        public bool IsMatrix => Dimensions.Width > 1 && Dimensions.Height > 1;

        /// <summary>
        /// Is this uniform an array?
        /// </summary>
        public bool IsArray => ArraySize > 1;
    }
}
