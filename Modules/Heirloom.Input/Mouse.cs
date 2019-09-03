using System;

namespace Heirloom.Input
{
    public abstract class Mouse
    {
        public event EventHandler<MouseMoveEventArgs> Moved;

        public event EventHandler<MouseScrollEventArgs> Scroll;

        public event EventHandler<MouseButtonEventArgs> ButtonDown;

        public event EventHandler<MouseButtonEventArgs> ButtonUp;

        protected virtual void OnScroll(float x, float y)
        {
            var args = new MouseScrollEventArgs(x, y);
            Scroll?.Invoke(this, args);
        }

        protected virtual void OnMoved(float x, float y)
        {
            var args = new MouseMoveEventArgs(x, y);
            Moved?.Invoke(this, args);
        }

        protected virtual void OnButton(MouseButton button, KeyModifiers modifiers, bool isDown)
        {
            var args = new MouseButtonEventArgs(button, modifiers, isDown);
            if (isDown) { ButtonDown?.Invoke(this, args); }
            else { ButtonUp?.Invoke(this, args); }
        }

        /// <summary>
        /// A hollow implementation that always reports zero, disconnected, etc.
        /// </summary>
        public static Mouse Null { get; } = new DummyMouse();

        private sealed class DummyMouse : Mouse { }
    }
}
