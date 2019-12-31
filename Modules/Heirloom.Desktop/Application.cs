using System;
using System.Collections.Generic;

using Heirloom.OpenGLES;

namespace Heirloom.Desktop
{
    public static class Application
    {
        private const double WaitEventsTimeout = 1.0 / 60.0;

        private static ConsumerQueue _invokeQueue;
        private static List<Window> _windows;

        private static readonly object _lock = new object();

        internal static WindowHandle ShareContext;

        /// <summary>
        /// Gets a value that determines if transparent framebuffers supported on this device/platform.
        /// </summary>
        public static bool SupportsTransparentFramebuffer { get; private set; }

        /// <summary>
        /// Gets a value determining if the application is ready to process window events and other desktop features.
        /// </summary>
        public static bool IsInitialized { get; private set; } = false;

        /// <summary>
        /// Gets a read-only list of currently opened windows.
        /// </summary>
        public static IReadOnlyList<Window> Windows
        {
            get
            {
                EnsureReady();
                return _windows;
            }
        }

        /// <summary>
        /// Initializes necessary windowing utilities and then executes the specified function when ready.
        /// This function is blocking.
        /// </summary>
        /// <param name="initialize"></param>
        /// <see cref="IsInitialized"/>
        public static void Run(Action initialize)
        {
            if (IsInitialized)
            {
                throw new InvalidOperationException("Application has already been initialized and is currently running.");
            }

            lock (_lock)
            {
                // Initialize GLFW
                if (!Glfw.Initialize())
                {
                    Console.WriteLine("Unable to initialize GLFW");
                    return;
                }

                // Set to use OpenGL 3.2 core (forward compatible)
                Glfw.SetWindowCreationHint(WindowAttribute.ClientApi, (int) ClientApi.OpenGL);
                Glfw.SetWindowCreationHint(WindowAttribute.OpenGLProfile, (int) OpenGLProfile.Core);
                Glfw.SetWindowCreationHint(WindowAttribute.OpenGLForwardCompatibility, true);
                Glfw.SetWindowCreationHint(WindowAttribute.ContextVersionMajor, 3);
                Glfw.SetWindowCreationHint(WindowAttribute.ContextVersionMinor, 2);

                // Create "dummy" window, the GL context sharing window
                Glfw.SetWindowCreationHint(WindowAttribute.Visible, false);
                Glfw.SetWindowCreationHint(WindowAttribute.TransparentFramebuffer, true);
                ShareContext = Glfw.CreateWindow(256, 256, "GLFW Background Window");

                // Determine if transparent framebuffers are possible
                SupportsTransparentFramebuffer = Glfw.GetWindowAttribute(ShareContext, WindowAttribute.TransparentFramebuffer) != 0;

                // Set default window creation hints
                Glfw.SetWindowCreationHint(WindowAttribute.FocusOnShow, true);
                Glfw.SetWindowCreationHint(WindowAttribute.Visible, true);

                // Bind share context and load GL functions
                Glfw.MakeContextCurrent(ShareContext);
                GL.LoadFunctions(Glfw.GetProcAddress);

                // Release context for use as a sharing context.
                Glfw.MakeContextCurrent(WindowHandle.None);

                // 
                _invokeQueue = new ConsumerQueue();
                _windows = new List<Window>();
            }

            IsInitialized = true;
            initialize();

            // While any window is open
            while (Windows.Count > 0)
            {
                // Poll window events
                Glfw.WaitEventsTimeout(WaitEventsTimeout);

                // Process invoke queue
                _invokeQueue.ProcessJobs();

                // Sleep thread, prevent tight spinning
                // NOTE: Removed because WaitEventsTimeout above should block...
                // Thread.Sleep(1);
            }

            // Shutdown GLFW
            Glfw.Terminate();
            IsInitialized = false;
        }

        /// <summary>
        /// Invoke an action on the window thread.
        /// </summary>
        internal static void Invoke(Action action, bool blocking = true)
        {
            if (blocking) { _invokeQueue.Invoke(action); }
            else { _invokeQueue.InvokeLater(action); }
        }

        /// <summary>
        /// Invoke an action on the window thread and wait for a return value.
        /// </summary>
        internal static T Invoke<T>(Func<T> action)
        {
            return _invokeQueue.Invoke(action);
        }

        /// <summary>
        /// Ensures the application has called <see cref="Run(Action)"/>.
        /// </summary>
        private static void EnsureReady()
        {
            if (IsInitialized == false)
            {
                throw new InvalidOperationException($"You must execute the application via {nameof(Application)}.{nameof(Run)}.");
            }
        }

        internal static void AddWindow(Window window)
        {
            EnsureReady();

            lock (_lock)
            {
                _windows.Add(window);
            }
        }

        internal static void RemoveWindow(Window window)
        {
            EnsureReady();

            lock (_lock)
            {
                _windows.Remove(window);
            }
        }
    }
}
