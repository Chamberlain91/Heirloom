using Heirloom.Math;

namespace Heirloom.Platforms.Desktop
{
    public readonly struct MouseScrollEvent
    {
        public readonly Vector Scroll;

        internal MouseScrollEvent(float x, float y)
        {
            Scroll = new Vector(x, y);
        }
    }
}
