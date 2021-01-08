using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Runtime.InteropServices;

using Meadows.Desktop.GLFW;
using Meadows.Drawing;
using Meadows.Mathematics;
using Meadows.Text;

namespace Meadows.Desktop
{
    public sealed class Window : IScreen, IDisposable, IInputSource
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

        private readonly InputSource _inputSource;

        private Image[] _icons = Array.Empty<Image>();

        private IntRectangle _restoreBounds;
        private string _title;

        private Display _fullscreenDisplay;
        private bool _visible = true;

        #region Constructors

        public Window(string title, MultisampleQuality multisample = MultisampleQuality.None, bool vsync = true)
            : this(title, (640, 480), multisample, vsync)
        { }

        public Window(string title, IntSize size, MultisampleQuality multisample = MultisampleQuality.None, bool vsync = true)
        {
            // 
            Surface = new Surface(multisample, SurfaceFormat.UnsignedByte, this);

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

            #region Bind Window Callbacks

            Glfw.SetWindowSizeCallback(Handle, _windowSizeCallback = (_, w, h) =>
            {
                Resized?.Invoke(this, new IntSize(w, h));
            });

            Glfw.SetFramebufferSizeCallback(Handle, _framebufferSizeCallback = (_, w, h) =>
            {
                var size = new IntSize(w, h);
                Surface.SetSize(size);
                SurfaceResized?.Invoke(this, size);
            });

            Glfw.SetWindowCloseCallback(Handle, _windowCloseCallback = _ =>
            {
                var shouldClose = OnClosing();
                Glfw.SetWindowShouldClose(Handle, shouldClose);
                if (shouldClose) { Dispose(); }
            });

            #endregion

            #region Bind Keyboard Callbacks

            Glfw.SetCharCallback(Handle, _charCallback = (_, cp) =>
            {
                var e = new CharacterEvent((UnicodeCharacter) cp);
                _inputSource.CharacterEvents.Enqueue(e);
            });

            Glfw.SetKeyCallback(Handle, _keyCallback = (_, key, code, action, modifiers) =>
            {
                switch (action)
                {
                    default:
                        throw new InvalidOperationException("Encountered illegal key action");

                    case KeyAction.Press:
                        _inputSource.KeyEvents.Enqueue(new KeyEvent(code, key, modifiers, ButtonState.Pressed));
                        break;

                    case KeyAction.Release:
                        _inputSource.KeyEvents.Enqueue(new KeyEvent(code, key, modifiers, ButtonState.Released));
                        break;

                    case KeyAction.Repeat:
                        _inputSource.KeyEvents.Enqueue(new KeyEvent(code, key, modifiers, ButtonState.Repeat));
                        break;
                }
            });

            #endregion

            #region Bind Mouse Callbacks

            var mousePosition = Vector.Zero;
            var mouseDelta = Vector.Zero;

            Glfw.SetCursorPositionCallback(Handle, _cursorPositionCallback = (_, x, y) =>
            {
                lock (_inputSource.MouseMoveEvents)
                {
                    // Compute delta position
                    var prevPosition = mousePosition;
                    mousePosition = new Vector((float) x, (float) y);
                    mouseDelta = mousePosition - prevPosition;

                    // 
                    var ev = new MouseMoveEvent(mousePosition, mouseDelta);
                    _inputSource.MouseMoveEvents.Enqueue(ev);
                }
            });

            Glfw.SetMouseButtonCallback(Handle, _mouseButtonCallback = (_, button, action, modifiers) =>
            {
                lock (_inputSource.MouseButtonEvents)
                {
                    switch (action)
                    {
                        default:
                            throw new InvalidOperationException("Encountered illegal mosue button action");

                        case KeyAction.Press:
                        {
                            var ev = new MouseButtonEvent((MouseButton) button, modifiers, ButtonState.Pressed, mousePosition);
                            _inputSource.MouseButtonEvents.Enqueue(ev);
                        }
                        break;

                        case KeyAction.Release:
                        {
                            var ev = new MouseButtonEvent((MouseButton) button, modifiers, ButtonState.Released, mousePosition);
                            _inputSource.MouseButtonEvents.Enqueue(ev);
                        }
                        break;
                    }
                }
            });

            Glfw.SetScrollCallback(Handle, _scrollCallback = (_, x, y) =>
            {
                lock (_inputSource.MouseScrollEvents)
                {
                    var ev = new MouseScrollEvent((float) x, (float) y);
                    _inputSource.MouseScrollEvents.Enqueue(ev);
                }
            });

            #endregion

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
            SetIcons(CreateDefaultIcons());

            // Create input source and assign window as input source if no other input source has been set.
            _inputSource = new InputSource(this);
            if (Input.InputSource == null)
            {
                Input.SetInputSource(this);
            }

            // Create graphics context for this window
            Graphics = CreateGraphicsContext(this, vsync);

            // Add this window to application
            lock (Application.Windows)
            {
                Application.Windows.Add(this);
            }

            static GraphicsContext CreateGraphicsContext(Window window, bool vsync)
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

        /// <summary>
        /// Performs final cleanup of <see cref="Window"/> before garbase collection.
        /// </summary>
        ~Window()
        {
            Dispose(false);
        }

        #endregion

        /// <summary>
        /// Gets the handle to the underlying GLFW window.
        /// </summary>
        internal WindowHandle Handle { get; }

        /// <summary>
        /// Gets the graphics context associated with this window.
        /// </summary>
        public GraphicsContext Graphics { get; }

        /// <summary>
        /// Gets the surface representing this window.
        /// </summary>
        public Surface Surface { get; }

        public event Action<IScreen, IntSize> Resized;

        public event Action<IScreen, IntSize> SurfaceResized;

        /// <summary>
        /// Gets a value that determines if this window been disposed.
        /// </summary>
        public bool IsDisposed { get; private set; }

        /// <summary>
        /// Gets a value that determines if this window is in fullscreen.
        /// </summary>
        public bool IsFullscreen => _fullscreenDisplay != null;

        /// <summary>
        /// Gets the fullscreen display this window is on. If not fullscreen, this value is null.
        /// </summary>
        public Display FullscreenDisplay => _fullscreenDisplay;

        #region Window Properties (Position, Size, State, etc)

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
        /// Gets or sets the window size (in screen coordinates).
        /// </summary>
        public IntSize Size
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

        /// <summary>
        /// Gets or sets the window title text.
        /// </summary>
        public string Title
        {
            get => _title;
            set => Application.Invoke(() => Glfw.SetWindowTitle(Handle, _title = value));
        }

        /// <summary>
        /// Gets or sets a value that determines if the window is allowed to be resized.
        /// </summary>
        public bool IsResizable
        {
            get => Application.Invoke(() => Glfw.GetWindowAttribute(Handle, WindowAttribute.Resizable) != 0);
            set => Application.Invoke(() => Glfw.SetWindowAttribute(Handle, WindowAttribute.Resizable, value));
        }

        /// <summary>
        /// Gets or sets a value that determines if the window is visible.
        /// </summary>
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

        /// <summary>
        /// Gets a value that determines if this window has been closed.
        /// </summary>
        public bool IsClosed { get; private set; }

        #endregion

        #region Window Methods (Focus, Maximize, Close, etc)

        /// <summary>
        /// Cause this window to gain focus.
        /// </summary>
        public void Focus()
        {
            Application.Invoke(() => Glfw.FocusWindow(Handle));
        }

        /// <summary>
        /// Cause this window to become maximized.
        /// </summary>
        public void Maximize()
        {
            if (State == WindowState.Fullscreen) { EndFullscreen(); }
            Application.Invoke(() => Glfw.MaximizeWindow(Handle));
        }

        /// <summary>
        /// Cause this window to become minimized.
        /// </summary>
        public void Minimize()
        {
            if (State == WindowState.Fullscreen) { EndFullscreen(); }
            Application.Invoke(() => Glfw.IconifyWindow(Handle));
        }

        /// <summary>
        /// Cause this window to be restored (neither maximized or minimized).
        /// </summary>
        public void Restore()
        {
            if (State == WindowState.Fullscreen) { EndFullscreen(); }
            Application.Invoke(() => Glfw.RestoreWindow(Handle));
        }

        /// <summary>
        /// Cause this window to close.
        /// </summary>
        public void Close()
        {
            OnClosed();
        }

        #endregion

        #region Window Events (Closing, etc)

        /// <summary>
        /// Event called when the screen is trying to close.
        /// Returning <c>false</c> will prevent the screen from closing, if possible.
        /// </summary>
        public event Func<IScreen, bool> Closing;

        /// <summary>
        /// Event called when the screen has closed.
        /// </summary>
        public event Action<IScreen> Closed;

        /// <summary>
        /// Call this function raise the <see cref="Closing"/> event.
        /// </summary>
        internal bool OnClosing()
        {
            // note: defaults to true to allow the screen to close without handlers.
            return Closing?.Invoke(this) ?? true;
        }

        /// <summary>
        /// Call this function raise the <see cref="Closed"/> event.
        /// </summary>
        internal void OnClosed()
        {
            Dispose();
        }

        #endregion

        #region Fullscreen Manipulation

        internal void BeginFullscreen(Display display)
        {
            BeginFullscreen(display, display.CurrentMode);
        }

        internal void BeginFullscreen(Display display, VideoMode mode)
        {
            Application.Invoke(() =>
            {
                // If not already fullscreen, keep record of the current bounds
                if (State != WindowState.Fullscreen) { _restoreBounds = Bounds; }

                // If unset, use the display refresh rate
                var refreshRate = mode.RefreshRate;
                if (refreshRate == 0) { refreshRate = display.RefreshRate; }

                // Enable fullscreen
                Glfw.SetWindowMonitor(Handle, display.MonitorHandle, 0, 0, mode.Width, mode.Height, refreshRate);
            });

            // Remember which display
            _fullscreenDisplay = display;
        }

        internal void EndFullscreen()
        {
            Application.Invoke(() =>
            {
                // Disable fullscreen, and restore bounds to pre-fullscreen bounds
                Glfw.SetWindowMonitor(Handle, MonitorHandle.None, _restoreBounds.X, _restoreBounds.Y, _restoreBounds.Width, _restoreBounds.Height, -1);
            });

            // Forget the fullscreen display
            _fullscreenDisplay = null;
        }

        #endregion

        private static Image[] CreateDefaultIcons()
        {
            return Generate().ToArray();

            static IEnumerable<Image> Generate()
            {
                var icon = new Image("Meadows/Embedded/icon.png");

                while (icon.Width > 32 && icon.Height > 32)
                {
                    yield return icon;
                    icon = Image.Downsample(icon);
                }

                yield return icon;
            }
        }

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

                    if (icon.Width != icon.Height)
                    {
                        throw new ArgumentException("Icon must be of square dimensions.");
                    }

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
        public void SetIcons(Image icon)
        {
            if (icon.Width != icon.Height)
            {
                throw new ArgumentException("Icon must be of square dimensions.");
            }

            var icons = new List<Image>() { icon };
            var count = (int) Calc.Max(Calc.Log(icon.Width) - 4, 0);
            for (var i = 0; i < count; i++)
            {
                var resized = Image.Downsample(icons[^1]);
                icons.Add(resized);
            }

            SetIcons(icons.ToArray());
        }

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

        public void Refresh()
        {
            // Process input events
            _inputSource.ProcessEvents();

            // Complete frame
            Graphics.CompleteFrame();
        }

        /// <summary>
        /// Gets an array of the currently opened windows.
        /// </summary>
        public static Window[] GetWindows()
        {
            // Returns an array copy of the windows list?
            // This might be inefficient?
            return Application.Windows.ToArray();
        }

        #region Input Source (pass-through)

        void IInputSource.Activate() { _inputSource.Activate(); }

        void IInputSource.Deactivate() { _inputSource.Deactivate(); }

        string IInputSource.TextInput => _inputSource.TextInput;

        bool IInputSource.SupportsSoftwareKeyboard => _inputSource.SupportsSoftwareKeyboard;

        void IInputSource.ShowSoftwareKeyboard()
        {
            _inputSource.ShowSoftwareKeyboard();
        }

        void IInputSource.HideSoftwareKeyboard()
        {
            _inputSource.HideSoftwareKeyboard();
        }

        bool IInputSource.TryGetKey(Key key, out ButtonState state)
        {
            return _inputSource.TryGetKey(key, out state);
        }

        Vector IInputSource.MousePosition => _inputSource.MousePosition;

        Vector IInputSource.MouseDelta => _inputSource.MouseDelta;

        Vector IInputSource.MouseScroll => _inputSource.MouseScroll;

        bool IInputSource.TryGetButton(MouseButton button, out ButtonState state)
        {
            return _inputSource.TryGetButton(button, out state);
        }

        #endregion

        private sealed class InputSource : IInputSource
        {
            public readonly Queue<MouseButtonEvent> MouseButtonEvents = new();
            public readonly Queue<MouseScrollEvent> MouseScrollEvents = new();
            public readonly Queue<MouseMoveEvent> MouseMoveEvents = new();

            public readonly Queue<CharacterEvent> CharacterEvents = new();
            public readonly Queue<KeyEvent> KeyEvents = new();

            private readonly Dictionary<MouseButton, ButtonState> _mouseStates = new();
            private readonly Dictionary<Key, ButtonState> _keyStates = new();

            private string _textInput;

            private Vector _mouseScroll;
            private Vector _mousePosition;
            private Vector _mouseDelta;

            public InputSource(Window window)
            {
                Window = window ?? throw new ArgumentNullException(nameof(window));
            }

            public Window Window { get; }

            public bool Active { get; internal set; }

            public void Activate()
            {
                Active = true;
            }

            public void Deactivate()
            {
                Active = false;

                // Clear events
                MouseButtonEvents.Clear();
                MouseScrollEvents.Clear();
                MouseMoveEvents.Clear();
                CharacterEvents.Clear();
                KeyEvents.Clear();

                // Clear mouse state
                _mousePosition = Vector.Zero;
                _mouseScroll = Vector.Zero;
                _mouseDelta = Vector.Zero;
                _mouseStates.Clear();

                // Clear keyboard state
                _textInput = string.Empty;
                _keyStates.Clear();
            }

            internal void ProcessEvents()
            {
                ProcessKeyboard();
                ProcessMouse();

                void ProcessMouse()
                {
                    RemoveRecentFlag(_mouseStates);

                    _mouseScroll = Vector.Zero;
                    _mouseDelta = Vector.Zero;

                    lock (MouseButtonEvents)
                    {
                        while (MouseButtonEvents.Count > 0)
                        {
                            var ev = MouseButtonEvents.Dequeue();

                            // If the event is reporting a new press (ie, 'now')
                            if (ev.State.HasFlag(ButtonState.Recent))
                            {
                                _mouseStates[ev.Button] = ev.State;
                            }
                        }
                    }

                    lock (MouseScrollEvents)
                    {
                        while (MouseScrollEvents.Count > 0)
                        {
                            var ev = MouseScrollEvents.Dequeue();
                            _mouseScroll += ev.Scroll;
                        }
                    }

                    lock (MouseMoveEvents)
                    {
                        while (MouseMoveEvents.Count > 0)
                        {
                            var ev = MouseMoveEvents.Dequeue();
                            _mousePosition = ev.Position;
                            _mouseDelta += ev.Delta;
                        }
                    }
                }

                void ProcessKeyboard()
                {
                    RemoveRecentFlag(_keyStates);

                    // 
                    while (KeyEvents.Count > 0)
                    {
                        var ev = KeyEvents.Dequeue();

                        // If the event is reporting a new press (ie, 'now')
                        if (ev.State.HasFlag(ButtonState.Recent) || ev.State.HasFlag(ButtonState.Repeat))
                        {
                            _keyStates[ev.Key] = ev.State;
                        }
                    }

                    //
                    _textInput = string.Empty;
                    while (CharacterEvents.Count > 0)
                    {
                        var ev = CharacterEvents.Dequeue();
                        _textInput += ev.Character;
                    }
                }
            }

            private static void RemoveRecentFlag<T>(Dictionary<T, ButtonState> states)
            {
                foreach (var key in states.Keys)
                {
                    var state = states[key];
                    if (state.HasFlag(ButtonState.Recent))
                    {
                        states[key] = state & ~ButtonState.Recent;
                    }
                    if (state.HasFlag(ButtonState.Repeat))
                    {
                        states[key] = state & ~ButtonState.Repeat;
                    }
                }
            }

            #region Keyboard

            public bool SupportsSoftwareKeyboard => false;

            public string TextInput => _textInput;

            public void ShowSoftwareKeyboard()
            {
                throw new NotImplementedException("Unable to show software keyboard, not supported.");
            }

            public void HideSoftwareKeyboard()
            {
                throw new NotImplementedException("Unable to hide software keyboard, not supported.");
            }

            public bool TryGetKey(Key key, out ButtonState state)
            {
                // Try to get state for button
                return _keyStates.TryGetValue(key, out state);
            }

            #endregion

            #region Mouse

            public Vector MousePosition => _mousePosition;

            public Vector MouseDelta => _mouseDelta;

            public Vector MouseScroll => _mouseScroll;

            public bool TryGetButton(MouseButton button, out ButtonState state)
            {
                // Try to get state for button
                return _mouseStates.TryGetValue(button, out state);
            }

            #endregion
        }
    }
}
