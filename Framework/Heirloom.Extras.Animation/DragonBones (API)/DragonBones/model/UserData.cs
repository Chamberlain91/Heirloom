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
    /// <summary>
    /// - The user custom data.
    /// </summary>
    /// <version>DragonBones 5.0</version>
    /// <language>en_US</language>

    /// <summary>
    /// - 用户自定义数据。
    /// </summary>
    /// <version>DragonBones 5.0</version>
    /// <language>zh_CN</language>
    internal class UserData : BaseObject
    {
        /// <summary>
        /// - The custom int numbers.
        /// </summary>
        /// <version>DragonBones 5.0</version>
        /// <language>en_US</language>

        /// <summary>
        /// - 自定义整数。
        /// </summary>
        /// <version>DragonBones 5.0</version>
        /// <language>zh_CN</language>
        public readonly List<int> ints = new List<int>();
        /// <summary>
        /// - The custom float numbers.
        /// </summary>
        /// <version>DragonBones 5.0</version>
        /// <language>en_US</language>

        /// <summary>
        /// - 自定义浮点数。
        /// </summary>
        /// <version>DragonBones 5.0</version>
        /// <language>zh_CN</language>
        public readonly List<float> floats = new List<float>();
        /// <summary>
        /// - The custom strings.
        /// </summary>
        /// <version>DragonBones 5.0</version>
        /// <language>en_US</language>

        /// <summary>
        /// - 自定义字符串。
        /// </summary>
        /// <version>DragonBones 5.0</version>
        /// <language>zh_CN</language>
        public readonly List<string> strings = new List<string>();

        /// <inheritDoc/>
        protected override void _OnClear()
        {
            ints.Clear();
            floats.Clear();
            strings.Clear();
        }

        /// <internal/>
        /// <private/>
        internal void AddInt(int value)
        {
            ints.Add(value);
        }
        /// <internal/>
        /// <private/>
        internal void AddFloat(float value)
        {
            floats.Add(value);
        }
        /// <internal/>
        /// <private/>
        internal void AddString(string value)
        {
            strings.Add(value);
        }

        /// <summary>
        /// - Get the custom int number.
        /// </summary>
        /// <version>DragonBones 5.0</version>
        /// <language>en_US</language>

        /// <summary>
        /// - 获取自定义整数。
        /// </summary>
        /// <version>DragonBones 5.0</version>
        /// <language>zh_CN</language>
        public int GetInt(int index = 0)
        {
            return index >= 0 && index < ints.Count ? ints[index] : 0;
        }
        /// <summary>
        /// - Get the custom float number.
        /// </summary>
        /// <version>DragonBones 5.0</version>
        /// <language>en_US</language>

        /// <summary>
        /// - 获取自定义浮点数。
        /// </summary>
        /// <version>DragonBones 5.0</version>
        /// <language>zh_CN</language>
        public float GetFloat(int index = 0)
        {
            return index >= 0 && index < floats.Count ? floats[index] : 0.0f;
        }
        /// <summary>
        /// - Get the custom string.
        /// </summary>
        /// <version>DragonBones 5.0</version>
        /// <language>en_US</language>

        /// <summary>
        /// - 获取自定义字符串。
        /// </summary>
        /// <version>DragonBones 5.0</version>
        /// <language>zh_CN</language>
        public string GetString(int index = 0)
        {
            return index >= 0 && index < strings.Count ? strings[index] : string.Empty;
        }
    }
}
