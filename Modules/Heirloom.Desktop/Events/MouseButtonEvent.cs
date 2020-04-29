namespace Heirloom.Desktop
{
    public readonly struct MouseButtonEvent
    {
        public readonly int Button;

        public readonly ButtonAction Action;

        public readonly KeyModifiers Modifiers;

        public readonly Vector Position;

        internal MouseButtonEvent(int button, ButtonAction action, KeyModifiers modifiers, Vector position)
        {
            Button = button;
            Action = action;
            Modifiers = modifiers;
            Position = position;
        }
    }
}
