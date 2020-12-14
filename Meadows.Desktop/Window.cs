using System;

using Meadows.Desktop.GLFW;
using Meadows.Drawing;
using Meadows.Mathematics;

namespace Meadows.Desktop
{
    public sealed class Window : Screen, IDisposable
    {
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

        private IntRectangle _restoreBounds;
        private string _title;

        private bool _visible = true;

        #region Constructors

        public Window(string title, IntSize size, MultisampleQuality multisample = MultisampleQuality.None, bool vsync = true)
            : base(multisample)
        {
            // 
            Handle = Application.Invoke(() =>
            {
                Log.Debug($"Creating Window (MSAA: {multisample})");

                // Create a window with a non-transparent framebuffer and the specified multisampling quality
                Glfw.SetWindowCreationHint(WindowCreationHint.Samples, (int) multisample);
                Glfw.SetWindowCreationHint(WindowAttribute.TransparentFramebuffer, false);

                // Create window
                return Glfw.CreateWindow(size.Width, size.Height, title, MonitorHandle.None, Application.ShareContext);
            });

            // == Bind Callbacks

            Glfw.SetWindowSizeCallback(Handle, _windowSizeCallback = (_, w, h) =>
            {
                OnResized(new IntSize(w, h));
            });

            Glfw.SetFramebufferSizeCallback(Handle, _framebufferSizeCallback = (_, w, h) =>
            {
                var size = new IntSize(w, h);
                Surface.SetSize(size);
                OnSurfaceResized(size);
            });

            Glfw.SetWindowContentScaleCallback(Handle, _windowContentScaleCallback = (_, xs, ys) =>
            {
                var scale = new Vector(xs, ys);
                OnContentScaleChanged(scale);
            });

            Glfw.SetWindowCloseCallback(Handle, _windowCloseCallback = _ =>
            {
                var shouldClose = OnClosing();
                Glfw.SetWindowShouldClose(Handle, shouldClose);
                if (shouldClose) { Dispose(); }
            });

            // Set initial properties
            Title = title ?? throw new ArgumentNullException(nameof(title));
            Size = size;

            // Get initial surface size
            Surface.SetSize(Application.Invoke(() =>
            {
                Glfw.GetFramebufferSize(Handle, out var w, out var h);
                return new IntSize(w, h);
            }));

            // todo: set default icons

            // Create graphics context for this window
            Graphics = CreateGraphicsContext(this, vsync);

            // Add this window to application
            lock (Application.Windows)
            {
                Application.Windows.Add(this);
            }
        }

        /// <summary>
        /// Performs final cleanup of <see cref="Window"/> before garbase collection.
        /// </summary>
        ~Window()
        {
            Dispose(false);
        }

        #endregion

        /// <summary>
        /// Gets a value that determines if this window been disposed.
        /// </summary>
        public bool IsDisposed { get; private set; }

        /// <summary>
        /// Gets the handle to the underlying GLFW window.
        /// </summary>
        internal WindowHandle Handle { get; }

        /// <summary>
        /// Gets the graphics context associated with this window.
        /// </summary>
        public override GraphicsContext Graphics { get; }

        #region Window Properties

        /// <summary>
        /// Gets the current state of the window.
        /// </summary>
        public WindowState State => Application.Invoke(() =>
        {
            // Is the window in an iconified state?
            if (Glfw.GetWindowAttribute(Handle, WindowAttribute.Iconified) != 0)
            {
                return WindowState.Minimized;
            }
            // Is the window in a maximized state?
            else if (Glfw.GetWindowAttribute(Handle, WindowAttribute.Maximized) != 0)
            {
                // Window was maximized
                return WindowState.Maximized;
            }
            // Is the window in a maximized state?
            else if (Glfw.GetWindowMonitor(Handle) != MonitorHandle.None)
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

        /// <summary>
        /// Gets or sets the window size (in screen coordinates).
        /// </summary>
        public override IntSize Size
        {
            get => Application.Invoke(() =>
            {
                Glfw.GetWindowSize(Handle, out var width, out var height);
                return new IntSize(width, height);
            });

            set
            {
                if (State == WindowState.Fullscreen) { EndFullscreen(); }
                Application.Invoke(() => Glfw.SetWindowSize(Handle, value.Width, value.Height));
            }
        }

        /// <summary>
        /// Gets or sets the window position (in screen coordinates).
        /// </summary>
        public IntVector Position
        {
            get => Application.Invoke(() =>
            {
                Glfw.GetWindowPosition(Handle, out var x, out var y);
                return new IntVector(x, y);
            });

            set => Application.Invoke(() => Glfw.SetWindowPosition(Handle, value.X, value.Y));
        }

        /// <summary>
        /// Gets the window bounds (in screen coordinates).
        /// </summary>
        public IntRectangle Bounds
        {
            get => new(Position, Size);

            set
            {
                Position = value.Position;
                Size = value.Size;
            }
        }

        public string Title
        {
            get => _title;
            set => Application.Invoke(() => Glfw.SetWindowTitle(Handle, _title = value));
        }

        public bool IsResizable
        {
            get => Application.Invoke(() => Glfw.GetWindowAttribute(Handle, WindowAttribute.Resizable) != 0);
            set => Application.Invoke(() => Glfw.SetWindowAttribute(Handle, WindowAttribute.Resizable, value));
        }

        public bool IsVisible
        {
            get => _visible;

            set
            {
                if (_visible && !value)
                {
                    // Was visible, asking not to be
                    Application.Invoke(() => Glfw.HideWindow(Handle));
                }
                else if (!_visible && value)
                {
                    // Was invisible, asking to be visible
                    Application.Invoke(() => Glfw.ShowWindow(Handle));
                }

                _visible = value;
            }
        }

        public bool IsClosed { get; private set; }

        #endregion

        #region Window Methods

        public void Focus()
        {
            Application.Invoke(() => Glfw.FocusWindow(Handle));
        }

        public void Maximize()
        {
            Application.Invoke(() => Glfw.MaximizeWindow(Handle));
        }

        public void Minimize()
        {
            Application.Invoke(() => Glfw.IconifyWindow(Handle));
        }

        public void Restore()
        {
            Application.Invoke(() => Glfw.RestoreWindow(Handle));
        }

        public void Close()
        {
            OnClosed();
        }

        #endregion

        #region Window Events

        /// <summary>
        /// Event called when the screen is trying to close.
        /// Returning <c>false</c> will prevent the screen from closing, if possible.
        /// </summary>
        public event Func<Screen, bool> Closing;

        /// <summary>
        /// Call this function raise the <see cref="Closing"/> event.
        /// </summary>
        internal bool OnClosing()
        {
            // note: defaults to true to allow the screen to close without handlers.
            return Closing?.Invoke(this) ?? true;
        }

        /// <summary>
        /// Event called when the screen has closed.
        /// </summary>
        public event Action<Screen> Closed;

        /// <summary>
        /// Call this function raise the <see cref="Closed"/> event.
        /// </summary>
        internal void OnClosed()
        {
            Dispose();
        }

        #endregion

        #region Fullscreen

        public void BeginFullscreen(Display display)
        {
            BeginFullscreen(display, display.CurrentMode);
        }

        public void BeginFullscreen(Display display, VideoMode mode)
        {
            Application.Invoke(() =>
            {
                // If not already fullscreen, keep record of the current bounds
                if (State != WindowState.Fullscreen) { _restoreBounds = Bounds; }

                // Enable fullscreen
                Glfw.SetWindowMonitor(Handle, display.MonitorHandle, 0, 0, mode.Width, mode.Height, mode.RefreshRate);
            });
        }

        public void EndFullscreen()
        {
            Application.Invoke(() =>
            {
                // Disable fullscreen, and restore bounds to pre-fullscreen bounds
                Glfw.SetWindowMonitor(Handle, MonitorHandle.None, _restoreBounds.X, _restoreBounds.Y, _restoreBounds.Width, _restoreBounds.Height, -1);
            });
        }

        #endregion

        #region Dispose

        /// <summary>
        /// Disposes this screen, freeing any unmanaged resources.
        /// </summary>
        private void Dispose(bool disposing)
        {
            if (!IsClosed)
            {
                IsClosed = true;
                Closed?.Invoke(this);
            }

            if (!IsDisposed)
            {
                IsDisposed = true;

                if (disposing)
                {
                    // Dispose managed...?
                }

                // todo: untrack input source...?

                // Dispose graphics context
                Graphics.Dispose();

                // Remove window from application tracking
                lock (Application.Windows)
                {
                    Application.Windows.Remove(this);
                }

                // Destroy GLFW Window
                Application.Invoke(() => Glfw.DestroyWindow(Handle));

                // This will 'keep alive' callbacks until this point
                GC.KeepAlive(_windowCloseCallback);
                GC.KeepAlive(_windowPositionCallback);
                GC.KeepAlive(_windowContentScaleCallback);
                GC.KeepAlive(_framebufferSizeCallback);
                GC.KeepAlive(_windowSizeCallback);

                GC.KeepAlive(_charCallback);
                GC.KeepAlive(_keyCallback);

                GC.KeepAlive(_cursorPositionCallback);
                GC.KeepAlive(_mouseButtonCallback);
                GC.KeepAlive(_scrollCallback);
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
        /// Gets an array of the currently opened windows.
        /// </summary>
        public static Window[] GetWindows()
        {
            // Returns an array copy of the windows list?
            // This might be inefficient?
            return Application.Windows.ToArray();
        }

        private static GraphicsContext CreateGraphicsContext(Window window, bool vsync)
        {
            if (GraphicsBackend.Current is ESWindowGraphicsBackend backend)
            {
                // Creates an OpenGL ES Context
                return backend.CreateGraphics(window, vsync);
            }
            else
            {
                throw new NotImplementedException("Unable to create graphics context for the current graphics backend.");
            }
        }
    }
}
