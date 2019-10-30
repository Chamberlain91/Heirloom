using System;
using System.Threading;
using Heirloom.Drawing;
using Heirloom.Drawing.OpenGLES;
using Heirloom.GLFW;
using Heirloom.Math;

namespace Heirloom.Desktop
{
    public class Window : IDisposable
    {
        private IntRectangle _bounds, _restoreBounds;
        private IntSize _framebufferSize;
        private Vector _contentScale;
        private string _title;

        private Vector _mousePosition;
        private Vector _mouseDelta;

        private readonly WindowCloseCallback _windowCloseCallback;
        private readonly WindowPositionCallback _windowPositionCallback;
        private readonly FramebufferSizeCallback _framebufferSizeCallback;
        private readonly WindowContentScaleCallback _windowContentScaleCallback;
        private readonly WindowSizeCallback _windowSizeCallback;

        private readonly CharCallback _charCallback;
        private readonly KeyCallback _keyCallback;

        private readonly CursorPositionCallback _cursorPositionCallback;
        private readonly MouseButtonCallback _mouseButtonCallback;
        private readonly ScrollCallback _scrollCallback;

        #region Constructors

        /// <summary>
        /// Constructs a new window with default settings.
        /// </summary>
        public Window(string title)
            : this(title, WindowCreationSettings.Default)
        { }

        /// <summary>
        /// Constructs a new window with specified multisample quality and otherwise default settings.
        /// </summary>
        public Window(string title, MultisampleQuality multisample)
            : this(title, new WindowCreationSettings { Multisample = multisample })
        { }

        /// <summary>
        /// Constructs a new window with the specified settings.
        /// </summary>
        public Window(string title, WindowCreationSettings settings)
        {
            // Watch window
            Application.AddWindow(this);

            // 
            WindowCreationSettings.FillDefaults(ref settings);

            // 
            Transparent = settings.UseTransparentFramebuffer.Value && Application.SupportsTransparentFramebuffer;
            Multisample = settings.Multisample.Value;
            VSync = settings.VSync.Value;

            // 
            WindowHandle = Application.Invoke(() =>
            {
                // 
                Glfw.SetWindowCreationHint(WindowCreationHint.Samples, (int) Multisample);

                // Create window
                Glfw.SetWindowCreationHint(WindowAttribute.TransparentFramebuffer, Transparent);
                var handle = Glfw.CreateWindow(settings.Size.Value.Width, settings.Size.Value.Height, title, MonitorHandle.None, Application.ShareContext);

                // Query intial window properties
                Glfw.GetWindowSize(handle, out _bounds.Width, out _bounds.Height);
                Glfw.GetWindowPosition(handle, out _bounds.X, out _bounds.Y);
                Glfw.GetFramebufferSize(handle, out _framebufferSize.Width, out _framebufferSize.Height);

                // Return window handle
                return handle;
            });

            // Set if the window is resizable (must happen after window is created)
            IsResizable = settings.IsResizable.Value;

            // 
            _title = title;

            // == Bind Callbacks

            Glfw.SetWindowCloseCallback(WindowHandle, _windowCloseCallback = _ =>
            {
                IsClosed = OnClosing();
                Glfw.SetWindowShouldClose(WindowHandle, IsClosed);

                if (IsClosed)
                {
                    OnClosed();
                    Dispose();
                }
            });

            Glfw.SetFramebufferSizeCallback(WindowHandle, _framebufferSizeCallback = (_, w, h) =>
            {
                _framebufferSize = (w, h);
                OnFramebufferResized(w, h);
            });

            Glfw.SetWindowSizeCallback(WindowHandle, _windowSizeCallback = (_, w, h) =>
            {
                _bounds.Size = (w, h);
                OnWindowResized(w, h);
            });

            Glfw.SetWindowPositionCallback(WindowHandle, _windowPositionCallback = (_, x, y) =>
            {
                _bounds.Position = (x, y);
                OnWindowMoved(x, y);
            });

            Glfw.SetWindowContentScaleCallback(WindowHandle, _windowContentScaleCallback = (_, xs, ys) =>
            {
                _contentScale = (xs, ys);
                OnContentScaleChanged(xs, ys);
            });

            // Key callbacks
            Glfw.SetCharCallback(WindowHandle, _charCallback = (_, cp) => OnCharTyped((UnicodeCharacter) cp));
            Glfw.SetKeyCallback(WindowHandle, _keyCallback = (_, k, c, a, m) => OnKeyPressed(k, c, a, m));

            // Mouse callbacks
            Glfw.SetCursorPositionCallback(WindowHandle, _cursorPositionCallback = (_, x, y) => OnMouseMove((float) x, (float) y));
            Glfw.SetMouseButtonCallback(WindowHandle, _mouseButtonCallback = (_, b, a, m) => OnMousePressed(b, a, m));
            Glfw.SetScrollCallback(WindowHandle, _scrollCallback = (_, x, y) => OnMouseScroll((float) x, (float) y));

            // == Construct Render Context

            RenderContext = new WindowRenderContext(this);
        }

        ~Window()
        {
            Dispose(false);

            // 
            GC.KeepAlive(_windowCloseCallback);
            GC.KeepAlive(_windowPositionCallback);
            GC.KeepAlive(_framebufferSizeCallback);
            GC.KeepAlive(_windowSizeCallback);

            GC.KeepAlive(_charCallback);
            GC.KeepAlive(_keyCallback);

            GC.KeepAlive(_cursorPositionCallback);
            GC.KeepAlive(_mouseButtonCallback);
            GC.KeepAlive(_scrollCallback);
        }

        #endregion

        #region Properties

        internal WindowHandle WindowHandle { get; private set; }

        /// <summary>
        /// Has this window been disposed?
        /// </summary>
        public bool IsDisposed { get; private set; }

        /// <summary>
        /// Has this window been closed?
        /// </summary>
        public bool IsClosed { get; private set; }

        /// <summary>
        /// Is vsync enabled on this window?
        /// </summary>
        public bool VSync { get; private set; }

        /// <summary>
        /// Does this window have a transparent framebuffer?
        /// </summary>
        public bool Transparent { get; private set; }

        /// <summary>
        /// The multisampling level configured on this window.
        /// </summary>
        public MultisampleQuality Multisample { get; private set; }

        /// <summary>
        /// The render context for drawing on this window.
        /// </summary>
        public RenderContext RenderContext { get; }

        /// <summary>
        /// Is the window visible?
        /// </summary>
        public bool IsVisible => Application.Invoke(() => Glfw.GetWindowAttribute(WindowHandle, WindowAttribute.Visible) != 0);

        /// <summary>
        /// Is the window decorated? (ie, window chrome)
        /// </summary>
        public bool IsDecorated
        {
            get => Application.Invoke(() => Glfw.GetWindowAttribute(WindowHandle, WindowAttribute.Decorated) != 0);
            set => Application.Invoke(() => Glfw.SetWindowAttribute(WindowHandle, WindowAttribute.Decorated, value));
        }

        /// <summary>
        /// Can the window be resized?
        /// </summary>
        public bool IsResizable
        {
            get => Application.Invoke(() => Glfw.GetWindowAttribute(WindowHandle, WindowAttribute.Resizable) != 0);
            set => Application.Invoke(() => Glfw.SetWindowAttribute(WindowHandle, WindowAttribute.Resizable, value));
        }

        /// <summary>
        /// Is the window "always on top"?
        /// </summary>
        public bool IsFloating
        {
            get => Application.Invoke(() => Glfw.GetWindowAttribute(WindowHandle, WindowAttribute.Floating) != 0);
            set => Application.Invoke(() => Glfw.SetWindowAttribute(WindowHandle, WindowAttribute.Floating, value));
        }

        /// <summary>
        /// Get or set the window title text.
        /// </summary>
        public string Title
        {
            get => _title;

            set => Application.Invoke(() =>
            {
                Glfw.SetWindowTitle(WindowHandle, value);
                _title = value;
            });
        }

        /// <summary>
        /// Gets or sets the window bounds in screen units.
        /// </summary>
        public IntRectangle Bounds
        {
            get => _bounds;

            set => Application.Invoke(() =>
            {
                Glfw.SetWindowSize(WindowHandle, value.Width, value.Height);
                Glfw.SetWindowPosition(WindowHandle, value.X, value.Y);
                _bounds = value;
            });
        }

        /// <summary>
        /// Gets or sets the window position in screen coordinates.
        /// </summary>
        public IntVector Position
        {
            get => Bounds.Position;

            set => Application.Invoke(() =>
            {
                Glfw.SetWindowPosition(WindowHandle, value.X, value.Y);
                _bounds.Position = value;
            });
        }

        /// <summary>
        /// Gets or sets the window size in screen units.
        /// </summary>
        public IntSize Size
        {
            get => Bounds.Size;

            set => Application.Invoke(() =>
            {
                Glfw.SetWindowSize(WindowHandle, value.Width, value.Height);
                _bounds.Size = value;
            });
        }

        /// <summary>
        /// The size of the underlying framebuffer in pixels.
        /// </summary>
        public IntSize FramebufferSize => _framebufferSize;

        /// <summary>
        /// Gets the content scaling factor.
        /// </summary>
        public Vector ContentScale => _contentScale;

        /// <summary>
        /// Gets the current state of the window.
        /// </summary>
        public WindowState State => Application.Invoke(() =>
        {
            // Is the window in an iconified state?
            if (Glfw.GetWindowAttribute(WindowHandle, WindowAttribute.Iconified) != 0)
            {
                return WindowState.Minimized;
            }
            // Is the window in a maximized state?
            else if (Glfw.GetWindowAttribute(WindowHandle, WindowAttribute.Maximized) != 0)
            {
                // Window was maximized
                return WindowState.Maximized;
            }
            // Is the window in a maximized state?
            else if (Glfw.GetWindowMonitor(WindowHandle) != MonitorHandle.None)
            {
                // Window is fullscreen
                return WindowState.Fullscreen;
            }
            else
            {
                // Window was not in a specific state
                return WindowState.Normal;
            }
        });

        #endregion

        #region Events

        public event Action<Window> Resized;

        public event Action<Window> FramebufferResized;

        public event Action<Window, ContentScaleEvent> ContentScaleChanged;

        public event Action<Window, KeyboardEvent> KeyPress;

        public event Action<Window, KeyboardEvent> KeyRelease;

        public event Action<Window, KeyboardEvent> KeyRepeat;

        public event Action<Window, CharEvent> CharTyped;

        public event Action<Window, MouseButtonEvent> MousePress;

        public event Action<Window, MouseButtonEvent> MouseRelease;

        public event Action<Window, MouseScrollEvent> MouseScroll;

        public event Action<Window, MouseMoveEvent> MouseMove;

        public event Func<Window, bool> Closing;

        public event Action<Window> Closed;

        #endregion

        #region OnEvents

        protected virtual void OnWindowResized(int w, int h)
        {
            Resized?.Invoke(this);
        }

        protected virtual void OnFramebufferResized(int w, int h)
        {
            FramebufferResized?.Invoke(this);
        }

        protected virtual void OnContentScaleChanged(float xScale, float yScale)
        {
            var ev = new ContentScaleEvent(xScale, yScale);
            ContentScaleChanged?.Invoke(this, ev);
        }

        protected virtual void OnWindowMoved(int x, int y)
        {
            // Does nothing by default
        }

        protected virtual void OnKeyPressed(Key key, int scanCode, ButtonAction action, KeyModifiers modifiers)
        {
            var ev = new KeyboardEvent(key, scanCode, action, modifiers);

            switch (action)
            {
                default:
                    throw new InvalidOperationException("Encountered illegal key action");

                case ButtonAction.Press:
                    KeyPress?.Invoke(this, ev);
                    break;

                case ButtonAction.Release:
                    KeyRelease?.Invoke(this, ev);
                    break;

                case ButtonAction.Repeat:
                    KeyRepeat?.Invoke(this, ev);
                    break;
            }
        }

        protected virtual void OnCharTyped(UnicodeCharacter character)
        {
            var ev = new CharEvent(character);
            CharTyped?.Invoke(this, ev);
        }

        protected virtual void OnMousePressed(int button, ButtonAction action, KeyModifiers modifiers)
        {
            var ev = new MouseButtonEvent(button, action, modifiers, _mousePosition);
            switch (action)
            {
                default:
                    throw new InvalidOperationException("Encountered illegal mosue button action");

                case ButtonAction.Press:
                    MousePress?.Invoke(this, ev);
                    break;

                case ButtonAction.Release:
                    MouseRelease?.Invoke(this, ev);
                    break;
            }
        }

        protected virtual void OnMouseMove(float x, float y)
        {
            // Compute delta motion
            var prevPosition = _mousePosition;
            _mousePosition.Set(x, y);
            _mouseDelta = _mousePosition - prevPosition;

            var ev = new MouseMoveEvent(_mousePosition, _mouseDelta);
            MouseMove?.Invoke(this, ev);
        }

        protected virtual void OnMouseScroll(float x, float y)
        {
            var ev = new MouseScrollEvent(x, y);
            MouseScroll?.Invoke(this, ev);
        }

        protected virtual bool OnClosing()
        {
            // note: returns true to allow the window to close by default
            return Closing?.Invoke(this) ?? true;
        }

        protected virtual void OnClosed()
        {
            Closed?.Invoke(this);
        }

        #endregion

        #region Window State (Show, Maximize, etc)

        public void Show()
        {
            Application.Invoke(() => Glfw.ShowWindow(WindowHandle));
        }

        public void Hide()
        {
            Application.Invoke(() => Glfw.HideWindow(WindowHandle));
        }

        public void Close()
        {
            OnClosed();
            Dispose();
        }

        public void Focus()
        {
            Application.Invoke(() => Glfw.FocusWindow(WindowHandle));
        }

        /// <summary>
        /// Sets the window to a maximized state.
        /// </summary>
        public void Maximize()
        {
            Application.Invoke(() => Glfw.MaximizeWindow(WindowHandle));
        }

        /// <summary>
        /// Sets the window to a minimized state.
        /// </summary>
        public void Minimize()
        {
            Application.Invoke(() => Glfw.IconifyWindow(WindowHandle));
        }

        /// <summary>
        /// Sets the window to a default size state.
        /// </summary>
        public void Restore()
        {
            Application.Invoke(() => Glfw.RestoreWindow(WindowHandle));
        }

        #endregion

        #region Fullscreen

        /// <summary>
        /// Sets the window to fullscreen using the nearest monitor and existing video mode.
        /// </summary>
        public void SetFullscreen()
        {
            SetFullscreen(GetNearestMonitor());
        }

        /// <summary>
        /// Sets the window to fullscreen using the nearest monitor and specified video mode.
        /// </summary>
        public void SetFullscreen(VideoMode mode)
        {
            SetFullscreen(GetNearestMonitor(), mode);
        }

        /// <summary>
        /// Sets the window to fullscreen using the specified monitor and existing video mode.
        /// </summary>
        public void SetFullscreen(Monitor monitor)
        {
            SetFullscreen(monitor, monitor?.CurrentVideoMode ?? default);
        }

        /// <summary>
        /// Sets the window to fullscreen using the specified monitor and video mode.
        /// </summary>
        public void SetFullscreen(Monitor monitor, VideoMode mode)
        {
            Application.Invoke(() =>
            {
                // 
                if (monitor == null)
                {
                    // Disable fullscreen, and restore bounds to pre-fullscreen bounds
                    Glfw.SetWindowMonitor(WindowHandle, MonitorHandle.None, _restoreBounds.X, _restoreBounds.Y, _restoreBounds.Width, _restoreBounds.Height, -1);
                }
                else
                {
                    // If not already fullscreen, keep record of the current bounds
                    if (State != WindowState.Fullscreen) { _restoreBounds = _bounds; }

                    // Enable fullscreen
                    Glfw.SetWindowMonitor(WindowHandle, monitor.MonitorHandle, 0, 0, mode.Width, mode.Height, mode.RefreshRate);
                }
            });
        }

        #endregion

        /// <summary>
        /// Gets the monitor this window is positioned on.
        /// </summary>
        public Monitor GetNearestMonitor()
        {
            // Whatever monitor contains the window's center point is
            // considered the nearest. Another method of evaluation may
            // be more appropriate such as minimal sum of corner distances.
            foreach (var monitor in Monitor.Monitors)
            {
                if (monitor.Workarea.Contains(Bounds.Center))
                {
                    return monitor;
                }
            }

            // Somehow arrived here, just use the primary monitor.
            return Monitor.Default;
        }

        #region MoveTo

        /// <summary>
        /// Moves the window to the center of the nearest monitor.
        /// </summary>
        public void MoveToCenter()
        {
            MoveToCenter(GetNearestMonitor());
        }

        /// <summary>
        /// Moves the window to the center of the specified monitor.
        /// </summary>
        public void MoveToCenter(Monitor monitor)
        {
            var area = monitor.Workarea;
            Position = new IntVector(area.Width - Size.Width, area.Height - Size.Height) / 2;
        }

        #endregion

        #region Dispose

        protected virtual void Dispose(bool disposeManaged)
        {
            if (!IsDisposed)
            {
                IsDisposed = true;

                // todo: wait for render context to empty?

                if (disposeManaged)
                {
                    // Terminate rendering context
                    ((IDisposable) RenderContext).Dispose();
                }

                // Destroy window
                Application.Invoke(() => Glfw.DestroyWindow(WindowHandle));
                Application.RemoveWindow(this);
            }
        }

        public void Dispose()
        {
            Dispose(true);
            GC.SuppressFinalize(this);
        }

        #endregion

        private sealed class WindowRenderContext : OpenGLRenderContext
        {
            public WindowRenderContext(Window window)
                : base(window.Multisample)
            {
                Window = window;

                // Set initial size, and whenever the window is resized, also set the default surface size
                Window.FramebufferResized += _ => SetDefaultSurfaceSize(Window.FramebufferSize);
                SetDefaultSurfaceSize(Window.FramebufferSize);
            }

            public Window Window { get; }

            protected override void PrepareContext()
            {
                // Wait for window to be set to help avoid the race condition, since the context thread is a different thread.
                SpinWait.SpinUntil(() => Window != null);

                // Make context current on context thread
                Glfw.MakeContextCurrent(Window.WindowHandle);
                Glfw.SetSwapInterval(Window.VSync ? 1 : 0);
            }

            protected override void SwapBuffers()
            {
                Invoke(() => Glfw.SwapBuffers(Window.WindowHandle), false);
            }
        }
    }
}
