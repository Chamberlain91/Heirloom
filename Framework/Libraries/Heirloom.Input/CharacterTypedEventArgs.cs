using System;

namespace Heirloom.Input
{
    public class CharacterTypedEventArgs : EventArgs
    {
        public readonly int Codepoint;

        public CharacterTypedEventArgs(int codepoint)
        {
            Codepoint = codepoint;
        }

        public string Text => char.ConvertFromUtf32(Codepoint);
    }
}
