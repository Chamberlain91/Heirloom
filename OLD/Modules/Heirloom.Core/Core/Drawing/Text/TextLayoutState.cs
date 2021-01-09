using System;
using System.Collections.Generic;

namespace Heirloom
{
    /// <summary>
    /// Represents information of any particular glyph during text layout.
    /// </summary>
    /// <category>Text</category>
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

        /// <summary>
        /// Compares this <see cref="TextLayoutState"/> for equality with another object.
        /// </summary>
        public override bool Equals(object obj)
        {
            return obj is TextLayoutState state && Equals(state);
        }

        /// <summary>
        /// Compares this <see cref="TextLayoutState"/> for equality with another <see cref="TextLayoutState"/>.
        /// </summary>
        public bool Equals(TextLayoutState other)
        {
            return Character.Equals(other.Character) &&
                   Position.Equals(other.Position) &&
                   EqualityComparer<GlyphMetrics>.Default.Equals(Metrics, other.Metrics);
        }

        /// <summary>
        /// Returns the hash code for this <see cref="TextLayoutState"/>.
        /// </summary>
        public override int GetHashCode()
        {
            return HashCode.Combine(Character, Position, Metrics);
        }

        /// <summary>
        /// Compares two instances of <see cref="TextLayoutState"/> for equality.
        /// </summary>
        public static bool operator ==(TextLayoutState left, TextLayoutState right)
        {
            return left.Equals(right);
        }

        /// <summary>
        /// Compares two instances of <see cref="TextLayoutState"/> for inequality.
        /// </summary>
        public static bool operator !=(TextLayoutState left, TextLayoutState right)
        {
            return !(left == right);
        }

        #endregion
    }
}
