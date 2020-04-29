namespace Heirloom.Desktop
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
