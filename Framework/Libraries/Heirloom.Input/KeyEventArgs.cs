using System;

namespace Heirloom.Input
{
    public class KeyEventArgs : EventArgs
    {
        public readonly Key Key;

        public readonly KeyModifiers Modifiers;

        public readonly int Code;

        public readonly bool IsDown;

        public KeyEventArgs(Key key, int code, KeyModifiers modifiers, bool isDown)
        {
            Key = key;
            Modifiers = modifiers;
            Code = code;
            IsDown = isDown;
        }
    }
}
