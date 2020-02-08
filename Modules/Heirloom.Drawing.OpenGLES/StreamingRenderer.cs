using System.Runtime.InteropServices;

using Heirloom.Math;
using Heirloom.OpenGLES;

namespace Heirloom.Drawing.OpenGLES
{
    internal sealed class StreamingRenderer : Renderer
    {
        internal readonly VertexArray VertexArray;

        internal readonly VertexBuffer<InstanceData> InstanceBuffer;
        internal readonly VertexBuffer<VertexData> VertexBuffer;
        internal readonly IndexBuffer IndexBuffer;

        public StreamingRenderer(OpenGLGraphics graphics)
            : base(graphics)
        {
            InstanceBuffer = new VertexBuffer<InstanceData>(ushort.MaxValue, false);
            VertexBuffer = new VertexBuffer<VertexData>(ushort.MaxValue, true);
            IndexBuffer = new IndexBuffer(ushort.MaxValue);

            VertexArray = new VertexArray(IndexBuffer, InstanceBuffer, VertexBuffer);

            // Set instance buffer to identity values
            ref var instance = ref InstanceBuffer.Data[0];
            instance.Transform = Matrix.Identity;
            instance.AtlasRect = Rectangle.One;
            InstanceBuffer.Count = 1;
        }

        public override bool IsDirty => IndexBuffer.Count > 0;

        protected override void Submit(Mesh mesh, in Matrix transform, in Color color)
        {
            // Buffers will exceed capacity (force flush).
            if ((VertexBuffer.Count + mesh.Vertices.Count) >= VertexBuffer.Capacity ||
                (IndexBuffer.Count + mesh.Indices.Count) >= IndexBuffer.Capacity)
            {
                // Render previous batch
                Graphics.Flush();
            }

            // Copy vertex data
            var vOffset = VertexBuffer.Count;
            for (var i = 0; i < mesh.Vertices.Count; i++)
            {
                ref var vtx = ref VertexBuffer.Data[vOffset + i];
                var mVtx = mesh.Vertices[i];

                vtx.Position = transform * mVtx.Position;
                vtx.UV = UVRect.Position + (mVtx.UV * (Vector) UVRect.Size);
                vtx.Color = color;
            }

            // Copy index data
            var iOffset = IndexBuffer.Count;
            for (var i = 0; i < mesh.Indices.Count; i++)
            {
                IndexBuffer.Data[iOffset + i] = (ushort) (vOffset + mesh.Indices[i]);
            }

            // 
            VertexBuffer.Count += mesh.Vertices.Count;
            IndexBuffer.Count += mesh.Indices.Count;
        }

        protected override void DrawBatch()
        {
            // Upload buffers to GPU
            InstanceBuffer.Upload();
            VertexBuffer.Upload();
            IndexBuffer.Upload();

            // Bind vertex configuration
            VertexArray.Bind();

            // Draw the geometry
            GL.DrawElementsInstanced(DrawMode.Triangles, IndexBuffer.Count, DrawElementType.UnsignedShort, InstanceBuffer.Count);
            GL.BindVertexArray(0);

            // Inform the graphics something was drawn
            Graphics.MarkSurfaceDirty();

            // Clear counts
            VertexBuffer.Count = 0;
            IndexBuffer.Count = 0;
        }

        [StructLayout(LayoutKind.Sequential, Pack = 1)]
        internal struct VertexData
        {
            [VertexAttribute(VertexAttributeName.Position)]
            public Vector Position;

            [VertexAttribute(VertexAttributeName.UV)]
            public Vector UV;

            [VertexAttribute(VertexAttributeName.Color)]
            public Color Color;
        }

        [StructLayout(LayoutKind.Sequential, Pack = 1)]
        internal struct InstanceData
        {
            [VertexAttribute(VertexAttributeName.AtlasRect)]
            public Rectangle AtlasRect;

            [VertexAttribute(VertexAttributeName.Transform)]
            public Matrix Transform;
        }
    }
}
