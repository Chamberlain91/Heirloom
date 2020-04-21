using System;
using System.Runtime.CompilerServices;

using Heirloom;

namespace Heirloom
{
    public delegate void TextLayoutCallback(string text, int index, ref TextLayoutState state);

    /// <summary>
    /// Utility to measure text and manually invoke the text layout function. <para/> Internally used by 
    /// <see cref="Graphics.DrawText(string, in Rectangle, Font, int, TextAlign, DrawTextCallback)"/> and its variants.
    /// </summary>
    public static class TextLayout
    {
        #region Measure

        /// <summary>
        /// Computes the bounding box that the specified text will occupy within an infinite layout size.
        /// </summary>
        /// <param name="text">The text to layout and measure.</param>
        /// <param name="fontSize">The font size to use.</param>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Rectangle Measure(string text, Font font, int fontSize)
        {
            return Measure(text, Size.Infinite, font, fontSize);
        }

        /// <summary>
        /// Computes the bounding box that the specified text will occupy within the given layout size.
        /// </summary>
        /// <param name="text">The text to layout and measure.</param>
        /// <param name="layoutSize">The size of the layout box.</param>
        /// <param name="fontSize">The font size to use.</param>
        /// <returns></returns>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Rectangle Measure(string text, in Size layoutSize, Font font, int fontSize)
        {
            return Measure(text, new Rectangle(Vector.Zero, layoutSize), font, fontSize);
        }

        /// <summary>
        /// Computes the bounding box that the specified text will occupy within the given layout size.
        /// </summary>
        /// <param name="text">The text to layout and measure.</param>
        /// <param name="layoutSize">The size of the layout box.</param>
        /// <param name="fontSize">The font size to use.</param>
        /// <returns></returns>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Rectangle Measure(string text, in Rectangle layoutBox, Font font, int fontSize)
        {
            if (text is null) { throw new ArgumentNullException(nameof(text)); }
            if (layoutBox.Width <= 0 || layoutBox.Height <= 0) { throw new ArgumentException($"Layout box size must be greater than zero.", nameof(layoutBox)); }
            if (fontSize <= 0) { throw new ArgumentException($"Font size must be greater than zero."); }

            // Get font atlas
            var atlas = FontManager.GetAtlas(font, fontSize);

            // Layout text, keeping track of the glyph box
            var measure = Rectangle.Zero;
            PerformLayout(text, layoutBox, TextAlign.Left, atlas, (string _, int index, ref TextLayoutState state) =>
            {
                // Include extents of glyph box
                measure.Include(state.Position);
                measure.Include(state.Position + (state.Metrics.AdvanceWidth, atlas.Metrics.LineAdvance));
            });

            return measure;
        }

        #endregion

        #region Perform Layout

        /// <summary>
        /// Performs the layout of text around the given position with the specified font and size, invoking the callback at each location.
        /// </summary>
        public static void PerformLayout(string text, Vector position, Font font, int size, TextAlign align, TextLayoutCallback layoutCallback)
        {
            var bounds = GetPositionAnchoredTextBounds(text, font, size, position, align);
            PerformLayout(text, bounds, font, size, align, layoutCallback);
        }

        /// <summary>
        /// Performs the layout of text within the given bounds with the specified font and size, invoking the callback at each location.
        /// </summary>
        public static void PerformLayout(string text, Rectangle bounds, Font font, int size, TextAlign align, TextLayoutCallback layoutCallback)
        {
            // Validate arguments
            if (text == null) { throw new ArgumentNullException(nameof(text)); }
            if (font == null) { throw new ArgumentNullException(nameof(font)); }
            if (size < 1) { throw new ArgumentException("Font size must be greater than zero.", nameof(size)); }
            if (layoutCallback == null) { throw new ArgumentNullException(nameof(layoutCallback)); }

            // Get atlas, layout text
            var atlas = FontManager.GetAtlas(font, size);
            PerformLayout(text, bounds, align, atlas, layoutCallback);
        }

        internal static void PerformLayout(string text, Rectangle bounds, TextAlign align, FontAtlas atlas, TextLayoutCallback layoutCallback)
        {
            // Extract atlas properties for brevity
            var fontSize = atlas.FontSize;
            var font = atlas.Font;

            // Create character layout state
            var state = new TextLayoutState
            {
                Position = bounds.Position,
                Character = (UnicodeCharacter) 0
            };

            // Find the first break point (if none, set to -1)
            var nextBreak = FindNextBreak(in text, 0, state.Character, in state.Position, in bounds, atlas, out var lineWidth);
            var offsetX = ComputeAlignmentOffset(bounds, align, lineWidth);
            state.Position.X += offsetX; // First line alignment offset

            // For each character
            for (var i = 0; i < text.Length; i++)
            {
                // Beyond bottom of layout box, can't render anymore!
                // This is a 'truncate' vertical overflow mode
                if (state.Position.Y > bounds.Bottom)
                {
                    break;
                }

                // 
                var previous = state.Character;
                state.Character = text.GetCharacter(i);

                // Apply kerning with previous character
                state.Position.X += font.GetKerning(previous, state.Character, fontSize);

                // Get the relevant glyph, if exists (should always exist?)
                if (atlas.TryGetGlyph(state.Character, out var glyph))
                {
                    // Get metrics for glyph as the desired font size
                    state.Metrics = glyph.GetMetrics(fontSize);

                    // Process character, if kept, advance pen position
                    layoutCallback(text, i, ref state);

                    // Apply horizontal advance
                    state.Position.X += state.Metrics.AdvanceWidth;
                }

                // We should break (newline, edge of bounds, etc)
                if (nextBreak == i)
                {
                    // Line feed
                    state.Position.Y += atlas.Metrics.LineAdvance;
                    state.Position.X = bounds.Left;

                    // Find the next break point
                    nextBreak = FindNextBreak(in text, i + 1, state.Character, in state.Position, in bounds, atlas, out lineWidth);
                    offsetX = ComputeAlignmentOffset(bounds, align, lineWidth);
                    state.Position.X += offsetX;
                }
            }
        }

        internal static Rectangle GetPositionAnchoredTextBounds(in string text, in Font font, in int size, in Vector position, in TextAlign align)
        {
            var measure = Measure(text, font, size).Size;

            var pos = position;

            switch (align)
            {
                default:
                case TextAlign.Left:
                    // nothing to do
                    break;

                case TextAlign.Center:
                    pos.X -= measure.Width / 2F;
                    break;

                case TextAlign.Right:
                    pos.X -= measure.Width;
                    break;
            }

            return new Rectangle(pos, measure);
        }

        private static float ComputeAlignmentOffset(in Rectangle bounds, TextAlign align, float lineWidth)
        {
            var offset = 0F;
            if (align != TextAlign.Left) { offset = bounds.Width - lineWidth; }
            if (align == TextAlign.Center) { offset /= 2F; }
            return offset;
        }

        // checks if character should break (newline or word too long, etc)
        private static int FindNextBreak(in string text, int index, UnicodeCharacter previous, in Vector position, in Rectangle bounds, FontAtlas atlas, out float width)
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
                var kerning = atlas.Font.GetKerning(previous, character, atlas.FontSize);
                edge += kerning;

                // Character is definintely a break (newline, etc)
                if (breakCategory == TextBreakCategory.Mandatory)
                {
                    width = prevEdge - position.X;
                    return i;
                }

                // Character could be a break if the next word violates the bounds (space, dash, etc)
                if (breakCategory == TextBreakCategory.Opportunity)
                {
                    // Mark the opportunity index
                    opportunityEdge = prevEdge;
                    opportunity = i;
                }

                if (atlas.TryGetGlyph(character, out var glyph))
                {
                    // Advance right edge
                    var metrics = glyph.GetMetrics(atlas.FontSize);
                    edge += metrics.AdvanceWidth;

                    // We found a break opportunity, we need to now check if we violate the bounds
                    if (opportunity >= 0)
                    {
                        // Violated bounds (within a tolerance approximated by character width)
                        if (edge > (bounds.Right + (metrics.AdvanceWidth / 10F)))
                        {
                            width = opportunityEdge - position.X;
                            return opportunity;
                        }
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
                if (c == '\n' || c == '\r') { return TextBreakCategory.Mandatory; }
                else { return TextBreakCategory.Opportunity; }
            }

            // Opportunity to break on dashes
            if (c == '-') { return TextBreakCategory.Opportunity; }

            // Shouldn't break
            return TextBreakCategory.None;
        }

        #endregion
    }
}
