using System.Collections.Generic;
using System.Linq;

using Heirloom.Math;

namespace Heirloom.Drawing
{
    internal sealed class FontAtlas
    {
        public Font Font { get; }

        public FontMetrics Metrics { get; }

        public float FontSize { get; }

        public Image Atlas { get; }

        private readonly Dictionary<UnicodeCharacter, Font.Glyph> _glyphs;
        private readonly Dictionary<UnicodeCharacter, Image> _images;

        // TODO: Keep a glyph life time counter, to know when its safe to recycle space.
        // TODO: Keep track of recycled space, if too fragmented, reconstruct atlas.

        public FontAtlas(Font font, int size)
            : this(font, size, new[] {
                UnicodeRange.BasicLatin,
                UnicodeRange.Latin1Supplement,
                UnicodeRange.LatinExtendedA,
                UnicodeRange.LatinExtendedB,
                UnicodeRange.Hiragana,
                UnicodeRange.Katakana,
                UnicodeRange.Cyrillic,
                UnicodeRange.CyrillicSupplement
            })
        { }

        public FontAtlas(Font font, int size, IEnumerable<UnicodeRange> characterRanges)
            : this(font, size, characterRanges.SelectMany(x => x))
        { }

        public FontAtlas(Font font, int size, IEnumerable<UnicodeCharacter> characters)
        {
            FontSize = size;
            Font = font;

            // Get Face Metrics
            Metrics = Font.GetMetrics(FontSize);

            // Get all valid glyphs in character set
            _glyphs = GetValidGlyphs(font, size, characters);

            // Pack each glyph
            var packer = new RectanglePacker<UnicodeCharacter>();
            foreach (var kv in _glyphs) // TODO: Sort by size?
            {
                packer.Insert(kv.Key, kv.Value.GetMetrics(FontSize).Box.Size + (2, 2));
            }

            // Create image with the bounding of all packed glyphs
            Atlas = new Image(packer.Bounds.Size);

            // For each glyph, render into atlas
            _images = new Dictionary<UnicodeCharacter, Image>();
            foreach (var c in packer.Keys)
            {
                // Render each glyph in the packed region for this character
                _images[c] = RenderGlyph(Atlas, packer.GetRectangle(c), _glyphs[c], FontSize);
            }
        }

        private static Image RenderGlyph(Image atlas, IntRectangle region, Font.Glyph glyph, float size)
        {
            // Create a sub-image for the packed region and render to it
            var cell = new Image(atlas, new IntRectangle(region.Position + (1, 1), region.Size - (2, 2)));
            cell.Origin = cell.Bounds.Center;
            glyph.RenderTo(cell, 0, 0, size);
            return cell;
        }

        private static Dictionary<UnicodeCharacter, Font.Glyph> GetValidGlyphs(Font font, float size, IEnumerable<UnicodeCharacter> characters)
        {
            var glyphs = new Dictionary<UnicodeCharacter, Font.Glyph>();

            // Find all valid glyphs
            foreach (var ch in characters)
            {
                // We already visited this character for some reason,
                // we can skip doing any work.
                if (glyphs.ContainsKey(ch))
                {
                    continue;
                }

                // Get the glyph information for the given size
                var glyph = font.GetGlyph(ch);

                // Unable to find glyph in this font face
                // TODO: Find a better 'missing' / 'not printable' function...?
                if (glyph == null || glyph.GetMetrics(size).Box.Width <= 0 || !glyph.IsVisibleCharacter)
                {
                    continue;
                }

                // Add glyph to character set
                glyphs.Add(ch, glyph);
            }

            // Extract array of glyphs
            return glyphs;
        }

        // TODO: Might not be useful, have to evaluate how stb works 
        public bool TryGetGlyph(UnicodeCharacter character, out Font.Glyph glyph)
        {
            glyph = GetGlyph(character);
            return glyph != null;
        }

        // TODO: Might be redundant? Just a cache on the font?
        public Font.Glyph GetGlyph(UnicodeCharacter ch)
        {
            // Do we already know about this glyph?
            if (_glyphs.ContainsKey(ch)) { return _glyphs[ch]; }
            else
            {
                // Get the glyph and store
                var glyph = Font.GetGlyph(ch);
                _glyphs[ch] = glyph;
                return glyph;
            }
        }

        public Image GetImage(UnicodeCharacter ch)
        {
            // TODO: On Demand Glyph Rendering with Dynamic Atlas

            if (_images.TryGetValue(ch, out var image))
            {
                return image;
            }

            return null;
        }
    }
}
