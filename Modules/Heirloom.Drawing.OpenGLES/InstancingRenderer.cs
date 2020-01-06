using System;
using System.Collections.Generic;
using System.Runtime.CompilerServices;

using Heirloom.Math;
using Heirloom.OpenGLES;

namespace Heirloom.Drawing.OpenGLES
{
    internal unsafe class InstancingRenderer : Renderer
    // todo: better design, weird coupling between Renderer and Graphics
    // could probably create a "gl state" tracker and move rendering impl into OpenGLGraphics itself
    {
        private readonly OpenGLGraphics _context;
        private readonly VertexArray _vertexArray;

        private readonly Dictionary<Texture, int> _textures;
        private readonly Texture[] _texturesState;

        private Mesh _mesh;
        private uint _meshVersion;

        #region Constructors

        public InstancingRenderer(OpenGLGraphics context)
        {
            _context = context;

            // Query for maximum textures 
            _texturesState = new Texture[context.Capabilities.MaxTextureUnits]; // todo: reserve 3 for effect units?
            _textures = new Dictionary<Texture, int>(context.Capabilities.MaxTextureUnits);

            // Create buffer sets
            _vertexArray = new VertexArray();
        }
         
        #endregion

        #region Properties

        public override bool IsDirty => _vertexArray.InstanceCount > 0;

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
            if (_vertexArray.InstanceCount == _vertexArray.InstanceElements.Length)
            {
                _context.Flush();
            }

            // Get OpenGL texture and packed rect
            var (texture, textureRect) = ResourceManager.GetTextureInfo(_context, image);

            int textureSlot;

            // Determine texture slot
            if (!_textures.ContainsKey(texture))
            {
                // Texture mechanism is full, emit batched drawing
                if (_textures.Count == _context.Capabilities.MaxTextureUnits)
                {
                    _context.Flush();
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
            _mesh = mesh;
            _meshVersion = mesh.Version;

            // Copy vertex data
            for (var i = 0; i < _mesh.Vertices.Count; i++)
            {
                _vertexArray.VertexElements[i].Position = _mesh.Vertices[i].Position;
                _vertexArray.VertexElements[i].UV = _mesh.Vertices[i].UV;
            }

            // Copy index data
            for (var i = 0; i < _mesh.Indices.Count; i++)
            {
                var index = _mesh.Indices[i];
                if (index >= ushort.MaxValue) { throw new InvalidOperationException($"Mesh must not have indices greater then or equal to {ushort.MaxValue}."); }

                _vertexArray.IndexElements[i] = (ushort) _mesh.Indices[i];
            }

            // Store vertex and index counts
            _vertexArray.VertexCount = _mesh.Vertices.Count;
            _vertexArray.IndexCount = _mesh.Indices.Count;
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public override void FlushPendingBatch()
        {
            if (_vertexArray.InstanceCount > 0)
            {
                // Perform on the rendering thread
                // todo: make invoke non-blocking but we need a vertex array ring buffer
                _context.Invoke(() =>
                {
                    // Update GPU side buffers
                    _vertexArray.Update();

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

                    // If indices were defined, use them
                    if (_vertexArray.IndexCount > 0) { GL.DrawElementsInstanced(DrawMode.Triangles, _vertexArray.IndexCount, DrawElementType.UnsignedShort, _vertexArray.InstanceCount); }
                    else { GL.DrawArraysInstanced(DrawMode.Triangles, _vertexArray.VertexCount, _vertexArray.InstanceCount); }

                    GL.BindVertexArray(0);
                });

                // Update current surface version number, as we have mutated it
                _context.MarkSurfaceDirty();

                // 
                _vertexArray.InstanceCount = 0;
                _textures.Clear();
            }
        }
    }
}
