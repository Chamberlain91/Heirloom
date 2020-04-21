using Heirloom.Drawing;

namespace Heirloom.Platforms.Desktop
{
    public readonly struct CharacterEvent
    {
        public readonly UnicodeCharacter Character;

        internal CharacterEvent(UnicodeCharacter character)
        {
            Character = character;
        }
    }
}
