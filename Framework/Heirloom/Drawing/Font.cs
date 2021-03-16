using System;

using Heirloom.Text;

namespace Heirloom.Drawing
{
    /// <summary>
    /// An object to represent a renderable text font.
    /// Provides functionality to query and measure aspects of the font.
    /// </summary>
    /// <category>Text</category>
    public abstract class Font : IDisposable
    {
        #region Defaults (Built-In)

        static Font()
        {
            // Load default pixel font
            Default = new TrueTypeFont("embedded/fonts/monogram_extended.ttf");

            // Load default sans-serif fonts
            SansSerif = new TrueTypeFont("embedded/fonts/montserrat/montserrat-regular.ttf");
            SansSerifBold = new TrueTypeFont("embedded/fonts/montserrat/montserrat-bold.ttf");
        }

        /// <summary>
        /// A default monospaced pixel font for easily rendering text to debug, show metrics, etc.
        /// Recommended size is 16px.
        /// </summary>
        /// <remarks>https://datagoblin.itch.io/monogram</remarks>
        public static Font Default { get; }

        /// <summary>
        /// A default sans-serif font for easily rendering text to debug, show metrics, etc.
        /// </summary>
        /// <remarks>https://fonts.google.com/specimen/Montserrat</remarks>
        public static Font SansSerif { get; }

        /// <summary>
        /// A default sans-serif font for easily rendering text to debug, show metrics, etc.
        /// </summary>
        /// <remarks>https://fonts.google.com/specimen/Montserrat</remarks>
        public static Font SansSerifBold { get; }

        #endregion

        /// <summary>
        /// Get the vertical metrics of the this font at the specified size.
        /// </summary>
        /// <param name="size">The size of the font.</param>
        public abstract FontMetrics GetMetrics(float size);

        /// <summary>
        /// Gets the information about a particular glyph in this font.
        /// </summary>
        /// <param name="ch">Some character.</param>
        public abstract Glyph GetGlyph(UnicodeCharacter ch);

        /// <summary>
        /// Gets the information about a particular glyph in this font.
        /// </summary>
        /// <param name="ch">Some character.</param>
        public Glyph GetGlyph(char ch)
        {
            return GetGlyph((UnicodeCharacter) ch);
        }

        /// <summary>
        /// Gets the spacing adjustment (ie, kerning) between any two characters.
        /// </summary>
        public abstract float GetKerning(UnicodeCharacter cp1, UnicodeCharacter cp2, float size);

        /// <summary>
        /// Dispose the current font, freeing unmanaged resources.
        /// </summary>
        public abstract void Dispose();
    }
}
