using System;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using System.Threading;

using Heirloom.Drawing;
using Heirloom.GLFW;
using Heirloom.OpenGLES;

namespace Heirloom.Platforms.Desktop
{
    internal abstract unsafe class GraphicsContext
    {
        private readonly Queue<Action> _invokeQueue;
        private readonly GraphicsAPI _graphicsApi;
        private Thread _thread;

        // GLFW
        private Glfw.MonitorCallback _monitorCallback;
        private Glfw.ErrorCallback _errorCallback;
        private Glfw.Window _shareContext;

        // Windows
        private readonly List<Window> _windows;

        #region Constructors

        protected GraphicsContext(GraphicsAPI api)
        {
            _windows = new List<Window>();
            _invokeQueue = new Queue<Action>();
            _graphicsApi = api;
        }

        ~GraphicsContext()
        {
            Glfw.Terminate();

            // 
            GC.KeepAlive(_monitorCallback);
            GC.KeepAlive(_errorCallback);
        }

        #endregion

        internal Glfw.Window ShareContext => _shareContext;

        internal abstract RenderContext CreateRenderingContext(Window window);

        private void Initialize()
        {
            // 
            _thread = Thread.CurrentThread;

            // -- Initialize GLFW Library

            // Print GLFW Errors
            Glfw.SetErrorCallback(_errorCallback = (code, message) =>
            {
                Console.ForegroundColor = ConsoleColor.Red;
                Console.WriteLine($"GLFW Error ({code}): {message}");
                Console.ResetColor();
            });

            if (Glfw.Init())
            {
                // Configure context creation hints
                switch (_graphicsApi)
                {
                    case GraphicsAPI.OpenGL:

                        // 
                        Glfw.WindowHint(Glfw.RED_BITS, 8);
                        Glfw.WindowHint(Glfw.BLUE_BITS, 8);
                        Glfw.WindowHint(Glfw.GREEN_BITS, 8);
                        Glfw.WindowHint(Glfw.ALPHA_BITS, 8);
                        Glfw.WindowHint(Glfw.STENCIL_BITS, 0);
                        Glfw.WindowHint(Glfw.DEPTH_BITS, 0);
                        Glfw.WindowHint(Glfw.SAMPLES, 0);

                        // Configure window hints for context creation
                        Glfw.WindowHint(Glfw.CLIENT_API, Glfw.OPENGL_API);
                        Glfw.WindowHint(Glfw.OPENGL_PROFILE, Glfw.OPENGL_CORE_PROFILE);
                        Glfw.WindowHint(Glfw.OPENGL_FORWARD_COMPAT, Glfw.True);
                        Glfw.WindowHint(Glfw.CONTEXT_VERSION_MAJOR, 3);
                        Glfw.WindowHint(Glfw.CONTEXT_VERSION_MINOR, 2);
                        break;

                    case GraphicsAPI.Vulkan:
                        Glfw.WindowHint(Glfw.CLIENT_API, Glfw.NO_API);
                        throw new NotImplementedException("Vulkan backend is not currently implemented! Sorry.");

                    default: // Unknown backend type
                        throw new ArgumentException("Unable to configure backend, unknown type.");
                }
            }
            else
            {
                throw new InvalidOperationException("Unable to initialize GLFW!");
            }

            // GLFW successfully initialized
            Console.WriteLine($"Initialized Drawing and Input Backends ({_graphicsApi})");

            // -- Initialize Screens (Monitors)

            // Monitor list change callback
            Glfw.SetMonitorCallback(_monitorCallback = (m, action) =>
            {
                if (action == Glfw.CONNECTED) { Screen.AddScreen(m); }
                else if (action == Glfw.DISCONNECTED) { Screen.RemoveScreen(m); }
            });

            // Gather existing monitors
            var monitors = Glfw.GetMonitors(out var count);
            for (var i = 0; i < count; i++)
            {
                Screen.AddScreen(monitors[i]);
            }

            // todo: opengl specific code at desktop abstraction, consider moving to GLContext somehow?
            if (_graphicsApi == GraphicsAPI.OpenGL)
            {
                // -- Create Background GL Context
                _shareContext = Glfw.CreateWindow(1, 1, "GLFW BACKGROUND WINDOW", null, Glfw.Window.None);
                Glfw.HideWindow(_shareContext); // We don't want this visible

                Glfw.MakeContextCurrent(_shareContext);
                Glfw.SwapInterval(0); // disable-vsync

                // Load OpenGL Functions
                if (!GL.HasLoadedFunctions)
                {
                    GL.LoadFunctions(Glfw.GetProcAddress);
                }
            }
        }

        public void PollEventsInternal()
        {
            // Process actions on this thread
            while (_invokeQueue.Count > 0)
            {
                var action = _invokeQueue.Dequeue();

                lock (action)
                {
                    action(); // perform action
                    Monitor.Pulse(action);
                }
            }

            // 
            Glfw.PollEvents();
        }

        #region Window List

        internal void AddWindow(Window window)
        {
            _windows.Add(window);
        }

        internal void RemoveWindow(Window window)
        {
            _windows.Remove(window);
        }

        internal IReadOnlyList<Window> Windows => _windows;

        #endregion

        #region Invoke (private)

        private void InvokeInternal(Action action)
        {
            if (Thread.CurrentThread == _thread) { action(); }
            else
            {
                lock (action)
                {
                    _invokeQueue.Enqueue(action);
                    Monitor.Wait(action);
                }
            }
        }

        private T InvokeInternal<T>(Func<T> action)
        {
            var retval = default(T);

            InvokeInternal(() =>
            {
                retval = action();
                return; // to prevent recursion on Invoke<T>
            });

            return retval;
        }

        #endregion

        #region Static / Singleton

        private static GraphicsContext _context;

        /// <summary>
        /// Return the default API current platform this application is running on (ie, OpenGL 3.3)
        /// </summary>
        public static GraphicsAPI GetDefaultBackendType()
        {
            return GraphicsAPI.OpenGL;
        }

        /// <summary>
        /// Can be used to initialize a specific backend.
        /// This is not needed if you want to use the platform default.
        /// </summary>
        // todo: make public somehow when implemented vulkan
        public static void Initialize(GraphicsAPI backend)
        {
            // Choose platform specific backend
            if (backend == GraphicsAPI.Automatic)
            {
                backend = GetDefaultBackendType();
            }

            // 
            if (_context == null)
            {
                // Construct context
                _context = CreateBackendContext();
                _context.Initialize();
            }

            GraphicsContext CreateBackendContext()
            {
                // Initialize based on desired backend
                switch (backend)
                {
                    case GraphicsAPI.OpenGL:
                        return new OpenGLGraphicsContext();

                    case GraphicsAPI.Vulkan:
                        // return new VKContext();
                        throw new NotImplementedException();

                    default:
                        throw new InvalidOperationException($"Unknown backend type '{backend}', unable to create rendering context.");
                }
            }
        }

        /// <summary>
        /// Gets the desktop context instance.
        /// This will initialize with defaults if not explicitly initialized beforehand.
        /// </summary>
        internal static GraphicsContext Instance
        {
            get
            {
                // If no context
                if (_context == null)
                {
                    // Initialize context with platform defaults
                    Initialize(GetDefaultBackendType());
                }

                // 
                return _context;
            }
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        internal static void Invoke(Action action)
        {
            Instance.InvokeInternal(action);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        internal static T Invoke<T>(Func<T> action)
        {
            return Instance.InvokeInternal(action);
        }

        #endregion 
    }
}
