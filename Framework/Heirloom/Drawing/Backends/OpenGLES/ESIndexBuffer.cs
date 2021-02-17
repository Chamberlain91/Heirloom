namespace Heirloom.Drawing.OpenGLES
{
    internal sealed class ESIndexBuffer : ESBuffer
    {
        public readonly ushort[] Data;
        public readonly int Capacity;
        public int Count;

        internal ESIndexBuffer(int capacity)
            : base(BufferTarget.ElementArray, (uint) (capacity * sizeof(ushort)))
        {
            Data = new ushort[capacity];
            Capacity = capacity;
            Count = 0;
        }

        public void Upload()
        {
            if (Count > 0)
            {
                Update(Data, Count);
            }
        }
    }
}
