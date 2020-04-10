using Heirloom.Math;

namespace Heirloom.Desktop
{
    public readonly struct MouseMoveEvent
    {
        public readonly Vector Position;

        public readonly Vector Delta;

        internal MouseMoveEvent(Vector position, Vector delta)
        {
            Position = position;
            Delta = delta;
        }
    }
}
