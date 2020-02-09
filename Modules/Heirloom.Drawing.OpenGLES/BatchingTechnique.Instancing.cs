using System.Runtime.CompilerServices;
using System.Runtime.InteropServices;

using Heirloom.Math;
using Heirloom.OpenGLES;

namespace Heirloom.Drawing.OpenGLES
{
    internal sealed class InstancingBatchingTechnique : BatchingTechnique
    {
        private readonly VertexBuffer<InstanceData> _instanceBuffer;
        private readonly VertexBuffer<VertexData> _vertexBuffer;
        private readonly IndexBuffer _indexBuffer;

        private readonly VertexArray _vertexArray;

        private bool _copyTemplate;
        private uint _templateVersion;
        private Mesh _template;

        public InstancingBatchingTechnique()
        {
            _instanceBuffer = new VertexBuffer<InstanceData>(ushort.MaxValue, false);
            _vertexBuffer = new VertexBuffer<VertexData>(ushort.MaxValue, true);
            _indexBuffer = new IndexBuffer(ushort.MaxValue);

            _vertexArray = new VertexArray(_indexBuffer, _instanceBuffer, _vertexBuffer);
        }

        public override bool IsDirty => _instanceBuffer.Count > 0;

        protected internal override bool Submit(Mesh mesh, in Rectangle uvRect, in Matrix transform, in Color color)
        {
            // Instance buffer at capacity, we need to force a flush.
            if (_instanceBuffer.Count == _instanceBuffer.Capacity) { return false; }

            // Update template mesh. Will return false when a different mesh is
            // detected to cause the renderer to flush.
            if (!TryUpdateTemplateMesh(mesh)) { return false; }

            // Append this instance to the instance buffer
            ref var instance = ref _instanceBuffer.Data[_instanceBuffer.Count++];
            instance.AtlasRect = uvRect;
            instance.Transform = transform;
            instance.Color = color;

            // Successfully submitted mesh
            return true;
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        private bool TryUpdateTemplateMesh(Mesh mesh)
        {
            // Different mesh or out-of-date.
            if (_template != mesh || _templateVersion != mesh.Version)
            {
                // Update template
                _templateVersion = mesh.Version;
                _template = mesh;

                // Mark to copy new template next submission
                _copyTemplate = true;

                // 
                return false;
            }

            // Do we need to copy the template?
            if (_copyTemplate)
            {
                // Copy vertex data
                for (var i = 0; i < mesh.Vertices.Count; i++)
                {
                    ref var vtx = ref _vertexBuffer.Data[i];
                    vtx.Position = mesh.Vertices[i].Position;
                    vtx.UV = mesh.Vertices[i].UV;
                }

                // Copy index data
                for (var i = 0; i < mesh.Indices.Count; i++)
                {
                    _indexBuffer.Data[i] = (ushort) mesh.Indices[i];
                }

                // Store vertex and index counts
                _vertexBuffer.Count = mesh.Vertices.Count;
                _indexBuffer.Count = mesh.Indices.Count;

                // We have copied the template
                _copyTemplate = false;
            }

            return true;
        }

        protected internal override void DrawBatch()
        {
            if (IsDirty)
            {
                // Upload buffers to GPU
                _instanceBuffer.Upload();
                _vertexBuffer.Upload();
                _indexBuffer.Upload();

                // Log.Info($"Drawing {_instanceBuffer.Count} x {IndexBuffer.Count / 3} triangles.");

                // Draw the geometry
                GL.BindVertexArray(_vertexArray.Handle);
                GL.DrawElementsInstanced(DrawMode.Triangles, _indexBuffer.Count, DrawElementType.UnsignedShort, _instanceBuffer.Count);
                GL.BindVertexArray(0);
            }

            // Clear instance count
            _instanceBuffer.Count = 0;
        }

        [StructLayout(LayoutKind.Sequential, Pack = 1)]
        private struct VertexData // 16 bytes
        {
            [VertexAttribute(VertexAttributeName.Position)]
            public Vector Position;

            [VertexAttribute(VertexAttributeName.UV)]
            public Vector UV;
        }

        [StructLayout(LayoutKind.Sequential, Pack = 1)]
        private struct InstanceData // 72 bytes
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
