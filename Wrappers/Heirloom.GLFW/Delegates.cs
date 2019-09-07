using System;
using System.Runtime.InteropServices;
using System.Security;

namespace Heirloom.GLFW
{
    #region Window

    [SuppressUnmanagedCodeSecurity, UnmanagedFunctionPointer(CallingConvention.Cdecl)]
    public unsafe delegate void WindowPositionCallback([MarshalAs(UnmanagedType.Struct)] WindowHandle window, int x, int y);

    [SuppressUnmanagedCodeSecurity, UnmanagedFunctionPointer(CallingConvention.Cdecl)]
    public unsafe delegate void WindowSizeCallback([MarshalAs(UnmanagedType.Struct)] WindowHandle window, int width, int height);

    [SuppressUnmanagedCodeSecurity, UnmanagedFunctionPointer(CallingConvention.Cdecl)]
    public unsafe delegate void WindowCloseCallback([MarshalAs(UnmanagedType.Struct)] WindowHandle window);

    [SuppressUnmanagedCodeSecurity, UnmanagedFunctionPointer(CallingConvention.Cdecl)]
    public unsafe delegate void WindowRefreshCallback([MarshalAs(UnmanagedType.Struct)] WindowHandle window);

    [SuppressUnmanagedCodeSecurity, UnmanagedFunctionPointer(CallingConvention.Cdecl)]
    public unsafe delegate void WindowFocusCallback([MarshalAs(UnmanagedType.Struct)] WindowHandle window, [MarshalAs(UnmanagedType.Bool)] bool state);

    [SuppressUnmanagedCodeSecurity, UnmanagedFunctionPointer(CallingConvention.Cdecl)]
    public unsafe delegate void WindowIconifyCallback([MarshalAs(UnmanagedType.Struct)] WindowHandle window, [MarshalAs(UnmanagedType.Bool)] bool state);

    [SuppressUnmanagedCodeSecurity, UnmanagedFunctionPointer(CallingConvention.Cdecl)]
    public unsafe delegate void WindowMaximizeCallback([MarshalAs(UnmanagedType.Struct)] WindowHandle window, [MarshalAs(UnmanagedType.Bool)] bool state);

    [SuppressUnmanagedCodeSecurity, UnmanagedFunctionPointer(CallingConvention.Cdecl)]
    public unsafe delegate void FramebufferSizeCallback([MarshalAs(UnmanagedType.Struct)] WindowHandle window, int width, int height);

    [SuppressUnmanagedCodeSecurity, UnmanagedFunctionPointer(CallingConvention.Cdecl)]
    public unsafe delegate void WindowContentScaleCallback([MarshalAs(UnmanagedType.Struct)] WindowHandle window, float xScale, float yScale);

    [SuppressUnmanagedCodeSecurity, UnmanagedFunctionPointer(CallingConvention.Cdecl)]
    public unsafe delegate void KeyCallback([MarshalAs(UnmanagedType.Struct)] WindowHandle window, Key key, int scancode, ButtonAction action, KeyModifiers modifiers);

    [SuppressUnmanagedCodeSecurity, UnmanagedFunctionPointer(CallingConvention.Cdecl)]
    public unsafe delegate void CharCallback([MarshalAs(UnmanagedType.Struct)] WindowHandle window, uint codepoint);

    [SuppressUnmanagedCodeSecurity, UnmanagedFunctionPointer(CallingConvention.Cdecl)]
    public unsafe delegate void MouseButtonCallback([MarshalAs(UnmanagedType.Struct)] WindowHandle window, int button, ButtonAction action, KeyModifiers modifiers);

    [SuppressUnmanagedCodeSecurity, UnmanagedFunctionPointer(CallingConvention.Cdecl)]
    public unsafe delegate void CursorPositionCallback([MarshalAs(UnmanagedType.Struct)] WindowHandle window, double x, double y);

    [SuppressUnmanagedCodeSecurity, UnmanagedFunctionPointer(CallingConvention.Cdecl)]
    public unsafe delegate void CursorEnterCallback([MarshalAs(UnmanagedType.Struct)] WindowHandle window, [MarshalAs(UnmanagedType.Bool)] bool state);

    [SuppressUnmanagedCodeSecurity, UnmanagedFunctionPointer(CallingConvention.Cdecl)]
    public unsafe delegate void ScrollCallback([MarshalAs(UnmanagedType.Struct)] WindowHandle window, double xOffset, double yOffset);

    [SuppressUnmanagedCodeSecurity, UnmanagedFunctionPointer(CallingConvention.Cdecl)]
    public unsafe delegate void DropCallback([MarshalAs(UnmanagedType.Struct)] WindowHandle window, int pathCount, IntPtr pathNames);

    #endregion

    #region Monitor

    [SuppressUnmanagedCodeSecurity, UnmanagedFunctionPointer(CallingConvention.Cdecl)]
    public unsafe delegate void MonitorCallback([MarshalAs(UnmanagedType.Struct)] MonitorHandle monitor, ConnectState state);

    #endregion

    [SuppressUnmanagedCodeSecurity, UnmanagedFunctionPointer(CallingConvention.Cdecl)]
    public unsafe delegate void JoystickCallback(int id, ConnectState state);

    [SuppressUnmanagedCodeSecurity, UnmanagedFunctionPointer(CallingConvention.Cdecl)]
    public unsafe delegate void ErrorCallback(int errorCode, [MarshalAs(UnmanagedType.LPStr)] string message);
}
