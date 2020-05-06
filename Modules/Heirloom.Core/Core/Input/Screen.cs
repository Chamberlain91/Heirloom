using System;
using System.Collections.Generic;
using System.Linq;

namespace Heirloom
{
    /// <summary>
    /// An abstract representation of the screen (ie, window, view, etc).
    /// </summary>
    public abstract class Screen : IInputSource, IDisposable
    {
        private readonly InputBuffer _inputs;

        /// <summary>
        /// Constructs a new screen.
        /// </summary>
        protected Screen()
        {
            _inputs = new InputBuffer();

            // Register this screen for input
            Input.AddInputSource(this);
        }

        #region Properties

        /// <summary>
        /// Gets the graphics context that can draw on this screen.
        /// </summary>
        public GraphicsContext Graphics { get; protected set; }

        /// <summary>
        /// Gets surface that represents this screen.
        /// </summary>
        public Surface Surface { get; protected set; }

        /// <summary>
        /// Gets the size of the screen.
        /// </summary>
        public abstract IntSize Size { get; set; }

        /// <summary>
        /// Gets the width of the screen.
        /// </summary>
        public int Width
        {
            get => Size.Width;
            set => Size = new IntSize(value, Height);
        }

        /// <summary>
        /// Gets the height of the screen.
        /// </summary>
        public int Height
        {
            get => Size.Height;
            set => Size = new IntSize(Width, value);
        }

        /// <summary>
        /// Gets a value that determines if this screen has been closed.
        /// </summary>
        public bool IsClosed { get; protected set; }

        /// <summary>
        /// Gets a value that determines if this window been disposed.
        /// </summary>
        public bool IsDisposed { get; protected set; }

        #endregion

        #region Screen Events

        /// <summary>
        /// Event called when the focused state of this screen changes.
        /// </summary>
        public event Action<Screen, bool> FocusChanged;

        /// <summary>
        /// Event called when the content scaling of this screen changes.
        /// </summary>
        public event Action<Screen, Vector> ContentScaleChanged;

        /// <summary>
        /// Event called when the screen surface is resized.
        /// On certain platforms, the screen size and surface may not be equal.
        /// </summary>
        /// <seealso cref="ContentScaleChanged"/>
        /// <seealso cref="Surface"/>
        public event Action<Screen, IntSize> FramebufferResized;

        /// <summary>
        /// Event called when the screen is resized.
        /// On certain platforms, the screen size and surface may not be equal.
        /// </summary>
        /// <seealso cref="ContentScaleChanged"/>
        /// <seealso cref="Surface"/>
        public event Action<Screen, IntSize> Resized;

        /// <summary>
        /// Event called when the screen is trying to close.
        /// Returning <c>false</c> will prevent the screen from closing, if possible.
        /// </summary>
        public event Func<Screen, bool> Closing;

        /// <summary>
        /// Event called when the screen has closed.
        /// </summary>
        public event Action<Screen> Closed;

        /// <summary>
        /// Call this function raise the <see cref="FocusChanged"/> event.
        /// </summary>
        protected virtual void OnFocusChanged(bool e)
        {
            FocusChanged?.Invoke(this, e);
        }

        /// <summary>
        /// Call this function raise the <see cref="ContentScaleChanged"/> event.
        /// </summary>
        protected virtual void OnContentScaleChanged(Vector e)
        {
            ContentScaleChanged?.Invoke(this, e);
        }

        /// <summary>
        /// Call this function raise the <see cref="FramebufferResized"/> event.
        /// </summary>
        protected virtual void OnFramebufferResized(IntSize e)
        {
            Surface.SetSize(e);
            FramebufferResized?.Invoke(this, e);
        }

        /// <summary>
        /// Call this function raise the <see cref="Resized"/> event.
        /// </summary>
        protected virtual void OnResized(IntSize e)
        {
            Resized?.Invoke(this, e);
        }

        /// <summary>
        /// Call this function raise the <see cref="Closing"/> event.
        /// </summary>
        protected virtual bool OnClosing()
        {
            // note: defaults to true to allow the screen to close without handlers.
            return Closing?.Invoke(this) ?? true;
        }

        /// <summary>
        /// Call this function raise the <see cref="Closed"/> event.
        /// </summary>
        protected virtual void OnClosed()
        {
            Dispose();
        }

        #endregion

        #region Keyboard

        /// <summary>
        /// An event raised when a character has been typed on the keyboard.
        /// </summary>
        public event Action<Screen, CharacterEvent> CharacterTyped;

        /// <summary>
        /// An event raised when a key has been pressed on the keyboard.
        /// </summary>
        public event Action<Screen, KeyEvent> KeyPressed;

        /// <summary>
        /// An event raised when a key has been released on the keyboard.
        /// </summary>
        public event Action<Screen, KeyEvent> KeyReleased;

        /// <summary>
        /// An event raised when a key has been 'repeated' on the keyboard.
        /// </summary>
        /// <remarks>
        /// This occurs when holding the key for an extended time.
        /// </remarks>
        public event Action<Screen, KeyEvent> KeyRepeat;

        /// <inheritdoc/>
        public abstract bool SupportsSoftwareKeyboard { get; }

        /// <inheritdoc/>
        public abstract void ShowSoftwareKeyboard();

        /// <inheritdoc/>
        public abstract void HideSoftwareKeyboard();

        bool IInputSource.TryGetKey(Key key, out ButtonState state)
        {
            state = _inputs.GetKey(key);
            return state != ButtonState.Up;
        }

        /// <summary>
        /// Call this function raise the <see cref="KeyPressed"/> event.
        /// </summary>
        protected virtual void OnKeyPressed(KeyEvent e)
        {
            lock (_inputs) { _inputs.KeyEvents.Enqueue(e); }
            KeyPressed?.Invoke(this, e);
        }

        /// <summary>
        /// Call this function raise the <see cref="KeyReleased"/> event.
        /// </summary>
        protected virtual void OnKeyReleased(KeyEvent e)
        {
            lock (_inputs) { _inputs.KeyEvents.Enqueue(e); }
            KeyReleased?.Invoke(this, e);
        }

        /// <summary>
        /// Call this function raise the <see cref="KeyRepeat"/> event.
        /// </summary>
        protected virtual void OnKeyRepeat(KeyEvent e)
        {
            lock (_inputs) { _inputs.KeyEvents.Enqueue(e); }
            KeyRepeat?.Invoke(this, e);
        }

        /// <summary>
        /// Call this function raise the <see cref="CharacterTyped"/> event.
        /// </summary>
        protected virtual void OnCharacterTyped(CharacterEvent e)
        {
            lock (_inputs) { _inputs.CharacterEvents.Enqueue(e); }
            CharacterTyped?.Invoke(this, e);
        }

        #endregion

        #region Mouse

        /// <summary>
        /// An event raised when a mouse button has been pressed.
        /// </summary>
        public event Action<Screen, MouseButtonEvent> MousePressed;

        /// <summary>
        /// An event raised when a mouse button has been released.
        /// </summary>
        public event Action<Screen, MouseButtonEvent> MouseReleased;

        /// <summary>
        /// An event raised when the user scrolls the mouse wheel.
        /// </summary>
        public event Action<Screen, MouseScrollEvent> MouseScrolled;

        /// <summary>
        /// An event raised when the user moves the mouse.
        /// </summary>
        public event Action<Screen, MouseMoveEvent> MouseMoved;

        bool IInputSource.TryGetButton(MouseButton button, out ButtonState state)
        {
            state = _inputs.GetButton(button);
            return state != ButtonState.Up;
        }

        /// <summary>
        /// Call this function raise the <see cref="MousePressed"/> event.
        /// </summary>
        protected virtual void OnMousePressed(MouseButtonEvent e)
        {
            lock (_inputs) { _inputs.MouseButtonEvents.Enqueue(e); }
            MousePressed?.Invoke(this, e);
        }

        /// <summary>
        /// Call this function raise the <see cref="MouseReleased"/> event.
        /// </summary>
        protected virtual void OnMouseReleased(MouseButtonEvent e)
        {
            lock (_inputs) { _inputs.MouseButtonEvents.Enqueue(e); }
            MouseReleased?.Invoke(this, e);
        }

        /// <summary>
        /// Call this function raise the <see cref="MouseScrolled"/> event.
        /// </summary>
        protected virtual void OnMouseScrolled(MouseScrollEvent e)
        {
            lock (_inputs) { _inputs.MouseScrollEvents.Enqueue(e); }
            MouseScrolled?.Invoke(this, e);
        }

        /// <summary>
        /// Call this function raise the <see cref="MouseMoved"/> event.
        /// </summary>
        protected virtual void OnMouseMoved(MouseMoveEvent e)
        {
            lock (_inputs) { _inputs.MouseMoveEvents.Enqueue(e); }
            MouseMoved?.Invoke(this, e);
        }

        #endregion

        /// <summary>
        /// Refresh the screen, presenting rendered graphics.
        /// </summary>
        public virtual void Refresh()
        {
            lock (_inputs) { _inputs.ProcessEvents(); }
            Graphics.EndFrame();
        }

        /// <summary>
        /// Attempts to close this screen.
        /// </summary>
        public abstract void Close();

        #region Dispose

        /// <summary>
        /// Disposes this screen, freeing any unmanaged resources.
        /// </summary>
        protected virtual void Dispose(bool disposeManaged)
        {
            if (!IsClosed)
            {
                IsClosed = true;
                Closed?.Invoke(this);
            }

            if (!IsDisposed)
            {
                IsDisposed = true;

                Input.RemoveInputSource(this);
                Graphics.Dispose();
            }
        }

        /// <summary>
        /// Dispose this screen, freeing any unmanaged resources.
        /// </summary>
        public void Dispose()
        {
            Dispose(true);
            GC.SuppressFinalize(this);
        }

        #endregion

        /// <summary>
        /// Buffers input for <see cref="IInputSource"/> queries by <see cref="Input"/>.
        /// </summary>
        private sealed class InputBuffer
        {
            public readonly Queue<MouseButtonEvent> MouseButtonEvents = new Queue<MouseButtonEvent>();
            public readonly Queue<MouseScrollEvent> MouseScrollEvents = new Queue<MouseScrollEvent>();
            public readonly Queue<MouseMoveEvent> MouseMoveEvents = new Queue<MouseMoveEvent>();

            public readonly Queue<CharacterEvent> CharacterEvents = new Queue<CharacterEvent>();
            public readonly Queue<KeyEvent> KeyEvents = new Queue<KeyEvent>();

            private readonly Dictionary<MouseButton, ButtonState> _mouseStates = new Dictionary<MouseButton, ButtonState>();
            private readonly Dictionary<Key, ButtonState> _keyStates = new Dictionary<Key, ButtonState>();

            private Vector _mouseScroll;
            private Vector _mousePosition;
            private Vector _mouseDelta;

            internal void ProcessEvents()
            {
                ProcessMouseEvents();
                ProcessKeyEvents();

                void ProcessMouseEvents()
                {
                    RemoveRecentFlag(_mouseStates);

                    while (MouseButtonEvents.Count > 0)
                    {
                        var ev = MouseButtonEvents.Dequeue();

                        // If the event is reporting a new press (ie, 'now')
                        if (ev.State.HasFlag(ButtonState.Now))
                        {
                            _mouseStates[ev.Button] = ev.State;
                        }
                    }

                    _mouseScroll = Vector.Zero;
                    _mouseDelta = Vector.Zero;

                    while (MouseScrollEvents.Count > 0)
                    {
                        var ev = MouseScrollEvents.Dequeue();
                        _mouseScroll += ev.Scroll;
                    }

                    while (MouseMoveEvents.Count > 0)
                    {
                        var ev = MouseMoveEvents.Dequeue();
                        _mousePosition = ev.Position;
                        _mouseDelta += ev.Delta;
                    }

                    // 
                    Input.UpdateMouse(_mousePosition, _mouseDelta);
                }

                void ProcessKeyEvents()
                {
                    RemoveRecentFlag(_keyStates);

                    while (KeyEvents.Count > 0)
                    {
                        var ev = KeyEvents.Dequeue();

                        // If the event is reporting a new press (ie, 'now')
                        if (ev.State.HasFlag(ButtonState.Now))
                        {
                            _keyStates[ev.Key] = ev.State;
                        }
                    }
                }
            }

            public ButtonState GetKey(Key key)
            {
                // Try to get state for button
                if (_keyStates.TryGetValue(key, out var state))
                {
                    return state;
                }

                // Button must have never been pressed, thus is up.
                return ButtonState.Up;
            }

            public Vector MouseScroll => _mouseScroll;

            public Vector MousePosition => _mousePosition;

            public Vector MouseDelta => _mouseDelta;

            public ButtonState GetButton(MouseButton button)
            {
                // Try to get state for button
                if (_mouseStates.TryGetValue(button, out var state))
                {
                    return state;
                }

                // Button must have never been pressed, thus is up.
                return ButtonState.Up;
            }

            private static void RemoveRecentFlag<T>(Dictionary<T, ButtonState> states)
            {
                foreach (var key in states.Keys.ToArray())
                {
                    var state = states[key];
                    if (state.HasFlag(ButtonState.Now))
                    {
                        states[key] = state & ~ButtonState.Now;
                    }
                }
            }
        }
    }
}
