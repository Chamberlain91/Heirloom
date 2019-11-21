using System;

using Heirloom.Drawing;
using Heirloom.GLFW;
using Heirloom.Math;

namespace Heirloom.Desktop
{
    public interface IWindow : IDisposable
    {
        Graphics Graphics { get; }

        bool IsClosed { get; }
        bool IsDecorated { get; set; }
        bool IsDisposed { get; }
        bool IsFloating { get; set; }
        bool IsResizable { get; set; }
        bool IsVisible { get; }

        string Title { get; set; }
        IntVector Position { get; set; }
        IntRectangle Bounds { get; set; }
        IntSize FramebufferSize { get; }
        IntSize Size { get; set; }
        Vector ContentScale { get; }

        WindowState State { get; }

        MultisampleQuality Multisample { get; }
        bool Transparent { get; }
        bool VSync { get; }

        event Func<Window, bool> Closing;
        event Action<Window> Closed;

        event Action<Window, ContentScaleEvent> ContentScaleChanged;
        event Action<Window> FramebufferResized;
        event Action<Window> Resized;

        event Action<Window, KeyboardEvent> KeyPress;
        event Action<Window, KeyboardEvent> KeyRelease;
        event Action<Window, KeyboardEvent> KeyRepeat;
        event Action<Window, CharEvent> CharTyped;

        event Action<Window, MouseMoveEvent> MouseMove;
        event Action<Window, MouseButtonEvent> MousePress;
        event Action<Window, MouseButtonEvent> MouseRelease;
        event Action<Window, MouseScrollEvent> MouseScroll;

        void Show();
        void Hide();
        void Focus();
        void Close();

        void Maximize();
        void Minimize();
        void Restore();

        void MoveToCenter();
        void MoveToCenter(Monitor monitor);

        Monitor GetNearestMonitor();

        void SetFullscreen();
        void SetFullscreen(Monitor monitor);
        void SetFullscreen(Monitor monitor, VideoMode mode);
        void SetFullscreen(VideoMode mode);
    }
}
