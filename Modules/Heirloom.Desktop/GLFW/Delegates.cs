using System;
using System.Runtime.InteropServices;
using System.Security;

namespace Heirloom.Desktop
{
    #region Window

    [SuppressUnmanagedCodeSecurity, UnmanagedFunctionPointer(CallingConvention.Cdecl)]
    internal unsafe delegate void WindowPositionCallback(WindowHandle window, int x, int y);

    [SuppressUnmanagedCodeSecurity, UnmanagedFunctionPointer(CallingConvention.Cdecl)]
    internal unsafe delegate void WindowSizeCallback(WindowHandle window, int width, int height);

    [SuppressUnmanagedCodeSecurity, UnmanagedFunctionPointer(CallingConvention.Cdecl)]
    internal unsafe delegate void WindowCloseCallback(WindowHandle window);

    [SuppressUnmanagedCodeSecurity, UnmanagedFunctionPointer(CallingConvention.Cdecl)]
    internal unsafe delegate void WindowRefreshCallback(WindowHandle window);

    [SuppressUnmanagedCodeSecurity, UnmanagedFunctionPointer(CallingConvention.Cdecl)]
    internal unsafe delegate void WindowFocusCallback(WindowHandle window, [MarshalAs(UnmanagedType.Bool)] bool state);

    [SuppressUnmanagedCodeSecurity, UnmanagedFunctionPointer(CallingConvention.Cdecl)]
    internal unsafe delegate void WindowIconifyCallback(WindowHandle window, [MarshalAs(UnmanagedType.Bool)] bool state);

    [SuppressUnmanagedCodeSecurity, UnmanagedFunctionPointer(CallingConvention.Cdecl)]
    internal unsafe delegate void WindowMaximizeCallback(WindowHandle window, [MarshalAs(UnmanagedType.Bool)] bool state);

    [SuppressUnmanagedCodeSecurity, UnmanagedFunctionPointer(CallingConvention.Cdecl)]
    internal unsafe delegate void FramebufferSizeCallback(WindowHandle window, int width, int height);

    [SuppressUnmanagedCodeSecurity, UnmanagedFunctionPointer(CallingConvention.Cdecl)]
    internal unsafe delegate void WindowContentScaleCallback(WindowHandle window, float xScale, float yScale);

    [SuppressUnmanagedCodeSecurity, UnmanagedFunctionPointer(CallingConvention.Cdecl)]
    internal unsafe delegate void KeyCallback(WindowHandle window, Key key, int scancode, KeyAction action, KeyModifiers modifiers);

    [SuppressUnmanagedCodeSecurity, UnmanagedFunctionPointer(CallingConvention.Cdecl)]
    internal unsafe delegate void CharCallback(WindowHandle window, uint codepoint);

    [SuppressUnmanagedCodeSecurity, UnmanagedFunctionPointer(CallingConvention.Cdecl)]
    internal unsafe delegate void MouseButtonCallback(WindowHandle window, int button, KeyAction action, KeyModifiers modifiers);

    [SuppressUnmanagedCodeSecurity, UnmanagedFunctionPointer(CallingConvention.Cdecl)]
    internal unsafe delegate void CursorPositionCallback(WindowHandle window, double x, double y);

    [SuppressUnmanagedCodeSecurity, UnmanagedFunctionPointer(CallingConvention.Cdecl)]
    internal unsafe delegate void CursorEnterCallback(WindowHandle window, [MarshalAs(UnmanagedType.Bool)] bool state);

    [SuppressUnmanagedCodeSecurity, UnmanagedFunctionPointer(CallingConvention.Cdecl)]
    internal unsafe delegate void ScrollCallback(WindowHandle window, double xOffset, double yOffset);

    [SuppressUnmanagedCodeSecurity, UnmanagedFunctionPointer(CallingConvention.Cdecl)]
    internal unsafe delegate void DropCallback(WindowHandle window, int pathCount, IntPtr pathNames);

    #endregion

    #region Monitor

    [SuppressUnmanagedCodeSecurity, UnmanagedFunctionPointer(CallingConvention.Cdecl)]
    internal unsafe delegate void MonitorCallback(MonitorHandle monitor, ConnectState state);

    #endregion

    [SuppressUnmanagedCodeSecurity, UnmanagedFunctionPointer(CallingConvention.Cdecl)]
    internal unsafe delegate void JoystickCallback(int id, ConnectState state);

    [SuppressUnmanagedCodeSecurity, UnmanagedFunctionPointer(CallingConvention.Cdecl)]
    internal unsafe delegate void ErrorCallback(int errorCode, [MarshalAs(UnmanagedType.LPStr)] string message);
}
