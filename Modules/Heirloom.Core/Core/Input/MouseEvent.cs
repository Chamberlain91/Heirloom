namespace Heirloom
{
    public readonly struct MouseButtonEvent
    {
        public readonly MouseButton Button;
        public readonly KeyModifiers Modifiers;
        public readonly ButtonState State;
        public readonly Vector Position;

        public MouseButtonEvent(MouseButton button, KeyModifiers modifiers, ButtonState state, Vector position)
        {
            Button = button;
            Modifiers = modifiers;
            State = state;
            Position = position;
        }
    }
}
