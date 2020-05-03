namespace Heirloom
{
    public readonly struct MouseScrollEvent
    {
        public readonly Vector Scroll;

        public MouseScrollEvent(float x, float y)
        {
            Scroll = new Vector(x, y);
        }
    }
}
