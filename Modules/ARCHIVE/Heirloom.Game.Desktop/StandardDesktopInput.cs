using System;
using System.Collections.Generic;

using Heirloom.Platforms.Desktop;
using Heirloom.Drawing;
using Heirloom.Math;

namespace Heirloom.Game.Desktop
{
    internal sealed class StandardDesktopInput : InputSource
    {
        // event queues
        private readonly Queue<MouseMoveEvent> _mouseMoveEvents;
        private readonly Queue<MouseButtonEvent> _mouseButtonEvents;
        private readonly Queue<MouseScrollEvent> _mouseScrollEvents;
        private readonly Queue<KeyEvent> _keyboardEvents;
        private readonly Queue<CharacterEvent> _characterEvents;

        // active state
        private readonly HashSet<string> _buttonNames;
        private readonly Dictionary<string, ButtonState> _buttonState;
        private Vector _pointer, _pointerDelta, _scroll;

        #region Constructor 

        public StandardDesktopInput(Window window)
        {
            Window = window ?? throw new ArgumentNullException(nameof(window));

            // 
            _mouseScrollEvents = new Queue<MouseScrollEvent>();
            _mouseButtonEvents = new Queue<MouseButtonEvent>();
            _mouseMoveEvents = new Queue<MouseMoveEvent>();
            _keyboardEvents = new Queue<KeyEvent>();
            _characterEvents = new Queue<CharacterEvent>();

            // 
            _buttonState = new Dictionary<string, ButtonState>();
            _buttonNames = new HashSet<string>();

            // Subscribe to keyboard events
            Window.KeyPress += OnKeyEvent;
            Window.KeyRelease += OnKeyEvent;
            Window.CharacterTyped += OnCharEvent;

            // Subscribe to mouse events
            Window.MousePress += OnMouseButtonEvent;
            Window.MouseRelease += OnMouseButtonEvent;
            Window.MouseScroll += OnMouseScrollEvent;
            Window.MouseMove += OnMouseMoveEvent;
        }

        ~StandardDesktopInput()
        {
            // Unsubscribe from keyboard events
            Window.KeyPress -= OnKeyEvent;
            Window.KeyRelease -= OnKeyEvent;
            Window.CharacterTyped -= OnCharEvent;

            // Unsubscribe from mouse events
            Window.MousePress -= OnMouseButtonEvent;
            Window.MouseRelease -= OnMouseButtonEvent;
            Window.MouseScroll -= OnMouseScrollEvent;
            Window.MouseMove -= OnMouseMoveEvent;
        }

        #endregion

        public Window Window { get; }

        #region Event Handlers

        private void OnMouseMoveEvent(Window window, MouseMoveEvent ev)
        {
            _mouseMoveEvents.Enqueue(ev);
        }

        private void OnMouseScrollEvent(Window window, MouseScrollEvent ev)
        {
            _mouseScrollEvents.Enqueue(ev);
        }

        private void OnMouseButtonEvent(Window window, MouseButtonEvent ev)
        {
            _mouseButtonEvents.Enqueue(ev);
        }

        private void OnKeyEvent(Window window, KeyEvent ev)
        {
            _keyboardEvents.Enqueue(ev);
        }

        private void OnCharEvent(Window window, CharacterEvent ev)
        {
            _characterEvents.Enqueue(ev);
        }

        #endregion

        protected override bool TryGetPointerPosition(out Vector state)
        {
            state = _pointer;
            return true;
        }

        protected override bool TryGetButton(string identifier, out ButtonState state)
        {
            return _buttonState.TryGetValue(identifier, out state);
        }

        protected override bool TryGetAxis(string identifier, out float state)
        {
            if (identifier == "scroll_x") { state = _scroll.X; return true; }
            if (identifier == "scroll_y") { state = _scroll.Y; return true; }

            if (identifier == "mouse_x") { state = _pointer.X; return true; }
            if (identifier == "mouse_y") { state = _pointer.Y; return true; }

            if (identifier == "mouse_dx") { state = _pointerDelta.X; return true; }
            if (identifier == "mouse_dy") { state = _pointerDelta.Y; return true; }

            state = default;
            return false;
        }

        protected override void Poll()
        {
            // Remove the 'now' flag from known buttons, as that was last frame
            foreach (var name in _buttonNames)
            {
                _buttonState[name] &= ~ButtonState.Now;
            }

            ProcessMouseMoveEvents();
            ProcessMouseScrollEvents();
            ProcessMouseButtonEvents();
            ProcessCharacterEvents();
            ProcessKeyboardEvents();

            void ProcessMouseMoveEvents()
            {
                _pointerDelta = Vector.Zero;
                while (_mouseMoveEvents.Count > 0)
                {
                    var ev = _mouseMoveEvents.Dequeue();
                    _pointerDelta += ev.Position - _pointer;
                    _pointer = ev.Position;
                }
            }

            void ProcessMouseScrollEvents()
            {
                while (_mouseScrollEvents.Count > 0)
                {
                    var ev = _mouseScrollEvents.Dequeue();
                    _scroll = ev.Scroll;
                }
            }

            void ProcessKeyboardEvents()
            {
                // For each known keyboard event
                while (_keyboardEvents.Count > 0)
                {
                    var ev = _keyboardEvents.Dequeue();

                    // Generate button identifier
                    var name = ev.Key.ToString().ToSnakeCase();

                    _buttonNames.Add(name);

                    switch (ev.Action)
                    {
                        case ButtonAction.Press:
                            _buttonState[name] = ButtonState.Pressed;
                            break;

                        case ButtonAction.Release:
                            _buttonState[name] = ButtonState.Released;
                            break;
                    }
                }
            }

            void ProcessMouseButtonEvents()
            {
                while (_mouseButtonEvents.Count > 0)
                {
                    var ev = _mouseButtonEvents.Dequeue();

                    // Generate button identifier
                    var name = $"mouse_{GetName(ev.Button)}";
                    _buttonNames.Add(name);

                    switch (ev.Action)
                    {
                        case ButtonAction.Press:
                            _buttonState[name] = ButtonState.Pressed;
                            break;

                        case ButtonAction.Release:
                            _buttonState[name] = ButtonState.Released;
                            break;
                    }
                }

                static string GetName(int index)
                {
                    if (index == 0) { return "left"; }
                    else if (index == 1) { return "right"; }
                    else if (index == 2) { return "middle"; }
                    else if (index >= 3) { return $"extra{index - 3}"; }

                    // Shouldn't occur, but here anyway
                    return "unknown";
                }
            }

            void ProcessCharacterEvents()
            {
                // todo: provide a mechanism for character input
                // Just burn through the characters 
                while (_characterEvents.Count > 0)
                {
                    _characterEvents.Dequeue();
                }
            }
        }
    }
}
