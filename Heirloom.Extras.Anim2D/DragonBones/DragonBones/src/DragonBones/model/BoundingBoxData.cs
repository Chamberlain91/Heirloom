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

namespace DragonBones
{
    /// <summary>
    /// - The base class of bounding box data.
    /// </summary>
    /// <see cref="DragonBones.RectangleData"/>
    /// <see cref="DragonBones.EllipseData"/>
    /// <see cref="DragonBones.PolygonData"/>
    /// <version>DragonBones 5.0</version>
    /// <language>en_US</language>

    /// <summary>
    /// - 边界框数据基类。
    /// </summary>
    /// <see cref="DragonBones.RectangleData"/>
    /// <see cref="DragonBones.EllipseData"/>
    /// <see cref="DragonBones.PolygonData"/>
    /// <version>DragonBones 5.0</version>
    /// <language>zh_CN</language>
    internal abstract class BoundingBoxData : BaseObject
    {
        /// <summary>
        /// - The bounding box type.
        /// </summary>
        /// <version>DragonBones 5.0</version>
        /// <language>en_US</language>

        /// <summary>
        /// - 边界框类型。
        /// </summary>
        /// <version>DragonBones 5.0</version>
        /// <language>zh_CN</language>
        public BoundingBoxType type;
        /// <private/>
        public uint color;
        /// <private/>
        public float width;
        /// <private/>
        public float height;

        /// <private/>
        protected override void _OnClear()
        {
            color = 0x000000;
            width = 0.0f;
            height = 0.0f;
        }

        /// <summary>
        /// - Check whether the bounding box contains a specific point. (Local coordinate system)
        /// </summary>
        /// <version>DragonBones 5.0</version>
        /// <language>en_US</language>

        /// <summary>
        /// - 检查边界框是否包含特定点。（本地坐标系）
        /// </summary>
        /// <version>DragonBones 5.0</version>
        /// <language>zh_CN</language>
        public abstract bool ContainsPoint(float pX, float pY);

        /// <summary>
        /// - Check whether the bounding box intersects a specific segment. (Local coordinate system)
        /// </summary>
        /// <version>DragonBones 5.0</version>
        /// <language>en_US</language>

        /// <summary>
        /// - 检查边界框是否与特定线段相交。（本地坐标系）
        /// </summary>
        /// <version>DragonBones 5.0</version>
        /// <language>zh_CN</language>
        public abstract int IntersectsSegment(float xA, float yA, float xB, float yB,
                                                Point intersectionPointA = null,
                                                Point intersectionPointB = null,
                                                Point normalRadians = null);
    }
}
