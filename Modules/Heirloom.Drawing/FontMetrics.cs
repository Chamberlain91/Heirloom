using System;

namespace Heirloom.Drawing
{
    /// <summary>
    /// Contains information about a font (ie, the vertical metrics).
    /// </summary>
    public readonly struct FontMetrics
    {
        /// <summary>
        /// The measure from the baseline to the top of any glyphs.
        /// </summary>
        public readonly float Ascent;

        /// <summary>
        /// The measure from the baseline to the bottom of any glyph.
        /// </summary>
        public readonly float Descent;

        /// <summary>
        /// The spacing between lines.
        /// </summary>
        public readonly float LineGap;

        internal FontMetrics(float ascent, float descent, float lineGap)
        {
            Ascent = ascent;
            Descent = descent;
            LineGap = lineGap;
        }

        /// <summary>
        /// The height of the line.
        /// </summary>
        public float Height => Ascent - Descent;

        /// <summary>
        /// The spacing between baselines of multiline text.
        /// </summary>
        public float LineAdvance => Height + LineGap;

        #region Equality

        public override bool Equals(object obj)
        {
            if (obj is FontMetrics metrics)
            {
                return (metrics.Ascent == Ascent)
                    && (metrics.Descent == Descent)
                    && (metrics.LineGap == LineGap);
            }

            return false;
        }

        public override int GetHashCode()
        {
            return HashCode.Combine(Ascent, Descent, LineGap);
        }

        public static bool operator ==(FontMetrics left, FontMetrics right)
        {
            return left.Equals(right);
        }

        public static bool operator !=(FontMetrics left, FontMetrics right)
        {
            return !(left == right);
        }

        #endregion
    }
}
