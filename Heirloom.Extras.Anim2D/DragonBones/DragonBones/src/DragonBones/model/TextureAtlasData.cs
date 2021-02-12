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
    /// - The texture atlas data.
    /// </summary>
    /// <version>DragonBones 3.0</version>
    /// <language>en_US</language>

    /// <summary>
    /// - 贴图集数据。
    /// </summary>
    /// <version>DragonBones 3.0</version>
    /// <language>zh_CN</language>
    internal abstract class TextureAtlasData : BaseObject
    {
        /// <private/>
        public bool AutoSearch;
        /// <private/>
        public uint Width;
        /// <private/>
        public uint Height;
        /// <private/>
        public float Scale;
        /// <summary>
        /// - The texture atlas name.
        /// </summary>
        /// <version>DragonBones 3.0</version>
        /// <language>en_US</language>

        /// <summary>
        /// - 贴图集名称。
        /// </summary>
        /// <version>DragonBones 3.0</version>
        /// <language>zh_CN</language>
        public string Name;
        /// <summary>
        /// - The image path of the texture atlas.
        /// </summary>
        /// <version>DragonBones 3.0</version>
        /// <language>en_US</language>

        /// <summary>
        /// - 贴图集图片路径。
        /// </summary>
        /// <version>DragonBones 3.0</version>
        /// <language>zh_CN</language>
        public string ImagePath;
        /// <private/>
        public readonly Dictionary<string, TextureData> Textures = new Dictionary<string, TextureData>();
        public TextureAtlasData()
        {
        }
        /// <inheritDoc/>
        protected override void _OnClear()
        {
            foreach (var value in Textures.Values)
            {
                value.ReturnToPool();
            }

            AutoSearch = false;
            Width = 0;
            Height = 0;
            Scale = 1.0f;
            Textures.Clear();
            Name = "";
            ImagePath = "";
        }
        /// <private/>
        public void CopyFrom(TextureAtlasData value)
        {
            AutoSearch = value.AutoSearch;
            Scale = value.Scale;
            Width = value.Width;
            Height = value.Height;
            Name = value.Name;
            ImagePath = value.ImagePath;

            foreach (var texture in Textures.Values)
            {
                texture.ReturnToPool();
            }

            Textures.Clear();

            foreach (var pair in value.Textures)
            {
                var texture = CreateTexture();
                texture.CopyFrom(pair.Value);
                Textures[pair.Key] = texture;
            }
        }
        /// <internal/>
        /// <private/>
        public abstract TextureData CreateTexture();
        /// <internal/>
        /// <private/>
        public void AddTexture(TextureData value)
        {
            if (value != null)
            {
                if (Textures.ContainsKey(value.Name))
                {
                    Helper.Assert(false, "Same texture: " + value.Name);
                    Textures[value.Name].ReturnToPool();
                }

                value.Parent = this;
                Textures[value.Name] = value;
            }
        }
        /// <private/>
        public TextureData GetTexture(string name)
        {
            return Textures.ContainsKey(name) ? Textures[name] : null;
        }
    }
}
