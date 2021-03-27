using System;

using Heirloom.Mathematics;

namespace Heirloom
{
    /// <summary>
    /// Represents the functionality of an input source.
    /// </summary>
    /// <category>User Input</category>
    public interface IInputSource
    {
        /// <summary>
        /// Called when this input source becomes active (ie, added to the input system).
        /// </summary>
        void Activate();

        /// <summary>
        /// Called when this input source is deactivated (ie, removed from the input system).
        /// </summary>
        void Deactivate();

        #region Keyboard

        /// <summary>
        /// Gets a value that determines if a software keyboard is supported on this platform.
        /// </summary>
        bool SupportsSoftwareKeyboard { get; }

        /// <summary>
        /// Gets the typed text since last update.
        /// </summary>
        string TextInput { get; }

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
        /// Gets the latest mouse position.
        /// </summary>
        Vector MousePosition { get; }

        /// <summary>
        /// Gets the latest difference in mouse position.
        /// </summary>
        Vector MouseDelta { get; }

        /// <summary>
        /// Gets the latest mouse wheel.
        /// </summary>
        Vector MouseScroll { get; }

        /// <summary>
        /// Attempts to retreive the state of the specified mouse button.
        /// </summary>
        /// <param name="button">The mouse button to query.</param>
        /// <param name="state">Outputs the current state of the button, if call was successful.</param>
        /// <returns>True if the value was sucessfully retreived.</returns>
        bool TryGetButton(MouseButton button, out ButtonState state);

        #endregion

        #region Touch

        /// <summary>
        /// Gets a value that determines if touch-based input is supported.
        /// </summary>
        bool HasTouchSupport { get; }

        /// <summary>
        /// Gets the number of touch points available this frame.
        /// </summary>
        int TouchCount { get; }

        /// <summary>
        /// Gets the information about one of the touch points this frame.
        /// </summary>
        Touch GetTouch(int index);

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

#endif
    }

    public readonly struct Touch
    {
        public readonly Vector Position;

        public readonly Vector Delta;

        public readonly ButtonState State;

        public readonly int Finger;

        public Touch(Vector position, Vector delta, int finger, ButtonState state)
        {
            Position = position;
            Delta = delta;
            Finger = finger;
            State = state;
        }
    }
}
