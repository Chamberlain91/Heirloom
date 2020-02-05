using System;
using System.Runtime.CompilerServices;
using System.Runtime.InteropServices;

using Heirloom.Math;
using Heirloom.OpenGLES;

namespace Heirloom.Drawing.OpenGLES
{
    internal unsafe class InstancingRenderer : Renderer
    // todo: better design, weird coupling between Renderer and Graphics
    // could probably create a "gl state" tracker and move rendering impl into OpenGLGraphics itself
    {
        private readonly OpenGLGraphics _context;

        private readonly VertexBuffer<InstanceData> _instances;
        private readonly VertexBuffer<VertexData> _vertices;
        private readonly IndexBuffer _indices;
        private readonly VertexArray _vao;

        private Mesh _mesh;
        private uint _meshVersion;

        private Texture _texture;

        #region Constructors

        public InstancingRenderer(OpenGLGraphics context)
        {
            _context = context;

            // 
            _instances = new VertexBuffer<InstanceData>(ushort.MaxValue, false);
            _vertices = new VertexBuffer<VertexData>(ushort.MaxValue, true);
            _indices = new IndexBuffer(ushort.MaxValue);

            // Create buffer sets
            _vao = new VertexArray(_indices, _instances, _vertices);
        }

        #endregion

        #region Properties

        public override bool IsDirty => _instances.Count > 0;

        #endregion

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public override void Submit(ImageSource image, Mesh mesh, in Matrix transform, in Color color)
        {
            // If the mesh reference changed or a change in version, update instancing template
            if (!ReferenceEquals(_mesh, mesh) || _meshVersion != mesh.Version)
            {
                // Render previous
                _context.Flush();

                // Set quad template
                SetTemplate(mesh);
            }

            // Unable to append another instance
            if (_instances.Count == _instances.Data.Length)
            {
                _context.Flush();
            }

            // Get OpenGL texture and packed rect
            var (texture, textureRect) = ResourceManager.GetTextureInfo(_context, image);

            // Different texture, flush
            if (_texture != texture)
            {
                _context.Flush();
                _texture = texture;
            }

            // 
            ref var vtx = ref _instances.Data[_instances.Count++];

            vtx.Transform = transform;
            vtx.TextureRect = textureRect;
            vtx.Color = color;
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        private void SetTemplate(Mesh mesh)
        {
            // 
            _mesh = mesh;
            _meshVersion = mesh.Version;

            // Copy vertex data
            for (var i = 0; i < _mesh.Vertices.Count; i++)
            {
                _vertices.Data[i].Position = _mesh.Vertices[i].Position;
                _vertices.Data[i].UV = _mesh.Vertices[i].UV;
            }

            // Copy index data
            for (var i = 0; i < _mesh.Indices.Count; i++)
            {
                var index = _mesh.Indices[i];
                if (index >= ushort.MaxValue) { throw new InvalidOperationException($"Mesh must not have indices greater then or equal to {ushort.MaxValue}."); }

                _indices.Data[i] = (ushort) _mesh.Indices[i];
            }

            // Store vertex and index counts
            _vertices.Count = _mesh.Vertices.Count;
            _indices.Count = _mesh.Indices.Count;
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public override void FlushPendingBatch()
        {
            if (_instances.Count > 0)
            {
                // Perform on the rendering thread
                // todo: make invoke non-blocking but we need a vertex array ring buffer
                _context.Invoke(() =>
                {
                    // Update GPU side buffers
                    _instances.Upload();
                    _vertices.Upload();
                    _indices.Upload();

                    // Bind texture
                    GL.ActiveTexture(0);
                    GL.BindTexture(TextureTarget.Texture2D, _texture.Handle);

                    // Bind the mesh configuration
                    // todo: merge w/ Drawing code below?
                    _vao.Bind();

                    // If indices were defined, use them
                    if (_indices.Count > 0)
                    {
                        GL.DrawElementsInstanced(DrawMode.Triangles, _indices.Count, DrawElementType.UnsignedShort, _instances.Count);
                    }
                    else
                    {
                        GL.DrawArraysInstanced(DrawMode.Triangles, _vertices.Count, _instances.Count);
                    }

                    GL.BindVertexArray(0); // ...
                });

                // Update current surface version number, as we have mutated it
                _context.MarkSurfaceDirty();

                // 
                _instances.Count = 0;
                _texture = null;
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
