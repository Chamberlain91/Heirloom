namespace Heirloom
{
    public readonly struct KeyEvent
    {
        public readonly int ScanCode;
        public readonly Key Key;
        public readonly KeyModifiers Modifiers;
        public readonly ButtonState State;

        public KeyEvent(int scanCode, Key key, KeyModifiers modifiers, ButtonState state)
        {
            ScanCode = scanCode;
            Key = key;
            Modifiers = modifiers;
            State = state;
        }
    }
}
