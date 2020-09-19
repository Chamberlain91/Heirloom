using Meadows.Input;

using System;

namespace Meadows.Engine.Input
{
    public static class Keyboard
    {
        /// <summary>
        /// Gets a value that determines if keyboard input is supported on this device.
        /// </summary>
        public static bool IsSupported => throw new NotImplementedException();

        /// <summary>
        /// Gets a value that determines if a software keyboard is supported on this platform (ex, on screen touch keyboard)
        /// </summary>
        public static bool SupportsSoftwareKeyboard => throw new NotImplementedException();

        /// <summary>
        /// An event raised when a character has been typed.
        /// </summary>
        public static event Action<CharacterEvent> CharacterTyped;

        /// <summary>
        /// Gets the latest state of a button the keyboard.
        /// </summary>
        /// <param name="key">Some key.</param>
        public static ButtonState GetKey(Key key)
        {
            throw new NotImplementedException();
        }

        /// <summary>
        /// Attempts to show the software keyboard.
        /// </summary>
        public static void ShowSoftwareKeyboard()
        {
            throw new NotImplementedException();
        }

        /// <summary>
        /// Hides the software keyboard.
        /// </summary>
        public static void HideSoftwareKeyboard()
        {
            throw new NotImplementedException();
        }

        internal static void SetDevice(KeyboardDevice keyboard)
        {
            throw new NotImplementedException();
        }
    }
}
