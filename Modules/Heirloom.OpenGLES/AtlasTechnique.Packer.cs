using System.Collections.Generic;
using System.IO;

using Heirloom.IO;

namespace Heirloom.OpenGLES
{
    internal class PackerAtlasTechnique : AtlasTechnique
    {
        private readonly Dictionary<Image, AtlasEntry> _entries;
        private readonly RectanglePackerImpl<Image> _packer;
        private readonly Texture _texture;

        private readonly HashSet<Image> _changeSet = new HashSet<Image>();

        public PackerAtlasTechnique(OpenGLGraphics graphics)
            : base(graphics)
        {
            _entries = new Dictionary<Image, AtlasEntry>();

            // Create packer - it will be at most 256 megabytes (8192^2 * 4 bytes)
            var pageSize = Calc.Min(8192, graphics.Capabilities.MaxTextureSize);
            _packer = new SkylinePacker<Image>(pageSize, pageSize);

            // Allocate texture (page)
            _texture = Graphics.Invoke(() => new Texture(_packer.Size, TextureSizedFormat.RGBA8));
        }

        internal override void Evict()
        {
            Log.Warning($"Evicting {_entries.Count} images");

            // DumpAtlasToDisk();

            _changeSet.Clear();
            _entries.Clear();
            _packer.Clear();
        }

        public void DumpAtlasToDisk()
        {
            // Copy entire atlas to disk
            var image = new Image(_texture.Width, _texture.Height);
            foreach (var (im, en) in _entries)
            {
                // Copy each image to disk...
                foreach (var co in Rasterizer.Rectangle(en.Rect))
                {
                    var co2 = co - en.Rect.Position;
                    image.SetPixel(co, im.GetPixel(co2));
                }
            }

            // Shuttle image to disk
            using var fs = new FileStream("atlas.png", FileMode.Create);
            image.WritePNG(fs);
        }

        internal override bool Submit(Image image, out Texture texture, out Rectangle uvRect)
        {
            // If the image entry is already known...
            if (_entries.TryGetValue(image, out var entry))
            {
                // Set output parameters
                texture = entry.Texture;
                uvRect = entry.UVRect;
            }
            // Was not known, so attempt to insert it
            else if (SubmitImage(image, out entry))
            {
                texture = entry.Texture;
                uvRect = entry.UVRect;
            }
            else
            {
                // Unable to insert image, return failure
                texture = default;
                uvRect = default;
                return false;
            }

            // Image and texture are out of sync...
            if (entry.Version != image.Version)
            {
                // Append image to change set
                _changeSet.Add(image);

                // Mark entry as up to date. We will actually commit the changes
                // shortly before draw batch.
                entry.Version = image.Version;
            }

            return true;
        }

        private bool SubmitImage(Image image, out AtlasEntry entry)
        {
            if (_packer.TryAdd(image, image.Size))
            {
                // Successfully added image to packing
                var rect = _packer.GetRectangle(image);

                // Construct atlas entry
                entry = new AtlasEntry
                {
                    Texture = _texture,
                    UVRect = new Rectangle
                    {
                        X = rect.X / (float) _texture.Width,
                        Y = rect.Y / (float) _texture.Height,
                        Width = rect.Width / (float) _texture.Width,
                        Height = rect.Height / (float) _texture.Height
                    },
                    Rect = rect
                };

                // Record image entry
                _entries.Add(image, entry);
                return true;
            }
            else
            {
                // Unable to insert into atlas
                entry = null;
                return false;
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

            //    public void Update(Image image)
            //    {
            //        // Image and texture are out of sync...
            //        if (Version != image.Version)
            //        {
            //            // Append image to change set
            //            _changeSet.Add(image);

            //            // Mark entry as up to date. We will actually commit the changes
            //            // shortly before draw batch.
            //            Version = image.Version;
            //        }
            //    }
        }
    }
}
