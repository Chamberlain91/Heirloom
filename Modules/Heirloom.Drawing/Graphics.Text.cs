using System;
using System.Runtime.CompilerServices;

using Heirloom.Math;

namespace Heirloom.Drawing
{
    public delegate void DrawTextCallback(string text, int index, ref CharacterDrawState state);

    public abstract partial class Graphics
    {
        #region Draw Styled Text (Extension Methods)

        /// <summary>
        /// Draws rich text to the current surface.
        /// </summary>
        /// <param name="styledText">The rich text to draw.</param>
        /// <param name="position">The anchor position to layout text around.</param>
        /// <param name="font">The font to render with.</param>
        /// <param name="size">The font size to render with.</param>
        /// <param name="align">The text alignment.</param>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public void DrawText(StyledText styledText, Vector position, Font font, int size, TextAlign align = TextAlign.Left)
        {
            DrawText(styledText.Text, position, font, size, align, styledText.Callback);
        }

        /// <summary>
        /// Draws rich text to the current surface.
        /// </summary>
        /// <param name="styledText">The rich text to draw.</param>
        /// <param name="bounds">The boundng region to layout text.</param>
        /// <param name="font">The font to render with.</param>
        /// <param name="size">The font size to render with.</param>
        /// <param name="align">The text alignment.</param>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public void DrawText(StyledText styledText, Rectangle bounds, Font font, int size, TextAlign align = TextAlign.Left)
        {
            DrawText(styledText.Text, bounds, font, size, align, styledText.Callback);
        }

        #endregion

        #region Draw Text (Extension Methods)

        /// <summary>
        /// Draws text to the current surface.
        /// </summary>
        /// <param name="text">The text to draw.</param>
        /// <param name="position">The anchor position to layout text around.</param>
        /// <param name="font">The font to render with.</param>
        /// <param name="size">The font size to render with.</param>
        /// <param name="callback">A callback for manipulating the style of the rendered text.</param> 
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public void DrawText(string text, Vector position, Font font, int size, DrawTextCallback callback)
        {
            DrawText(text, position, font, size, TextAlign.Left, callback);
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
        public void DrawText(string text, Vector position, Font font, int size, TextAlign align = TextAlign.Left, DrawTextCallback callback = null)
        {
            var bounds = TextRenderer.GetPositionAnchoredTextBounds(text, font, size, position, align);
            DrawText(text, bounds, font, size, align, callback);
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
        public void DrawText(string text, Rectangle bounds, Font font, int size, DrawTextCallback callback)
        {
            DrawText(text, bounds, font, size, TextAlign.Left, callback);
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
        public void DrawText(string text, Rectangle bounds, Font font, int size, TextAlign align = TextAlign.Left, DrawTextCallback callback = null)
        {
            if (text is null) { throw new ArgumentNullException(nameof(text)); }
            if (font == null) { throw new ArgumentNullException(nameof(font)); }
            if (size < 1) { throw new ArgumentException("Font size must be greater than zero.", nameof(size)); }

            // Remember context state
            var color = Color;

            // Select atlas
            var atlas = FontManager.GetAtlas(font, size);

            // Character render state
            var state = new CharacterDrawState { Color = color };

            // Layout text
            TextRenderer.PerformLayout(text, bounds, align, atlas, (string _, int index, ref CharacterLayoutState layout) =>
            {
                // Set initial state
                state.Transform = Matrix.Identity;
                state.Position = layout.Position;
                state.Color = color;

                // Process character (per character animation, etc)
                callback?.Invoke(text, index, ref state);

                // Compute render position
                state.Position.X += layout.Metrics.Offset.X;
                state.Position.Y += layout.Metrics.Offset.Y + atlas.Metrics.Ascent;

                // Get glyph image
                var image = atlas.GetImage(layout.Character);

                // If has image data, draw to surface
                if (image != null)
                {
                    // Draw to surface
                    Color = state.Color;
                    DrawImage(image, Matrix.CreateTranslation(state.Position + image.Origin) * state.Transform);
                }
            });

            // Restore context state
            Color = color;
        }

        #endregion 
    }
}
