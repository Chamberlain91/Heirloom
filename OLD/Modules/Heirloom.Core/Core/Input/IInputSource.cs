using System;

namespace Heirloom
{
    /// <summary>
    /// Represents the functionality of an input source.
    /// </summary>
    /// <category>User Input</category>
    public interface IInputSource
    {
        #region Keyboard

        /// <summary>
        /// An event raised when a character has been typed.
        /// </summary>
        event Action<Screen, CharacterEvent> CharacterTyped;

        /// <summary>
        /// An event raised when a button on the keyboard was pressed.
        /// </summary>
        event Action<Screen, KeyEvent> KeyPressed;

        /// <summary>
        /// An event raised when a button on the keyboard was released.
        /// </summary>
        event Action<Screen, KeyEvent> KeyReleased;

        /// <summary>
        /// An event raised when a button on the keyboard was 'repeated'.
        /// </summary>
        /// <remarks>
        /// This usually occurs from holding the key down for a period of time.
        /// </remarks>
        event Action<Screen, KeyEvent> KeyRepeat;

        /// <summary>
        /// Gets a value that determines if a software keyboard is supported on this platform.
        /// </summary>
        bool SupportsSoftwareKeyboard { get; }

        /// <summary>
        /// Attempts to retreive the state of the specified key.
        /// </summary>
        /// <param name="key">Some key.</param>
        /// <param name="state">Outputs the current state of the key, if call was successful.</param>
        /// <returns>True if the value was sucessfully retreived.</returns>
        bool TryGetKey(Key key, out ButtonState state);

        /// <summary>
        /// Attempts to show the software keyboard.
        /// </summary>
        void ShowSoftwareKeyboard();

        /// <summary>
        /// Hides the software keyboard.
        /// </summary>
        void HideSoftwareKeyboard();

        #endregion

        #region Mouse 

        /// <summary>
        /// An event raised when a button the mouse is pressed.
        /// </summary>
        event Action<Screen, MouseButtonEvent> MousePressed;

        /// <summary>
        /// An event raised when a button the mouse is released.
        /// </summary>
        event Action<Screen, MouseButtonEvent> MouseReleased;

        /// <summary>
        /// An event raised when mouse has been scrolled.
        /// </summary>
        event Action<Screen, MouseScrollEvent> MouseScrolled;

        /// <summary>
        /// An event raised when mouse has been moved.
        /// </summary>
        event Action<Screen, MouseMoveEvent> MouseMoved;

        /// <summary>
        /// Attempts to retreive the state of the specified mouse button.
        /// </summary>
        /// <param name="button">The mouse button to query.</param>
        /// <param name="state">Outputs the current state of the button, if call was successful.</param>
        /// <returns>True if the value was sucessfully retreived.</returns>
        bool TryGetButton(MouseButton button, out ButtonState state);

        #endregion

#if false

        #region Gamepad

        event Action<int> GamepadDisconnected;
        event Action<int> GamepadConnected;

        bool IsGamepadSupported { get; }
        int GamepadCount { get; }

        ButtonState GetButton(GamepadButton button, int index);
        float GetAxis(GamepadAxis axis, int index);

        #endregion

        #region Touch

        event Action<Screen, TouchEvent> TouchDown;
        event Action<Screen, TouchEvent> TouchUp;
        event Action<Screen, TouchEvent> TouchMoved;

        bool IsTouchSupported { get; }
        int TouchCount { get; }

        Touch GetTouch(int index);

        #endregion

#endif
    }
}
