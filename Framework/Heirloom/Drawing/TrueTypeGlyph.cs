using System;

using Heirloom.Mathematics;
using Heirloom.Text;

using static StbTrueTypeSharp.StbTrueType;

namespace Heirloom.Drawing
{
    internal sealed class TrueTypeGlyph : Glyph
    {
        private readonly int _advanceWidth;
        private UnicodeCharacter _character;
        private readonly TrueTypeFont _font;

        internal readonly int Index;

        #region Constructors

        internal unsafe TrueTypeGlyph(TrueTypeFont font, int index)
        {
            // Get glyph index (search)
            Index = index;

            _font = font;
            Font = font;

            // Get horizontal metrics (at raw scale)
            int advWidth, bearing;
            stbtt_GetGlyphHMetrics(_font.Info, Index, &advWidth, &bearing);
            _advanceWidth = advWidth;
        }

        #endregion

        #region Properties

        internal bool HasCodepoint { get; private set; }

        public override Font Font { get; }

        public override UnicodeCharacter Character => _character;

        public override bool CanBeRendered
        {
            get
            {
                if (Index == 0) { return false; }
                return stbtt_IsGlyphEmpty(_font.Info, Index) == 0;
            }
        }

        #endregion

        public override unsafe GlyphMetrics GetMetrics(float fontSize)
        {
            // Compute scaling factor
            var scale = _font.ComputeScale(fontSize);

            // Compute advance width (left edge advancement)
            var advanceWidth = _advanceWidth * scale;

            // Get glyph box info
            int x0, x1, y0, y1;
            stbtt_GetGlyphBitmapBox(_font.Info, Index, scale, scale, &x0, &y0, &x1, &y1);

            // Glyph bounds
            var offset = new IntVector(x0, y0);
            var size = new IntSize(x1 - x0, y1 - y0);

            // Return metrics to user
            return new GlyphMetrics(advanceWidth, offset, size);
        }

        internal void SetCodepoint(UnicodeCharacter codepoint)
        {
            if (HasCodepoint) { throw new InvalidOperationException("Unable to set glyph's codpoint, already set"); }
            else
            {
                _character = codepoint;
                HasCodepoint = true;
            }
        }

        public override Image CreateImage(float size)
        {
            // Compute scaling factor
            var scale = _font.ComputeScale(size);

            // Creates an image to store the rendered glyph
            var image = new Image(GetMetrics(size).Size);

            // Render into image
            _font.RenderTo(Index, scale, image, 0, 0);

            return image;
        }

        /// <summary>
        /// Converts this glyph into a string representation.
        /// </summary>
        public override string ToString()
        {
            var str = char.ConvertFromUtf32((int) Character);
            return $"Glyph '{str}'";
        }
    }
}
