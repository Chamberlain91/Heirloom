using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Reflection;
using System.Threading;

using Heirloom.Desktop.GLFW;

using Heirloom.Drawing;
using Heirloom.Drawing.OpenGLES;
using Heirloom.Hardware;
using Heirloom.Utilities;

namespace Heirloom.Desktop
{
    public abstract class Application
    {
        private const double WaitEventsTimeout = 1.0 / 200.0;

        private static GraphicsBackend _backend;

        private static ConsumerQueue _actionQueue;

        internal static WindowHandle ShareContext;

        internal static readonly Dictionary<MonitorHandle, Display> Monitors = new Dictionary<MonitorHandle, Display>();
        internal static readonly List<Window> Windows = new List<Window>();

        /// <summary>
        /// Gets a value that determines if the application is running.
        /// </summary>
        public static bool IsRunning { get; private set; }

        /// <summary>
        /// Initialize application systems. This function blocks, processing all window events until all <see cref="Window"/> have been closed.
        /// </summary>
        public static void Run<TApp>() where TApp : new()
        {
            // Ensure we have attempted to call this function recursively
            if (IsRunning) { throw new InvalidOperationException("Application has already been initialized and is currently running."); }

            // Ensure this method has been called on the main thread
            EnsureOnMainThread();

            // Create action queue
            _actionQueue = new ConsumerQueue(Thread.CurrentThread);

            // Mark the application as running
            IsRunning = true;

            lock (Windows) // todo: why this lock?
            {
                // Initialize application systems
                InitializeWindowSystem();
                InitializeGraphics();
                InitializeAudio();

                // Collect System Information
                SystemInformation.CpuInfo = HardwareDetector.DetectCpuInfo();
                SystemInformation.GpuInfo = HardwareDetector.DetectGpuInfo();
            }

            // Create application instance
            Activator.CreateInstance<TApp>();

            // Perform main window / events loop
            ExecuteWindowLoop();

            // Dispose application systems
            DisposeAudio();
            DisposeGraphics();
            DisposeWindowSystem();

            // Mark the application as no longer running
            IsRunning = false;
        }

        /// <summary>
        /// Terminate the desktop application.
        /// </summary>
        public static void Exit()
        {
            throw new NotImplementedException("Exit not implemented yet.");
        }

        #region Thread Invoke

        internal static void Invoke(Action action)
        {
            _actionQueue.Invoke(action);
        }

        internal static T Invoke<T>(Func<T> action)
        {
            return _actionQueue.Invoke(action);
        }

        #endregion

        #region Window System

        private static void InitializeWindowSystem()
        {
            // Initialize GLFW
            if (!Glfw.Initialize())
            {
                throw new InvalidOperationException("Unable to initialize GLFW.");
            }

            // Register monitor callback, invoked when the monitor configuration changes
            Glfw.SetMonitorCallback(OnMonitorCallback);

            // Scan currently connected monitors, synthetically invoking monitor callback
            foreach (var monitor in Glfw.GetMonitors())
            {
                OnMonitorCallback(monitor, ConnectState.Connected);
            }
        }

        private static void OnMonitorCallback(MonitorHandle handle, ConnectState state)
        {
            var name = Glfw.GetMonitorName(handle);

            var primary = Glfw.GetPrimaryMonitor();

            // Connected Monitor
            if (state == ConnectState.Connected)
            {
                // We can only insert if unknown
                if (!Monitors.ContainsKey(handle))
                {
                    Monitors[handle] = new Display(name, handle);
                }
            }
            // Disconnected Monitor
            else
            {
                // We can only remove if known already
                if (Monitors.ContainsKey(handle))
                {
                    Monitors.Remove(handle);
                }
            }

            // Set default monitor
            Display.Primary = Monitors[primary];
        }

        private static void DisposeWindowSystem()
        {
            Glfw.Terminate();
        }

        private static void ExecuteWindowLoop()
        {
            // While any window is open
            while (Windows.Count > 0)
            {
                // Poll window events
                Glfw.WaitEventsTimeout(WaitEventsTimeout);

                // Process invoke queue
                _actionQueue.ProcessJobs();

                // Sleep thread, prevent tight spinning
                // NOTE: Removed because WaitEventsTimeout above should block...
                // Thread.Sleep(1);
            }
        }

        #endregion

        #region Graphics System

        private static void InitializeGraphics()
        {
            // Initialize OpenGL ES Backend
            _backend = InitializeES();

            // todo: Vulkan Backend and ability to switch / auto select
            // _backend = InitiaizeVK();

            // Reset default window creation hints
            Glfw.SetWindowCreationHint(WindowAttribute.FocusOnShow, true);
            Glfw.SetWindowCreationHint(WindowAttribute.Visible, true);

            static GraphicsBackend InitializeES()
            {
                var backend = default(GraphicsBackend);

                // Set GLFW hints to use OpenGL 3.2 core (forward compatible)
                // We are assuming OpenGL is the only implementation relevant here. Sorry Vulkan!
                // In the future if a Vulkan implementation is introduced to Heirloom, we will have to rewrite this
                // initialization procedure.
                Glfw.SetWindowCreationHint(WindowAttribute.ClientApi, (int) ClientApi.OpenGL);
                Glfw.SetWindowCreationHint(WindowAttribute.OpenGLProfile, (int) OpenGLProfile.Core);
                Glfw.SetWindowCreationHint(WindowAttribute.OpenGLForwardCompatibility, true);
                Glfw.SetWindowCreationHint(WindowAttribute.ContextVersionMajor, 3);
                Glfw.SetWindowCreationHint(WindowAttribute.ContextVersionMinor, 2);

                // Creates the "sharing window" that is permanently hidden to the user.
                // It is used to query window capabilities and assist with sharing OpenGL resources.
                // It is also used when shaders or other OpenGL resources that need to be created
                // on an OGL bound thread but do not have a clear context available to them.
                Glfw.SetWindowCreationHint(WindowAttribute.TransparentFramebuffer, true);
                Glfw.SetWindowCreationHint(WindowAttribute.Visible, false);
                ShareContext = Glfw.CreateWindow(256, 256, "GLFW Background Window");

                // Use share context temporarily to load the GL functions and initialize the graphics adapter
                Glfw.MakeContextCurrent(ShareContext);
                {
                    // Loads the GL functions via GLFW lookup
                    GLES.LoadFunctions(Glfw.GetProcAddress);

                    // Initialize the OpenGL ES graphics backend. On desktop platforms this
                    // only uses ES 3.0 features, but actually uses a OpenGL 3.2 context.
                    backend = new ESWindowGraphicsBackend();

                    // Compiles default shaders and other renderer initialization
                    GraphicsBackend.InitializeBackend();
                }
                Glfw.MakeContextCurrent(WindowHandle.None);

                return backend;
            }
        }

        private static void DisposeGraphics()
        {
            // TODO: How to dispose of the graphics backend...?
            //       Drawing resources may have a global reference to backend resources.
            //       These references are hard to clean up. The design was made around the assumption
            //       of *one* backend initialized per application lifetime. The desktop application
            //       pattern supports this with the assumption that Application.Run() is the top function
            //       (read: pseudo main function) and when it exits, so does the application process.

            // Dispose of the graphics backend
            _backend.Dispose();
            _backend = null;
        }

        #endregion

        #region Audio System

        private static void InitializeAudio()
        {
            Log.Warning("TODO: Initialize Audio System");
        }

        private static void DisposeAudio()
        {
            Log.Warning("TODO: Dispose Audio System");
        }

        #endregion

        /// <summary>
        /// Ensures the user has called <see cref="Run(Action)"/>.
        /// </summary>
        private static void EnsureReady()
        {
            if (IsRunning == false)
            {
                throw new InvalidOperationException($"You must execute the application via {nameof(Application)}.{nameof(Run)}.");
            }
        }

        /// <summary>
        /// Ensures the user has called <see cref="Run(Action)"/> on the main thread.
        /// </summary>
        private static void EnsureOnMainThread()
        {
            if (Thread.CurrentThread.IsAlive)
            {
                var correctEntryMethod = Assembly.GetEntryAssembly().EntryPoint;

                var frames = new StackTrace().GetFrames();
                for (var i = frames.Length - 1; i >= 0; i--)
                {
                    if (correctEntryMethod == frames[i].GetMethod())
                    {
                        return;
                    }
                }
            }

            throw new InvalidOperationException($"You must execute the application on the main thread.");
        }
    }
}
