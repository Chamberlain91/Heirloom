using System;

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
        private string _title;

        private readonly WindowCloseCallback _windowCloseCallback;
        private readonly WindowPositionCallback _windowPositionCallback;
        private readonly FramebufferSizeCallback _framebufferSizeCallback;
        private readonly WindowSizeCallback _windowSizeCallback;

        private readonly CharCallback _charCallback;
        private readonly KeyCallback _keyCallback;

        public Window(int width, int height, string title, bool vsync = true, bool transparentFramebuffer = false)
        {
            // Watch window
            Application.AddWindow(this);

            // 
            Transparent = transparentFramebuffer && Application.SupportsTransparentFramebuffer;
            VSync = vsync;

            // 
            _title = title;

            // 
            WindowHandle = Application.Invoke(() =>
            {
                // Create window
                Glfw.SetWindowCreationHint(WindowAttribute.TransparentFramebuffer, transparentFramebuffer);
                var handle = Glfw.CreateWindow(width, height, title, MonitorHandle.None, Application.ShareContext);

                // Query intial window properties
                Glfw.GetWindowSize(handle, out _bounds.Width, out _bounds.Height);
                Glfw.GetWindowPosition(handle, out _bounds.X, out _bounds.Y);
                Glfw.GetFramebufferSize(handle, out _framebufferSize.Width, out _framebufferSize.Height);

                // Return window handle
                return handle;
            });

            // == Construct Render Context

            RenderContext = new WindowRenderContext(this);

            // == Bind Callbacks

            Glfw.SetWindowCloseCallback(WindowHandle, _windowCloseCallback = _ =>
            {
                IsClosed = OnClosing();
                Glfw.SetWindowShouldClose(WindowHandle, IsClosed);
                if (IsClosed) { Dispose(); }
            });

            Glfw.SetFramebufferSizeCallback(WindowHandle, _framebufferSizeCallback = (_, w, h) =>
            {
                _framebufferSize = (w, h);
                OnFramebufferResized(w, h);
            });

            Glfw.SetWindowSizeCallback(WindowHandle, _windowSizeCallback = (_, w, h) =>
            {
                _bounds.Size = (w, h);
                OnResized(w, h);
            });

            Glfw.SetWindowPositionCallback(WindowHandle, _windowPositionCallback = (_, x, y) =>
            {
                _bounds.Position = (x, y);
                OnMoved(x, y);
            });

            // 
            Glfw.SetCharCallback(WindowHandle, _charCallback = (_, cp) => OnCharTyped((UnicodeCharacter) cp));
            Glfw.SetKeyCallback(WindowHandle, _keyCallback = (_, k, c, a, m) => OnKeyPressed(k, c, a, m));
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
        }

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

        public IntVector Position
        {
            get => Bounds.Position;

            set => Application.Invoke(() =>
            {
                Glfw.SetWindowPosition(WindowHandle, value.X, value.Y);
                _bounds.Position = value;
            });
        }

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
        /// The size of the underlying framebuffer.
        /// </summary>
        public IntSize FramebufferSize => _framebufferSize;

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

        public event Action<Window> Resized;

        public event Action<Window> FramebufferResized;

        #region Events / Callback Sinks

        protected virtual bool OnClosing()
        {
            return true; // Yes, should close by default
        }

        protected virtual void OnResized(int w, int h)
        {
            Resized?.Invoke(this);
        }

        protected virtual void OnFramebufferResized(int w, int h)
        {
            FramebufferResized?.Invoke(this);
        }

        protected virtual void OnMoved(int x, int y)
        {
            // Does nothing by default
        }

        protected virtual void OnKeyPressed(Key key, int scancode, ButtonAction action, KeyModifiers modifiers)
        {
            // Does nothing by default
        }

        protected virtual void OnCharTyped(UnicodeCharacter character)
        {
            // Does nothing by default
        }

        #endregion

        public void Show()
        {
            Application.Invoke(() => Glfw.ShowWindow(WindowHandle));
        }

        public void Hide()
        {
            Application.Invoke(() => Glfw.HideWindow(WindowHandle));
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

        /// <summary>
        /// Sets the specified window fullscreen on this monitor.
        /// </summary>
        public void SetFullscreen(Monitor monitor)
        {
            SetFullscreen(monitor, monitor?.CurrentVideoMode ?? default);
        }

        /// <summary>
        /// Sets the specified window fullscreen on this monitor.
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
                    RenderContext.Dispose();
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
            {
                Window = window;

                // Set initial size, and whenever the window is resized, also set the default surface size
                Window.FramebufferResized += _ => SetDefaultSurfaceSize(Window.FramebufferSize);
                SetDefaultSurfaceSize(Window.FramebufferSize);

                // Begin GL thread
                StartThread();
            }

            public Window Window { get; }

            protected override void PrepareContext()
            {
                Glfw.MakeContextCurrent(Window.WindowHandle);
                Glfw.SetSwapInterval(Window.VSync ? 1 : 0);
            }

            public override void SwapBuffers()
            {
                Flush(); // todo: move to parent type, we always need to flush here!
                Invoke(() => Glfw.SwapBuffers(Window.WindowHandle), false);
            }
        }
    }
}
