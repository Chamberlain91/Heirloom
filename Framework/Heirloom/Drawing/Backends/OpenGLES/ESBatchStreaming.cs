using System.Runtime.InteropServices;

using Heirloom.Mathematics;

namespace Heirloom.Drawing.OpenGLES
{
    internal sealed class ESBatchStreaming : ESBatch
    {
        private readonly VertexBuffer<InstanceData> _instanceBuffer;
        private readonly VertexBuffer<VertexData> _vertexBuffer;

        private readonly ESVertexArray _vertexArray;

        private Color? _clearColor;

        public ESBatchStreaming(ESGraphicsContext context)
            : base(context)
        {
            _instanceBuffer = new VertexBuffer<InstanceData>(BatchCapacity, false);
            _vertexBuffer = new VertexBuffer<VertexData>(BatchCapacity, true);

            _vertexArray = new ESVertexArray(_instanceBuffer, _vertexBuffer);

            // Set instance buffer to identity values
            ref var instance = ref _instanceBuffer.Data[0];
            instance.Transform = Matrix.Identity;

            // Set instance count exactly one
            _instanceBuffer.Count = 1;
        }

        public override bool IsDirty => _clearColor.HasValue || _vertexBuffer.Count > 0;

        public override void Clear(Color color)
        {
            _clearColor = color;
        }

        public override bool Submit(Mesh mesh, Rectangle uvRect, Matrix matrix, Color color)
        {
            // If buffers will overflow, we need to force a flush
            if ((_vertexBuffer.Count + mesh.Vertices.Count) >= _vertexBuffer.Capacity)
            {
                Log.Debug("Batch size exceeded, returning failure.");
                return false;
            }

            // Copy vertex data
            var vOffset = _vertexBuffer.Count;
            for (var i = 0; i < mesh.Vertices.Count; i++)
            {
                ref var vtx = ref _vertexBuffer.Data[vOffset + i];
                var mVtx = mesh.Vertices[i];

                vtx.Position = matrix * mVtx.Position;
                vtx.AtlasRect = uvRect;
                vtx.UV = mVtx.UV;
                vtx.Color = mVtx.Color * color;
            }

            // Advance counters by mesh size
            _vertexBuffer.Count += mesh.Vertices.Count;

            // Successfully submitted mesh
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

                // Log.Info($"Drawing {_indexBuffer.Count / 3} triangles.");

                // Draw the geometry
                GLES.BindVertexArray(_vertexArray.Handle);
                GLES.DrawArraysInstanced(DrawMode.Triangles, _vertexBuffer.Count, _instanceBuffer.Count);
                GLES.BindVertexArray(0);
            }

            // Clear vertex buffer
            _vertexBuffer.Count = 0;
        }

        [StructLayout(LayoutKind.Sequential, Pack = 1)]
        private struct VertexData
        {
            [ESVertexAttribute(ESVertexAttributeName.Position)]
            public Vector Position;

            [ESVertexAttribute(ESVertexAttributeName.UV)]
            public Vector UV;

            [ESVertexAttribute(ESVertexAttributeName.AtlasRect)]
            public Rectangle AtlasRect;

            [ESVertexAttribute(ESVertexAttributeName.Color)]
            public Color Color;
        }

        [StructLayout(LayoutKind.Sequential, Pack = 1)]
        private struct InstanceData
        {
            [ESVertexAttribute(ESVertexAttributeName.Transform)]
            public Matrix Transform;
        }
    }
}
