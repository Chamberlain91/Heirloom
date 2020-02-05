using System.Collections.Generic;

using Heirloom.Drawing;
using Heirloom.Math;

namespace StreamingAtlas
{
    public class Atlas
    {
        private readonly Dictionary<Image, Entry> _entries;
        private readonly IRectanglePacker<Image> _packer;
        private readonly HashSet<Entry> _dirties;

        private readonly uint _tick;

        public Surface Surface { get; }

        public Atlas(int width, int height)
        {
            Surface = new Surface(width, height);
            _packer = new ShelfPacker<Image>(width, height);
            _entries = new Dictionary<Image, Entry>();
            _dirties = new HashSet<Entry>();
        }

        /// <summary>
        /// Mark the image as needed in the atlas.
        /// </summary>
        public bool Register(Image image)
        {
            if (_entries.TryGetValue(image, out var entry))
            {
                // Update age as it has been used recently.
                entry.Age = _tick;

                // Version numbers have changed, need to update atlas.
                if (entry.IsDirty) { _dirties.Add(entry); }

                // Image was already in the atlas.
                return true;
            }
            // Try to insert into packer
            else if (_packer.Add(image, image.Size))
            {
                // Construct new entry for image
                var atlasRect = _packer.GetRectangle(image);
                entry = _entries[image] = new Entry(image, atlasRect);

                // Entry is new, put on dirty list.
                _dirties.Add(entry);

                // Image was successfuly inserted into atlas.
                return true;
            }
            else
            {
                // Was not contained nor were we able to insert it.
                return false;
            }
        }

        public void CommitChanges(Graphics gfx)
        {
            gfx.PushState();
            {
                gfx.ResetState();
                gfx.Surface = Surface;

                // Draw dirty images
                foreach (var entry in _dirties)
                {
                    // Update vertion number to match
                    entry.Version = entry.Image.Version;

                    // Draw image to surface
                    var rectangle = _packer.GetRectangle(entry.Image);
                    gfx.DrawImage(entry.Image, rectangle);
                }

                // Clear dirty list
                _dirties.Clear();
            }
            gfx.PopState();
        }

        private class Entry
        {
            public readonly IntRectangle Rectangle;
            public readonly Image Image;

            public bool IsDirty => Version != Image.Version;

            public uint Version;

            public uint Age;

            public Entry(Image image, IntRectangle rectangle)
            {
                Rectangle = rectangle;
                Image = image;
                Age = 0;
            }
        }
    }
}
