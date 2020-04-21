using System;

using Heirloom.Math;

namespace Heirloom.Drawing
{
    /// <summary>
    /// Contains information about a glyph (ie, the horizontal metrics).
    /// </summary>
    public readonly struct GlyphMetrics
    {
        /// <summary>
        /// The advance width of the glyph. 
        /// This is the spacing between the glyph's left edge and the next glyph.
        /// </summary>
        public readonly float AdvanceWidth;

        /// <summary>
        /// The bearing of this glyph.
        /// </summary>
        public readonly float Bearing;

        private readonly IntRectangle _box;

        internal GlyphMetrics(float advanceWidth, float bearing, IntRectangle box)
        {
            AdvanceWidth = advanceWidth;
            Bearing = bearing;
            _box = box;
        }

        /// <summary>
        /// The glyph offset from the pen position.
        /// </summary>
        public IntVector Offset => _box.Position;

        /// <summary>
        /// The glyph bounds size.
        /// </summary>
        public IntSize Size => _box.Size;

        #region Equality

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

        public override int GetHashCode()
        {
            return HashCode.Combine(AdvanceWidth, Bearing, _box);
        }

        public static bool operator ==(GlyphMetrics left, GlyphMetrics right)
        {
            return left.Equals(right);
        }

        public static bool operator !=(GlyphMetrics left, GlyphMetrics right)
        {
            return !(left == right);
        }

        #endregion
    }
}
