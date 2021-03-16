using System;

using Heirloom.Mathematics;

namespace Heirloom.Drawing
{
    /// <summary>
    /// Contains information about a glyph (ie, the horizontal metrics).
    /// </summary>
    /// <category>Text</category>
    public readonly struct GlyphMetrics
    {
        /// <summary>
        /// The advance width of the glyph. 
        /// This is the spacing between the glyph's left edge and the next glyph.
        /// </summary>
        public readonly float AdvanceWidth;

        /// <summary>
        /// The glyph offset from the pen position (in pixels).
        /// </summary>
        public readonly IntVector Offset;

        /// <summary>
        /// The size of the glyph (in pixels).
        /// </summary>
        public readonly IntSize Size;

        #region Constructors

        internal GlyphMetrics(float advanceWidth, IntVector offset, IntSize size)
        {
            AdvanceWidth = advanceWidth;
            Offset = offset;
            Size = size;
        }

        #endregion

        #region Equality

        /// <summary>
        /// Compares this <see cref="GlyphMetrics"/> for equality with another object.
        /// </summary>
        public override bool Equals(object obj)
        {
            if (obj is GlyphMetrics metrics)
            {
                return (metrics.AdvanceWidth == AdvanceWidth)
                    && (metrics.Offset == Offset)
                    && (metrics.Size == Size);
            }

            return false;
        }

        /// <summary>
        /// Returns the hash code this <see cref="GlyphMetrics"/>.
        /// </summary>
        public override int GetHashCode()
        {
            return HashCode.Combine(AdvanceWidth, Offset, Size);
        }

        /// <summary>
        /// Compares two instances of <see cref="GlyphMetrics"/> for equality.
        /// </summary>
        public static bool operator ==(GlyphMetrics left, GlyphMetrics right)
        {
            return left.Equals(right);
        }

        /// <summary>
        /// Compares two instances of <see cref="GlyphMetrics"/> for inequality.
        /// </summary>
        public static bool operator !=(GlyphMetrics left, GlyphMetrics right)
        {
            return !(left == right);
        }

        #endregion
    }
}
