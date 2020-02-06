using System.Runtime.InteropServices;

using Heirloom.OpenGLES;

namespace Heirloom.Drawing.OpenGLES
{
    internal abstract class VertexBuffer
    {
        public abstract void Bind();
        public abstract void Upload();
    }

    internal sealed unsafe class VertexBuffer<TStruct> : VertexBuffer
        where TStruct : struct
    {
        // Attribute Layout
        public readonly VertexAttribute[] Attributes;
        public readonly int ByteStride;

        // GPU
        public readonly Buffer Buffer;

        // CPU
        public readonly TStruct[] Data;
        public int Capacity => Data.Length;
        public int Count;

        public readonly bool IsPerVertex;

        public VertexBuffer(int capacity, bool isPerVertex)
        {
            Attributes = VertexAttribute.GenerateAttributes(typeof(TStruct));
            Buffer = new Buffer(BufferTarget.Array, (uint) (capacity * Marshal.SizeOf<TStruct>()));
            Data = new TStruct[capacity];

            IsPerVertex = isPerVertex;

            // Compute stride
            foreach (var attr in Attributes)
            {
                ByteStride += VertexAttribute.GetSizeInBytes(attr.Type, attr.Size);
            }
        }

        public override void Bind()
        {
            // == Bind Buffer
            Buffer.Bind();

            // == Configure Pointers
            var offset = 0;
            foreach (var attr in Attributes)
            {
                attr.SetAttributePointer(offset, ByteStride, IsPerVertex ? 0 : 1);
                offset += VertexAttribute.GetSizeInBytes(attr.Type, attr.Size);
            }
        }

        public override void Upload()
        {
            Buffer.Update(Data, Count, 0);
        }
    }
}
