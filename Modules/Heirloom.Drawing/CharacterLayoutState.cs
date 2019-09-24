
using Heirloom.Math;

namespace Heirloom.Drawing
{
    public struct CharacterLayoutState
    {
        /// <summary>
        /// The current character.
        /// </summary>
        public UnicodeCharacter Character;

        /// <summary>
        /// The position of the glyph.
        /// </summary>
        public Vector Position;

        /// <summary>
        /// The metrics of the glyph being rendered.
        /// </summary>
        public GlyphMetrics Metrics { get; internal set; }
    }
}
