using System.Runtime.InteropServices;

using Heirloom.Mathematics;

namespace Heirloom.Drawing.OpenGLES
{
    internal sealed class ESBatchInstance : ESBatch
    {
        private readonly VertexBuffer<InstanceData> _instanceBuffer;
        private readonly VertexBuffer<VertexData> _vertexBuffer;
        private readonly ESIndexBuffer _indexBuffer;

        private readonly ESVertexArray _vertexArray;

        private Color? _clearColor;

        private bool _copyTemplate;
        private uint _templateVersion;
        private Mesh _template;

        public ESBatchInstance(ESGraphicsContext context)
            : base(context)
        {
            _instanceBuffer = new VertexBuffer<InstanceData>(BatchCapacity * 8, false);
            _vertexBuffer = new VertexBuffer<VertexData>(BatchCapacity, true);
            _indexBuffer = new ESIndexBuffer(BatchCapacity);

            _vertexArray = new ESVertexArray(_indexBuffer, _instanceBuffer, _vertexBuffer);
        }

        public override bool IsDirty => _clearColor.HasValue || _instanceBuffer.Count > 0;

        public override void Clear(Color color)
        {
            _clearColor = color;
        }

        public override bool Submit(Mesh mesh, Rectangle uvRect, Matrix transform, Color color)
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

        /*[MethodImpl(MethodImplOptions.AggressiveInlining)]*/
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

        public override void Commit()
        {
            if (_clearColor.HasValue)
            {
                // Extract color from nullable
                var color = _clearColor.Value;
                _clearColor = null;

                // Clear
                GLES.SetClearColor(color.R, color.G, color.B, color.A);
                GLES.Clear(ClearMask.Color);
            }

            if (IsDirty)
            {
                // Upload buffers to GPU
                _instanceBuffer.Upload();
                _vertexBuffer.Upload();
                _indexBuffer.Upload();

                // Draw the geometry
                GLES.BindVertexArray(_vertexArray.Handle);
                if (_indexBuffer.Count > 0) { GLES.DrawElementsInstanced(DrawMode.Triangles, _indexBuffer.Count, DrawElementType.UnsignedShort, _instanceBuffer.Count); }
                else { GLES.DrawArraysInstanced(DrawMode.Triangles, _vertexBuffer.Count, _instanceBuffer.Count); }
                GLES.BindVertexArray(0);
            }

            // Clear instance count
            _instanceBuffer.Count = 0;
        }

        [StructLayout(LayoutKind.Sequential, Pack = 1)]
        private struct VertexData // 16 bytes
        {
            [ESVertexAttribute(ESVertexAttributeName.Position)]
            public Vector Position;

            [ESVertexAttribute(ESVertexAttributeName.UV)]
            public Vector UV;
        }

        [StructLayout(LayoutKind.Sequential, Pack = 1)]
        private struct InstanceData // 72 bytes
        {
            [ESVertexAttribute(ESVertexAttributeName.Transform)]
            public Matrix Transform; // 36

            [ESVertexAttribute(ESVertexAttributeName.AtlasRect)]
            public Rectangle AtlasRect; // 16

            [ESVertexAttribute(ESVertexAttributeName.Color, Normalize = true)]
            public Color Color; // 16
        }
    }
}
