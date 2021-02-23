using System;
using System.Collections.Generic;
using System.Runtime.CompilerServices;

using Heirloom.Mathematics;

namespace Heirloom.Drawing.OpenGLES
{
    internal sealed class ESAtlasPacker : ESAtlas
    {
        private readonly ConditionalWeakTable<Image, AtlasEntry> _entries;
        private readonly AtlasPage _page;

        public ESAtlasPacker(ESGraphicsContext context)
            : base(context)
        {
            _entries = new ConditionalWeakTable<Image, AtlasEntry>();

            // Create packer - it will be at most 64 megabytes (4096^2 * 4 bytes)
            var pageSize = Calc.Min(4096, GraphicsBackend.Current.Capabilities.MaxTextureSize);

            // Allocate texture
            var textureSize = new IntSize(pageSize, pageSize);
            var texture = Context.Invoke(() => new ESTexture(textureSize, TextureSizedFormat.RGBA8));

            // Allocate new atlas page
            _page = new AtlasPage(texture);
        }

        private AtlasEntry GetAtlasEntry(Image image)
        {
            if (!_entries.TryGetValue(image, out var entry))
            {
                // Entry was not contained, we will allocate one
                entry = new AtlasEntry(image);
                _entries.Add(image, entry);
            }

            return entry;
        }

        public override bool Submit(Image image, out ESTexture atlasTexture, out Rectangle atlasRect)
        {
            // Request atlas entry (ie, indirect identifier)
            var entry = GetAtlasEntry(image);
            if (entry.AtlasPage != null)
            {
                // Entry was already known, simply emit. 
                atlasTexture = entry.AtlasPage.Texture;
                atlasRect = entry.UVRect;
            }
            else if (SubmitImage(entry))
            {
                // Image is not contained in an atlas page
                atlasTexture = entry.AtlasPage.Texture;
                atlasRect = entry.UVRect;
            }
            else
            {
                // Unable to insert image, return failure
                // todo: allocate another atlas page?
                atlasTexture = default;
                atlasRect = default;
                return false;
            }

            // Ensure entry is up to date
            entry.UpdateVersion(image);

            // Image is sucessfully contained
            return true;
        }

        private bool SubmitImage(AtlasEntry entry)
        {
            // todo: multiple pages
            return _page.Insert(entry);
        }

        /// <summary>
        /// Commit changes to atlas textures. Note: this method must be called on GL thread.
        /// </summary>
        public override void Commit()
        {
            _page.Commit();
        }

        public override void Evict()
        {
            _page.Evict();
        }

        private class AtlasPage
        {
            internal readonly RectanglePackerImpl<AtlasEntry> Packer;
            internal readonly ESTexture Texture;

            private readonly HashSet<AtlasEntry> _changes = new HashSet<AtlasEntry>();

            public AtlasPage(ESTexture texture)
            {
                Packer = new SkylinePacker2<AtlasEntry>(texture.Size);
                Texture = texture;
            }

            public void MarkDirty(AtlasEntry entry)
            {
                _changes.Add(entry);
            }

            public bool Insert(AtlasEntry entry)
            {
                if (Packer.Contains(entry))
                {
                    return true;
                }
                else
                {
                    // Attempt to pack image
                    if (Packer.TryAdd(entry, entry.ImageSize))
                    {
                        // Associate atlas entry with this page
                        entry.Associate(this);

                        // Entry was successfully inserted into this atlas
                        return true;
                    }

                    // Image failed to pack
                    return false;
                }
            }

            /// <summary>
            /// Commit changes to texture. Note: this method must be called on GL thread.
            /// </summary>
            public void Commit()
            {
                if (_changes.Count > 0)
                {
                    Texture.Bind();

                    // Process changes
                    foreach (var entry in _changes)
                    {
                        // If image reference is still valid, write it to the texture
                        if (entry.ImageReference.TryGetTarget(out var image))
                        {
                            Texture.Update(entry.PixelRect.X, entry.PixelRect.Y, image);
                        }
                    }

                    // Clear changes
                    _changes.Clear();
                }
            }

            /// <summary>
            /// Determines the ratio of alive vs dead images in terms of surface area.
            /// </summary>
            public float GetAliveRatio()
            {
                var occupiedArea = 0F;
                var aliveArea = 0F;

                foreach (var entry in Packer.Elements)
                {
                    // Accumulate occupied area
                    occupiedArea += entry.ImageSize.Area;

                    // If entry image is valid, we can
                    if (entry.ImageReference.TryGetTarget(out _))
                    {
                        // Image is alive, it contributes to surface area
                        aliveArea += entry.ImageSize.Area;
                    }
                }

                return aliveArea / occupiedArea;
            }

            public void Compact()
            {
                var aliveRatio = GetAliveRatio();
                if (aliveRatio < 0.5F) // todo: what is best threshold?
                {
                    Log.Warning($"TODO: Implement Atlas Compacting");
                }
            }

            /// <summary>
            /// Clear the page. It no longer contains any packed image.
            /// </summary>
            public void Evict()
            {
                Log.Warning($"[Atlas] Evicting {Packer.Elements.Count} images");

                // Remove atlas information for each entry
                foreach (var entry in Packer.Elements)
                {
                    entry.Associate(null);
                }

                // Clear packer and changes
                Packer.Clear();
                _changes.Clear();
            }
        }

        private class AtlasEntry : IEquatable<AtlasEntry>
        {
            private static uint _counter;

            internal readonly uint Id = _counter++;

            internal readonly WeakReference<Image> ImageReference;
            internal IntSize ImageSize;

            internal AtlasPage AtlasPage;
            internal IntRectangle PixelRect;
            internal Rectangle UVRect;
            internal uint Version;

            internal AtlasEntry(Image image)
            {
                ImageReference = new WeakReference<Image>(image);
                ImageSize = image.Size;
            }

            internal void Associate(AtlasPage page)
            {
                if (page == null)
                {
                    // Clear information about atlas residence
                    AtlasPage = null;
                    PixelRect = default;
                    UVRect = default;
                    Version = 0;
                }
                else
                {
                    var rect = page.Packer.GetRectangle(this);

                    // Store atlas page reference
                    AtlasPage = page;

                    // Store packed rects
                    PixelRect = rect;
                    UVRect = new Rectangle
                    {
                        X = rect.X / (float) page.Texture.Width,
                        Y = rect.Y / (float) page.Texture.Height,
                        Width = rect.Width / (float) page.Texture.Width,
                        Height = rect.Height / (float) page.Texture.Height
                    };
                }
            }

            internal void UpdateVersion(Image image)
            {
                // Image and texture are out of sync...
                if (Version != image.Version)
                {
                    // Schedule entry to update the texture
                    AtlasPage.MarkDirty(this);

                    // Mark entry as up to date.
                    Version = image.Version;
                }
            }

            #region Equality

            public override bool Equals(object obj)
            {
                return obj is AtlasEntry entry
                    && Equals(entry);
            }

            public bool Equals(AtlasEntry other)
            {
                return other.Id == Id;
            }

            public override int GetHashCode()
            {
                return HashCode.Combine(Id);
            }

            public static bool operator ==(AtlasEntry left, AtlasEntry right)
            {
                return EqualityComparer<AtlasEntry>.Default.Equals(left, right);
            }

            public static bool operator !=(AtlasEntry left, AtlasEntry right)
            {
                return !(left == right);
            }

            #endregion
        }
    }
}
