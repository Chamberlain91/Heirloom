using Heirloom.Mathematics;

namespace Heirloom.Drawing
{
    public sealed class Uniform
    {
        internal Uniform(int location, string name, UniformType type, IntSize dimensions, int arraySize)
        {
            Location = location;

            Name = name;
            Type = type;
            Dimensions = dimensions;
            ArraySize = arraySize;
        }

        /// <summary>
        /// The uniform location.
        /// </summary>
        internal int Location { get; }

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
        /// <remarks>
        /// (1, N)
        /// </remarks>
        public bool IsVector => Dimensions.Width == 1 && Dimensions.Height > 1;

        /// <summary>
        /// Is this uniform a matrix?
        /// </summary>
        /// <remarks>
        /// (M, N)
        /// </remarks>
        public bool IsMatrix => Dimensions.Width > 1 && Dimensions.Height > 1;

        /// <summary>
        /// Is this uniform an array?
        /// </summary>
        public bool IsArray => ArraySize > 1;
    }
}
