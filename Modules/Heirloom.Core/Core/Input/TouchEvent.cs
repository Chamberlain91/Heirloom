namespace Heirloom
{
    public readonly struct TouchEvent
    {
        public readonly Touch Touch;

        public TouchEvent(Touch touch)
        {
            Touch = touch;
        }
    }
}
