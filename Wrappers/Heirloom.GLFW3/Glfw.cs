using System;
using System.Diagnostics;
using System.Runtime.CompilerServices;
using System.Runtime.InteropServices;

namespace Heirloom.GLFW3
{
    internal static unsafe partial class Glfw
    {
        private const string Library = "glfw3";

        private static MonitorCallback _monitorCallback;

        public static bool IsInitialized { get; private set; }

        public static bool Initialize()
        {
            var success = glfwInit();
            CheckError(nameof(Initialize));
            IsInitialized = success;
            return success;
        }

        public static void Terminate()
        {
            GC.KeepAlive(_monitorCallback);

            // Clear callbacks?
            _monitorCallback = null;
        }

        #region Monitors

        internal static MonitorHandle GetPrimaryMonitor()
        {
            var handle = glfwGetPrimaryMonitor();
            CheckError(nameof(GetPrimaryMonitor));
            return handle;
        }

        internal static MonitorHandle[] GetMonitors()
        {
            var array = glfwGetMonitors(out var count);

            var monitors = new MonitorHandle[count];
            for (var i = 0; i < count; i++)
            {
                monitors[i] = array[i];
            }

            return monitors;
        }

        internal static VideoMode GetVideoMode(MonitorHandle monitor)
        {
            var ptr = glfwGetVideoMode(monitor);
            return Marshal.PtrToStructure<VideoMode>(ptr);
        }

        internal static VideoMode[] GetVideoModes(MonitorHandle monitor)
        {
            var array = glfwGetVideoModes(monitor, out var count);

            var modes = new VideoMode[count];
            for (var i = 0; i < count; i++)
            {
                modes[i] = array[i];
            }

            return modes;
        }

        internal static string GetMonitorName(MonitorHandle monitor)
        {
            var cstr = glfwGetMonitorName(monitor);
            CheckError(nameof(GetMonitorName));

            return Marshal.PtrToStringAnsi((IntPtr) cstr);
        }

        internal static MonitorCallback SetMonitorCallback(MonitorCallback callback)
        {
            var previous = glfwSetMonitorCallback(_monitorCallback = callback);
            CheckError(nameof(SetMonitorCallback));
            return previous;
        }

        #endregion

        #region Windows

        internal static WindowHandle CreateWindow(int width, int height, string title, MonitorHandle monitor, WindowHandle share)
        {
            var handle = glfwCreateWindow(width, height, title, monitor, share);
            CheckError(nameof(CreateWindow));
            return handle;
        }

        internal static void ShowWindow(WindowHandle window)
        {
            glfwShowWindow(window);
            CheckError(nameof(ShowWindow));
        }

        internal static void HideWindow(WindowHandle window)
        {
            glfwHideWindow(window);
            CheckError(nameof(HideWindow));
        }

        internal static void FocusWindow(WindowHandle window)
        {
            glfwFocusWindow(window);
            CheckError(nameof(FocusWindow));
        }

        internal static void PollEvents()
        {
            glfwPollEvents();
            CheckError(nameof(PollEvents));
        }

        #endregion

        [Conditional("DEBUG"), MethodImpl(MethodImplOptions.AggressiveInlining)]
        internal static void CheckError(string command)
        {
            var foundError = false;
            ErrorCode err;

            var messageBuffer = stackalloc char[1024];

            var infloop = 0;
            while ((err = glfwGetError(&messageBuffer)) != ErrorCode.None && (++infloop < 10))
            {
                var message = Marshal.PtrToStringAnsi((IntPtr) messageBuffer);
                Console.WriteLine($"GLFW Error ({command}) '{message}'.");
                foundError = true;
            }

            // 
            if (foundError)
            {
                throw new GlfwException($"GL error detected in a call to {command}");
            }
        }

        internal enum ErrorCode : int
        {
            None = 0,
            NotInitialized = 0x10001
        }

        internal enum ConnectState
        {
            Connected = 0x00040001,
            Disconnected = 0x00040002
        }
    }
}
