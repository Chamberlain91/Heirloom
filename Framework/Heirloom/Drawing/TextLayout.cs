using System;
using System.Runtime.CompilerServices;

using Heirloom.Mathematics;
using Heirloom.Text;

namespace Heirloom.Drawing
{
    /// <summary>
    /// Delegate type for the callback when performing text layout.
    /// </summary>
    /// <param name="text">The text currently set for layout.</param>
    /// <param name="index">The index of the current character being considered for layout.</param>
    /// <param name="state">The state of the current character in layout.</param>
    /// <category>Text</category>
    public delegate void TextLayoutCallback(string text, int index, ref TextLayoutState state);

    public enum TextTruncate
    {
        /// <summary>
        /// Text exceeding layout bounds is clipped.
        /// </summary>
        Hidden,

        /// <summary>
        /// Text exceeding layout bounds is visible.
        /// </summary>
        Overflow,
    }

    /// <summary>
    /// Utility to measure text and manually invoke the text layout function. <para/> Internally used by 
    /// <see cref="GraphicsContext.DrawText(string, Rectangle, Font, int, TextAlign, DrawTextCallback)"/> and its variants.
    /// </summary>
    /// <category>Text</category>
    public static class TextLayout
    {
        #region Measure

        /// <summary>
        /// Computes the bounding box that the specified text will occupy within an infinite layout size.
        /// </summary>
        /// <param name="text">The text to layout and measure.</param>
        /// <param name="font">The font to use.</param>
        /// <param name="fontSize">The font size to use.</param>
        /*[MethodImpl(MethodImplOptions.AggressiveInlining)]*/
        public static Rectangle Measure(string text, Font font, int fontSize)
        {
            return Measure(text, Size.Infinite, font, fontSize);
        }

        /// <summary>
        /// Computes the bounding box that the specified text will occupy within the given layout size.
        /// </summary>
        /// <param name="text">The text to layout and measure.</param>
        /// <param name="layoutSize">The size of the layout box.</param>
        /// <param name="font">The font to use.</param>
        /// <param name="fontSize">The font size to use.</param>
        /// <returns></returns>
        /*[MethodImpl(MethodImplOptions.AggressiveInlining)]*/
        public static Rectangle Measure(string text, Size layoutSize, Font font, int fontSize)
        {
            return Measure(text, new Rectangle(Vector.Zero, layoutSize), font, fontSize);
        }

        /// <summary>
        /// Computes the bounding box that the specified text will occupy within the given layout size.
        /// </summary>
        /// <param name="text">The text to layout and measure.</param>
        /// <param name="layoutBox">The layout box.</param>
        /// <param name="font">The font to use.</param>
        /// <param name="fontSize">The size to measure the font with.</param>
        /// <returns></returns>
        /*[MethodImpl(MethodImplOptions.AggressiveInlining)]*/
        public static Rectangle Measure(string text, Rectangle layoutBox, Font font, int fontSize)
        {
            if (text is null) { throw new ArgumentNullException(nameof(text)); }
            if (layoutBox.Width <= 0 || layoutBox.Height <= 0) { throw new ArgumentException($"Layout box size must be greater than zero.", nameof(layoutBox)); }
            if (fontSize <= 0) { throw new ArgumentException($"Font size must be greater than zero."); }

            // Empty text, zero size.
            if (string.IsNullOrEmpty(text))
            {
                return new Rectangle(layoutBox.Position, Size.Zero);
            }
            else
            {
                // Get font table
                var glyphTable = GlyphTable.GetGlyphTable(font, fontSize);

                // Layout text, keeping track of the glyph box
                var measure = Rectangle.InvertedInfinite;
                PerformLayout(text, layoutBox, TextAlign.Default, TextTruncate.Hidden, glyphTable, (string _, int index, ref TextLayoutState state) =>
                {
                    // Include extents of glyph box
                    measure.Include(state.Position);
                    measure.Include(state.Position + (state.Metrics.AdvanceWidth, glyphTable.Metrics.LineAdvance));
                });

                return measure;
            }
        }

        #endregion

        #region Perform Layout

        /// <summary>
        /// Performs the layout of text around the given position with the specified font and size, invoking the callback at each location.
        /// </summary>
        public static Rectangle PerformLayout(string text, Vector position, Font font, int size, TextAlign align, TextLayoutCallback layoutCallback)
        {
            var bounds = GetPositionAnchoredTextBounds(text, font, size, position, align);
            return PerformLayout(text, bounds, font, size, align, layoutCallback);
        }

        /// <summary>
        /// Performs the layout of text within the given bounds with the specified font and size, invoking the callback at each location.
        /// </summary>
        public static Rectangle PerformLayout(string text, Rectangle bounds, Font font, int size, TextAlign align, TextLayoutCallback layoutCallback)
        {
            // Validate arguments
            if (text == null) { throw new ArgumentNullException(nameof(text)); }
            if (font == null) { throw new ArgumentNullException(nameof(font)); }
            if (size < 1) { throw new ArgumentException("Font size must be greater than zero.", nameof(size)); }
            if (layoutCallback == null) { throw new ArgumentNullException(nameof(layoutCallback)); }

            // Get atlas, layout text
            var glyphTable = GlyphTable.GetGlyphTable(font, size);
            return PerformLayout(text, bounds, align, TextTruncate.Hidden, glyphTable, layoutCallback);
        }

        internal static Rectangle PerformLayout(string text, Rectangle bounds, TextAlign align, TextTruncate truncate, GlyphTable glyphTable, TextLayoutCallback layoutCallback)
        {
            // Extract atlas properties for brevity
            var fontSize = glyphTable.FontSize;
            var font = glyphTable.Font;

            // Create character layout state
            var state = new TextLayoutState
            {
                Position = bounds.Position,
                Character = (UnicodeCharacter) 0
            };

            // Vertical alignment
            if (align.HasFlag(TextAlign.Middle) || align.HasFlag(TextAlign.Bottom))
            {
                // todo: Optimize? GetPositionAnchoredTextBounds and this both measure the text...
                //       There is likely a more optimized path that can only measure one or less times.
                var centerMeasure = Measure(text, bounds, glyphTable.Font, glyphTable.FontSize);
                var offsetY = bounds.Height - centerMeasure.Height;
                if (align.HasFlag(TextAlign.Middle)) { offsetY /= 2F; }
                state.Position.Y += offsetY;
            }

            // Find the first break point (if none, set to -1)
            var nextBreak = FindNextBreak(text, 0, state.Character, state.Position, bounds, glyphTable, out var lineWidth);
            var offsetX = ComputeAlignmentOffset(bounds, align, lineWidth);
            state.Position.X += offsetX; // First line alignment offset

            var measure = Rectangle.InvertedInfinite;

            // For each character
            for (var i = 0; i < text.Length; i++)
            {
                // Beyond bottom of layout box...
                if (state.Position.Y > bounds.Bottom)
                {
                    if (truncate == TextTruncate.Hidden)
                    {
                        break;
                    }
                }

                // 
                var previous = state.Character;
                state.Character = text.GetCharacter(i);

                // Apply kerning with previous character
                state.Position.X += font.GetKerning(previous, state.Character, fontSize);

                // Get the relevant glyph, if exists (should always exist?)
                var glyph = glyphTable.Font.GetGlyph(state.Character);

                // Get metrics for glyph as the desired font size
                state.Metrics = glyph.GetMetrics(fontSize);

                // Process character, if kept, advance pen position
                layoutCallback(text, i, ref state);

                // Include extents of glyph box
                measure.Include(state.Position);
                measure.Include(state.Position + (state.Metrics.AdvanceWidth, glyphTable.Metrics.LineAdvance));

                // Apply horizontal advance
                state.Position.X += state.Metrics.AdvanceWidth;

                // We should break (newline, edge of bounds, etc)
                if (nextBreak == i)
                {
                    // Line feed
                    state.Position.Y += glyphTable.Metrics.LineAdvance;
                    state.Position.X = bounds.Left;

                    // Find the next break point
                    nextBreak = FindNextBreak(text, i + 1, state.Character, state.Position, bounds, glyphTable, out lineWidth);
                    offsetX = ComputeAlignmentOffset(bounds, align, lineWidth);
                    state.Position.X += offsetX;
                }
            }

            return measure;
        }

        internal static Rectangle GetPositionAnchoredTextBounds(string text, Font font, int size, Vector position, TextAlign align)
        {
            var measure = Measure(text, font, size).Size;

            var pos = position;

            // 
            if (align.HasFlag(TextAlign.Center)) { pos.X -= measure.Width / 2; }
            if (align.HasFlag(TextAlign.Right)) { pos.X -= measure.Width; }

            // 
            if (align.HasFlag(TextAlign.Middle)) { pos.Y -= measure.Height / 2; }
            if (align.HasFlag(TextAlign.Bottom)) { pos.Y -= measure.Height; }

            return new Rectangle(pos, measure);
        }

        private static float ComputeAlignmentOffset(Rectangle bounds, TextAlign align, float lineWidth)
        {
            var offset = 0F;
            if (align.HasFlag(TextAlign.Center) || align.HasFlag(TextAlign.Right)) { offset = bounds.Width - lineWidth; }
            if (align.HasFlag(TextAlign.Center)) { offset /= 2F; }
            return offset;
        }

        // checks if character should break (newline or word too long, etc)
        private static int FindNextBreak(string text, int index, UnicodeCharacter previous, Vector position, Rectangle bounds, GlyphTable glyphTable, out float width)
        {
            var opportunity = -1;
            var opportunityEdge = 0F;

            var edge = position.X;
            var prevEdge = edge;

            // For each character in the future, do we see a possible break?
            for (var i = index; i < text.Length; i++)
            {
                var character = text.GetCharacter(i);
                var breakCategory = GetBreakCategory(character);

                // Add kerning
                var kerning = glyphTable.Font.GetKerning(previous, character, glyphTable.FontSize);
                edge += kerning;

                // Character is definintely a break (newline, etc)
                if (breakCategory == TextBreakCategory.Mandatory)
                {
                    width = prevEdge - position.X;
                    return i;
                }

                // Character could be a break if the next word violates the bounds (space, dash, etc)
                if (breakCategory == TextBreakCategory.Opportunity || breakCategory == TextBreakCategory.OpportunityKeep)
                {
                    // Mark the opportunity index
                    opportunityEdge = prevEdge;
                    opportunity = i;

                    // 
                    if (breakCategory == TextBreakCategory.OpportunityKeep)
                    {
                        opportunity--;
                    }
                }

                // Advance right edge
                var metrics = glyphTable.GetGlyphMetrics(character);
                edge += metrics.AdvanceWidth;

                // We found a break opportunity, we need to now check if we violate the bounds
                if (opportunity >= 0)
                {
                    // Violated bounds (within a tolerance approximated by character width)
                    // todo: Why is this tolerance here?
                    if (edge > (bounds.Right + (metrics.AdvanceWidth / 10F)))
                    {
                        width = opportunityEdge - position.X;
                        return opportunity;
                    }
                }

                // 
                previous = character;
                prevEdge = edge;
            }

            // No allowable break
            width = prevEdge - position.X;
            return -1;
        }

        // classifies a character into its break category
        private static TextBreakCategory GetBreakCategory(UnicodeCharacter character)
        {
            var c = (char) character;

            // Break on whitespaces
            if (char.IsWhiteSpace(c))
            {
                if (c == '\n') { return TextBreakCategory.Mandatory; }
                else { return TextBreakCategory.Opportunity; }
            }

            // Opportunity to break on dashes
            if (c == '-') { return TextBreakCategory.OpportunityKeep; }

            // Shouldn't break
            return TextBreakCategory.None;
        }

        #endregion
    }
}
