using System;
using System.Collections.Generic;

using Heirloom.Math;

namespace Heirloom.Drawing
{
    /// <summary>
    /// Represents information of any particular glyph during text layout.
    /// </summary>
    public struct TextLayoutState : IEquatable<TextLayoutState>
    {
        /// <summary>
        /// The current character.
        /// </summary>
        public UnicodeCharacter Character { get; internal set; }

        /// <summary>
        /// The metrics of the glyph being rendered.
        /// </summary>
        public GlyphMetrics Metrics { get; internal set; }

        /// <summary>
        /// The position of top left corner of the current glyph image.
        /// </summary>
        public Vector Position;

        #region Equality

        public override bool Equals(object obj)
        {
            return obj is TextLayoutState state && Equals(state);
        }

        public bool Equals(TextLayoutState other)
        {
            return Character.Equals(other.Character) &&
                   Position.Equals(other.Position) &&
                   EqualityComparer<GlyphMetrics>.Default.Equals(Metrics, other.Metrics);
        }

        public override int GetHashCode()
        {
            return HashCode.Combine(Character, Position, Metrics);
        }

        public static bool operator ==(TextLayoutState left, TextLayoutState right)
        {
            return left.Equals(right);
        }

        public static bool operator !=(TextLayoutState left, TextLayoutState right)
        {
            return !(left == right);
        }

        #endregion
    }
}
