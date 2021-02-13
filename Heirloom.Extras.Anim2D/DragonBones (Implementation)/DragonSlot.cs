using DragonBones;

using Heirloom.Drawing;
using Heirloom.Mathematics;

namespace Heirloom.Extras.Anim2D
{
    internal sealed class DragonSlot : Slot
    {
        private readonly MeshBuffer _meshBuffer = new MeshBuffer();
        private Mesh _mesh = new Mesh();

        private DragonArmature _proxy;

        private bool _skewed;

        private BlendMode _currentBlendMode;
        private Image _currentImage;

        private Transform _transform = new Transform();
        private DragonBones.Matrix _transformMatrix = new DragonBones.Matrix();

        public DragonTextureAtlasData CurrentTextureAtlasData
        {
            get
            {
                if (_textureData == null || _textureData.Parent == null)
                {
                    return null;
                }

                return _textureData.Parent as DragonTextureAtlasData;
            }
        }

        protected override void _InitDisplay(object value, bool isRetain)
        {
            // nothing
        }

        protected override void _DisposeDisplay(object value, bool isRelease)
        {
            if (!isRelease)
            {
                // destroy game object
            }
        }

        protected override void _OnUpdateDisplay()
        {
            // unity impl would add MeshRenderer, MeshFilter and allocate MeshBuffer
            _proxy = _armature.Proxy as DragonArmature;
        }

        protected override void _AddDisplay()
        {
            // ...
        }

        protected override void _ReplaceDisplay(object value)
        {
            // ...
        }

        protected override void _RemoveDisplay()
        {
            // ...
        }

        protected override void _UpdateZOrder()
        {
            // ...
        }

        internal override void _UpdateVisible()
        {
            // parent.visible
        }

        internal override void _UpdateBlendMode()
        {
            // ...
            // _blendMode
        }

        protected override void _UpdateColor()
        {
            for (var i = 0; i < _meshBuffer.Vertices.Length; i++)
            {
                var r = _colorTransform.RedMultiplier;
                var g = _colorTransform.GreenMultiplier;
                var b = _colorTransform.BlueMultiplier;
                var a = _colorTransform.AlphaMultiplier;

                _meshBuffer.Vertices[i].Color = new Color(r, g, b, a);
            }
        }

        protected override void _UpdateFrame()
        {
            var currentVerticesData = (_deformVertices != null && _display == _meshDisplay) ? _deformVertices.verticesData : null;

            _meshBuffer.Clear();

            if (_displayIndex >= 0 && _display != null && _textureData is DragonTextureData textureData)
            {
                var currentTextureAtlas = CurrentTextureAtlasData.Image;
                if (currentTextureAtlas != null)
                {
                    //
                    var textureAtlasWidth = CurrentTextureAtlasData.Width > 0.0f ? (int) CurrentTextureAtlasData.Width : currentTextureAtlas.Width;
                    var textureAtlasHeight = CurrentTextureAtlasData.Height > 0.0f ? (int) CurrentTextureAtlasData.Height : currentTextureAtlas.Height;

                    var textureScale = _armature.ArmatureData.scale * textureData.Parent.Scale;
                    var sourceX = textureData.Region.x;
                    var sourceY = textureData.Region.y;
                    var sourceWidth = textureData.Region.Width;
                    var sourceHeight = textureData.Region.Height;

                    if (currentVerticesData != null)
                    {
                        var data = currentVerticesData.data;
                        var meshOffset = currentVerticesData.offset;
                        var intArray = data.intArray;
                        var floatArray = data.floatArray;
                        var vertexCount = intArray[meshOffset + (int) BinaryOffset.MeshVertexCount];
                        var triangleCount = intArray[meshOffset + (int) BinaryOffset.MeshTriangleCount] * 3;
                        int vertexOffset = intArray[meshOffset + (int) BinaryOffset.MeshFloatOffset];
                        if (vertexOffset < 0)
                        {
                            vertexOffset += 65536; // Fixed out of bouds bug. 
                        }

                        var uvOffset = vertexOffset + (vertexCount * 2);

                        // Allocate vertex arrays
                        if (_meshBuffer.Vertices?.Length != vertexCount) { _meshBuffer.Vertices = new MeshVertex[vertexCount]; }
                        if (_meshBuffer.Raw?.Length != vertexCount) { _meshBuffer.Raw = new Vector[vertexCount]; }

                        // Allocate triangle arrays
                        if (_meshBuffer.Triangles?.Length != triangleCount) { _meshBuffer.Triangles = new int[triangleCount]; }

                        // 
                        for (int i = 0, iV = vertexOffset, iU = uvOffset, l = vertexCount; i < l; ++i)
                        {
                            ref var vertex = ref _meshBuffer.Vertices[i];
                            ref var raw = ref _meshBuffer.Raw[i];

                            vertex.UV.X = (sourceX + (floatArray[iU++] * sourceWidth)) / textureAtlasWidth;
                            vertex.UV.Y = (sourceY + (floatArray[iU++] * sourceHeight)) / textureAtlasHeight;

                            raw.X = floatArray[iV++] * textureScale;
                            raw.Y = floatArray[iV++] * textureScale;

                            vertex.Position.X = raw.X;
                            vertex.Position.Y = raw.Y;
                        }

                        for (var i = 0; i < triangleCount; ++i)
                        {
                            _meshBuffer.Triangles[i] = intArray[meshOffset + (int) BinaryOffset.MeshVertexIndices + i];
                        }

                        var isSkinned = currentVerticesData.weight != null;
                        if (isSkinned)
                        {
                            _IdentityTransform();
                        }
                    }
                    else
                    {
                        // TODO: QUAD OPTIMIZATION?

                        if (_meshBuffer.Raw == null || _meshBuffer.Raw.Length != 4)
                        {
                            _meshBuffer.Vertices = new MeshVertex[4];
                            _meshBuffer.Raw = new Vector[4];
                        }

                        // Normal texture.                        
                        for (int i = 0, l = 4; i < l; ++i)
                        {
                            var u = 0.0f;
                            var v = 0.0f;

                            switch (i)
                            {
                                case 0:
                                    break;

                                case 1:
                                    u = 1.0f;
                                    break;

                                case 2:
                                    u = 1.0f;
                                    v = 1.0f;
                                    break;

                                case 3:
                                    v = 1.0f;
                                    break;

                                default:
                                    break;
                            }

                            var scaleWidth = sourceWidth * textureScale;
                            var scaleHeight = sourceHeight * textureScale;
                            var pivotX = _pivotX;
                            var pivotY = _pivotY;

                            ref var vertex = ref _meshBuffer.Vertices[i];
                            ref var raw = ref _meshBuffer.Raw[i];

                            if (textureData.Rotated)
                            {
                                var temp = scaleWidth;
                                scaleWidth = scaleHeight;
                                scaleHeight = temp;

                                pivotX = scaleWidth - _pivotX;
                                pivotY = scaleHeight - _pivotY;

                                vertex.UV.X = (sourceX + ((1.0f - v) * sourceWidth)) / textureAtlasWidth;
                                vertex.UV.Y = (sourceY + (u * sourceHeight)) / textureAtlasHeight;
                            }
                            else
                            {
                                vertex.UV.X = (sourceX + (u * sourceWidth)) / textureAtlasWidth;
                                vertex.UV.Y = (sourceY + (v * sourceHeight)) / textureAtlasHeight;
                            }

                            raw.X = (u * scaleWidth) - pivotX;
                            raw.Y = (v * scaleHeight) - pivotY;

                            vertex.Position.X = raw.X;
                            vertex.Position.Y = raw.Y;
                        }

                        _meshBuffer.Triangles = new[] { 0, 1, 2, 0, 2, 3 };
                    }

                    _currentImage = currentTextureAtlas;
                    _currentBlendMode = BlendMode.Normal;
                    _blendModeDirty = true;
                    _visibleDirty = true;
                    _colorDirty = true;

                    return;
                }
            }
            else
            {
                // Nothing to render
                _currentImage = null;
            }
        }

        protected override void _UpdateMesh()
        {
            var scale = _armature.ArmatureData.scale;
            var deformVertices = _deformVertices.vertices;
            var bones = _deformVertices.bones;
            var hasDeform = deformVertices.Count > 0;
            var verticesData = _deformVertices.verticesData;
            var weightData = verticesData.weight;

            var data = verticesData.data;
            var vertextCount = data.intArray[verticesData.offset + (int) BinaryOffset.MeshVertexCount];

            if (weightData != null)
            {
                int weightFloatOffset = data.intArray[weightData.offset + 1/*(int)BinaryOffset.MeshWeightOffset*/];
                if (weightFloatOffset < 0)
                {
                    weightFloatOffset += 65536; // Fixed out of bouds bug. 
                }

                var iB = weightData.offset + (int) BinaryOffset.WeigthBoneIndices + weightData.bones.Count;
                var iV = weightFloatOffset;
                var iF = 0;

                for (var i = 0; i < vertextCount; ++i)
                {
                    var boneCount = data.intArray[iB++];
                    float xG = 0.0f, yG = 0.0f;
                    for (var j = 0; j < boneCount; ++j)
                    {
                        var boneIndex = data.intArray[iB++];
                        var bone = bones[boneIndex];
                        if (bone != null)
                        {
                            var matrix = bone.GlobalTransformMatrix;
                            var weight = data.floatArray[iV++];
                            var xL = data.floatArray[iV++] * scale;
                            var yL = data.floatArray[iV++] * scale;

                            if (hasDeform)
                            {
                                xL += deformVertices[iF++];
                                yL += deformVertices[iF++];
                            }

                            xG += ((matrix.A * xL) + (matrix.C * yL) + matrix.Tx) * weight;
                            yG += ((matrix.B * xL) + (matrix.D * yL) + matrix.Ty) * weight;
                        }
                    }

                    ref var vertex = ref _meshBuffer.Vertices[i];
                    vertex.Position.X = xG;
                    vertex.Position.Y = yG;
                }
            }
            else if (hasDeform)
            {
                int vertexOffset = data.intArray[verticesData.offset + (int) BinaryOffset.MeshFloatOffset];
                if (vertexOffset < 0)
                {
                    vertexOffset += 65536; // Fixed out of bouds bug. 
                }

                // 
                for (int i = 0, iV = 0, iF = 0, l = vertextCount; i < l; ++i)
                {
                    ref var vertex = ref _meshBuffer.Vertices[i];
                    ref var raw = ref _meshBuffer.Raw[i];

                    var rx = (data.floatArray[vertexOffset + iV++] * scale) + deformVertices[iF++];
                    var ry = (data.floatArray[vertexOffset + iV++] * scale) + deformVertices[iF++];

                    raw.X = rx;
                    raw.Y = ry;

                    vertex.Position.X = rx;
                    vertex.Position.Y = ry;
                }

                //// todo: Compute bounds
                //_meshBuffer.Update(_mesh);
            }
        }

        protected override void _UpdateTransform()
        {
            UpdateGlobalTransform(); // Update transform.

            // Modify mesh skew...?
            // TODO child armature skew...?
            if (_display == _rawDisplay || _display == _meshDisplay)
            {
                var skew = Global.skew;
                var dSkew = skew;

                if (_armature.FlipX && _armature.FlipY)
                {
                    dSkew = -skew + Transform.PI;
                }
                else if (!_armature.FlipX && !_armature.FlipY)
                {
                    dSkew = -skew - Transform.PI;
                }

                var skewed = dSkew < -0.01f || 0.01f < dSkew;
                if (_skewed || skewed)
                {
                    _skewed = skewed;

                    var isPositive = Global.scaleX >= 0.0f;
                    var cos = Calc.Cos(dSkew);
                    var sin = Calc.Sin(dSkew);

                    for (int i = 0, l = _meshBuffer.Vertices.Length; i < l; ++i)
                    {
                        var x = _meshBuffer.Raw[i].X;
                        var y = _meshBuffer.Raw[i].Y;

                        if (isPositive)
                        {
                            _meshBuffer.Vertices[i].Position.X = x + (y * sin);
                        }
                        else
                        {
                            _meshBuffer.Vertices[i].Position.X = -x + (y * sin);
                        }

                        _meshBuffer.Vertices[i].Position.Y = y * cos;
                    }
                }

                //var skew = global.skew;
                //var dSkew = skew;

                //if (_armature.flipX && _armature.flipY)
                //{
                //    dSkew = -skew + Transform.PI;
                //}
                //else if (!_armature.flipX && !_armature.flipY)
                //{
                //    dSkew = -skew - Transform.PI;
                //}

                //transform.skew = dSkew;

                // Set slot position
                _transform.x = Global.x;
                _transform.y = Global.y;

                // Set slot rotation
                _transform.rotation = Global.rotation;

                // Set slot scale
                _transform.scaleX = (_armature.FlipX ? -1 : +1) * Global.scaleX;
                _transform.scaleY = (_armature.FlipY ? +1 : -1) * Global.scaleY; 
            }

            if (_childArmature != null)
            {
                _childArmature.FlipX = _armature.FlipX;
                _childArmature.FlipY = _armature.FlipY;
            }
        }

        protected override void _IdentityTransform()
        {
            // Reset transform
            _transform.x = 0F;
            _transform.y = 0F;
            _transform.rotation = 0F;
            _transform.skew = 0F;
            _transform.scaleX = 1F;
            _transform.scaleY = 1F;
        }

        internal void Draw(GraphicsContext gfx, Mathematics.Matrix matrix)
        {
            if (_currentImage == null) { return; }

            // todo: isDirty or version check to reduce work
            _meshBuffer.Update(_mesh);

            // Convert to matrix
            _transform.ToMatrix(_transformMatrix);

            // Copy DB Matrix to Heirlooom Matrix
            var m = Mathematics.Matrix.Identity;
            m.M0 = _transformMatrix.A;
            m.M1 = _transformMatrix.B;
            m.M2 = _transformMatrix.Tx;
            m.M3 = _transformMatrix.C;
            m.M4 = _transformMatrix.D;
            m.M5 = _transformMatrix.Ty;

            // Draw slot
            gfx.Draw(_mesh, _currentImage, matrix * m);

            //// Draw triangle wireframe
            //var xform = matrix * m;
            //for (var i = 0; i < _mesh.Vertices.Count; i += 3)
            //{
            //    var a = xform * _mesh.Vertices[i + 0].Position;
            //    var b = xform * _mesh.Vertices[i + 1].Position;
            //    var c = xform * _mesh.Vertices[i + 2].Position;

            //    gfx.DrawTriangleOutline(a, b, c);
            //}
        }

        private class MeshBuffer
        {
            public MeshVertex[] Vertices;
            public Vector[] Raw;

            public int[] Triangles;

            internal void Clear()
            {
                // 
            }

            internal void Update(Mesh mesh)
            {
                mesh.Clear();

                for (var i = 0; i < Triangles.Length; i += 3)
                {
                    var i0 = Triangles[i + 0];
                    var i1 = Triangles[i + 1];
                    var i2 = Triangles[i + 2];

                    mesh.AddVertex(Vertices[i0]);
                    mesh.AddVertex(Vertices[i1]);
                    mesh.AddVertex(Vertices[i2]);
                }
            }
        }
    }
}

