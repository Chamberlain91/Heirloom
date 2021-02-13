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
    /// - The slot data.
    /// </summary>
    /// <version>DragonBones 3.0</version>
    /// <language>en_US</language>

    /// <summary>
    /// - 插槽数据。
    /// </summary>
    /// <version>DragonBones 3.0</version>
    /// <language>zh_CN</language>
    internal class SlotData : BaseObject
    {
        /// <internal/>
        /// <private/>
        public static readonly ColorTransform DEFAULT_COLOR = new ColorTransform();

        /// <internal/>
        /// <private/>
        public static ColorTransform CreateColor()
        {
            return new ColorTransform();
        }

        /// <private/>
        public BlendMode blendMode;
        /// <private/>
        public int displayIndex;
        /// <private/>
        public int zOrder;
        /// <summary>
        /// - The slot name.
        /// </summary>
        /// <version>DragonBones 3.0</version>
        /// <language>en_US</language>

        /// <summary>
        /// - 插槽名称。
        /// </summary>
        /// <version>DragonBones 3.0</version>
        /// <language>zh_CN</language>
        public string name;
        /// <private/>
        public ColorTransform color = null; // Initial value.
        /// <private/>
        public UserData userData = null; // Initial value.
        /// <summary>
        /// - The parent bone data.
        /// </summary>
        /// <version>DragonBones 3.0</version>
        /// <language>en_US</language>

        /// <summary>
        /// - 父骨骼数据。
        /// </summary>
        /// <version>DragonBones 3.0</version>
        /// <language>zh_CN</language>
        public BoneData parent;
        /// <inheritDoc/>
        protected override void _OnClear()
        {
            if (userData != null)
            {
                userData.ReturnToPool();
            }

            blendMode = BlendMode.Normal;
            displayIndex = 0;
            zOrder = 0;
            name = "";
            color = null; //
            userData = null;
            parent = null; //
        }
    }
}
