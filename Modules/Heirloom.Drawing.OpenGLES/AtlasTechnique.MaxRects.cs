using System;
using System.Collections.Generic;
using System.Runtime.CompilerServices;

using Heirloom.Drawing.OpenGLES.Utilities;
using Heirloom.Math;
using Heirloom.OpenGLES;

namespace Heirloom.Drawing.OpenGLES
{
    internal class MaxRectsAtlasTechnique : AtlasTechnique
    {
        private readonly ConditionalWeakTable<Image, AtlasEntry> _entries;
        private readonly RectanglePacker<Image> _packer;
        private readonly Texture _texture;

        private readonly HashSet<Image> _changeSet = new HashSet<Image>();

        public MaxRectsAtlasTechnique(OpenGLGraphics graphics)
            : base(graphics)
        {
            // 
            var pageSize = GraphicsAdapter.Capabilities.MaxImageSize;

            // 
            _entries = new ConditionalWeakTable<Image, AtlasEntry>();
            _packer = new MaxrectsPacker<Image>(pageSize, pageSize);

            // Allocate texture (page)
            _texture = Graphics.Invoke(() => new Texture(_packer.Size));
        }

        internal override void GetTextureInformation(Image image, out Texture texture, out Rectangle uvRect)
        {
            // If the image entry is already known...
            if (_entries.TryGetValue(image, out var entry))
            {
                texture = entry.Texture;
                uvRect = entry.UVRect;
            }
            // 
            else if (_packer.Add(image, image.Size))
            {
                // Successfully added image to packing
                var rect = _packer.GetRectangle(image);

                // Compute and emit UV space rectangle
                uvRect.X = rect.X / (float) _texture.Width;
                uvRect.Y = rect.Y / (float) _texture.Height;
                uvRect.Width = rect.Width / (float) _texture.Width;
                uvRect.Height = rect.Height / (float) _texture.Height;

                // Emit texture
                texture = _texture; // feature for multiple pages later

                // Record image entry
                _entries.Add(image, entry = new AtlasEntry
                {
                    Texture = texture,
                    UVRect = uvRect,
                    Rect = rect
                });
            }
            else
            {
                throw new InvalidOperationException("Unable to insert image...");
            }

            // Image and texture are out of sync, write image to texture
            if (entry.Version != image.Version)
            {
                // Append image to change set
                _changeSet.Add(image);

                // Mark entry as up to date. We will actually commit the changes
                // shortly before draw batch.
                entry.Version = image.Version;
            }
        }

        internal override void CommitChanges()
        {
            if (_changeSet.Count > 0)
            {
                GL.BindTexture(TextureTarget.Texture2D, _texture.Handle);

                foreach (var image in _changeSet)
                {
                    _entries.TryGetValue(image, out var entry); // must pass, because the change set holds the key
                    _texture.Update(entry.Rect.X, entry.Rect.Y, image);
                }

                // 
                _changeSet.Clear();
            }
        }

        private class AtlasEntry
        {
            public Texture Texture;
            public IntRectangle Rect;
            public Rectangle UVRect;
            public uint Version;
        }
    }
}
