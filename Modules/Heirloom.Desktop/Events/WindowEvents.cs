using Heirloom.Math;

namespace Heirloom.Desktop
{
    public readonly struct WindowEvents
    {
        public readonly float XScale;

        public readonly float YScale;

        internal WindowEvents(float xScale, float yScale)
        {
            XScale = xScale;
            YScale = yScale;
        }

        public Vector ContentScale => new Vector(XScale, YScale);
    }
}
