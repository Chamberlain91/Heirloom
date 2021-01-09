namespace Heirloom.Drawing.OpenGLES
{
    internal class ActiveUniform
    {
        /// <summary>
        /// Number of components of the uniform type.
        /// </summary>
        public readonly int Size;

        /// <summary>
        /// The type of uniform.
        /// </summary>
        public readonly ActiveUniformType Type;

        /// <summary>
        /// Index of this uniform.
        /// </summary>
        public readonly uint Index;

        /// <summary>
        /// Name of this uniform.
        /// </summary>
        public readonly string Name;

        /// <summary>
        /// Offset of this uniform within a block.
        /// </summary>
        public readonly int Offset;

        internal ActiveUniform(string name, uint index, int size, ActiveUniformType type, int offset)
        {
            Size = size;
            Index = index;
            Name = name;
            Type = type;
            Offset = offset;
        }

        public override string ToString()
        {
            if (Offset >= 0)
            {
                return $"{Index} \"{Name}\" {Size}*{Type} offset: {Offset} bytes";
            }
            else
            {
                return $"{Index} \"{Name}\" {Size}*{Type}";
            }
        }
    }
}
