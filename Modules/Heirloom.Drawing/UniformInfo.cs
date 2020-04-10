using Heirloom.Math;

namespace Heirloom.Drawing
{
    public sealed class UniformInfo
    {
        public UniformInfo(string name, UniformType type, IntSize dimensions, int arraySize)
        {
            Name = name;
            Type = type;
            Dimensions = dimensions;
            ArraySize = arraySize;
        }

        // ie, uStrength
        public string Name { get; }

        // ie, float, image, etc
        public UniformType Type { get; }

        // ie, float 1x3 => vec3
        public IntSize Dimensions { get; }

        // ie, 2 => float[2]
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
