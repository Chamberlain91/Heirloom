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
    /// - The bone data.
    /// </summary>
    /// <version>DragonBones 3.0</version>
    /// <language>en_US</language>

    /// <summary>
    /// - 骨骼数据。
    /// </summary>
    /// <version>DragonBones 3.0</version>
    /// <language>zh_CN</language>
    internal class BoneData : BaseObject
    {
        /// <private/>
        public bool inheritTranslation;
        /// <private/>
        public bool inheritRotation;
        /// <private/>
        public bool inheritScale;
        /// <private/>
        public bool inheritReflection;
        /// <summary>
        /// - The bone length.
        /// </summary>
        /// <version>DragonBones 3.0</version>
        /// <language>en_US</language>

        /// <summary>
        /// - 骨骼长度。
        /// </summary>
        /// <version>DragonBones 3.0</version>
        /// <language>zh_CN</language>
        public float length;
        /// <summary>
        /// - The bone name.
        /// </summary>
        /// <version>DragonBones 3.0</version>
        /// <language>en_US</language>

        /// <summary>
        /// - 骨骼名称。
        /// </summary>
        /// <version>DragonBones 3.0</version>
        /// <language>zh_CN</language>
        public string name;
        /// <private/>
        public readonly Transform transform = new Transform();
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
        public BoneData parent = null;

        /// <inheritDoc/>
        protected override void _OnClear()
        {
            if (userData != null)
            {
                userData.ReturnToPool();
            }

            inheritTranslation = false;
            inheritRotation = false;
            inheritScale = false;
            inheritReflection = false;
            length = 0.0f;
            name = "";
            transform.Identity();
            userData = null;
            parent = null;
        }
    }
}
