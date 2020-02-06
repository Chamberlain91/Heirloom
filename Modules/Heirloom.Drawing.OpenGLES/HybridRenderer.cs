using System;
using System.Runtime.InteropServices;

using Heirloom.Math;
using Heirloom.OpenGLES;

namespace Heirloom.Drawing.OpenGLES
{
    internal sealed class HybridBatcher : Renderer
    {
        private readonly BatchingTechnique _technique;
        private bool _isDirty;

        public HybridBatcher(OpenGLGraphics graphics)
            : base(graphics)
        {
            _technique = new InstancingTechnique(graphics);
        }

        public override bool IsDirty => _isDirty;

        public override void Submit(ImageSource image, Mesh mesh, in Matrix transform, in Color color)
        {
            // todo: buffer calls and then selectively choose between "streaming" an "instancing" techniques.
            _technique.Submit(image, mesh, in transform, in color);
            _isDirty = true;
        }

        public override void FlushBatch()
        {
            _technique.DrawBatch();
            _isDirty = false;
        }

        private abstract class BatchingTechnique
        {
            protected readonly OpenGLGraphics Graphics;

            protected BatchingTechnique(OpenGLGraphics graphics)
            {
                Graphics = graphics ?? throw new ArgumentNullException(nameof(graphics));
            }

            internal abstract bool IsDirty { get; }

            internal abstract void Submit(ImageSource image, Mesh mesh, in Matrix transform, in Color color);
            internal abstract void DrawBatch();
        }

        private abstract class BatchingTechnique<TPerInstance, TPerVertex> : BatchingTechnique
            where TPerInstance : struct
            where TPerVertex : struct
        {
            internal readonly VertexArray VertexArray;

            internal readonly VertexBuffer<TPerInstance> InstanceBuffer;
            internal readonly VertexBuffer<TPerVertex> VertexBuffer;
            internal readonly IndexBuffer IndexBuffer;

            private Texture _texture;

            protected BatchingTechnique(OpenGLGraphics graphics)
                : base(graphics)
            {
                InstanceBuffer = new VertexBuffer<TPerInstance>(ushort.MaxValue, false);
                VertexBuffer = new VertexBuffer<TPerVertex>(ushort.MaxValue, true);
                IndexBuffer = new IndexBuffer(ushort.MaxValue);

                VertexArray = new VertexArray(IndexBuffer, InstanceBuffer, VertexBuffer);
            }

            internal override void DrawBatch()
            {
                Graphics.Invoke(() =>
                {
                    // Upload buffers to GPU
                    InstanceBuffer.Upload();
                    VertexBuffer.Upload();
                    IndexBuffer.Upload();

                    // Bind texture
                    GL.ActiveTexture(0);
                    GL.BindTexture(TextureTarget.Texture2D, _texture.Handle);

                    // Bind vertex configuration
                    VertexArray.Bind();

                    // 
                    GL.DrawElementsInstanced(DrawMode.Triangles, IndexBuffer.Count, DrawElementType.UnsignedShort, InstanceBuffer.Count);
                    GL.BindVertexArray(0);
                });

                // Inform the graphics something was drawn
                Graphics.MarkSurfaceDirty();
            }

            protected void UseImage(ImageSource image, out Rectangle uvRect)
            {
                // todo: replace with requesting from atlas
                var (texture, rect) = ResourceManager.GetTextureInfo(Graphics, image);

                // Inconsistent texture, flush and update state
                if (texture != _texture)
                {
                    // Complete pending work
                    Graphics.Flush();

                    // Store new texture reference
                    _texture = texture;
                }

                // Emit uv-space atlas packed rectangle
                uvRect = rect;
            }
        }

        private class InstancingTechnique : BatchingTechnique<InstancingTechnique.InstanceData, InstancingTechnique.VertexData>
        {
            private uint _templateVersion;
            private Mesh _template;

            public InstancingTechnique(OpenGLGraphics graphics)
                : base(graphics)
            { }

            internal override bool IsDirty => InstanceBuffer.Count > 0;

            internal override void Submit(ImageSource image, Mesh mesh, in Matrix transform, in Color color)
            {
                UseMesh(mesh);
                UseImage(image, out var atlasRect);
                AppendInstance(in transform, in color, in atlasRect);
            }

            internal override void DrawBatch()
            {
                base.DrawBatch();

                // Clear instance count
                InstanceBuffer.Count = 0;
            }

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
                        var index = mesh.Indices[i];
                        if (index >= ushort.MaxValue) { throw new InvalidOperationException($"Mesh must not have indices greater then or equal to {ushort.MaxValue}."); }
                        IndexBuffer.Data[i] = (ushort) mesh.Indices[i];
                    }

                    // Store vertex and index counts
                    VertexBuffer.Count = mesh.Vertices.Count;
                    IndexBuffer.Count = mesh.Indices.Count;
                }
            }

            private void AppendInstance(in Matrix transform, in Color color, in Rectangle atlasRect)
            {
                // Get the next available instance
                ref var instance = ref InstanceBuffer.Data[InstanceBuffer.Count++];

                instance.TextureRect = atlasRect;
                instance.Transform = transform;
                instance.Color = color;

                // Instance buffer at capacity (forced flush).
                if (InstanceBuffer.Count == InstanceBuffer.Capacity)
                {
                    Graphics.Flush();
                }
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

                [VertexAttribute(VertexAttributeName.Color, Normalize = true)]
                public Color Color; // 16

                [VertexAttribute(VertexAttributeName.ImageRect)]
                public Rectangle TextureRect; // 16
            }
        }
    }
}
