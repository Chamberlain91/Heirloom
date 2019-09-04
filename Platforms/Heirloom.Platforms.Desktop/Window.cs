using System;
using System.Collections.Generic;
using System.Runtime.CompilerServices;

using Heirloom.Drawing;
using Heirloom.GLFW;
using Heirloom.Input;
using Heirloom.Math;
using Heirloom.Platforms.Desktop.Input;

namespace Heirloom.Platforms.Desktop
{
    public class Window
    {
        private Glfw.Window _window;
        private IntVector _position;
        private IntSize _framebufferSize;
        private IntSize _size;
        private Vector _contentScale;
        private string _title;

        private WindowState _state;
        private bool _isResizable;
        private bool _isDecorated;

        private IntRectangle _fullscreenRestore;

        private Glfw.WindowCloseCallback _closeCallback;
        private Glfw.WindowFocusCallback _focusCallback;
        private Glfw.WindowSizeCallback _sizeCallback;
        private Glfw.FramebufferSizeCallback _framebufferSizeCallback;
        private Glfw.WindowContentScaleCallback _contentScaleCallback;
        private Glfw.WindowMaximizeCallback _maximizeCallback;
        private Glfw.WindowIconifyCallback _iconifyCallback;
        private Glfw.WindowPositionCallback _positionCallback;
        private Glfw.WindowRefreshCallback _refreshCallback;

        #region Constructors

        public unsafe Window(int width, int height, string title, bool vsync = false, bool transparentFramebuffer = false)
        {
            var context = ContextManager.Instance;

            VSync = vsync;

            // 
            _size = new IntSize(width, height);
            _title = title;

            // Execute on the window thread
            _window = ContextManager.Invoke(() =>
            {
                // Must release context to construct shared context. (why? find documentation)
                Glfw.MakeContextCurrent(Glfw.Window.None);

                // 
                Glfw.WindowHint(Glfw.TRANSPARENT_FRAMEBUFFER, transparentFramebuffer ? 1 : 0);
                Glfw.WindowHint(Glfw.SCALE_TO_MONITOR, 1);

                // Construct window with shared context
                var window = Glfw.CreateWindow(width, height, title, null, context.ShareContext);

                // Make context current again
                Glfw.MakeContextCurrent(context.ShareContext);

                // Detect if transparency was really accepted
                SupportsTransparentFramebuffer = Glfw.GetWindowAttrib(window, Glfw.TRANSPARENT_FRAMEBUFFER) == 1;

                // Get initial framebuffer size
                Glfw.GetFramebufferSize(window, out var fw, out var fh);
                _framebufferSize = new IntSize(fw, fh);

                // Get the content scale
                Glfw.GetWindowContentScale(window, out var cx, out var cy);
                _contentScale = new Vector(cx, cy);

                // Get initial window position
                Glfw.GetWindowPos(window, out var wx, out var wy);
                _position = (wx, wy);

                // Event when window is closing
                Glfw.SetWindowCloseCallback(window, _closeCallback = (_) =>
                {
                    // Invoke closing event, determines if it really should close.
                    if (OnClosing())
                    {
                        // Hide window and terminate thread
                        RemoveWindow(this);
                        Glfw.HideWindow(window);
                        IsClosed = true;

                        // Close window
                        RenderContext.Dispose();
                        OnClosed();
                    }
                    else
                    {
                        // Mark GLFW to not close the window
                        Glfw.SetWindowShouldClose(window, 0);
                        IsClosed = false;
                    }
                });

                // Event when window is resized
                Glfw.SetWindowSizeCallback(window, _sizeCallback = (_, w, h) =>
                {
                    // Set the new size
                    _size = new IntSize(w, h);

                    // Invoke event
                    OnResized();
                });

                // Event when framebuffer is resized
                Glfw.SetFramebufferSizeCallback(window, _framebufferSizeCallback = (_, w, h) =>
                {
                    // Set the new framebuffer size
                    _framebufferSize = new IntSize(w, h);

                    // Invoke event
                    OnFramebufferResized();
                });

                // Event when content scale changes
                Glfw.SetWindowContentScaleCallback(window, _contentScaleCallback = (_, scaleX, scaleY) =>
                {
                    // Set the new framebuffer size
                    _contentScale = new Vector(scaleX, scaleY);

                    // Invoke event
                    OnFramebufferResized();
                });

                // Event for moving the window
                Glfw.SetWindowPositionCallback(window, _positionCallback = (_, x, y) =>
                {
                    // Set the new position
                    _position = new IntVector(x, y);

                    // Invoke event
                    OnMoved();
                });

                // Event for window focus change
                Glfw.SetWindowFocusCallback(window, _focusCallback = (_, state) =>
                {
                    if (state == Glfw.True) { OnGainedFocus(); }
                    else { OnLostFocus(); }
                });

                // Event for minimize/restore
                Glfw.SetWindowIconifyCallback(window, _iconifyCallback = (_, state) =>
                {
                    if (state == Glfw.True) { OnStateChanged(WindowState.Minimized); }
                    else { OnStateChanged(WindowState.Normal); }
                });

                // Event for maximize/restore
                Glfw.SetWindowMaximizeCallback(window, _maximizeCallback = (_, state) =>
                {
                    if (state == Glfw.True) { OnStateChanged(WindowState.Maximized); }
                    else { OnStateChanged(WindowState.Normal); }
                });

                // Event for maximize/restore
                Glfw.SetWindowRefreshCallback(window, _refreshCallback = (_) =>
                {
                    OnRefreshNeeded();
                });

                return window;
            });

            // Create input handlers
            Keyboard = new KeyboardDevice(_window);
            Mouse = new MouseDevice(_window);

            // Create rendering context
            RenderContext = context.CreateRenderContext(this);

            //
            AddWindow(this);
        }

        ~Window()
        {
            GC.KeepAlive(_positionCallback);
            GC.KeepAlive(_maximizeCallback);
            GC.KeepAlive(_iconifyCallback);
            GC.KeepAlive(_refreshCallback);
            GC.KeepAlive(_closeCallback);
            GC.KeepAlive(_focusCallback);
            GC.KeepAlive(_framebufferSizeCallback);
            GC.KeepAlive(_sizeCallback);
        }

        #endregion

        #region Properties

        internal Glfw.Window Native => _window;

        public RenderContext RenderContext { get; }

        public Keyboard Keyboard { get; }

        public Mouse Mouse { get; }

        public Gamepad Gamepad { get; }

        public bool VSync { get; }

        /// <summary>
        /// Does this window support a transparent framebuffer?
        /// </summary>
        public bool SupportsTransparentFramebuffer { get; private set; }

        /// <summary>
        /// Has the window been closed?
        /// </summary>
        public bool IsClosed { get; private set; }

        /// <summary>
        /// Gets or sets the window title text.
        /// </summary>
        public string Title
        {
            get => _title;

            set
            {
                _title = value;
                ContextManager.Invoke(()
                    => Glfw.SetWindowTitle(_window, _title));
            }
        }

        /// <summary>
        /// Gets or sets the size of the window.
        /// </summary>
        public IntSize Size
        {
            get => _size;

            set
            {
                _size = value;
                ContextManager.Invoke(()
                    => Glfw.SetWindowSize(_window, _size.Width, _size.Height));
            }
        }

        /// <summary>
        /// Gets the size of the framebuffer on this window.
        /// </summary>
        internal IntSize FramebufferSize => _framebufferSize;

        /// <summary>
        /// The scaling factor for this particular window.
        /// </summary>
        public Vector ContentScale => _contentScale;

        /// <summary>
        /// Gets or set the height of the window.
        /// </summary>
        public int Height
        {
            get => _size.Height;

            set
            {
                _size = new IntSize(Width, value);
                ContextManager.Invoke(()
                    => Glfw.SetWindowSize(_window, _size.Width, _size.Height));
            }
        }

        /// <summary>
        /// Gets or sets the width of the window.
        /// </summary>
        public int Width
        {
            get => _size.Width;

            set
            {
                _size = new IntSize(value, Height);
                ContextManager.Invoke(()
                    => Glfw.SetWindowSize(_window, _size.Width, _size.Height));
            }
        }

        /// <summary>
        /// Gets or sets the windows position.
        /// </summary>
        public IntVector Position
        {
            get => _position;

            set
            {
                _position = value;
                ContextManager.Invoke(()
                    => Glfw.SetWindowPos(_window, _position.X, _position.Y));
            }
        }

        /// <summary>
        /// Gets or sets the windows state.
        /// </summary>
        public WindowState State
        {
            get => _state;

            set
            {
                if (_state != value)
                {
                    // 
                    _state = value;

                    // 
                    ContextManager.Invoke(() =>
                    {
                        switch (_state)
                        {
                            case WindowState.Maximized:
                                Glfw.MaximizeWindow(_window);
                                break;

                            case WindowState.Minimized:
                                Glfw.IconifyWindow(_window);
                                break;

                            case WindowState.Normal:
                                Glfw.RestoreWindow(_window);
                                break;
                        }
                    });
                }
            }
        }

        /// <summary>
        /// Gets or sets if this window is decorated (aka, window borders).
        /// </summary>
        public bool IsDecorated
        {
            get => _isDecorated;

            set
            {
                _isDecorated = value;
                ContextManager.Invoke(()
                    => Glfw.SetWindowAttrib(_window, Glfw.DECORATED, value ? 1 : 0));
            }
        }

        /// <summary>
        /// Gets or sets if this window is allowed to be resized.
        /// </summary>
        public bool IsResizeable
        {
            get => _isResizable;

            set
            {
                _isResizable = value;
                ContextManager.Invoke(()
                    => Glfw.SetWindowAttrib(_window, Glfw.RESIZABLE, value ? 1 : 0));
            }
        }

        #endregion

        #region Events

        /// <summary>
        /// Event triggered when the size of the window changes.
        /// </summary>
        public event EventHandler Resized;

        /// <summary>
        /// Event triggered when the size of the default surface changes.
        /// </summary>
        public event EventHandler SurfaceResized;

        /// <summary>
        /// Event triggered when the content scale (dpi scaling) changes on this window.
        /// </summary>
        public event EventHandler ContentScaleChanged;

        /// <summary>
        /// Event triggered when the window is moved.
        /// </summary>
        public event EventHandler Moved;

        /// <summary>
        /// Event triggered when the window attempting to close.
        /// </summary>
        public event EventHandler<ClosingEventArgs> Closing;

        /// <summary>
        /// Event triggered when the window has closed.
        /// </summary>
        public event EventHandler Closed;

        /// <summary>
        /// Event triggered when the window has gained focus.
        /// </summary>
        public event EventHandler GainedFocus;

        /// <summary>
        /// Event triggered when the window has lost focus.
        /// </summary>
        public event EventHandler LostFocus;

        /// <summary>
        /// Event triggered when the window is put into a minimized state.
        /// </summary>
        public event EventHandler Minimized;

        /// <summary>
        /// Event triggered when the window is put into a maximized state.
        /// </summary>
        public event EventHandler Maximized;

        /// <summary>
        /// Event triggered when the window is restored from a maximized or minimized state.
        /// </summary>
        public event EventHandler Restored;

        /// <summary>
        /// Event triggered when the window needs to be redrawn due to a resize or other form of damage.
        /// </summary>
        public event EventHandler RefreshNeeded;

        #endregion

        #region Event Triggers

        protected virtual bool OnClosing()
        {
            var args = new ClosingEventArgs();
            Closing?.Invoke(this, args);

            return !args.PreventClose;
        }

        protected virtual void OnClosed()
        {
            Closed?.Invoke(this, EventArgs.Empty);
        }

        protected virtual void OnResized()
        {
            Resized?.Invoke(this, EventArgs.Empty);
        }

        protected virtual void OnFramebufferResized()
        {
            SurfaceResized?.Invoke(this, EventArgs.Empty);
        }

        protected virtual void OnContentScaleChanged()
        {
            ContentScaleChanged?.Invoke(this, EventArgs.Empty);
        }

        protected virtual void OnMoved()
        {
            Moved?.Invoke(this, EventArgs.Empty);
        }

        protected virtual void OnLostFocus()
        {
            LostFocus?.Invoke(this, EventArgs.Empty);
        }

        protected virtual void OnGainedFocus()
        {
            GainedFocus?.Invoke(this, EventArgs.Empty);
        }

        protected virtual void OnStateChanged(WindowState state)
        {
            _state = state;

            switch (state)
            {
                case WindowState.Maximized:
                    Maximized?.Invoke(this, EventArgs.Empty);
                    break;

                case WindowState.Minimized:
                    Minimized?.Invoke(this, EventArgs.Empty);
                    break;

                case WindowState.Normal:
                    Restored?.Invoke(this, EventArgs.Empty);
                    break;
            }
        }

        protected virtual void OnRefreshNeeded()
        {
            RefreshNeeded?.Invoke(this, EventArgs.Empty);
        }

        #endregion

        public void Close()
        {
            // Invoke the close callback to manually close the window
            _closeCallback(_window);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public void Maximize() { State = WindowState.Maximized; }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public void Minimize() { State = WindowState.Minimized; }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public void Restore() { State = WindowState.Normal; }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public void RequestAttention()
        {
            ContextManager.Invoke(()
                => Glfw.RequestWindowAttention(_window));
        }

        /// <summary>
        /// Enable fullscreen with the current video mode of the screen.
        /// </summary>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public unsafe void SetFullscreen(Monitor screen)
        {
            if (screen == null) { throw new ArgumentNullException(nameof(screen)); }

            SetFullscreen(screen.CurrentVideoMode);
        }

        /// <summary>
        /// Enable fullscreen with the given video mode.
        /// </summary>
        public unsafe void SetFullscreen(VideoMode mode)
        {
            if (mode == null) { throw new ArgumentNullException(nameof(mode)); }

            // If not already in fullscreen, we need to keep the window dimensions
            if (State != WindowState.Fullscreen)
            {
                // Record the rectangle of the current window
                _fullscreenRestore = new IntRectangle(Position, Size);
            }

            // Set to fullscreen
            ContextManager.Invoke(()
                => Glfw.SetWindowMonitor(_window, mode.Monitor.Native, 0, 0, mode.Width, mode.Height, mode.RefreshRate));

            OnStateChanged(WindowState.Fullscreen);
        }

        /// <summary>
        /// Disable fullscreen, restoring to a regular window.
        /// </summary>
        public unsafe void ExitFullscreen()
        {
            if (State == WindowState.Fullscreen)
            {
                // Decompose rect for readability
                var (x, y, w, h) = _fullscreenRestore;

                // Restore from fullscreen
                ContextManager.Invoke(()
                    => Glfw.SetWindowMonitor(_window, null, x, y, w, h, Glfw.DONT_CARE));

                // 
                OnStateChanged(WindowState.Normal);
            }
        }

        ///// <summary>
        ///// Causes the window to be rendered and swap buffers (aka, put the image on screen).
        ///// </summary>
        //public void Refresh()
        //{
        //    // Flush all pending work
        //    _context.Flush();

        //    /* 
        //     * Conceptually, I should be able to use InvokeLater for every case, as logically
        //     * speaking its a queue. I should be able to submit to the queue without waiting
        //     * and still have expected ordering. When using InvokeLater in a single window case
        //     * an unexpected occasional freeze seems to occur and I am not sure why. The need
        //     * for the non-blocking invoke is to avoid waiting for each windows swap to finish
        //     * in a multi-windowed application. This causes a significant slow down in a
        //     * single threaded application driving the windows as I've been implementing it as
        //     * Draw A, B, C, Swap A, B, C.
        //     */

        //    // swap buffers (aka, puts the rendered image on screen)
        //    if (GraphicsContext.Instance.Windows.Count > 1) { _context.InvokeLater(() => Glfw.SwapBuffers(_window)); }
        //    else { _context.Invoke(() => Glfw.SwapBuffers(_window)); }

        //    // Mark that we refreshed the window
        //    _refreshRate.Tick();
        //}

        /// <summary>
        /// Poll for window events (resize, keyboard, etc). Must be called on the main thread!.
        /// </summary>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static void PollEvents()
        {
            // 
            ContextManager.Instance.PollEventsInternal();
        }

        #region Window List

        private static readonly List<Window> _windows;

        static Window()
        {
            _windows = new List<Window>();
        }

        public static IReadOnlyList<Window> Windows => _windows;

        internal static void AddWindow(Window window)
        {
            _windows.Add(window);
        }

        internal static void RemoveWindow(Window window)
        {
            _windows.Remove(window);
        }

        #endregion
    }
}
