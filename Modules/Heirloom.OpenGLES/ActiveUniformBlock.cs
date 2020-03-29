using System.Collections.Generic;
using System.Linq;

namespace Heirloom.OpenGLES
{
    public class ActiveUniformBlock
    {
        /// <summary>
        /// Name of this block.
        /// </summary>
        public readonly string Name;

        /// <summary>
        /// Index of this block.
        /// </summary>
        public readonly uint Index;

        /// <summary>
        /// Size in bytes of this block.
        /// </summary>
        public readonly int DataSize;

        /// <summary>
        /// Mapping of active uniforms within this block.
        /// </summary>
        public readonly IReadOnlyDictionary<string, ActiveUniform> Uniforms;

        internal ActiveUniformBlock(string name, uint index, int dataSize, ActiveUniform[] blockUniforms)
        {
            Name = name;
            Index = index;
            DataSize = dataSize;
            Uniforms = blockUniforms.ToDictionary(u => u.Name);
        }

        public override string ToString()
        {
            return $"{Index} \"{Name}\" {DataSize} byte {Uniforms.Count} uniforms";
        }
    }
}
