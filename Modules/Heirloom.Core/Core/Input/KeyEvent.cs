namespace Heirloom
{
    /// <summary>
    /// Contains the data of an event when a key has been pressed or released on some input source.
    /// </summary>
    /// <category>User Input</category>
    public readonly struct KeyEvent
    {
        /// <summary>
        /// The scan code associated with some key.
        /// </summary>
        public readonly int ScanCode;

        /// <summary>
        /// The standard <see cref="Key"/> enum associated with this event.
        /// </summary>
        public readonly Key Key;

        /// <summary>
        /// The modifier keys pressed when the event was generated.
        /// </summary>
        public readonly KeyModifiers Modifiers;

        /// <summary>
        /// The state of the key when the event was generated.
        /// </summary>
        public readonly ButtonState State;

        /// <summary>
        /// Constructs a new <see cref="KeyEvent"/>.
        /// </summary>
        /// <param name="scanCode">The scan code.</param>
        /// <param name="key">The associated key.</param>
        /// <param name="modifiers">The associated key modifiers.</param>
        /// <param name="state">The state of the key.</param>
        public KeyEvent(int scanCode, Key key, KeyModifiers modifiers, ButtonState state)
        {
            ScanCode = scanCode;
            Key = key;
            Modifiers = modifiers;
            State = state;
        }
    }
}
