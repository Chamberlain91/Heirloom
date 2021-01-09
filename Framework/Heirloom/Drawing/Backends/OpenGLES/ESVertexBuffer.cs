using System.Collections.Generic;
using System.Runtime.CompilerServices;
using System.Runtime.InteropServices;

namespace Heirloom.Drawing.OpenGLES
{
    internal abstract class ESVertexBuffer : ESBuffer
    {
        public readonly ESVertexAttribute[] Attributes;
        public readonly bool IsPerVertex;
        public readonly int ByteStride;

        protected ESVertexBuffer(ESVertexAttribute[] attributes, bool isPerVertex, uint sizeInBytes)
            : base(BufferTarget.Array, sizeInBytes)
        {
            ByteStride = ComputeAttributeStride(attributes);
            IsPerVertex = isPerVertex;
            Attributes = attributes;
        }

        private static int ComputeAttributeStride(IEnumerable<ESVertexAttribute> attributes)
        {
            var stride = 0;

            foreach (var attr in attributes)
            {
                stride += ESVertexAttribute.GetSizeInBytes(attr.Type, attr.Size);
            }

            return stride;
        }

        public abstract void Upload();
    }

    internal sealed unsafe class VertexBuffer<TStruct> : ESVertexBuffer
        where TStruct : struct
    {
        // CPU Buffer
        public readonly TStruct[] Data;
        public readonly int Capacity;
        public int Count;

        public VertexBuffer(int capacity, bool isPerVertex)
            : base(attributes: ESVertexAttribute.GenerateAttributes(typeof(TStruct)), isPerVertex,
                   sizeInBytes: (uint) (capacity * Marshal.SizeOf<TStruct>()))
        {
            Data = new TStruct[capacity];
            Capacity = capacity;
            Count = 0;
        }

        public override void Bind()
        {
            // == Bind Buffer
            base.Bind();

            // == Configure Pointers
            var offset = 0;
            foreach (var attr in Attributes)
            {
                attr.SetAttributePointer(offset, ByteStride, IsPerVertex ? 0 : 1);
                offset += ESVertexAttribute.GetSizeInBytes(attr.Type, attr.Size);
            }
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public override void Upload()
        {
            Update(Data, Count);
        }
    }
}
