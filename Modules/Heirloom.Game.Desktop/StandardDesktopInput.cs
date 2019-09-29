using System;
using System.Collections.Generic;

using Heirloom.Desktop;
using Heirloom.Extras;
using Heirloom.GLFW;
using Heirloom.Math;

namespace Heirloom.Game.Desktop
{
    public sealed class StandardDesktopInput : InputSource
    {
        private readonly Queue<MouseMoveEvent> _mouseMoveEvents;
        private readonly Queue<MouseButtonEvent> _mouseButtonEvents;
        private readonly Queue<KeyboardEvent> _keyboardEvents;
        private readonly Queue<CharEvent> _characterEvents;

        private readonly HashSet<string> _buttonNames;
        private readonly Dictionary<string, ButtonState> _buttonState;
        private Vector _pointer;

        public StandardDesktopInput(Window window)
        {
            Window = window ?? throw new ArgumentNullException(nameof(window));

            // 
            _mouseButtonEvents = new Queue<MouseButtonEvent>();
            _mouseMoveEvents = new Queue<MouseMoveEvent>();
            _keyboardEvents = new Queue<KeyboardEvent>();
            _characterEvents = new Queue<CharEvent>();

            // 
            _buttonState = new Dictionary<string, ButtonState>();
            _buttonNames = new HashSet<string>();

            // Subscribe to keyboard events
            window.KeyPress += OnKeyEvent;
            window.KeyRelease += OnKeyEvent;
            window.CharTyped += OnCharEvent;

            // Subscribe to mouse events
            window.MousePress += OnMouseButtonEvent;
            window.MouseRelease += OnMouseButtonEvent;
            window.MouseMove += OnMouseMoveEvent;
        }

        ~StandardDesktopInput()
        {
            // Unsubscribe from keyboard events
            Window.KeyPress -= OnKeyEvent;
            Window.KeyRelease -= OnKeyEvent;
            Window.CharTyped -= OnCharEvent;

            // Unsubscribe from mouse events
            Window.MousePress -= OnMouseButtonEvent;
            Window.MouseRelease -= OnMouseButtonEvent;
            Window.MouseMove -= OnMouseMoveEvent;
        }

        public Window Window { get; }

        private void OnMouseMoveEvent(MouseMoveEvent ev)
        {
            _mouseMoveEvents.Enqueue(ev);
        }

        private void OnMouseButtonEvent(MouseButtonEvent ev)
        {
            _mouseButtonEvents.Enqueue(ev);
        }

        private void OnKeyEvent(KeyboardEvent ev)
        {
            _keyboardEvents.Enqueue(ev);
        }

        private void OnCharEvent(CharEvent ev)
        {
            _characterEvents.Enqueue(ev);
        }

        protected override bool TryGetPointer(out Vector state)
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
            ProcessMouseButtonEvents();
            ProcessCharacterEvents();
            ProcessKeyboardEvents();
        }

        private void ProcessKeyboardEvents()
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

        private void ProcessMouseButtonEvents()
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

            string GetName(int index)
            {
                if (index == 0) { return "left"; }
                else if (index == 1) { return "right"; }
                else if (index == 2) { return "middle"; }
                else if (index >= 3) { return $"extra{index - 3}"; }

                // Shouldn't occur, but here anyway
                return "unknown";
            }
        }

        private void ProcessMouseMoveEvents()
        {
            var delta = Vector.Zero;
            while (_mouseMoveEvents.Count > 0)
            {
                var ev = _mouseMoveEvents.Dequeue();
                delta += ev.Position - _pointer;
                _pointer = ev.Position;
            }
        }

        private void ProcessCharacterEvents()
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
