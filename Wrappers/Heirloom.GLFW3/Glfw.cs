using System;
using System.Diagnostics;
using System.Runtime.CompilerServices;
using System.Runtime.InteropServices;

namespace Heirloom.GLFW3
{
    public static unsafe partial class Glfw
    {
        private const string Library = "glfw3";

        public static bool Init()
        {
            var success = glfwInit();
            CheckError(nameof(Init));
            return success;
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static void SetInitHint(InitHint hint, bool state)
        {
            glfwInitHint(hint, state);
            CheckError(nameof(SetInitHint));
        }

        public static void Terminate()
        {
            glfwTerminate();
            CheckError(nameof(Terminate));
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static void GetVersion(out int major, out int minor, out int revision)
        {
            glfwGetVersion(out major, out minor, out revision);
            CheckError(nameof(GetVersion));
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Version GetVersion()
        {
            GetVersion(out var maj, out var min, out var rev);
            return new Version(maj, min, 0, rev);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static string GetVersionString()
        {
            var cstr = glfwGetVersionString();
            CheckError(nameof(GetVersionString));
            return Marshal.PtrToStringAnsi((IntPtr) cstr);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static void PollEvents()
        {
            glfwPollEvents();
            CheckError(nameof(PollEvents));
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static void WaitEvents()
        {
            glfwWaitEvents();
            CheckError(nameof(WaitEvents));
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static void WaitEventsTimeout(double timeout)
        {
            glfwWaitEventsTimeout(timeout);
            CheckError(nameof(WaitEventsTimeout));
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static void PostEmptyEvent()
        {
            glfwPostEmptyEvent();
            CheckError(nameof(PostEmptyEvent));
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static double GetTime()
        {
            var time = glfwGetTime();
            CheckError(nameof(GetTime));
            return time;
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static void SetTime(double time)
        {
            glfwSetTime(time);
            CheckError(nameof(SetTime));
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static ulong GetTimerValue()
        {
            var value = glfwGetTimerValue();
            CheckError(nameof(GetTimerValue));
            return value;
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static ulong GetTimerFrequency()
        {
            var freq = glfwGetTimerFrequency();
            CheckError(nameof(GetTimerFrequency));
            return freq;
        }

        #region Monitors

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static MonitorHandle[] GetMonitors()
        {
            var array = glfwGetMonitors(out var count);
            CheckError(nameof(GetMonitors));

            var monitors = new MonitorHandle[count];
            for (var i = 0; i < count; i++)
            {
                monitors[i] = array[i];
            }

            return monitors;
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static MonitorHandle GetPrimaryMonitor()
        {
            var handle = glfwGetPrimaryMonitor();
            CheckError(nameof(GetPrimaryMonitor));
            return handle;
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static void GetMonitorPosition(MonitorHandle monitor, out int x, out int y)
        {
            glfwGetMonitorPos(monitor, out x, out y);
            CheckError(nameof(GetMonitorPosition));
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static void GetMonitorWorkarea(MonitorHandle monitor, out int x, out int y, out int width, out int height)
        {
            glfwGetMonitorWorkarea(monitor, out x, out y, out width, out height);
            CheckError(nameof(GetMonitorWorkarea));
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static void GetMonitorPhysicalSize(MonitorHandle monitor, out int widthMM, out int heightMM)
        {
            glfwGetMonitorPhysicalSize(monitor, out widthMM, out heightMM);
            CheckError(nameof(GetMonitorPhysicalSize));
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static void GetMonitorContentScale(MonitorHandle monitor, out float xScale, out float yScale)
        {
            glfwGetMonitorContentScale(monitor, out xScale, out yScale);
            CheckError(nameof(GetMonitorContentScale));
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static string GetMonitorName(MonitorHandle monitor)
        {
            var cstr = glfwGetMonitorName(monitor);
            CheckError(nameof(GetMonitorName));
            return Marshal.PtrToStringAnsi((IntPtr) cstr);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static VideoMode GetVideoMode(MonitorHandle monitor)
        {
            var ptr = glfwGetVideoMode(monitor);
            CheckError(nameof(GetVideoMode));
            return Marshal.PtrToStructure<VideoMode>(ptr);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static VideoMode[] GetVideoModes(MonitorHandle monitor)
        {
            var array = glfwGetVideoModes(monitor, out var count);
            CheckError(nameof(GetVideoModes));

            var modes = new VideoMode[count];
            for (var i = 0; i < count; i++)
            {
                modes[i] = array[i];
            }

            return modes;
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static void SetGamma(MonitorHandle monitor, float gamma)
        {
            glfwSetGamma(monitor, gamma);
            CheckError(nameof(SetGamma));
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static MonitorCallback SetMonitorCallback(MonitorCallback callback)
        {
            var previous = glfwSetMonitorCallback(callback);
            CheckError(nameof(SetMonitorCallback));
            return previous;
        }

        #endregion

        #region Window

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static void ResetWindowHints()
        {
            glfwDefaultWindowHints();
            CheckError(nameof(ResetWindowHints));
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static void SetWindowHint(WindowHint hint, bool value)
        {
            SetWindowHint(hint, value ? 1 : 0);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static void SetWindowHint(WindowHint hint, int value)
        {
            glfwWindowHint(hint, value);
            CheckError(nameof(SetWindowHint));
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static void SetWindowHint(WindowHint hint, string value)
        {
            glfwWindowHintString(hint, value);
            CheckError(nameof(SetWindowHint));
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static WindowHandle CreateWindow(int width, int height, string title, MonitorHandle monitor = default, WindowHandle share = default)
        {
            var handle = glfwCreateWindow(width, height, title, monitor, share);
            CheckError(nameof(CreateWindow));
            return handle;
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static void DestroyWindow(WindowHandle window)
        {
            glfwDestroyWindow(window);
            CheckError(nameof(DestroyWindow));
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static bool GetWindowShouldClose(WindowHandle window)
        {
            var state = glfwWindowShouldClose(window);
            CheckError(nameof(GetWindowShouldClose));
            return state;
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static void SetWindowShouldClose(WindowHandle window, bool state)
        {
            glfwSetWindowShouldClose(window, state);
            CheckError(nameof(SetWindowShouldClose));
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static void SetWindowTitle(WindowHandle window, string title)
        {
            glfwSetWindowTitle(window, title);
            CheckError(nameof(SetWindowTitle));
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static void SetWindowIcon(WindowHandle window, ImageData image)
        {
            SetWindowIcons(window, new[] { image });
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static void SetWindowIcons(WindowHandle window, ImageData[] images)
        {
            fixed (ImageData* ptr = images)
            {
                glfwSetWindowIcon(window, images.Length, ptr);
                CheckError(nameof(SetWindowIcon));
            }
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static void GetWindowPosition(WindowHandle window, out int xPos, out int yPos)
        {
            glfwGetWindowPos(window, out xPos, out yPos);
            CheckError(nameof(GetWindowPosition));
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static void SetWindowPosition(WindowHandle window, int xPos, int yPos)
        {
            glfwSetWindowPos(window, xPos, yPos);
            CheckError(nameof(SetWindowPosition));
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static void GetWindowSize(WindowHandle window, out int width, out int height)
        {
            glfwGetWindowSize(window, out width, out height);
            CheckError(nameof(GetWindowSize));
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static void SetWindowSize(WindowHandle window, int width, int height)
        {
            glfwSetWindowSize(window, width, height);
            CheckError(nameof(SetWindowSize));
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static void SetWindowSizeLimits(WindowHandle window, int minWidth, int minHeight, int maxWidth, int maxHeight)
        {
            glfwSetWindowSizeLimits(window, minWidth, minHeight, maxWidth, maxHeight);
            CheckError(nameof(SetWindowSizeLimits));
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static void SetWindowAspectRatio(WindowHandle window, int numerator, int denominator)
        {
            glfwSetWindowAspectRatio(window, numerator, denominator);
            CheckError(nameof(SetWindowAspectRatio));
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static void GetFramebufferSize(WindowHandle window, out int width, out int height)
        {
            glfwGetFramebufferSize(window, out width, out height);
            CheckError(nameof(GetFramebufferSize));
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static void GetWindowBounds(WindowHandle window, out int left, out int top, out int right, out int bottom)
        {
            glfwGetWindowFrameSize(window, out left, out top, out right, out bottom);
            CheckError(nameof(GetWindowBounds));
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static void GetWindowContentScale(WindowHandle window, out float xScale, out float yScale)
        {
            glfwGetWindowContentScale(window, out xScale, out yScale);
            CheckError(nameof(GetWindowContentScale));
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static void SetWindowOpacity(WindowHandle window, float opacity)
        {
            glfwSetWindowOpacity(window, opacity);
            CheckError(nameof(SetWindowOpacity));
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static float GetWindowOpacity(WindowHandle window)
        {
            var opacity = glfwGetWindowOpacity(window);
            CheckError(nameof(GetWindowOpacity));
            return opacity;
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static void ShowWindow(WindowHandle window)
        {
            glfwShowWindow(window);
            CheckError(nameof(ShowWindow));
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static void IconifyWindow(WindowHandle window)
        {
            glfwIconifyWindow(window);
            CheckError(nameof(IconifyWindow));
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static void MaximizeWindow(WindowHandle window)
        {
            glfwMaximizeWindow(window);
            CheckError(nameof(MaximizeWindow));
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static void RestoreWindow(WindowHandle window)
        {
            glfwRestoreWindow(window);
            CheckError(nameof(RestoreWindow));
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static void RequestWindowAttention(WindowHandle window)
        {
            glfwRequestWindowAttention(window);
            CheckError(nameof(RequestWindowAttention));
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static void HideWindow(WindowHandle window)
        {
            glfwHideWindow(window);
            CheckError(nameof(HideWindow));
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static void FocusWindow(WindowHandle window)
        {
            glfwFocusWindow(window);
            CheckError(nameof(FocusWindow));
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static MonitorHandle GetWindowMonitor(WindowHandle window)
        {
            var monitor = glfwGetWindowMonitor(window);
            CheckError(nameof(GetWindowMonitor));
            return monitor;
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static void SetWindowMonitor(WindowHandle window, MonitorHandle monitor, int x, int y, int width, int height, int refresh)
        {
            glfwSetWindowMonitor(window, monitor, x, y, width, height, refresh);
            CheckError(nameof(SetWindowMonitor));
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static int GetWindowAttrib(WindowHandle window, WindowAttribute attribute)
        {
            var value = glfwGetWindowAttrib(window, attribute);
            CheckError(nameof(GetWindowAttrib));
            return value;
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static void SetWindowAttrib(WindowHandle window, WindowAttribute attribute, bool value)
        {
            glfwSetWindowAttrib(window, attribute, value ? 1 : 0);
            CheckError(nameof(SetWindowAttrib));
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static WindowPositionCallback SetWindowPositionCallback(WindowHandle window, WindowPositionCallback callback)
        {
            var old = glfwSetWindowPosCallback(window, callback);
            CheckError(nameof(SetWindowPositionCallback));
            return old;
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static WindowSizeCallback SetWindowSizeCallback(WindowHandle window, WindowSizeCallback callback)
        {
            var old = glfwSetWindowSizeCallback(window, callback);
            CheckError(nameof(SetWindowSizeCallback));
            return old;
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static WindowCloseCallback SetWindowCloseCallback(WindowHandle window, WindowCloseCallback callback)
        {
            var old = glfwSetWindowCloseCallback(window, callback);
            CheckError(nameof(SetWindowCloseCallback));
            return old;
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static WindowRefreshCallback SetWindowRefreshCallback(WindowHandle window, WindowRefreshCallback callback)
        {
            var old = glfwSetWindowRefreshCallback(window, callback);
            CheckError(nameof(SetWindowRefreshCallback));
            return old;
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static WindowFocusCallback SetWindowFocusCallback(WindowHandle window, WindowFocusCallback callback)
        {
            var old = glfwSetWindowFocusCallback(window, callback);
            CheckError(nameof(SetWindowFocusCallback));
            return old;
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static WindowIconifyCallback SetWindowIconifyCallback(WindowHandle window, WindowIconifyCallback callback)
        {
            var old = glfwSetWindowIconifyCallback(window, callback);
            CheckError(nameof(SetWindowIconifyCallback));
            return old;
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static WindowMaximizeCallback SetWindowMaximizeCallback(WindowHandle window, WindowMaximizeCallback callback)
        {
            var old = glfwSetWindowMaximizeCallback(window, callback);
            CheckError(nameof(SetWindowMaximizeCallback));
            return old;
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static FramebufferSizeCallback SetFramebufferSizeCallback(WindowHandle window, FramebufferSizeCallback callback)
        {
            var old = glfwSetFramebufferSizeCallback(window, callback);
            CheckError(nameof(SetFramebufferSizeCallback));
            return old;
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static WindowContentScaleCallback SetWindowContentScaleCallback(WindowHandle window, WindowContentScaleCallback callback)
        {
            var old = glfwSetWindowContentScaleCallback(window, callback);
            CheckError(nameof(SetWindowContentScaleCallback));
            return old;
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static KeyCallback SetKeyCallback(WindowHandle window, KeyCallback callback)
        {
            var old = glfwSetKeyCallback(window, callback);
            CheckError(nameof(SetKeyCallback));
            return old;
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static CharCallback SetCharCallback(WindowHandle window, CharCallback callback)
        {
            var old = glfwSetCharCallback(window, callback);
            CheckError(nameof(SetCharCallback));
            return old;
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static MouseButtonCallback SetMouseButtonCallback(WindowHandle window, MouseButtonCallback callback)
        {
            var old = glfwSetMouseButtonCallback(window, callback);
            CheckError(nameof(SetMouseButtonCallback));
            return old;
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static CursorPositionCallback SetCursorPositionCallback(WindowHandle window, CursorPositionCallback callback)
        {
            var old = glfwSetCursorPosCallback(window, callback);
            CheckError(nameof(SetCursorPositionCallback));
            return old;
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static CursorEnterCallback SetCursorEnterCallback(WindowHandle window, CursorEnterCallback callback)
        {
            var old = glfwSetCursorEnterCallback(window, callback);
            CheckError(nameof(SetCursorEnterCallback));
            return old;
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static ScrollCallback SetScrollCallback(WindowHandle window, ScrollCallback callback)
        {
            var old = glfwSetScrollCallback(window, callback);
            CheckError(nameof(SetScrollCallback));
            return old;
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static DropCallback SetDropCallback(WindowHandle window, DropCallback callback)
        {
            var old = glfwSetDropCallback(window, callback);
            CheckError(nameof(SetDropCallback));
            return old;
        }

        #endregion

        #region Window (Input)

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static int GetInputMode(WindowHandle window, InputMode mode)
        {
            var value = glfwGetInputMode(window, mode);
            CheckError(nameof(GetInputMode));
            return value;
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static void SetInputMode(WindowHandle window, InputMode mode, int value)
        {
            glfwSetInputMode(window, mode, value);
            CheckError(nameof(SetInputMode));
        }

        public static bool IsRawMouseMotionSupported
        {
            get
            {
                var value = glfwRawMouseMotionSupported();
                CheckError(nameof(IsRawMouseMotionSupported));
                return value;
            }
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static string GetKeyName(Key key, int scancode)
        {
            var cstr = glfwGetKeyName(key, scancode);
            CheckError(nameof(GetKeyName));
            return Marshal.PtrToStringAnsi((IntPtr) cstr);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static int GetKeyScancode(Key key)
        {
            var scancode = glfwGetKeyScancode(key);
            CheckError(nameof(GetKeyScancode));
            return scancode;
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static ButtonAction GetKey(WindowHandle window, Key key)
        {
            var action = glfwGetKey(window, key);
            CheckError(nameof(GetKey));
            return action;
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static ButtonAction GetMouseButton(WindowHandle window, int button)
        {
            var action = glfwGetMouseButton(window, button);
            CheckError(nameof(GetMouseButton));
            return action;
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static void GetCursorPosition(WindowHandle window, out double xPos, out double yPos)
        {
            glfwGetCursorPos(window, out xPos, out yPos);
            CheckError(nameof(GetCursorPosition));
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static void SetCursorPosition(WindowHandle window, double xPos, double yPos)
        {
            glfwSetCursorPos(window, xPos, yPos);
            CheckError(nameof(SetCursorPosition));
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static CursorHandle CreateCursor(ImageData image, int xHot, int yHot)
        {
            var cursor = glfwCreateCursor(ref image, xHot, yHot);
            CheckError(nameof(CreateCursor));
            return cursor;
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static CursorHandle CreateCursor(StandardCursor standardCursor)
        {
            var cursor = glfwCreateStandardCursor(standardCursor);
            CheckError(nameof(CreateCursor));
            return cursor;
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static void DestroyCursor(CursorHandle cursor)
        {
            glfwDestroyCursor(cursor);
            CheckError(nameof(DestroyCursor));
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static void SetCursor(WindowHandle window, CursorHandle cursor)
        {
            glfwSetCursor(window, cursor);
            CheckError(nameof(SetCursor));
        }

        #endregion

        #region Window (OpenGL)

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static void MakeContextCurrent(WindowHandle window)
        {
            glfwMakeContextCurrent(window);
            CheckError(nameof(MakeContextCurrent));
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static WindowHandle GetCurrentContext()
        {
            var value = glfwGetCurrentContext();
            CheckError(nameof(GetCurrentContext));
            return value;
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static void SwapBuffers(WindowHandle window)
        {
            glfwSwapBuffers(window);
            CheckError(nameof(SwapBuffers));
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static void SetSwapInterval(int interval)
        {
            glfwSwapInterval(interval);
            CheckError(nameof(SetSwapInterval));
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static bool IsExtensionSupported(string extension)
        {
            var value = glfwExtensionSupported(extension);
            CheckError(nameof(IsExtensionSupported));
            return value;
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static IntPtr GetProcAddress(string name)
        {
            var addr = glfwGetProcAddress(name);
            CheckError(nameof(GetProcAddress));
            return addr;
        }

        #endregion

        public static string ReadString(IntPtr ptr)
        {
            return Marshal.PtrToStringAnsi(ptr);
        }

        public static string ReadString(IntPtr ptr, int index)
        {
            ptr = Marshal.ReadIntPtr(ptr + (index * IntPtr.Size));
            return Marshal.PtrToStringAnsi(ptr);
        }

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
    }
}
