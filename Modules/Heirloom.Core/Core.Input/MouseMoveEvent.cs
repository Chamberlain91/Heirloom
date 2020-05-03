namespace Heirloom
{
    public readonly struct MouseMoveEvent
    {
        public readonly Vector Position;

        public readonly Vector Delta;

        public MouseMoveEvent(Vector position, Vector delta)
        {
            Position = position;
            Delta = delta;
        }
    }
}
