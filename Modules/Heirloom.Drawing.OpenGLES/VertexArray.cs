using System;

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

        internal void Bind()
        {
            GL.BindVertexArray(Handle);
        }
    }
}
