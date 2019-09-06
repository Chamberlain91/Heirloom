using System;
using System.Collections.Generic;
using System.Threading;

namespace Heirloom.GLFW3
{
    public static class Application
    {
        public static IReadOnlyList<Window> Windows => _windows;

        internal static ConsumerQueue Queue { get; private set; }

        internal static Glfw.WindowHandle DummyWindow;

        private static List<Window> _windows;

        public static void Run(Action startAction)
        {
            // Initialize GLFW
            if (!Glfw.Initialize())
            {
                Console.WriteLine("Unable to initialize GL FW");
                return;
            }

            // Create "dummy" window, the GL context sharing window
            DummyWindow = Glfw.CreateWindow(256, 256, "GLFW Background Window", Glfw.MonitorHandle.None, Glfw.WindowHandle.None);
            Glfw.HideWindow(DummyWindow);

            // 
            Queue = new ConsumerQueue();
            _windows = new List<Window>();

            // 
            startAction();

            // While any window is open
            while (Windows.Count > 0)
            {
                // Poll window events
                Glfw.PollEvents();

                // Process invoke queue
                Queue.ProcessJobs();

                // Sleep thread, prevent tight spinning
                Thread.Sleep(1);
            }

            // Shutdown GLFW
            Glfw.Terminate();
        }

        internal static void AddWindow(Window window)
        {
            lock (_windows)
            {
                _windows.Add(window);
            }
        }

        internal static void RemoveWindow(Window window)
        {
            lock (_windows)
            {
                _windows.Remove(window);
            }
        }
    }
}
