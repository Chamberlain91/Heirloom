using System;

using static StbTrueTypeSharp.StbTrueType;

namespace Heirloom
{
    /// <summary>
    /// A glyph represents the metrics and rendering of a character from the associated <see cref="Font"/>.
    /// </summary>
    public class Glyph
    {
        private readonly int _advanceWidth;
        private readonly int _bearing;

        internal readonly int Index;
        internal IntRectangle Box;

        #region Constructors

        internal unsafe Glyph(Font font, int index)
        {
            // Get glyph index (search)
            Index = index;
            Font = font;

            // Get horizontal metrics (at raw scale)
            int advWidth, bearing;
            stbtt_GetGlyphHMetrics(Font.Info, Index, &advWidth, &bearing);
            _advanceWidth = advWidth;
            _bearing = bearing;

            // Get glyph box info
            int x0, x1, y0, y1;
            stbtt_GetGlyphBitmapBox(Font.Info, Index, 1F, 1F, &x0, &y0, &x1, &y1);
            Box = new IntRectangle(x0, y0, x1 - x0, y1 - y0);
        }

        #endregion

        #region Properties

        internal bool HasCodepoint { get; private set; }

        /// <summary>
        /// Gets the associated font.
        /// </summary>
        public Font Font { get; }

        /// <summary>
        /// Gets the character this glyph represents.
        /// </summary>
        public UnicodeCharacter Character { get; private set; }

        /// <summary>
        /// Get a value that determines if this glyph can be rendered.
        /// </summary>
        public bool CanBeRendered
        {
            get
            {
                if (Index == 0) { return false; }
                return stbtt_IsGlyphEmpty(Font.Info, Index) == 0;
            }
        }

        #endregion

        /// <summary>
        /// Get the horizontal metrics of the this glyph at the specified size.
        /// </summary>
        /// <param name="size">The size of the font.</param>
        public GlyphMetrics GetMetrics(float size)
        {
            // Compute scaling factor
            var scale = Font.ComputeScale(size);

            // Compute metrics
            var advanceWidth = _advanceWidth * scale;
            var bearing = _bearing * scale;

            // Compute scaled pixel box
            var x0 = Calc.Floor(Box.X * scale);
            var y0 = Calc.Floor(Box.Y * scale);
            var x1 = Calc.Ceil((Box.X + Box.Width) * scale);
            var y1 = Calc.Ceil((Box.Y + Box.Height) * scale);

            // Return metrics to user
            return new GlyphMetrics(advanceWidth, bearing, new IntRectangle(x0, y0, x1 - x0, y1 - y0));
        }

        internal void SetCodepoint(UnicodeCharacter codepoint)
        {
            if (HasCodepoint) { throw new InvalidOperationException("Unable to set glyph's codpoint, already set"); }
            else
            {
                Character = codepoint;
                HasCodepoint = true;
            }
        }

        /// <summary>
        /// Renders the glyph into an image.
        /// </summary>
        /// <param name="size">The font size of the rendered glyph.</param>
        public Image RenderGlyph(float size)
        {
            // Creates an image to store the rendered glyph
            var imageSize = GetMetrics(size).Size;
            var image = new Image(imageSize) { Origin = (IntVector) imageSize / 2 };

            // Render glyph and return
            RenderTo(image, 0, 0, size);
            return image;
        }

        /// <summary>
        /// Renders the glyph into an image.
        /// </summary>
        /// <param name="image">Some image to render the glyph into.</param>
        /// <param name="x">Offset on the x-axis in pixels.</param>
        /// <param name="y">Offset on the y-axis in pixels.</param>
        /// <param name="size">The font size of the rendered glyph.</param>
        public void RenderTo(Image image, int x, int y, float size)
        {
            if (image is null) { throw new ArgumentNullException(nameof(image)); }

            // Compute scaling factor
            var scale = Font.ComputeScale(size);

            // Render into image
            Font.RenderTo(Index, scale, image, x, y);
        }

        public override string ToString()
        {
            var c = char.ConvertFromUtf32((int) Character);
            return $"{c}";
        }
    }
}
