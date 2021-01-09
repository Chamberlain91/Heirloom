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
        private readonly IntRectangle _box;

        /// <summary>
        /// The advance width of the glyph. 
        /// This is the spacing between the glyph's left edge and the next glyph.
        /// </summary>
        public readonly float AdvanceWidth;

        /// <summary>
        /// The bearing of this glyph.
        /// </summary>
        public readonly float Bearing;

        #region Constructors

        internal GlyphMetrics(float advanceWidth, float bearing, IntRectangle box)
        {
            AdvanceWidth = advanceWidth;
            Bearing = bearing;
            _box = box;
        }

        #endregion

        #region Properties

        /// <summary>
        /// The glyph offset from the pen position.
        /// </summary>
        public IntVector Offset => _box.Position;

        /// <summary>
        /// The glyph bounds size.
        /// </summary>
        public IntSize Size => _box.Size;

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
                    && (metrics.Bearing == Bearing)
                    && (metrics._box == _box);
            }

            return false;
        }

        /// <summary>
        /// Returns the hash code this <see cref="GlyphMetrics"/>.
        /// </summary>
        public override int GetHashCode()
        {
            return HashCode.Combine(AdvanceWidth, Bearing, _box);
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
