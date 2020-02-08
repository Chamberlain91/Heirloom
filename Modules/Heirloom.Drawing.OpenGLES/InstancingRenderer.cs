using System.Runtime.CompilerServices;
using System.Runtime.InteropServices;

using Heirloom.Math;
using Heirloom.OpenGLES;

namespace Heirloom.Drawing.OpenGLES
{
    internal sealed class InstancingRenderer : Renderer
    {
        internal readonly VertexArray VertexArray;

        internal readonly VertexBuffer<InstanceData> InstanceBuffer;
        internal readonly VertexBuffer<VertexData> VertexBuffer;
        internal readonly IndexBuffer IndexBuffer;

        private uint _templateVersion;
        private Mesh _template;

        public InstancingRenderer(OpenGLGraphics graphics)
            : base(graphics)
        {
            InstanceBuffer = new VertexBuffer<InstanceData>(ushort.MaxValue, false);
            VertexBuffer = new VertexBuffer<VertexData>(ushort.MaxValue, true);
            IndexBuffer = new IndexBuffer(ushort.MaxValue);

            VertexArray = new VertexArray(IndexBuffer, InstanceBuffer, VertexBuffer);
        }

        public override bool IsDirty => InstanceBuffer.Count > 0;

        protected override void Submit(Mesh mesh, in Matrix transform, in Color color)
        {
            UseMesh(mesh);
            AppendInstance(in transform, UVRect, in color);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        private void UseMesh(Mesh mesh)
        {
            // Different mesh or out-of-date.
            if (_template != mesh || _templateVersion != mesh.Version)
            {
                // Complete pending work
                Graphics.Flush();

                // Update template
                _templateVersion = mesh.Version;
                _template = mesh;

                // Copy vertex data
                for (var i = 0; i < mesh.Vertices.Count; i++)
                {
                    VertexBuffer.Data[i].Position = mesh.Vertices[i].Position;
                    VertexBuffer.Data[i].UV = mesh.Vertices[i].UV;
                }

                // Copy index data
                for (var i = 0; i < mesh.Indices.Count; i++)
                {
                    IndexBuffer.Data[i] = (ushort) mesh.Indices[i];
                }

                // Store vertex and index counts
                VertexBuffer.Count = mesh.Vertices.Count;
                IndexBuffer.Count = mesh.Indices.Count;
            }
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        private void AppendInstance(in Matrix transform, in Rectangle uvRect, in Color color)
        {
            // Get the next available instance
            ref var instance = ref InstanceBuffer.Data[InstanceBuffer.Count++];

            instance.AtlasRect = uvRect;
            instance.Transform = transform;
            instance.Color = color;

            // Instance buffer at capacity (forced flush).
            if (InstanceBuffer.Count == InstanceBuffer.Capacity)
            {
                Graphics.Flush();
            }
        }

        protected override void DrawBatch()
        {
            // Upload buffers to GPU
            InstanceBuffer.Upload();
            VertexBuffer.Upload();
            IndexBuffer.Upload();

            // Bind vertex configuration
            VertexArray.Bind();

            // Log.Info($"Drawing {IndexBuffer.Count / 3} triangles.");

            // Draw the geometry
            GL.DrawElementsInstanced(DrawMode.Triangles, IndexBuffer.Count, DrawElementType.UnsignedShort, InstanceBuffer.Count);
            GL.BindVertexArray(0);

            // Inform the graphics something was drawn
            Graphics.MarkSurfaceDirty();

            // Clear instance count
            InstanceBuffer.Count = 0;
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

            [VertexAttribute(VertexAttributeName.AtlasRect)]
            public Rectangle AtlasRect; // 16

            [VertexAttribute(VertexAttributeName.Color, Normalize = true)]
            public Color Color; // 16
        }
    }
}
