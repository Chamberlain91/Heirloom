using System;
using System.Collections.Generic;
using System.IO;
using System.Runtime.InteropServices;
using System.Threading;

using Heirloom.Drawing;
using Heirloom.Math;

namespace Heirloom.Desktop
{
    public class Window
    {
        private IntRectangle _bounds, _restoreBounds;
        private IntSize _framebufferSize;
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
        {
            _title = title ?? throw new ArgumentNullException(nameof(title));

            // Extract 
            HasTransparentFramebuffer = transparent && Application.SupportsTransparentFramebuffer;
            Multisample = multisample;

            // 
            Handle = Application.Invoke(() =>
            {
                // 
                Glfw.SetWindowCreationHint(WindowCreationHint.Samples, (int) Multisample);

                // Create window
                Glfw.SetWindowCreationHint(WindowAttribute.TransparentFramebuffer, HasTransparentFramebuffer);
                var handle = Glfw.CreateWindow(512, 512, title, MonitorHandle.None, Application.ShareContext);

                // Query intial window properties
                Glfw.GetWindowSize(handle, out _bounds.Width, out _bounds.Height);
                Glfw.GetWindowPosition(handle, out _bounds.X, out _bounds.Y);
                Glfw.GetFramebufferSize(handle, out _framebufferSize.Width, out _framebufferSize.Height);

                // Return window handle
                return handle;
            });

            // == Bind Callbacks

            Glfw.SetWindowCloseCallback(Handle, _windowCloseCallback = _ =>
            {
                IsClosed = OnClosing();
                Glfw.SetWindowShouldClose(Handle, IsClosed);

                if (IsClosed)
                {
                    OnClosed();
                    Dispose();
                }
            });

            Glfw.SetFramebufferSizeCallback(Handle, _framebufferSizeCallback = (_, w, h) =>
            {
                _framebufferSize = (w, h);
                OnFramebufferResized(w, h);
            });

            Glfw.SetWindowSizeCallback(Handle, _windowSizeCallback = (_, w, h) =>
            {
                _bounds.Size = (w, h);
                OnWindowResized(w, h);
            });

            Glfw.SetWindowPositionCallback(Handle, _windowPositionCallback = (_, x, y) =>
            {
                _bounds.Position = (x, y);
                OnWindowMoved(x, y);
            });

            Glfw.SetWindowContentScaleCallback(Handle, _windowContentScaleCallback = (_, xs, ys) =>
            {
                _contentScale = (xs, ys);
                OnContentScaleChanged(xs, ys);
            });

            // Key callbacks
            Glfw.SetCharCallback(Handle, _charCallback = (_, cp) => OnCharTyped((UnicodeCharacter) cp));
            Glfw.SetKeyCallback(Handle, _keyCallback = (_, k, c, a, m) => OnKeyPressed(k, c, a, m));

            // Mouse callbacks
            Glfw.SetCursorPositionCallback(Handle, _cursorPositionCallback = (_, x, y) => OnMouseMove((float) x, (float) y));
            Glfw.SetMouseButtonCallback(Handle, _mouseButtonCallback = (_, b, a, m) => OnMousePressed(b, a, m));
            Glfw.SetScrollCallback(Handle, _scrollCallback = (_, x, y) => OnMouseScroll((float) x, (float) y));

            // Set the default icons
            SetIcons(_defaultWindowIcons);

            // Inform system to track this window
            Application.AddWindow(this);

            // == Construct Graphics Context

            Graphics = Application.GraphicsFactory.CreateGraphics(this, vsync);

            // To help prevent the weird window framebuffer isn't the same size error...?
            Thread.Sleep(1);
        }

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
        /// Gets a value that determines if this window been disposed.
        /// </summary>
        public bool IsDisposed { get; private set; }

        /// <summary>
        /// Gets a value that determines if this window been closed.
        /// </summary>
        public bool IsClosed { get; private set; }

        /// <summary>
        /// Gets a value that determines if this window supports a transparent framebuffer.
        /// </summary>
        public bool HasTransparentFramebuffer { get; private set; }

        /// <summary>
        /// Gets the multisampling level configured on this window.
        /// </summary>
        public MultisampleQuality Multisample { get; private set; }

        /// <summary>
        /// Gets the graphics context associated with this window.
        /// </summary>
        public Graphics Graphics { get; }

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
        public IntSize Size
        {
            get => Bounds.Size;

            set => Application.Invoke(() =>
            {
                Glfw.SetWindowSize(Handle, value.Width, value.Height);
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

        #region Events

        public event Action<Window> Resized;

        public event Action<Window> FramebufferResized;

        public event Action<Window, WindowEvents> ContentScaleChanged;

        public event Action<Window, KeyEvent> KeyPress;

        public event Action<Window, KeyEvent> KeyRelease;

        public event Action<Window, KeyEvent> KeyRepeat;

        public event Action<Window, CharacterEvent> CharacterTyped;

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
            var ev = new WindowEvents(xScale, yScale);
            ContentScaleChanged?.Invoke(this, ev);
        }

        protected virtual void OnWindowMoved(int x, int y)
        {
            // Does nothing by default
        }

        protected virtual void OnKeyPressed(Key key, int scanCode, ButtonAction action, KeyModifiers modifiers)
        {
            var ev = new KeyEvent(key, scanCode, action, modifiers);

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
            var ev = new CharacterEvent(character);
            CharacterTyped?.Invoke(this, ev);
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
            Application.Invoke(() => Glfw.ShowWindow(Handle));
        }

        public void Hide()
        {
            Application.Invoke(() => Glfw.HideWindow(Handle));
        }

        public void Close()
        {
            OnClosed();
            Dispose();
        }

        public void Focus()
        {
            Application.Invoke(() => Glfw.FocusWindow(Handle));
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
        /// Sets the window to fullscreen using the nearest monitor and existing video mode.
        /// </summary>
        public void SetFullscreen()
        {
            SetFullscreen(Monitor);
        }

        /// <summary>
        /// Sets the window to fullscreen using the nearest monitor and specified video mode.
        /// </summary>
        public void SetFullscreen(VideoMode mode)
        {
            SetFullscreen(Monitor, mode);
        }

        /// <summary>
        /// Sets the window to fullscreen using the specified monitor and existing video mode.
        /// </summary>
        public void SetFullscreen(Monitor monitor)
        {
            SetFullscreen(monitor, monitor?.CurrentMode ?? default);
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
                    Glfw.SetWindowMonitor(Handle, MonitorHandle.None, _restoreBounds.X, _restoreBounds.Y, _restoreBounds.Width, _restoreBounds.Height, -1);
                }
                else
                {
                    // If not already fullscreen, keep record of the current bounds
                    if (State != WindowState.Fullscreen) { _restoreBounds = _bounds; }

                    // Enable fullscreen
                    Glfw.SetWindowMonitor(Handle, monitor.MonitorHandle, 0, 0, mode.Width, mode.Height, mode.RefreshRate);
                }
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

            SetCursor(cursor, (IntVector) cursor.Origin);
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

        private static Image[] LoadDefaultIcons()
        {
            return new Image[] {
                new Image(GetStream("Files.icon_128.png")),
                new Image(GetStream("Files.icon_64.png")),
                new Image(GetStream("Files.icon_32.png")),
                new Image(GetStream("Files.icon_16.png"))
            };

            static Stream GetStream(string file)
            {
                var type = typeof(Window);

                // Return stream
                return type.Assembly.GetManifestResourceStream($"{type.Namespace}.{file}");
            }
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
                    Graphics.Dispose();
                }

                // Destroy window
                Application.Invoke(() => Glfw.DestroyWindow(Handle));
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

        public void Dispose()
        {
            Dispose(true);
            GC.SuppressFinalize(this);
        }

        #endregion
    }
}
