using System.Runtime.CompilerServices;
using Heirloom.OpenGLES;

namespace Heirloom.Drawing.OpenGLES
{
    internal sealed unsafe class IndexBuffer
    {
        public readonly Buffer Buffer;
        public readonly ushort[] Data;
        public int Count;

        public IndexBuffer(int capacity)
        {
            Buffer = new Buffer(BufferTarget.ElementArray, (uint) (capacity * sizeof(ushort)));
            Data = new ushort[capacity];
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public void Bind()
        {
            Buffer.Bind();
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public void Upload()
        {
            Buffer.Update(Data, Count, 0);
        }
    }
}
