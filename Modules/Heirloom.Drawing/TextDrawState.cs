using System;
using System.Collections.Generic;

using Heirloom.Math;

namespace Heirloom.Drawing
{
    /// <summary>
    /// Represents information of any particular glyph when drawing text.
    /// </summary>
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

        public override bool Equals(object obj)
        {
            return obj is TextDrawState state && Equals(state);
        }

        public bool Equals(TextDrawState other)
        {
            return EqualityComparer<Matrix>.Default.Equals(Transform, other.Transform) &&
                   Position.Equals(other.Position) &&
                   Color.Equals(other.Color);
        }

        public override int GetHashCode()
        {
            return HashCode.Combine(Transform, Position, Color);
        }

        public static bool operator ==(TextDrawState left, TextDrawState right)
        {
            return left.Equals(right);
        }

        public static bool operator !=(TextDrawState left, TextDrawState right)
        {
            return !(left == right);
        }

        #endregion
    }
}
