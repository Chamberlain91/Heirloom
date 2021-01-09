using System;

using Heirloom.Mathematics;

namespace Heirloom
{
    /// <summary>
    /// Provides a centralized query style input layer.
    /// This is useful for implementing games.
    /// </summary>
    /// <category>User Input</category>
    public static class Input
    {
        /// <summary>
        /// Gets the input source currently tracked.
        /// </summary>
        public static IInputSource InputSource { get; private set; }

        /// <summary>
        /// Begins tracking input from the given input source.
        /// </summary>
        public static void SetInputSource(IInputSource source)
        {
            InputSource?.Deactivate();
            InputSource = source;
            InputSource?.Activate();
        }

        #region Keyboard

        /// <summary>
        /// Is the keyboard emulated by software? (ie mobile deviecs)
        /// </summary>
        public static bool SupportsSoftwareKeyboard => InputSource?.SupportsSoftwareKeyboard ?? false;

        /// <summary>
        /// Gets the typed text since last update.
        /// </summary>
        public static string TextInput => InputSource?.TextInput ?? string.Empty;

        /// <summary>
        /// Gets the latest state of a button the keyboard.
        /// </summary>
        /// <param name="key">Some key.</param>
        public static ButtonState GetKeyState(Key key)
        {
            if (InputSource != null)
            {
                // Return on the first non key up state.
                if (InputSource.TryGetKey(key, out var state) && state != ButtonState.Up)
                {
                    return state;
                }
            }

            // No known state, so must not be pressed.
            return ButtonState.Up;
        }

        private static bool CheckKey(Key key, ButtonState state)
        {
            return GetKeyState(key).HasFlag(state);
        }

        public static bool IsKeyPressed(Key key, bool repeat = false)
        {
            var success = CheckKey(key, ButtonState.Pressed);
            if (repeat) { success |= CheckKey(key, ButtonState.Repeat); }
            return success;
        }

        public static bool IsKeyReleased(Key key)
        {
            return CheckKey(key, ButtonState.Released);
        }

        public static bool IsKeyDown(Key key)
        {
            return CheckKey(key, ButtonState.Down);
        }

        public static bool IsKeyUp(Key key)
        {
            return CheckKey(key, ButtonState.Up);
        }

        /// <summary>
        /// Shows the software keyboard.
        /// </summary>
        public static void ShowSoftwareKeyboard()
        {
            if (SupportsSoftwareKeyboard)
            {
                InputSource.ShowSoftwareKeyboard();
            }
        }

        /// <summary>
        /// Hides the software keyboard.
        /// </summary>
        public static void HideSoftwareKeyboard()
        {
            if (SupportsSoftwareKeyboard)
            {
                InputSource.HideSoftwareKeyboard();
            }
        }

        #endregion

        #region Mouse

        /// <summary>
        /// Gets the latest mouse position.
        /// </summary>
        public static Vector MousePosition => InputSource?.MousePosition ?? Vector.Zero;

        /// <summary>
        /// Gets the latest mouse position delta.
        /// </summary>
        public static Vector MouseDelta => InputSource?.MouseDelta ?? Vector.Zero;

        /// <summary>
        /// Gets the latest state of a mouse button.
        /// </summary>
        /// <param name="button">Some button.</param>
        public static ButtonState GetButtonState(MouseButton button)
        {
            if (InputSource != null)
            {
                // Return on the first non button up state.
                if (InputSource.TryGetButton(button, out var state) && state != ButtonState.Up)
                {
                    return state;
                }
            }

            // No known state, so must not be pressed.
            return ButtonState.Up;
        }

        private static bool CheckButton(MouseButton button, ButtonState state)
        {
            return GetButtonState(button).HasFlag(state);
        }

        public static bool IsMousePressed(MouseButton button)
        {
            return CheckButton(button, ButtonState.Pressed);
        }

        public static bool IsMouseReleased(MouseButton button)
        {
            return CheckButton(button, ButtonState.Released);
        }

        public static bool IsMouseDown(MouseButton button)
        {
            return CheckButton(button, ButtonState.Down);
        }

        public static bool IsMouseUp(MouseButton button)
        {
            return CheckButton(button, ButtonState.Up);
        }

        #endregion

#if false

        #region Gamepad

        public static bool IsGamepadSupported => false;

        public static int GamepadCount => 0;

        public static event Action<int> GamepadDisconnected;

        public static event Action<int> GamepadConnected;

        public static ButtonState GetButton(GamepadButton button, int gamepadIndex = 0)
        {
            throw new NotImplementedException();
        }

        public static bool CheckButton(GamepadButton button, ButtonState state, int gamepadIndex = 0)
        {
            return GetButton(button, gamepadIndex).HasFlag(state);
        }

        public static float GetAxis(GamepadAxis axis, int gamepadIndex = 0)
        {
            throw new NotImplementedException();
        }

        #endregion

#endif
    }
}
