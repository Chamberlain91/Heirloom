using System;

using Heirloom.GLFW;
using Heirloom.Input;

namespace Heirloom.Platforms.Desktop.Input
{
    internal sealed class MouseDevice : Mouse
    {
        private Glfw.CursorPositionCallback _positionCallback;
        private Glfw.MouseButtonCallback _buttonCallback;
        private Glfw.ScrollCallback _scrollCallback;

        internal MouseDevice(Glfw.Window window)
        {
            ContextManager.Invoke(() =>
            {
                // Mouse moved
                Glfw.SetCursorPosCallback(window, _positionCallback = (_, x, y) =>
                {
                    OnMoved((float) x, (float) y);
                });

                // Mouse wheel
                Glfw.SetScrollCallback(window, _scrollCallback = (_, x, y) =>
                {
                    OnScroll((float) x, (float) y);
                });

                // Mouse wheel
                Glfw.SetMouseButtonCallback(window, _buttonCallback = (_, btn, action, mods) =>
                {
                    OnButton((MouseButton) btn, (KeyModifiers) mods, action == Glfw.Press);
                });
            });
        }

        ~MouseDevice()
        {
            GC.KeepAlive(_buttonCallback);
            GC.KeepAlive(_positionCallback);
            GC.KeepAlive(_scrollCallback);
        }
    }
}
