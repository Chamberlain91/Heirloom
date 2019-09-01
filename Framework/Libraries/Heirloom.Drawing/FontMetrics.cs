namespace Heirloom.Drawing
{
    // TODO: Rename to LineMetrics?
    public readonly struct FontMetrics
    {
        public readonly float Ascent;

        public readonly float Descent;

        public readonly float LineGap;

        internal FontMetrics(float ascent, float descent, float lineGap)
        {
            Ascent = ascent;
            Descent = descent;
            LineGap = lineGap;
        }

        public float Height => Ascent - Descent;

        public float LineAdvance => Height + LineGap;
    }
}
