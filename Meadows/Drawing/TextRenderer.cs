using System;
using System.Runtime.CompilerServices;

using Meadows.Mathematics;

namespace Meadows.Drawing
{
    public static class TextRenderer
    {
        /// <summary>
        /// Delegate type for the callback when drawing text.
        /// </summary>
        /// <param name="text">The complete string being drawn.</param>
        /// <param name="index">The index of the character currently being drawn.</param>
        /// <param name="state">The state of the character currently being drawn.</param>
        /// <category>Drawing</category>
        public delegate void CharacterCallback(string text, int index, ref TextRendererState state);

        /// <summary>
        /// Draws text to the current surface.
        /// </summary>
        /// <param name="text">The text to draw.</param>
        /// <param name="position">The anchor position to layout text around.</param>
        /// <param name="font">The font to render with.</param>
        /// <param name="size">The font size to render with.</param>
        /// <param name="callback">A callback for manipulating the style of the rendered text.</param> 
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static void DrawText(this GraphicsContext ctx, string text, in Vector position, Font font, int size, CharacterCallback callback)
        {
            DrawText(ctx, text, in position, font, size, TextAlign.Left, callback);
        }

        /// <summary>
        /// Draws text to the current surface.
        /// </summary>
        /// <param name="text">The text to draw.</param>
        /// <param name="position">The anchor position to layout text around.</param>
        /// <param name="font">The font to render with.</param>
        /// <param name="size">The font size to render with.</param>
        /// <param name="align">The text alignment.</param>
        /// <param name="callback">A callback for manipulating the style of the rendered text.</param> 
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static void DrawText(this GraphicsContext ctx, string text, in Vector position, Font font, int size, TextAlign align = TextAlign.Left, CharacterCallback callback = null)
        {
            // note: removes vertical alignment since this is computed via measure in GetPositionAnchoredTextBounds.
            var bounds = TextLayout.GetPositionAnchoredTextBounds(text, font, size, in position, align);
            DrawText(ctx, text, bounds, font, size, align & (TextAlign) 0b00_11, callback);
        }

        /// <summary>
        /// Draws text to the current surface.
        /// </summary>
        /// <param name="text">The text to draw.</param>
        /// <param name="bounds">The boundng region to layout text.</param>
        /// <param name="font">The font to render with.</param>
        /// <param name="size">The font size to render with.</param>
        /// <param name="callback">A callback for manipulating the style of the rendered text.</param> 
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static void DrawText(this GraphicsContext ctx, string text, in Rectangle bounds, Font font, int size, CharacterCallback callback)
        {
            DrawText(ctx, text, in bounds, font, size, TextAlign.Left, callback);
        }

        /// <summary>
        /// Draws text to the current surface.
        /// </summary>
        /// <param name="text">The text to draw.</param>
        /// <param name="bounds">The boundng region to layout text.</param>
        /// <param name="font">The font to render with.</param>
        /// <param name="size">The font size to render with.</param>
        /// <param name="align">The text alignment.</param>
        /// <param name="callback">A callback for manipulating the style of the rendered text.</param> 
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static void DrawText(this GraphicsContext ctx, string text, in Rectangle bounds, Font font, int size, TextAlign align = TextAlign.Left, CharacterCallback callback = null)
        {
            if (text is null) { throw new ArgumentNullException(nameof(text)); }
            if (font == null) { throw new ArgumentNullException(nameof(font)); }
            if (size < 1) { throw new ArgumentException("Font size must be greater than zero.", nameof(size)); }

            // Remember context state
            var color = ctx.Color;

            // Get glyph table for the font and size
            var glyphTable = GlyphTable.GetGlyphTable(font, size);

            // Character render state
            var state = new TextRendererState { Color = color };

            // Layout text
            TextLayout.PerformLayout(text, bounds, align, glyphTable, (string _, int index, ref TextLayoutState layout) =>
            {
                // Set initial state
                state.Transform = Matrix.Identity;
                state.Position = layout.Position;
                state.Color = color;

                // Process character (per character animation, etc)
                callback?.Invoke(text, index, ref state);

                // Compute render position
                state.Position.X += layout.Metrics.Offset.X;
                state.Position.Y += layout.Metrics.Offset.Y + glyphTable.Metrics.Ascent;

                // Try to get image from glyph table
                if (glyphTable.TryGetImage(layout.Character, out var image))
                {
                    // Draw to surface
                    ctx.Color = state.Color;
                    ctx.DrawImage(image, Matrix.CreateTranslation(state.Position) * state.Transform);
                }
            });

            // Restore context state
            ctx.Color = color;
        }
    }
}
