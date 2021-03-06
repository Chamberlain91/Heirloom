using Heirloom.OpenGLES;

namespace Heirloom.OpenGLES
{
    /// <summary>
    /// Represents indices of a mesh.
    /// </summary>
    internal sealed unsafe class IndexBuffer : Buffer
    {
        // CPU Buffer
        public readonly ushort[] Data;
        public readonly int Capacity;
        public int Count;

        public IndexBuffer(int capacity)
            : base(BufferTarget.ElementArray, (uint) (capacity * sizeof(ushort)))
        {
            Data = new ushort[capacity];
            Capacity = capacity;
        }

        public void Upload()
        {
            Update(Data, Count);
        }
    }
}
