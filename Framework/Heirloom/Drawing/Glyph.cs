using Heirloom.Text;

namespace Heirloom.Drawing
{
    /// <summary>
    /// A glyph represents the metrics and rendering of a character from the associated <see cref="Drawing.Font"/>.
    /// </summary>
    /// <category>Text</category>
    public abstract class Glyph
    {
        /// <summary>
        /// Gets the character this glyph represents.
        /// </summary>
        public abstract UnicodeCharacter Character { get; }

        /// <summary>
        /// Gets the associated font.
        /// </summary>
        public abstract Font Font { get; }

        /// <summary>
        /// Get a value that determines if this glyph can be rendered.
        /// </summary>
        public abstract bool CanBeRendered { get; }

        /// <summary>
        /// Get the horizontal metrics of the this glyph at the specified size.
        /// </summary>
        /// <param name="fontSize">The size of the font.</param>
        public abstract GlyphMetrics GetMetrics(float fontSize);

        /// <summary>
        /// Renders the glyph into an image.
        /// </summary>
        /// <param name="fontSize">The font size of the rendered glyph.</param>
        public abstract Image CreateImage(float fontSize);
    }
}
