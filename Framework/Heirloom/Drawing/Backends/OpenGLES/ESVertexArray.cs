using System;
using System.Diagnostics;

namespace Heirloom.Drawing.OpenGLES
{
    internal unsafe class ESVertexArray
    {
        public readonly ESVertexBuffer[] VertexBuffers;
        public readonly ESIndexBuffer IndexBuffer;

        public readonly uint Handle;

        public ESVertexArray(ESIndexBuffer indexBuffer, params ESVertexBuffer[] vertexBuffers)
        {
            VertexBuffers = vertexBuffers ?? throw new ArgumentNullException(nameof(vertexBuffers));
            IndexBuffer = indexBuffer ?? throw new ArgumentNullException(nameof(indexBuffer));

            if (VertexBuffers.Length == 0)
            {
                throw new ArgumentException("Must specify at least one vertex buffer.");
            }

            DebugAttributes();

            // Construct VAO
            Handle = GLES.GenVertexArray();
            GLES.BindVertexArray(Handle);
            {
                // Bind the vertex buffers
                foreach (var buffer in VertexBuffers)
                {
                    buffer.Bind();
                }

                // Bind the index buffer
                IndexBuffer.Bind();
            }
            GLES.BindVertexArray(0);
        }

        [Conditional("DEBUG")]
        private void DebugAttributes()
        {
            Log.Debug("Vertex Attributes:");
            foreach (var vertexBuffer in VertexBuffers)
            {
                var count = vertexBuffer.Size / vertexBuffer.ByteStride;
                Log.Debug($"  {(vertexBuffer.IsPerVertex ? "Vertex" : "Instance")} Buffer ({vertexBuffer.Size} / {vertexBuffer.ByteStride} bytes => {count} elements)");
                foreach (var attr in vertexBuffer.Attributes)
                {
                    Log.Debug($"    {attr}");
                }
            }
            Log.Debug("");
        }
    }
}
