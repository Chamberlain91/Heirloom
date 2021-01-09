namespace Heirloom.Drawing.OpenGLES
{
    internal class ActiveAttribute
    {
        /// <summary>
        /// Number of components of the attribute type.
        /// </summary>
        public readonly int Size;

        /// <summary>
        /// The type of attribute.
        /// </summary>
        public readonly ActiveAttribType Type;

        /// <summary>
        /// Name of this attribute.
        /// </summary>
        public readonly string Name;

        internal ActiveAttribute(string name, int size, ActiveAttribType type)
        {
            Size = size;
            Name = name;
            Type = type;
        }

        public override string ToString()
        {
            return $"\"{Name}\" {Size}*{Type}";
        }
    }
}
