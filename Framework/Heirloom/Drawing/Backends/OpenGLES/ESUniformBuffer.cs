using System.Runtime.CompilerServices;

namespace Heirloom.Drawing.OpenGLES
{
    internal sealed class ESUniformBuffer : ESBuffer
    {
        public ESUniformBuffer(uint sizeInBytes)
            : base(BufferTarget.UniformBuffer, sizeInBytes)
        { }

        /*[MethodImpl(MethodImplOptions.AggressiveInlining)]*/
        public new unsafe void Update(void* data, int offset, int size)
        {
            base.Update(data, offset, size);
        }

        /*[MethodImpl(MethodImplOptions.AggressiveInlining)]*/
        public new unsafe void Update<T>(T[] data, int offset, int size) where T : struct
        {
            base.Update(data, offset, size);
        }
    }
}
