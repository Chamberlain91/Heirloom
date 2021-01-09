using System;
using System.Collections.Generic;

namespace Heirloom
{
    /// <summary>
    /// Represents information of any particular glyph when drawing text.
    /// </summary>
    /// <category>Drawing</category>
    public struct TextDrawState : IEquatable<TextDrawState>
    {
        /// <summary>
        /// The relative transform to apply to the current glyph image (set to <see cref="Matrix.Identity"/> by default).
        /// </summary>
        public Matrix Transform;

        /// <summary>
        /// The position of top left corner of the current glyph image.
        /// </summary>
        public Vector Position;

        /// <summary>
        /// The color of the current glyph.
        /// </summary>
        public Color Color;

        #region Equality

        /// <summary>
        /// Compares this <see cref="TextDrawState"/> for equality with another object.
        /// </summary>
        public override bool Equals(object obj)
        {
            return obj is TextDrawState state && Equals(state);
        }

        /// <summary>
        /// Compares this <see cref="TextDrawState"/> for equality with another <see cref="TextDrawState"/>.
        /// </summary>
        public bool Equals(TextDrawState other)
        {
            return EqualityComparer<Matrix>.Default.Equals(Transform, other.Transform) &&
                   Position.Equals(other.Position) &&
                   Color.Equals(other.Color);
        }

        /// <summary>
        /// Returns the hash code for this <see cref="TextDrawState"/>.
        /// </summary>
        public override int GetHashCode()
        {
            return HashCode.Combine(Transform, Position, Color);
        }

        /// <summary>
        /// Compares two instances of <see cref="TextDrawState"/> for equality.
        /// </summary>
        public static bool operator ==(TextDrawState left, TextDrawState right)
        {
            return left.Equals(right);
        }

        /// <summary>
        /// Compares two instances of <see cref="TextDrawState"/> for inequality.
        /// </summary>
        public static bool operator !=(TextDrawState left, TextDrawState right)
        {
            return !(left == right);
        }

        #endregion
    }
}
