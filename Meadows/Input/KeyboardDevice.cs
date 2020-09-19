using System;

namespace Meadows.Input
{
    public abstract class KeyboardDevice
    {
        /// <summary>
        /// An event raised when a character has been typed.
        /// </summary>
        public abstract event Action<CharacterEvent> CharacterTyped;

        /// <summary>
        /// An event raised when a button on the keyboard was pressed.
        /// </summary>
        public abstract event Action<KeyEvent> KeyPressed;

        /// <summary>
        /// An event raised when a button on the keyboard was released.
        /// </summary>
        public abstract event Action<KeyEvent> KeyReleased;

        /// <summary>
        /// An event raised when a button on the keyboard was 'repeated'.
        /// </summary>
        /// <remarks>
        /// This usually occurs from holding the key down for a period of time.
        /// </remarks>
        public abstract event Action<KeyEvent> KeyRepeat;

        /// <summary>
        /// Gets a value that determines if a software keyboard is supported on this platform.
        /// </summary>
        public abstract bool SupportsSoftwareKeyboard { get; }

        /// <summary>
        /// Attempts to retreive the state of the specified key.
        /// </summary>
        /// <param name="key">Some key.</param>
        /// <param name="state">Outputs the current state of the key, if call was successful.</param>
        /// <returns>True if the value was sucessfully retreived.</returns>
        public abstract bool TryGetKey(Key key, out ButtonState state);

        /// <summary>
        /// Attempts to show the software keyboard.
        /// </summary>
        public abstract void ShowSoftwareKeyboard();

        /// <summary>
        /// Hides the software keyboard.
        /// </summary>
        public abstract void HideSoftwareKeyboard();
    }
}
