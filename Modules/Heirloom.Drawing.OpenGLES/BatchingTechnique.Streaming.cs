using System.Runtime.InteropServices;

using Heirloom.Math;
using Heirloom.OpenGLES;

namespace Heirloom.Drawing.OpenGLES
{
    internal sealed class StreamingBatchingTechnique : BatchingTechnique
    {
        private readonly VertexBuffer<InstanceData> _instanceBuffer;
        private readonly VertexBuffer<VertexData> _vertexBuffer;
        private readonly IndexBuffer _indexBuffer;

        private readonly VertexArray _vertexArray;

        public StreamingBatchingTechnique()
        {
            _instanceBuffer = new VertexBuffer<InstanceData>(ushort.MaxValue, false);
            _vertexBuffer = new VertexBuffer<VertexData>(ushort.MaxValue, true);
            _indexBuffer = new IndexBuffer(ushort.MaxValue);

            _vertexArray = new VertexArray(_indexBuffer, _instanceBuffer, _vertexBuffer);

            // Set instance buffer to identity values
            ref var instance = ref _instanceBuffer.Data[0];
            instance.Transform = Matrix.Identity;
            // instance.AtlasRect = Rectangle.One;

            // Set instance count exactly one
            _instanceBuffer.Count = 1;
        }

        public override bool IsDirty => _indexBuffer.Count > 0;

        protected internal override bool Submit(Mesh mesh, in Rectangle uvRect, in Matrix transform, in Color color)
        {
            // If buffers will overflow, we need to force a flush
            if ((_vertexBuffer.Count + mesh.Vertices.Count) >= _vertexBuffer.Capacity ||
                (_indexBuffer.Count + mesh.Indices.Count) >= _indexBuffer.Capacity)
            { return false; }

            // Copy vertex data
            var vOffset = _vertexBuffer.Count;
            for (var i = 0; i < mesh.Vertices.Count; i++)
            {
                ref var vtx = ref _vertexBuffer.Data[vOffset + i];
                var mVtx = mesh.Vertices[i];

                vtx.Position = transform * mVtx.Position;
                vtx.AtlasRect = uvRect;
                vtx.UV = mVtx.UV;
                vtx.Color = color;
            }

            // Copy index data
            var iOffset = _indexBuffer.Count;
            for (var i = 0; i < mesh.Indices.Count; i++)
            {
                _indexBuffer.Data[iOffset + i] = (ushort) (vOffset + mesh.Indices[i]);
            }

            // Advance counters by mesh size
            _vertexBuffer.Count += mesh.Vertices.Count;
            _indexBuffer.Count += mesh.Indices.Count;

            // Successfully submitted mesh
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

                // Log.Info($"Drawing {IndexBuffer.Count / 3} triangles.");

                // Draw the geometry
                GL.BindVertexArray(_vertexArray.Handle);
                GL.DrawElementsInstanced(DrawMode.Triangles, _indexBuffer.Count, DrawElementType.UnsignedShort, _instanceBuffer.Count);
                GL.BindVertexArray(0);
            }

            // Clear counts
            _vertexBuffer.Count = 0;
            _indexBuffer.Count = 0;
        }

        [StructLayout(LayoutKind.Sequential, Pack = 1)]
        private struct VertexData
        {
            [VertexAttribute(VertexAttributeName.Position)]
            public Vector Position;

            [VertexAttribute(VertexAttributeName.UV)]
            public Vector UV;

            [VertexAttribute(VertexAttributeName.AtlasRect)]
            public Rectangle AtlasRect;

            [VertexAttribute(VertexAttributeName.Color)]
            public Color Color;
        }

        [StructLayout(LayoutKind.Sequential, Pack = 1)]
        private struct InstanceData
        {
            [VertexAttribute(VertexAttributeName.Transform)]
            public Matrix Transform;
        }
    }
}
