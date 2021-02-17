using System.Runtime.InteropServices;

using Heirloom.Mathematics;

namespace Heirloom.Drawing.OpenGLES
{
    internal sealed class ESBatchStreaming : ESBatch
    {
        private readonly VertexBuffer<InstanceData> _instanceBuffer;
        private readonly VertexBuffer<VertexData> _vertexBuffer;
        private readonly ESIndexBuffer _indexBuffer;

        private readonly ESVertexArray _vertexArray;

        private Color? _clearColor;

        public ESBatchStreaming(ESGraphicsContext context)
            : base(context)
        {
            _instanceBuffer = new VertexBuffer<InstanceData>(capacity: 1, false);
            _vertexBuffer = new VertexBuffer<VertexData>(BatchCapacity, true);
            _indexBuffer = new ESIndexBuffer(BatchCapacity * 8);

            _vertexArray = new ESVertexArray(_indexBuffer, _instanceBuffer, _vertexBuffer);

            // Set instance buffer to identity values
            ref var instance = ref _instanceBuffer.Data[0];
            instance.Transform = Matrix.Identity;

            // Set instance count exactly one
            _instanceBuffer.Count = 1;
        }

        public override bool IsDirty => _clearColor.HasValue || _vertexBuffer.Count > 0 || _indexBuffer.Count > 0;

        public override void Clear(Color color)
        {
            _clearColor = color;
        }

        public override bool Submit(Mesh mesh, Rectangle uvRect, Matrix matrix, Color color)
        {
            // Determine how many indices are needed for this submission
            var indexCount = mesh.IsIndexed ? mesh.Indices.Count : mesh.Vertices.Count;

            // If buffers will overflow, we need to force a flush
            var hasExceedVertexCapacity = (_vertexBuffer.Count + mesh.Vertices.Count) >= _vertexBuffer.Capacity;
            var hasExceedIndexCapacity = (_indexBuffer.Count + indexCount) >= _indexBuffer.Capacity;
            if (hasExceedVertexCapacity || hasExceedIndexCapacity)
            {
                //if (hasExceedVertexCapacity) { Log.Warning($"Batch Vertex Capacity Exceed"); }
                //if (hasExceedIndexCapacity) { Log.Warning($"Batch Index Capacity Exceed"); }
                return false;
            }

            var vOffset = _vertexBuffer.Count;
            var iOffset = _indexBuffer.Count;

            // Copy vertex data
            for (var i = 0; i < mesh.Vertices.Count; i++)
            {
                ref var vBuff = ref _vertexBuffer.Data[vOffset + i];
                ref var vMesh = ref mesh.Vertices.Data[i];

                // Compute final vertex values
                Matrix.Multiply(matrix, vMesh.Position, ref vBuff.Position);
                Color.Multiply(vMesh.Color, color, ref vBuff.Color);
                vBuff.AtlasRect = uvRect;
                vBuff.UV = vMesh.UV;
            }

            // Advance vertex count by mesh size
            _vertexBuffer.Count += mesh.Vertices.Count;

            // Mesh is indexed, we can simply copy the indices from the mesh into the batch.
            if (mesh.IsIndexed)
            {
                // Copy index data
                for (var i = 0; i < mesh.Indices.Count; i++)
                {
                    ref var iBuff = ref _indexBuffer.Data[iOffset + i];
                    ref var iMesh = ref mesh.Indices.Data[i];

                    iBuff = (ushort) (vOffset + iMesh);
                }

                _indexBuffer.Count += mesh.Indices.Count;
            }
            // Mesh was not index (but batch is), generate indices for vertex-triangle stream.
            else
            {
                // Generate index data
                for (var i = 0; i < mesh.Vertices.Count; i++)
                {
                    ref var iBuff = ref _indexBuffer.Data[iOffset + i];
                    ref var iMesh = ref i;

                    iBuff = (ushort) (vOffset + iMesh);
                }

                _indexBuffer.Count += mesh.Vertices.Count;
            }

            // Successfully submitted mesh
            return true;
        }

        public override void Commit()
        {
            if (IsDirty)
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

                // Upload buffers to GPU
                _instanceBuffer.Upload();
                _vertexBuffer.Upload();
                _indexBuffer.Upload();

                // Draw the geometry
                GLES.BindVertexArray(_vertexArray.Handle);
                if (_indexBuffer.Count > 0) { GLES.DrawElementsInstanced(DrawMode.Triangles, _indexBuffer.Count, DrawElementType.UnsignedShort, _instanceBuffer.Count); }
                else { GLES.DrawArraysInstanced(DrawMode.Triangles, _vertexBuffer.Count, _instanceBuffer.Count); }
                GLES.BindVertexArray(0);

                // Clear streaming buffers
                _vertexBuffer.Count = 0;
                _indexBuffer.Count = 0;
            }
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
