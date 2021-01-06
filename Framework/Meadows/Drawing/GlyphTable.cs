using System.Collections.Generic;

using Meadows.Text;

namespace Meadows.Drawing
{
    internal sealed class GlyphTable
    {
        private readonly Dictionary<UnicodeCharacter, Image> _images;

        internal readonly FontMetrics Metrics;
        internal readonly Font Font;
        internal readonly int FontSize;

        private static readonly Dictionary<(Font, int), GlyphTable> _glyphTables;

        static GlyphTable()
        {
            _glyphTables = new Dictionary<(Font, int), GlyphTable>();
        }

        private GlyphTable(Font font, int size)
        {
            _images = new Dictionary<UnicodeCharacter, Image>();

            // 
            Font = font;
            FontSize = size;

            // Get Face Metrics
            Metrics = Font.GetMetrics(FontSize);
        }

        internal GlyphMetrics GetGlyphMetrics(UnicodeCharacter ch)
        {
            var glyph = Font.GetGlyph(ch);
            return glyph.GetMetrics(FontSize);
        }

        internal bool TryGetImage(UnicodeCharacter ch, out Image image)
        {
            // Get glyph
            var glyph = Font.GetGlyph(ch);

            // Can this glyph be rendered?
            if (glyph.CanBeRendered)
            {
                // Have we rendered this glyph yet?
                if (!_images.TryGetValue(ch, out image))
                {
                    // Render glyph and place into table
                    image = glyph.RenderGlyph(FontSize);
                    _images[ch] = image;
                }

                // Return image for glyph at requested size.
                return true;
            }
            else
            {
                image = null;
                return false;
            }
        }

        public static GlyphTable GetGlyphTable(Font font, int size)
        {
            var key = (font, size);

            if (!_glyphTables.TryGetValue(key, out var table))
            {
                table = new GlyphTable(font, size);
                _glyphTables[key] = table;
            }

            return table;
        }
    }
}
