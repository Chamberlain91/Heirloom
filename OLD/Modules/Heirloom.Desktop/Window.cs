using System;
using System.Collections.Generic;
using System.Runtime.InteropServices;

namespace Heirloom.Desktop
{
    /// <summary>
    /// Represents a physical window on a desktop platform, implements <see cref="Screen"/>.
    /// </summary>
    public sealed class Window : Screen
    {
        private IntRectangle _bounds, _restoreBounds;
        private Vector _contentScale;
        private string _title;

        private Vector _mousePosition;
        private Vector _mouseDelta;

        private CursorHandle _cursorHandle;

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

        private Image[] _icons = Array.Empty<Image>();

        /// <summary>
        /// Gets the default window icon set.
        /// </summary>
        private static readonly Image[] _defaultWindowIcons = LoadDefaultIcons();

        #region Constructors

        /// <summary>
        /// Constructs a new window.
        /// </summary>
        /// <param name="title">The text in the titlebar of the window.</param>
        /// <param name="multisample">What level of MSAA to use.</param>
        /// <param name="vsync">Enable VSync on this window.</param>
        /// <param name="transparent">Enable transparent framebuffer (if OS supports it).</param>
        public Window(string title, MultisampleQuality multisample = MultisampleQuality.None, bool vsync = true, bool transparent = false)
            : this(title, (512, 512), multisample, vsync, transparent)
        { }

        /// <summary>
        /// Constructs a new window.
        /// </summary>
        /// <param name="title">The text in the titlebar of the window.</param>
        /// <param name="size">The initial size of the window.</param>
        /// <param name="multisample">What level of MSAA to use.</param>
        /// <param name="vsync">Enable VSync on this window.</param>
        /// <param name="transparent">Enable transparent framebuffer (if OS supports it).</param>
        public Window(string title, IntSize size, MultisampleQuality multisample = MultisampleQuality.None, bool vsync = true, bool transparent = false)
        {
            _title = title ?? throw new ArgumentNullException(nameof(title));

            // Extract 
            HasTransparentFramebuffer = transparent && Application.SupportsTransparentFramebuffer;

            IntSize framebufferSize = default;

            // 
            Handle = Application.Invoke(() =>
            {
                Log.Debug($"Creating Window (MSAA: {multisample})");

                // 
                Glfw.SetWindowCreationHint(WindowCreationHint.Samples, (int) multisample);
                Glfw.SetWindowCreationHint(WindowAttribute.TransparentFramebuffer, HasTransparentFramebuffer);

                // Create window
                var handle = Glfw.CreateWindow(size.Width, size.Height, title, MonitorHandle.None, Application.ShareContext);

                // Query intial window properties
                Glfw.GetFramebufferSize(handle, out framebufferSize.Width, out framebufferSize.Height);
                Glfw.GetWindowSize(handle, out _bounds.Width, out _bounds.Height);
                Glfw.GetWindowPosition(handle, out _bounds.X, out _bounds.Y);

                // Return window handle
                return handle;
            });

            // == Bind Callbacks

            Glfw.SetWindowSizeCallback(Handle, _windowSizeCallback = (_, w, h) =>
            {
                _bounds.Size = new IntSize(w, h);
                OnResized(_bounds.Size);
            });

            Glfw.SetFramebufferSizeCallback(Handle, _framebufferSizeCallback = (_, w, h) =>
            {
                framebufferSize = new IntSize(w, h);
                OnFramebufferResized(framebufferSize);
            });

            Glfw.SetWindowContentScaleCallback(Handle, _windowContentScaleCallback = (_, xs, ys) =>
            {
                _contentScale = new Vector(xs, ys);
                OnContentScaleChanged(_contentScale);
            });

            Glfw.SetWindowPositionCallback(Handle, _windowPositionCallback = (_, x, y) =>
            {
                _bounds.Position = (x, y);
                // todo: Window moved event
            });

            Glfw.SetWindowCloseCallback(Handle, _windowCloseCallback = _ =>
            {
                var shouldClose = OnClosing();
                Glfw.SetWindowShouldClose(Handle, shouldClose);
                if (shouldClose) { Dispose(); }
            });

            // [Keyboard Callbacks]

            Glfw.SetCharCallback(Handle, _charCallback = (_, cp) =>
            {
                var e = new CharacterEvent((UnicodeCharacter) cp);
                OnCharacterTyped(e);
            });

            Glfw.SetKeyCallback(Handle, _keyCallback = (_, key, code, action, modifiers) =>
            {
                switch (action)
                {
                    default:
                        throw new InvalidOperationException("Encountered illegal key action");

                    case KeyAction.Press:
                        OnKeyPressed(new KeyEvent(code, key, modifiers, ButtonState.Pressed));
                        break;

                    case KeyAction.Release:
                        OnKeyReleased(new KeyEvent(code, key, modifiers, ButtonState.Released));
                        break;

                    case KeyAction.Repeat:
                        OnKeyRepeat(new KeyEvent(code, key, modifiers, ButtonState.Down));
                        break;
                }
            });

            // [Mouse callbacks]

            Glfw.SetCursorPositionCallback(Handle, _cursorPositionCallback = (_, x, y) =>
            {
                // Compute delta motion
                var prevPosition = _mousePosition;
                _mousePosition.Set((float) x, (float) y);
                _mouseDelta = _mousePosition - prevPosition;

                OnMouseMoved(new MouseMoveEvent(_mousePosition, _mouseDelta));
            });

            Glfw.SetMouseButtonCallback(Handle, _mouseButtonCallback = (_, button, action, modifiers) =>
            {
                switch (action)
                {
                    default:
                        throw new InvalidOperationException("Encountered illegal mosue button action");

                    case KeyAction.Press:
                        OnMousePressed(new MouseButtonEvent((MouseButton) button, modifiers, ButtonState.Pressed, _mousePosition));
                        break;

                    case KeyAction.Release:
                        OnMouseReleased(new MouseButtonEvent((MouseButton) button, modifiers, ButtonState.Released, _mousePosition));
                        break;
                }
            });

            Glfw.SetScrollCallback(Handle, _scrollCallback = (_, x, y) =>
            {
                var e = new MouseScrollEvent((float) x, (float) y);
                OnMouseScrolled(e);
            });

            // Set the default icons
            SetIcons(_defaultWindowIcons);

            // Inform system to track this window
            Application.AddWindow(this);

            // Create "default surface"
            Surface = new Surface(framebufferSize, multisample, SurfaceType.UnsignedByte, true);

            // Create graphics context for this window
            Graphics = Application.CreateGraphics(this, vsync);
        }

        /// <summary>
        /// Performs final cleanup of <see cref="Window"/> before garbase collection.
        /// </summary>
        ~Window()
        {
            Dispose(false);
        }

        #endregion

        #region Properties

        /// <summary>
        /// Gets the handle to the underlying GLFW window.
        /// </summary>
        internal WindowHandle Handle { get; }

        /// <summary>
        /// Gets a value that determines if this window supports a transparent framebuffer.
        /// </summary>
        public bool HasTransparentFramebuffer { get; private set; }

        /// <summary>
        /// Gets a value that determines if the window is visible.
        /// </summary>
        public bool IsVisible => Application.Invoke(() => Glfw.GetWindowAttribute(Handle, WindowAttribute.Visible) != 0);

        /// <summary>
        /// Gets a value that determines if the window is decorated.
        /// </summary>
        public bool IsDecorated
        {
            get => Application.Invoke(() => Glfw.GetWindowAttribute(Handle, WindowAttribute.Decorated) != 0);
            set => Application.Invoke(() => Glfw.SetWindowAttribute(Handle, WindowAttribute.Decorated, value));
        }

        /// <summary>
        /// Gets a value that determines if the window be resized.
        /// </summary>
        public bool IsResizable
        {
            get => Application.Invoke(() => Glfw.GetWindowAttribute(Handle, WindowAttribute.Resizable) != 0);
            set => Application.Invoke(() => Glfw.SetWindowAttribute(Handle, WindowAttribute.Resizable, value));
        }

        /// <summary>
        /// Gets a value that determines if the window "always on top".
        /// </summary>
        public bool IsFloating
        {
            get => Application.Invoke(() => Glfw.GetWindowAttribute(Handle, WindowAttribute.Floating) != 0);
            set => Application.Invoke(() => Glfw.SetWindowAttribute(Handle, WindowAttribute.Floating, value));
        }

        /// <summary>
        /// Gets or set the window title text.
        /// </summary>
        public string Title
        {
            get => _title;

            set => Application.Invoke(() =>
            {
                Glfw.SetWindowTitle(Handle, value);
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
                Glfw.SetWindowSize(Handle, value.Width, value.Height);
                Glfw.SetWindowPosition(Handle, value.X, value.Y);
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
                Glfw.SetWindowPosition(Handle, value.X, value.Y);
                _bounds.Position = value;
            });
        }

        /// <summary>
        /// Gets or sets the window size in screen units.
        /// </summary>
        public override IntSize Size
        {
            get => Bounds.Size;

            set => Application.Invoke(() =>
            {
                Glfw.SetWindowSize(Handle, value.Width, value.Height);
                _bounds.Size = value;
            });
        }

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
        /// Gets the monitor this window is positioned on by checking the center point of the window bounds.
        /// </summary>
        public Monitor Monitor
        {
            get
            {
                // Whatever monitor contains the window's center point is
                // considered the nearest. Another method of evaluation may
                // be more appropriate such as minimal sum of corner distances.
                foreach (var monitor in Application.Monitors)
                {
                    if (monitor.Workarea.Contains(Bounds.Center))
                    {
                        return monitor;
                    }
                }

                // Somehow arrived here, just use the primary monitor.
                return Application.DefaultMonitor;
            }
        }

        /// <summary>
        /// Gets this windows icon set.
        /// </summary>
        public IReadOnlyList<Image> Icons => _icons;

        #endregion

        #region Window State (Show, Maximize, etc)

        /// <summary>
        /// Shows the window, making it visible.
        /// </summary>
        public void Show()
        {
            Application.Invoke(() => Glfw.ShowWindow(Handle));
        }

        /// <summary>
        /// Hides the window, minimizing it.
        /// </summary>
        public void Hide()
        {
            Application.Invoke(() => Glfw.HideWindow(Handle));
        }

        /// <summary>
        /// Brings focus to this window.
        /// </summary>
        public void Focus()
        {
            Application.Invoke(() => Glfw.FocusWindow(Handle));
        }

        /// <summary>
        /// Closes this window.
        /// </summary>
        public override void Close()
        {
            OnClosed();
        }

        /// <summary>
        /// Sets the window to a maximized state.
        /// </summary>
        public void Maximize()
        {
            Application.Invoke(() => Glfw.MaximizeWindow(Handle));
        }

        /// <summary>
        /// Sets the window to a minimized state.
        /// </summary>
        public void Minimize()
        {
            Application.Invoke(() => Glfw.IconifyWindow(Handle));
        }

        /// <summary>
        /// Sets the window to a default size state.
        /// </summary>
        public void Restore()
        {
            Application.Invoke(() => Glfw.RestoreWindow(Handle));
        }

        #endregion

        #region Fullscreen

        /// <summary>
        /// Puts the window into fullscreen using the nearest monitor and existing video mode.
        /// </summary>
        public void BeginFullscreen()
        {
            BeginFullscreen(Monitor);
        }

        /// <summary>
        /// Sets the window to fullscreen using the specified monitor and existing video mode.
        /// </summary>
        public void BeginFullscreen(Monitor monitor)
        {
            BeginFullscreen(monitor.CurrentMode, monitor);
        }

        /// <summary>
        /// Puts the window into fullscreen using the nearest monitor and specified video mode.
        /// </summary>
        public void BeginFullscreen(VideoMode mode)
        {
            BeginFullscreen(mode, Monitor);
        }

        /// <summary>
        /// Sets the window to fullscreen using the specified monitor and video mode.
        /// </summary>
        public void BeginFullscreen(VideoMode mode, Monitor monitor)
        {
            Application.Invoke(() =>
            {
                // If not already fullscreen, keep record of the current bounds
                if (State != WindowState.Fullscreen) { _restoreBounds = _bounds; }

                // Enable fullscreen
                Glfw.SetWindowMonitor(Handle, monitor.MonitorHandle, 0, 0, mode.Width, mode.Height, mode.RefreshRate);
            });
        }

        /// <summary>
        /// Disables fullscreen mode.
        /// </summary>
        public void EndFullscreen()
        {
            Application.Invoke(() =>
            {
                // Disable fullscreen, and restore bounds to pre-fullscreen bounds
                Glfw.SetWindowMonitor(Handle, MonitorHandle.None, _restoreBounds.X, _restoreBounds.Y, _restoreBounds.Width, _restoreBounds.Height, -1);
            });
        }

        #endregion

        #region MoveTo

        /// <summary>
        /// Moves the window to the center of the nearest monitor.
        /// </summary>
        public void MoveToCenter()
        {
            MoveToCenter(Monitor);
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

        #region Set Icon

        /// <summary>
        /// Assigns a set of icon images to the window (the image with the most desireable szie by the system is chosen).
        /// </summary>
        public void SetIcons(Image[] icons)
        {
            if (icons is null) { throw new ArgumentNullException(nameof(icons)); }

            Application.Invoke(() =>
            {
                // 
                var data = new ImageData[icons.Length];
                var pins = new GCHandle[icons.Length];

                // Pin each image and 
                for (var i = 0; i < icons.Length; i++)
                {
                    var icon = icons[i];

                    // Pin the pixels (prevent GC collection)
                    var pixels = icon.GetPixels();
                    pins[i] = GCHandle.Alloc(pixels, GCHandleType.Pinned);

                    // Construct image data
                    data[i] = new ImageData
                    {
                        Width = icon.Width,
                        Height = icon.Height,
                        Pixels = pins[i].AddrOfPinnedObject(),
                    };
                }

                _icons = icons;
                Glfw.SetWindowIcons(Handle, data);

                // Free GC Pins
                for (var i = 0; i < icons.Length; i++)
                {
                    pins[i].Free();
                }
            });
        }

        /// <summary>
        /// Assigns a new icon image to the window.
        /// </summary>
        public void SetIcon(Image icon)
        {
            SetIcons(new[] { icon });
        }

        private static Image[] LoadDefaultIcons()
        {
            return new Image[] {
                new Image("Embedded/icon_128.png"),
                new Image("Embedded/icon_64.png"),
                new Image("Embedded/icon_32.png"),
                new Image("Embedded/icon_16.png")
            };
        }

        #endregion

        #region Set Cursor Image

        /// <summary>
        /// Changes the appearance of the cursor on this window.
        /// </summary>
        public void SetCursor(StandardCursor cursor)
        {
            Application.Invoke(() =>
            {
                // Remove previous cursor
                if (_cursorHandle != CursorHandle.None) { Glfw.DestroyCursor(_cursorHandle); }

                // Construct and set a new cursor
                _cursorHandle = Glfw.CreateCursor(cursor);
                Glfw.SetCursor(Handle, _cursorHandle);
            });
        }

        /// <summary>
        /// Changes the appearance of the cursor on this window.
        /// </summary>
        public void SetCursor(Image cursor)
        {
            if (cursor is null) { throw new ArgumentNullException(nameof(cursor)); }

            SetCursor(cursor, cursor.Origin);
        }

        /// <summary>
        /// Changes the appearance of the cursor on this window.
        /// </summary>
        public void SetCursor(Image cursor, IntVector hotspot)
        {
            if (cursor is null) { throw new ArgumentNullException(nameof(cursor)); }

            Application.Invoke(() =>
            {
                unsafe
                {
                    // Gets a copy of the image pixels
                    var pixels = cursor.GetPixels();

                    // 
                    fixed (ColorBytes* ptr = pixels)
                    {
                        var data = new ImageData
                        {
                            Width = cursor.Width,
                            Height = cursor.Height,
                            Pixels = (IntPtr) ptr
                        };

                        // Remove previous cursor
                        if (_cursorHandle != CursorHandle.None) { Glfw.DestroyCursor(_cursorHandle); }

                        // Construct and set a new cursor
                        _cursorHandle = Glfw.CreateCursor(data, hotspot.X, hotspot.Y);
                        Glfw.SetCursor(Handle, _cursorHandle);
                    }
                }
            });
        }

        #endregion

        #region Software Keyboard

        /// <inheritdoc/>
        public override bool SupportsSoftwareKeyboard => false;

        /// <inheritdoc/>
        public override void ShowSoftwareKeyboard()
        {
            throw new NotImplementedException();
        }

        /// <inheritdoc/>
        public override void HideSoftwareKeyboard()
        {
            throw new NotImplementedException();
        }

        #endregion

        #region Dispose

        /// <inheritdoc/>
        protected override void Dispose(bool disposeManaged)
        {
            if (!IsDisposed)
            {
                base.Dispose(disposeManaged);

                // Destroy window
                Application.Invoke(() => Glfw.DestroyWindow(Handle));

                // Remove window from application
                Application.RemoveWindow(this);

                // 
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

        #endregion
    }
}
