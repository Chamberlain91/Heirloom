using System;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using System.Threading;

using Heirloom.Drawing;
using Heirloom.GLFW;
using Heirloom.OpenGLES;

namespace Heirloom.Platforms.Desktop
{
    internal abstract unsafe class ContextManager
    {
        private readonly Queue<Action> _invokeQueue;
        private readonly GraphicsAPI _graphicsApi;
        private Thread _thread;

        // GLFW
        private Glfw.MonitorCallback _monitorCallback;
        private Glfw.ErrorCallback _errorCallback;
        private Glfw.Window _shareContext;

        #region Constructors

        protected ContextManager(GraphicsAPI api)
        {
            _invokeQueue = new Queue<Action>();
            _graphicsApi = api;
        }

        ~ContextManager()
        {
            Glfw.Terminate();

            // 
            GC.KeepAlive(_monitorCallback);
            GC.KeepAlive(_errorCallback);
        }

        #endregion

        internal Glfw.Window ShareContext => _shareContext;

        internal abstract RenderContext CreateRenderContext(Window window);

        abstract protected void Configure();

        abstract protected void LoadFunctions();

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
                // We desire windows
                Glfw.WindowHint(Glfw.RED_BITS, 8);
                Glfw.WindowHint(Glfw.BLUE_BITS, 8);
                Glfw.WindowHint(Glfw.GREEN_BITS, 8);
                Glfw.WindowHint(Glfw.ALPHA_BITS, 8);
                Glfw.WindowHint(Glfw.STENCIL_BITS, 0);
                Glfw.WindowHint(Glfw.DEPTH_BITS, 0);
                Glfw.WindowHint(Glfw.SAMPLES, 0);

                // Backend Specific Configuration (GL or VK)
                Configure();
            }
            else
            {
                throw new InvalidOperationException("Unable to initialize GLFW!");
            }

            // -- Create Sharing Context Window
            _shareContext = Glfw.CreateWindow(1, 1, "GLFW CONTEXT SHARE WINDOW", null, Glfw.Window.None);
            Glfw.HideWindow(_shareContext); // We don't want this visible

            // 
            LoadFunctions();

            // -- Initialize Screens (Monitors)

            // Monitor list change callback
            Glfw.SetMonitorCallback(_monitorCallback = (m, action) =>
            {
                if (action == Glfw.CONNECTED) { Monitor.AddMonitor(m); }
                else if (action == Glfw.DISCONNECTED) { Monitor.RemoveMonitor(m); }
            });

            // Gather existing monitors
            var monitors = Glfw.GetMonitors(out var count);
            for (var i = 0; i < count; i++)
            {
                Monitor.AddMonitor(monitors[i]);
            }

            // GLFW successfully initialized
            Console.WriteLine($"Initialized GLFW ({_graphicsApi})");
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
                    System.Threading.Monitor.Pulse(action);
                }
            }

            // 
            Glfw.PollEvents();
        }

        #region Invoke (private)

        private void InvokeInternal(Action action)
        {
            if (Thread.CurrentThread == _thread) { action(); }
            else
            {
                lock (action)
                {
                    _invokeQueue.Enqueue(action);
                    System.Threading.Monitor.Wait(action);
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

        private static ContextManager _context;

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

            ContextManager CreateBackendContext()
            {
                // Initialize based on desired backend
                switch (backend)
                {
                    case GraphicsAPI.OpenGL:
                        return new GLContextManager();

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
        internal static ContextManager Instance
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
