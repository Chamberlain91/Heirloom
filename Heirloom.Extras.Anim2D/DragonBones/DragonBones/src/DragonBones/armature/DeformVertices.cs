/**
 * The MIT License (MIT)
 *
 * Copyright (c) 2012-2017 DragonBones team and other contributors
 *
 * Permission is hereby granted, free of charge, to any person obtaining a copy of
 * this software and associated documentation files (the "Software"), to deal in
 * the Software without restriction, including without limitation the rights to
 * use, copy, modify, merge, publish, distribute, sublicense, and/or sell copies of
 * the Software, and to permit persons to whom the Software is furnished to do so,
 * subject to the following conditions:
 * 
 * The above copyright notice and this permission notice shall be included in all
 * copies or substantial portions of the Software.
 * 
 * THE SOFTWARE IS PROVIDED "AS IS", WITHOUT WARRANTY OF ANY KIND, EXPRESS OR
 * IMPLIED, INCLUDING BUT NOT LIMITED TO THE WARRANTIES OF MERCHANTABILITY, FITNESS
 * FOR A PARTICULAR PURPOSE AND NONINFRINGEMENT. IN NO EVENT SHALL THE AUTHORS OR
 * COPYRIGHT HOLDERS BE LIABLE FOR ANY CLAIM, DAMAGES OR OTHER LIABILITY, WHETHER
 * IN AN ACTION OF CONTRACT, TORT OR OTHERWISE, ARISING FROM, OUT OF OR IN
 * CONNECTION WITH THE SOFTWARE OR THE USE OR OTHER DEALINGS IN THE SOFTWARE.
 */
using System.Collections.Generic;
namespace DragonBones
{
    /// <internal/>
    internal class DeformVertices : BaseObject
    {
        public bool verticesDirty;
        public readonly List<float> vertices = new List<float>();
        public readonly List<Bone> bones = new List<Bone>();
        public VerticesData verticesData;

        protected override void _OnClear()
        {
            verticesDirty = false;
            vertices.Clear();
            bones.Clear();
            verticesData = null;
        }

        public void init(VerticesData verticesDataValue, Armature armature)
        {
            verticesData = verticesDataValue;

            if (verticesData != null)
            {
                var vertexCount = 0;
                if (verticesData.weight != null)
                {
                    vertexCount = verticesData.weight.count * 2;
                }
                else
                {
                    vertexCount = (int) verticesData.data.intArray[verticesData.offset + (int) BinaryOffset.MeshVertexCount] * 2;
                }

                verticesDirty = true;
                vertices.ResizeList(vertexCount);
                bones.Clear();
                //
                for (int i = 0, l = vertices.Count; i < l; ++i)
                {
                    vertices[i] = 0.0f;
                }

                if (verticesData.weight != null)
                {
                    for (int i = 0, l = verticesData.weight.bones.Count; i < l; ++i)
                    {
                        var bone = armature.GetBone(verticesData.weight.bones[i].name);
                        bones.Add(bone);
                    }
                }
            }
            else
            {
                verticesDirty = false;
                vertices.Clear();
                bones.Clear();
                verticesData = null;
            }
        }

        public bool isBonesUpdate()
        {
            foreach (var bone in bones)
            {
                if (bone != null && bone._childrenTransformDirty)
                {
                    return true;
                }
            }

            return false;
        }
    }
}
