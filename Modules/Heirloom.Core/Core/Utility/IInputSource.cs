using System;

namespace Heirloom
{
    /// <summary>
    /// Represents the functionality of an input source.
    /// </summary>
    public interface IInputSource
    {
        #region Keyboard

        event Action<Screen, CharacterEvent> CharacterTyped;

        event Action<Screen, KeyEvent> KeyPressed;
        event Action<Screen, KeyEvent> KeyReleased;
        event Action<Screen, KeyEvent> KeyRepeat;

        /// <summary>
        /// Gets a value that determines if a software keyboard is supported on this platform.
        /// </summary>
        bool SupportsSoftwareKeyboard { get; }

        /// <summary>
        /// Attempts to retreive the state of the specified key.
        /// </summary>
        /// <param name="key">Some key.</param>
        /// <param name="state">The output of the current state of the key.</param>
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

        event Action<Screen, MouseButtonEvent> MousePressed;
        event Action<Screen, MouseButtonEvent> MouseReleased;
        event Action<Screen, MouseScrollEvent> MouseScrolled;
        event Action<Screen, MouseMoveEvent> MouseMoved;

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
