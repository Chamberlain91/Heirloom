using System;

namespace Heirloom.UI
{
    public readonly struct GuiIdentifier
    {
        public readonly int Hash;

        public GuiIdentifier(string name)
            : this(HashCode.Combine(name))
        { }

        public GuiIdentifier(string name, string kind)
            : this(HashCode.Combine(name, kind))
        { }

        public GuiIdentifier(int hash)
        {
            Hash = hash;
        }

        public static implicit operator int(GuiIdentifier identifier)
        {
            return identifier.Hash;
        }

        public static implicit operator GuiIdentifier(string title)
        {
            return new GuiIdentifier(title);
        }

        public static implicit operator GuiIdentifier(int hash)
        {
            return new GuiIdentifier(hash);
        }
    }
}
