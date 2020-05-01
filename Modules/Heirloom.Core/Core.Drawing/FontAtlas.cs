using System.Collections.Generic;
using System.Linq;

using Heirloom;

namespace Heirloom
{
    internal sealed class FontAtlas
    {
        public Font Font { get; }

        public FontMetrics Metrics { get; }

        public float FontSize { get; }

        private readonly Dictionary<UnicodeCharacter, Image> _images = new Dictionary<UnicodeCharacter, Image>();
        private readonly Dictionary<UnicodeCharacter, Glyph> _glyphs;

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
        }

        private static Dictionary<UnicodeCharacter, Glyph> GetValidGlyphs(Font font, float size, IEnumerable<UnicodeCharacter> characters)
        {
            var glyphs = new Dictionary<UnicodeCharacter, Glyph>();

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
                if (glyph == null || glyph.GetMetrics(size).Size.Width <= 0 || !glyph.CanBeRendered)
                {
                    continue;
                }

                // Add glyph to character set
                glyphs.Add(ch, glyph);
            }

            // Extract array of glyphs
            return glyphs;
        }

        // TODO: Perhaps the font itself can cache this...!
        public bool TryGetGlyph(UnicodeCharacter character, out Glyph glyph)
        {
            // Do we already know about this glyph?
            if (_glyphs.TryGetValue(character, out glyph) == false)
            {
                // Get the glyph and store
                glyph = Font.GetGlyph(character);
                _glyphs[character] = glyph;
            }

            // If the cached glyph was
            return glyph != null;
        }

        public Image GetImage(UnicodeCharacter ch)
        {
            // If we already have the glyph, return it
            if (_images.TryGetValue(ch, out var image))
            {
                return image;
            }

            // We did not have the glyph, so we will now render it
            if (TryGetGlyph(ch, out var glyph) && glyph.CanBeRendered)
            {
                lock (_images)
                {
                    image = RenderGlyph(glyph, FontSize);
                    _images[ch] = image;
                    return image;
                }
            }

            // Glyph was not know and could not be rendered
            return null;
        }

        private static Image RenderGlyph(Glyph glyph, float size)
        {
            // Creates an image to store the rendered glyph
            var imageSize = glyph.GetMetrics(size).Size;
            var image = new Image(imageSize) { Origin = (IntVector) imageSize / 2 };

            // Render glyph and return
            glyph.RenderTo(image, 0, 0, size);
            return image;
        }
    }
}
