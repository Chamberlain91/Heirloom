
using Heirloom.Math;

namespace Heirloom.Drawing
{
    public struct CharacterDrawState
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
        /// The color of the glyph.
        /// </summary>
        public Color Color;
    }
}
