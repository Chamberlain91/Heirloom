using System.Collections.Generic;
using System.Runtime.CompilerServices;
using System.Runtime.InteropServices;

using Heirloom.OpenGLES;

namespace Heirloom.OpenGLES
{
    internal abstract class VertexBuffer : Buffer
    {
        public readonly VertexAttribute[] Attributes;
        public readonly bool IsPerVertex;
        public readonly int ByteStride;

        protected VertexBuffer(VertexAttribute[] attributes, bool isPerVertex, uint sizeInBytes)
            : base(BufferTarget.Array, sizeInBytes)
        {
            ByteStride = ComputeAttributeStride(attributes);
            IsPerVertex = isPerVertex;
            Attributes = attributes;
        }

        private static int ComputeAttributeStride(IEnumerable<VertexAttribute> attributes)
        {
            var stride = 0;

            foreach (var attr in attributes)
            {
                stride += VertexAttribute.GetSizeInBytes(attr.Type, attr.Size);
            }

            return stride;
        }

        public abstract void Upload();
    }

    internal sealed unsafe class VertexBuffer<TStruct> : VertexBuffer
        where TStruct : struct
    {
        // CPU Buffer
        public readonly TStruct[] Data;
        public readonly int Capacity;
        public int Count;

        public VertexBuffer(int capacity, bool isPerVertex)
            : base(attributes: VertexAttribute.GenerateAttributes(typeof(TStruct)), isPerVertex,
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
                offset += VertexAttribute.GetSizeInBytes(attr.Type, attr.Size);
            }
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public override void Upload()
        {
            Update(Data, Count);
        }
    }
}
