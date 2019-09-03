using System.Runtime.InteropServices;
using System.Security;

namespace Heirloom.GLFW
{
    public static unsafe partial class Glfw
    {
        [SuppressUnmanagedCodeSecurity]
        [UnmanagedFunctionPointer(CallingConvention.Cdecl)]
        public unsafe delegate void WindowPositionCallback([MarshalAs(UnmanagedType.Struct)] Window window, int p1, int p2);

        [SuppressUnmanagedCodeSecurity]
        [UnmanagedFunctionPointer(CallingConvention.Cdecl)]
        public unsafe delegate void WindowSizeCallback([MarshalAs(UnmanagedType.Struct)] Window window, int p1, int p2);

        [SuppressUnmanagedCodeSecurity]
        [UnmanagedFunctionPointer(CallingConvention.Cdecl)]
        public unsafe delegate void WindowCloseCallback([MarshalAs(UnmanagedType.Struct)] Window window);

        [SuppressUnmanagedCodeSecurity]
        [UnmanagedFunctionPointer(CallingConvention.Cdecl)]
        public unsafe delegate void WindowRefreshCallback([MarshalAs(UnmanagedType.Struct)] Window window);

        [SuppressUnmanagedCodeSecurity]
        [UnmanagedFunctionPointer(CallingConvention.Cdecl)]
        public unsafe delegate void WindowFocusCallback([MarshalAs(UnmanagedType.Struct)] Window window, int p1);

        [SuppressUnmanagedCodeSecurity]
        [UnmanagedFunctionPointer(CallingConvention.Cdecl)]
        public unsafe delegate void WindowIconifyCallback([MarshalAs(UnmanagedType.Struct)] Window window, int p1);

        [SuppressUnmanagedCodeSecurity]
        [UnmanagedFunctionPointer(CallingConvention.Cdecl)]
        public unsafe delegate void WindowMaximizeCallback([MarshalAs(UnmanagedType.Struct)] Window window, int p1);

        [SuppressUnmanagedCodeSecurity]
        [UnmanagedFunctionPointer(CallingConvention.Cdecl)]
        public unsafe delegate void FramebufferSizeCallback([MarshalAs(UnmanagedType.Struct)] Window window, int p1, int p2);

        [SuppressUnmanagedCodeSecurity]
        [UnmanagedFunctionPointer(CallingConvention.Cdecl)]
        public unsafe delegate void WindowContentScaleCallback([MarshalAs(UnmanagedType.Struct)] Window window, float p1, float p2);

        [SuppressUnmanagedCodeSecurity]
        [UnmanagedFunctionPointer(CallingConvention.Cdecl)]
        public unsafe delegate void KeyCallback([MarshalAs(UnmanagedType.Struct)] Window window, int key, int scancode, int action, int modifiers);

        [SuppressUnmanagedCodeSecurity]
        [UnmanagedFunctionPointer(CallingConvention.Cdecl)]
        public unsafe delegate void CharCallback([MarshalAs(UnmanagedType.Struct)] Window window, uint codepoint);

        [SuppressUnmanagedCodeSecurity]
        [UnmanagedFunctionPointer(CallingConvention.Cdecl)]
        public unsafe delegate void CharModifierCallback([MarshalAs(UnmanagedType.Struct)] Window window, uint p1, int p2);

        [SuppressUnmanagedCodeSecurity]
        [UnmanagedFunctionPointer(CallingConvention.Cdecl)]
        public unsafe delegate void MouseButtonCallback([MarshalAs(UnmanagedType.Struct)] Window window, int button, int action, int mods);

        [SuppressUnmanagedCodeSecurity]
        [UnmanagedFunctionPointer(CallingConvention.Cdecl)]
        public unsafe delegate void CursorPositionCallback([MarshalAs(UnmanagedType.Struct)] Window window, double x, double y);

        [SuppressUnmanagedCodeSecurity]
        [UnmanagedFunctionPointer(CallingConvention.Cdecl)]
        public unsafe delegate void CursorEnterCallback([MarshalAs(UnmanagedType.Struct)] Window window, int p1);

        [SuppressUnmanagedCodeSecurity]
        [UnmanagedFunctionPointer(CallingConvention.Cdecl)]
        public unsafe delegate void ScrollCallback([MarshalAs(UnmanagedType.Struct)] Window window, double p1, double p2);

        [SuppressUnmanagedCodeSecurity]
        [UnmanagedFunctionPointer(CallingConvention.Cdecl)]
        public unsafe delegate void DropCallback([MarshalAs(UnmanagedType.Struct)] Window window, int p1, char** p2);

        [SuppressUnmanagedCodeSecurity]
        [UnmanagedFunctionPointer(CallingConvention.Cdecl)]
        public unsafe delegate void JoystickCallback(int p1, int p2);

        [SuppressUnmanagedCodeSecurity]
        [UnmanagedFunctionPointer(CallingConvention.Cdecl)]
        public unsafe delegate void MonitorCallback(Monitor* p0, int p1);

        [SuppressUnmanagedCodeSecurity]
        [UnmanagedFunctionPointer(CallingConvention.Cdecl)]
        public unsafe delegate void ErrorCallback(int errorCode, [MarshalAs(UnmanagedType.LPStr)] string message);

    }
}
