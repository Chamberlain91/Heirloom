using System;

using DragonBones;

using Heirloom.Drawing;
using Heirloom.Mathematics;

namespace Heirloom.Extras.Animation
{
    internal sealed class DragonSlot : Slot
    {
        private static readonly int[] _quadTriangles = new[] { 0, 1, 2, 0, 2, 3 };

        private readonly MeshBuffer _meshBuffer = new MeshBuffer();

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
            // ...
        }

        protected override void _DisposeDisplay(object value, bool isRelease)
        {
            // ...
        }

        protected override void _OnUpdateDisplay()
        {
            // ...
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
            // ...
            // parent.visible
        }

        internal override void _UpdateBlendMode()
        {
            // ...
            // _blendMode
        }

        protected override void _UpdateColor()
        {
            for (var i = 0; i < _meshBuffer.Mesh.Vertices.Count; i++)
            {
                var r = _colorTransform.RedMultiplier;
                var g = _colorTransform.GreenMultiplier;
                var b = _colorTransform.BlueMultiplier;
                var a = _colorTransform.AlphaMultiplier;

                var v = _meshBuffer.Mesh.Vertices[i];
                v.Color = new Color(r, g, b, a);
                _meshBuffer.Mesh.Vertices[i] = v;
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

                        // Ensure mesh buffers are large enough for the mesh data
                        _meshBuffer.Resize(vertexCount, triangleCount);

                        //
                        var iUV = uvOffset;
                        var iXY = vertexOffset;
                        for (var i = 0; i < vertexCount; i++)
                        {
                            // Compute raw (untransformed?) position
                            ref var raw = ref _meshBuffer.Raw[i];
                            raw.X = floatArray[iXY++] * textureScale;
                            raw.Y = floatArray[iXY++] * textureScale;

                            // Compute texture atlas coordinates
                            var u = (sourceX + (floatArray[iUV++] * sourceWidth)) / textureAtlasWidth;
                            var v = (sourceY + (floatArray[iUV++] * sourceHeight)) / textureAtlasHeight;

                            var vertex = _meshBuffer.Mesh.Vertices[i];
                            vertex.Position = raw;
                            vertex.UV = new Vector(u, v);

                            _meshBuffer.Mesh.Vertices[i] = vertex;
                        }

                        for (var t = 0; t < triangleCount; ++t)
                        {
                            var i = intArray[meshOffset + (int) BinaryOffset.MeshVertexIndices + t];
                            _meshBuffer.Mesh.Indices[t] = i;
                        }

                        var isSkinned = currentVerticesData.weight != null;
                        if (isSkinned)
                        {
                            _IdentityTransform();
                        }
                    }
                    else
                    {
                        // Ensure mesh buffers are large enough for the mesh data
                        _meshBuffer.Resize(4, 6);

                        // Copy quad indices
                        _meshBuffer.Mesh.AddIndices(_quadTriangles);

                        // Normal quad image
                        for (var i = 0; i < 4; i++)
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

                            // ref var vertex = ref _meshBuffer.Vertices[i];
                            ref var raw = ref _meshBuffer.Raw[i];

                            var UV = Vector.Zero;

                            if (textureData.Rotated)
                            {
                                var temp = scaleWidth;
                                scaleWidth = scaleHeight;
                                scaleHeight = temp;

                                pivotX = scaleWidth - _pivotX;
                                pivotY = scaleHeight - _pivotY;

                                UV.X = (sourceX + ((1.0f - v) * sourceWidth)) / textureAtlasWidth;
                                UV.Y = (sourceY + (u * sourceHeight)) / textureAtlasHeight;
                            }
                            else
                            {
                                UV.X = (sourceX + (u * sourceWidth)) / textureAtlasWidth;
                                UV.Y = (sourceY + (v * sourceHeight)) / textureAtlasHeight;
                            }

                            raw.X = (u * scaleWidth) - pivotX;
                            raw.Y = (v * scaleHeight) - pivotY;

                            // Overwrite vertex position and uv
                            var vertex = _meshBuffer.Mesh.Vertices[i];
                            vertex.Position = raw;
                            vertex.UV = UV;

                            _meshBuffer.Mesh.Vertices[i] = vertex;
                        }
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

                    // Overwrite vertex position
                    var vertex = _meshBuffer.Mesh.Vertices[i];
                    vertex.Position = new Vector(xG, yG);

                    _meshBuffer.Mesh.Vertices[i] = vertex;
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
                    //ref var vertex = ref _meshBuffer.Vertices[i];
                    ref var raw = ref _meshBuffer.Raw[i];

                    var rx = (data.floatArray[vertexOffset + iV++] * scale) + deformVertices[iF++];
                    var ry = (data.floatArray[vertexOffset + iV++] * scale) + deformVertices[iF++];

                    raw.X = rx;
                    raw.Y = ry;

                    // Overwrite vertex position
                    var vertex = _meshBuffer.Mesh.Vertices[i];
                    vertex.Position = raw;

                    _meshBuffer.Mesh.Vertices[i] = vertex;
                }
            }
        }

        protected override void _UpdateTransform()
        {
            UpdateGlobalTransform(); // Update transform.

            var flipX = _armature.FlipX;
            var flipY = _armature.FlipY;

            // Modify mesh skew...?
            // TODO child armature skew...?
            if (_display == _rawDisplay || _display == _meshDisplay)
            {
                //var skew = Global.skew;
                //var dSkew = skew;

                //if (flipX && flipY)
                //{
                //    dSkew = -skew + Transform.PI;
                //}
                //else if (!flipX && !flipY)
                //{
                //    dSkew = -skew - Transform.PI;
                //}

                //var skewed = dSkew < -0.01f || 0.01f < dSkew;
                //if (_skewed || skewed)
                //{
                //    _skewed = skewed;

                //    var isPositive = Global.scaleX >= 0.0f;
                //    var cos = Calc.Cos(dSkew);
                //    var sin = Calc.Sin(dSkew);

                //    for (int i = 0, l = _meshBuffer.Vertices.Length; i < l; ++i)
                //    {
                //        var x = _meshBuffer.Raw[i].X;
                //        var y = _meshBuffer.Raw[i].Y;

                //        if (isPositive)
                //        {
                //            _meshBuffer.Vertices[i].Position.X = x + (y * sin);
                //        }
                //        else
                //        {
                //            _meshBuffer.Vertices[i].Position.X = -x + (y * sin);
                //        }

                //        _meshBuffer.Vertices[i].Position.Y = y * cos;
                //    }

                //    // Mark mesh for update
                //    _isMeshDirty = true;
                //}

                // Set slot position
                _transform.x = Global.x;
                _transform.y = Global.y;

                // Set slot rotation
                _transform.rotation = Global.rotation;

                // todo: comment on what this is actually doing
                // these dragonbones people have bad code for something so neat

                if (flipX != flipY)
                {
                    _transform.rotation = -_transform.rotation;
                }

                if (flipX || flipY)
                {
                    if (flipX && flipY)
                    {
                        _transform.rotation += Calc.Pi;
                    }
                    else
                    {
                        if (flipX)
                        {
                            _transform.rotation = Calc.Pi - _transform.rotation;
                        }
                        else
                        {
                            _transform.rotation = -_transform.rotation;
                        }
                    }
                }

                // Set slot scale
                _transform.scaleX = (flipX ? -1 : +1) * Global.scaleX;
                _transform.scaleY = (flipY ? -1 : +1) * Global.scaleY;
            }

            if (_childArmature != null)
            {
                _childArmature.FlipX = flipX;
                _childArmature.FlipY = flipY;
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

            if (Visible)
            {
                // Convert dragonebones to heirloom matrix 
                _transform.ToMatrix(_transformMatrix);
                var transformMatrix = Helper.GetHeirloomMatrix(_transformMatrix);

                // Combine transform and draw
                Mathematics.Matrix.Multiply(matrix, transformMatrix, ref matrix);
                gfx.Draw(_meshBuffer.Mesh, _currentImage, matrix);
            }

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
            public Mesh Mesh = new Mesh();
            public Vector[] Raw = new Vector[4];

            public int TriangleCount;
            public int VertexCount;

            internal void Resize(int vertexCount, int triangleCount)
            {
                if (vertexCount > Mesh.Vertices.Count)
                {
                    Array.Resize(ref Raw, vertexCount);
                }

                Mesh.SetIndexCount(triangleCount);
                Mesh.SetVertexCount(vertexCount);

                TriangleCount = triangleCount;
                VertexCount = vertexCount;
            }

            internal void Clear()
            {
                TriangleCount = 0;
                VertexCount = 0;
            }
        }
    }
}

