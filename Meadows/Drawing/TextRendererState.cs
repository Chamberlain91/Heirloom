using System;
using System.Collections.Generic;

using Meadows.Mathematics;

namespace Meadows.Drawing
{
    /// <summary>
    /// Represents information of any particular glyph when drawing text.
    /// </summary>
    /// <category>Drawing</category>
    public struct TextRendererState : IEquatable<TextRendererState>
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
        /// Compares this <see cref="TextRendererState"/> for equality with another object.
        /// </summary>
        public override bool Equals(object obj)
        {
            return obj is TextRendererState state && Equals(state);
        }

        /// <summary>
        /// Compares this <see cref="TextRendererState"/> for equality with another <see cref="TextRendererState"/>.
        /// </summary>
        public bool Equals(TextRendererState other)
        {
            return EqualityComparer<Matrix>.Default.Equals(Transform, other.Transform) &&
                   Position.Equals(other.Position) &&
                   Color.Equals(other.Color);
        }

        /// <summary>
        /// Returns the hash code for this <see cref="TextRendererState"/>.
        /// </summary>
        public override int GetHashCode()
        {
            return HashCode.Combine(Transform, Position, Color);
        }

        /// <summary>
        /// Compares two instances of <see cref="TextRendererState"/> for equality.
        /// </summary>
        public static bool operator ==(TextRendererState left, TextRendererState right)
        {
            return left.Equals(right);
        }

        /// <summary>
        /// Compares two instances of <see cref="TextRendererState"/> for inequality.
        /// </summary>
        public static bool operator !=(TextRendererState left, TextRendererState right)
        {
            return !(left == right);
        }

        #endregion
    }
}
