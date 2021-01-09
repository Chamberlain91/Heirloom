using Meadows.Text;

namespace Meadows
{
    /// <summary>
    /// Contains the data of an event when a character has been typed on some input source.
    /// </summary>
    /// <category>User Input</category>
    public readonly struct CharacterEvent
    {
        /// <summary>
        /// The unicode character.
        /// </summary>
        public readonly UnicodeCharacter Character;

        /// <summary>
        /// Constructs a new instance of <see cref="CharacterEvent"/>.
        /// </summary>
        public CharacterEvent(UnicodeCharacter character)
        {
            Character = character;
        }
    }
}
