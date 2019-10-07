using Heirloom.Math;

namespace Heirloom.Drawing
{
    public struct CharacterDrawState
    {
        /// <summary>
        /// A relative transform to apply to the glyphs (Set to <see cref="Matrix.Identity"/> by default).
        /// </summary>
        public Matrix Transform;

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
