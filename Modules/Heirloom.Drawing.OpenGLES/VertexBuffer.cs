using System.Collections.Generic;
using System.Runtime.CompilerServices;
using System.Runtime.InteropServices;

using Heirloom.OpenGLES;

namespace Heirloom.Drawing.OpenGLES
{
    internal abstract class VertexBuffer : Buffer
    {
        protected VertexBuffer(uint sizeInBytes)
            : base(BufferTarget.Array, sizeInBytes)
        { }

        public abstract void Upload();
    }

    internal sealed unsafe class VertexBuffer<TStruct> : VertexBuffer
        where TStruct : struct
    {
        // Attribute Layout
        public readonly VertexAttribute[] Attributes;
        public readonly bool IsPerVertex;
        public readonly int ByteStride;

        public readonly int Capacity;

        // CPU Buffer
        public readonly TStruct[] Data;
        public int Count;

        public VertexBuffer(int capacity, bool isPerVertex)
            : base((uint) (capacity * Marshal.SizeOf<TStruct>()))
        {
            Attributes = VertexAttribute.GenerateAttributes(typeof(TStruct));
            ByteStride = ComputeAttributeStride(Attributes);
            IsPerVertex = isPerVertex;

            // 
            Data = new TStruct[capacity];
            Capacity = capacity;
            Count = 0;
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
