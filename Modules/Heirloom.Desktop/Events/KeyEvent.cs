namespace Heirloom.Desktop
{
    public readonly struct KeyEvent
    {
        public readonly Key Key;

        public readonly int ScanCode;

        public readonly ButtonAction Action;

        public readonly KeyModifiers Modifiers;

        internal KeyEvent(Key key, int scanCode, ButtonAction action, KeyModifiers modifiers)
        {
            Key = key;
            ScanCode = scanCode;
            Action = action;
            Modifiers = modifiers;
        }
    }
}
