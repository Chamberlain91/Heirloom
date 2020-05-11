namespace Heirloom
{
    public readonly struct CharacterEvent
    {
        public readonly UnicodeCharacter Character;

        public CharacterEvent(UnicodeCharacter character)
        {
            Character = character;
        }
    }
}
