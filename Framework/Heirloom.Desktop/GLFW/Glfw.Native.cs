using System;
using System.Runtime.InteropServices;
using System.Security;

namespace Heirloom.Desktop.GLFW
{
    internal static unsafe partial class Glfw
    {
#pragma warning disable IDE1006 // Naming Styles
#pragma warning disable CS0649  // Default value null
#pragma warning disable CS0169  // Unassigned

        [DllImport(Library), SuppressUnmanagedCodeSecurity]
        [return: MarshalAs(UnmanagedType.Bool)]
        private static extern bool glfwInit();

        [DllImport(Library), SuppressUnmanagedCodeSecurity]
        private static extern void glfwInitHint(InitHint hint, [MarshalAs(UnmanagedType.Bool)] bool state);

        [DllImport(Library), SuppressUnmanagedCodeSecurity]
        private static extern void glfwTerminate();

        [DllImport(Library), SuppressUnmanagedCodeSecurity]
        private static extern void glfwGetVersion(out int major, out int minor, out int revision);

        [DllImport(Library), SuppressUnmanagedCodeSecurity]
        private static extern char* glfwGetVersionString();

        [DllImport(Library), SuppressUnmanagedCodeSecurity]
        private static extern ErrorCode glfwGetError(char** description);

        [DllImport(Library), SuppressUnmanagedCodeSecurity]
        private static extern ErrorCallback glfwSetErrorCallback(ErrorCallback callback);

        [DllImport(Library), SuppressUnmanagedCodeSecurity]
        private static extern void glfwPollEvents();

        [DllImport(Library), SuppressUnmanagedCodeSecurity]
        private static extern void glfwWaitEvents();

        [DllImport(Library), SuppressUnmanagedCodeSecurity]
        private static extern void glfwWaitEventsTimeout(double timeout);

        [DllImport(Library), SuppressUnmanagedCodeSecurity]
        private static extern void glfwPostEmptyEvent();

        [DllImport(Library), SuppressUnmanagedCodeSecurity]
        private static extern double glfwGetTime();

        [DllImport(Library), SuppressUnmanagedCodeSecurity]
        private static extern void glfwSetTime(double time);

        [DllImport(Library), SuppressUnmanagedCodeSecurity]
        private static extern ulong glfwGetTimerValue();

        [DllImport(Library), SuppressUnmanagedCodeSecurity]
        private static extern ulong glfwGetTimerFrequency();

        #region Monitor

        [DllImport(Library), SuppressUnmanagedCodeSecurity]
        private static extern MonitorHandle* glfwGetMonitors(out int count);

        [DllImport(Library), SuppressUnmanagedCodeSecurity]
        private static extern MonitorHandle glfwGetPrimaryMonitor();

        [DllImport(Library), SuppressUnmanagedCodeSecurity]
        private static extern void glfwGetMonitorPos(MonitorHandle monitor, out int x, out int y);

        [DllImport(Library), SuppressUnmanagedCodeSecurity]
        private static extern void glfwGetMonitorWorkarea(MonitorHandle monitor, out int x, out int y, out int width, out int height);

        [DllImport(Library), SuppressUnmanagedCodeSecurity]
        private static extern void glfwGetMonitorPhysicalSize(MonitorHandle monitor, out int widthMM, out int heightMM);

        [DllImport(Library), SuppressUnmanagedCodeSecurity]
        private static extern void glfwGetMonitorContentScale(MonitorHandle monitor, out float xScale, out float yScale);

        [DllImport(Library), SuppressUnmanagedCodeSecurity]
        private static extern char* glfwGetMonitorName(MonitorHandle monitor);

        [DllImport(Library), SuppressUnmanagedCodeSecurity]
        private static extern MonitorCallback glfwSetMonitorCallback(MonitorCallback callback);

        [DllImport(Library), SuppressUnmanagedCodeSecurity]
        private static extern VideoMode* glfwGetVideoModes(MonitorHandle monitor, out int count);

        [DllImport(Library), SuppressUnmanagedCodeSecurity]
        private static extern IntPtr glfwGetVideoMode(MonitorHandle monitor);

        [DllImport(Library), SuppressUnmanagedCodeSecurity]
        private static extern void glfwSetGamma(MonitorHandle monitor, float gamma);

        #endregion

        #region Window

        [DllImport(Library), SuppressUnmanagedCodeSecurity]
        private static extern void glfwDefaultWindowHints();

        [DllImport(Library), SuppressUnmanagedCodeSecurity]
        private static extern void glfwWindowHint(WindowCreationHint hint, int value);

        [DllImport(Library), SuppressUnmanagedCodeSecurity]
        private static extern void glfwWindowHintString(WindowCreationHint hint, string value);

        [DllImport(Library), SuppressUnmanagedCodeSecurity]
        private static extern WindowHandle glfwCreateWindow(int width, int height, string title, MonitorHandle monitor, WindowHandle share);

        [DllImport(Library), SuppressUnmanagedCodeSecurity]
        private static extern void glfwDestroyWindow(WindowHandle window);

        [DllImport(Library), SuppressUnmanagedCodeSecurity]
        [return: MarshalAs(UnmanagedType.Bool)]
        private static extern bool glfwWindowShouldClose(WindowHandle window);

        [DllImport(Library), SuppressUnmanagedCodeSecurity]
        private static extern void glfwSetWindowShouldClose(WindowHandle window, [MarshalAs(UnmanagedType.Bool)] bool value);

        [DllImport(Library), SuppressUnmanagedCodeSecurity]
        private static extern void glfwSetWindowTitle(WindowHandle window, string title);

        [DllImport(Library), SuppressUnmanagedCodeSecurity]
        private static extern void glfwSetWindowIcon(WindowHandle window, int count, ImageData* images);

        [DllImport(Library), SuppressUnmanagedCodeSecurity]
        private static extern void glfwGetWindowPos(WindowHandle window, out int x, out int y);

        [DllImport(Library), SuppressUnmanagedCodeSecurity]
        private static extern void glfwSetWindowPos(WindowHandle window, int x, int y);

        [DllImport(Library), SuppressUnmanagedCodeSecurity]
        private static extern void glfwGetWindowSize(WindowHandle window, out int width, out int height);

        [DllImport(Library), SuppressUnmanagedCodeSecurity]
        private static extern void glfwSetWindowSize(WindowHandle window, int width, int height);

        [DllImport(Library), SuppressUnmanagedCodeSecurity]
        private static extern void glfwSetWindowSizeLimits(WindowHandle window, int minWidth, int minHeight, int maxWidth, int maxHeight);

        [DllImport(Library), SuppressUnmanagedCodeSecurity]
        private static extern void glfwSetWindowAspectRatio(WindowHandle window, int numerator, int denominator);

        [DllImport(Library), SuppressUnmanagedCodeSecurity]
        private static extern void glfwGetFramebufferSize(WindowHandle window, out int width, out int height);

        [DllImport(Library), SuppressUnmanagedCodeSecurity]
        private static extern void glfwGetWindowFrameSize(WindowHandle window, out int left, out int top, out int right, out int bottom);

        [DllImport(Library), SuppressUnmanagedCodeSecurity]
        private static extern void glfwGetWindowContentScale(WindowHandle window, out float xScale, out float yScale);

        [DllImport(Library), SuppressUnmanagedCodeSecurity]
        private static extern void glfwSetWindowOpacity(WindowHandle window, float opacity);

        [DllImport(Library), SuppressUnmanagedCodeSecurity]
        private static extern float glfwGetWindowOpacity(WindowHandle window);

        [DllImport(Library), SuppressUnmanagedCodeSecurity]
        private static extern void glfwIconifyWindow(WindowHandle window);

        [DllImport(Library), SuppressUnmanagedCodeSecurity]
        private static extern void glfwMaximizeWindow(WindowHandle window);

        [DllImport(Library), SuppressUnmanagedCodeSecurity]
        private static extern void glfwRestoreWindow(WindowHandle window);

        [DllImport(Library), SuppressUnmanagedCodeSecurity]
        private static extern void glfwRequestWindowAttention(WindowHandle window);

        [DllImport(Library), SuppressUnmanagedCodeSecurity]
        private static extern void glfwShowWindow(WindowHandle window);

        [DllImport(Library), SuppressUnmanagedCodeSecurity]
        private static extern void glfwHideWindow(WindowHandle window);

        [DllImport(Library), SuppressUnmanagedCodeSecurity]
        private static extern void glfwFocusWindow(WindowHandle window);

        [DllImport(Library), SuppressUnmanagedCodeSecurity]
        private static extern MonitorHandle glfwGetWindowMonitor(WindowHandle window);

        [DllImport(Library), SuppressUnmanagedCodeSecurity]
        private static extern void glfwSetWindowMonitor(WindowHandle window, MonitorHandle monitor, int x, int y, int width, int height, int refresh);

        [DllImport(Library), SuppressUnmanagedCodeSecurity]
        private static extern int glfwGetWindowAttrib(WindowHandle window, WindowAttribute attribute);

        [DllImport(Library), SuppressUnmanagedCodeSecurity]
        private static extern void glfwSetWindowAttrib(WindowHandle window, WindowAttribute attribute, int value);

        [DllImport(Library), SuppressUnmanagedCodeSecurity]
        private static extern WindowPositionCallback glfwSetWindowPosCallback(WindowHandle window, WindowPositionCallback callback);

        [DllImport(Library), SuppressUnmanagedCodeSecurity]
        private static extern WindowSizeCallback glfwSetWindowSizeCallback(WindowHandle window, WindowSizeCallback callback);

        [DllImport(Library), SuppressUnmanagedCodeSecurity]
        private static extern WindowCloseCallback glfwSetWindowCloseCallback(WindowHandle window, WindowCloseCallback callback);

        [DllImport(Library), SuppressUnmanagedCodeSecurity]
        private static extern WindowRefreshCallback glfwSetWindowRefreshCallback(WindowHandle window, WindowRefreshCallback callback);

        [DllImport(Library), SuppressUnmanagedCodeSecurity]
        private static extern WindowFocusCallback glfwSetWindowFocusCallback(WindowHandle window, WindowFocusCallback callback);

        [DllImport(Library), SuppressUnmanagedCodeSecurity]
        private static extern WindowIconifyCallback glfwSetWindowIconifyCallback(WindowHandle window, WindowIconifyCallback callback);

        [DllImport(Library), SuppressUnmanagedCodeSecurity]
        private static extern WindowMaximizeCallback glfwSetWindowMaximizeCallback(WindowHandle window, WindowMaximizeCallback callback);

        [DllImport(Library), SuppressUnmanagedCodeSecurity]
        private static extern FramebufferSizeCallback glfwSetFramebufferSizeCallback(WindowHandle window, FramebufferSizeCallback callback);

        [DllImport(Library), SuppressUnmanagedCodeSecurity]
        private static extern WindowContentScaleCallback glfwSetWindowContentScaleCallback(WindowHandle window, WindowContentScaleCallback callback);

        [DllImport(Library), SuppressUnmanagedCodeSecurity]
        private static extern int glfwGetInputMode(WindowHandle window, InputMode mode);

        [DllImport(Library), SuppressUnmanagedCodeSecurity]
        private static extern void glfwSetInputMode(WindowHandle window, InputMode mode, int value);

        [DllImport(Library), SuppressUnmanagedCodeSecurity]
        [return: MarshalAs(UnmanagedType.Bool)]
        private static extern bool glfwRawMouseMotionSupported();

        [DllImport(Library), SuppressUnmanagedCodeSecurity]
        private static extern char* glfwGetKeyName(Key key, int scancode);

        [DllImport(Library), SuppressUnmanagedCodeSecurity]
        private static extern int glfwGetKeyScancode(Key key);

        [DllImport(Library), SuppressUnmanagedCodeSecurity]
        private static extern KeyAction glfwGetKey(WindowHandle window, Key key);

        [DllImport(Library), SuppressUnmanagedCodeSecurity]
        private static extern KeyAction glfwGetMouseButton(WindowHandle window, int button);

        [DllImport(Library), SuppressUnmanagedCodeSecurity]
        private static extern void glfwGetCursorPos(WindowHandle window, out double xPos, out double yPos);

        [DllImport(Library), SuppressUnmanagedCodeSecurity]
        private static extern void glfwSetCursorPos(WindowHandle window, double xPos, double yPos);

        [DllImport(Library), SuppressUnmanagedCodeSecurity]
        private static extern CursorHandle glfwCreateCursor(ref ImageData image, int xHot, int yHot);

        [DllImport(Library), SuppressUnmanagedCodeSecurity]
        private static extern CursorHandle glfwCreateStandardCursor(StandardCursor cursorType);

        [DllImport(Library), SuppressUnmanagedCodeSecurity]
        private static extern void glfwDestroyCursor(CursorHandle handle);

        [DllImport(Library), SuppressUnmanagedCodeSecurity]
        private static extern void glfwSetCursor(WindowHandle window, CursorHandle handle);

        [DllImport(Library), SuppressUnmanagedCodeSecurity]
        private static extern KeyCallback glfwSetKeyCallback(WindowHandle window, KeyCallback callback);

        [DllImport(Library), SuppressUnmanagedCodeSecurity]
        private static extern CharCallback glfwSetCharCallback(WindowHandle window, CharCallback callback);

        [DllImport(Library), SuppressUnmanagedCodeSecurity]
        private static extern MouseButtonCallback glfwSetMouseButtonCallback(WindowHandle window, MouseButtonCallback callback);

        [DllImport(Library), SuppressUnmanagedCodeSecurity]
        private static extern CursorPositionCallback glfwSetCursorPosCallback(WindowHandle window, CursorPositionCallback callback);

        [DllImport(Library), SuppressUnmanagedCodeSecurity]
        private static extern CursorEnterCallback glfwSetCursorEnterCallback(WindowHandle window, CursorEnterCallback callback);

        [DllImport(Library), SuppressUnmanagedCodeSecurity]
        private static extern ScrollCallback glfwSetScrollCallback(WindowHandle window, ScrollCallback callback);

        [DllImport(Library), SuppressUnmanagedCodeSecurity]
        private static extern DropCallback glfwSetDropCallback(WindowHandle window, DropCallback callback);

        #endregion

        #region Window (OpenGL)

        [DllImport(Library), SuppressUnmanagedCodeSecurity]
        private static extern void glfwMakeContextCurrent(WindowHandle window);

        [DllImport(Library), SuppressUnmanagedCodeSecurity]
        private static extern WindowHandle glfwGetCurrentContext();

        [DllImport(Library), SuppressUnmanagedCodeSecurity]
        private static extern void glfwSwapBuffers(WindowHandle window);

        [DllImport(Library), SuppressUnmanagedCodeSecurity]
        private static extern void glfwSwapInterval(int interval);

        [DllImport(Library), SuppressUnmanagedCodeSecurity]
        [return: MarshalAs(UnmanagedType.Bool)]
        private static extern bool glfwExtensionSupported(string extension);

        [DllImport(Library), SuppressUnmanagedCodeSecurity]
        private static extern IntPtr glfwGetProcAddress(string name);

        #endregion

        // TODO: Joystick

        // TODO: GamePad 

#pragma warning restore IDE1006 // Naming Styles
#pragma warning restore CS0649  // Default value null
#pragma warning restore CS0169  // Unassigned
    }
}
