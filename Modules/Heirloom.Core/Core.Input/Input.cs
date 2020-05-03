using System;
using System.Collections.Generic;
using System.Linq;

namespace Heirloom
{
    /// <summary>
    /// Provides a centralized query style input layer.
    /// This is useful for implementing games.
    /// </summary>
    /// <example>
    /// The following is an example checking for button on the keyboard:
    /// <code>
    /// if(Input.CheckButton(Key.A, ButtonState.Pressed)) {
    ///     Log.Info("Pressed A Key");
    /// }
    /// </code>
    /// </example>
    public static class Input
    {
        private static readonly HashSet<IInputSource> _sources;
        private static Vector _mousePosition;
        private static Vector _mouseDelta;

        static Input()
        {
            _sources = new HashSet<IInputSource>();
        }

        public static event Action<Screen, CharacterEvent> CharacterTyped;

        public static event Action<Screen, KeyEvent> KeyPressed;

        public static event Action<Screen, KeyEvent> KeyReleased;

        public static event Action<Screen, KeyEvent> KeyRepeat;

        public static event Action<Screen, MouseButtonEvent> MousePressed;

        public static event Action<Screen, MouseButtonEvent> MouseReleased;

        public static event Action<Screen, MouseScrollEvent> MouseScrolled;

        public static event Action<Screen, MouseMoveEvent> MouseMoved;

        #region Add/Remove Sources

        /// <summary>
        /// Adds and begins tracking input from an input source.
        /// </summary>
        public static void AddInputSource(IInputSource source)
        {
            if (_sources.Add(source))
            {
                // Register Events
                source.CharacterTyped += OnCharacterTyped;
                source.KeyPressed += OnKeyPressed;
                source.KeyReleased += OnKeyReleased;
                source.KeyRepeat += OnKeyRepeat;
                source.MousePressed += OnMousePressed;
                source.MouseReleased += OnMouseReleased;
                source.MouseScrolled += OnMouseScrolled;
                source.MouseMoved += OnMouseMoved;
            }
        }

        /// <summary>
        /// Removes and tops tracking input from an input source.
        /// </summary>
        public static void RemoveInputSource(IInputSource source)
        {
            if (_sources.Remove(source))
            {
                // Remove Events
                source.CharacterTyped -= OnCharacterTyped;
                source.KeyPressed -= OnKeyPressed;
                source.KeyReleased -= OnKeyReleased;
                source.KeyRepeat -= OnKeyRepeat;
                source.MousePressed -= OnMousePressed;
                source.MouseReleased -= OnMouseReleased;
                source.MouseScrolled -= OnMouseScrolled;
                source.MouseMoved -= OnMouseMoved;
            }
        }

        #endregion

        #region Keyboard

        /// <summary>
        /// Is the keyboard emulated by software? (ie mobile deviecs)
        /// </summary>
        public static bool SupportsSoftwareKeyboard => _sources.Any(s => s.SupportsSoftwareKeyboard);

        /// <summary>
        /// Gets the latest state of a button the keyboard.
        /// </summary>
        /// <param name="key">Some key.</param>
        public static ButtonState GetButton(Key key)
        {
            foreach (var source in _sources)
            {
                // Return on the first non key up state.
                if (source.TryGetKey(key, out var state) && state != ButtonState.Up)
                {
                    return state;
                }
            }

            // No known state, so must not be pressed.
            return ButtonState.Up;
        }

        /// <summary>
        /// Checks if the lastest state of a button on the keyboard matcheas the desired state.
        /// </summary>
        /// <param name="key">Some key.</param>
        /// <param name="state">Some desired comparison state.</param>
        public static bool CheckButton(Key key, ButtonState state)
        {
            return GetButton(key).HasFlag(state);
        }

        /// <summary>
        /// Shows the software keyboard.
        /// </summary>
        public static void ShowSoftKeyboard()
        {
            if (SupportsSoftwareKeyboard)
            {
                var source = _sources.First(s => s.SupportsSoftwareKeyboard);
                source.ShowSoftwareKeyboard();
            }
        }

        /// <summary>
        /// Hides the software keyboard.
        /// </summary>
        public static void HideSoftKeyboard()
        {
            if (SupportsSoftwareKeyboard)
            {
                var source = _sources.First(s => s.SupportsSoftwareKeyboard);
                source.HideSoftwareKeyboard();
            }
        }

        #endregion

        #region Mouse

        /// <summary>
        /// Gets the latest mouse position.
        /// </summary>
        public static Vector MousePosition => _mousePosition;

        /// <summary>
        /// Gets the latest mouse position delta.
        /// </summary>
        public static Vector MouseDelta => _mouseDelta;

        /// <summary>
        /// Gets the latest state of a mouse button.
        /// </summary>
        /// <param name="button">Some button.</param>
        public static ButtonState GetButton(MouseButton button)
        {
            foreach (var source in _sources)
            {
                // Return on the first non button up state.
                if (source.TryGetButton(button, out var state) && state != ButtonState.Up)
                {
                    return state;
                }
            }

            // No known state, so must not be pressed.
            return ButtonState.Up;
        }

        /// <summary>
        /// Checks if the lastest state of a mouse button matcheas the desired state.
        /// </summary>
        /// <param name="button">Some button.</param>
        /// <param name="state">Some desired comparison state.</param>
        public static bool CheckMouse(MouseButton button, ButtonState state)
        {
            return GetButton(button).HasFlag(state);
        }

        internal static void UpdateMouse(Vector position, Vector delta)
        {
            _mousePosition = position;
            _mouseDelta = delta;
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

        #region Event Invocations

        private static void OnCharacterTyped(Screen s, CharacterEvent e)
        {
            CharacterTyped?.Invoke(s, e);
        }

        private static void OnKeyPressed(Screen s, KeyEvent e)
        {
            KeyPressed?.Invoke(s, e);
        }

        private static void OnKeyReleased(Screen s, KeyEvent e)
        {
            KeyReleased?.Invoke(s, e);
        }

        private static void OnKeyRepeat(Screen s, KeyEvent e)
        {
            KeyRepeat?.Invoke(s, e);
        }

        private static void OnMouseReleased(Screen s, MouseButtonEvent e)
        {
            MouseReleased?.Invoke(s, e);
        }

        private static void OnMousePressed(Screen s, MouseButtonEvent e)
        {
            MousePressed?.Invoke(s, e);
        }

        private static void OnMouseScrolled(Screen s, MouseScrollEvent e)
        {
            MouseScrolled?.Invoke(s, e);
        }

        private static void OnMouseMoved(Screen s, MouseMoveEvent e)
        {
            MouseMoved?.Invoke(s, e);
        }

        #endregion
    }
}
