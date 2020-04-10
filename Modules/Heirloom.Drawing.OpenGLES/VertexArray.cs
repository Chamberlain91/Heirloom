using System;
using System.Diagnostics;

using Heirloom.IO;
using Heirloom.OpenGLES;

namespace Heirloom.Drawing.OpenGLES
{
    internal unsafe class VertexArray
    {
        public readonly VertexBuffer[] VertexBuffers;
        public readonly IndexBuffer IndexBuffer;

        public readonly uint Handle;

        public VertexArray(IndexBuffer indexBuffer, params VertexBuffer[] vertexBuffers)
        {
            VertexBuffers = vertexBuffers ?? throw new ArgumentNullException(nameof(vertexBuffers));
            IndexBuffer = indexBuffer;

            if (VertexBuffers.Length == 0)
            {
                throw new ArgumentException("Must specify at least one vertex buffer.");
            }

            DebugAttributes();

            // Construct VAO
            Handle = GL.GenVertexArray();
            GL.BindVertexArray(Handle);
            {
                // Bind the vertex buffers
                foreach (var buffer in VertexBuffers)
                {
                    buffer.Bind();
                }

                // If given, bind the index buffer
                IndexBuffer?.Bind();
            }
            GL.BindVertexArray(0);
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
