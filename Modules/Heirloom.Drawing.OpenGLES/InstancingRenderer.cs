using System.Collections.Generic;
using System.Runtime.CompilerServices;

using Heirloom.Math;
using Heirloom.OpenGLES;

namespace Heirloom.Drawing.OpenGLES
{
    internal unsafe class InstancingRenderer : Renderer
    {
        private readonly OpenGLRenderContext _renderingContext;
        private readonly GLVertexArray _vertexArray;

        private readonly Dictionary<GLTexture, int> _textures;
        private readonly GLTexture[] _texturesState;
        private readonly int _maxTextureUnits;

        #region Constructors

        public InstancingRenderer(OpenGLRenderContext renderingContext, int maxTextureUnits)
        {
            _renderingContext = renderingContext;

            // Query for maximum textures
            _maxTextureUnits = maxTextureUnits;
            _texturesState = new GLTexture[_maxTextureUnits];
            _textures = new Dictionary<GLTexture, int>(_maxTextureUnits);

            // Create buffer sets
            _vertexArray = new GLVertexArray();
        }

        ~InstancingRenderer()
        {
            // TODO: Dispose BufferSets
        }

        #endregion

        #region Properties

        public Mesh Mesh { get; private set; }

        public override bool IsDirty => _vertexArray.InstanceCount > 0;

        #endregion

        public override void Submit(ImageSource image, Mesh mesh, in Matrix transform, in Color color)
        {
            // Change the template mesh (used to batch same mesh into instances)
            if (!ReferenceEquals(Mesh, mesh))
            {
                // Render previous
                Flush();

                // Set quad template
                SetTemplate(mesh);
            }

            // Unable to append another instance
            if (_vertexArray.InstanceCount == _vertexArray.InstanceElements.Length)
            {
                Flush();
            }

            // Get OpenGL texture and packed rect
            var (texture, textureRect) = ResourceManager.GetTextureInfo(_renderingContext, image);

            int textureSlot;

            // Determine texture slot
            if (!_textures.ContainsKey(texture))
            {
                // Texture mechanism is full, emit batched drawing
                if (_textures.Count == _maxTextureUnits)
                {
                    Flush();
                }

                // Unknown texture, assign new slot
                textureSlot = _textures.Count;
                _textures[texture] = textureSlot;
            }
            else
            {
                // Already known texture
                textureSlot = _textures[texture];
            }

            // 
            ref var vtx = ref _vertexArray.InstanceElements[_vertexArray.InstanceCount++];

            vtx.Transform = transform;
            vtx.TextureRect = textureRect;
            vtx.TextureSlot = textureSlot;
            vtx.Color = color;
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        private void SetTemplate(Mesh mesh)
        {
            // 
            Mesh = mesh;

            // Copy vertex data
            for (var i = 0; i < Mesh.Vertices.Count; i++)
            {
                _vertexArray.VertexElements[i].Position = Mesh.Vertices[i].Position;
                _vertexArray.VertexElements[i].UV = Mesh.Vertices[i].UV;
            }

            // Copy index data
            for (var i = 0; i < Mesh.Indices.Count; i++)
            {
                _vertexArray.IndexElements[i] = Mesh.Indices[i];
            }

            // Store vertex and index counts
            _vertexArray.VertexCount = Mesh.Vertices.Count;
            _vertexArray.IndexCount = Mesh.Indices.Count;
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public override void Flush()
        {
            if (_vertexArray.InstanceCount > 0)
            {
                // Perform on the rendering thread
                _renderingContext.Invoke(() =>
                {
                    // Update instance buffer
                    _vertexArray.InstanceBuffer.Update(_vertexArray.InstanceElements, _vertexArray.InstanceCount, 0);

                    // Update template mesh
                    _vertexArray.VertexBuffer.Update(_vertexArray.VertexElements, _vertexArray.VertexCount, 0);
                    _vertexArray.IndexBuffer.Update(_vertexArray.IndexElements, _vertexArray.IndexCount, 0);

                    // Bind textures
                    foreach (var kv in _textures)
                    {
                        var slot = (uint) kv.Value;

                        // 
                        if (_texturesState[slot] != kv.Key)
                        {
                            GL.ActiveTexture(slot);
                            GL.BindTexture(TextureTarget.Texture2D, kv.Key.Handle);
                        }
                    }

                    // Draw elements
                    GL.BindVertexArray(_vertexArray.Handle);
                    GL.DrawElementsInstanced(DrawMode.Triangles, _vertexArray.IndexCount, DrawElementType.UnsignedShort, _vertexArray.InstanceCount);
                    GL.BindVertexArray(0);
                });

                // Update current surface version number, as we have mutated it
                _renderingContext.MarkSurfaceDirty();

                // 
                _vertexArray.InstanceCount = 0;
                _textures.Clear();
            }
        }
    }
}
