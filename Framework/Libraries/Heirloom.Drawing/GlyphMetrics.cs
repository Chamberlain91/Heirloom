using Heirloom.Math;

namespace Heirloom.Drawing
{
    public readonly struct GlyphMetrics
    {
        public readonly float AdvanceWidth;

        public readonly float Bearing;

        public readonly IntRectangle Box;

        public IntVector Offset => Box.Position;

        public IntSize Size => Box.Size;

        internal GlyphMetrics(float advanceWidth, float bearing, IntRectangle box)
        {
            AdvanceWidth = advanceWidth;
            Bearing = bearing;
            Box = box;
        }
    }
}
