using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Reflection;
using System.Threading;

using Heirloom.Desktop.Hardware;
using Heirloom.Drawing;
using Heirloom.IO;
using Heirloom.OpenGLES;

namespace Heirloom.Desktop
{
    public static class Application
    {
        private const double WaitEventsTimeout = 1.0 / 100.0;

        private static readonly ConsumerQueue _invokeQueue = new ConsumerQueue();

        private static readonly Dictionary<MonitorHandle, Monitor> _monitors = new Dictionary<MonitorHandle, Monitor>();
        private static readonly List<Window> _windows = new List<Window>();

        private static readonly object _lock = new object();

        internal static WindowHandle ShareContext;

        /// <summary>
        /// Gets the graphics adapter.
        /// </summary>
        internal static GraphicsAdapter GraphicsAdapter { get; private set; }

        /// <summary>
        /// Gest the graphics factory.
        /// </summary>
        internal static IWindowGraphicsFactory GraphicsFactory { get; private set; }

        /// <summary>
        /// Gets a value that determines if transparent window framebuffers are supported on this device/platform.
        /// </summary>
        public static bool SupportsTransparentFramebuffer { get; private set; }

        /// <summary>
        /// Gets a value determining if the application has been initialized.
        /// </summary>
        public static bool IsInitialized { get; private set; } = false;

        #region Windows

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

        #endregion

        #region Monitors

        /// <summary>
        /// The default (primary) monitor.
        /// </summary>
        public static Monitor DefaultMonitor { get; private set; }

        /// <summary>
        /// Gets all currently connected monitors.
        /// </summary>
        public static IEnumerable<Monitor> Monitors
        {
            get
            {
                EnsureReady();
                return _monitors.Values;
            }
        }

        private static void OnMonitorCallback(MonitorHandle monitor, ConnectState state)
        {
            var name = Glfw.GetMonitorName(monitor);

            var primary = Glfw.GetPrimaryMonitor();
            var isPrimary = primary == monitor;

            // Connected Monitor
            if (state == ConnectState.Connected)
            {
                // We can only insert if unknown
                if (!_monitors.ContainsKey(monitor))
                {
                    _monitors[monitor] = new Monitor(name, monitor);
                }
            }
            // Disconnected Monitor
            else
            {
                // We can only remove if known already
                if (_monitors.ContainsKey(monitor))
                {
                    _monitors.Remove(monitor);
                }
            }

            // Set default monitor
            DefaultMonitor = _monitors[primary];
        }

        #endregion

        #region Hardware

        /// <summary>
        /// Gets detected information about the CPU.
        /// </summary>
        public static CpuInfo CpuInfo { get; private set; }

        /// <summary>
        /// Gets detected information about the GPU.
        /// </summary>
        public static GpuInfo GpuInfo { get; private set; }

        #endregion

        /// <summary>
        /// Initializes windowing utilities, executes <paramref name="startup"/> and 
        /// then continuously processes window events until all windows are closed. This is a blocking function.
        /// </summary>
        /// <see cref="IsInitialized"/>
        public static void Run(Action startup)
        {
            EnsureMainThread();

            if (startup is null)
            {
                throw new ArgumentNullException(nameof(startup));
            }

            if (IsInitialized)
            {
                throw new InvalidOperationException("Application has already been initialized and is currently running.");
            }

            // Initialize
            Initialize();

            // Execute application startup
            startup();

            // Perform main window loop (blocking while any window exists)
            PerformMainWindowLoop();

            // Clean up resources before GLFW termination
            GraphicsAdapter.Dispose();

            // Shutdown GLFW
            Glfw.Terminate();
            IsInitialized = false;
        }

        private static void Initialize()
        {
            lock (_lock)
            {
                // Try to initialize GLFW
                if (!Glfw.Initialize())
                {
                    throw new InvalidOperationException("Unable to initialize GLFW.");
                }

                // Initializes monitor list and callback
                InitializeMonitors();

                // Initialize graphics contexts
                InitializeGraphics();

                // Determine if transparent framebuffers are possible
                SupportsTransparentFramebuffer = Glfw.GetWindowAttribute(ShareContext, WindowAttribute.TransparentFramebuffer) != 0;

                // Detect hardware information 
                CpuInfo = HardwareDetector.DetectCpuInfo();
                GpuInfo = HardwareDetector.DetectGpuInfo();

                // 
                Console.WriteLine(CpuInfo);
                Console.WriteLine(GpuInfo);
            }

            // Mark initialization as complete
            IsInitialized = true;
        }

        private static void InitializeGraphics()
        {
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
            ShareContext = CreateSharingWindow();

            // Use share context temporarily to load the GL functions and construct the graphics adapter object...
            Glfw.MakeContextCurrent(ShareContext);
            {
                // Loads the GL functions via GLFW lookup
                GL.LoadFunctions(Glfw.GetProcAddress);

                // On the desktop we actually use GL 3.2, but the implementation is limited to GLES 3.0 features.
                // This is to help make a uniform implementation for supportd on mobile platforms. This however,
                // does allow the user to create incompatible shaders between platforms. As mobile is currently not
                // a supported project, this is not yet a concern.
                GraphicsAdapter = new OpenGLWindowGraphicsAdapter();
                GraphicsFactory = GraphicsAdapter as IWindowGraphicsFactory;
                GraphicsAdapter.Initialize();

                // Set default window creation hints
                Glfw.SetWindowCreationHint(WindowAttribute.FocusOnShow, true);
                Glfw.SetWindowCreationHint(WindowAttribute.Visible, true);
            }
            Glfw.MakeContextCurrent(WindowHandle.None);
        }

        private static void InitializeMonitors()
        {
            // Register monitor callback, invoked when the monitor configuration changes.
            Glfw.SetMonitorCallback(OnMonitorCallback);

            // Scan currently connected monitors
            foreach (var monitor in Glfw.GetMonitors())
            {
                OnMonitorCallback(monitor, ConnectState.Connected);
            }
        }

        private static void PerformMainWindowLoop()
        {
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
        }

        private static WindowHandle CreateSharingWindow()
        {
            // Create "dummy" window, the GL context sharing window
            Glfw.SetWindowCreationHint(WindowAttribute.TransparentFramebuffer, true);
            Glfw.SetWindowCreationHint(WindowAttribute.Visible, false);
            return Glfw.CreateWindow(256, 256, "GLFW Background Window");
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

        #region Application Runtime Validation

        /// <summary>
        /// Ensures the user has called <see cref="Run(Action)"/>.
        /// </summary>
        private static void EnsureReady()
        {
            if (IsInitialized == false)
            {
                throw new InvalidOperationException($"You must execute the application via {nameof(Application)}.{nameof(Run)}.");
            }
        }

        /// <summary>
        /// Ensures the user has called <see cref="Run(Action)"/> on the main thread.
        /// </summary>
        private static void EnsureMainThread()
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

        #endregion 
    }
}
