using System;
using System.Runtime.CompilerServices;
using System.Runtime.InteropServices;

using Heirloom.Math;
using Heirloom.OpenGLES;

namespace Heirloom.Drawing.OpenGLES
{
    internal unsafe class VertexArray
    {
        // Instance Buffer
        // todo: GLVertexBuffer<InstanceData> ...?
        public readonly Buffer InstanceBuffer;
        public readonly VertexAttribute[] InstanceAttributes;
        public readonly InstanceData[] InstanceElements;
        public int InstanceCount;

        public readonly Buffer VertexBuffer;
        public readonly VertexAttribute[] VertexAttributes;
        public readonly VertexData[] VertexElements;
        public int VertexCount;

        public readonly Buffer IndexBuffer;
        public readonly ushort[] IndexElements;
        public int IndexCount;

        public readonly uint Handle;

        public VertexArray()
        {
            // Client buffer storage
            InstanceElements = new InstanceData[ushort.MaxValue];
            VertexElements = new VertexData[ushort.MaxValue];
            IndexElements = new ushort[ushort.MaxValue];

            // Construct buffers
            InstanceBuffer = new Buffer(BufferTarget.Array, (uint) (sizeof(InstanceData) * InstanceElements.Length));
            VertexBuffer = new Buffer(BufferTarget.Array, (uint) (sizeof(VertexData) * VertexElements.Length));
            IndexBuffer = new Buffer(BufferTarget.ElementArray, (uint) (sizeof(ushort) * IndexElements.Length));

            // Create Attributes
            InstanceAttributes = VertexAttribute.GenerateAttributes(typeof(InstanceData));
            VertexAttributes = VertexAttribute.GenerateAttributes(typeof(VertexData));

            // Construct VAO
            Handle = GL.GenVertexArray();
            GL.BindVertexArray(Handle);
            {
                // Binds vertex buffer (per vertex) to vao
                VertexBuffer.Bind();
                ConfigureAttributes(VertexAttributes, false);

                // Binds vertex buffer (per instance) to vao
                InstanceBuffer.Bind();
                ConfigureAttributes(InstanceAttributes, true);

                // Bind index buffer (triangles) to vao
                IndexBuffer.Bind();
            }
            GL.BindVertexArray(0);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        private void ConfigureAttributes(VertexAttribute[] attributes, bool instanced)
        {
            var stride = 0; // 
            foreach (var attr in attributes)
            {
                stride += VertexAttribute.GetSizeInBytes(attr.Type, attr.Size);
            }

            var offset = 0; // 
            foreach (var attr in attributes)
            {
                attr.SetAttributePointer(offset, stride, instanced ? 1 : 0);
                offset += VertexAttribute.GetSizeInBytes(attr.Type, attr.Size);
            }
        }

        internal void Update()
        {
            // Update instance buffer
            InstanceBuffer.Update(InstanceElements, InstanceCount, 0);

            // Update template mesh
            VertexBuffer.Update(VertexElements, VertexCount, 0);
            IndexBuffer.Update(IndexElements, IndexCount, 0);
        }

        [StructLayout(LayoutKind.Sequential, Pack = 1)]
        internal struct VertexData // 16 bytes
        {
            [VertexAttribute(VertexAttributeName.Position)]
            public Vector Position;

            [VertexAttribute(VertexAttributeName.UV)]
            public Vector UV;
        }

        [StructLayout(LayoutKind.Sequential, Pack = 1)]
        internal struct InstanceData // 72 bytes
        {
            [VertexAttribute(VertexAttributeName.Transform)]
            public Matrix Transform; // 36

            [VertexAttribute(VertexAttributeName.Color, Normalize = true)]
            public Color Color; // 16

            [VertexAttribute(VertexAttributeName.ImageUnit)]
            public int TextureSlot; // 4

            [VertexAttribute(VertexAttributeName.ImageRect)]
            public Rectangle TextureRect; // 16
        }
    }
}
