using System.Collections.Generic;

using Meadows.Mathematics;

namespace Meadows.Drawing.OpenGLES
{
    internal sealed class ESAtlasPacker : ESAtlas
    {
        //private readonly Dictionary<Image, AtlasEntry> _entries;
        private readonly RectanglePackerImpl<Image> _packer;
        private readonly ESTexture _texture;

        private readonly HashSet<Image> _changeSet = new();

        private int _version;

        public ESAtlasPacker(ESGraphicsContext context)
            : base(context)
        {
            //_entries = new Dictionary<Image, AtlasEntry>();

            // Create packer - it will be at most 256 megabytes (8192^2 * 4 bytes)
            var pageSize = Calc.Min(8192, context.Capabilities.MaxTextureSize);
            _packer = new SkylinePacker<Image>(pageSize, pageSize);

            // Allocate texture (page)
            _texture = Context.Invoke(() => new ESTexture(_packer.Size, TextureSizedFormat.RGBA8));
        }

        public override bool Submit(Image image, out ESTexture atlasTexture, out Rectangle atlasRect)
        {

            // If the image entry is already known...
            var entry = image.TEMP_TEST as AtlasEntry;
            // if (_entries.TryGetValue(image, out var entry))
            if (entry != null)
            {

                // Set output parameters
                atlasTexture = entry.Texture;
                atlasRect = entry.UVRect;
            }
            // Was not known, so attempt to insert it
            else if (SubmitImage(image, out entry))
            {
                atlasTexture = entry.Texture;
                atlasRect = entry.UVRect;
            }
            else
            {
                // Unable to insert image, return failure
                atlasTexture = default;
                atlasRect = default;
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
                // _entries.Add(image, entry);
                image.TEMP_TEST = entry;
                return true;
            }
            else
            {
                // Unable to insert into atlas
                entry = null;
                return false;
            }
        }

        public override void Commit()
        {
            if (_changeSet.Count > 0)
            {
                GLES.BindTexture(TextureTarget.Texture2D, _texture.Handle);

                foreach (var image in _changeSet)
                {
                    // var entry = _entries[image];
                    var entry = image.TEMP_TEST as AtlasEntry;
                    _texture.Update(entry.Rect.X, entry.Rect.Y, image);
                }

                //
                _changeSet.Clear();
                _version++;
            }
        }

        public override void Evict()
        {
            //Log.Warning($"Evicting {_entries.Count} images");

            _changeSet.Clear();
            //_entries.Clear();
            _packer.Clear();
        }

        private class AtlasEntry
        {
            public ESTexture Texture;
            public IntRectangle Rect;
            public Rectangle UVRect;
            public uint Version;
        }
    }
}
