using System.Runtime.CompilerServices;

using Heirloom.OpenGLES;

namespace Heirloom.Drawing.OpenGLES
{
    internal sealed class UniformBuffer : Buffer
    {
        public UniformBuffer(uint sizeInBytes)
            : base(BufferTarget.UniformBuffer, sizeInBytes)
        { }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public new unsafe void Update(void* data, int offset, int size)
        {
            base.Update(data, offset, size);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public new unsafe void Update<T>(T[] data, int offset, int size) where T : struct
        {
            base.Update(data, offset, size);
        }
    }
}
